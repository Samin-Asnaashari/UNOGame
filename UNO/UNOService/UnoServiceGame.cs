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

        public void SendMessageGame(string message)
        {
            Player player = getPlayerFromGameContext();
            Game.Game game = player.Game;

            if (message.Length == 3 && message.ToLower().Contains("uno"))
            {
                game.Uno(player);
            }

            foreach (Player otherplayer in game.Players)
            {
                if (otherplayer != player)
                {
                    otherplayer.IGameCallback.SendMessageGameCallback(message);
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
