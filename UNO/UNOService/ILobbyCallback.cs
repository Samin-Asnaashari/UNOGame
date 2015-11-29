using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace UNOService
{
    public interface ILobbyCallback
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
        void PlayerDisconnected(Player player);

        /// <summary>
        /// Send to all party members that this user has left the party
        /// </summary>
        /// <param name="player"></param>
        [OperationContract(IsOneWay = true)]
        void PlayerLeftParty(Player player);

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

        /// <summary>
        /// Party is full so user receives notification
        /// </summary>
        /// <param name="players"></param>
        [OperationContract]
        void PartyIsFull();


        /// <summary>
        /// 
        /// </summary>
        [OperationContract]
        void NotifyGameStarted(List<Player> players);

        /// <summary>
        /// Message sended from service to all players in game
        /// </summary>
        [OperationContract]
        void SendChatMessageLobbyCallback(String message);
    }
}
