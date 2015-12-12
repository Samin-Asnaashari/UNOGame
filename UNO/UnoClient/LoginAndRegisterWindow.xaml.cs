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
    }
}
