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
        public Party(Player host)
        {
            Players = new List<Player>();
            Players.Add(host);
        }

        [DataMember]
        public int PartyID { get; private set; }

        [DataMember]
        public Player Host
        {
            get
            {
                if (Players.Count > 0)
                    return Players[0];
                else
                    return null;
            }
        }

        [DataMember]
        public List<Player> Players { get; private set; }
    }
}
