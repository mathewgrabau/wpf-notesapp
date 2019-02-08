using NotesApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NotesApp.ViewModels.Commands
{
    public class BeginEditCommand : ICommand
    {
        public NotesViewModel ViewModel { get; set; }

        public event EventHandler CanExecuteChanged;

        public BeginEditCommand(NotesViewModel viewModel)
        {
            ViewModel = viewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            ViewModel.StartEditing();
        }
    }
    public class HasEditedCommand : ICommand
    {
        public NotesViewModel ViewModel { get; set; }

        public event EventHandler CanExecuteChanged;

        public HasEditedCommand(NotesViewModel viewModel)
        {
            ViewModel = viewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            Notebook notebook = parameter as Notebook;
            ViewModel.HasRenamedAsync(notebook);
        }
    }
}
