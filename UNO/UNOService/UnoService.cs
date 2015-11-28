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

            foreach (var item in playersOnline)//for checking player legit and get gameID or party he is playing in right now
            {
                if (currentPlayerCallback == item.IGameCallback)
                {
                    return item;
                }
            }
            return null;
        }

        private List<Player> CalculateGamePlayerPositions(Player callingPlayer, Game.Game game)//Calculating player positions of a game based on the calling player parameter and depending on direction of the game
        {
            List<Player> OrderedPlayerPositions = new List<Player>();
            OrderedPlayerPositions[0] = callingPlayer;

            for (int i = 0; i < game.Players.Count; i++)
            {
                if (OrderedPlayerPositions[0] == game.Players[i])
                {
                    if (game.Direction == Direction.clockwise)
                    {
                        OrderedPlayerPositions[1] = game.Players[(i + 1) % 4];//nextToMePlayer
                        OrderedPlayerPositions[2] = game.Players[(i - 1) % 4];//previousToMePlayer
                        OrderedPlayerPositions[3] = game.Players[(i + 2) % 4];//Last PLayer
                    }
                    else
                    {
                        OrderedPlayerPositions[1] = game.Players[(i - 1) % 4];
                        OrderedPlayerPositions[2] = game.Players[(i + 1) % 4];
                        OrderedPlayerPositions[3] = game.Players[(i + 2) % 4];
                    }
                }
            }
            return OrderedPlayerPositions;

        }

        public void SendMessageGame(string message)
        {
            Player player = CheckPlayerLegitimacy();
            if (player != null)
            {
                Game.Game game = games.Find(x => x.GameID == player.GameID);

                //check if UNO said//

                if (!game.UNOsaidAlready)
                {
                    game.UNOsaidAlready = true;

                    List<Player> OrderedPlayers = CalculateGamePlayerPositions(player, game);

                    if (OrderedPlayers[0].Hand.Count == 1 && game.TurnToPlay.UserName == OrderedPlayers[1].UserName)
                    {
                        foreach (var item3 in game.Players)//send everybody the text message and maybe also saying that he is save from punishment cause he was the first
                        {
                            item3.IGameCallback.SendMessageGameCallback(message);
                        }
                    }
                    else if(OrderedPlayers[0].UserName == game.TurnToPlay.UserName && OrderedPlayers[2].Hand.Count == 1)
                    {
                        OrderedPlayers[2].IGameCallback.CardsAssigned(new List<Card>());//draw two cards from deck and punish the player //also a method needed to notify all other players for this change
                        foreach (var item3 in game.Players)
                        {
                            item3.IGameCallback.SendMessageGameCallback(message);
                            item3.IGameCallback.NotifyOpponentsOfPlayerPunished(OrderedPlayers[2].UserName);
                        }
                    }
                    else if(OrderedPlayers[2].UserName == game.TurnToPlay.UserName && OrderedPlayers[3].Hand.Count==1)
                    {
                        OrderedPlayers[3].IGameCallback.CardsAssigned(new List<Card>());//draw two cards from deck and punish the player //also a method needed to notify all other players for this change
                        foreach (var item3 in game.Players)
                        {
                            item3.IGameCallback.SendMessageGameCallback(message);
                            item3.IGameCallback.NotifyOpponentsOfPlayerPunished(OrderedPlayers[3].UserName);
                        }
                    }
                    else if(OrderedPlayers[3].UserName == game.TurnToPlay.UserName && OrderedPlayers[2].Hand.Count == 1)
                    {
                        OrderedPlayers[2].IGameCallback.CardsAssigned(new List<Card>());
                        foreach (var item3 in game.Players)
                        {
                            item3.IGameCallback.SendMessageGameCallback(message);
                            item3.IGameCallback.NotifyOpponentsOfPlayerPunished(OrderedPlayers[2].UserName);
                        }
                    }
                    game.UNOsaidAlready = false;
                }
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
