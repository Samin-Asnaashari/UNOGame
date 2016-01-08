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
        public GameClient GameProxy;

        private string username;
        public int GameID;

        // TODO Authenticate using password
        public GameWindow(string username, string password)
        {
            this.username = username;
            this.GameID = 1;

            GameProxy = new GameClient(new InstanceContext(this));
            GameProxy.SubscribeToGameEvents(username, GameID);

            InitializeComponent();
            this.Title = "Uno Game: " + username;
            player1Hand.Username = username;
            //TODO: position the players
        }

        public void CardsAssigned(Card[] cards)
        {
            foreach(Card c in cards)
            {
                player1Hand.addCard(new CardControl(c)); //Add cards to your own hand
            }

            for (int i = 0; i < 7; i++)
                player2Hand.addCard(new CardControl());
        }

        public void NotifyPlayerLeft(string userName)
        {
            throw new NotImplementedException();
        }

        public void SendMessageGameCallback(string message)
        {
            chat.Items.Add(message);
            chat.ScrollIntoView(chat.Items[chat.Items.Count - 1]); //Scroll to bottom
        }

        public void TurnChanged(Player player)
        {
            
        }

        public void NotifyOpponentsOfPlayerPunished(string userName)
        {
            throw new NotImplementedException();
        }

        private async void DeckOfCards_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Card takenCard = await GameProxy.takeCardAsync();
            player1Hand.addCard(new CardControl(takenCard));
        }

        public void CardPlayed(Card c)
        {
            lastPlayedCard.Content = new CardControl(c);
        }

        private void buttonSendMessage_Click(object sender, RoutedEventArgs e)
        {
            chat.Items.Add($"{username}: {chatMessage.Text}");
            GameProxy.SendMessageGame(chatMessage.Text);

            chatMessage.Text = "";
        }
    }
}
