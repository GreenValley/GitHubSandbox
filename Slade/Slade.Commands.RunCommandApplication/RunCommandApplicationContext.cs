using System;
using System.Collections.Generic;

namespace Slade.Commands.RunCommandApplication
{
    /// <summary>
    /// Implements the <see cref="IApplicationContext"/> interface to provide a context specific to working
    /// with a running instance of the <see cref="RunCommandConsoleApplication"/> class.
    /// </summary>
    public sealed class RunCommandApplicationContext : IRunCommandApplicationContext
    {
        private readonly Dictionary<string, string> mProgramRegistrations =
            new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);

        /// <summary>
        /// Provides access to the collection of program registrations stored within the current context.
        /// </summary>
        public Dictionary<string, string> ProgramRegistrations
        {
            get { return mProgramRegistrations; }
        }

        /// <summary>
        /// Loads the state of the application context from a backing store.
        /// </summary>
        public void Load()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Saves the state of the application context into a backing store.
        /// </summary>
        public void Save()
        {
            throw new NotImplementedException();
        }
    }
}