using System;
using System.ServiceModel;

namespace Slade.Applications.DuplexCommunication.Networking
{
    /// <summary>
    /// Implements the <see cref="ICommunicationChannel"/> using a SOAP based service client proxy.
    /// </summary>
    public class CommunicationChannelClient : CommunicationChannelBase<ClientBase<ICommunicationChannel>>
    {
        private UserProfile mUserProfile;
        private string mServerAddress;

        /// <summary>
        /// Initializes the connection parameters for the client side communication channel.
        /// </summary>
        /// <param name="profile">Information pertaining to the connected user.</param>
        /// <param name="serverAddress">The URI of the hosted service to which to connect.</param>
        /// <exception cref="ArgumentNullException">Thrown when the given user profile is null.</exception>
        /// <exception cref="ArgumentException">Thrown when the given server address is not a valid string.</exception>
        public void Initialize(UserProfile profile, string serverAddress)
        {
            VerificationProvider.VerifyNotNull(profile, "profile");
            VerificationProvider.VerifyValidString(serverAddress, "serverAddress");

            mUserProfile = profile;
            mServerAddress = serverAddress;

            OpenConnection();
        }

        /// <summary>
        /// Opens a new connection to the hosted service.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown when no call has first been to initialize the channel.</exception>
        public void OpenConnection()
        {
            AssertInitialized();
            CloseConnection();

            var internalChannel = new InternalCommunicationChannelClient(mServerAddress);
            CommunicationObject = internalChannel;

            internalChannel.ClientChannel.Connect(mUserProfile);
        }

        public override void Connect(UserProfile profile)
        {
            // Simply re-initialize the communication channel to trigger a re-connect
            Initialize(profile, mServerAddress);
        }

        /// <summary>
        /// Closes any open connections to the hosted service.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown when no call has first been to initialize the channel.</exception>
        public void CloseConnection()
        {
            AssertInitialized();

            var internalChannel = (InternalCommunicationChannelClient)CommunicationObject;

            if (internalChannel != null)
            {
                internalChannel.ClientChannel.Disconnect(mUserProfile.UserId);
                internalChannel.Close();
            }
        }

        public override void Disconnect(Guid userId)
        {
            // Make sure the user profile Id is the same
            if (mUserProfile.UserId != userId)
            {
                throw new ArgumentException("The given user Id does not match the one stored under the user profile.");
            }

            CloseConnection();
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

        private void AssertInitialized()
        {
            if (mUserProfile == null || String.IsNullOrWhiteSpace(mServerAddress))
            {
                throw new InvalidOperationException("The communication client has not been initialized.");
            }
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