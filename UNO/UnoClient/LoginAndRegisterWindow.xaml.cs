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
            bool exist = client.CheckUserName(txtUsername.Text);
            if (exist)
            {
                Border myBorder1 = new Border();
                myBorder1.BorderBrush = Brushes.SlateBlue;
                myBorder1.BorderThickness = new Thickness(5, 10, 15, 20);
                myBorder1.Background = Brushes.AliceBlue;
                myBorder1.Padding = new Thickness(5);
                myBorder1.CornerRadius = new CornerRadius(15);
                txtUsername.BorderBrush = myBorder1.BorderBrush;
                txtUsername.BorderThickness = myBorder1.BorderThickness;
            }
            //else
            //    lblUsernameError.Visible = false;
        }
    }
}
