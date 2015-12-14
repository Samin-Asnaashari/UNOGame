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

namespace UnoClient
{
    /// <summary>
    /// Interaction logic for CardControl.xaml
    /// </summary>
    public partial class CardControl : UserControl
    {
        public delegate void PlayCardHandler(proxy.Card card, UserControl CardControl);
        public PlayCardHandler OnButtonPress;
        public proxy.Card Card { get; set; }

        public CardControl(proxy.Card C,PlayCardHandler buttonPressDelegate)
        {
            InitializeComponent();
            OnButtonPress = buttonPressDelegate;
            Card = C;
        }

        private void PlayerCard_Click(object sender, RoutedEventArgs e)
        {
            OnButtonPress?.Invoke(Card,this);
        }

        public void DrawCardBackground(proxy.Card c)
        {
            var brush = new ImageBrush();
            if (c.Type == proxy.CardType.Normal)
            {
                brush.ImageSource = new BitmapImage(new Uri("Images/NumberedCard/"+c.Color+"_"+c.Number+".png"));
            }
            else if (c.Type == proxy.CardType.Skip)
            {
                brush.ImageSource = new BitmapImage(new Uri("Images/SkipCard/" + c.Color + "_Skip.png"));
            }
            else if(c.Type == proxy.CardType.Reverse)
            {
                brush.ImageSource = new BitmapImage(new Uri("Images/ReverseCard/" + c.Color + "_Reverse.png"));
            }
            else if(c.Type == proxy.CardType.Draw2)
            {
                brush.ImageSource = new BitmapImage(new Uri("Images/Draw2Card/" + c.Color + "_Draw2.png"));
            }
            else if(c.Type == proxy.CardType.Draw4Wild || c.Type == proxy.CardType.Wild)
            {
                brush.ImageSource = new BitmapImage(new Uri("Images/NumberedCard/" + c.Type + ".png"));
            }
            PlayerCard.Background = brush;
        }
    }
}
