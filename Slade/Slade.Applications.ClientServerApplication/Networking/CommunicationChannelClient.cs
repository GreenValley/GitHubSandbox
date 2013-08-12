using System;
using System.ServiceModel;

namespace Slade.Applications.ClientServerApplication.Networking
{
    /// <summary>
    /// Implements the <see cref="ICommunicationChannel"/> using a SOAP based service client proxy.
    /// </summary>
    public class CommunicationChannelClient : ClientBase<ICommunicationChannel>, ICommunicationChannel
    {
        /// <summary>
        /// Transmits the given communication message to the recipient.
        /// </summary>
        /// <param name="message">The communication message to be transmitted to the recipient.</param>
        /// <exception cref="ArgumentNullException">Thrown when the given communication message is null.</exception>
        public void Transmit(CommunicationMessage message)
        {
            VerificationProvider.VerifyNotNull(message, "message");

            Channel.Transmit(message);
        }
    }
}