using Microsoft.Graph.Models;
using Microsoft.Identity.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
using System.Diagnostics;
using Microsoft.Graph.Models.CallRecords;
using AVDSupervisorApp.Views;
using AVDSupervisorApp.Services;
using AVDSupervisorApp.Datas;

namespace AVDSupervisorApp.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly AzureAuthentication _azureAuthentication;
        private const string BaseUrl = "https://management.azure.com";
        internal string token;
        internal string sessionID;
        internal string Servername;
        internal List<string> hostPoolNames = new List<string>();

        public MainWindow(AzureAuthentication azure, string _token)
        {
            InitializeComponent();
            this._azureAuthentication = azure;
            this.token = _token;
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            _azureAuthentication.SignOut();

            var login = new LoginWindow();
            login.Show();
            this.Close();
        }

        private async void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            ConnectButton.IsEnabled = false;
            HostPoolService hostPoolService = new HostPoolService();

            if (hostPoolNames == null || hostPoolNames.Count == 0)
                hostPoolNames = await hostPoolService.GetHostPoolNamesAsync(token);

            if (hostPoolNames != null)
            {
                string email = SearchTextBox.Text;
                if (email.EndsWith(Config.EmailDomain) == false)
                    email = email + Config.EmailDomain;

                foreach (string hostPoolName in hostPoolNames)
                {
                    if (Config.omittedHostPools.Contains(hostPoolName))
                        continue;

                    List<UserSession> userSessions = await hostPoolService.GetUserSessionsForHostPool(hostPoolName, token);

                    // Check if any user session matches the search text
                    foreach (UserSession session in userSessions)
                    {
                        if (session.Properties.UserPrincipalName.Equals(email, StringComparison.OrdinalIgnoreCase))
                        {
                            // Enable the "Start" button
                            ConnectButton.IsEnabled = true;
                            UserDataGrid.Visibility = Visibility.Visible;
                            UserNameRun.Text = session.Properties.ActiveDirectoryUserName;
                            StatusRun.Text = "Active";
                            StatusRun.Foreground = Brushes.Green;


                            string[] idParts = session.Id.Split('/');
                            Servername = idParts[idParts.Length - 3];
                            sessionID = idParts[idParts.Length - 1]; // Value before the last '/'

                            return; // No need to continue checking other host pools
                        }
                    }
                }

                // If no matching user session found, show a  indication thAT user is not active
                UserDataGrid.Visibility = Visibility.Visible;
                UserNameRun.Text = "";
                StatusRun.Text = "Not Active";
                StatusRun.Foreground = Brushes.Red;
            }
            else
            {
                MessageBox.Show("Invalid email address. Please check it once!");
            }
        }

        private void ConnectButton_Click(object sender, RoutedEventArgs e)
        {
            // Create a new Process object.
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            // Set the StartInfo.FileName property to the path of the CMD executable.
            process.StartInfo.FileName = "cmd.exe";
            // Set the StartInfo.Arguments property to the CMD command that you want to execute.
            if(ControlCheckBox.IsChecked == true)
                process.StartInfo.Arguments = $"Mstsc.exe /shadow:{sessionID} /v:{Servername} /noConsentPrompt /control";
            else
                process.StartInfo.Arguments = $"/c Mstsc.exe /shadow:{sessionID} /v:{Servername} /noConsentPrompt";
            // Start the process.
            process.Start();
            ConnectButton.IsEnabled = false;
            UserDataGrid.Visibility = Visibility.Collapsed;
        }
    }
}
