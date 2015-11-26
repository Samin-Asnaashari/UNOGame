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

namespace UnoClient
{
    /// <summary>
    /// Interaction logic for LobbyControl.xaml
    /// </summary>
    public partial class LobbyWindow : UserControl, proxy.ILobbyCallback
    {
        public SwitchWindowHandler OnSwitchWindow;

        public LobbyWindow(SwitchWindowHandler switchWindowCallback)
        {
            OnSwitchWindow += switchWindowCallback;
            InitializeComponent();
        }

        private void switchWindow(WindowType type)
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
