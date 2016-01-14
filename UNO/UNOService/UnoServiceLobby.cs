using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using UNOService.Game;

namespace UNOService
{
    public partial class UnoService : ILobby
    {
        private List<Party> parties = new List<Party>();

        public List<Player> GetOnlineList()
        {
            ILobbyCallback currentPlayerCallback = OperationContext.Current.GetCallbackChannel<ILobbyCallback>();

            return playersOnline.Where(x => x.ILobbyCallback != currentPlayerCallback).ToList();
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

        public void CreateParty()
        {
            Player host = getPlayerFromLobbyContext();
            Party party = new Party(host);
            parties.Add(party);
            host.Party = party;
        }

        public void LeaveParty()
        {
            Player player = getPlayerFromLobbyContext();

            if (player.Party == null)
            {
                Debug.WriteLine($"{player.UserName} tried to leave a party but was not in a party");
                return;
            }

            player.Party.Players.Remove(player);

            foreach (Player currentPlayer in player.Party.Players)
            {
                currentPlayer.ILobbyCallback.PlayerLeftParty(player);
            }

            if (player == player.Party.Host) // If host leaves, disband the whole party.
            {
                foreach (Player currentPlayer in player.Party.Players)
                {
                    currentPlayer.Party = null;
                }
                parties.Remove(player.Party);
            }

            player.Party = null;

        }

        public void SendInvites(List<string> playerNames)
        {
            Player host = getPlayerFromLobbyContext();

            if (host.Party == null)
            {
                Debug.WriteLine($"{host.UserName} tried to invite players but was not in a party");
                return;
            }

            if (host.Party.Host != host)
            {
                Debug.WriteLine($"{host.UserName} tried to invite players but was not a host");
                return;
            }

            foreach (string username in playerNames)
            {
                Player player;

                if (tryGetPlayerFromUsername(username, out player))
                {
                    if (!host.Party.Players.Contains(player)) // Prevent sending invites to players in the party already
                    {
                        player.ILobbyCallback.ReceiveInvite(host.UserName);
                    }
                }
            }
        }

        /// <summary>
        /// Returns false if party does not exist or is full.
        /// </summary>
        /// <param name="host"></param>
        /// <returns></returns>
        public bool AnswerInvite(string host)
        {
            Player player = getPlayerFromLobbyContext();
            Party partyPlayerWasInvitedTo;

            if (player.Party == null && tryGetPartyFromHost(host, out partyPlayerWasInvitedTo)) // They can only join a party if they are not in one yet.
            {
                if (partyPlayerWasInvitedTo.Players.Count < 4)
                {
                    foreach (Player partyPlayer in partyPlayerWasInvitedTo.Players)
                    {
                        partyPlayer.ILobbyCallback.PlayerAddedToParty(player.UserName);
                    }
                    partyPlayerWasInvitedTo.Players.Add(player);
                    player.Party = partyPlayerWasInvitedTo;
                    return true;
                }
            }

            return false;
        }

        private bool tryGetPartyFromHost(string host, out Party foundParty)
        {
            foreach (Party party in parties)
            {
                if (party.Host.UserName.Equals(host))
                {
                    foundParty = party;
                    return true;
                }
            }

            foundParty = null;
            return false;
        }

        public void StartGame()
        {
            Player host = getPlayerFromLobbyContext();

            if (host.Party == null)
            {
                Debug.WriteLine($"{host.UserName} tried to start a game but was not in a party");
                return;
            }

            if (host != host.Party.Host)
            {
                Debug.WriteLine($"{host.UserName} tried to start a game but was not host");
                return;
            }

            if (host.Party.Players.Count >= 2 && host.Party.Players.Count <= 4)
            {
                gameID++;
                Game.Game Game = new Game.Game(gameID, host.Party.Players,databaseHandler);
                games.Add(Game);
                foreach (Player partyMember in host.Party.Players)
                {
                    partyMember.Game = Game;
                    //Everyone will be notified except host who initated this function
                    if (partyMember != host)
                    {
                        partyMember.ILobbyCallback.NotifyGameStarted();
                    }
                }
            }
            else
            {
                Debug.WriteLine($"{host.UserName} tried to start a game but party did not have enough players: {host.Party.Players.Count}");
                return;
            }

        }

        public void SendMessageParty(string message)
        {
            Player messageSender = getPlayerFromLobbyContext();

            if (messageSender.Party == null)
            {
                Debug.WriteLine($"{messageSender.UserName} tried to send a message but was not in a party");
                return;
            }

            message = $"{messageSender.UserName}: {message}";

            foreach (Player player in messageSender.Party.Players)
            {
                if (player != messageSender)
                {
                    player.ILobbyCallback.SendChatMessageLobbyCallback(message);
                }
            }
        }

        public void SubscribeToLobbyEvents(string username, string password)
        {
            //TODO Validate subscription using password
            ILobbyCallback clientCallbackLobby = OperationContext.Current.GetCallbackChannel<ILobbyCallback>();
            Player player = playersOnline.Find(x => x.UserName == username);
            player.ILobbyCallback = clientCallbackLobby;

            foreach (var onlinePlayer in playersOnline)
            {
                if (onlinePlayer != player)
                    onlinePlayer.ILobbyCallback?.PlayerConnected(player); // Added null check because it could fail if 2 people logged in at the same time
            }
        }

        public List<Player> GetPartyMembers()
        {
            Player player = getPlayerFromLobbyContext();

            if (player.Party == null)
            {
                Debug.WriteLine($"{player.UserName} tried to get party members but was not in a party");
                return new List<Player>();
            }
            return player.Party.Players;
        }

        public void StartTheReplay(int GameID)
        {
            Player player = getPlayerFromLobbyContext();
            List<Player> players = databaseHandler.GetPlayersOfTheGame(GameID);

            //put the player on top of the list 
            foreach (var item in players)
            {
                if(item.UserName == player.UserName)
                {
                    Player i = item;
                    players.Remove(i);
                    players.Insert(0,i);
                }
            }

            Game.Game Game = new Game.Game(gameID, players, databaseHandler);
            Game.Deck = databaseHandler.GetDeck(GameID);

            foreach (var item in players)
            {
                playersInReplay.Add(item);
                item.Game = Game;
            }

            Game.StartGameReplay(player);
            player.ILobbyCallback.NotifyGameStarted();


        }



    }
}