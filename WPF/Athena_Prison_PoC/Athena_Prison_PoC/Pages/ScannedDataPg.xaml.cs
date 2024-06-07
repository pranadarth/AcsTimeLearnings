using Athena_Prison_PoC.ViewModel;
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
using System.IO;
using System.Windows.Shapes;
using Path = System.IO.Path;

namespace Athena_Prison_PoC.Pages
{
    /// <summary>
    /// Interaction logic for ScannedDataPg.xaml
    /// </summary>
    public partial class ScannedDataPg : UserControl

    {
        public ScannedDataPg()
        {
            InitializeComponent();
           DataContext = new ViewModel.ScannedViewModel();
        }

    }
}
