namespace Slade.Applications.ClientServerApplication.Networking
{
    /// <summary>
    /// Contains information pertaining to a simple peer-to-peer network connection that can be data bound.
    /// </summary>
    public sealed class ConnectionInformation : NotificationObject
    {
        private string mUsername;
        private string mAddress;

        private bool mIsEditing = true;

        /// <summary>
        /// Gets or sets the display name for the connected user.
        /// </summary>
        public string Username
        {
            get { return mUsername; }
            set { SetValue<string>(ref mUsername, value, "Username"); }
        }

        /// <summary>
        /// Gets or sets the raw URI to use as the address for hosting/connecting the service.
        /// </summary>
        public string Address
        {
            get { return mAddress; }
            set { SetValue<string>(ref mAddress, value, "Address"); }
        }

        public bool IsEditing
        {
            get { return mIsEditing; }
            set { SetValue<bool>(ref mIsEditing, value, "IsEditing"); }
        }
    }
}