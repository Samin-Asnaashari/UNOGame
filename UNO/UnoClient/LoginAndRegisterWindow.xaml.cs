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

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsername.Text;

            if(client.Login(username, txtPassword.Password))
            {
                //Login succesful
                new LobbyWindow(username, txtPassword.Password).Show();
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
                new LobbyWindow(username, txtPassword.Password).Show();
                this.Close();
            }
            else
            {
                //Register unsuccesful
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
    }
}
