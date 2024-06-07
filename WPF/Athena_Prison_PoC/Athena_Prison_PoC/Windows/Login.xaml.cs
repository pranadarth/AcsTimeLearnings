using CredentialManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
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

namespace Athena_Prison_PoC.Windows
{
    
    public partial class Login : Window
    {
        public string UsernameData { get; private set; }
        public SecureString PasswordData { get; private set; }

        public Login()
        {
            InitializeComponent();
            Username.Text = Environment.UserName;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            // Retrieve the entered credentials from the dialog
            
            PasswordData = GetSecurePassword(Password.SecurePassword);

            DialogResult = true;
            Close();
        }

        private SecureString GetSecurePassword(SecureString secureString)
        {
            IntPtr unmanagedString = IntPtr.Zero;

            try
            {
                unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(secureString);
                char[] charArray = new char[secureString.Length];
                Marshal.Copy(unmanagedString, charArray, 0, secureString.Length);

                // Create a new SecureString and append the characters
                SecureString securePassword = new SecureString();
                foreach (char c in charArray)
                {
                    securePassword.AppendChar(c);
                }

                return securePassword;
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
            }
        }
    }
}
