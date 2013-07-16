using Slade.Commands.Parsing;
using System;

namespace Slade.Commands.RunCommandApplication
{
    /// <summary>
    /// Application used to provide a Run command that can be used to register executables and script files
    /// that can be run through the use of this application by specifying the registered name.
    /// </summary>
    public class RunCommandConsoleApplication : ConsoleApplication
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RunCommandConsoleApplication" /> class.
        /// </summary>
        /// <param name="arguments">A collection of all arguments passed through to the application
        /// from the command line.</param>
        /// <exception cref="ArgumentNullException">Thrown when the given collection of arguments is null.</exception>
        public RunCommandConsoleApplication(string[] arguments)
            : base(arguments)
        {
        }

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
            // TODO: Check to see if we have a registration command.
        }

        private void HandleLaunching(CommandResultSet commands)
        {
            // TODO: Check to see if we have a launching command.
        }
    }
}