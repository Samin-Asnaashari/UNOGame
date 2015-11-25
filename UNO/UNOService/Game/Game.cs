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

        public Game(int gameID, List<Player> players)
        {

        }
    }
}
