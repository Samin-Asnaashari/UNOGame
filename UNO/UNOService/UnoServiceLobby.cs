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

        public Player getPlayerFromLobbyContext()
        {
            ILobbyCallback currentPlayerCallback = OperationContext.Current.GetCallbackChannel<ILobbyCallback>();

            return playersOnline.Find(x => x.ILobbyCallback == currentPlayerCallback);
        }

        public Party CreateParty()
        {
            Player host = getPlayerFromLobbyContext();
            Party party = new Party(host);
            parties.Add(host.UserName, party);
            host.IsInParty = true;

            return party;
        }

        public void LeaveParty(Party p)
        {
            Player player = getPlayerFromLobbyContext();

            p.Players.Remove(player);
            player.IsInParty = false;

            foreach (Player currentPlayer in p.Players)
            {
                if (player.UserName != currentPlayer.UserName) //To prevent deadlock
                    currentPlayer.ILobbyCallback.PlayerLeftParty(player);
            }

            if (player.UserName == p.Host.UserName) // If host leaves, disband the whole party.
            {
                foreach (Player currentPlayer in p.Players)
                {
                    currentPlayer.IsInParty = false;
                }

                parties.Remove(p.Host.UserName);
            }
        }

        public void SendInvites(Party p, List<string> playerNames)
        {
            foreach (string username in playerNames)
            {
                Player player;

                if (tryGetPlayerFromUsername(username, out player))
                {
                    if (!p.Players.Contains(player)) // Prevent sending invites to players in the party already
                    {
                        player.ILobbyCallback.ReceiveInvite(p);
                    }
                }
            }
        }

        /// <summary>
        /// Returns false if party does not exist or is full.
        /// </summary>
        /// <param name="host"></param>
        /// <returns></returns>
        public bool AnswerInvite(Party p)
        {
            Player player = getPlayerFromLobbyContext();
            Party partyPlayerWasInvitedTo = p; //TODO: Fixme

            if (player.IsInParty == false && parties.TryGetValue(partyPlayerWasInvitedTo.Host.UserName, out partyPlayerWasInvitedTo)) // They can only join a party if they are not in one yet.
            {
                if (partyPlayerWasInvitedTo.Players.Count < 4)
                {
                    foreach (Player partyPlayer in partyPlayerWasInvitedTo.Players)
                    {
                        partyPlayer.ILobbyCallback.PlayerAddedToParty(player.UserName);
                    }
                    partyPlayerWasInvitedTo.Players.Add(player);
                    player.IsInParty = true;
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

        public void SendMessageParty(string message, Party p)
        {
            Player messageSender = getPlayerFromLobbyContext();

            message = $"{messageSender.UserName}: {message}";

            foreach (Player player in p.Players)
            {
                if (player.UserName != messageSender.UserName) //To prevent deadlock
                {
                    player.ILobbyCallback.SendChatMessageLobbyCallback(message);
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