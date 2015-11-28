using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace UNOService.Game
{
    [DataContract]
    public class Game
    {
        [DataMember]
        public int GameID { get; set; }
        public List<Player> Players { get; set; }
        public List<Card> Deck { get; set; }
        public List<Card> PlayedCards { get; set; }
        public Direction Direction { get; set; }
        public bool UNOsaidAlready { get; set; }
        public Player TurnToPlay { get; set; }

        public Game(int gameID, List<Player> players)
        {
            this.GameID = gameID;
            this.Direction = Direction.clockwise;
            this.Players = players;
        }

        public void CreateDeck()
        {

        }
    }
}
