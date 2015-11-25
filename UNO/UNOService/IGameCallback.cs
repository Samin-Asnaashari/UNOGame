using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace UNOService
{
	interface IGameCallback
	{
		/// <summary>
		/// Tells the client only what his card deck is.
		/// </summary>
		/// <param name="deck"></param>
		[OperationContract]
		void DeckAssigned(List<Card> deck);

		/// <summary>
		/// Will notify the client that someone's turn has ended and someone else turn starts.
		/// </summary>
		/// <param name="player"></param>
		[OperationContract]
		void TurnChanged(Player player);
	}
}
