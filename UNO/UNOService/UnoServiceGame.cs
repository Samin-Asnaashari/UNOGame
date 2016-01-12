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

        public bool ValidPlayerTurn(string UserName)
        {
            Game.Game game = getPlayerFromGameContext().Game;
            if (game.CurrentPlayer.UserName == UserName)
            {
                return true;
            }
            else
                return false;
        }

        public bool TryPlayCard(Card card)
        {
            Player playerWhoWantsToPlayACard = getPlayerFromGameContext();
            Game.Game game = playerWhoWantsToPlayACard.Game;

            return game.TryPlayCard(playerWhoWantsToPlayACard, card);
        }

        // TODO use password
        public void SubscribeToGameEvents(string userName)
        {
            IGameCallback clientCallbackGame = OperationContext.Current.GetCallbackChannel<IGameCallback>();

            Player self = playersOnline.Find(x => x.UserName == userName);

            self.IGameCallback = clientCallbackGame;

            Game.Game game = self.Game;

            foreach (Player player in game.Players)
            {
                if (player.IGameCallback == null)
                    return;
            }

            game.Start();
        }


        public void SaveReplay()
        {
            throw new NotImplementedException();
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

            if (message.Length == 3 && message.ToLower().Contains("uno"))
            {
                game.Uno(player);
            }

            foreach (Player currentPlayer in game.Players)
            {
                if (currentPlayer != player)
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

        public void EndGame()
        {
            foreach (var player in getPlayerFromGameContext().Game.Players)
            {
                player.IGameCallback.EndOfTheGame(null);
            }
        }

        public void TakeCards()
        {
            Player player = getPlayerFromGameContext();
            Game.Game game = player.Game;

            if (player == player.Game.CurrentPlayer)
            {
                game.GiveCardsToPlayer(player);
            }
        }
    }
}
