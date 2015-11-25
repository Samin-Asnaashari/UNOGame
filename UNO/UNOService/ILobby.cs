using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace UNOService
{
    [ServiceContract(CallbackContract = typeof(ILobbyCallback))]
    interface ILobby
    {
        /// <summary>
        /// Player get the current online list when entering lobby
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<Player> GetOnlineList();

        /// <summary>
        /// Party signal sent to be created
        /// </summary>
        [OperationContract]
        void CreateParty();

        /// <summary>
        /// Leave the current party
        /// </summary>
        [OperationContract]
        void LeaveParty();

        /// <summary>
        /// Selected players from online list get sent to the server
        /// </summary>
        /// <param name="players"></param>
        [OperationContract]
        void SendInvites(List<Player> players);

        /// <summary>
        /// Player answering the invite from the service(host)
        /// </summary>
        /// <param name="answer"></param>
        [OperationContract]
        void AnswerInvite(bool answer);

        /// <summary>
        /// 
        /// </summary>
        [OperationContract]
        void StartGame();

        /// <summary>
        /// message sended in chat
        /// </summary>
        /// <param name="message"></param>
        [OperationContract]
        void SendMessageLobby(string message);
    }
}
