
using Athena_Prison_PoC.Commands;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Athena_Prison_PoC.ViewModel
{
    public class ScannedViewModel : BaseViewModel
    {
        private Uri _pdfUri;

        public Uri PdfUri
        {
            get { return _pdfUri; }
            set
            {
                _pdfUri = value;
                OnPropertyChanged(nameof(PdfUri));
            }
        }

        // Add home-specific logic here
        public ScannedViewModel()
        {
            // Load the PDF file path as needed
            LoadPdf("menu-form");
        }

        private void LoadPdf(string fileName)
        {
            var enviroment = System.Environment.CurrentDirectory;
            string appDirectory = Directory.GetParent(enviroment).Parent.FullName;

            string filePath = Path.Combine(appDirectory, "Data", fileName+".pdf");
           // MessageBox.Show(filePath);
            try
            {
                if (File.Exists(filePath))
                {
                    PdfUri = new Uri(filePath);
                }
                else
                {
                    MessageBox.Show("PDF file not found.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading PDF: {ex.Message}");
            }
        }
    }
}
