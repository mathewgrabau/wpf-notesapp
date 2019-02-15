using NotesApp.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Speech.Recognition;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.WindowsAzure.Storage;

namespace NotesApp.Views
{
    /// <summary>
    /// Interaction logic for NotebookWindow.xaml
    /// </summary>
    public partial class NotebookWindow : Window
    {
        SpeechRecognitionEngine _recognizer;

        NotesViewModel _viewModel;

        public NotebookWindow()
        {
            InitializeComponent();

            //_viewModel = new NotesViewModel();
            _viewModel = this.Resources["viewModel"] as NotesViewModel;
            containerDockPanel.DataContext = _viewModel;
            _viewModel.SelectedNoteChanged += _viewModel_SelectedNoteChanged;

            // Setup the event handlers for responding to the application events
            var currentCulture = (from r in SpeechRecognitionEngine.InstalledRecognizers()
                                  where r.Culture.Equals(Thread.CurrentThread.CurrentCulture)
                                  select r).FirstOrDefault();
            if (currentCulture != null)
            {
                _recognizer = new SpeechRecognitionEngine(currentCulture);

                var builder = new GrammarBuilder();
                builder.AppendDictation();
                var grammar = new Grammar(builder);

                // Do the configuration now.
                _recognizer.LoadGrammar(grammar);
                _recognizer.SetInputToDefaultAudioDevice();
                _recognizer.SpeechRecognized += _recognizer_SpeechRecognized;
            }
            else
            {
                speechButton.IsEnabled = false;
            }

            // Need the list of the fonts
            var fonts = Fonts.SystemFontFamilies.OrderBy(f=>f.Source);
            fontFamilyComboBox.ItemsSource = fonts;

            List<double> fontSizes = new List<double>() { 8, 9, 10, 11, 12, 14, 16, 18, 20, 22, 24 };
            fontSizeComboBox.ItemsSource = fontSizes;
        }

        private void _viewModel_SelectedNoteChanged(object sender, EventArgs e)
        {
            contentEditorRichTextBox.Document.Blocks.Clear();
            if (!string.IsNullOrEmpty(_viewModel.SelectedNote.FileLocation))
            {
                // TODO - exception handling?
                using (var fileStream = new FileStream(_viewModel.SelectedNote.FileLocation, FileMode.Open))
                {
                    var range = new TextRange(contentEditorRichTextBox.Document.ContentStart, contentEditorRichTextBox.Document.ContentEnd);
                    range.Load(fileStream, DataFormats.Rtf);
                }
            }
        }

        // Used to prevent the login window being able to work/be taken into account here.
        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);

            if (string.IsNullOrEmpty(App.UserId))
            {
                var loginWindow = new LoginWindow();
                loginWindow.ShowDialog();

                _viewModel.ReadNotebooks();
            }
        }

        private void _recognizer_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            string recognizedText = e.Result.Text;

            contentEditorRichTextBox.Document.Blocks.Add(new Paragraph(new Run(recognizedText)));
        }

        private void ExitItem_Click(object sender, RoutedEventArgs e)
        {
            // Triggers the shut down
            Application.Current.Shutdown();
        }
        
        /// <summary>
        /// Updates the character count.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ContentEditorRichTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            int numberOfCharacters = new TextRange(contentEditorRichTextBox.Document.ContentStart, contentEditorRichTextBox.Document.ContentEnd).Text.Length;

            statusTextBlock.Text = $"Document length: {numberOfCharacters} characters";
        }

        private void BoldButton_Click(object sender, RoutedEventArgs e)
        {
            var isButtonEnabled = (sender as ToggleButton).IsChecked.GetValueOrDefault();

            if (isButtonEnabled)
            {
                contentEditorRichTextBox.Selection.ApplyPropertyValue(Inline.FontWeightProperty, FontWeights.Bold);
            }
            else
            {
                contentEditorRichTextBox.Selection.ApplyPropertyValue(Inline.FontWeightProperty, FontWeights.Normal);
            }
        }

        private void SpeechButton_Click(object sender, RoutedEventArgs e)
        {
            var isButtonChecked = (sender as ToggleButton).IsChecked.GetValueOrDefault();
            if (isButtonChecked)
            {
                _recognizer.RecognizeAsync(RecognizeMode.Multiple);
            }
            else
            {
                _recognizer.RecognizeAsyncStop();
            }
        }

        private void ContentEditorRichTextBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            var selectedState = contentEditorRichTextBox.Selection.GetPropertyValue(Inline.FontWeightProperty);
            boldButton.IsChecked = selectedState != DependencyProperty.UnsetValue && selectedState.Equals(FontWeights.Bold);

            var italicState = contentEditorRichTextBox.Selection.GetPropertyValue(Inline.FontStyleProperty);
            italicButton.IsChecked = italicState != DependencyProperty.UnsetValue && italicState.Equals(FontStyles.Italic);

            var underlineState = contentEditorRichTextBox.Selection.GetPropertyValue(Inline.TextDecorationsProperty);
            underlineButton.IsChecked = underlineState != DependencyProperty.UnsetValue && underlineState.Equals(TextDecorations.Underline);

            // Font settings now (note the handling for the size combo box - a bit more robust than what was present before.
            fontFamilyComboBox.SelectedItem = contentEditorRichTextBox.Selection.GetPropertyValue(Inline.FontFamilyProperty);
            var fontSizeValue = contentEditorRichTextBox.Selection.GetPropertyValue(Inline.FontSizeProperty);
            fontSizeComboBox.Text = fontSizeValue != DependencyProperty.UnsetValue ? fontSizeValue.ToString() : string.Empty;
        }

        private void ItalicButton_Click(object sender, RoutedEventArgs e)
        {
            var isButtonEnabled = (sender as ToggleButton).IsChecked.GetValueOrDefault();

            if (isButtonEnabled)
            {
                contentEditorRichTextBox.Selection.ApplyPropertyValue(Inline.FontStyleProperty, FontStyles.Italic);
            }
            else
            {
                contentEditorRichTextBox.Selection.ApplyPropertyValue(Inline.FontStyleProperty, FontStyles.Normal);
            }
        }

        private void UnderlineButton_Click(object sender, RoutedEventArgs e)
        {
            var isButtonEnabled = (sender as ToggleButton).IsChecked.GetValueOrDefault();

            if (isButtonEnabled)
            {
                contentEditorRichTextBox.Selection.ApplyPropertyValue(Inline.TextDecorationsProperty, TextDecorations.Underline);
            }
            else
            {
                contentEditorRichTextBox.Selection.ApplyPropertyValue(Inline.TextDecorationsProperty, null);
            }
        }

        private void FontFamilyComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (fontFamilyComboBox.SelectedItem != null)
            {
                contentEditorRichTextBox.Selection.ApplyPropertyValue(Inline.FontFamilyProperty, fontFamilyComboBox.SelectedItem);
            }
        }

        private void FontSizeComboBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            int intValue;
            double doubleValue;
            if (int.TryParse(fontSizeComboBox.Text, out intValue) || double.TryParse(fontSizeComboBox.Text, out doubleValue))
            {
                contentEditorRichTextBox.Selection.ApplyPropertyValue(Inline.FontSizeProperty, fontSizeComboBox.Text);
            }
        }

        private void SaveFileButton_Click(object sender, RoutedEventArgs e)
        {
            string noteFileName = $"{_viewModel.SelectedNote.Id}.rtf";
            string noteFile = System.IO.Path.Combine(Environment.CurrentDirectory, $"{_viewModel.SelectedNote.Id}.rtf");
           _viewModel.SelectedNote.FileLocation = noteFile;

            // Ensuring proper file access here.
            using (var rtfStream = new FileStream(noteFile, FileMode.Create))
            {
                var textRange = new TextRange(contentEditorRichTextBox.Document.ContentStart, contentEditorRichTextBox.Document.ContentEnd);
                textRange.Save(rtfStream, DataFormats.Rtf);
            }

            UploadFile(noteFile);

            _viewModel.UpdateSelectedNoteAsync();
        }

        private async void UploadFile(string noteFileLocation, string noteFileName)
        {
            // NOTE: need to add system files ehre.
            var account = CloudStorageAccount.Parse("");
            var blobClient = account.CreateCloudBlobClient();

            var notesContainer = blobClient.GetContainerReference("notes");  // The name matching is important here.
            await notesContainer.CreateIfNotExistsAsync();

            using (var rtfStream = new FileStream(noteFile, FileMode.Open))
            {
                // Need the container now to be able to move this stuff here.
            }
        }
    }
}
