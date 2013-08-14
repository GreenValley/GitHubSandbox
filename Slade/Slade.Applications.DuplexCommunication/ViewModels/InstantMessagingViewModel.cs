using Slade.Applications.DuplexCommunication.Networking;
using System;

namespace Slade.Applications.DuplexCommunication.ViewModels
{
    public class InstantMessagingViewModel : ViewModelBase
    {
        private readonly ConnectionManager mConnectionManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="InstantMessagingViewModel"/> class.
        /// </summary>
        /// <param name="connectionManager">Manager of the client and server communication channels.</param>
        /// <exception cref="ArgumentNullException">Thrown when the given connection manager is null.</exception>
        public InstantMessagingViewModel(ConnectionManager connectionManager)
        {
            VerificationProvider.VerifyNotNull(connectionManager, "connectionManager");

            mConnectionManager = connectionManager;
        }

        /// <summary>
        /// Provides access to the manager of the communication channel connections.
        /// </summary>
        public ConnectionManager ConnectionManager
        {
            get { return mConnectionManager; }
        }
    }
}