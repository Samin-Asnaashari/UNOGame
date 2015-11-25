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

        [OperationContract(IsOneWay = true)]
        void PlayerDisConnected(Player player);

        /// <summary>
        /// Update party when answer is accept
        /// </summary>
        [OperationContract(IsOneWay = true)]
        void PlayerAddedToParty(string playerName);

        [OperationContract]
        void SentInvite(String hostName);
        //[OperationContract]
    }
}
