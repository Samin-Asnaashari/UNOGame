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
using System.Windows.Navigation;
using System.Windows.Shapes;
using UnoClient.proxy;

namespace UnoClient
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginAndRegisterWindow : UserControl
    {
        public SwitchWindowHandler OnSwitchWindow;
        private LoginAndSignUpClient login;

        public LoginAndRegisterWindow(SwitchWindowHandler switchWindowCallback)
        {
            OnSwitchWindow += switchWindowCallback;
            InitializeComponent();
            login = new LoginAndSignUpClient();       
        }

        private void switchWindow(WindowType type, String username)
        {
            OnSwitchWindow(type, username);
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (login.Login(textBoxUsername.Text, textBoxPassword.Text))
            {
                string username = textBoxUsername.Text;
                {
                    switchWindow(WindowType.Lobby, username);
                    //go to lobby form//pass the username in the lobby form constructor
                }
            }
            else
                MessageBox.Show("Wrong combination of pass and username");
        }
    }
}
