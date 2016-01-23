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
    /// Interaction logic for ColorPickerWindow.xaml
    /// </summary>
    public partial class ColorPickerWindow : Window
    {
        public Brush selectedColor;
        private bool picked = false;

        public ColorPickerWindow()
        {
            selectedColor = Brushes.Red;

            InitializeComponent();

            foreach (Rectangle rect in colors.Children)
                rect.MouseLeftButtonUp += Color_MouseLeftButtonUp;
        }

        private void Color_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            foreach (Rectangle rect in colors.Children)
                rect.StrokeThickness = 1;

            Rectangle color = (Rectangle)sender;
            color.StrokeThickness = 5;

            selectedColor = color.Fill;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            picked = true;
            this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!picked) //If a person is trying to close the window without having pressed the 'Pick' button, the window won't close.
                e.Cancel = true;
        }
    }
}