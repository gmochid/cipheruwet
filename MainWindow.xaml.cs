using System;
using System.Collections.Generic;
using System.IO;
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
using Microsoft.Win32;

namespace cipheruwet
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        
        // --- GENERIC UI ---
        private void ShowMessageBox(string message,
            MessageBoxButton button = MessageBoxButton.OK,
            MessageBoxImage icon = MessageBoxImage.Error)
        {
            string messageBoxText = message;
            string caption = "Cipheruwet";
            MessageBox.Show(messageBoxText, caption, button, icon);
        }

        // --- OPEN FILE UI ---

        private void ShowOpenDialog(TextBox target)
        {
            // Instantiate open file dialog
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.FileName = "";
            dlg.DefaultExt = "";
            dlg.Filter = "All files (*.*)|*.*";

            // Show dialog
            Nullable<bool> result = dlg.ShowDialog();

            // Process choice
            if (result == true)
            {
                string filename = dlg.FileName;
                target.Text = filename;
            }
        }

        private void browseUnencryptedFileButton_Click(object sender, RoutedEventArgs e)
        {
            ShowOpenDialog(unencryptedFileName);
        }

        private void browseEncryptedFileButton_Click(object sender, RoutedEventArgs e)
        {
            ShowOpenDialog(encryptedFileName);
        }

        // --- SAVE FILE UI ---

        private void ShowSaveDialog(TextBox target, TextBox source = null)
        {
            // Source file name
            String sourceDirectoryName = System.IO.Path.GetDirectoryName(source.Text);
            String sourceFileNameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(source.Text);
            String sourceExtension = System.IO.Path.GetExtension(source.Text);

            // Instantiate save file dialog
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.InitialDirectory = sourceDirectoryName;
            dlg.FileName = "";
            dlg.DefaultExt = sourceExtension;
            dlg.Filter = "All files (*.*)|*.*";

            // Show dialog
            Nullable<bool> result = dlg.ShowDialog();

            // Process choice
            if (result == true)
            {
                string filename = dlg.FileName;
                target.Text = filename;
            }
        }

        private void browseEncryptionDestinationFileButton_Click(object sender, RoutedEventArgs e)
        {
            ShowSaveDialog(encryptionDestinationFileName, unencryptedFileName);
        }

        private void browseDecryptionDestinationFileButton_Click(object sender, RoutedEventArgs e)
        {
            ShowSaveDialog(decryptionDestinationFileName, encryptedFileName);
        }

        private void encryptButton_Click(object sender, RoutedEventArgs e)
        {
            // TODO Multihreading
            try
            {
                string sourceFileName = unencryptedFileName.Text;
                string destinationFileName = encryptionDestinationFileName.Text;
                string key = encryptionKey.Text;
                string mode = encryptionMode.Text;

                ShowMessageBox(mode);
                // Engine.StartEncryption(
            }
            catch (Exception exc)
            {
                ShowMessageBox(exc.Message);
            }
        }

        private void decryptButton_Click(object sender, RoutedEventArgs e)
        {

        }

        // --- READ FILE UI ---

        private void ShowReaderWindow(TextBox target)
        {
            string FileName = target.Text;
            ReaderWindow rw = new ReaderWindow();

            try
            {
                rw.LoadFile(FileName);
                rw.Show();
                rw.ProcessFile();
            }
            catch (Exception e)
            {
                rw.Close();
                ShowMessageBox(e.Message);
            }
        }

        private void openUnencryptedFileButton_Click(object sender, RoutedEventArgs e)
        {
            ShowReaderWindow(unencryptedFileName);
        }

        private void openEncryptionDestinationFileButton_Click(object sender, RoutedEventArgs e)
        {
            ShowReaderWindow(encryptionDestinationFileName);
        }

        private void openEncryptedFileButton_Click(object sender, RoutedEventArgs e)
        {
            ShowReaderWindow(encryptedFileName);
        }

        private void openDecryptionDestinationFileButton_Click(object sender, RoutedEventArgs e)
        {
            ShowReaderWindow(decryptionDestinationFileName);
        }
    }
}
