using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace UNOService
{
    [ServiceContract]
    public interface ILoginAndSignUp
    {
        /// <summary>
        /// Login to game
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [OperationContract]
        bool Login(String userName, String password);

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
