using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace UNOService.Game
{
    [DataContract]
    public class Move
    {
        public enum Types { Play, Keep, Take, PunishedCard, Assigned }
        [DataMember]
        public string UserName { get; set; }
        //public DateTime Time { get; set; }
        public int GameID { get; set; }
        [DataMember]
        public Card card { get; set; }
        [DataMember]
        public Types Type { get; set; }

        public Move(string username, int gameID, Card card, Types type)
        {
            this.UserName = username;
            this.GameID = gameID;
            this.card = card;
            this.Type = type;
        }

        public Move(string username, int gameID, Types type)
        {
            this.UserName = username;
            this.GameID = gameID;
            this.Type = type;
        }

        public Move NextMove(int GameID, List<Move> moves)
        {
            Move m = moves[0];
            moves.Remove(m);
            return m;

        }
    }
}
