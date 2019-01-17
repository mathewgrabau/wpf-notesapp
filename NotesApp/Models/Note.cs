using System;
using System.ComponentModel;

namespace NotesApp.Models
{
    public class Note : INotifyPropertyChanged
    {

        private int _id;

        public int Id
        {
            get { return _id; }
            set
            {
                _id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        private string _title;

        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;

                OnPropertyChanged(nameof(Title));
            }
        }

        private DateTime _createdTime;

        public DateTime CreatedTime
        {
            get { return _createdTime; }
            set
            {
                _createdTime = value;
                OnPropertyChanged(nameof(UpdatedTime));
            }
        }

        private DateTime _updatedTime;

        public DateTime UpdatedTime
        {
            get { return _updatedTime; }
            set
            {
                _updatedTime = value;
                OnPropertyChanged(nameof(UpdatedTime));
            }
        }

        private string _fileLocation;

        public string FileLocation
        {
            get { return _fileLocation; }
            set
            {
                _fileLocation = value;
                OnPropertyChanged(nameof(FileLocation));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}