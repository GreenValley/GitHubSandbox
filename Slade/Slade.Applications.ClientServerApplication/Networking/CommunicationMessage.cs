using System;

namespace Slade.Applications.ClientServerApplication.Networking
{
    /// <summary>
    /// Represents a message sent to/from a connected user.
    /// </summary>
    public class CommunicationMessage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CommunicationMessage"/> class.
        /// </summary>
        /// <param name="username">The name of the user the message originated from.</param>
        /// <param name="message">The message sent by the connected user.</param>
        /// <param name="timestamp">The date/time the message was sent.</param>
        /// <param name="isLocalUser">Indicates whether the message originated from the local user.</param>
        /// <exception cref="ArgumentException">Thrown when either the given username or message is
        /// not a valid string.</exception>
        public CommunicationMessage(string username, string message, DateTime timestamp, bool isLocalUser)
        {
            VerificationProvider.VerifyValidString(username, "username");
            VerificationProvider.VerifyValidString(message, "message");

            Username = username;
            Message = message;
            Timestamp = timestamp;
            IsLocalUser = isLocalUser;
        }

        /// <summary>
        /// Gets the name of the user the message originated from.
        /// </summary>
        public string Username { get; private set; }

        /// <summary>
        /// Gets the message sent by the connected user.
        /// </summary>
        public string Message { get; private set; }

        /// <summary>
        /// Gets the date/time the message was sent by the connected user.
        /// </summary>
        public DateTime Timestamp { get; private set; }

        /// <summary>
        /// Gets a value that indicates whether the message originated from the local user.
        /// </summary>
        public bool IsLocalUser { get; private set; }
    }
}