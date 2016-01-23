using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace UNOService
{
    public interface IGameCallback
	{
		/// <summary>
		/// The client gets an unspecified amount of cards.
		/// </summary>
		/// <param name="cards"></param>
		[OperationContract(IsOneWay = true)]
		void AssignCards(List<Card> cards);

        [OperationContract(IsOneWay = true)]
        void InitializeGame(List<string> playersUserNames);

        [OperationContract(IsOneWay = true)]
        void NotifyPlayersNumberOfCardsTaken(int nrOfCardsTaken, string playerWhoTookCardsUserName);

        /// <summary>
        /// Crad that has been played notify other opponents
        /// </summary>
        /// <param name="c"></param>
        [OperationContract(IsOneWay = true)]
        void CardPlayed(Card c, string playerWhoPlayed);

        /// <summary>
        /// message sended in chat
        /// </summary>
        /// <param name="message"></param>
        [OperationContract(IsOneWay = true)]
        void SendMessageGameCallback(string message);

        [OperationContract(IsOneWay = true)]
        void EndOfTheGame(string winner);

        /// <summary>
        /// Will notify the client that someone's turn has ended and someone else turn starts.
        /// </summary>
        /// <param name="player"></param>
        [OperationContract(IsOneWay = true)]
        void TurnChanged(Player player);
    }
}
