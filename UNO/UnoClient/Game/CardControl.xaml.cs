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
    /// Interaction logic for CardControl.xaml
    /// </summary>
    public partial class CardControl : UserControl
    {
        private Card card;
        private string picturePath;

        public CardControl()  //Unkown type of card, so 'closed' card
        {
            InitializeComponent();
        }

        public CardControl(Card c)
        {
            card = c;
            picturePath = "/Images/";
            
            switch(card.Type)
            {
                case CardType.normal:
                    picturePath += "Numbered_" + card.Color.ToString() + "/";
                    picturePath += card.Color.ToString() + "_" + card.Number + ".png";
                    break;

                case CardType.wild:
                    picturePath += "wild/Wild.png";
                    break;

                case CardType.draw4Wild:
                    picturePath += "wild/Draw4Wild.png";
                    break;

                default:
                    picturePath += card.Type.ToString() + "/";
                    picturePath += card.Color.ToString() + ".png";
                        break;
            }

            Uri uriSource = new Uri(@"/UnoClient;component" + picturePath, UriKind.Relative);

            InitializeComponent();
            image.Source = new BitmapImage(uriSource);
        }

        public Card getCard()
        {
            return card;
        }
    }
}
