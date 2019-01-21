using NotesApp.Models;
using NotesApp.ViewModels.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesApp.ViewModels
{
    public class NotesViewModel
    {
        public ObservableCollection<Notebook> Notebooks { get; set; }

        private Notebook _selectedNotebook;

        public Notebook SelectedNotebook
        {
            get { return _selectedNotebook; }
            set
            {
                _selectedNotebook = value;

                // TODO get the notes
            }
        }

        public ObservableCollection<Note> Notes { get; set; }

        public NewNotebookCommand NewNotebookCommand { get; set; }

        public NewNoteCommand NewNoteCommand { get; set; }

        public NotesViewModel()
        {
            NewNotebookCommand = new NewNotebookCommand(this);
            NewNoteCommand = new NewNoteCommand(this);
        }

        public void CreateNotebook()
        {
            var newNotebook = new Notebook
            {
                Name = "New Notebook"
            };

            DatabaseHelper.Insert(new Notebook);

        }

        public void CreateNote(int notebookId)
        {
            var newNote = new Note()
            {
                NotebookId = notebookId,
                CreatedTime = DateTime.Now,
                UpdatedTime = DateTime.Now
            };

            // Adding it into the database now
            DatabaseHelper.Insert(newNote);
        }
    }
}
