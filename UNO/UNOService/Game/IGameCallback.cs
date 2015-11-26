﻿using System;
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
		[OperationContract]
		void CardsAssigned(List<Card> cards);

		/// <summary>
		/// Will notify the client that someone's turn has ended and someone else turn starts.
		/// </summary>
		/// <param name="player"></param>
		[OperationContract]
		void TurnChanged(Player player);

		/// <summary>
		/// Crad that has been played notify other opponents
		/// </summary>
		/// <param name="c"></param>
		void CardPlayed(Card c);

        /// <summary>
        /// message sended in chat
        /// </summary>
        /// <param name="message"></param>
        [OperationContract]
        void SendMessageGameCallback(string message);

        /// <summary>
        /// Notify other players about player left
        /// </summary>
        [OperationContract]
        void NotifyPlayerLeft(string userName);
    }
}
