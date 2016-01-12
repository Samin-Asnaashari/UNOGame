using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
    /// Interaction logic for CardHand.xaml
    /// </summary>
    public partial class CardHand : UserControl
    {
        public bool IsHorizontal { get; set; }
        public string Username { get; private set; }
        bool active; // If cards should be clickable for the user

        public void Instantiate(string userName, bool active = false)
        {
            this.Username = userName;
            this.active = active;

            if (!active)
            {
                AddFakeCards(7);
            }
        }

        public CardHand()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        public void AddFakeCards(int nrOfCards)
        {
            for (int i = 0; i < nrOfCards; i++)
            {
                AddCard(new CardControl());
            }
        }

        public void AddCard(CardControl c)
        {
            if (active)
            {
                c.AddHandler(MouseUpEvent, new RoutedEventHandler(cardClicked));
            }
            Hand.Children.Insert(0, c);
        }

        private void cardClicked(object sender, RoutedEventArgs e)
        {
            CardControl cardControl = ((CardControl)sender);
            GameWindow parent = ((GameWindow)Window.GetWindow(this));
            Card cardToBePlayed = cardControl.GetCard();

            if (cardToBePlayed.Type == CardType.draw4Wild || cardToBePlayed.Type == CardType.wild)
            {
                ColorPickerWindow colorPicker = new ColorPickerWindow();
                colorPicker.Owner = parent;
                colorPicker.ShowDialog();

                cardToBePlayed.Color = colorPicker.SelectedColor;
            }

            bool playSucces = parent.GameProxy.TryPlayCard(cardToBePlayed);//if special card, color chosen is saved in color attribute to be handled in the server

            if (playSucces)
            {
                parent.CardPlayed(cardControl.GetCard(), "PlayerWhoPLayed");
                Hand.Children.Remove(cardControl);
            }
            else
                MessageBox.Show("Invalid move");

        }

        private void sv_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (IsHorizontal)
            {
                if (e.Delta > 0)
                    sv.LineLeft();
                else
                    sv.LineRight();
            }
            else
            {
                if (e.Delta > 0)
                    sv.LineUp();
                else
                    sv.LineDown();
            }

            e.Handled = true;
        }
    }
}
