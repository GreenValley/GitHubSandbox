using System;
using System.ServiceModel;

namespace Slade.Applications.ClientServerApplication.Networking
{
    /// <summary>
    /// Implements the <see cref="ICommunicationChannel"/> using a SOAP based service client proxy.
    /// </summary>
    public class CommunicationChannelClient : CommunicationChannelBase<ClientBase<ICommunicationChannel>>
    {
        private string mServerAddress;

        /// <summary>
        /// Opens a connection to the given address.
        /// </summary>
        /// <param name="address">The address to which to send messages.</param>
        /// <exception cref="ArgumentException">Thrown when the given address is not a valid string.</exception>
        public override void OpenConnection(string address)
        {
            VerificationProvider.VerifyValidString(address, "address");

            // Only reset the connection if the given address differs from the one we are currently using
            if (String.Equals(mServerAddress, address, StringComparison.InvariantCultureIgnoreCase))
            {
                return;
            }

            CloseConnection();

            // Update the endpoint address before opening the connection
            // Note: A connection will be opened upon constructing the client channel.
            mServerAddress = address;
            CommunicationObject = new InternalCommunicationChannelClient(address);
        }

        /// <summary>
        /// Closes any open connections and prevents the transmission of any further messages.
        /// </summary>
        public override void CloseConnection()
        {
            if (CommunicationObject != null)
            {
                CommunicationObject.Close();
                CommunicationObject = null;
            }
        }

        /// <summary>
        /// Transmits the given communication message to the recipient.
        /// </summary>
        /// <param name="message">The communication message to be transmitted to the recipient.</param>
        /// <exception cref="ArgumentNullException">Thrown when the given communication message is null.</exception>
        /// <exception cref="InvalidOperationException">Thrown when no client channel has been opened.</exception>
        public override void Transmit(CommunicationMessage message)
        {
            VerificationProvider.VerifyNotNull(message, "message");

            EnsureConnected();

            var internalChannel = (InternalCommunicationChannelClient)CommunicationObject;
            internalChannel.ClientChannel.Transmit(message);
        }

        /// <summary>
        /// Extends the <see cref="ClientBase{TChannel}"/> class to provide a proxy implementation of the <see cref="ICommunicationChannel"/> interface.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Instances of this class are required since the channel is automatically opened upon creation of this class, which means construction is the 
        /// only point at which a different endpoint address may be specified.
        /// </para>
        /// </remarks>
        private sealed class InternalCommunicationChannelClient : ClientBase<ICommunicationChannel>
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="InternalCommunicationChannelClient"/> class.
            /// </summary>
            /// <param name="address">The address to which to send messages.</param>
            internal InternalCommunicationChannelClient(string address)
                : base(new BasicHttpBinding(), new EndpointAddress(address))
            {
            }

            /// <summary>
            /// Provides public access to the protected <see cref="ClientBase{TChannel}.Channel"/> property.
            /// </summary>
            public ICommunicationChannel ClientChannel
            {
                get { return Channel; }
            }
        }
    }
}