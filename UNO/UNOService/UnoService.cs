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
                if (item.GameID == GameID)
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

        public void playCard(int GameID, Card card)
        {
            //get player 
            //is it valid card ?  // maybe with method better 
            //put cart in the table (public Card ACardOnTheTable { get; set; }  [playedcards.count-1])
            //add it to the game playedcard
            //delete from hand 
            //add to the last card of the deck of card (when a card assign to the player will be deleted from game deck of card)
            // if is it is  last card player won 
            //if after this play only one left another methode will take care of said uno condition
            Player PlayerWhoWantsToPlaACard = getPlayerFromGameContext();
                    FindGame(GameID).PlayedCards.Add(card);
                    PlayerWhoWantsToPlaACard.Remove(card);
                    FindGame(GameID).Deck.Add(card);
        }

        /// <summary>
        /// Party may not exist anymore, so use this method for safety
        /// </summary>
        /// <param name="username"></param>
        /// <param name="party"></param>
        /// <returns></returns>
        private bool tryGetPartyFromUsername(string username, out Party party)
        {
            party = parties.FirstOrDefault(x => x.Host.UserName == username);

            return party != null;
        }

        /// <summary>
        /// Username could be null if he logged out, so use this try method for safety.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="player"></param>
        /// <returns></returns>
        private bool tryGetPlayerFromUsername(string username, out Player player)
        {
            player = playersOnline.FirstOrDefault(x => x.UserName == username);

            return player != null;
        }

        /// <summary>
        /// Get the player from the game context. Safe to assume this is never null.
        /// </summary>
        /// <returns></returns>
        private Player getPlayerFromGameContext()
        {
            IGameCallback currentPlayerCallback = OperationContext.Current.GetCallbackChannel<IGameCallback>();

            return playersOnline.Find(x => x.IGameCallback == currentPlayerCallback);
        }


        /// <summary>
        /// Get the player from the lobby context. Safe to assume this is never null.
        /// </summary>
        /// <returns></returns>
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

        public void SendInvites(List<string> playerNames)
        {
            Player host = getPlayerFromLobbyContext();

            foreach (string username in playerNames)
            {
                Player player;

                if (tryGetPlayerFromUsername(username, out player))
                {
                    player.ILobbyCallback.ReceiveInvite(host.UserName);
                }
            }
        }

        public void StartGame(string partyID)
        {
            //maybe need to check authorized Host and players  
            if (GetPartyMembers(partyID).Count == 4)
            {
                Game.Game Game = new Game.Game(gameID, GetPartyMembers(partyID));
                gameID++;
                games.Add(Game);
                for (int i = 1; i < GetPartyMembers(partyID).Count; i++)
                {
                    //everyone will be notified except host
                    GetPartyMembers(partyID)[i].ILobbyCallback.NotifyGameStarted(partyID);
                }
                parties.Remove(parties.Find(x => x.PartyID.CompareTo(partyID) == 0));
            }
            else
            {
                throw new Exception("Party needs to be full....");
            }
          
        }


        public List<Player> GetPartyMembers(string partyID)
        {
            Party party; 

            if (tryGetPartyFromUsername(partyID, out party))
            {
                return party.Players;
            }

            return new List<Player>();
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


        public void SubscribeToGameEvents(string userName,int gameID)
        {
            IGameCallback clientCallbackGame = OperationContext.Current.GetCallbackChannel<IGameCallback>();
            Player player = games.Find(x => x.GameID == gameID).Players.Find(y => y.UserName.CompareTo(userName) == 0);
            player.IGameCallback = clientCallbackGame;

            foreach (var item in games.Find(x => x.GameID == gameID).Players)
            {
                if (item != player)
                {
                    //item.IGameCallback.CardsAssigned();
                    //item.IGameCallback.SendMessageGameCallback();  
                    //item.IGameCallback.TurnChanged(player);
                    //item.IGameCallback.NotifyOpponentsOfPlayerPunished(item.UserName);
                    //item.IGameCallback.CardPlayed();
                }
                    
            }
        }

        public void CreateParty()
        {
            Player host = getPlayerFromLobbyContext();
            parties.Add(new Party(host));
        }

        public void LeaveParty(string partyID)
        {
            Player WhoWantsToLeaveTheParty = getPlayerFromLobbyContext() ;
            Party currentParty = parties.Find(x => x.PartyID.CompareTo(partyID) == 0);

            currentParty.Players.Remove(WhoWantsToLeaveTheParty);

            if (WhoWantsToLeaveTheParty.UserName.CompareTo(partyID) == 0 )
            {
                for (int i = 0; i < currentParty.Players.Count; i++)
                {
                    currentParty.Players[i].ILobbyCallback.PlayerLeftParty(WhoWantsToLeaveTheParty);
                }
                parties.Remove(currentParty);
            }
            else
            {
                for (int i = 0; i < currentParty.Players.Count; i++)
                {
                    currentParty.Players[i].ILobbyCallback.PlayerLeftParty(WhoWantsToLeaveTheParty);
                }
            }
        }

        public bool AnswerInvite(bool answer, string partyID)
        {
            Player player = getPlayerFromLobbyContext();
            if (answer)
            {
                Party party;

                if (tryGetPartyFromUsername(partyID, out party))
                {
                    foreach (Player partyPlayer in party.Players)
                    {
                        partyPlayer.ILobbyCallback.PlayerAddedToParty(player.UserName);
                    }
                    party.Players.Add(player);
                    return true;
                }
            }

            return false;
        }

        public void SendMessageParty(string message, string partyID)
        {
            Player messageSender = getPlayerFromLobbyContext();

            Party party;

            message = $"{messageSender.UserName}: {message}";

            if (tryGetPartyFromUsername(partyID, out party))
            {
                foreach (Player player in party.Players)
                {
                    if (player != messageSender)
                    {
                        player.ILobbyCallback.SendChatMessageLobbyCallback(message);
                    }
                }
            }
        }
    }
}
