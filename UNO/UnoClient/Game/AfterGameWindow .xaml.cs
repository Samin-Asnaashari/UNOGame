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

namespace UnoClient.Game
{
    /// <summary>
    /// Interaction logic for AfterGameWindow.xaml
    /// </summary>
    public partial class AfterGameWindow : Window
    {
        public GameClient GameProxy;
        //public ReplayClient ReplayProxy;

        private string username;
        private string password;

        public AfterGameWindow(GameClient GameProxy, string username, string winner, string password)
        {
            this.GameProxy = GameProxy;
            InitializeComponent();

            //ReplayProxy = new ReplayClient();

            this.username = username;
            this.password = password;

            if (username == winner)
            {
                status.Text = "Congratulation You Won :) ";
            }
            else
            {
                //status.Foreground =  FFC51111;
                status.Text = "Sorry You Lost :( ";
            }

            Winner.Text = winner;
        }

        private void save_Click(object sender, RoutedEventArgs e)
        {
            //ReplayProxy.SaveReplay();
            GameProxy.SaveReplay();
            new LobbyWindow(username, password).Show();
            this.Close();

            //player state is in lobby
        }

        private void gotolobby_Click(object sender, RoutedEventArgs e)
        {
            new LobbyWindow(username, password).Show();
            this.Close();

            //player state is in lobby
        }
    }
}
