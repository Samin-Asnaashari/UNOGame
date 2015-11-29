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

using UnoClient.proxy;

namespace UnoClient
{
	/// <summary>
	/// Interaction logic for LoginAndRegisterWindow.xaml
	/// </summary>
	public partial class LoginAndRegisterWindow : Window
	{
        private LoginAndSignUpClient client;

		public LoginAndRegisterWindow()
		{
            client = new LoginAndSignUpClient();
			InitializeComponent();
		}

        private void lblNoAccount_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            //wpLoginControls.Visibility = Visibility.Collapsed;
            //wpRegisterControls.Visibility = Visibility.Visible;
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsername.Text;

            if(client.Login(username, txtPassword.Password))
            {
                //Login succesful
                new LobbyWindow(username).Show();
                this.Close();
            }
            else
            {
                //Login unsuccesful
                MessageBox.Show("Username or password incorrect.");
            }
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsername.Text;

            if (client.SignUp(username, txtPassword.Password))
            {
                //Register succesful
                new LobbyWindow(username).Show();
                this.Close();
            }
            else
            {
                //Register unsuccesful
            }
        }

        private void txtUsername_KeyUp(object sender, KeyEventArgs e)
        {
            bool exist = client.CheckUserName(txtUsername.Text);//only done if user is in register window wpf configuration.
            if (exist)
            {
                //Border myBorder1 = new Border();
                //myBorder1.BorderThickness = new Thickness(2.01);
                //myBorder1.BorderBrush = Brushes.Red;

                txtUsername.BorderThickness = new Thickness(2);
                txtUsername.BorderBrush = Brushes.Red;
            }
            else
            {
                txtUsername.BorderThickness = new Thickness(1);
                txtUsername.BorderBrush = Brushes.Red;
            }
        }
    }
}
