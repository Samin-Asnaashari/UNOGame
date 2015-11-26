using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace UNOService
{
    [ServiceContract]
    public interface ILoginAndSignUp
    {
        /// <summary>
        /// Login the guest
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [OperationContract]
        bool Login(string userName, string password);

        /// <summary>
        /// SignUp the guest
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [OperationContract]
        bool SignUp(string userName, string password);
        
        /// <summary>
        /// CheckUserName for uniqueness
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [OperationContract]
        bool CheckUserName(string userName);
    }
}
