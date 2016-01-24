using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNOService.Game
{
    public class Move
    {
        public enum Types { Play,Keep,Take, PunishedCard,Assigned}
        public string UserName { get; set; }
        //public DateTime Time { get; set; }
        public int GameID { get; set; }
        public Card card { get; set; }
        public Types Type { get; set; }

        public Move(string username,int gameID,Card card,Types type)
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
    }
}
