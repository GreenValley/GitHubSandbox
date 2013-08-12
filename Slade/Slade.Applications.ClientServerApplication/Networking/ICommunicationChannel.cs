using System;
using System.ServiceModel;

namespace Slade.Applications.ClientServerApplication.Networking
{
    /// <summary>
    /// Provides a contract to a channel used to transmit instances of the <see cref="CommunicationMessage"/> class.
    /// </summary>
    [ServiceContract]
    public interface ICommunicationChannel
    {
        /// <summary>
        /// Transmits the given communication message to the recipient.
        /// </summary>
        /// <param name="message">The communication message to be transmitted to the recipient.</param>
        /// <exception cref="ArgumentNullException">Thrown when the given communication message is null.</exception>
        void Transmit(CommunicationMessage message);
    }
}