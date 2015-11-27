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
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public delegate void SwitchWindowHandler(WindowType type, String username);

    public partial class MainWindow : Window
    {
        GameWindow game;
        LobbyWindow lobby;
        LoginAndRegisterWindow login;

        public MainWindow()
        {
            InitializeComponent();
            login = new LoginAndRegisterWindow(switchWindow);
            lobby = new LobbyWindow(switchWindow, "");
            game = new GameWindow(switchWindow);
            contentControl.Content = login;
        }

        private void switchWindow(WindowType type, String username)
        {
            UserControl controlToShow = null;
            switch (type)
            {
                case WindowType.Game: controlToShow = game;
                    break;
                case WindowType.Lobby:
                    {
                        controlToShow = new LobbyWindow(switchWindow, username);
                    }
                    break;
                case WindowType.Login: controlToShow = login;
                    break;
                default: MessageBox.Show("This window is not implemented yet");
                    break;
            }

            contentControl.Content = controlToShow;
        }

        private void GameButton_Click(object sender, RoutedEventArgs e)
        {
            switchWindow(WindowType.Game, "");
        }

        private void LobbyButton_Click(object sender, RoutedEventArgs e)
        {
            switchWindow(WindowType.Lobby, "");
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            switchWindow(WindowType.Lobby, "");
        }
    }
}
