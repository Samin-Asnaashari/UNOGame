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
        private LobbyClient LobbyProxy;
        PartyControl party;
        string username;

        public LobbyWindow(string username, string password)
        {
            this.username = username;
            InitializeComponent();
            LobbyProxy = new LobbyClient(new InstanceContext(this));
            LobbyProxy.SubscribeToLobbyEvents(username, password);

            labelUsername.Content = "Welcome " + username;

            // Get online players and show them in the list
            var onlinePlayers = LobbyProxy.GetOnlineList();
            if (onlinePlayers.Count() > 0)
            {
                inviteButton.IsEnabled = true;
            }

            foreach (var item in onlinePlayers)
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
            if (player.UserName == party.Host)
            {
                hidePartyWindow();
            }
            else
            {
                party.RemovePlayer(player.UserName);
            }
        }

        // Recieve a message to show in the party
        public void SendChatMessageLobbyCallback(string message)
        {
            party?.DisplayMessage(message);
        }

        public void ReceiveInvite(string hostName)
        {
            // Prevent multiple invites from the same person
            foreach (var inviteChild in listInvitations.Children)
            {
                var inviteControl = (InviteControl)inviteChild;
                if (inviteControl.InviteSenderName == hostName)
                    return;
            }

            listInvitations.Children.Add(new InviteControl(hostName, inviteResponse));
        }

        // Show a new party window, host name is used to enable/disable the invite button
        private void hidePartyWindow()
        {
            party = null;
            partyGrid.Children.Clear();
            // Enable the player to invite other players (would create a new party)
            inviteButton.IsEnabled = true;
        }

        // Show a new party window, host name is used to enable/disable the invite button
        private void showPartyWindow(string host)
        {
            if (host == username)
            {
                LobbyProxy.CreateParty();
            }
            //party?.Leave() // Maybe need to leave any existing party first, but it shouldn't be needed
            party = new PartyControl(username, host, leaveParty, LobbyProxy.SendMessageParty, startGame);
            foreach (var player in LobbyProxy.GetPartyMembers())
            {
                // Host and player are already in the lobby, due to being required in constructor
                if (player.UserName != username && player.UserName != host)
                {
                    party.AddPlayer(player.UserName);
                }
            }

            partyGrid.Children.Add(party);

            inviteButton.IsEnabled = (username == host);
        }

        private void inviteButton_Click(object sender, RoutedEventArgs e)
        {
            sendInvites();
        }

        // Invite all selected players in the list
        private void sendInvites()
        {
            List<string> playersToInvite = new List<string>();
            foreach (PlayerListElementControl playerControl in listOnlinePlayers.Children)
            {
                if (playerControl.IsChecked)
                {
                    playersToInvite.Add(playerControl.Player.UserName);
                }
            }

            if (playersToInvite.Count > 0)
            {
                if (party == null)
                {
                    showPartyWindow(username);
                }
                LobbyProxy.SendInvites(playersToInvite.ToArray());
            }
        }

        // Is called from within the PartyControl
        private void leaveParty(string host)
        {
            // Remove lobby window visually
            partyGrid.Children.Clear();
            // Tell server we left the party
            LobbyProxy.LeaveParty();
            party = null;
            // Enable the player to invite other players (would create a new party)
            inviteButton.IsEnabled = true;
        }

        // Accept or decline an invitation
        private void inviteResponse(bool accept, InviteControl sender)
        {
            listInvitations.Children.Remove(sender);

            if (accept)
            {
                if (LobbyProxy.AnswerInvite(sender.InviteSenderName))
                {
                    showPartyWindow(sender.InviteSenderName);
                }
                else
                {
                    //TODO Rework into the UI
                    MessageBox.Show("Could not join party");
                }
            }
        }

        private void startGame()
        {
            if (party.Host.Equals(username))
            {
                LobbyProxy.StartGame();
                
                NotifyGameStarted();
            }
        }

        public void NotifyGameStarted()
        {
            new Game.GameWindow(username, "password").Show();
            this.Hide();
        }
    }
}
