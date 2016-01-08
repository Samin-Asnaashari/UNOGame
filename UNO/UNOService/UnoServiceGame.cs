using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using UNOService.Game;

namespace UNOService
{
    partial class UnoService : IGame
    {
        private delegate void AllPlayersConnectedEventHandler(object sender, Game.Game game);
        private event AllPlayersConnectedEventHandler AllPlayersConnected;

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

            Player PlayerWhoWantsToPlayACard = getPlayerFromGameContext();
            Game.Game game = PlayerWhoWantsToPlayACard.Game;
            game.PlayedCards.Add(card);
            PlayerWhoWantsToPlayACard.Remove(card);
            game.Deck.Add(card);

            foreach (Player p in game.Players)
            {
                if (p.UserName != PlayerWhoWantsToPlayACard.UserName) //Prevent deadlock
                    p.IGameCallback.CardPlayed(card);
            }
        }

        // TODO use password
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
            if (AllPlayersConnected != null)
                AllPlayersConnected(this, game);
        }

        public void AssignCards(Player player, int numberofcardtoassign)
        {
            List<Card> cards = new List<Card>();
            for (int i = 0; i < numberofcardtoassign; i++)
            {
                Card Cardtoassign = takeCard(-1);
                player.Hand.Add(Cardtoassign);
                player.Game.Deck.Remove(Cardtoassign);
                cards.Add(Cardtoassign);
            }

            getPlayerFromGameContext().IGameCallback.CardsAssigned(cards);

        }

        public void SaveReplay(int gameID)
        {
            throw new NotImplementedException();//jndjnjdn
        }

        private void Uno(Player playerWhoCalledUno, Game.Game game)
        {
            //Hi coen I dont want to change your code or delete hahah like someone did to mine so I wrote what I think is missing or incorrect.
            //so as we know players can call uno on the playerWhoShouldCallUNO only while the NEXT(player next to playerWhoShouldCallUNO depending on the current direcion of the game) player turn is not played.
            //So this check is missing here otherwise at the next next player turn(or any other turn hope you understand) anyone can still call uno on all(foreach loop) the players
            //that has one card in their hand and they will get punished at the moment. And also the playerWhoShouldCallUNO can also say uno at the next next player turn and be safe.
            //The method should be made from a perspective that you dont know which player is gonna call uno first but I think you know this. 
            //And the game.currentPlayer should match, at the moment of saying Uno or calling UNO on somebody, the player next to the playerWhoShouldCallUNO depending on the current direcion of the game.
            //(Thats why my method calculatePlayerPositions that calculates positions depending on the player who is saying or calling Uno)

            if (playerWhoCalledUno == game.PreviousPlayer && playerWhoCalledUno.UnoSaid == false) // Player called Uno on himself // added that piece just to not double put true if already true(if playerWhoShouldCallUNO say uno two times)
            {
                playerWhoCalledUno.UnoSaid = true;
            }
            else // Call Uno on other players
            {
                foreach (Player player in game.Players)
                {
                    if (player != playerWhoCalledUno)
                    {
                        if (player.Hand.Count == 1 && player.UnoSaid == false)
                        {           
                            player.UnoSaid = true;
                            //player.IGameCallback.CardsAssigned(
                        }
                    }
                }
            }
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
            Game.Game game = player.Game;

            if (message.ToLower().Contains("uno"))
            {
                Uno(player, game);
            }

            message = $"{player.UserName}: {message}";

            foreach (Player currentPlayer in game.Players)
            {
                if (currentPlayer != player)//how is the player who is sending the message gonna see the message in the chat?Handled in the client?
                {
                    currentPlayer.IGameCallback.SendMessageGameCallback(message);
                }
            }
        }

        public Card takeCard(int removethis)
        {
            Player player = getPlayerFromGameContext();
            Game.Game game = player.Game;
            if (game.Deck.Count() == 0)
            {
                game.Deck = game.PlayedCards;
                game.Shuffle();
            }
            //???
            List<Card> c = new List<Card>();
            c.Add(game.Deck[0]);
            player.IGameCallback.CardsAssigned(c);
            return game.Deck[0];
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

        private void UnoServiceGame_AllPlayersConnected(object sender, Game.Game game)
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
