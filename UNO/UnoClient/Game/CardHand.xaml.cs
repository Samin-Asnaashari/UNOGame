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
            Card cardToBePlayed = cardControl.getCard();

            if(cardToBePlayed.Type == CardType.draw4Wild || cardToBePlayed.Type == CardType.wild)
            {
                ColorPickerWindow colorPicker = new ColorPickerWindow();
                colorPicker.ShowDialog();

                Brush selectedColor = colorPicker.selectedColor;

                if (selectedColor.Equals(Brushes.Red))
                    cardToBePlayed.Color = CardColor.Red;
                else if (selectedColor.Equals(Brushes.Green))
                    cardToBePlayed.Color = CardColor.Green;
                else if (selectedColor.Equals(Brushes.Blue))
                    cardToBePlayed.Color = CardColor.Blue;
                else if (selectedColor.Equals(Brushes.Yellow))
                    cardToBePlayed.Color = CardColor.Yellow;
            }

            bool playSucces = parent.GameProxy.playCard(/*parent.GameID, */cardToBePlayed);//if special card, color chosen is saved in color attribute to be handled in the server

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
