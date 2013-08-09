using System;

namespace Slade.Applications.ClientServerApplication.ViewModels
{
    /// <summary>
    /// Controls the information pertaining to the peer-to-peer network connection required by the application.
    /// </summary>
    public class ConnectionViewModel : ViewModelBase
    {
        private readonly ConnectionInformation mConnectionInformation;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionInformation"/> class.
        /// </summary>
        /// <param name="connectionInformation">Information pertaining to the configurable network connection.</param>
        /// <exception cref="ArgumentNullException">Thrown when the given connection information is null.</exception>
        public ConnectionViewModel(ConnectionInformation connectionInformation)
        {
            VerificationProvider.VerifyNotNull(connectionInformation, "connectionInformation");

            mConnectionInformation = connectionInformation;
        }

        /// <summary>
        /// Provides access to the current configurable network connection information.
        /// </summary>
        public ConnectionInformation ConnectionInformation
        {
            get { return mConnectionInformation; }
        }
    }
}