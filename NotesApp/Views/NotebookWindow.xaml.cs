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
using System.Windows.Shapes;

namespace NotesApp.Views
{
    /// <summary>
    /// Interaction logic for NotebookWindow.xaml
    /// </summary>
    public partial class NotebookWindow : Window
    {
        public NotebookWindow()
        {
            InitializeComponent();
        }

        private void ExitItem_Click(object sender, RoutedEventArgs e)
        {
            // Triggers the shut down
            Application.Current.Shutdown();
        }
    }
}
