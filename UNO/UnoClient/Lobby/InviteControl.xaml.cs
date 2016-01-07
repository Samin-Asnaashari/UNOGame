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
    /// Interaction logic for InviteControl.xaml
    /// </summary>
    public partial class InviteControl : UserControl
    {
        public delegate void ResponseButtonHandler(bool accept, InviteControl inviteControl);
        public ResponseButtonHandler OnButtonPress;
        public string InviteSenderName { get; private set; }

        public Party partyInQuestion;

        public InviteControl(Party p, ResponseButtonHandler buttonPressDelegate)
        {
            InitializeComponent();

            partyInQuestion = p;
            InviteSenderName = p.Host.UserName;
            OnButtonPress = buttonPressDelegate;
            inviteMessage.Text = $"{InviteSenderName} has invited you to their party";
        }

        private void buttonAccept_Click(object sender, RoutedEventArgs e)
        {
            OnButtonPress?.Invoke(true, this);
        }

        private void buttonDecline_Click(object sender, RoutedEventArgs e)
        {
            OnButtonPress?.Invoke(false, this);
        }
    }
}
