using System;
using System.ServiceModel;

namespace Slade.Applications.DuplexCommunication.Networking
{
    /// <summary>
    /// Provides an abstract model for working with implementations of the <see cref="ICommunicationObject"/> interface.
    /// </summary>
    /// <typeparam name="TCommunicationObject">The type of communication object handled by this instance.</typeparam>
    public abstract class CommunicationChannelBase<TCommunicationObject> : NotificationObject, ICommunicationChannel
        where TCommunicationObject : class, ICommunicationObject
    {
        /// <summary>
        /// Raised when the connection state of the communication channel changes.
        /// </summary>
        public event EventHandler StateChanged;

        private TCommunicationObject mCommunicationObject;

        /// <summary>
        /// Gets or sets the communication object instance for this class.
        /// </summary>
        protected TCommunicationObject CommunicationObject
        {
            get { return mCommunicationObject; }
            set
            {
                if (mCommunicationObject != null)
                {
                    // Unhook all event handlers from the existing communication object instance
                    mCommunicationObject.Opening -= mCommunicationObject_StateChanged;
                    mCommunicationObject.Opened -= mCommunicationObject_StateChanged;
                    mCommunicationObject.Closing -= mCommunicationObject_StateChanged;
                    mCommunicationObject.Closed -= mCommunicationObject_StateChanged;
                    mCommunicationObject.Faulted -= mCommunicationObject_StateChanged;
                }

                SetValue<TCommunicationObject>(ref mCommunicationObject, value, "CommunicationObject", "State");

                if (mCommunicationObject != null)
                {
                    // Hook all event handlers to the new communication object instance
                    mCommunicationObject.Opening += mCommunicationObject_StateChanged;
                    mCommunicationObject.Opened += mCommunicationObject_StateChanged;
                    mCommunicationObject.Closing += mCommunicationObject_StateChanged;
                    mCommunicationObject.Closed += mCommunicationObject_StateChanged;
                    mCommunicationObject.Faulted += mCommunicationObject_StateChanged;
                }
            }
        }

        /// <summary>
        /// Gets the state of the communication channel connection.
        /// </summary>
        public CommunicationState State
        {
            get { return CommunicationObject == null ? (CommunicationState)(-1) : CommunicationObject.State; }
        }

        /// <summary>
        /// Connects the given user to the hosted service channel.
        /// </summary>
        /// <param name="profile">Information pertaining to the user to be connected.</param>
        /// <exception cref="ArgumentNullException">Thrown when the given user profile is null.</exception>
        public abstract void Connect(UserProfile profile);

        /// <summary>
        /// Disconnects the specified user from the hosted service channel.
        /// </summary>
        /// <param name="userId">The Id of the user to be disconnected.</param>
        public abstract void Disconnect(Guid userId);

        /// <summary>
        /// Transmits the given communication message to the recipient.
        /// </summary>
        /// <param name="message">The communication message to be transmitted to the recipient.</param>
        /// <exception cref="ArgumentNullException">Thrown when the given communication message is null.</exception>
        public abstract void Transmit(CommunicationMessage message);

        private void mCommunicationObject_StateChanged(object sender, EventArgs e)
        {
            OnStateChanged(e);
            OnPropertyChanged("State");

            StateChanged.SafeInvoke(sender, e);
        }

        /// <summary>
        /// Allows derived types to handle the event of a state change in the communication object instance.
        /// </summary>
        protected virtual void OnStateChanged(EventArgs e)
        {
        }

        protected void EnsureConnected()
        {
            if (mCommunicationObject == null)
            {
                throw new InvalidOperationException("No communication object connection has been established.");
            }
        }
    }
}