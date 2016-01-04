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
    /// Interaction logic for PartyControl.xaml
    /// </summary>
    public partial class PartyControl : UserControl
    {
        private Player self;
        private Party party;

        private LobbyClient lobbyProxy;
        
        public PartyControl(Player player, Party party, ref LobbyClient lobbyProxy)
        {
            this.self = player;
            this.party = party;
            this.lobbyProxy = lobbyProxy;

            InitializeComponent();

            if (party.Host.UserName.Equals(player.UserName))
                buttonStartGame.Visibility = Visibility.Visible;

            AddPlayer(player.UserName);
        }

        // Only host can use the start game button, when at least 2 people are in the lobby
        private void updateStartGameButton()
        {
            if (party.Host.UserName.Equals(self.UserName))
            {
                buttonStartGame.IsEnabled = (listBoxPlayersInParty.Items.Count >= 2);
            }
        }

        // When a player joins the party, add their name
        public void AddPlayer(string name)
        {
            listBoxPlayersInParty.Items.Add(name);

            updateStartGameButton();
        }

        // When a player leaves the party, remove their name
        public void RemovePlayer(string name)
        {
            listBoxPlayersInParty.Items.Remove(name);
            updateStartGameButton();
        }

        private void buttonPartyClose_Click(object sender, RoutedEventArgs e)
        {
            Leave();
        }

        // Leave the party and notify the server
        public void Leave()
        {
            lobbyProxy.LeaveParty(party.Host.UserName);
            ((Panel)this.Parent).Children.Remove(this); //Remove the usercontrol from window
        }

        // Send a message
        private void buttonSendPartyMessage_Click(object sender, RoutedEventArgs e)
        {
            listBoxPartyChat.Items.Add($"{self.UserName}: {textBoxPartyChat.Text}");
            lobbyProxy.SendMessageParty(textBoxPartyChat.Text, party.Host.UserName);

            textBoxPartyChat.Text = "";
        }

        // Show a recieved message
        public void DisplayMessage(string message)
        {
            listBoxPartyChat.Items.Add(message);
        }

        public Party getParty()
        {
            return party;
        }

        public void setParty(Party p)
        {
            party = p;
        }

        private void buttonStartGame_Click(object sender, RoutedEventArgs e)
        {
            buttonStartGame.IsEnabled = false;
            if (party.Host.UserName.Equals(self.UserName))
            {
                int GameID = lobbyProxy.StartGame(self.UserName);
                new Game.GameWindow(self.UserName, GameID).Show();
            }
            else
            {
                buttonStartGame.Visibility = Visibility.Collapsed;
            }
        }
    }
}
