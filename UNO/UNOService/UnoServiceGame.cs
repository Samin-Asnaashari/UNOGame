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

        public bool playCard(Card card)
        {

            Player PlayerWhoWantsToPlayACard = getPlayerFromGameContext();
            Game.Game game = PlayerWhoWantsToPlayACard.Game;
            //game.PlayedCards.LastOrDefault();
            if (game.CurrentPlayer.UserName == PlayerWhoWantsToPlayACard.UserName)
            {
                if (CheckPlayedCard(game.PlayedCards[game.PlayedCards.Count - 1], card))
                {
                    game.PlayedCards.Add(card);

                    PlayerWhoWantsToPlayACard.Remove(card);

                    foreach (Player p in game.Players)
                    {
                        if (p.UserName != PlayerWhoWantsToPlayACard.UserName) //Prevent deadlock
                            p.IGameCallback.CardPlayed(card, PlayerWhoWantsToPlayACard.UserName);
                    }

                    if (PlayerWhoWantsToPlayACard.Hand.Count() == 0)
                    {
                        //game.End();
                    }
                    else
                    {
                        if (game.PreviousPlayer.Hand.Count == 1 && !game.PreviousPlayer.UnoSaid)//server makes player save from being called by uno after next player's turn has been played
                        {
                            game.PreviousPlayer.UnoSaid = true;
                        }

                        game.CardAction(card);
                        game.EndTurn();

                    }
                    return true;
                }
                else
                {
                    return false;
                    //message in logstatus wrong card for playing
                }
            }
            else
                return false;

        }


        public bool CheckPlayedCard(Card tablecard, Card cardtoplay)
        {
            if (cardtoplay.Type == CardType.draw4Wild || cardtoplay.Type == CardType.wild)
            {
                return true;
            }
            else if(cardtoplay.Color == tablecard.Color || (cardtoplay.Type == CardType.normal && cardtoplay.Number == tablecard.Number))
            {
                return true;
            }
            else if(cardtoplay.Type == tablecard.Type && cardtoplay.Type != CardType.normal)
            {
                return true;
            }
            else 
            {
                return false;
            }
        }

        // TODO use password
        public void SubscribeToGameEvents(string userName)
        {
            IGameCallback clientCallbackGame = OperationContext.Current.GetCallbackChannel<IGameCallback>();

            Player self = playersOnline.Find(x => x.UserName == userName);

            self.IGameCallback = clientCallbackGame;

            Game.Game game = self.Game;

            //AssignCards(self,2);

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
                Card Cardtoassign = takeCard();
                player.Hand.Add(Cardtoassign);
                player.Game.Deck.Remove(Cardtoassign);
                cards.Add(Cardtoassign);
            }

            player.IGameCallback.CardsAssigned(cards, null);

        }

        public Card takeCard()
        {
            Player callingPlayer = getPlayerFromGameContext();
           Game.Game game = callingPlayer.Game;
            if (game.Deck.Count() == 0)
            {
                game.ReFillDeck();
            }
            //make it better 

            Card taken = game.Deck[0];
            game.Deck.RemoveAt(0);

            callingPlayer.UnoSaid = false;//if he had uno but could not play/win
            callingPlayer.AddCard(taken);

            foreach (var item in game.Players)
            {
                if (item.UserName != callingPlayer.UserName)
                    item.IGameCallback.NotifyPlayersNumberOfCardsTaken(1, callingPlayer.UserName);
            }

            return taken;

            //List<Card> card=new List<Card>();
            //card.Add(game.Deck[game.Deck.Count -1]);
            //getPlayerFromGameContext().IGameCallback.CardsAssigned(card, null);
            //return game.Deck[game.Deck.Count - 1];
        }


        public void SaveReplay(int gameID)
        {
            throw new NotImplementedException();//jndjnjdn
        }

        private bool Uno(Player playerWhoCalledUno, Game.Game game)
        {
            //Hi coen I dont want to change your code or delete hahah like someone did to mine so I wrote what I think is missing or incorrect.
            //so as we know players can call uno on the playerWhoShouldCallUNO only while the NEXT(player next to playerWhoShouldCallUNO depending on the current direcion of the game) player turn is not played.
            //So this check is missing here otherwise at the next next player turn(or any other turn hope you understand) anyone can still call uno on all(foreach loop) the players
            //that has one card in their hand and they will get punished at the moment. And also the playerWhoShouldCallUNO can also say uno at the next next player turn and be safe.
            //The method should be made from a perspective that you dont know which player is gonna call uno first but I think you know this. 
            //And the game.currentPlayer should match, at the moment of saying Uno or calling UNO on somebody, the player next to the playerWhoShouldCallUNO depending on the current direcion of the game.

            if (playerWhoCalledUno == game.PreviousPlayer && playerWhoCalledUno.Hand.Count == 1 && playerWhoCalledUno.UnoSaid == false) // Player called Uno on himself // added that piece just to not double put true if already true(if playerWhoShouldCallUNO say uno two times)
            {
                playerWhoCalledUno.UnoSaid = true;
                return true;
            }
            else // Call Uno on other players
            {
                List<Card> cards = new List<Card>();
                Player playerPunished = null;
                foreach (Player player in game.Players)
                {
                    if (player != playerWhoCalledUno)
                    {
                        if (player.Hand.Count == 1 && player.UnoSaid == false)
                        {
                            cards = game.PickANumberOfCardsFromDeck(2);
                            player.AddCard(cards);
                            player.IGameCallback.CardsAssigned(cards, null);
                            playerPunished = player;
                            break;
                            //player.UnoSaid = true;
                            //player.IGameCallback.CardsAssigned(
                        }
                    }
                }
                if (playerPunished != null)
                {
                    foreach (Player player in game.Players)
                    {
                        if (player.UserName != playerPunished.UserName)
                            player.IGameCallback.NotifyPlayersNumberOfCardsTaken(2, playerPunished.UserName);
                    }
                    return false;
                }
                else
                    return true;
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

            bool saved = false;

            if (message.ToLower().IndexOf("uno", 0, 3) == 0 && message.Length == 3)
            {
                if (!game.UnoSaidAlready)//prevent multiple unos and multiple incorrect punishments
                {
                    game.UnoSaidAlready = true;
                    saved = Uno(player, game);
                    game.UnoSaidAlready = false;
                }
                else
                    saved = true;
            }

            message = $"{player.UserName}: {message}";

            if (saved)
            {
                message += "(saved)";
            }
            else
                message += "(Punished)";

            foreach (Player currentPlayer in game.Players)
            {
                //if (currentPlayer != player)//how is the player who is sending the message gonna see the message in the chat?Handled in the client?
                {
                    currentPlayer.IGameCallback.SendMessageGameCallback(message);
                }
            }
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
            game.GiveEachPlayer7Cards();

            List<string> playersUserNames = new List<string>();

            foreach (var item in game.Players)
            {
                playersUserNames.Add(item.UserName);
            }

            foreach (Player player in game.Players)
            {
                player.IGameCallback.CardsAssigned(player.Hand, playersUserNames);
            }

            while(game.Deck.First().Type != CardType.normal)
            {
                game.PlayedCards.Add(game.Deck.First());
                game.Deck.RemoveAt(0); //Remove next card from deck
            }

            game.PlayedCards.Add(game.Deck.First());
            game.Deck.RemoveAt(0); //Remove next card from deck

            foreach (Player player in game.Players)
            {
                player.IGameCallback.CardPlayed(game.PlayedCards.Last(), "FirstAtStart");
                player.IGameCallback.TurnChanged(game.CurrentPlayer); //Notify that the host is the first player to start
            }
        }
    }
}
