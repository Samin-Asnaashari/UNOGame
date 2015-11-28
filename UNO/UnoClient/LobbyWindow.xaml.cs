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
    public partial class LobbyWindow : UserControl, ILobbyCallback
    {
        public SwitchWindowHandler OnSwitchWindow;
        private LobbyClient Lobby;
        private InstanceContext context;

        public LobbyWindow(SwitchWindowHandler switchWindowCallback, String username)// each time window is switch to lobby getonlinelist, subscribeToLobbyEvents, etc must be done
        {
            OnSwitchWindow += switchWindowCallback;
            InitializeComponent();

            for (int i = 0; i < 50; i++)
            {
                listOnlinePlayers.Children.Add(new PlayerListElementControl(i.ToString()));
            }

            if (username != "")
            {
                context = new InstanceContext(this);
                Lobby = new LobbyClient(context);
                Lobby.SubScribeToLobbyEvents(username); // subscribeEventsTolobby
                
                foreach (var item in Lobby.GetOnlineList())
                {
                    listOnlinePlayers.Children.Add(new PlayerListElementControl(item));
                }
            }
        }

        private void switchWindow(WindowType type, String username)
        {
            OnSwitchWindow(type, username);
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
            throw new NotImplementedException();
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
