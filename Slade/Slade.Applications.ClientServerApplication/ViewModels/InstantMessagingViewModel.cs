using Slade.Applications.ClientServerApplication.Networking;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Slade.Applications.ClientServerApplication.ViewModels
{
    /// <summary>
    /// Controls the activity of sending and receiving instance messages through the configured connection.
    /// </summary>
    public class InstantMessagingViewModel : ViewModelBase
    {
        private readonly ConnectionInformation mConnectionInformation;

        private readonly ObservableCollection<CommunicationMessage> mMessages =
            new ObservableCollection<CommunicationMessage>();

        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionInformation"/> class.
        /// </summary>
        /// <param name="connectionInformation">Information pertaining to the configurable network connection.</param>
        /// <exception cref="ArgumentNullException">Thrown when the given connection information is null.</exception>
        public InstantMessagingViewModel(ConnectionInformation connectionInformation)
        {
            VerificationProvider.VerifyNotNull(connectionInformation, "connectionInformation");

            mConnectionInformation = connectionInformation;

            SendMessageCommand = new DelegateCommand(x => SendMessage((string)x), CanSendMessage);
        }

        /// <summary>
        /// Provides access to the command used to send the current message to the recipient.
        /// </summary>
        public ICommand SendMessageCommand { get; private set; }

        /// <summary>
        /// Provides access to the current configurable network connection information.
        /// </summary>
        public ConnectionInformation ConnectionInformation
        {
            get { return mConnectionInformation; }
        }

        /// <summary>
        /// Provides access to the collection of messages sent to/from the connected user.
        /// </summary>
        public IEnumerable<CommunicationMessage> Messages
        {
            get { return mMessages; }
        }

        private void SendMessage(string message)
        {
            // Log the message locally so we can keep track of sent messages
            var communicationMessage = new CommunicationMessage(ConnectionInformation.Username, message, DateTime.Now);
            mMessages.Add(communicationMessage);

            // TODO: Transmit the message to the recipient.
        }

        private static bool CanSendMessage(object parameter)
        {
            return !String.IsNullOrWhiteSpace(parameter as string);
        }
    }
}