// App.xaml.cs
using Microsoft.Extensions.DependencyInjection;
using Project.DAO;
using Project.Models;
using Project.View;
using Project.ViewModels;
using System.Windows;

namespace Project
{
    public partial class App : Application
    {
        private ServiceProvider serviceProvider;

        public App()
        {
            var services = new ServiceCollection();
            ConfigureServices(services);
            serviceProvider = services.BuildServiceProvider();
        }

        public IServiceProvider Services
        {
            get { return serviceProvider; }
        }

        private void ConfigureServices(IServiceCollection services)
        {
            // Register DbContext
            services.AddDbContext<PrnContext>();

            // Register services
            services.AddScoped<UserDAO>();
            services.AddScoped<LoginViewModel>();

            // Register views
            services.AddTransient<LoginWindow>();
            services.AddTransient<AdminWindow>();
            services.AddTransient<PoliceWindow>();
            services.AddTransient<AreaLeaderWindow>();
            services.AddTransient<CitizenWindow>();
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            try
            {
                // Create admin account
                using (var scope = serviceProvider.CreateScope())
                {
                    var userDAO = scope.ServiceProvider.GetRequiredService<UserDAO>();
                    userDAO.CreateAdminAccount().Wait();
                }

                // Get LoginWindow from DI container
                var loginWindow = serviceProvider.GetRequiredService<LoginWindow>();
                loginWindow.Show();
                MainWindow = loginWindow;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Startup error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Shutdown();
            }
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            serviceProvider.Dispose();
        }
    }
}
