using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using UNOService.Game;

namespace UNOService
{
    public class UnoService : ILoginAndSignUp, IGame, ILobby
    {
        private List<Game.Game> games;
        private List<Party> parties;
        private static int gameID = 0;
        private static int partyID = 0;

        public UnoService()
        {

        }

        public bool Login(string userName, string password)
        {
            throw new NotImplementedException();
        }

        public bool SignUp(string userName, string password)
        {
            throw new NotImplementedException();
        }

        public string CheckUserName(string userName)
        {
            throw new NotImplementedException();
        }

        public void SaveReplay(int gameID)
        {
            throw new NotImplementedException();
        }

        public Card takeCard(int GameID)
        {
            throw new NotImplementedException();
        }

        public void playCard(int GameID)
        {
            throw new NotImplementedException();
        }

        public void LeaveGame(int GameID)
        {
            throw new NotImplementedException();
        }

        public void SendMessageGame(string message, int GameID)
        {
            throw new NotImplementedException();
        }

        public List<Player> GetOnlineList()
        {
            throw new NotImplementedException();
        }

        public void SendInvites(List<Player> players)
        {
            throw new NotImplementedException();
        }

        public bool AnswerInvite(bool answer)
        {
            throw new NotImplementedException();
        }

        public void StartGame(int GameID)
        {
            throw new NotImplementedException();
        }

        public void SendMessageLobby(string message, int partyID)
        {
            throw new NotImplementedException();
        }

        public void CreateParty(int partyID)
        {
            throw new NotImplementedException();
        }

        public void LeaveParty(int partyID)
        {
            throw new NotImplementedException();
        }

        public List<Player> GetPartyMembers()
        {
            throw new NotImplementedException();
        }
    }
}
