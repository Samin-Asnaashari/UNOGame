using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
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
    public partial class GameWindow : proxy.IGameCallback
    {
        private GameClient GameProxy;
        private string username;

        // TODO Authenticate using password
        public GameWindow(string username)
        {
            this.username = username;
            InitializeComponent();
            GameProxy = new GameClient(new InstanceContext(this));
            GameProxy.SubscribeToGameEvents(username);

            //position the players and cards 

            //foreach (var item in ?username)
            //{
            //    var cardControl=(CardControl)item;
            //    PlayerCards.Children.Add(cardControl);
            //}
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
            ChatrichTextBox.AppendText(message);
        }

        public void TurnChanged(Player player)
        {
            throw new NotImplementedException();
        }

        public void NotifyOpponentsOfPlayerPunished(string userName)
        {
            throw new NotImplementedException();
        }
    }
}
