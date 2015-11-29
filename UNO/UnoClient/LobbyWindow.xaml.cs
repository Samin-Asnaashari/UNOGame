﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using UnoClient.proxy;
using System.ServiceModel;

namespace UnoClient
{
    /// <summary>
    /// Interaction logic for LobbyControl.xaml
    /// </summary>
    public partial class LobbyWindow : ILobbyCallback
    {
        private LobbyClient Lobby;
        PartyControl party;

        public LobbyWindow(String username)
        {
            InitializeComponent();

            Lobby = new LobbyClient(new InstanceContext(this));
            Lobby.SubScribeToLobbyEvents(username); // subscribeEventsTolobby
            foreach (var item in Lobby.GetOnlineList())
            {
                listOnlinePlayers.Children.Add(new PlayerListElementControl(item));
            }
        }

        public void NotifyGameStarted(Player[] players)
        {
            throw new NotImplementedException();
        }

        public void PartyIsFull()
        {
            throw new NotImplementedException();
        }

        public void PlayerAddedToParty(string playerName)
        {
            throw new NotImplementedException();
        }

        public void PlayerConnected(Player player)
        {
            listOnlinePlayers.Children.Add(new PlayerListElementControl(player));
        }

        public void PlayerDisConnected(Player player)
        {
            foreach (UIElement playerControl in listOnlinePlayers.Children)
            {
                var selectedPlayer = ((PlayerListElementControl)playerControl).Player;

                if (selectedPlayer.UserName == player.UserName)
                {
                    listOnlinePlayers.Children.Remove(playerControl);
                }
            }

            party?.RemovePlayer(player.UserName);
        }

        public void SendChatMessageLobbyCallback(string message)
        {
            throw new NotImplementedException();
        }

        public void SentInvite(string hostName)
        {
            if (MessageBox.Show($"{hostName} has invited you to a game", "Game Invite", MessageBoxButton.YesNoCancel) == MessageBoxResult.Yes)
            {
                // If the players accepts an invitation to another players party, 
                // they do not have invite rights. (Inviting causes a new party to be created)
                //party = new PartyControl(hostName);
                partyGrid.Children.Add(party);
                inviteButton.IsEnabled = false;
            }
        }

        private void inviteButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
