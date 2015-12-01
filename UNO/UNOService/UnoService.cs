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
        private DatabaseHandler databaseHandler;

        private List<Player> playersOnline = new List<Player>();
        private List<Game.Game> games = new List<Game.Game>();
        private List<Party> parties = new List<Party>();

        static Action<Player> whoseTurnIs = delegate { };
        static Action<Card> TableCard = delegate { };
        static Action<String> NewMessage = delegate { };
        static Action<string> PlayerPunnished = delegate { }; //who is punhed how many cards 


        public UnoService()
        {
            databaseHandler = new DatabaseHandler();
        }

        public bool Login(string userName, string password)
        {
            bool loginSuccessFull = false;
            try
            {
                if (playersOnline.Find(x => x.UserName.ToLower() == userName.ToLower()) == null)//user not logged in yet
                {
                    loginSuccessFull = databaseHandler.CheckLogin(userName, password);
                    if (loginSuccessFull)
                        CreatePlayer(userName);
                }
            }
            catch (Exception)
            {
                return false;
            }
            return loginSuccessFull;
        }

        private void CreatePlayer(string username)//method maybe not needed
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
                CreatePlayer(userName);
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

        public Game.Game FindGame(int GameID)
        {
            foreach (var item in games)
            {
                if (item.GameID==GameID)
                {
                    return item;
                }
            }
            return null;
        }

        public Card takeCard(int GameID)
        {
            return FindGame(GameID).Deck[0];
        }

        public void playCard(int GameID,Card card)
        {
            //get player 
            //is it valid card ?  // maybe with method better 
            //put cart in the table (public Card ACardOnTheTable { get; set; }  [playedcards.count-1])
            //add it to the game playedcard
            //delete from hand 
            // if is it is  last card player won 
            //if after this play only one left another methode will take care of said uno condition
            Player PlayerWhoWantsToPlaACard = getPlayerFromGameContext();
            for (int i = 0; i < PlayerWhoWantsToPlaACard.Hand.Count; i++)
            {
                if(PlayerWhoWantsToPlaACard.Hand[i] ==card)
                {
                    FindGame(GameID).PlayedCards.Add(PlayerWhoWantsToPlaACard.Hand[i]);
                    PlayerWhoWantsToPlaACard.Hand.Remove(PlayerWhoWantsToPlaACard.Hand[i]);
                    break;
                }
            }
        }

        private bool tryGetPlayerFromUsername(string username, out Player player)
        {
            player = playersOnline.FirstOrDefault(x => x.UserName == username);

            return player != null;
        }

        private Player getPlayerFromGameContext()
        {
            IGameCallback currentPlayerCallback = OperationContext.Current.GetCallbackChannel<IGameCallback>();

            return playersOnline.Find(x => x.IGameCallback == currentPlayerCallback);
        }

        private Player getPlayerFromLobbyContext()
        {
            ILobbyCallback currentPlayerCallback = OperationContext.Current.GetCallbackChannel<ILobbyCallback>();

            return playersOnline.Find(x => x.ILobbyCallback == currentPlayerCallback);
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
            Player player = getPlayerFromGameContext();
            if (player != null)
            {
                Game.Game game = games.Find(x => x.GameID == player.GameID);

                bool messageAlreadySent = false;

                if (message.ToLower().IndexOf("uno", 0, 3) == 0 && message.Length == 3)//checking for uno
                {
                    if (!game.UNOsaidAlready)
                    {
                        game.UNOsaidAlready = true;//make sure that only one 'saying uno' at a time

                        List<Player> OrderedPlayers = CalculateGamePlayerPositions(player, game);

                        if (OrderedPlayers[0].Hand.Count == 1 && game.TurnToPlay.UserName == OrderedPlayers[1].UserName && !OrderedPlayers[0].UnoSaid)
                        {
                            OrderedPlayers[0].UnoSaid = true; //put this variable also true if after next player turn nobody said uno in turn to play and make it untrue if player has one card but has to draw a card
                            foreach (var item3 in game.Players)//send everybody the text message and maybe also saying that he is save from punishment cause he was the first
                            {
                                item3.IGameCallback.SendMessageGameCallback(message);
                            }
                            messageAlreadySent = true;
                        }
                        else if (OrderedPlayers[0].UserName == game.TurnToPlay.UserName && OrderedPlayers[2].Hand.Count == 1 && !OrderedPlayers[2].UnoSaid)
                        {
                            OrderedPlayers[2].IGameCallback.CardsAssigned(new List<Card>());//draw two cards from deck and punish the player //also a method needed to notify all other players for this change
                            OrderedPlayers[2].AddCard(new List<Card>());//same cards that have been sent
                            foreach (var item3 in game.Players)
                            {
                                item3.IGameCallback.SendMessageGameCallback(message);
                                item3.IGameCallback.NotifyOpponentsOfPlayerPunished(OrderedPlayers[2].UserName);
                            }
                            messageAlreadySent = true;
                        }
                        else if (OrderedPlayers[2].UserName == game.TurnToPlay.UserName && OrderedPlayers[3].Hand.Count == 1 && !OrderedPlayers[3].UnoSaid)
                        {
                            OrderedPlayers[3].IGameCallback.CardsAssigned(new List<Card>());//draw two cards from deck and punish the player //also a method needed to notify all other players for this change
                            OrderedPlayers[3].AddCard(new List<Card>());//same cards that have been sent
                            foreach (var item3 in game.Players)
                            {
                                item3.IGameCallback.SendMessageGameCallback(message);
                                item3.IGameCallback.NotifyOpponentsOfPlayerPunished(OrderedPlayers[3].UserName);
                            }
                            messageAlreadySent = true;
                        }
                        else if (OrderedPlayers[3].UserName == game.TurnToPlay.UserName && OrderedPlayers[2].Hand.Count == 1 && !OrderedPlayers[2].UnoSaid)
                        {
                            OrderedPlayers[2].IGameCallback.CardsAssigned(new List<Card>());
                            OrderedPlayers[3].AddCard(new List<Card>());//same cards that have been sent
                            foreach (var item3 in game.Players)
                            {
                                item3.IGameCallback.SendMessageGameCallback(message);
                                item3.IGameCallback.NotifyOpponentsOfPlayerPunished(OrderedPlayers[2].UserName);
                            }
                            messageAlreadySent = true;
                        }
                        game.UNOsaidAlready = false;
                    }
                }

                if (!messageAlreadySent)
                {
                    foreach (var item3 in game.Players)
                    {
                        item3.IGameCallback.SendMessageGameCallback(message);
                    }
                }
            }
        }

       
        public List<Player> GetOnlineList()
        {
            ILobbyCallback currentPlayerCallback = OperationContext.Current.GetCallbackChannel<ILobbyCallback>();

            return playersOnline.Where(x => x.ILobbyCallback != currentPlayerCallback).ToList();
        }

        public void SendInvites(List<Player> players)
        {
            Player host = getPlayerFromLobbyContext();

            foreach (Player sentPlayer in players)
            {
                Player actualPlayer;

                if (tryGetPlayerFromUsername(sentPlayer.UserName, out actualPlayer))
                {
                    actualPlayer.ILobbyCallback.ReceiveInvite(host.UserName);
                }
            }
        }

        public void StartGame(int GameID)
        {
            throw new NotImplementedException();
        }


        public List<Player> GetPartyMembers()
        {
            throw new NotImplementedException();
        }

        public void SubscribeToLobbyEvents(string username, string password)
        {
            //TODO Validate subscription using password
            ILobbyCallback clientCallbackLobby = OperationContext.Current.GetCallbackChannel<ILobbyCallback>();
            Player player = playersOnline.Find(x => x.UserName == username);
            player.ILobbyCallback = clientCallbackLobby;

            foreach (var item in playersOnline)
            {
                if (item != player)
                    item.ILobbyCallback.PlayerConnected(player);
            }
        }

        //Do we need fire the event ????????????????
        public void SubscribeToGameEvents(GameEventType GameEventMask/*string username*/)
        {
            Player CurrentPlayer = getPlayerFromGameContext();
            if (GameEventMask == GameEventType.Turn)
            {
                whoseTurnIs += CurrentPlayer.IGameCallback.TurnChanged;
            }
            else if (GameEventMask == GameEventType.CardOnTHeTable)
            {
                TableCard += CurrentPlayer.IGameCallback.CardPlayed;
            }
            else if (GameEventMask == GameEventType.Message)
            {
                NewMessage += CurrentPlayer.IGameCallback.SendMessageGameCallback;
            }
            else if (GameEventMask == GameEventType.OnePlayerPunished)
            {
                PlayerPunnished += CurrentPlayer.IGameCallback.NotifyOpponentsOfPlayerPunished;
            }
        }

        public void CreateParty(string partyID)
        {
            Player player = getPlayerFromLobbyContext();
            Player host;

            if (tryGetPlayerFromUsername(partyID, out host))
            {
                parties.Add(new Party(host));
            }
        }

        public void LeaveParty(string partyID)
        {
            throw new NotImplementedException();
        }

        public bool AnswerInvite(bool answer, string partyID)
        {
            throw new NotImplementedException();
        }

        public void SendMessageParty(string message, string partyID)
        {
            throw new NotImplementedException();
        }
    }
}
