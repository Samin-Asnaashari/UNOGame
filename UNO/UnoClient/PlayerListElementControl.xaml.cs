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
    /// Interaction logic for PlayerListElementControl.xaml
    /// </summary>
    public partial class PlayerListElementControl : UserControl
    {

        public bool IsChecked
        {
            get { return (bool)checkBox.IsChecked; }
        }

        public PlayerListElementControl(proxy.Player player)
        {
            InitializeComponent();
            checkBox.Content = player.UserName;
            SetState(player.State);
        }

        public PlayerListElementControl(string player)
        {
            InitializeComponent();
            checkBox.Content = player;
        }

        public void SetState(proxy.PlayerState state)
        {
            switch (state)
            {
                case proxy.PlayerState.InGame:
                    setCheckbox(false);
                    break;
                case proxy.PlayerState.InLobby:
                    setCheckbox(true);
                    break;
            }
        }

        private void setCheckbox(bool enabled)
        {
            if (enabled)
            {
                checkBox.IsEnabled = enabled;
                checkBox.Background = Brushes.Green;
            }
            else
            {
                checkBox.IsEnabled = enabled;
                checkBox.Background = Brushes.Yellow;
            }
        }
    }
}
