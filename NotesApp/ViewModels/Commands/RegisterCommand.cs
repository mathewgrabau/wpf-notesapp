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
            return true;
        }

        public void Execute(object parameter)
        {
            // TOOD: Login functionality
        }
    }
}
