using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace UNOService
{
    [ServiceContract]
    public interface ILogin
    {
        /// <summary>
        /// Login to game
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [OperationContract]
        bool Login(String userName, String password);
    }


}
