using Slade.Commands.Parsing;
using Slade.Conversion;
using System;

namespace Slade.Commands
{
    /// <summary>
    /// Provides an abstract implementation of an application used to handle interaction through the
    /// use of a command line interface.
    /// </summary>
    public abstract class ConsoleApplication<TContext>
        where TContext : IApplicationContext
    {
        private readonly ObjectConverterFactory mObjectConverterFactory = new ObjectConverterFactory();
        private readonly CommandLineParser mParser = new CommandLineParser();
        private readonly ExecutableCommandRegistrar mRegistrar;

        private readonly TContext mApplicationContext;

        private readonly string[] mArguments;
        private CommandResultSet mCommands;

        private bool mIsInitialized;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConsoleApplication" /> class.
        /// </summary>
        /// <param name="applicationContext">An instance of the strongly-typed application context.</param>
        /// <param name="arguments">A collection of all arguments passed through to the application
        /// from the command line.</param>
        /// <exception cref="ArgumentNullException">Thrown when the given application context or collection of arguments is null.</exception>
        protected ConsoleApplication(TContext applicationContext, string[] arguments)
        {
            VerificationProvider.VerifyNotNull(applicationContext, "applicationContext");
            VerificationProvider.VerifyNotNull(arguments, "arguments");

            mRegistrar = new ExecutableCommandRegistrar(mObjectConverterFactory);

            mArguments = arguments;

            // Initialize and load the application context
            mApplicationContext = applicationContext;
            mApplicationContext.Load();
        }

        private void Initialize()
        {
            if (mIsInitialized)
            {
                // The application has already run it's initialization process
                return;
            }

            RegisterCommands(mRegistrar);
            InitializeCore();

            // Flag that the application has now finished performing initialization
            mIsInitialized = true;
        }

        /// <summary>
        /// Allows derived types to perform custom initialization when the application is initialized.
        /// </summary>
        protected virtual void InitializeCore()
        {
        }

        /// <summary>
        /// Allows derived types to perform custom registration of application-specific supported commands.
        /// </summary>
        /// <param name="registrar">Handler of executable command registration.</param>
        protected virtual void RegisterCommands(ExecutableCommandRegistrar registrar)
        {
        }

        /// <summary>
        /// Provides access to the strongly-typed context for the current application.
        /// </summary>
        protected TContext ApplicationContext
        {
            get { return mApplicationContext; }
        }

        /// <summary>
        /// Allows the application to be configured by derived classes.
        /// </summary>
        /// <param name="configuration">An action used to configure the rule set being used by the application for
        /// defining the context parameters for the command-line argument parsing operation.</param>
        /// <exception cref="ArgumentNullException">Thrown when the given configuration action is null.</exception>
        protected ConsoleApplication<TContext> Configure(Action<CommandLineRuleSet> configuration)
        {
            VerificationProvider.VerifyNotNull(configuration, "configuration");

            configuration.Invoke(mParser.RuleSet);

            return this;
        }

        /// <summary>
        /// Starts execution of the console application.
        /// </summary>
        /// <remarks>
        /// <para>
        /// If the command-line arguments have not already been parsed by this point, then they will be before
        /// application execution properly begins.
        /// </para>
        /// </remarks>
        public void Run()
        {
            Initialize();
            EnsureArgumentsParsed();

            try
            {
                RunCore(mCommands);
            }
            catch (Exception ex)
            {
                ConsoleHelper.WriteLine(ConsoleMessageType.Error, "Application execution failed:\n{0}", ex.Message);
            }

            mApplicationContext.Save();
        }

        /// <summary>
        /// Executes the operations contained within the console application implementation.
        /// </summary>
        /// <param name="commands">A set of commands parsed from the given command-line arguments.</param>
        protected virtual void RunCore(CommandResultSet commands)
        {
            // TODO: Perform automatic command execution.
        }

        private void EnsureArgumentsParsed()
        {
            if (mCommands == null)
            {
                try
                {
                    var parsedCommands = mParser.Parse(mArguments);
                    mCommands = new CommandResultSet(mObjectConverterFactory, parsedCommands);
                }
                catch (Exception ex)
                {
                    ConsoleHelper.WriteLine(ConsoleMessageType.Error, "Failed to parse command-line arguments:\n{0}",
                                            ex.Message);
                }
            }
        }
    }
}