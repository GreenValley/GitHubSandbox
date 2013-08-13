using System;
namespace Slade.Applications.ClientServerApplication.Networking
{
    /// <summary>
    /// Contains information pertaining to a simple peer-to-peer network connection that can be data bound.
    /// </summary>
    public sealed class ConnectionInformation : NotificationObject
    {
        private string mUsername;
        private string mHostingAddress;
        private string mRecipientAddress;

        private bool mIsEditing = true;

        /// <summary>
        /// Gets or sets the display name for the connected user.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown when an attempt is made to change the property
        /// value without first setting the <see cref="IsEditing"/> property to true.</exception>
        public string Username
        {
            get { return mUsername; }
            set
            {
                AssertEditing();
                SetValue<string>(ref mUsername, value, "Username");
            }
        }

        /// <summary>
        /// Gets or sets the raw URI to use as the address for hosting the service.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown when an attempt is made to change the property
        /// value without first setting the <see cref="IsEditing"/> property to true.</exception>
        public string HostingAddress
        {
            get { return mHostingAddress; }
            set
            {
                AssertEditing();
                SetValue<string>(ref mHostingAddress, value, "HostingAddress");
            }
        }

        /// <summary>
        /// Gets or sets the raw URI to use as the address of the recipient service to connect to.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown when an attempt is made to change the property
        /// value without first setting the <see cref="IsEditing"/> property to true.</exception>
        public string RecipientAddress
        {
            get { return mRecipientAddress; }
            set
            {
                AssertEditing();
                SetValue<string>(ref mRecipientAddress, value, "RecipientAddress");
            }
        }

        /// <summary>
        /// Gets or sets a value that indicates whether the connection information is currently being edited.
        /// </summary>
        public bool IsEditing
        {
            get { return mIsEditing; }
            set { SetValue<bool>(ref mIsEditing, value, "IsEditing"); }
        }

        private void AssertEditing()
        {
            if (!IsEditing)
            {
                throw new InvalidOperationException(
                    "Connection information values cannot be changed when the object is not set to being edited.");
            }
        }
    }
}