using System;
using System.Windows.Input;

namespace NotesApp.ViewModels.Commands
{
    public class NewNoteCommand : ICommand
    {
        public NotesViewModel ViewModel { get; set; }
        public event EventHandler CanExecuteChanged;

        public NewNoteCommand(NotesViewModel viewModel)
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
        }
    }
}
