using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace UNOService
{
    [ServiceContract(CallbackContract = typeof(ILobbyCallback))]
    public interface ILobby
    {
        /// <summary>
        /// Player get the current online list when entering lobby
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<Player> GetOnlineList();

        /// <summary>
        /// Get the player from the lobby context. Safe to assume this is never null.
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        Player getPlayerFromLobbyContext();

        /// <summary>
        /// Party signal sent to be created
        /// </summary>
        /// <returns>Returns the created party</returns>
        [OperationContract]
        Party CreateParty();

        /// <summary>
        /// Leave the current party
        /// </summary>
        [OperationContract]
        void LeaveParty(Party p);

        /// <summary>
        /// Selected players from online list get sent to the server
        /// </summary>
        /// <param name="players"></param>
        [OperationContract]
        void SendInvites(Party p, List<string> playerNames);

        /// <summary>
        /// Player answering the invite from the service(host)
        /// </summary>
        /// <param name="answer"></param>
        [OperationContract]
        bool AnswerInvite(Party p);

        /// <summary>
        /// 
        /// </summary>
        [OperationContract]
        void StartGame(string host);

        /// <summary>
        /// message sended in chat
        /// </summary>
        /// <param name="message"></param>
        [OperationContract]
        void SendMessageParty(string message, Party p);
        
        /// <summary>
        /// Subscribe to lobby events
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        void SubscribeToLobbyEvents(string username, string password);
    }
}
