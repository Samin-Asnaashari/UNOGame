﻿using System;
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
        public bool Horizontal { get; set; }

        public CardHand()
        {
            InitializeComponent();
        }

        public void addCard(CardControl c)
        {
            Hand.Children.Add(c);
        }
    }
}
