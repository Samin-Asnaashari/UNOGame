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
        private UIElementCollection cards { get { return Hand.Children; } }


        public delegate void CardClickedHandler(CardControl cardControl);
        public CardClickedHandler OnCardClicked;

        public void Instantiate(string userName, CardClickedHandler onCardClicked = null)
        {
            this.Username = userName;

            // This is the clients control
            if (onCardClicked != null)
            {
                OnCardClicked = onCardClicked;
            }

        }

        public CardHand()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        public void AddPlaceHolderCards(int nrOfCards)
        {
            for (int i = 0; i < nrOfCards; i++)
            {
                AddCard(new CardControl());
            }
        }

        public void AddCard(CardControl c)
        {
            if (OnCardClicked != null)
            {
                c.AddHandler(MouseUpEvent, new RoutedEventHandler(cardClicked));
            }

            cards.Insert(0, c);
        }

        public void RemoveCard(CardControl cardControl)
        {
            cards.Remove(cardControl);
        }


        private void cardClicked(object sender, RoutedEventArgs e)
        {
            CardControl cardControl = ((CardControl)sender);
            GameWindow parent = ((GameWindow)Window.GetWindow(this));

            OnCardClicked(cardControl);
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
