﻿using System;
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

            //game.moves.Add((new Move(playerWhoWantsToPlayACard.UserName, game.GameID, card, Move.Types.Play)));

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

        public void SubscribeToReplayGameEvents(string userName)
        {
            IGameCallback clientCallbackGame = OperationContext.Current.GetCallbackChannel<IGameCallback>();
            Player player = playersOnline.Find(x => x.UserName == userName);
            player.IGameCallback = clientCallbackGame;
            Game.Game game = player.Game;
            game.StartGameReplay(player);
        }

        public void SaveReplay()  //after game window 
        {
            Player p = getPlayerFromGameContext();
            databaseHandler.InsertGamePlayed(p);

            databaseHandler.InsertPlayers(p.Game.Players);

            foreach (var item in p.Game.moves)
            {
                //databaseHandler.InsertMove(p.Game.GameID,p.UserName,item.Type);
                databaseHandler.InsertMove(p.Game.GameID, p.UserName, databaseHandler.FindCard(item.card), item.Type);
            }
            p.Game.InsertDeckIntoDatabase();
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
            Game.Game game = getPlayerFromGameContext().Game;

            if (!game.hasGameEnded()) //Otherwise we are sending EndOfTheGame(null) to the winner
            {
                foreach (var player in game.Players)
                {
                    player.IGameCallback.EndOfTheGame(null);
                }
            }
            ////who won ?
            //foreach (var player in getPlayerFromGameContext().Game.Players)
            //{
            //    if (player != getPlayerFromGameContext())
            //    {
            //        player.IGameCallback.EndOfTheGame(null); //insert the winner 
            //    }
            //    databaseHandler.AddGamesPlayed(player.UserName);
            //}
            //databaseHandler.AddPlayerWon();
        }

        public void TakeCards()
        {
            Player player = getPlayerFromGameContext();
            Game.Game game = player.Game;

            if (player == player.Game.CurrentPlayer)
            {
                //game.moves.Add((new Move(player.UserName, game.GameID, Move.Types.Take)));
                game.GiveCardsToPlayer(player);
            }
        }

        public void ChooseNotToPlayCard()
        {
            Player player = getPlayerFromGameContext();
            Game.Game game = player.Game;

            //game.moves.Add((new Move(player.UserName, game.GameID, Move.Types.Keep)));
            game.ChooseNotToPlayCard(player);
        }

        public List<Move> GetMoves()
        {
            //return databaseHandler.GettMoves(getPlayerFromGameContext().Game.GameID);
            return getPlayerFromGameContext().Game.moves;
        }

        //public void SaveReplay(/*Player p*/)  //after game window 
        //{
        //    //Player p = getPlayerFromGameContext();

        //    databaseHandler.InsertGamePlayed(p);
        //    databaseHandler.InsertPlayers(p.Game.Players);
        //    foreach (var item in p.Game.moves)
        //    {
        //        //databaseHandler.InsertMove(p.Game.GameID,p.UserName,item.Type);
        //        databaseHandler.InsertMove(p.Game.GameID, p.UserName, databaseHandler.FindCard(item.card), item.Type);
        //    }
        //    p.Game.InsertDeckIntoDatabase();
        //}
    }
}
