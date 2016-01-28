using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using UNOService.Game;

namespace UNOService
{
    [ServiceContract(CallbackContract = typeof(IGameCallback))]
    [ServiceKnownType(typeof(CardColor))]
    public interface IGame
    {
        /// <summary>
        /// Saves the the game played with all moves
        /// </summary>
        [OperationContract]
        void SaveReplay();

        /// <summary>
        /// Assigns the taken card to the players deck and returns that card.
        /// </summary>
        /// <permission>Only the player whose turn it is can take a card.</permission>
        /// <returns>Card</returns>
        [OperationContract(IsOneWay = true)]
        void TakeCards();

        /// <summary>
        /// Player who calls this metod will play card c.
        /// </summary>
        /// <permission>Only the player whose turn it is can play a card.</permission>
        /// <param name="c"></param>
        /// 
        [OperationContract]
        bool TryPlayCard(Card card);

        /// <summary>
        /// message sended in chat
        /// </summary>
        /// <param name="message"></param>
        [OperationContract(IsOneWay = true)]
        void SendMessageGame(string message);

        /// <summary>
        /// Subscribe to All Game Events
        /// </summary>
        [OperationContract(IsOneWay = true)]
        void SubscribeToGameEvents(string UserName);

        [OperationContract(IsOneWay = true)]
        void SubscribeToReplayGameEvents(string UserName);

        [OperationContract(IsOneWay = true)]
        void EndGame();

        /// <summary>
        /// When a played picks a card, they do not have to play it.
        /// </summary>
        [OperationContract(IsOneWay = true)]
        void ChooseNotToPlayCard();

        [OperationContract]
        List<Move> GetMoves();

    }
}
