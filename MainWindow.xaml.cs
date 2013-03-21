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

        // --- OPEN FILE UI ---

        private void summonOpenDialog(TextBox target)
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
            summonOpenDialog(unencryptedFileName);
        }

        private void browseEncryptedFileButton_Click(object sender, RoutedEventArgs e)
        {
            summonOpenDialog(encryptedFileName);
        }

        // --- SAVE FILE UI ---

        private void summonSaveDialog(TextBox target, TextBox source = null)
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
            summonSaveDialog(encryptionDestinationFileName, unencryptedFileName);
        }

        private void browseDecryptionDestinationFileButton_Click(object sender, RoutedEventArgs e)
        {
            summonSaveDialog(decryptionDestinationFileName, encryptedFileName);
        }

        private void encryptButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void decryptButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
