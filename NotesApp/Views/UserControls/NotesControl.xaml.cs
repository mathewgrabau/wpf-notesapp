using NotesApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NotesApp.Views.UserControls
{
    /// <summary>
    /// Interaction logic for NotesControl.xaml
    /// </summary>
    public partial class NotesControl : UserControl
    {
        public Note Note
        {
            get
            {
                return (Note)GetValue(NoteProperty);
            }
            set
            {
                SetValue(NoteProperty, value);
            }
        }

        public static readonly DependencyProperty NoteProperty = 
            DependencyProperty.Register("Note", typeof(Note), typeof(NotesControl), new PropertyMetadata(null, SetValues));

        private static void SetValues(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            NotesControl note = d as NotesControl;

            if (note != null)
            {
                note.titleTextBlock.Text = (e.NewValue as Note).Title;
                note.editedTextBlock.Text = (e.NewValue as Note).UpdatedTime.ToShortDateString();
                note.contentTextBlock.Text = (e.NewValue as Note).Title;
            }
        }

        public NotesControl()
        {
            InitializeComponent();
        }
    }
}
