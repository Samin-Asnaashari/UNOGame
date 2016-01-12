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

            Player player = playersOnline.Find(x => x.UserName == userName);

            player.IGameCallback = clientCallbackGame;

            Game.Game game = player.Game;

            foreach (Player otherPlayers in game.Players)
            {
                if (otherPlayers.IGameCallback == null)
                    return;
            }

            game.StartGame();
        }

        public void SaveReplay()
        {
            throw new NotImplementedException();
        }

        public void SendMessageGame(string message)
        {
            Player player = getPlayerFromGameContext();
            Game.Game game = player.Game;

            game.SendMessage(player, message);
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

        public void ChooseNotToPlayCard()
        {
            Player player = getPlayerFromGameContext();
            Game.Game game = player.Game;

            game.ChooseNotToPlayCard(player);
        }

        public bool IsValidCard(Card card)
        {
            Player player = getPlayerFromGameContext();
            Game.Game game = player.Game;

            return game.IsValidCard(player, card);
        }
    }
}
