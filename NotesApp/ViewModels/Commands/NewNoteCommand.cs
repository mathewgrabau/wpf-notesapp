using NotesApp.Models;
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
            Notebook selected = parameter as Notebook;
            return selected != null;
        }

        public void Execute(object parameter)
        {
            Notebook selectedNotebook = parameter as Notebook;
            if (selectedNotebook != null)
            {
                ViewModel.CreateNote(selectedNotebook.Id);
            }
        }
    }
}
