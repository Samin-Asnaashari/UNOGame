using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Diagnostics;

namespace UNOService.Game
{
    public class Game
    {
        public int GameID { get; set; }
        public List<Player> Players { get; set; }
        public Stack<Card> Deck { get; set; }
        public Stack<Card> PlayedCards { get; set; }
        public Direction Direction { get; set; }
        Random rand;

        private int nextPlayerTurn;
        public Player CurrentPlayer { get; private set; }
        private Player PreviousPlayer;

        public int cardPickQueue;

        public Game(int gameID, List<Player> players)
        {
            rand = new Random(GameID);
            this.GameID = gameID;
            this.Players = players;
            this.Deck = new Stack<Card>();
            this.PlayedCards = new Stack<Card>();
            this.Direction = Direction.clockwise;
            CurrentPlayer = Players[0];

            createDeck();
            cardPickQueue = 0;
        }

        public void SendMessage(Player player, string message)
        {
            sendMessage(player.UserName, message);

            if (message.Length == 3 && message.ToLower().Contains("uno"))
            {
                Uno(player);
            }
        }

        private void sendMessage(string message)
        {
            foreach (Player player in Players)
            {
                player.IGameCallback.SendMessageGameCallback(message);
            }
        }

        private void sendMessage(string userNameSender, string message)
        {
            sendMessage($"{userNameSender}: {message}");
        }

        public void StartTurn()
        {
            // An action has happened, previous player is safe from UNO
            if (PreviousPlayer != null)
            {
                PreviousPlayer.UnoSaid = true;
            }
        }

        public void Uno(Player playerWhoCalledUno)
        {
            if (playerWhoCalledUno == PreviousPlayer && playerWhoCalledUno.Hand.Count == 1 && playerWhoCalledUno.UnoSaid == false) // Player called Uno on himself
            {
                playerWhoCalledUno.UnoSaid = true;
                sendMessage($"{playerWhoCalledUno.UserName} said Uno on themself");
            }
            else // Call Uno on other players
            {
                foreach (Player player in Players)
                {
                    if (player != playerWhoCalledUno)
                    {
                        if (player.Hand.Count == 1 && player.UnoSaid == false)
                        {
                            sendMessage($"{playerWhoCalledUno.UserName} said Uno on {player.UserName}");
                            giveCardsToPlayer(player, 2);
                            player.UnoSaid = true;
                        }
                    }
                }
            }
        }

        public void ChooseNotToPlayCard(Player player)
        {
            // Make sure this only happen if the player has picked a card
            if (player.AlreadyPickedCards)
            {
                EndTurn();
            }
            else
            {
                Debug.WriteLine($"{player.UserName} tried to ChooseNotToPlayCard() but has not picked a card");
            }
        }

        public bool IsValidCard(Player player, Card cardtoplay)
        {
            Card tableCard = PlayedCards.Peek();

            // Only current player may play a card
            if (CurrentPlayer != player)
            {
                Debug.WriteLine($"{player.UserName} tried to play a card, but it was not their turn");
                return false;
            }

            if (!player.HasCard(cardtoplay))
            {
                Debug.WriteLine($"{player.UserName} tried to play a card, but it was not in their hand");
                return false;
            }

            // If there is a card draw queue, only 'attack' cards may be played
            if (cardPickQueue != 0)
            {
                if (cardtoplay.Type == CardType.draw4Wild || cardtoplay.Type == CardType.draw2)
                {
                    return true;
                }
                else
                {
                    Debug.WriteLine($"{player.UserName} tried to play a card, but it was not an attack card. Picking queue: {cardPickQueue}");
                    return false;
                }
            }

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

            Debug.WriteLine($"{player.UserName} tried to play a card, but it was not a valid card for unknown reasons");
            return false;
        }

        public bool TryPlayCard(Player playerWhoPerformedAction, Card card)
        {
            if (IsValidCard(playerWhoPerformedAction, card))
            {
                StartTurn();

                cardAction(card);

                PlayedCards.Push(card);
                playerWhoPerformedAction.Remove(card);

                // Tell other users a card was played
                foreach (Player p in Players)
                {
                    if (p.UserName != playerWhoPerformedAction.UserName) //Prevent deadlock
                        p.IGameCallback.CardPlayed(card, playerWhoPerformedAction.UserName);
                }

                // Check end game condition
                if (playerWhoPerformedAction.Hand.Count() == 0)
                {
                    //game.End();
                    foreach (Player player in Players)
                    {
                        if (player != playerWhoPerformedAction) //Prevent deadlock
                            player.IGameCallback.EndOfTheGame(playerWhoPerformedAction.UserName);
                    }
                }

                EndTurn();

                return true;
            }

            return false;
        }

        private void cardAction(Card card)
        {
            switch (card.Type)
            {
                case CardType.draw2:
                    cardPickQueue += 2;
                    break;
                case CardType.draw4Wild:
                    cardPickQueue += 4;
                    break;
                case CardType.skip:
                    skip();
                    break;
                case CardType.reverse:
                    switchDirection();
                    break;
            }
        }

        public void GiveCardsToPlayer(Player player)
        {
            if (!player.AlreadyPickedCards)
            {
                StartTurn();

                if (cardPickQueue == 0)
                {
                    player.AlreadyPickedCards = true;
                    giveCardsToPlayer(player, 1);
                }
                else
                {
                    giveCardsToPlayer(player, cardPickQueue);
                    cardPickQueue = 0;
                    EndTurn();
                }
            }
            else
            {
                Debug.WriteLine($"{player.UserName} tried to pick a card but has already picked");
            }
        }

        private void giveCardsToPlayer(Player player, int amountOfCards)
        {
            List<Card> cards = getCardsFromDeck(amountOfCards);
            player.Hand.AddRange(cards);
            player.IGameCallback.AssignCards(cards);

            foreach (Player otherPlayer in Players)
            {
                if (otherPlayer != player)
                {
                    otherPlayer.IGameCallback.NotifyPlayersNumberOfCardsTaken(amountOfCards, player.UserName);
                }
            }

            // If player picks cards, uno is reset because they have more then one card.
            CurrentPlayer.UnoSaid = false;
        }

        private List<Card> getCardsFromDeck(int numberOfCardsToPick)
        {
            List<Card> pickedCards = new List<Card>();
            for (int i = 0; i < numberOfCardsToPick; i++)
            {
                if (Deck.Count == 0)
                {
                    refillDeck();
                }

                pickedCards.Add(Deck.Pop());
            }

            return pickedCards;
        }

        private Stack<Card> shuffle(Stack<Card> deckToShuffle)
        {
            List<Card> cards = deckToShuffle.ToList();

            for (int n = (deckToShuffle.Count - 1); n > 0; --n)
            {
                int k = rand.Next(n + 1);
                Card temp = cards[n];
                cards[n] = cards[k];
                cards[k] = temp;
            }

            return new Stack<Card>(cards);
        }

        private void createDeck()
        {
            List<CardColor> cardColors = new List<CardColor>();
            cardColors.Add(CardColor.Blue);
            cardColors.Add(CardColor.Green);
            cardColors.Add(CardColor.Red);
            cardColors.Add(CardColor.Yellow);

            List<CardType> cardTypes = new List<CardType>();
            cardTypes.Add(CardType.draw2);
            cardTypes.Add(CardType.reverse);
            cardTypes.Add(CardType.skip);

            for (int i = 0; i < 4; i++)//adding cards with only 4 ocurrences
            {
                Deck.Push(new Card(CardType.draw4Wild, CardColor.None, -1));
                Deck.Push(new Card(CardType.wild, CardColor.None, -1));
            }

            while (cardTypes.Count != 0)// adding the remaining 3 types
            {
                for (int i = 0; i < 4; i++)
                {
                    for (int i2 = 0; i2 < 2; i2++)
                    {
                        Deck.Push(new Card(cardTypes[cardTypes.Count - 1], cardColors[i], -1));
                    }
                }
                cardTypes.RemoveAt(cardTypes.Count - 1);
            }

            for (int i = 0; i < 4; i++)//adding normal cards 0
            {
                Deck.Push(new Card(CardType.normal, cardColors[i], 0));
            }

            while (cardColors.Count != 0)//adding normal cards 1 to 9
            {
                for (int i = 1; i < 10; i++)
                {
                    Deck.Push(new Card(CardType.normal, cardColors[cardColors.Count - 1], i));
                    Deck.Push(new Card(CardType.normal, cardColors[cardColors.Count - 1], i));
                }

                cardColors.RemoveAt(cardColors.Count - 1);
            }

            this.Deck = shuffle(this.Deck);
        }

        public void EndTurn()
        {
            PreviousPlayer = CurrentPlayer;

            if (Direction == Direction.clockwise)
            {
                nextPlayerTurn = (nextPlayerTurn + 1) % Players.Count();
            }
            else
            {
                nextPlayerTurn = (nextPlayerTurn - 1 + Players.Count) % Players.Count();
            }

            CurrentPlayer = Players[nextPlayerTurn];
            CurrentPlayer.AlreadyPickedCards = false;

            // If player has one card at beginning of turn, he is safe from Uno
            CurrentPlayer.UnoSaid = (CurrentPlayer.Hand.Count == 1);

            if (PreviousPlayer != CurrentPlayer) // Prevent deadlock when skipping turn with two players.
            {
                CurrentPlayer.IGameCallback.SetActivePlayer();
            }
        }

        private void switchDirection()
        {
            if (Direction == Direction.clockwise)
            {
                Direction = Direction.counterClockwise;
            }
            else
            {
                Direction = Direction.clockwise;
            }
        }

        private void refillDeck()
        {
            Card lastCard = PlayedCards.Pop();
            PlayedCards = shuffle(PlayedCards);
            this.Deck = this.PlayedCards;
            this.PlayedCards = new Stack<Card>();
            PlayedCards.Push(lastCard);
        }

        private void skip()
        {
            if (Direction == Direction.clockwise)
            {
                nextPlayerTurn = (nextPlayerTurn + 1) % Players.Count();
            }
            else
            {
                nextPlayerTurn = (nextPlayerTurn - 1 + Players.Count) % Players.Count();
            }
        }

        public void StartGame()
        {
            List<string> playersUserNames = Players.Select(x => x.UserName).ToList();

            foreach (Player player in Players)
            {
                // TODO Make sure this value is 7, I keep changing it to test UNO
                player.AddCard(getCardsFromDeck(7));
                player.IGameCallback.InitializeGame(player.Hand, playersUserNames);
            }

            do
            {
                PlayedCards.Push(Deck.Pop());
            }
            while (PlayedCards.Peek().Type != CardType.normal);


            foreach (Player player in Players)
            {
                player.IGameCallback.CardPlayed(PlayedCards.Peek(), "FirstAtStart");
            }

            CurrentPlayer.IGameCallback.SetActivePlayer();
        }
    }
}
