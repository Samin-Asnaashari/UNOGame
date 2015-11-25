using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNOService.Game
{
    public class Game
    {
        public int GameID { get; set; }
        public List<Player> Players { get; set; }

        public Game(int gameID, List<Player> players)
        {

        }
    }
}
