using Slade.Applications.ClientServerApplication.Networking;
using System;
using System.Windows;
using System.Windows.Input;

namespace Slade.Applications.ClientServerApplication.ViewModels
{
    /// <summary>
    /// Controls the information pertaining to the peer-to-peer network connection required by the application.
    /// </summary>
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
            ConnectionInformation = connectionManager.ConnectionInformation;

            RefreshHostingConnectionCommand =
                new DelegateCommand(
                    x => RefreshConnection(mConnectionManager.ChannelService, ConnectionInformation.HostingAddress));

            RefreshClientConnectionCommand =
                new DelegateCommand(
                    x => RefreshConnection(mConnectionManager.ChannelClient, ConnectionInformation.RecipientAddress));
        }

        /// <summary>
        /// Provides access to the command used to refresh the connection for the hosted service.
        /// </summary>
        public ICommand RefreshHostingConnectionCommand { get; private set; }

        /// <summary>
        /// Provides access to the command used to refresh the connection to the recipient.
        /// </summary>
        public ICommand RefreshClientConnectionCommand { get; private set; }

        /// <summary>
        /// Provides access to the current configurable network connection information.
        /// </summary>
        public ConnectionInformation ConnectionInformation { get; private set; }

        private void RefreshConnection(ICommunicationChannel channel, string address)
        {
            try
            {
                channel.OpenConnection(address);
            }
            catch (Exception ex)    // TODO: Determine specific exception types.
            {
                MessageBox.Show(ex.Message, "Connection Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}