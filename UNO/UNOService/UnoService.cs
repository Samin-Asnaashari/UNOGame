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
    public partial class UnoService : ILoginAndSignUp, IGame
    {
        private static int gameID = 0;
        private DatabaseHandler databaseHandler;

        private List<Player> playersOnline = new List<Player>();
        private List<Game.Game> games = new List<Game.Game>();

        public UnoService()
        {
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

        public Card takeCard()
        {
            Game.Game game = FindGame(getPlayerFromGameContext().GameID);
            if (game.Deck.Count() == 0)
            {
                game.Deck = game.tempDeck;
                game.Shuffle(game.Deck);
            }
            //???
            List<Card> c = new List<Card>();
            c.Add(game.Deck[0]);
            getPlayerFromGameContext().IGameCallback.CardsAssigned(c);
            return game.Deck[0];
        }

        public bool CheckPlayedCard(Card tablecard,Card cardtoplay)
        {
            if(cardtoplay.Type == CardType.Draw4Wild || cardtoplay.Type == CardType.Wild)
            {
                return true;
            }
            else
            {
                if(cardtoplay.Type == tablecard.Type || cardtoplay.Color == tablecard.Color)
                {
                    return true;
                }

                return false;
            }
        }

        public bool playCard(Card card)
        {
            Player PlayerWhoWantsToPlayCard = getPlayerFromGameContext();
            Game.Game g = FindGame(PlayerWhoWantsToPlayCard.GameID);
            if (CheckPlayedCard(FindGame(PlayerWhoWantsToPlayCard.GameID).PlayedCards[0],card))
            {
                g.PlayedCards.Add(card);
                PlayerWhoWantsToPlayCard.Hand.Remove(card);
                g.tempDeck.Add(card);

                getPlayerFromGameContext().IGameCallback.CardPlayed(card);
                Player next = g.Players[g.Players.IndexOf(getPlayerFromGameContext()) + 1];
                g.TurnToPlay = next;
                getPlayerFromGameContext().IGameCallback.TurnChanged(next);
                CardAction(card);  //??

                return true;
            }
            return false;

            //check if won   / check if said uno 
        }

        public void CardAction(Card card)
        {
            Game.Game g = FindGame(getPlayerFromGameContext().GameID);

            if (card.Type == CardType.Skip)
            {
                Player next = g.Players[g.Players.IndexOf(getPlayerFromGameContext())+2];
                g.TurnToPlay = next;
                getPlayerFromGameContext().IGameCallback.TurnChanged(next);
            }
            else if(card.Type == CardType.Reverse)
            {
                if(g.Direction == Direction.clockwise)
                {
                    g.Direction = Direction.counterClockwise;
                }
                else
                {
                    g.Direction = Direction.clockwise;
                }
            }
            else if (card.Type == CardType.Draw2)
            {
                //wait for next player action
                //AssignCard to next player if he/she didn't put also draw2 otherwise add 4 to next next 
            }
            else if (card.Type == CardType.Wild)
            {
                //show color to other player to choose
            }
            else if (card.Type == CardType.Draw4Wild)
            {
                //AssignCards4
            }
            //notify who punished 
        }

        public void AssignCards(Player player ,int numberofcardtoassign)
        {
            List<Card> cards = new List<Card>();
            for (int i = 0; i < numberofcardtoassign; i++)
            {
                Card Cardtoassign = takeCard();
                player.Hand.Add(Cardtoassign);
                FindGame(player.GameID).Deck.Remove(Cardtoassign);
                cards.Add(Cardtoassign);
            }

            getPlayerFromGameContext().IGameCallback.CardsAssigned(cards);

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

        //public void GetPlayerFromUsername(string username, out Player player)
        //{
        //    player = playersOnline.FirstOrDefault(x => x.UserName == username);
        //}
        //public void GetGame(string username, out Game.Game game)
        //{
        //    Player p;
        //    GetPlayerFromUsername(username, out p);
        //    game = games.FirstOrDefault(x => x.GameID == p.GameID);
        //}

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

        public void SubscribeToGameEvents(string userName)
        {
            AssignCards(getPlayerFromGameContext(),7);
                    
            
        }

    }
}
