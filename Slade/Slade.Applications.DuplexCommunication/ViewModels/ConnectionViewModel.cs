using Slade.Applications.DuplexCommunication.Networking;
using System;
using System.Windows.Input;

namespace Slade.Applications.DuplexCommunication.ViewModels
{
    public class ConnectionViewModel : ViewModelBase
    {
        private readonly ConnectionManager mConnectionManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionViewModel"/> class.
        /// </summary>
        /// <param name="connectionManager">Manager of the client and server communication channels.</param>
        /// <exception cref="ArgumentNullException">Thrown when the given connection manager is null.</exception>
        public ConnectionViewModel(ConnectionManager connectionManager)
        {
            VerificationProvider.VerifyNotNull(connectionManager, "connectionManager");

            mConnectionManager = connectionManager;

            RefreshConnectionCommand = new DelegateCommand(x => RefreshConnection());
        }

        /// <summary>
        /// Provides access to the command used to refresh the active connections to the communication channels.
        /// </summary>
        public ICommand RefreshConnectionCommand { get; private set; }

        /// <summary>
        /// Provides access to the manager of the communication channel connections.
        /// </summary>
        public ConnectionManager ConnectionManager
        {
            get { return mConnectionManager; }
        }

        private void RefreshConnection()
        {
            System.Windows.MessageBox.Show("TODO: RefreshConnection");
        }
    }
}