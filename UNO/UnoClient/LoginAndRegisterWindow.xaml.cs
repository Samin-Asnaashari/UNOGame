using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
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

using UnoClient.proxy;

namespace UnoClient
{
    /// <summary>
    /// Interaction logic for LoginAndRegisterWindow.xaml
    /// </summary>
    public partial class LoginAndRegisterWindow : Window
    {
        private LoginAndSignUpClient client; //the proxy 

        public LoginAndRegisterWindow()
        {
            client = new LoginAndSignUpClient();
            InitializeComponent();
        }
        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            btnLogin.IsEnabled = false;

            string username = txtUsername.Text;

            StatusCode sc = client.Login(username, txtPassword.Password);

            if (sc.Code > 0)
            {
                //Login succesful
                new LobbyWindow(username, txtPassword.Password).Show();
                this.Close();
            }
            else
            {
                MessageBox.Show(sc.Status, "Error");
            }

            btnLogin.IsEnabled = true;
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsername.Text;

            StatusCode sc = client.SignUp(username, txtPassword.Password);
            if (sc.Code > 0)
            {
                //Register succesful
                new LobbyWindow(username, txtPassword.Password).Show();
                this.Close();
            }
            else
            {
                MessageBox.Show(sc.Status, "Error");
            }
        }

        private async void txtUsername_KeyUp(object sender, KeyEventArgs e)
        {
            if (wpRegisterControls.Height > 0) //Register WrapPanel is visible
            {
                bool exist = await client.CheckUserNameAsync(txtUsername.Text); //Async such that the user interface won't freeze
                if (exist)
                {
                    txtUsername.Background = new SolidColorBrush(Color.FromRgb(255, 85, 85));
                }
                else
                {
                    txtUsername.Background = Brushes.White;
                }
            }
        }

        private void VerifyPassWord_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtPassword.Password.Length == txtVerifyPassword.Password.Length)
            {
                if (txtPassword.Password != txtVerifyPassword.Password)
                {
                    txtVerifyPassword.Background = new SolidColorBrush(Color.FromRgb(255, 85, 85));
                }
                else
                {
                    txtVerifyPassword.Background = Brushes.White;
                }
            }
        }

        private void txtPassword_LostFocus(object sender, RoutedEventArgs e)
        {
            if (wpRegisterControls.Height > 0) //Register WrapPanel is visible
            {
                if (txtPassword.Password.Length > 0 && txtPassword.Password.Length < 6)
                    txtPassword.Background = new SolidColorBrush(Color.FromRgb(255, 85, 85));
                else
                    txtPassword.Background = Brushes.White;
            }
            else
                txtPassword.Background = Brushes.White;
        }

        private void txtPassword_KeyUp(object sender, KeyEventArgs e)
        {
            if (wpRegisterControls.Height > 0) //Register WrapPanel is visible
            {
                if (((SolidColorBrush)txtPassword.Background).Color.Equals(Color.FromRgb(255, 85, 85)))
                {
                    if (txtPassword.Password.Length >= 6)
                        txtPassword.Background = Brushes.White;
                }
            }
        }

        private void lblNoAccount_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            btnLogin.IsDefault = false;
            btnRegister.IsDefault = true;
            btnLogin.IsTabStop = false;
        }

        private void TextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            btnRegister.IsDefault = false;
            btnLogin.IsDefault = true;
            btnLogin.IsTabStop = true;
        }
    }
}
