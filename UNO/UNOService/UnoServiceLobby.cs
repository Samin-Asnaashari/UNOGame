using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using UNOService.Game;

namespace UNOService
{
    public partial class UnoService : ILobby
    {
        private Dictionary<string, Party> parties = new Dictionary<string, Party>();

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
            parties.Add(host.UserName, new Party(host));
        }

        public void LeaveParty(string host)
        {
            Player player = getPlayerFromLobbyContext();
            Party currentParty;

            if (parties.TryGetValue(host, out currentParty))
            {
                currentParty.Players.Remove(player);

                if (player.UserName.CompareTo(host) == 0) // If host leaves, disband the whole party.
                {
                    for (int i = 0; i < currentParty.Players.Count; i++)
                    {
                        currentParty.Players[i].ILobbyCallback.PlayerLeftParty(player);
                    }
                    parties.Remove(host);
                }
                else
                {
                    for (int i = 0; i < currentParty.Players.Count; i++)
                    {
                        currentParty.Players[i].ILobbyCallback.PlayerLeftParty(player);
                    }
                }
            }
        }

        public void SendInvites(List<string> playerNames)
        {
            Player host = getPlayerFromLobbyContext();

            foreach (string username in playerNames)
            {
                Player player;

                if (tryGetPlayerFromUsername(username, out player))
                {
                    player.ILobbyCallback.ReceiveInvite(host.UserName);
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
            Party party;

            if (parties.TryGetValue(host, out party))
            {
                if (party.Players.Count < 4)
                {
                    foreach (Player partyPlayer in party.Players)
                    {
                        partyPlayer.ILobbyCallback.PlayerAddedToParty(player.UserName);
                    }
                    party.Players.Add(player);
                    return true;
                }
            }

            return false;
        }

        public void StartGame(string host)
        {
            //maybe need to check authorized Host and players  
            if (GetPartyMembers(host).Count >= 2 && GetPartyMembers(host).Count <= 4)
            {
                Game.Game Game = new Game.Game(gameID, GetPartyMembers(host));
                gameID++;
                games.Add(Game);
                for (int i = 1; i < GetPartyMembers(host).Count; i++)
                {
                    //everyone will be notified except host
                    GetPartyMembers(host)[i].ILobbyCallback.NotifyGameStarted(host);
                }
                parties.Remove(host);
            }
            else
            {
                throw new Exception("Party needs to be full....");
            }

        }

        public void SendMessageParty(string message, string host)
        {
            Player messageSender = getPlayerFromLobbyContext();

            Party party;

            message = $"{messageSender.UserName}: {message}";

            if (parties.TryGetValue(host, out party))
            {
                foreach (Player player in party.Players)
                {
                    if (player != messageSender)
                    {
                        player.ILobbyCallback.SendChatMessageLobbyCallback(message);
                    }
                }
            }
        }

        public List<Player> GetPartyMembers(string host)
        {
            Party party;

            if (parties.TryGetValue(host, out party))
            {
                return party.Players;
            }

            return new List<Player>();
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

    }
}