using Slade.Conversion;
using System;
using System.Collections.Generic;

namespace Slade.Commands
{
    /// <summary>
    /// Handles registration of instances of the <see cref="ExecutableCommandRegistration"/> class.
    /// </summary>
    public sealed class ExecutableCommandRegistrar
    {
        private readonly ObjectConverterFactory mObjectConverterFactory;

        private readonly Dictionary<string, IExecutableCommandRegistration> mCommandRegistrations =
            new Dictionary<string, IExecutableCommandRegistration>(StringComparer.InvariantCultureIgnoreCase);

        /// <summary>
        /// Initializes a new instance of the <see cref="ExecutableCommandRegistrar"/> class.
        /// </summary>
        /// <param name="objectConverterFactory">A factory used to create requested object converters.</param>
        /// <exception cref="ArgumentNullException">Thrown when the given object converter factory is null.</exception>
        internal ExecutableCommandRegistrar(ObjectConverterFactory objectConverterFactory)
        {
            VerificationProvider.VerifyNotNull(objectConverterFactory, "objectConverterFactory");

            mObjectConverterFactory = objectConverterFactory;
        }

        /// <summary>
        /// Registers the given information to make the command available for later execution.
        /// </summary>
        /// <typeparam name="TValue">The type of value expected to be handled by the execution action.</typeparam>
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
        public void Register<TValue>(string name, Action<TValue> executionAction)
        {
            VerificationProvider.VerifyValidString(name, "name");
            VerificationProvider.VerifyNotNull(executionAction, "executionAction");

            mCommandRegistrations[name] = new ExecutableCommandRegistration<TValue>(name, executionAction, mObjectConverterFactory);
        }
    }
}