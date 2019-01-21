using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NotesApp.ViewModels.Commands
{
    public class NewNotebookCommand : ICommand
    {
        public NotesViewModel ViewModel { get; set; }
        public event EventHandler CanExecuteChanged;

        public NewNotebookCommand(NotesViewModel viewModel)
        {
            ViewModel = viewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            // TOOD: Login functionality

            ViewModel.CreateNotebook();
        }
    }
}
