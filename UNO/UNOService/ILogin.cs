using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace UNOService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
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
