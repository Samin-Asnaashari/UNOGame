﻿using System;
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
using System.Timers;
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
        private bool clockWiseGameDirection = true;
        bool playedCard; // Needed to prevent skipping a players turn if skip card was played, then we picked a card.

        bool closingConfirmed;

        List<CardHand> playerHands;

        List<Move> moves;

        //Timer timer;
        string Type;

        // TODO Authenticate using password
        public GameWindow(string username, string password, string Type)
        {
            this.username = username;
            this.password = password;

            GameProxy = new GameClient(new InstanceContext(this));

            if (Type == "Normal")
            {
                GameProxy.SubscribeToGameEvents(username);
            }
            else
            {
                GameProxy.SubscribeToReplayGameEvents(username);
            }

            InitializeComponent();

            this.Title = "Uno Game: " + username;
            player1Hand.Instantiate(username, playCard);

            setControlsEnabled(false);

            this.Type = Type;
            if (Type == "RePlay")
            {
                //disable everything
                System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
                dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
                dispatcherTimer.Interval = new TimeSpan(0, 5, 0);
                dispatcherTimer.Start();
                //timer = new Timer();

                moves = GameProxy.GetMoves();
            }
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            //for each time show one move
            //if {Play,Keep,Take, PunishedCard,Assigned}........do action untill is finished 
            if (moves.Count != 0)
            {
                if (moves[0].Type == Move.Types.Play)
                {

                }
                else if (moves[0].Type == Move.Types.Keep)
                {

                }
                else if (moves[0].Type == Move.Types.PunishedCard)
                {

                }
                else if (moves[0].Type == Move.Types.Assigned)
                {

                }
                else if (moves[0].Type == Move.Types.Take)
                {

                }
                moves.Remove(moves[0]);
            }
            else
            {
                new LobbyWindow(username, password).Show();
                this.Close();
            }
        }

        // Give client information about other players in the game
        public void InitializeGame(List<string> playersUserNames)
        {
            // If two players, face each other.
            if (playersUserNames.Count == 2)
            {
                playerHands = new List<CardHand>() { player1Hand, player3Hand, player2Hand, player4Hand };
            }
            else
            {
                // Else player order should be clockwise
                playerHands = new List<CardHand>() { player1Hand, player2Hand, player3Hand, player4Hand };
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
                // Choose to keep or play the card recieved

                Card card = cards.First();
                CardControl cardControl = new CardControl(card);
                var action = DrawCardChoiceWindow.UserChoice.Keep;
                player1Hand.AddCard(cardControl);

                DrawCardChoiceWindow drawChoiceWindow = new DrawCardChoiceWindow(card);
                drawChoiceWindow.Owner = this;
                drawChoiceWindow.Play.IsEnabled = isValidCard(card);
                drawChoiceWindow.ShowDialog();
                action = drawChoiceWindow.Action;

                switch (action)
                {
                    case DrawCardChoiceWindow.UserChoice.Keep:
                        GameProxy.ChooseNotToPlayCard();
                        break;
                    case DrawCardChoiceWindow.UserChoice.Play:
                        if (!playCard(cardControl)) // Just in case
                            GameProxy.ChooseNotToPlayCard();
                        break;
                    default:
                        // Should never happen
                        throw new NotImplementedException();
                }
            }

            endTurn();
        }

        private bool playCard(CardControl cardControl)
        {
            Card cardToBePlayed = cardControl.GetCard();

            if (cardToBePlayed.Type == CardType.draw4Wild || cardToBePlayed.Type == CardType.wild)
            {
                ColorPickerWindow colorPicker = new ColorPickerWindow();
                colorPicker.Owner = this;
                colorPicker.ShowDialog();

                Brush selectedColor = colorPicker.selectedColor;

                if (selectedColor.Equals(Brushes.Red))
                    cardToBePlayed.Color = CardColor.Red;
                else if (selectedColor.Equals(Brushes.Green))
                    cardToBePlayed.Color = CardColor.Green;
                else if (selectedColor.Equals(Brushes.Blue))
                    cardToBePlayed.Color = CardColor.Blue;
                else if (selectedColor.Equals(Brushes.Yellow))
                    cardToBePlayed.Color = CardColor.Yellow;
            }

            bool playSuccess = GameProxy.TryPlayCard(cardToBePlayed);//if special card, color chosen is saved in color attribute to be handled in the server

            if (playSuccess)
            {
                playedCard = true;
                CardPlayed(cardToBePlayed, username);
                player1Hand.RemoveCard(cardControl);

                if (player1Hand.getNrOfCards() == 0) //End game, we won!
                    EndOfTheGame(username);
            }
            else
            {
                MessageBox.Show("Invalid move");
            }

            return playSuccess;
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
            // Every card played is added the the last played card 'pile'
            lastPlayedCard.Content = new CardControl(c);

            if (c.Type == CardType.reverse)
            {
                clockWiseGameDirection = !clockWiseGameDirection;
            }

            if (player1Hand.Username == playerWhoPlayed)
                endTurn(); // We played a card so turn ends
            else if (player2Hand.Username == playerWhoPlayed)
                player2Hand.Hand.Children.RemoveAt(0);
            else if (player3Hand.Username == playerWhoPlayed)
                player3Hand.Hand.Children.RemoveAt(0);
            else if (player4Hand.Username == playerWhoPlayed)
                player4Hand.Hand.Children.RemoveAt(0);
        }

        private bool isValidCard(Card cardtoplay)
        {
            CardControl cardControl = lastPlayedCard.Content as CardControl;
            Card tableCard = cardControl.GetCard();

            // Wild cards can be played on any color
            if (cardtoplay.Type == CardType.draw4Wild || cardtoplay.Type == CardType.wild)
            {
                return true;
            }
            // Any matching color can be played, or matching numbers for normal cards
            else if (cardtoplay.Color == tableCard.Color || (cardtoplay.Type == CardType.normal && cardtoplay.Number == tableCard.Number))
            {
                return true;
            }
            // Any matching types can also be played
            else if (cardtoplay.Type == tableCard.Type && cardtoplay.Type != CardType.normal)
            {
                return true;
            }

            return false;
        }

        private void buttonSendMessage_Click(object sender, RoutedEventArgs e)
        {
            GameProxy.SendMessageGame(chatMessage.Text);
            chatMessage.Text = "";
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

            cardHand.AddPlaceHolderCards(nrOfCardsTaken);

            SendMessageGameCallback($"{playerWhoTookCardsUserName} recieved {nrOfCardsTaken} cards");
        }

        public void EndOfTheGame(string winner)
        {
            AfterGameWindow end = new AfterGameWindow(GameProxy,username, winner, password);
            end.Show();

            closingConfirmed = true;
            this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (closingConfirmed) //If we are manually forcing this window to close
                return;

            GameProxy.EndGame();
        }

        public void TurnChanged(string activePlayer)
        {
            setControlsEnabled(false);

            if (activePlayer.Equals(this.username)) // Its our turn
            {
                setControlsEnabled(true);
                this.Activate(); // Bring Window to foreground
            }

            foreach (CardHand hand in playerHands)
            {
                if (hand.Username.Equals(activePlayer))
                    hand.IsTurn = true;
                else
                    hand.IsTurn = false;
            }
        }

        // Call this locally when turn is finished to prevent deadlock
        private void endTurn()
        {
            int nextPlayerTurn = 0;

            if (playedCard)
            {
                CardControl cardControl = lastPlayedCard.Content as CardControl;
                if (cardControl != null)
                {
                    var card = cardControl.GetCard();

                    if (card.Type == CardType.skip) // If we played a skip card, skip the next player
                    {
                        if (clockWiseGameDirection)
                        {
                            nextPlayerTurn = (nextPlayerTurn + 1) % playerHands.Count();
                        }
                        else
                        {
                            nextPlayerTurn = (nextPlayerTurn - 1 + playerHands.Count) % playerHands.Count();
                        }
                    }
                }
            }

            if (clockWiseGameDirection)
            {
                nextPlayerTurn = (nextPlayerTurn + 1) % playerHands.Count();
            }
            else
            {
                nextPlayerTurn = (nextPlayerTurn - 1 + playerHands.Count) % playerHands.Count();
            }

            TurnChanged(playerHands[nextPlayerTurn].Username);
        }

        private void setControlsEnabled(bool enabled)
        {
            if (enabled)
            {
                playedCard = false;
                Debug.WriteLine($"{username} started turn clientside");
            }
            else
            {
                Debug.WriteLine($"{username} ended turn clientside");
            }
            player1Hand.Hand.IsEnabled = enabled;
            DeckOfCards.IsEnabled = enabled;
        }

        private void chatMessage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) //We want to send the message
                buttonSendMessage_Click(sender, null);
        }
    }
}
