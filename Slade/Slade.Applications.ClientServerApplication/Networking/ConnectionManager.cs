using System;

namespace Slade.Applications.ClientServerApplication.Networking
{
    /// <summary>
    /// Maintains and manages the client and server components of the communication system.
    /// </summary>
    public sealed class ConnectionManager : DisposableObject
    {
        private readonly CommunicationChannelClient mChannelClient = new CommunicationChannelClient();
        private readonly CommunicationChannelService mChannelService = new CommunicationChannelService();

        private readonly ConnectionInformation mConnectionInformation;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionManager"/> class.
        /// </summary>
        /// <param name="connectionInformation">Information pertaining to the communication channels.</param>
        /// <exception cref="ArgumentNullException">Thrown when the given connection information is null.</exception>
        public ConnectionManager(ConnectionInformation connectionInformation)
        {
            VerificationProvider.VerifyNotNull(connectionInformation, "connectionInformation");

            mConnectionInformation = connectionInformation;
        }

        /// <summary>
        /// Provides access to the channel connected to the recipient service.
        /// </summary>
        public CommunicationChannelClient ChannelClient
        {
            get { return mChannelClient; }
        }

        /// <summary>
        /// Provides access to the channel to the hosted communication service.
        /// </summary>
        public CommunicationChannelService ChannelService
        {
            get { return mChannelService; }
        }

        /// <summary>
        /// Provides access to the information pertaining to the communication channels.
        /// </summary>
        public ConnectionInformation ConnectionInformation
        {
            get { return mConnectionInformation; }
        }

        /// <summary>
        /// Refreshes the hosted and client service connections using the latest connection information.
        /// </summary>
        public void RefreshConnections()
        {
            ChannelService.OpenConnection(ConnectionInformation.HostingAddress);
            ChannelClient.OpenConnection(ConnectionInformation.RecipientAddress);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <param name="disposing">True if the object's Dispose method has been explicitly called; false if the call
        /// has come through the object's finalizer.</param>
        protected override void Dispose(bool disposing)
        {
            // Close both the client and server connections
            ChannelService.CloseConnection();
            ChannelClient.CloseConnection();

            base.Dispose(disposing);
        }
    }
}