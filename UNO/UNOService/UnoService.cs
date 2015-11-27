using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using UNOService.Game;

namespace UNOService
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class UnoService : ILoginAndSignUp, IGame, ILobby
    {
        private static int gameID = 0;
        private static int partyID = 0;
        private DatabaseHandler databaseHandler;

        private List<Player> playersOnline;
        private List<Game.Game> games;
        private List<Party> parties;


        public UnoService()
        {
            databaseHandler = new DatabaseHandler();
            playersOnline = new List<Player>();
        }

        public bool Login(string userName, string password)
        {
            bool loginSuccessFull = false;
            try
            {

                if (playersOnline.Find(x => x.UserName.ToLower() == userName.ToLower()) == null)//user already logged in
                {
                    loginSuccessFull = databaseHandler.CheckLogin(userName, password);
                    if (loginSuccessFull)
                        CreatePlayerForLobby(userName);
                }
            }
            catch (Exception)
            {
                return false;
            }
            return loginSuccessFull;
        }

        private void CreatePlayerForLobby(string username)//method maybe not needed
        {
            Player toBeAdded = new Player(username);
            toBeAdded.State = PlayerState.InLobby;
            playersOnline.Add(toBeAdded);
        }

        public bool SignUp(string userName, string password)
        {
            try
            {
                databaseHandler.InsertPlayer(userName, password);
                CreatePlayerForLobby(userName);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool CheckUserName(string userName)
        {
            bool exist;
            try
            {
                exist = databaseHandler.CheckUserName(userName);
            }
            catch (Exception)
            {
                return false;
            }

            return exist;
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

        private Player CheckPlayerLegitimacy()
        {
            IGameCallback currentPlayerCallback = OperationContext.Current.GetCallbackChannel<IGameCallback>();

            foreach (var item in playersOnline)//for checking player legit and get gameID he is playing in right now
            {
                if (currentPlayerCallback == item.IGameCallback)
                {
                    return item;
                }
            }
            return null;
        }

        public void SendMessageGame(string message, int GameID)//game id not needed the calling player we will get him from game.players list with his operationContext //here we also know that his gameID is legit and also he is a real player of the game
        {
            Player player = CheckPlayerLegitimacy();
            if (player != null)
            {
                Game.Game game = games.Find(x => x.GameID == player.GameID);

                //check if UNO said 

                if (!game.UNOsaidAlready)
                {
                    game.UNOsaidAlready = true;

                    //check if next player turn is not up, next player dependency depends on direction of the game

                    foreach (var item in game.Players)
                    {
                        if (item.IGameCallback == player.IGameCallback)
                        {
                            if (item.Hand.Count == 1)
                            {
                                foreach (var item2 in game.Players)
                                {
                                    if (item.UnoSaid > item2.UnoSaid)
                                    {
                                        item.IGameCallback.CardsAssigned(new List<Card>());//draw two cards from deck //also a method needed to notify all other players for this change
                                        foreach (var item3 in game.Players)
                                        {
                                            item3.IGameCallback.SendMessageGameCallback(message);
                                        }
                                        return;
                                    }
                                }
                            }
                        }
                    }
                }
                //at the end put uno saidAlready to false again
            }
        }

        public List<Player> GetOnlineList()
        {
            return playersOnline;
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

        public void SubScribeToLobbyEvents(string username)
        {
            ILobbyCallback clientCallbackLobby = OperationContext.Current.GetCallbackChannel<ILobbyCallback>();
            Player player = playersOnline.Find(x => x.UserName == username);
            player.ILobbyCallback = clientCallbackLobby;

            foreach (var item in playersOnline)
            {
                if (item != player)
                    item.ILobbyCallback.PlayerConnected(player);
            }
        }
    }
}
