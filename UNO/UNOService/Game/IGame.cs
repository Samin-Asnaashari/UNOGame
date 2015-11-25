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
	}
}
