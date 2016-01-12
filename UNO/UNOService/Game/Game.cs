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

        public void Uno(Player playerWhoCalledUno)
        {
            if (playerWhoCalledUno == PreviousPlayer && playerWhoCalledUno.Hand.Count == 1 && playerWhoCalledUno.UnoSaid == false) // Player called Uno on himself
            {
                Debug.WriteLine($"{playerWhoCalledUno.UserName} said Uno on themself");
                playerWhoCalledUno.UnoSaid = true;
            }
            else // Call Uno on other players
            {
                foreach (Player player in Players)
                {
                    if (player != playerWhoCalledUno)
                    {
                        if (player.Hand.Count == 1 && player.UnoSaid == false)
                        {
                            Debug.WriteLine($"{playerWhoCalledUno.UserName} said Uno on {player.UserName}");
                            GiveCardsToPlayer(player, 2);
                            player.UnoSaid = true;
                        }
                    }
                }
            }
        }

        private bool isValidMove(Player player, Card cardtoplay)
        {
            Card tableCard = PlayedCards.Peek();

            // Only current player may play a card
            if (CurrentPlayer != player)
            {
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

            return false;
        }

        public bool TryPlayCard(Player playerWhoPerformedAction, Card card)
        {
            if (isValidMove(playerWhoPerformedAction, card))
            {
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

                // An action has happened, previous player is safe from UNO
                if (PreviousPlayer != null)
                {
                    PreviousPlayer.UnoSaid = true;
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
            if (cardPickQueue == 0)
            {
                GiveCardsToPlayer(player, 1);
            }
            else
            {
                GiveCardsToPlayer(player, cardPickQueue);
            }

        }

        public void GiveCardsToPlayer(Player player, int amountOfCards)
        {
            if (player.AlreadyPickedCards)
            {
                // Player already picked a card, but chose not to play it. Clicking the deck ends the turn.
                EndTurn();
            }
            else
            {
                // Reset UNO Status
                CurrentPlayer.UnoSaid = false;
                player.AlreadyPickedCards = true;
                List<Card> cards = pickANumberOfCardsFromDeck(amountOfCards);
                player.Hand.AddRange(cards);
                player.IGameCallback.AssignCards(cards);

                foreach (Player otherPlayer in Players)
                {
                    if (otherPlayer != player)
                    {
                        otherPlayer.IGameCallback.NotifyPlayersNumberOfCardsTaken(amountOfCards, player.UserName);
                    }
                }

                cardPickQueue = 0;

                if (amountOfCards > 1)
                    EndTurn();
            }
        }

        private List<Card> pickANumberOfCardsFromDeck(int numberOfCardsToPick)
        {
            // An action has happened, previous player is safe from UNO
            if (PreviousPlayer != null)
            {
                PreviousPlayer.UnoSaid = true;
            }

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

        public void Start()
        {

            List<string> playersUserNames = Players.Select(x => x.UserName).ToList();

            foreach (Player player in Players)
            {
                // TODO Make sure this value is 7, I keep changing it to test UNO
                player.AddCard(pickANumberOfCardsFromDeck(7));
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
