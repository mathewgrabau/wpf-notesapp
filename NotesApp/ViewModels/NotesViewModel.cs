using NotesApp.Models;
using NotesApp.ViewModels.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesApp.ViewModels
{
    public class NotesViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Notebook> Notebooks { get; set; }

        private Notebook _selectedNotebook;

        public Notebook SelectedNotebook
        {
            get { return _selectedNotebook; }
            set
            {
                _selectedNotebook = value;
                ReadNotes();
            }
        }

        private Note _selectedNote;

        public Note SelectedNote
        {
            get { return _selectedNote; }
            set
            {
                _selectedNote = value;
                SelectedNoteChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public ObservableCollection<Note> Notes { get; set; }

        public NewNotebookCommand NewNotebookCommand { get; set; }

        public NewNoteCommand NewNoteCommand { get; set; }

        public BeginEditCommand BeginEditCommand { get; set; }

        public HasEditedCommand HasEditedCommand { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler SelectedNoteChanged;

        private bool _isEditing;

        public bool IsEditing
        {
            get
            {
                return _isEditing;
            }
            set
            {
                _isEditing = value;
                OnPropertyChanged(nameof(IsEditing));
            }
        }

        public NotesViewModel()
        {
            IsEditing = false;

            NewNotebookCommand = new NewNotebookCommand(this);
            NewNoteCommand = new NewNoteCommand(this);
            BeginEditCommand = new BeginEditCommand(this);
            HasEditedCommand = new HasEditedCommand(this);

            // Init the variables
            Notebooks = new ObservableCollection<Notebook>();
            Notes = new ObservableCollection<Note>();

            // Init the different stuff.
            ReadNotebooks();
            ReadNotes();
        }

        void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void CreateNotebook()
        {
            var newNotebook = new Notebook
            {
                Name = "New Notebook",
                UserId = App.UserId
            };

            DatabaseHelper.Insert(newNotebook);

            // Refreshing
            ReadNotebooks();
        }

        /// <summary>
        /// Creates the note that should be added to the database.
        /// </summary>
        /// <param name="notebookId"></param>
        public void CreateNote(string notebookId)
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
            // Invoke the read notes to bind the list again
            ReadNotes();
        }

        public void ReadNotebooks()
        {
            using (var connection = new SQLite.SQLiteConnection(DatabaseHelper.DatabaseFile))
            {
                try
                {
                    string userId = App.UserId;
                    if (!string.IsNullOrEmpty(userId))
                    {
                        var notebooks = connection.Table<Notebook>().Where(x => x.UserId == userId).ToList();

                        Notebooks.Clear();
                        foreach (var notebook in notebooks)
                        {
                            Notebooks.Add(notebook);
                        }
                    }
                    else
                    {
                        Notebooks.Clear();
                    }
                }
                catch (SQLite.SQLiteException)
                {
                    // TODO logging or information should get shown here?

                    Notebooks.Clear();
                }
            }

            // Refreshing now
            ReadNotes();
        }

        public void ReadNotes()
        {
            if (SelectedNotebook != null)
            {
                using (var connection = new SQLite.SQLiteConnection(DatabaseHelper.DatabaseFile))
                {

                    var notes = connection.Table<Note>().Where(note => note.NotebookId == SelectedNotebook.Id).ToList();

                    Notes.Clear();
                    foreach (var note in notes)
                    {
                        Notes.Add(note);
                    }
                }
            }
        }

        public void StartEditing()
        {
            IsEditing = true;
        }

        /// <summary>
        /// Indicates that an instance has been renamed.
        /// </summary>
        /// <param name="notebook">The notebook instance that has been renamed</param>
        public void HasRenamed(Notebook notebook)
        {
            if (notebook == null)
            {
                return;
            }

            DatabaseHelper.Update(notebook);
            IsEditing = false;
            ReadNotebooks();
        }

        public void UpdateSelectedNote()
        {
            DatabaseHelper.Update(SelectedNote);
        }
    }
}
