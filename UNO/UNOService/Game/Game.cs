﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace UNOService.Game
{
    [DataContract]
    public class Game
    {
        [DataMember]
        public int GameID { get; private set; }

        [DataMember]
        public List<Player> Players { get; set; }

        public List<Card> Deck { get; set; }
        public List<Card> PlayedCards { get; set; }
        public Direction Direction { get; set; }
        public bool UNOsaidAlready { get; set; }
        public Player TurnToPlay { get; set; }

        public Game(int gameID, List<Player> players)
        {
            this.GameID = gameID;
            this.Players = players;

            this.Deck = new List<Card>();
            this.PlayedCards = new List<Card>();

            this.Direction = Direction.clockwise;
            
            CreateDeck();
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
            Shuffle(Deck);
        }
    }
}
