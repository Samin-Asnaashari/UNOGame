using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace UNOService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class UnoService : ILoginAndSignUp, ILobby, IGame
    {
        public void AnswerInvite(bool answer)
        {
            throw new NotImplementedException();
        }

        //    public void Login(string userName, string password)
        //    {
        //        throw new NotImplementedException();
        //    }

        //    bool ILogin.Login(string userName, string password)
        //    {
        //        throw new NotImplementedException();
        //    }
        public string CheckUserName(string userName)
        {
            throw new NotImplementedException();
        }

        public void CreateParty()
        {
            throw new NotImplementedException();
        }

        public List<Player> GetOnlineList()
        {
            throw new NotImplementedException();
        }

        public void LeaveGame()
        {
            throw new NotImplementedException();
        }

        public void LeaveParty()
        {
            throw new NotImplementedException();
        }

        public bool Login(string userName, string password)
        {
            throw new NotImplementedException();
        }

        public void playCard(Card c)
        {
            throw new NotImplementedException();
        }

        public void SaveReplay(int gameID)
        {
            throw new NotImplementedException();
        }

        public void SendInvites(List<Player> players)
        {
            throw new NotImplementedException();
        }

        public void SendMessageGame(string message)
        {
            throw new NotImplementedException();
        }

        public void SendMessageLobby(string message)
        {
            throw new NotImplementedException();
        }

        public bool SignUp(string userName, string password)
        {
            throw new NotImplementedException();
        }

        public void StartGame()
        {
            throw new NotImplementedException();
        }

        public Card takeCard()
        {
            throw new NotImplementedException();
        }
    }
}
