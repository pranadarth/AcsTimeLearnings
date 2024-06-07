using Athena_Prison_PoC.Commands;
using Athena_Prison_PoC.Pages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Athena_Prison_PoC.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        public ObservableCollection<string> Pages { get; } = new ObservableCollection<string>
        {
            "Home",
            "ScanReport"
        };

        private Dictionary<string, UserControl> _viewCache = new Dictionary<string, UserControl>();
        private UserControl _currentView;
        private string _currentPage;

        public UserControl CurrentView
        {
            get => _currentView;
            set
            {
                _currentView = value;
                OnPropertyChanged(nameof(CurrentView));
            }
        }

        public string CurrentPage
        {
            get => _currentPage;
            set
            {
                _currentPage = value;
                OnPropertyChanged(nameof(CurrentPage));
                Navigate(value);
            }
        }

        public RelayCommand NavigateCommand { get; }

        public MainViewModel()
        {
            NavigateCommand = new RelayCommand(param => Navigate(param.ToString()));
            // Set default view
            Navigate("Home");
        }

        private void Navigate(string view)
        {
            if (!_viewCache.TryGetValue(view, out UserControl cachedView))
            {
                cachedView = CreateViewInstance(view);
                _viewCache.Add(view, cachedView);
            }

            CurrentView = cachedView;
        }

        private UserControl CreateViewInstance(string view)
        {
            switch (view)
            {
                case "Home":
                    return new HomePg();
                    break;
                case "ScanReport":
                    return new ScannedDataPg();
                    break;
                default:
                    throw new ArgumentException("Invalid view");
            }
        }
    }
}
