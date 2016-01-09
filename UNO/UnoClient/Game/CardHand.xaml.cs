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

namespace UnoClient.Game
{
    /// <summary>
    /// Interaction logic for CardHand.xaml
    /// </summary>
    public partial class CardHand : UserControl
    {
        public bool IsHorizontal { get; set; }
        public string Username { get; set; }

        public CardHand()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        public void addCard(CardControl c)
        {
            c.AddHandler(MouseUpEvent, new RoutedEventHandler(CardClicked));
            Hand.Children.Insert(0, c);
        }

        private void CardClicked(object sender, RoutedEventArgs e)
        {
            CardControl cardControl = ((CardControl)sender);
            GameWindow parent = ((GameWindow)Window.GetWindow(this));
            bool playSucces = parent.GameProxy.playCard(/*parent.GameID, */cardControl.getCard());
            if (playSucces)
            {
                parent.CardPlayed(cardControl.getCard(), "PlayerWhoPLayed");
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
