// Project.ViewModels/LoginViewModel.cs
using Microsoft.Extensions.DependencyInjection; // Add this for GetRequiredService
using Project.DAO; // Correct DAO namespace
using Project.Enums; // Correct Enums namespace
using Project.Models; // Add this for the User class
using Project.View; // Add this for the windows
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace Project.ViewModels // Correct namespace   
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private readonly UserDAO _userDAO;

        private string _email = string.Empty;
        private string _password = string.Empty;
        private Role _selectedRole;
        private string _errorMessage = string.Empty;

        public string Email
        {
            get { return _email; }
            set { _email = value; OnPropertyChanged(nameof(Email)); }
        }

        public string Password
        {
            get { return _password; }
            set { _password = value; OnPropertyChanged(nameof(Password)); }
        }

        public Role SelectedRole
        {
            get { return _selectedRole; }
            set { _selectedRole = value; OnPropertyChanged(nameof(SelectedRole)); }
        }

        public string ErrorMessage
        {
            get { return _errorMessage; }
            set { _errorMessage = value; OnPropertyChanged(nameof(ErrorMessage)); }
        }

        public ObservableCollection<Role> Roles { get; } = new ObservableCollection<Role>(Enum.GetValues(typeof(Role)).Cast<Role>());

        public ICommand LoginCommand { get; }

        // Constructor (using dependency injection)
        public LoginViewModel(IServiceProvider serviceProvider)
        {
            _userDAO = serviceProvider.GetRequiredService<UserDAO>();
            LoginCommand = new AsyncRelayCommand(Login);
            SelectedRole = Role.Citizen;
        }

        private async Task Login()
        {
            try
            {
                ErrorMessage = string.Empty;
                var user = await _userDAO.AuthenticateUser(Email, Password, SelectedRole);

                if (user != null)
                {
                    Window nextWindow = null;
                    switch (user.Role)
                    {
                        case Role.Admin:
                            nextWindow = new AdminWindow();
                            break;
                        case Role.Police:
                            nextWindow = new PoliceWindow();
                            break;
                        case Role.AreaLeader:
                            nextWindow = new AreaLeaderWindow();
                            break;
                        case Role.Citizen:
                            nextWindow = new CitizenWindow();
                            break;
                    }

                    if (nextWindow != null)
                    {
                        nextWindow.Show();
                        Application.Current.MainWindow.Close();
                    }
                }
                else
                {
                    ErrorMessage = "Invalid email, password, or role.";
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = "An error occurred during login.";
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class AsyncRelayCommand : ICommand
    {
        private readonly Func<Task> _execute;
        private readonly Func<bool>? _canExecute;
        private bool _isExecuting;

        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public AsyncRelayCommand(Func<Task> execute, Func<bool>? canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute(object? parameter)
        {
            return !_isExecuting && (_canExecute?.Invoke() ?? true);
        }

        public async void Execute(object? parameter)
        {
            if (_isExecuting)
                return;

            _isExecuting = true;
            try
            {
                await _execute();
            }
            finally
            {
                _isExecuting = false;
            }
        }
    }
}
