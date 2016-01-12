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
using System.Windows.Shapes;

namespace UnoClient.Game
{
    /// <summary>
    /// Interaction logic for DrawCardChoiceWindow.xaml
    /// </summary>
    public partial class DrawCardChoiceWindow : Window
    {
        public enum UserChoice
        {
            Play,
            Keep
        }

        public UserChoice Action;

        public DrawCardChoiceWindow(proxy.Card card)
        {
            InitializeComponent();
            cardPreview.Content = new CardControl(card);
        }

        private void Play_Click(object sender, RoutedEventArgs e)
        {
            Action = UserChoice.Play;
            this.Close();
        }

        private void Keep_Click(object sender, RoutedEventArgs e)
        {
            Action = UserChoice.Keep;
            this.Close();
        }
    }
}
