using AVDSupervisorApp;
using AVDSupervisorApp.Services;
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

namespace AVDSupervisorApp.Views
{

    public partial class LoginWindow : Window
    {
        private readonly AzureAuthentication _azureAuthentication;

        public LoginWindow()
        {
            InitializeComponent();

            // Initialize AzureAuthentication with your Azure AD app settings
            _azureAuthentication = new AzureAuthentication();
        }

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {

            (bool isAuthenticated, string token) = await _azureAuthentication.AuthenticateAsync();

            if (isAuthenticated)
            {
                bool isAccess = await _azureAuthentication.UserHasAccess();
                if (!isAccess)
                {
                    ShowErrorMessage("You do not have permission to access this application.", "Access denied!");
                    _ = Task.Run(async () =>
                    {
                        await Task.Delay(3000);
                        ClearErrorMessage();
                    });
                    return;
                }
                // Authentication successful, navigate to main application window
                MainWindow mainWindow = new MainWindow(_azureAuthentication, token);
                mainWindow.Show();

                // Close the login window
                this.Close();
            }
            else
            {
                // Authentication failed, display error message
                ShowErrorMessage("Invalid Credentials. Please try again.", "Login failed!");

                //Disappears after 3 secs
                _ = Task.Run(async () =>
                {
                    await Task.Delay(3000);
                    ClearErrorMessage();
                });
            }
        }

        private void ShowErrorMessage(string message, string header)
        {
            ErrorLabelHeader.Text = header;
            ErrorLabel.Text = message;
            ErrorLabel.Visibility = Visibility.Visible;
        }

        private void ClearErrorMessage()
        {
            Dispatcher.Invoke(() =>
            {
                ErrorLabelHeader.Text = string.Empty;
                ErrorLabel.Text = string.Empty;
                ErrorLabel.Visibility = Visibility.Collapsed;
            });

        }
    }
}
