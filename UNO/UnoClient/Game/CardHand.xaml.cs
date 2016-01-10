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
                var window = new Window();
                window.Closing += Window_Closing;
                window.Width = 330;
                window.Height = 100;
                window.WindowStartupLocation = WindowStartupLocation.CenterScreen;

                var MainstackPanel = new StackPanel { Orientation = Orientation.Vertical };
                var ColorsStackPanel = new StackPanel { Orientation = Orientation.Horizontal };
                var BtnStackPanel = new StackPanel { Orientation = Orientation.Horizontal };

                MainstackPanel.Name = "main";
                ColorsStackPanel.Name = "color";
                BtnStackPanel.Name = "btnn";

                MainstackPanel.Width = 330;
                MainstackPanel.Height = 100;
                
                ColorsStackPanel.Height = 30;
                
                BtnStackPanel.Height = 30;

                MainstackPanel.HorizontalAlignment = HorizontalAlignment.Left;
                MainstackPanel.VerticalAlignment = VerticalAlignment.Top;

                RadioButton Yellow = new RadioButton();
                Yellow.Content = "Yellow";
                Yellow.Name = "yellowRbtn";
                Yellow.IsChecked = true;
                RadioButton Green = new RadioButton();
                Green.Content = "Green";
                Green.Name = "greenRbtn";
                RadioButton Blue = new RadioButton();
                Blue.Content = "Blue";
                Blue.Name = "blueRbtn";
                RadioButton Red = new RadioButton();
                Red.Content = "Red";
                Red.Name = "redRbtn";

                Thickness radioBtnMargin = new Thickness(5,7,5,5);

                Yellow.Margin = radioBtnMargin;
                Green.Margin = radioBtnMargin;
                Red.Margin = radioBtnMargin;
                Blue.Margin = radioBtnMargin;

                ColorsStackPanel.Children.Add(new Label { Content = "Choose a color" });
                ColorsStackPanel.Children.Add(Yellow);
                ColorsStackPanel.Children.Add(Green);
                ColorsStackPanel.Children.Add(Blue);
                ColorsStackPanel.Children.Add(Red);

                Button ChooseBtn = new Button();
                ChooseBtn.Content = "Choose";
                radioBtnMargin = new Thickness(140, 0, 0, 0);
                ChooseBtn.Margin = radioBtnMargin;
                ChooseBtn.Click += ChooseBtn_Click;
                BtnStackPanel.Children.Add(ChooseBtn);

                MainstackPanel.Children.Add(ColorsStackPanel);
                MainstackPanel.Children.Add(BtnStackPanel);

                window.ApplyTemplate();
                MainstackPanel.ApplyTemplate();
                ColorsStackPanel.ApplyTemplate();
                BtnStackPanel.ApplyTemplate();

                window.Content = MainstackPanel;
                window.ShowDialog();
                try
                {
                    String Color = "";
                    foreach (var item in ColorsStackPanel.Children)
                    {
                        if (item.GetType() == typeof(RadioButton))
                            if (((RadioButton)item).IsChecked == true)
                            {
                                Color = ((RadioButton)item).Content.ToString();
                            }
                    }

                    switch (Color)
                    {
                        case "Yellow":
                            cardToBePlayed.Color = CardColor.Yellow;
                            break;
                        case "Green":
                            cardToBePlayed.Color = CardColor.Green;
                            break;
                        case "Red":
                            cardToBePlayed.Color = CardColor.Red;
                            break;
                        case "Blue":
                            cardToBePlayed.Color = CardColor.Blue;
                            break;
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
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

        private void ChooseBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Window parent = (Window)((StackPanel)((StackPanel)((Button)sender).Parent).Parent).Parent;
                parent.Closing -= Window_Closing;
                parent.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
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
