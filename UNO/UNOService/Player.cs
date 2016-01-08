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
        [DataMember]
        public List<Card> Hand { get; set; }
        [DataMember]
        public PlayerState State { get; set; }
        public ILobbyCallback ILobbyCallback { get; set; }
        public IGameCallback IGameCallback { get; set; }
        [DataMember]
        public Game.Game Game { get; set; }
        [DataMember]
        public Party Party { get; set; }
        [DataMember]
        public bool UnoSaid { get; set; }


        public Player(String username)
        {
            this.UserName = username;
            this.State = PlayerState.InLobby; //cause after login or sign up it always goes to lobby
            Hand = new List<Card>();
        }

        public void AddCard(List<Card> cards)
        {
            foreach (var item in cards)
            {
                this.Hand.Add(item);
            }
            
        }

        public override string ToString()
        {
            return this.UserName + "--- " + this.State;
        }

        public void Remove(Card card)
        {
            Hand.Remove(card);
        }
    }
}