using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UNOService.Game;
using System.ServiceModel;

namespace UNOService
{
    [ServiceContract(/*CallbackContract = typeof(IGameCallback)*/)]
    public interface IReplay
    {
        /// <summary>
        /// Saves the the game played with all moves
        /// </summary>
        [OperationContract]
        void SaveReplay(Player p);
    }
}
