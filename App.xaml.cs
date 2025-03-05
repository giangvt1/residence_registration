// Project/App.xaml.cs
using Microsoft.EntityFrameworkCore; // IMPORTANT: Add this for UseSqlServer
using Microsoft.Extensions.Configuration; // IMPORTANT: Add this for configuration
using Microsoft.Extensions.DependencyInjection;
using Project.DAO;
using Project.Models;
using Project.View;
using Project.ViewModels;
using System.IO; // For Directory.GetCurrentDirectory
using System.Windows;

namespace Project
{
    public partial class App : Application
    {
        private IServiceProvider serviceProvider; // Change to IServiceProvider

        public App()
        {
            // No need to create ServiceCollection here. Do it in OnStartup.
        }


        private void ConfigureServices(IServiceCollection services)
        {
            // --- Configure EF Core with the connection string ---
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory()) // Or AppContext.BaseDirectory
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true) // Make sure appsettings.json is copied to output
                .Build();

            services.AddDbContext<PrnContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("Default"))); // Use "Default", or your connection string name

            // Register services
            services.AddTransient<UserDAO>(); // Use AddTransient for ViewModels/Windows
            services.AddTransient<LoginViewModel>();

            // Register views
            services.AddTransient<LoginWindow>();
            services.AddTransient<AdminWindow>();
            services.AddTransient<PoliceWindow>();
            services.AddTransient<AreaLeaderWindow>();
            services.AddTransient<CitizenWindow>();
        }

        //In App.xaml.cs
        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e); // Call base.OnStartup FIRST

            // Now create the ServiceCollection and configure services
            var services = new ServiceCollection();
            ConfigureServices(services);
            serviceProvider = services.BuildServiceProvider();
            //Run migration
            using (var scope = serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<PrnContext>();
                dbContext.Database.Migrate();
            }

            // Get LoginWindow from DI container and show it
            try
            {
                //Create admin account
                using (var scope = serviceProvider.CreateScope())
                {
                    var userDAO = scope.ServiceProvider.GetRequiredService<UserDAO>();
                    await userDAO.CreateAdminAccount(); // AWAIT the call
                }
                var loginWindow = serviceProvider.GetRequiredService<LoginWindow>();
                MainWindow = loginWindow; // Set MainWindow *before* showing
                loginWindow.Show();
            }
            catch (Exception ex)
            {
                // Log the FULL exception details (for debugging)
                MessageBox.Show($"Startup error: {ex.Message}\n\n{ex.StackTrace}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Shutdown();
            }


        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            if (serviceProvider is IDisposable disposableServiceProvider)
            {
                disposableServiceProvider.Dispose();
            }
        }
    }
}