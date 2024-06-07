using Athena_Prison_PoC.ViewModel;
using Athena_Prison_PoC.Windows;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Principal;
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

namespace Athena_Prison_PoC
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
            //RunAsAdminButton_Click();
            if (!RunATBegin())
               Application.Current.Shutdown();

        }

        public bool RunATBegin()
        {
            SecureString securePassword = GetSecurePassword();

            if (securePassword != null)
            {
                // Use the entered credentials to run a process with elevated privileges
                return RunProcessWithCredentials(securePassword);
            }
            return false;
        }
        private SecureString GetSecurePassword()
        {
            // Display a dialog to get the user's credentials
            var dialog = new Login();
            bool? dialogResult = dialog.ShowDialog();
            SecureString securePassword = dialog.PasswordData;

            return securePassword;



        }

        private bool RunProcessWithCredentials(SecureString password)
        {
            try
            {
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = "", // Replace with your actual executable
                    UserName = Environment.UserName,            // Replace with the username entered by the user
                    Domain = ".",                    // Replace with the domain if applicable
                    UseShellExecute = false,
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true
                };

                // Set the password through the SecureStringToBSTR method
                IntPtr passwordPtr = Marshal.SecureStringToBSTR(password);
                try
                {
                    startInfo.Password = new System.Security.SecureString();
                    for (int i = 0; i < password.Length; i++)
                    {
                        char c = (char)Marshal.PtrToStructure(new IntPtr(passwordPtr.ToInt64() + i * 2), typeof(char));
                        startInfo.Password.AppendChar(c);
                    }
                }
                finally
                {
                    Marshal.ZeroFreeBSTR(passwordPtr);
                }

                using (Process process = new Process { StartInfo = startInfo })
                {
                    process.Start();
                    process.WaitForExit();
                }

                // Your application logic after running the process with elevated privileges
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }


        /*   private void RunAsAdminButton_Click()
           {
               // Check if the application is already running with administrative privileges
               if (!IsRunAsAdmin())
               {
                   // If not, restart the application with administrative privileges
                   RunAsAdmin();
               }
               else
               {
                   MessageBox.Show("The application is already running with administrative privileges.");
               }
           }

           private bool IsRunAsAdmin()
           {
               WindowsIdentity identity = WindowsIdentity.GetCurrent();
               WindowsPrincipal principal = new WindowsPrincipal(identity);

               return principal.IsInRole(WindowsBuiltInRole.Administrator);
           }

           private void RunAsAdmin()
           {
               try
               {
                   ProcessStartInfo startInfo = new ProcessStartInfo
                   {
                       FileName = Application.ResourceAssembly.Location,
                       UseShellExecute = true,
                       Verb = "runas" // This is the key to running as administrator
                   };

                   Process.Start(startInfo);

                   Application.Current.Shutdown();
               }
               catch (Exception ex)
               {
                   MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
               }
           }*/



    }


}
