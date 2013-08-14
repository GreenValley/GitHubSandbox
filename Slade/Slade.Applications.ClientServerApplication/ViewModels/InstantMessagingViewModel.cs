using Slade.Applications.ClientServerApplication.Networking;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ServiceModel;
using System.Windows.Input;

namespace Slade.Applications.ClientServerApplication.ViewModels
{
    /// <summary>
    /// Controls the activity of sending and receiving instance messages through the configured connection.
    /// </summary>
    public class InstantMessagingViewModel : ViewModelBase
    {
        private readonly ConnectionManager mConnectionManager;

        private readonly ObservableCollection<CommunicationMessage> mMessages =
            new ObservableCollection<CommunicationMessage>();

        private string mCurrentMessage;

        /// <summary>
        /// Initializes a new instance of the <see cref="InstantMessagingViewModel"/> class.
        /// </summary>
        /// <param name="connectionManager">Manager of the client and server communication channels.</param>
        /// <exception cref="ArgumentNullException">Thrown when the given connection manager is null.</exception>
        public InstantMessagingViewModel(ConnectionManager connectionManager)
        {
            VerificationProvider.VerifyNotNull(connectionManager, "connectionManager");

            ConnectionInformation = connectionManager.ConnectionInformation;

            mConnectionManager = connectionManager;
            mConnectionManager.ChannelClient.StateChanged += ChannelClient_StateChanged;
            mConnectionManager.ChannelService.CommunicationMessageReceived += ChannelService_CommunicationMessageReceived;

            // Note: The command cannot use a command parameter as we are specifically acting upon the CurrentMessage property.
            SendMessageCommand = new DelegateCommand(x => SendMessage(), x => CanSendMessage());
        }

        /// <summary>
        /// Provides access to the command used to send the current message to the recipient.
        /// </summary>
        public ICommand SendMessageCommand { get; private set; }

        /// <summary>
        /// Provides access to the current configurable network connection information.
        /// </summary>
        public ConnectionInformation ConnectionInformation { get; private set; }

        /// <summary>
        /// Provides access to the collection of messages sent to/from the connected user.
        /// </summary>
        public IEnumerable<CommunicationMessage> Messages
        {
            get { return mMessages; }
        }

        /// <summary>
        /// Gets or sets the message from the user ready to send to the recipient.
        /// </summary>
        public string CurrentMessage
        {
            get { return mCurrentMessage; }
            set { SetValue<string>(ref mCurrentMessage, value, "CurrentMessage"); }
        }

        private void SendMessage()
        {
            SendMessage(CurrentMessage);

            // Clear the message so the user can type a new one
            CurrentMessage = String.Empty;
        }

        private void SendMessage(string message)
        {
            // Log the message locally so we can keep track of sent messages
            var communicationMessage = new CommunicationMessage(ConnectionInformation.Username, message, DateTime.Now, isLocalUser: true);
            mMessages.Add(communicationMessage);

            // Transmit the message to the recipient
            mConnectionManager.ChannelClient.Transmit(communicationMessage);
        }

        private bool CanSendMessage()
        {
            return !String.IsNullOrWhiteSpace(CurrentMessage);
        }

        private void ChannelClient_StateChanged(object sender, EventArgs e)
        {
            var clientConnectionState = mConnectionManager.ChannelClient.State;

            if (clientConnectionState == CommunicationState.Opening || clientConnectionState == CommunicationState.Opened)
            {
                // A new connection has just been established with the service, so transmit a greeting message
                SendMessage("Connection Established!");
            }
        }

        private void ChannelService_CommunicationMessageReceived(object sender, CommunicationMessageReceivedEventArgs e)
        {
            // Simply add the received communication message to the observable collection
            mMessages.Add(e.Message);
        }
    }
}