using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace UNOService
{
    interface ILobby
    {
        [OperationContract]
        List<Player> GetOnlineList();

        [OperationContract]
        void CreateParty();

        [OperationContract]
        void LeaveParty();

        [OperationContract]
        void SendInvites(List<Player> players);

        [OperationContract]
        void AnswerInvite(bool answer);
    }
}
