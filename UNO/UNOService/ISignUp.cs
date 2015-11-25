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
        /// <summary>
        /// player sign up
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [OperationContract]
        bool SignUp(string userName, string password);

        /// <summary>
        /// Username is send to service to be check for uniqueness
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        [OperationContract]
        String CheckUserName(String userName);
    }
}
