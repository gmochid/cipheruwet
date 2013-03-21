using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace cipheruwet
{
    /// <summary>
    /// Interaction logic for ReaderWindow.xaml
    /// </summary>
    public partial class ReaderWindow : Window
    {
        public string FileName;
        public string Extension;
        public string Error;
        private bool IsText;

        string[] textExtensions = {".txt", ".text", ".html", ".htm", ".xhtml",
                                   ".xml", ".plist", ".xaml", ".rdf", ".rss",
                                   ".atom", ".c", ".h", ".cs", ".cpp", ".java",
                                   ".py", ".pl", ".php", ".gitignore", ".manifest",
                                   ".js", ".css", ".asp", ".aspx", ".jsp", ".rb",
                                   ".rhtml"};

        public ReaderWindow(string FileName = null)
        {
            InitializeComponent();

            if (FileName != null)
            {
                LoadFile(FileName);
            }
        }

        public void LoadFile(string fileName)
        {
            if (File.Exists(fileName))
            {
                this.FileName = fileName;
                Extension = System.IO.Path.GetExtension(fileName);

                Title = System.IO.Path.GetFileName(fileName);

                // Some text file extensions

                if (textExtensions.Contains(Extension))
                {
                    IsText = true;
                }
                else
                {
                    IsText = false;
                }
            }
            else
            {
                throw new Exception("File not found.");
            }
        }

        public void ProcessFile()
        {
            // If error occurs, throw an exception
            if (File.Exists(FileName))
            {
                // File exists.
                if (IsText)
                {
                    using (StreamReader sr = new StreamReader(FileName))
                    {
                        while (sr.Peek() >= 0)
                        {
                            string line = sr.ReadLine();
                            fileContents.AppendText(line + "\n");
                        }
                    }
                }
                else
                {
                    FileStream f = File.OpenRead(FileName);
                    using (BinaryReader br = new BinaryReader(f))
                    {
                        int pos = 0;
                        int length = (int) f.Length;

                        while (pos < length)
                        {
                            // Display each byte in hex
                            byte v = br.ReadByte();
                            fileContents.AppendText(v.ToString("X2") + " ");

                            pos += sizeof(byte);
                        }
                    }
                }
            }
            else
            {
                throw new Exception("File not found.");
            }
        }
    }
}
