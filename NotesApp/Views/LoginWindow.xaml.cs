using NotesApp.ViewModels;
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
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();

            // Create the view model
            var viewModel = new LoginViewModel();
            containerGrid.DataContext = viewModel;
            viewModel.HasLoggedIn += ViewModel_HasLoggedIn;
        }

        private void ViewModel_HasLoggedIn(object sender, EventArgs e)
        {
            // Close the window now
            Close();
        }

        private void HaveAccountButton_Click(object sender, RoutedEventArgs e)
        {
            registerStackPanel.Visibility = Visibility.Collapsed;
            loginStackPanel.Visibility = Visibility.Visible;
        }

        private void NoAccountButton_Click(object sender, RoutedEventArgs e)
        {
            loginStackPanel.Visibility = Visibility.Collapsed;
            registerStackPanel.Visibility = Visibility.Visible;
        }
    }
}
