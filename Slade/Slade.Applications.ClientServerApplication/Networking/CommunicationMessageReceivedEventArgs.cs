using System;

namespace Slade.Applications.ClientServerApplication.Networking
{
    /// <summary>
    /// Contains information pertinent to the event of receiving a communication message.
    /// </summary>
    public sealed class CommunicationMessageReceivedEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CommunicationMessage"/> class.
        /// </summary>
        /// <param name="message">The communication message received through network communications.</param>
        /// <exception cref="ArgumentNullException">Thrown when the given communication message is null.</exception>
        public CommunicationMessageReceivedEventArgs(CommunicationMessage message)
        {
            VerificationProvider.VerifyNotNull(message, "message");

            Message = message;
        }

        /// <summary>
        /// Provides access to the communication message received through network communications.
        /// </summary>
        public CommunicationMessage Message { get; private set; }
    }
}