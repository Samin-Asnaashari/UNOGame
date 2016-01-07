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
        public delegate void LeavePartyHandler(string host);
        public LeavePartyHandler OnLeaveParty;

        public delegate void SendMessageHandler(string message);
        public SendMessageHandler OnSendMessage;

        public delegate void StartGameHandler();
        public StartGameHandler OnStartGame;

        public string Host
        {
            get; private set;
        }
        string player;
        public PartyControl(string player, string host, LeavePartyHandler leavePartyDelegate, SendMessageHandler sendMessageDelegate, StartGameHandler startGameDelegate)
        {
            OnLeaveParty = leavePartyDelegate;
            OnSendMessage = sendMessageDelegate;
            OnStartGame = startGameDelegate;

            this.Host = host;
            this.player = player;
            InitializeComponent();
            AddPlayer(host);

            // Only host can see the start game button
            if (player == host)
            {
                buttonStartGame.Visibility = Visibility.Visible;
            }
            else
            {
                AddPlayer(player);
            }
        }

        // Only host can use the start game button, when at least 2 people are in the lobby
        private void updateStartGameButton()
        {
            if (player == Host)
            {
                buttonStartGame.IsEnabled = (listBoxPlayersInParty.Items.Count > 1);
            }
        }

        // When a player joins the party, add their name
        public void AddPlayer(string player)
        {
            listBoxPlayersInParty.Items.Add(player);

            updateStartGameButton();
        }

        // When a player leaves the party, remove their name
        public void RemovePlayer(string player)
        {
            listBoxPlayersInParty.Items.Remove(player);
            updateStartGameButton();
        }

        private void buttonPartyClose_Click(object sender, RoutedEventArgs e)
        {
            Leave();
        }

        // Leave the party and notify the server
        public void Leave()
        {
            OnLeaveParty?.Invoke(Host);
        }

        // Send a message
        private void buttonSendPartyMessage_Click(object sender, RoutedEventArgs e)
        {
            listBoxPartyChat.Items.Add($"{player}: {textBoxPartyChat.Text}");
            OnSendMessage?.Invoke(textBoxPartyChat.Text);
        }

        // Show a recieved message
        public void DisplayMessage(string message)
        {
            listBoxPartyChat.Items.Add(message);
        }

        // Start game
        private void buttonStartGame_Click(object sender, RoutedEventArgs e)
        {
            OnStartGame?.Invoke();
        }
    }

}
