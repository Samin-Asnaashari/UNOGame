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
    /// Interaction logic for GameWindow.xaml
    /// </summary>
    public partial class GameWindow : UserControl, proxy.IGameCallback
    {
        public SwitchWindowHandler OnSwitchWindow;

        public GameWindow(SwitchWindowHandler switchWindowCallback)
        {
            OnSwitchWindow += switchWindowCallback;
            InitializeComponent();
        }

        private void switchWindow(WindowType type, String username)
        {
            OnSwitchWindow(type, username);
        }

        public void CardsAssigned(Card[] cards)
        {
            throw new NotImplementedException();
        }

        public void NotifyPlayerLeft(string userName)
        {
            throw new NotImplementedException();
        }

        public void SendMessageGameCallback(string message)
        {
            throw new NotImplementedException();
        }

        public void TurnChanged(Player player)
        {
            throw new NotImplementedException();
        }
    }
}
