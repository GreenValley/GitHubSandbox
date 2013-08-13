using Slade.Applications.ClientServerApplication.Networking;
using Slade.Applications.ClientServerApplication.ViewModels;

namespace Slade.Applications.ClientServerApplication
{
    /// <summary>
    /// Represents the logic control behind the main window of the application.
    /// </summary>
    public partial class MainWindow
    {
        private readonly ConnectionInformation mConnectionInformation = new ConnectionInformation();
        private readonly ConnectionManager mConnectionManager;

        private readonly ConnectionViewModel mConnectionViewModel;
        private readonly InstantMessagingViewModel mInstantMessagingViewModel;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow()
        {
            mConnectionManager = new ConnectionManager(mConnectionInformation);

            mConnectionViewModel = new ConnectionViewModel(mConnectionManager);
            mInstantMessagingViewModel = new InstantMessagingViewModel(mConnectionManager);

            InitializeComponent();

            // Manually setup the data context through here rather than the editable markup
            DataContext = this;
        }

        /// <summary>
        /// Provides access to the current configurable network connection information.
        /// </summary>
        public ConnectionInformation ConnectionInformation
        {
            get { return mConnectionInformation; }
        }

        /// <summary>
        /// Provides access to the view model for controlling the connection configuration.
        /// </summary>
        public ConnectionViewModel ConnectionViewModel
        {
            get { return mConnectionViewModel; }
        }

        /// <summary>
        /// Provides access to the view model for controlling the instant messaging process.
        /// </summary>
        public InstantMessagingViewModel InstantMessagingViewModel
        {
            get { return mInstantMessagingViewModel; }
        }
    }
}