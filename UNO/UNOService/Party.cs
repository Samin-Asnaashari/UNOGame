using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace UNOService
{
    [DataContract]
    public class Party
    {
        public Player Host { get; set; }
        public List<Player> players { get; set; }

        public Party(Player host)
        {
        }

        public void AddPlayer(Player player)
        {

        }

        public void RemovePlayer(Player player)
        {

        }
    }
}
