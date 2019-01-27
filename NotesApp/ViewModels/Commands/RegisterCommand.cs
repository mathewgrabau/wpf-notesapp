using NotesApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NotesApp.ViewModels.Commands
{
    public class RegisterCommand : ICommand
    {
        public LoginViewModel ViewModel { get; set; }
        public event EventHandler CanExecuteChanged;

        public RegisterCommand(LoginViewModel loginViewModel)
        {
            ViewModel = loginViewModel;
        }

        public bool CanExecute(object parameter)
        {
            var user = parameter as User;

            // TODO I want to figure out how to make these work properly (big gap in the video there)
#if false
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

            if (string.IsNullOrEmpty(user.Email))
            {
                return false;
            }

            if (string.IsNullOrEmpty(user.LastName))
            {
                return false;
            }

            if (string.IsNullOrEmpty(user.FirstName))
            {
                return false;
            }
#endif

            return true;
        }

        public void Execute(object parameter)
        {
            // TOOD: Login functionality
            ViewModel.Register();
        }
    }
}
