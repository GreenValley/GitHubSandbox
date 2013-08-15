using System;
using System.ServiceModel;

namespace Slade.Applications.ClientServerApplication.Networking
{
    /// <summary>
    /// Provides a server-side implementation of the <see cref="ICommunicationChannel"/> using a self-hosting service proxy.
    /// </summary>
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class CommunicationChannelService : CommunicationChannelBase<ServiceHost>
    {
        /// <summary>
        /// Raised when a communication message is transmitted over the communication channel and received by this service.
        /// </summary>
        public event EventHandler<CommunicationMessageReceivedEventArgs> CommunicationMessageReceived;

        private string mHostingAddress;

        /// <summary>
        /// Opens a connection to listen on the specified address.
        /// </summary>
        /// <param name="address">The URL address on which to listen for incoming messages.</param>
        /// <exception cref="ArgumentException">Thrown when the given address is not a valid string.</exception>
        public override void OpenConnection(string address)
        {
            VerificationProvider.VerifyValidString(address, "address");

            // Only reset the connection if the given address differs from the one we are currently using
            if (String.Equals(mHostingAddress, address, StringComparison.InvariantCultureIgnoreCase))
            {
                return;
            }

            CloseConnection();

            // Fire up the service host using ourselves as the hosted service
            mHostingAddress = address;
            CommunicationObject = new ServiceHost(this, new Uri(address));
            CommunicationObject.Open();
        }

        /// <summary>
        /// Closes down any existing connections and stops listening for incoming messages.
        /// </summary>
        public override void CloseConnection()
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