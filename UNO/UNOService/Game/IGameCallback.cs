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
		/// The client gets an unspecified amount of cards.
		/// </summary>
		/// <param name="cards"></param>
		[OperationContract]
		void CardsAssigned(List<Card> cards);

		/// <summary>
		/// Will notify the client that someone's turn has ended and someone else turn starts.
		/// </summary>
		/// <param name="player"></param>
		[OperationContract]
		void TurnChanged(Player player);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="c"></param>
		void CardPlayed(Card c);
	}
}
