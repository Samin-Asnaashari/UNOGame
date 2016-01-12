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
            //this.GameID = 1;

            this.password = password;

            GameProxy = new GameClient(new InstanceContext(this));

            GameProxy.SubscribeToGameEvents(username);

            InitializeComponent();
            this.Title = "Uno Game: " + username;
            player1Hand.Username = username;

            //TODO: position the players
        }

        public void CardsAssigned(List<Card> cards, List<string> playersUserNames)
        {
            foreach(Card c in cards)
            {
                player1Hand.addCard(new CardControl(c)); //Add cards to your own hand
            }

            if (playersUserNames != null)
            {
                List<CardHand> playerHands = new List<CardHand>();
                playerHands.Add(player2Hand);
                playerHands.Add(player3Hand);
                playerHands.Add(player4Hand);

                for (int i = 0; i < playersUserNames.Count ; i++)
                {
                    if (username != playersUserNames[i])
                        playerHands[i].Username = playersUserNames[i];
                    else
                    {
                        playersUserNames.RemoveAt(i);
                        i--;
                    }
                }


                if(player2Hand.Username != null)
                    for (int i = 0; i < 7; i++)
                        player2Hand.addCard(new CardControl());

                if (player3Hand.Username != null)
                    for (int i = 0; i < 7; i++)
                        player3Hand.addCard(new CardControl());

                if (player4Hand.Username != null)
                    for (int i = 0; i < 7; i++)
                        player4Hand.addCard(new CardControl());
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
                player1Hand.addCard(new CardControl(takenCard));
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
                    player2Hand.addCard(new CardControl());
                }
            else if (player3Hand.Username == playerWhoTookCardsUserName)
                for (int i = 0; i < nrOfCardsTaken; i++)
                {
                    player3Hand.addCard(new CardControl());
                }
            else if (player4Hand.Username == playerWhoTookCardsUserName)
                for (int i = 0; i < nrOfCardsTaken; i++)
                {
                    player4Hand.addCard(new CardControl());
                }
        }

        public void EndOfTheGame(string winner)
        {
            AfterGameWindow end = new AfterGameWindow(username,winner,password);
            end.Show();
            this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            GameProxy.EndGame();
        }
    }
}
