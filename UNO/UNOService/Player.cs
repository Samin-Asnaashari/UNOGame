﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using UNOService.Game;
using System.ServiceModel;

namespace UNOService
{
    [DataContract]
    public class Player
    {
        [DataMember]
        public string UserName { get; private set; } //unique

        public List<Card> Hand { get; set; }

        //public List<Card> SaveHands { get; set; }

        [DataMember]
        public PlayerState State { get; set; }
        public ILobbyCallback ILobbyCallback { get; set; }
        public IGameCallback IGameCallback { get; set; }

        public Game.Game Game { get; set; }

        public Party Party { get; set; }

        public bool UnoSaid { get; set; }
        public bool AlreadyPickedCards; // To prevent played getting cards from the deck multiple times. Does not affect UNO or Attack cards.

        public Player(String username)
        {
            this.UserName = username;
            this.State = PlayerState.InLobby; //cause after login or sign up it always goes to lobby
            Hand = new List<Card>();

            //SaveHands = new List<Card>(); 
        }

        public void AddCard(List<Card> cards)
        {
            foreach (var item in cards)
            {
                this.Hand.Add(item);
            }

        }

        //public void AddCard(Card card)
        //{
        //    this.Hand.Add(card);
        //    SaveHands.Add(card);
        //}

        public override string ToString()
        {
            return this.UserName;
        }

        public bool HasCard(Card card)
        {
            foreach (var item in Hand)
            {
                if ((item.Color == card.Color && item.Number == card.Number && item.Type == card.Type) ||
                    (card.Type == item.Type && (card.Type == CardType.wild || card.Type == CardType.draw4Wild)))
                {
                    return true;
                }
            }

            return false;
        }

        public void Remove(Card card)
        {
            foreach (var item in Hand)
            {
                if (item.Color == card.Color && item.Number == card.Number && item.Type == card.Type)
                {
                    Hand.Remove(item);
                    break;
                }
                else if (card.Type == item.Type && (card.Type == CardType.wild || card.Type == CardType.draw4Wild))
                {
                    Hand.Remove(item);
                    break;
                }
            }
        }
    }
}