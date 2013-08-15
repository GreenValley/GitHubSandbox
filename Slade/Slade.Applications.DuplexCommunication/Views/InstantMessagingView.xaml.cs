using System.Windows.Input;

namespace Slade.Applications.DuplexCommunication.Views
{
    public partial class InstantMessagingView
    {
        public InstantMessagingView()
        {
            InitializeComponent();
        }

        private void CurrentMessage_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                SendMessageButton.Command.SafeExecute(SendMessageButton.CommandParameter);

                e.Handled = true;
            }
        }
    }
}