using Slade.Commands.Parsing;
using System;
using System.Collections.Generic;

namespace Slade.Commands.RunCommandApplication
{
    /// <summary>
    /// Application used to provide a Run command that can be used to register executables and script files
    /// that can be run through the use of this application by specifying the registered name.
    /// </summary>
    public class RunCommandConsoleApplication : ConsoleApplication
    {
        private readonly Dictionary<string, string> mProgramRegistrations =
            new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);

        /// <summary>
        /// Initializes a new instance of the <see cref="RunCommandConsoleApplication" /> class.
        /// </summary>
        /// <param name="arguments">A collection of all arguments passed through to the application
        /// from the command line.</param>
        /// <exception cref="ArgumentNullException">Thrown when the given collection of arguments is null.</exception>
        public RunCommandConsoleApplication(string[] arguments)
            : base(arguments)
        {
            Configure(
                configuration =>
                {
                    configuration.AllowMultipleValues = true;
                    configuration.AllowSwitches = false;
                    configuration.Prefixes = CommandPrefixes.ForwardSlash;
                    configuration.Separators = CommandSeparators.Equals;
                }
            );
        }

        // TODO: It would be nice to be able to register commands with patterns and descriptions so that the a default help command can be provided
        // TODO: for all commands and each individual command - using the format of "command help" or "command help parameter".

        /// <summary>
        /// Executes the operations contained within the console application implementation.
        /// </summary>
        /// <param name="commands">A set of commands parsed from the given command-line arguments.</param>
        protected override void RunCore(CommandResultSet commands)
        {
            HandleRegistration(commands);
            HandleLaunching(commands);
        }

        private void HandleRegistration(CommandResultSet commands)
        {
            string[] registrationParameters;

            if (!commands.TryGetValue<string[]>("register", out registrationParameters))
            {
                return;
            }

            if (registrationParameters.Length != 2)
            {
                ConsoleHelper.WriteLine(ConsoleMessageType.Error,
                                        "Invalid number of values specified for command registration. Please specify the name and program path.");

                return;
            }

            // Extract the registration name and program path from the registration parameters
            string registrationName = registrationParameters[0];
            string programPath = registrationParameters[1];

            VerificationProvider.VerifyValidString(registrationName, "registrationName");
            VerificationProvider.VerifyValidString(programPath, "programPath");

            // Check we haven't already made a registration with this name
            // TODO: This information needs to be persisted to some file store.
            if (mProgramRegistrations.ContainsKey(registrationName))
            {
                ConsoleHelper.WriteLine(ConsoleMessageType.Warning,
                                        "A program has already been registered under the name '{0}' and will be overridden.",
                                        registrationName);
            }

            // Store the registration, overriding any existing registrations
            mProgramRegistrations[registrationName] = programPath;

            ConsoleHelper.WriteLine(ConsoleMessageType.Information,
                                    "A registration has been successfully made under '{0}' for the path '{1}'.",
                                    registrationName, programPath);
        }

        private void HandleLaunching(CommandResultSet commands)
        {
            // TODO: Check to see if we have a launching command.
        }
    }
}