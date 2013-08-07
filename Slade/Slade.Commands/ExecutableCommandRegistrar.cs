using Slade.Commands.Parsing;
using System;
using System.Collections.Generic;

namespace Slade.Commands
{
    /// <summary>
    /// Handles registration of instances of the <see cref="ExecutableCommandRegistration"/> class.
    /// </summary>
    public sealed class ExecutableCommandRegistrar
    {
        private readonly Dictionary<string, ExecutableCommandRegistration> mCommandRegistrations =
            new Dictionary<string, ExecutableCommandRegistration>(StringComparer.InvariantCultureIgnoreCase);

        /// <summary>
        /// Registers the given information to make the command available for later execution.
        /// </summary>
        /// <param name="name">The name of the command to be registered.</param>
        /// <param name="executionAction">The action to be executed when the command is triggered.</param>
        /// <exception cref="ArgumentException">Thrown when the given command name is not a valid string.</exception>
        /// <exception cref="ArgumentNullException">Thrown when the given execution action is null.</exception>
        /// <remarks>
        /// <para>
        /// If a command registration already exists under the given name, that registration will be overwritten
        /// by the given information.
        /// </para>
        /// </remarks>
        public void Register(string name, Action<CommandResult> executionAction)
        {
            VerificationProvider.VerifyValidString(name, "name");
            VerificationProvider.VerifyNotNull(executionAction, "executionAction");

            mCommandRegistrations[name] = new ExecutableCommandRegistration(name, executionAction);
        }
    }
}