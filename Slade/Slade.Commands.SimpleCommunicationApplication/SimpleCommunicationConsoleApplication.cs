using Slade.Commands.Parsing;
using Slade.Commands.SimpleCommunicationApplication.Networking;
using System;
using System.ServiceModel;
using System.Threading;

namespace Slade.Commands.SimpleCommunicationApplication
{
    /// <summary>
    /// Application used to provide one node in a simple peer-to-peer communication network.
    /// </summary>
    public class SimpleCommunicationConsoleApplication : ConsoleApplication<ISimpleCommunicationApplicationContext>
    {
        private readonly AutoResetEvent mWaitHandle = new AutoResetEvent(initialState: false);

        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleCommunicationConsoleApplication"/> class.
        /// </summary>
        /// <param name="applicationContext">An instance of the strongly-typed application context.</param>
        /// <param name="arguments">A collection of all arguments passed through to the application
        /// from the command line.</param>
        /// <exception cref="ArgumentNullException">Thrown when the given application context or collection of arguments is null.</exception>
        public SimpleCommunicationConsoleApplication(ISimpleCommunicationApplicationContext applicationContext, string[] arguments)
            : base(applicationContext, arguments)
        {
            Configure(
                configuration =>
                {
                    configuration.AllowMultipleValues = false;
                    configuration.AllowSwitches = false;
                    configuration.Prefixes = CommandPrefixes.ForwardSlash;
                    configuration.Separators = CommandSeparators.Equals;
                }
            );
        }

        /// <summary>
        /// Registers all supported commands with the console application's registrar.
        /// </summary>
        /// <param name="registrar">A collection of command registrations for the application.</param>
        protected override void RegisterCommands(ExecutableCommandRegistrar registrar)
        {
            base.RegisterCommands(registrar);

            registrar.Register<string>("listen", HandleListening);
            registrar.Register<string>("send", HandleSending);
        }

        private void HandleListening(string address)
        {
            var service = new SimpleCommunicationService();
            service.MessageReceived += message => ConsoleHelper.WriteLine(ConsoleMessageType.Information, message);

            var host = new ServiceHost(service);
            host.AddServiceEndpoint(typeof(ISimpleCommunicationService), new BasicHttpBinding(), address);

            host.Open();
            mWaitHandle.WaitOne();
        }

        private void HandleSending(string message)
        {
            var serviceClient = new SimpleCommunicationServiceClient();
            serviceClient.SendMessage(message);
        }
    }
}