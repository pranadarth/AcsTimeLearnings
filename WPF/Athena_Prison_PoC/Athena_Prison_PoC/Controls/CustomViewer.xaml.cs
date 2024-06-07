using Athena_Prison_PoC.Commands;
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

namespace Athena_Prison_PoC.Controls
{
    /// <summary>
    /// Interaction logic for CustomViewer.xaml
    /// </summary>
    public partial class CustomViewer : UserControl
    {
        public CustomViewer()
        {
            InitializeComponent();
        }
        public static readonly DependencyProperty PdfUriProperty =
           DependencyProperty.Register("PdfUri", typeof(Uri), typeof(CustomViewer), new PropertyMetadata(null, OnPdfUriChanged));

        public Uri PdfUri
        {
            get { return (Uri)GetValue(PdfUriProperty); }
            set { SetValue(PdfUriProperty, value); }
        }

        private static void OnPdfUriChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as CustomViewer;
            var newUri = (Uri)e.NewValue;

            if (control != null && newUri != null)
            {
                control.pdfWebViewer.Navigate(newUri);
            }
        }

        public RelayCommand ReloadCommand
        {
            get { return new RelayCommand(_ => Reload()); }
        }

        private void Reload()
        {
            if (PdfUri != null)
            {
                pdfWebViewer.Navigate(PdfUri);
            }
        }
    }
}
