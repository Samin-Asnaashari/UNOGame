using System;
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

<<<<<<< HEAD
        public LobbyWindow(String username)
=======
        public LobbyWindow(SwitchWindowHandler switchWindowCallback, string username)// each time window is switch to lobby getonlinelist, subscribeToLobbyEvents, etc must be done
>>>>>>> 778d0e9686dc41404219c942435fda789d4b85e0
        {
            InitializeComponent();

            for (int i = 0; i < 50; i++)
            {
                listOnlinePlayers.Children.Add(new PlayerListElementControl(i.ToString()));
            }

            if (username != "")
            {
                Lobby = new LobbyClient(new InstanceContext(this));
                Lobby.SubScribeToLobbyEvents(username); // subscribeEventsTolobby
                
                foreach (var item in Lobby.GetOnlineList())
                {
                    listOnlinePlayers.Children.Add(new PlayerListElementControl(item));
                }
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
            foreach(UIElement playerControl in listOnlinePlayers.Children)
            {
                var selectedPlayer = ((PlayerListElementControl)playerControl).Player;

                if (selectedPlayer.UserName == player.UserName)
                {
                    listOnlinePlayers.Children.Remove(playerControl);
                }
            }
        }

        public void SendChatMessageLobbyCallback(string message)
        {
            throw new NotImplementedException();
        }

        public void SentInvite(string hostName)
        {
            throw new NotImplementedException();
        }

    }
}
