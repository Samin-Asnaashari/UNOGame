using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using UNOService.Game;

namespace UNOService
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public partial class UnoService : ILoginAndSignUp, IGame
    {
        private static int gameID = 0;
        private DatabaseHandler databaseHandler;

        private List<Player> playersOnline = new List<Player>();
        private List<Game.Game> games = new List<Game.Game>();

        private delegate void AllPlayersConnectedEventHandler(object sender, Game.Game game);
        private event AllPlayersConnectedEventHandler AllPlayersConnected;

        public UnoService()
        {
            this.AllPlayersConnected += UnoService_AllPlayersConnected;
            databaseHandler = new DatabaseHandler();
        }

        public StatusCode Login(string userName, string password)
        {
            bool loginSuccessful = false;
            try
            {
                if (playersOnline.Find(x => x.UserName.ToLower() == userName.ToLower()) == null) //user not logged in yet
                {
                    loginSuccessful = databaseHandler.CheckLogin(userName, password);
                    if (loginSuccessful)
                        CreatePlayer(userName);
                    else
                        return new StatusCode(-21); //Credentials incorrect
                }
                else
                {
                    return new StatusCode(-20); //User already logged in
                }
            }
            catch (Exception e)
            {
                return new StatusCode(e.Message);
            }

            return new StatusCode(20); //Login successful
        }

        private void CreatePlayer(string username) //method maybe not needed
        {
            Player toBeAdded = new Player(username);
            toBeAdded.State = PlayerState.InLobby;
            playersOnline.Add(toBeAdded);
        }

        public StatusCode SignUp(string userName, string password)
        {
            if (CheckUserName(userName))
                return new StatusCode(-10); //Username is taken

            if (password.Length < 6)
                return new StatusCode(-11); //Password is too short

            try
            {
                databaseHandler.InsertPlayer(userName, password);
                CreatePlayer(userName);
                return new StatusCode(10); //Registration successful
            }
            catch (Exception e)
            {
                return new StatusCode(e.Message); //Error status depending on exception
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
                exist = false;
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
            Game.Game game = FindGame(GameID);
            Card c = game.Deck[0];
            game.Deck.RemoveAt(0);

            return c;
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
            Game.Game game = FindGame(GameID);

            Player PlayerWhoWantsToPlayACard = getPlayerFromGameContext();
            game.PlayedCards.Add(card);
            PlayerWhoWantsToPlayACard.Remove(card);
            game.Deck.Add(card);

            foreach (Player p in game.Players)
            {
                if (p.UserName != PlayerWhoWantsToPlayACard.UserName) //Prevent deadlock
                    p.IGameCallback.CardPlayed(card);
            }
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

        public void SubscribeToGameEvents(string userName, int gameID)
        {
            IGameCallback clientCallbackGame = OperationContext.Current.GetCallbackChannel<IGameCallback>();
            Game.Game game = games.Find(x => x.GameID == gameID);

            Player self = game.Players.Find(y => y.UserName.CompareTo(userName) == 0);
            self.IGameCallback = clientCallbackGame;

            foreach (Player player in game.Players)
            {
                if (player.IGameCallback == null)
                    return;
            }

            //Every player now has a game callback, so we can start sending events            
            if(AllPlayersConnected != null)
                AllPlayersConnected(this, game);
        }

        private void UnoService_AllPlayersConnected(object sender, Game.Game game)
        {
            game.CreateDeck();
            game.Shuffle();

            foreach (Player player in game.Players)
            {
                List<Card> hand = game.Deck.GetRange(0, 7);
                player.IGameCallback.CardsAssigned(hand);
                game.Deck.RemoveRange(0, 7); //Remove from deck
            }

            game.PlayedCards.Add(game.Deck.First());
            game.Deck.RemoveAt(0); //Remove next card from deck

            foreach (Player player in game.Players)
            {
                player.IGameCallback.CardPlayed(game.PlayedCards.First());
                player.IGameCallback.TurnChanged(game.CurrentPlayer); //Notify that the host is the first player to start
            }
        }
    }
}