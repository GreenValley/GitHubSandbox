using Slade.Applications.DuplexCommunication.Networking;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Slade.Applications.DuplexCommunication.ViewModels
{
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

            mConnectionManager = connectionManager;

            SendMessageCommand = new DelegateCommand(x => SendMessage(), x => CanSendMessage());
        }

        /// <summary>
        /// Provides access to the command used to send the value of the <see cref="CurrentMessage"/> property across the communication channel.
        /// </summary>
        public ICommand SendMessageCommand { get; private set; }

        /// <summary>
        /// Provides access to the manager of the communication channel connections.
        /// </summary>
        public ConnectionManager ConnectionManager
        {
            get { return mConnectionManager; }
        }

        /// <summary>
        /// Provides access to the collection of messages sent and received over the communication channel.
        /// </summary>
        public IEnumerable<CommunicationMessage> Messages
        {
            get { return mMessages; }
        }

        /// <summary>
        /// Gets or sets the message currently being edited by the local user.
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
            var communicationMessage = new CommunicationMessage(ConnectionManager.ConnectionInformation.Username, message, DateTime.Now, isLocalUser: true);
            mMessages.Add(communicationMessage);

            // TODO: Transmit the message to the recipient
        }

        private bool CanSendMessage()
        {
            return !String.IsNullOrWhiteSpace(CurrentMessage);
        }
    }
}