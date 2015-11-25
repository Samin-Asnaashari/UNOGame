using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace UNOService
{
	[ServiceContract(CallbackContract = typeof(IGameCallback))]
	interface IGame
	{
        /// <summary>
        /// Saves the the game played with all moves
        /// </summary>
        [OperationContract]
        void SaveReplay(int gameID);
		//List<Player> players;

		/// <summary>
		/// Assigns the taken card to the players deck and returns that card.
		/// </summary>
		/// <permission>Only the player whose turn it is can take a card.</permission>
		/// <returns>Card</returns>
		[OperationContract]
		Card takeCard();

		/// <summary>
		/// Player who calls this metod will play card c.
		/// </summary>
		/// <permission>Only the player whose turn it is can play a card.</permission>
		/// <param name="c"></param>
		[OperationContract]
		void playCard(Card c);

        /// <summary>
        /// Player leaves the game when playing
        /// </summary>
        [OperationContract]
        void LeaveGame();

        /// <summary>
        /// message sended in chat
        /// </summary>
        /// <param name="message"></param>
        [OperationContract]
        void SendMessageGame(string message);
    }
}
