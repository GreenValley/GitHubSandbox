using System;

namespace Slade.Applications.DuplexCommunication.Networking
{
    /// <summary>
    /// Maintains and manages the client and server components of the communication system.
    /// </summary>
    public sealed class ConnectionManager : NotificationObject
    {
        private readonly ConnectionInformation mConnectionInformation;

        private readonly CommunicationChannelClient mChannelClient = new CommunicationChannelClient();
        private readonly CommunicationChannelService mChannelService = new CommunicationChannelService();

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
        /// Provides access to the information pertaining to the communication channels.
        /// </summary>
        public ConnectionInformation ConnectionInformation
        {
            get { return mConnectionInformation; }
        }

        /// <summary>
        /// Provides access to the communication channel client.
        /// </summary>
        public CommunicationChannelClient ChannelClient
        {
            get { return mChannelClient; }
        }

        /// <summary>
        /// Provides access to the hostable service for the communication channel.
        /// </summary>
        public CommunicationChannelService ChannelService
        {
            get { return mChannelService; }
        }
    }
}