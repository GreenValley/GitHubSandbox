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
        protected override void RunCore()
        {
            throw new System.NotImplementedException();
        }
    }
}