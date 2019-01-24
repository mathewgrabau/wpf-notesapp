using System;
using System.Collections.Generic;
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

namespace NotesApp.Views
{
    /// <summary>
    /// Interaction logic for NotebookWindow.xaml
    /// </summary>
    public partial class NotebookWindow : Window
    {
        SpeechRecognitionEngine _recognizer;

        public NotebookWindow()
        {
            InitializeComponent();

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

        }
    }
}
