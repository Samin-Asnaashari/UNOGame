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
using System.Windows.Navigation;
using System.Windows.Shapes;
using UnoClient.proxy;

namespace UnoClient.Game
{
    /// <summary>
    /// Interaction logic for GameWindow.xaml
    /// </summary>
    public partial class GameWindow : proxy.IGameCallback
    {
        public GameClient GameProxy;

        private string username;

        private string password;

        // TODO Authenticate using password
        public GameWindow(string username, string password)
        {
            this.username = username;

            this.password = password;

            GameProxy = new GameClient(new InstanceContext(this));

            GameProxy.SubscribeToGameEvents(username);

            InitializeComponent();
            this.Title = "Uno Game: " + username;
            player1Hand.Instantiate(username, true);
        }

        public void CardsAssigned(List<Card> cards, List<string> playersUserNames)
        {
            foreach (Card c in cards)
            {
                player1Hand.AddCard(new CardControl(c)); //Add cards to your own hand
            }

            // Shift list until player is first
            while (playersUserNames.First() != username)
            {
                playersUserNames.Add(playersUserNames.First());
                playersUserNames.Remove(playersUserNames.First());
            }

            List<CardHand> playerHands = new List<CardHand>() { player1Hand, player2Hand, player3Hand, player4Hand };

            for (int i = 1; i < playersUserNames.Count; i++) // Start at 1 because we are always at 0
            {
                playerHands[i].Instantiate(playersUserNames[i]);
            }
        }

        public void NotifyPlayerLeft(string userName)
        {
            throw new NotImplementedException();
        }

        public void SendMessageGameCallback(string message)
        {
            chat.Items.Add(message);
            chat.ScrollIntoView(chat.Items[chat.Items.Count - 1]); //Scroll to bottom
        }

        public void TurnChanged(Player player)
        {

        }

        public void NotifyOpponentsOfPlayerPunished(string userName)
        {
            throw new NotImplementedException();
        }

        private async void DeckOfCards_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (GameProxy.ValidPlayerTurn(username))
            {
                Card takenCard = await GameProxy.takeCardAsync();
                player1Hand.AddCard(new CardControl(takenCard));
            }
            else
            {
                MessageBox.Show("Is Not Your Turn!");
            }

        }

        //check if it right player who want is its turn 
        public void CardPlayed(Card c, string playerWhoPlayed)
        {
            if (player2Hand.Username == playerWhoPlayed)
                player2Hand.Hand.Children.RemoveAt(0);
            else if (player3Hand.Username == playerWhoPlayed)
                player3Hand.Hand.Children.RemoveAt(0);
            else if (player4Hand.Username == playerWhoPlayed)
                player4Hand.Hand.Children.RemoveAt(0);

            lastPlayedCard.Content = new CardControl(c);
        }

        private void buttonSendMessage_Click(object sender, RoutedEventArgs e)
        {
            //chat.Items.Add($"{username}: {chatMessage.Text}");
            GameProxy.SendMessageGame(chatMessage.Text);

            chatMessage.Text = "";
        }

        public void NotifyPlayersNumberOfCardsTaken(int nrOfCardsTaken, string playerWhoTookCardsUserName)
        {
            if (player2Hand.Username == playerWhoTookCardsUserName)
                for (int i = 0; i < nrOfCardsTaken; i++)
                {
                    player2Hand.AddCard(new CardControl());
                }
            else if (player3Hand.Username == playerWhoTookCardsUserName)
                for (int i = 0; i < nrOfCardsTaken; i++)
                {
                    player3Hand.AddCard(new CardControl());
                }
            else if (player4Hand.Username == playerWhoTookCardsUserName)
                for (int i = 0; i < nrOfCardsTaken; i++)
                {
                    player4Hand.AddCard(new CardControl());
                }
        }

        public void EndOfTheGame(string winner)
        {
            AfterGameWindow end = new AfterGameWindow(username, winner, password);
            end.Show();
            this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            GameProxy.EndGame();
        }
    }
}
