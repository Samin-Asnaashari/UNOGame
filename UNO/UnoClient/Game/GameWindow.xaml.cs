using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    [CallbackBehavior(ConcurrencyMode = ConcurrencyMode.Reentrant)] // Needed for choosing if player keeps or plays the card they pick
    public partial class GameWindow : proxy.IGameCallback
    {
        public GameClient GameProxy;

        private string username;

        private string password;
        List<CardHand> playerHands;


        // TODO Authenticate using password
        public GameWindow(string username, string password)
        {
            this.username = username;

            this.password = password;

            GameProxy = new GameClient(new InstanceContext(this));

            GameProxy.SubscribeToGameEvents(username);

            InitializeComponent();

            playerHands = new List<CardHand>() { player1Hand, player2Hand, player3Hand, player4Hand };

            this.Title = "Uno Game: " + username;
            player1Hand.Instantiate(username, true);
        }

        // Give client information about other players in the game
        public void InitializeGame(List<Card> cards, List<string> playersUserNames)
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

            for (int i = 1; i < playersUserNames.Count; i++) // Start at 1 because we are always at 0
            {
                playerHands[i].Instantiate(playersUserNames[i]);
            }

            List<CardHand> notActivePlayers = playerHands.Where(x => string.IsNullOrWhiteSpace(x.Username)).ToList();

            foreach (var control in notActivePlayers)
            {
                control.Visibility = Visibility.Hidden;
                playerHands.Remove(control);
            }

        }

        public void AssignCards(List<Card> cards)
        {
            SendMessageGameCallback($"{username} received {cards.Count} cards");

            if (cards.Count > 1)
            {
                foreach (Card c in cards)
                {
                    player1Hand.AddCard(new CardControl(c)); //Add cards to your own hand
                }
            }
            else
            {
                Card card = cards.First();
                var action = DrawCardChoiceWindow.UserChoice.Keep;

                // If picked card can be played, show the user a choice
                if (GameProxy.IsValidCard(card))
                {
                    DrawCardChoiceWindow drawChoiceWindow = new DrawCardChoiceWindow(card);
                    drawChoiceWindow.Owner = this;
                    drawChoiceWindow.ShowDialog();
                    action = drawChoiceWindow.Action;
                }

                switch (action)
                {
                    case DrawCardChoiceWindow.UserChoice.Keep:
                        {
                            player1Hand.AddCard(new CardControl(card));
                            GameProxy.ChooseNotToPlayCard();
                        }
                        break;
                    case DrawCardChoiceWindow.UserChoice.Play:
                        {
                            if (GameProxy.TryPlayCard(card))
                            {
                                CardPlayed(card, username);
                            }
                            else
                            {
                                // Should never happen
                                throw new Exception("Card could not be played");
                            }
                        }
                        break;
                    default:
                        // Should never happen
                        throw new NotImplementedException();
                }
            }

            endTurn();
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

        private void DeckOfCards_MouseDown(object sender, MouseButtonEventArgs e)
        {
            GameProxy.TakeCards();
        }

        //check if it right player who want is its turn 
        public void CardPlayed(Card c, string playerWhoPlayed)
        {
            if (player1Hand.Username == playerWhoPlayed)
                endTurn(); // We played a card so turn ends
            else if (player2Hand.Username == playerWhoPlayed)
                player2Hand.Hand.Children.RemoveAt(0);
            else if (player3Hand.Username == playerWhoPlayed)
                player3Hand.Hand.Children.RemoveAt(0);
            else if (player4Hand.Username == playerWhoPlayed)
                player4Hand.Hand.Children.RemoveAt(0);

            lastPlayedCard.Content = new CardControl(c);
        }

        private void buttonSendMessage_Click(object sender, RoutedEventArgs e)
        {
            GameProxy.SendMessageGame(chatMessage.Text);
        }

        public void NotifyPlayersNumberOfCardsTaken(int nrOfCardsTaken, string playerWhoTookCardsUserName)
        {
            CardHand cardHand = null;

            if (player2Hand.Username == playerWhoTookCardsUserName)
                cardHand = player2Hand;
            else if (player3Hand.Username == playerWhoTookCardsUserName)
                cardHand = player3Hand;
            else if (player4Hand.Username == playerWhoTookCardsUserName)
                cardHand = player4Hand;

            cardHand.AddFakeCards(nrOfCardsTaken);

            SendMessageGameCallback($"{playerWhoTookCardsUserName} recieved {nrOfCardsTaken} cards");
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

        public void SetActivePlayer()
        {
            //player1Hand.Hand.IsEnabled = true;
            //DeckOfCards.IsEnabled = true;
            player1Hand.sv.Background = Brushes.Yellow;
            Debug.WriteLine($"{username} started turn");

        }

        private void endTurn()
        {
            // If only two players and skipping turns, don't change active player
            if (playerHands.Count == 2)
            {
                // TODO This always returns null, so skip cards look incorrect clientside
                Card card = lastPlayedCard.GetCard();
                if (card != null)
                {
                    if (card.Type == CardType.skip)
                        return;
                }
            }

            player1Hand.sv.Background = Brushes.Brown;
            //player1Hand.Hand.IsEnabled = false;
            //DeckOfCards.IsEnabled = false;
            Debug.WriteLine($"{username} ended turn");
        }

    }
}
