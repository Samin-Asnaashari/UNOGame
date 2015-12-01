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

        public LobbyWindow(string username, string password)
        {
            this.username = username;
            InitializeComponent();

            Lobby = new LobbyClient(new InstanceContext(this));
            Lobby.SubscribeToLobbyEvents(username, password);

            // Get online players and show them in the list
            foreach (var item in Lobby.GetOnlineList())
            {
                listOnlinePlayers.Children.Add(new PlayerListElementControl(item));
            }
        }

        public void ChangePlayerState(Player player)
        {
            foreach (UIElement playerControl in listOnlinePlayers.Children)
            {
                var selectedPlayer = ((PlayerListElementControl)playerControl).Player;
                if (player.UserName == selectedPlayer.UserName)
                {
                    selectedPlayer.State = player.State;
                }
            }
        }

        // Occurs after a player accepts an invite to an already full party
        //TODO Rework into the UI instead of a messagebox
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
            // If player is returning to lobby from a game, he will already be in the list, and just needs a status change
            // May not be needed
            foreach (UIElement playerControl in listOnlinePlayers.Children)
            {
                var selectedPlayer = ((PlayerListElementControl)playerControl).Player;
                if (player.UserName == selectedPlayer.UserName)
                {
                    ChangePlayerState(player);
                    return;
                }
            }

            listOnlinePlayers.Children.Add(new PlayerListElementControl(player));

            inviteButton.IsEnabled = true;
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

            //TODO PlayerLeftParty(player); Should this be done here, or does it also get called from the server when a player disconnects?

            if (listOnlinePlayers.Children.Count == 0)
            {
                inviteButton.IsEnabled = false;
            }
        }

        // Remove player from the party list
        public void PlayerLeftParty(Player player)
        {
            party?.RemovePlayer(player.UserName);
        }

        // Recieve a message to show in the party
        public void SendChatMessageLobbyCallback(string message)
        {
            party?.DisplayMessage(message);
        }

        public void ReceiveInvite(string hostName)
        {
            listInvitations.Children.Add(new InviteControl(hostName, inviteResponse));
        }

        // Show a new party window
        private void createParty(string host)
        {
            if (host == username)
            {
                Lobby.CreateParty(host);
            }
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

            if (playersToInvite.Count > 0)
            {
                Lobby.SendInvites(playersToInvite.ToArray());
            }
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

        // Accept or decline an invitation
        private void inviteResponse(bool accept, InviteControl sender)
        {
            listInvitations.Children.Remove(sender);

            if (accept)
            {
                Lobby.AnswerInvite(true, sender.InviteSenderName);
                createParty(sender.InviteSenderName);
            }
            else
            {
                Lobby.AnswerInvite(false, sender.InviteSenderName);
            }
        }
    }
}
