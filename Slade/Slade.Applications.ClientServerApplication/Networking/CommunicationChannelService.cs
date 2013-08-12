using System;
using System.ServiceModel;

namespace Slade.Applications.ClientServerApplication.Networking
{
    /// <summary>
    /// Provides a server-side implementation of the <see cref="ICommunicationChannel"/> using a self-hosting service proxy.
    /// </summary>
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class CommunicationChannelService : ICommunicationChannel
    {
        /// <summary>
        /// Raised when a communication message is transmitted over the communication channel and received by this service.
        /// </summary>
        public event EventHandler<CommunicationMessageReceivedEventArgs> CommunicationMessageReceived;

        private ServiceHost mService;

        /// <summary>
        /// Opens a connection to listen on the specified address.
        /// </summary>
        /// <param name="address">The URL address on which to listen for incoming messages.</param>
        /// <exception cref="ArgumentException">Thrown when the given address is not a valid string.</exception>
        public void OpenConnection(string address)
        {
            VerificationProvider.VerifyValidString(address, "address");

            CloseConnection();

            // Fire up the service host using ourselves as the hosted service
            mService = new ServiceHost(this, new Uri(address));
            mService.Open();
        }

        /// <summary>
        /// Closes down any existing connections and stops listening for incoming messages.
        /// </summary>
        public void CloseConnection()
        {
            if (mService != null)
            {
                mService.Close();
                mService = null;
            }
        }

        /// <summary>
        /// Transmits the given communication message to the recipient.
        /// </summary>
        /// <param name="message">The communication message to be transmitted to the recipient.</param>
        /// <exception cref="ArgumentNullException">Thrown when the given communication message is null.</exception>
        public void Transmit(CommunicationMessage message)
        {
            VerificationProvider.VerifyNotNull(message, "message");

            CommunicationMessageReceived.SafeInvoke<CommunicationMessageReceivedEventArgs>(this, new CommunicationMessageReceivedEventArgs(message));
        }
    }
}