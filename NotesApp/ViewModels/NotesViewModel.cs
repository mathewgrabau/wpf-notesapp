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

            // Init the variables
            Notebooks = new ObservableCollection<Notebook>();
            Notes = new ObservableCollection<Note>();

            // Init the different stuff.
            ReadNotebooks();
        }

        public void CreateNotebook()
        {
            var newNotebook = new Notebook
            {
                Name = "New Notebook"
            };

            DatabaseHelper.Insert(newNotebook);
        }

        /// <summary>
        /// Creates the note that should be added to the database.
        /// </summary>
        /// <param name="notebookId"></param>
        public void CreateNote(int notebookId)
        {
            var newNote = new Note()
            {
                NotebookId = notebookId,
                CreatedTime = DateTime.Now,
                UpdatedTime = DateTime.Now,
                Title = "New Note"
            };

            // Adding it into the database now
            DatabaseHelper.Insert(newNote);
        }

        public void ReadNotebooks()
        {
            using (var connection = new SQLite.SQLiteConnection(DatabaseHelper.DatabaseFile))
            {
                try
                {
                    var notebooks = connection.Table<Notebook>().ToList();

                    Notebooks.Clear();
                    foreach (var notebook in notebooks)
                    {
                        Notebooks.Add(notebook);
                    }
                }
                catch (SQLite.SQLiteException)
                {
                    // TODO logging or information should get shown here?

                    Notebooks.Clear();
                }
            }
        }

        public void ReadNotes()
        {
            using (var connection = new SQLite.SQLiteConnection(DatabaseHelper.DatabaseFile))
            {
                if (SelectedNotebook != null)
                {
                    var notes = connection.Table<Note>().Where(note => note.Id == SelectedNotebook.Id).ToList();

                    Notes.Clear();
                    foreach (var note in notes)
                    {
                        Notes.Add(note);
                    }
                }
            }
        }
    }
}
