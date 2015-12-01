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
        public Player Host { get { return players[0]; } }
        private List<Player> players;

        public Party(Player host)
        {
            players = new List<Player>();
            players.Add(host);
        }

        public void AddPlayer(Player player)
        {
            players.Add(player);
        }

        public void RemovePlayer(Player player)
        {
            players.Remove(player);
        }

        /// <summary>
        /// Return the list of players, but doesn't allow the list to be modified.
        /// </summary>
        /// <returns></returns>
        public IList<Player> GetPlayers()
        {
            return players.AsReadOnly();
        }
    }
}
