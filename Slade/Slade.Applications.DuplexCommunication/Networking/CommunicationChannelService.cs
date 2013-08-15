using System;
using System.ServiceModel;

namespace Slade.Applications.DuplexCommunication.Networking
{
    /// <summary>
    /// Provides a server-side implementation of the <see cref="ICommunicationChannel"/> using a self-hosting service proxy.
    /// </summary>
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class CommunicationChannelService : CommunicationChannelBase<ServiceHost>
    {
        /// <summary>
        /// Raised when a new user connects to the server.
        /// </summary>
        public event EventHandler UserConnected;

        /// <summary>
        /// Raised when a user has been disconnected from the server.
        /// </summary>
        public event EventHandler UserDisconnected;

        /// <summary>
        /// Raised when a communication message is transmitted over the communication channel and received by this service.
        /// </summary>
        public event EventHandler<CommunicationMessageReceivedEventArgs> CommunicationMessageReceived;

        private string mHostingAddress;

        /// <summary>
        /// Provides access to the connected user information.
        /// </summary>
        public UserProfile ConnectedUser { get; private set; }

        /// <summary>
        /// Starts the channel listening on the specified address.
        /// </summary>
        /// <param name="address">The URL address on which to listen for incoming messages.</param>
        /// <exception cref="ArgumentException">Thrown when the given address is not a valid string.</exception>
        public void Start(string address)
        {
            VerificationProvider.VerifyValidString(address, "address");

            // Only reset the connection if the given address differs from the one we are currently using
            if (String.Equals(mHostingAddress, address, StringComparison.InvariantCultureIgnoreCase))
            {
                return;
            }

            Stop();

            // Fire up the service host using ourselves as the hosted service
            mHostingAddress = address;
            CommunicationObject = new ServiceHost(this, new Uri(address));
            CommunicationObject.Open();
        }

        /// <summary>
        /// Stops listening for incoming messages.
        /// </summary>
        public void Stop()
        {
            if (CommunicationObject != null)
            {
                // We can only close the connection if it is opening or already open
                if (State == CommunicationState.Opening || State == CommunicationState.Opened)
                {
                    CommunicationObject.Close();
                }

                CommunicationObject = null;
            }
        }

        public override void Connect(UserProfile profile)
        {
            VerificationProvider.VerifyNotNull(profile, "profile");

            ConnectedUser = profile;

            UserConnected.SafeInvoke(this);
        }

        public override void Disconnect(Guid userId)
        {
            if (ConnectedUser.UserId == userId)
            {
                UserDisconnected.SafeInvoke(this);

                // TODO: We should do this before raising the event and then pass the info through the event args.
                ConnectedUser = null;
            }
        }

        /// <summary>
        /// Transmits the given communication message to the recipient.
        /// </summary>
        /// <param name="message">The communication message to be transmitted to the recipient.</param>
        /// <exception cref="ArgumentNullException">Thrown when the given communication message is null.</exception>
        public override void Transmit(CommunicationMessage message)
        {
            VerificationProvider.VerifyNotNull(message, "message");

            // Make sure we mark the message as being from a remote user
            message.IsLocalUser = false;

            CommunicationMessageReceived.SafeInvoke(this, new CommunicationMessageReceivedEventArgs(message));
        }
    }
}