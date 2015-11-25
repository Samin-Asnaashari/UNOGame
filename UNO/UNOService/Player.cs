using System;
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
        public string UserName { get; private set; } //unique
        public List<Card> Hand { get; set; }
        public PlayerState State { get; set; }
        public ILobby ILobby { get; set; }
        public IGame IGame { get; set; }


        public Player()
        {
            Hand = new List<Card>();
        }

        public void AddCard(List<Card> cards)
        {

        }

        public void Remove(Card card)
        {

        }

        public void ChangeState(PlayerState state)
        {
            
        }
    }
}