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

        /// <summary>
        /// Party may not exist anymore, so use this method for safety
        /// </summary>
        /// <param name="username"></param>
        /// <param name="party"></param>
        /// <returns></returns>
        private bool tryGetPartyFromUsername(string username, out Party party)
        {
            party = parties.FirstOrDefault(x => x.Host.UserName == username);

            return party != null;
        }

        public void CreateParty()
        {
            Player host = getPlayerFromLobbyContext();
            parties.Add(new Party(host));
        }

        public void LeaveParty(string partyID)
        {
            Player WhoWantsToLeaveTheParty = getPlayerFromLobbyContext();
            Party currentParty = parties.Find(x => x.PartyID.CompareTo(partyID) == 0);

            currentParty.Players.Remove(WhoWantsToLeaveTheParty);

            if (WhoWantsToLeaveTheParty.UserName.CompareTo(partyID) == 0)
            {
                for (int i = 0; i < currentParty.Players.Count; i++)
                {
                    currentParty.Players[i].ILobbyCallback.PlayerLeftParty(WhoWantsToLeaveTheParty);
                }
                parties.Remove(currentParty);
            }
            else
            {
                for (int i = 0; i < currentParty.Players.Count; i++)
                {
                    currentParty.Players[i].ILobbyCallback.PlayerLeftParty(WhoWantsToLeaveTheParty);
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

        public bool AnswerInvite(bool answer, string partyID)
        {
            Player player = getPlayerFromLobbyContext();
            if (answer)
            {
                Party party;

                if (tryGetPartyFromUsername(partyID, out party))
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

        public void StartGame(string partyID)
        {
            //maybe need to check authorized Host and players  
            if (GetPartyMembers(partyID).Count == 4)
            {
                Game.Game Game = new Game.Game(gameID, GetPartyMembers(partyID));
                gameID++;
                games.Add(Game);
                for (int i = 1; i < GetPartyMembers(partyID).Count; i++)
                {
                    //everyone will be notified except host
                    GetPartyMembers(partyID)[i].ILobbyCallback.NotifyGameStarted(partyID);
                }
                parties.Remove(parties.Find(x => x.PartyID.CompareTo(partyID) == 0));
            }
            else
            {
                throw new Exception("Party needs to be full....");
            }

        }

        public void SendMessageParty(string message, string partyID)
        {
            Player messageSender = getPlayerFromLobbyContext();

            Party party;

            message = $"{messageSender.UserName}: {message}";

            if (tryGetPartyFromUsername(partyID, out party))
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

        public List<Player> GetPartyMembers(string partyID)
        {
            Party party;

            if (tryGetPartyFromUsername(partyID, out party))
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