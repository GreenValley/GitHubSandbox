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
        /// Opens a connection to the specified address.
        /// </summary>
        /// <param name="address">The URL address on which to open a connection.</param>
        /// <exception cref="ArgumentException">Thrown when the given address is not a valid string.</exception>
        void OpenConnection(string address);

        /// <summary>
        /// Closes down any existing connections.
        /// </summary>
        void CloseConnection();

        /// <summary>
        /// Transmits the given communication message to the recipient.
        /// </summary>
        /// <param name="message">The communication message to be transmitted to the recipient.</param>
        /// <exception cref="ArgumentNullException">Thrown when the given communication message is null.</exception>
        [OperationContract]
        void Transmit(CommunicationMessage message);
    }
}