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

namespace UnoClient.Game
{
    /// <summary>
    /// Interaction logic for GameWindow.xaml
    /// </summary>
    public partial class GameWindow : proxy.IGameCallback
    {
        private GameClient GameProxy;

        private string username;

        // Game ID should only be known serverside. The player has a game variable so the game id can be found from the context.
        private int GameID;

        // TODO Authenticate using password
        public GameWindow(string username, string password)
        {
            this.username = username;

            GameProxy = new GameClient(new InstanceContext(this));
            GameProxy.SubscribeToGameEvents(username, GameID);

            InitializeComponent();
            //TODO: position the players
        }

        public void CardsAssigned(Card[] cards)
        {
            foreach (Card c in cards)
            {
                player1Hand.addCard(new CardControl(c)); //Add cards to your own hand
            }
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

        public void NotifyOpponentsOfPlayerPunished(string userName)
        {
            throw new NotImplementedException();
        }

        private async void DeckOfCards_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Card takenCard = await GameProxy.takeCardAsync(GameID);
            player1Hand.addCard(new CardControl(takenCard));
        }

        public void changePlayedCard(CardControl cardControl)
        {
            lastPlayedCard = cardControl;
            lastPlayedCard.UpdateLayout();
            Card card = cardControl.getCard();

            GameProxy.playCardAsync(GameID, card);
        }

        public void CardPlayed(Card c)
        {
            lastPlayedCard = new CardControl(c);
        }
    }
}
