using System;
using System.Collections.Generic;
using System.Globalization;
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

        private List<Player> playersOnline;
        private List<Game.Game> games;
        private List<Party> parties;

        static Action<Player> whoseTurnIs = delegate { };
        static Action<Card> TableCard = delegate { };
        static Action<String> NewMessage = delegate { };
        static Action<string> PlayerPunnished = delegate { }; //who is punhed how many cards 


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
            //TODO If player plays his second card, set his UnoSafe to false

            //get player 
            //is it valid card ?  // maybe with method better 
            //put cart in the table (public Card ACardOnTheTable { get; set; }  [playedcards.count-1])
            //add it to the game playedcard
            //delete from hand 
            // if is it is  last card player won 
            //if after this play only one left another methode will take care of said uno condition
            Player PlayerWhoWantsToPlayCard = getPlayerFromGameContext();
            for (int i = 0; i < PlayerWhoWantsToPlayCard.Hand.Count; i++)
            {
                if (PlayerWhoWantsToPlayCard.Hand[i] == card)
                {
                    FindGame(GameID).PlayedCards.Add(PlayerWhoWantsToPlayCard.Hand[i]);
                    PlayerWhoWantsToPlayCard.Hand.Remove(PlayerWhoWantsToPlayCard.Hand[i]);
                    break;
                }
            }
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

        public void SendMessageGame(string message)
        {
            Player player = getPlayerFromGameContext();
            Game.Game game = games.Find(x => x.GameID == player.GameID);

            if (message.ToLower().Contains("uno"))
            {
                Uno(player, game);
            }

            message = $"{player.UserName}: {message}";

            foreach (Player currentPlayer in game.Players)
            {
                if (currentPlayer != player)
                {
                    currentPlayer.IGameCallback.SendMessageGameCallback(message);
                }
            }

        }

        private void Uno(Player playerWhoCalledUno, Game.Game game)
        {
            if (playerWhoCalledUno == game.PreviousPlayer) // Player called Uno on himself
            {
                playerWhoCalledUno.UnoSafe = true;
            }
            else // Call Uno on other players
            {
                foreach (Player player in game.Players)
                {
                    if (player != playerWhoCalledUno)
                    {
                        if (player.Hand.Count == 1 && player.UnoSafe == false)
                        {
                            //player.IGameCallback.CardsAssigned(// 2 cards from pile)
                        }
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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
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
