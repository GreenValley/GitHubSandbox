using Slade.Commands.Parsing;
using System;

namespace Slade.Commands
{
    /// <summary>
    /// Contains information pertaining to a command registered as being available within the a console application.
    /// </summary>
    public sealed class ExecutableCommandRegistration
    {
        private readonly string mName;
        private readonly Action<CommandResult> mExecutionAction;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExecutableCommandRegistration"/> class.
        /// </summary>
        /// <param name="name">The name of the command used for registration.</param>
        /// <param name="executionAction">The action to be invoked when the command is triggered.</param>
        /// <exception cref="ArgumentException">Thrown when the given name is an invalid string.</exception>
        /// <exception cref="ArgumentNullException">Thrown when the given execution action is null.</exception>
        internal ExecutableCommandRegistration(string name, Action<CommandResult> executionAction)
        {
            VerificationProvider.VerifyValidString(name, "name");
            VerificationProvider.VerifyNotNull(executionAction, "executionAction");

            mName = name;
            mExecutionAction = executionAction;
        }

        /// <summary>
        /// Gets the name of the command used for registration.
        /// </summary>
        public string Name
        {
            get { return mName; }
        }

        /// <summary>
        /// Executes the registered command by invoking the contained action.
        /// </summary>
        /// <param name="result">The result of the command parsing process pertinent to the registered command.</param>
        /// <exception cref="ArgumentNullException">Thrown when the given command result is null.</exception>
        /// <exception cref="InvalidOperationException">Thrown when invocation of the contained action fails.</exception>
        public void Execute(CommandResult result)
        {
            VerificationProvider.VerifyNotNull(result, "result");

            try
            {
                mExecutionAction.Invoke(result);
            }
            catch (Exception ex)
            {
                // Wrap the exception within an InvalidOperationException and re-throw
                throw new InvalidOperationException("An error occurred while executing the command. See inner exception for details.", ex);
            }
        }
    }
}