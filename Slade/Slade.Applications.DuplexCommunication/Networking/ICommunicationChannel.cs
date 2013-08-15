using System;
using System.ServiceModel;

namespace Slade.Applications.DuplexCommunication.Networking
{
    /// <summary>
    /// Provides a contract to a channel used to transmit instances of the <see cref="CommunicationMessage"/> class.
    /// </summary>
    [ServiceContract]
    public interface ICommunicationChannel
    {
        /// <summary>
        /// Connects the given user to the hosted service channel.
        /// </summary>
        /// <param name="profile">Information pertaining to the user to be connected.</param>
        /// <exception cref="ArgumentNullException">Thrown when the given user profile is null.</exception>
        [OperationContract]
        void Connect(UserProfile profile);

        /// <summary>
        /// Disconnects the specified user from the hosted service channel.
        /// </summary>
        /// <param name="userId">The Id of the user to be disconnected.</param>
        [OperationContract]
        void Disconnect(Guid userId);

        /// <summary>
        /// Transmits the given communication message to the recipient.
        /// </summary>
        /// <param name="message">The communication message to be transmitted to the recipient.</param>
        /// <exception cref="ArgumentNullException">Thrown when the given communication message is null.</exception>
        [OperationContract]
        void Transmit(CommunicationMessage message);
    }
}