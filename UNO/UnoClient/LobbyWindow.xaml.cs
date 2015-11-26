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
    public partial class LobbyWindow : UserControl, proxy.ILobbyCallback
    {
        public SwitchWindowHandler OnSwitchWindow;
        private LobbyClient Lobby;
        private InstanceContext context;

        public LobbyWindow(SwitchWindowHandler switchWindowCallback)
        {
            OnSwitchWindow += switchWindowCallback;
            InitializeComponent();

            // subscribeEventsTolobby must be done somehow
            context = new InstanceContext(this);
            Lobby = new LobbyClient(context);
            foreach (var item in Lobby.GetOnlineList())
            {
                //listBoxOnlinePlayers.Items.Add(item.UserName + "--- " + item.State);add to wpf listbox
            }
            
        }

        private void switchWindow(WindowType type)// each time window is switch to lobby getonlinelist, subscribeToLobbyEvents, etc must be done
        {
            OnSwitchWindow(type);
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
            throw new NotImplementedException();
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
