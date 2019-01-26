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
    /// Interaction logic for Notebook.xaml
    /// </summary>
    public partial class NotebookControl : UserControl
    {
        /// <summary>
        ///  The bound/displayed notebook associated to the control.
        /// </summary>
        public NotesApp.Models.Notebook Notebook
        {
            get { return (Models.Notebook)GetValue(NotebookProperty); }
            set { SetValue(NotebookProperty, value); }
        }

        // Dependency property
        public static readonly DependencyProperty NotebookProperty =
            DependencyProperty.Register("Notebook", typeof(Models.Notebook), typeof(NotebookControl), new PropertyMetadata(null, SetValues));


        public NotebookControl()
        {
            InitializeComponent();
        }


        private static void SetValues(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            NotebookControl notebookControl = d as NotebookControl;

            if (notebookControl != null)
            {
                notebookControl.nameTextBlock.Text = (e.NewValue as Models.Notebook).Name;
            }
        }
    }
}
