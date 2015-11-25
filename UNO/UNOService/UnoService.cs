using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace UNOService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class UnoService : ILobby, ILogin, ISignUp, IGame
    {
		bool ILogin.Login(string userName, string password)
		{
			throw new NotImplementedException();
		}

		bool ISignUp.SignUp(string userName, string password)
		{
			throw new NotImplementedException();
		}

		String ISignUp.CheckUserName(String userName)
		{
			throw new NotImplementedException();
		}

		List<Player> ILobby.GetOnlineList()
		{
			throw new NotImplementedException();
		}

		void ILobby.CreateParty()
		{
			throw new NotImplementedException();
		}

		void ILobby.LeaveParty()
		{
			throw new NotImplementedException();
		}

		void ILobby.SendInvites(List<Player> players)
		{
			throw new NotImplementedException();
		}

		void ILobby.AnswerInvite(bool answer)
		{
			throw new NotImplementedException();
		}

		void ILobby.StartGame()
		{
			throw new NotImplementedException();
		}

		void ILobby.SendMessageLobby(string message)
		{
			throw new NotImplementedException();
		}

		
		void IGame.SaveReplay(int gameID)
		{
			throw new NotImplementedException();
		}

		Card IGame.takeCard()
		{
			throw new NotImplementedException();
		}

		void IGame.playCard(Card c)
		{
			throw new NotImplementedException();
		}

		void IGame.LeaveGame()
		{
			throw new NotImplementedException();
		}

		void IGame.SendMessageGame(string message)
		{
			throw new NotImplementedException();
		}
    }
}