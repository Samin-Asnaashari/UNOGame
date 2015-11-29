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

namespace UnoClient
{
    /// <summary>
    /// Interaction logic for PartyControl.xaml
    /// </summary>
    public partial class PartyControl : UserControl
    {
        string host;
        string player;
        proxy.LobbyClient lobby;
        public PartyControl(string player, string host, proxy.LobbyClient lobby)
        {
            this.lobby = lobby;
            this.host = host;
            this.player = player;
            InitializeComponent();
            AddPlayer(player);
            if (player == host)
            {
                buttonStartGame.Visibility = Visibility.Visible;
            }
        }

        private void updateStartGameButton()
        {
            if (player == host)
            {
                buttonStartGame.IsEnabled = (listBoxPlayersInParty.Items.Count > 1);
            }
        }

        public void AddPlayer(string player)
        {
            listBoxPlayersInParty.Items.Add(player);

            updateStartGameButton();
        }

        public void RemovePlayer(string player)
        {
            listBoxPlayersInParty.Items.Remove(player);
            updateStartGameButton();
        }

        private void buttonPartyClose_Click(object sender, RoutedEventArgs e)
        {
        }

        private void buttonSendPartyMessage_Click(object sender, RoutedEventArgs e)
        {
            string message = textBoxPartyChat.Text;
            //lobby.SendMessageLobby(message, host);
        }
    }

}
