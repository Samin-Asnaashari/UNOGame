using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace UNOService
{
    interface ISignUp
    {
        [OperationContract]
        bool SignUp(string userName, string password);

        [OperationContract]
        String CheckUserName(String userName);
    }
}
