using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace UNOService.Game
{
    public class Game
    {
        public int GameID { get; set; }
        public List<Player> Players { get; set; }
        public List<Card> Deck { get; set; }
        public List<Card> PlayedCards { get; set; }
        public Direction Direction { get; set; }

        public bool UnoSaidAlready { get; set; }

        private int previousTurn { get; set; }
        private int currentTurn { get; set; }
        public Player CurrentPlayer { get { return Players[currentTurn]; } }
        public Player PreviousPlayer { get { if (previousTurn == -1) { return null; } else { return Players[previousTurn]; } } }

        public int draw2and4s;

        public Game(int gameID, List<Player> players)
        {
            this.GameID = gameID;
            this.Players = players;
            this.Deck = new List<Card>();
            this.PlayedCards = new List<Card>();
            this.Direction = Direction.clockwise;

            CreateDeck();

            draw2and4s = 0;
        }

        public List<Card> PickANumberOfCardsFromDeck(int numberOfCardsToPick)
        {
            List<Card> pickedCards = new List<Card>();
            for (int i = 0; i < numberOfCardsToPick; i++)
            {
                pickedCards.Add(Deck[0]);
                Deck.RemoveAt(0);
            }

            return pickedCards;
        }

        public void GiveEachPlayer7Cards()
        {
            List<Card> cards;
            int count;
            foreach (var item in Players)
            {
                count = 0;
                cards = new List<Card>();
                while (count != 7)
                {
                    cards.Add(Deck[0]);
                    Deck.RemoveAt(0);
                    count++;
                }
                item.AddCard(cards);
            }
        }

        public void Shuffle(List<Card> deckToShuffle)
        {
            Random r = new Random();

            for (int n = (deckToShuffle.Count - 1); n > 0; --n)
            {
                int k = r.Next(n + 1);
                Card temp = deckToShuffle[n];
                deckToShuffle[n] = deckToShuffle[k];
                deckToShuffle[k] = temp;
            }
        }

        public void CreateDeck()
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
                Deck.Add(new Card(CardType.draw4Wild, CardColor.none, -1));
                Deck.Add(new Card(CardType.wild, CardColor.none, -1));
            }

            while (cardTypes.Count != 0)// adding the remaining 3 types
            {
                for (int i = 0; i < 4; i++)
                {
                    for (int i2 = 0; i2 < 2; i2++)
                    {
                        Deck.Add(new Card(cardTypes[cardTypes.Count - 1], cardColors[i], -1));
                    }
                }
                cardTypes.RemoveAt(cardTypes.Count - 1);
            }

            for (int i = 0; i < 4; i++)//adding normal cards 0
            {
                Deck.Add(new Card(CardType.normal, cardColors[i], 0));
            }

            while (cardColors.Count != 0)//adding normal cards 1 to 9
            {
                for (int i = 1; i < 10; i++)
                {
                    Deck.Add(new Card(CardType.normal, cardColors[cardColors.Count - 1], i));
                    Deck.Add(new Card(CardType.normal, cardColors[cardColors.Count - 1], i));
                }

                cardColors.RemoveAt(cardColors.Count - 1);
            }

            Shuffle(this.Deck);
        }

        public void EndTurn()
        {
            //TODO This also needs to be implemented everywhere the player can perform a first action...
            //PreviousPlayer.UnoSaid = true; // Make player immune to uno.

            previousTurn = currentTurn;

            if (Direction == Direction.clockwise)
            {
                currentTurn = (currentTurn + 1) % Players.Count();
            }
            else
            {
                currentTurn--;

                if (currentTurn < 0)
                {
                    currentTurn += Players.Count();
                }
            }

            // This is now the next player
            //CurrentPlayer.UnoSaid = false;
        }

        public void SwitchDirection()
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

        public void ReFillDeck()
        {
            Card lastCard = PlayedCards.Last();
            PlayedCards.Remove(lastCard);
            this.Shuffle(PlayedCards);
            this.Deck = this.PlayedCards;
            this.PlayedCards = new List<Card>();
            PlayedCards.Add(lastCard);
        }
    }
}
