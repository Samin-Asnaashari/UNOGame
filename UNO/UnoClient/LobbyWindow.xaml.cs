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
using System.ServiceModel;

namespace UnoClient
{
    /// <summary>
    /// Interaction logic for LobbyControl.xaml
    /// </summary>
    public partial class LobbyWindow : ILobbyCallback
    {
        private LobbyClient Lobby;
        PartyControl party;
        string username;

        public LobbyWindow(string username)
        {
            this.username = username;
            InitializeComponent();

            Lobby = new LobbyClient(new InstanceContext(this));
            Lobby.SubScribeToLobbyEvents(username);

            // Get online players and show them in the list
            foreach (var item in Lobby.GetOnlineList())
            {
                listOnlinePlayers.Children.Add(new PlayerListElementControl(item));
            }
        }

        public void NotifyGameStarted(Player[] players)
        {
            throw new NotImplementedException();
        }

        // Occurs after a player accepts an invite to an already full party
        // May be reworked into the UI instead of a messagebox
        public void PartyIsFull()
        {
            MessageBox.Show("Party is full");
        }

        // Add a player to the party
        public void PlayerAddedToParty(string playerName)
        {
            party?.AddPlayer(playerName);
        }

        // Add a player to the online list
        public void PlayerConnected(Player player)
        {
            listOnlinePlayers.Children.Add(new PlayerListElementControl(player));
        }

        // Remove player from the online player list
        public void PlayerDisconnected(Player player)
        {
            foreach (UIElement playerControl in listOnlinePlayers.Children)
            {
                var selectedPlayer = ((PlayerListElementControl)playerControl).Player;

                if (selectedPlayer.UserName == player.UserName)
                {
                    listOnlinePlayers.Children.Remove(playerControl);
                }
            }

            // PlayerLeftParty(player); // Should this be done here, or does it also get called from the server when a player disconnects?
        }

        // Remove player from the party list
        public void PlayerLeftParty(Player player)
        {
            party?.RemovePlayer(player.UserName);
        }

        public void SendChatMessageLobbyCallback(string message)
        {
            party?.DisplayMessage(message);
        }

        // Actually means we are recieving an invite..
        // May be reworked into the UI instead of a messagebox
        public void SentInvite(string hostName)
        {
            if (MessageBox.Show($"{hostName} has invited you to a game", "Game Invite", MessageBoxButton.YesNoCancel) == MessageBoxResult.Yes)
            {
                Lobby.AnswerInvite(true, hostName);
                createParty(hostName);
            }
            else
            {
                Lobby.AnswerInvite(false, hostName);
            }
        }

        // Show a new party window
        private void createParty(string host)
        {
            //party?.Leave() // Maybe need to leave any existing party first, but it shouldn't be needed
            party = new PartyControl(username, host, leaveParty, sendPartyMessage);
            partyGrid.Children.Add(party);

            inviteButton.IsEnabled = (username == host);
        }

        // Invite all selected players in the list
        private void inviteButton_Click(object sender, RoutedEventArgs e)
        {
            List<Player> playersToInvite = new List<Player>();
            foreach (PlayerListElementControl playerControl in listOnlinePlayers.Children)
            {
                if (playerControl.IsChecked)
                {
                    playersToInvite.Add(playerControl.Player);
                }
            }

            Lobby.SendInvites(playersToInvite.ToArray());
        }

        // Is called from within the PartyControl
        private void leaveParty(string host)
        {
            // Remove lobby window visually
            partyGrid.Children.Clear();
            // Tell server we left the party
            Lobby.LeaveParty(host);
            party = null;
            // Enable the player to invite other players (would create a new party)
            inviteButton.IsEnabled = true;
        }

        // Is called from within the PartyControl
        private void sendPartyMessage(string message, string host)
        {
            Lobby.SendMessageParty(message, host);
        }
    }
}
