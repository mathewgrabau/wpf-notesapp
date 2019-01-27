using NotesApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NotesApp.ViewModels.Commands
{
    public class LoginCommand : ICommand
    {
        public LoginViewModel ViewModel { get; set; }
        public event EventHandler CanExecuteChanged;

        public LoginCommand(LoginViewModel loginViewModel)
        {
            ViewModel = loginViewModel;
        }

        public bool CanExecute(object parameter)
        {
            var user = parameter as User;
// Just for testing
#if false
            // No binding, cannot execute.
            if (user == null)
            {
                return false;
            }

            // Ensuring both are provided.
            if (string.IsNullOrEmpty(user.Username))
            {
                return false;
            }

            if (string.IsNullOrEmpty(user.Password))
            {
                return false;
            }
#endif

            return true;
        }

        public void Execute(object parameter)
        {
            // TOOD: Login functionality
            ViewModel.Login();
        }
    }
}
