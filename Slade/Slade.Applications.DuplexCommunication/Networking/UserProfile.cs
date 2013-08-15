using System;

namespace Slade.Applications.DuplexCommunication.Networking
{
    /// <summary>
    /// Represents a connected user and all pertinent information.
    /// </summary>
    public class UserProfile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserProfile"/> class.
        /// </summary>
        /// <param name="username">The name of the relevant user.</param>
        /// <exception cref="ArgumentException">Thrown when the given username is not a valid string.</exception>
        public UserProfile(string username)
        {
            VerificationProvider.VerifyValidString(username, "username");

            UserId = Guid.NewGuid();
            Username = username;
        }

        /// <summary>
        /// Gets or sets a unique Id to identify the user.
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Gets or sets the name of the relevant user.
        /// </summary>
        public string Username { get; set; }
    }
}