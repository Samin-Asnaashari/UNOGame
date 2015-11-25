using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace UNOService
{
    interface ILobbyCallback
    {
        /// <summary>
        /// Update the list of the online clients
        /// </summary>
        /// <param name="player"></param>
        [OperationContract(IsOneWay = true)]
        void PlayerConnected(Player player);

        /// <summary>
        /// Sending all other players that this player is removed from online list
        /// </summary>
        /// <param name="player"></param>
        [OperationContract(IsOneWay = true)]
        void PlayerDisConnected(Player player);

        /// <summary>
        /// Update party when answer is accept
        /// </summary>
        [OperationContract(IsOneWay = true)]
        void PlayerAddedToParty(string playerName);

        /// <summary>
        /// Lobby service sent invite to player
        /// </summary>
        /// <param name="hostName"></param>
        [OperationContract]
        void SentInvite(String hostName);
        //[OperationContract]
    }
}
