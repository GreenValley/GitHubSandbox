﻿using Slade.Commands.Parsing;
using Slade.Conversion;
using System;

namespace Slade.Commands
{
    /// <summary>
    /// Provides an abstract implementation of an application used to handle interaction through the
    /// use of a command line interface.
    /// </summary>
    public abstract class ConsoleApplication
    {
        private readonly CommandLineParser mParser = new CommandLineParser();
        private readonly ObjectConverterFactory mObjectConverterFactory = new ObjectConverterFactory();

        private readonly string[] mArguments;
        private CommandResultSet mCommands;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConsoleApplication" /> class.
        /// </summary>
        /// <param name="arguments">A collection of all arguments passed through to the application
        /// from the command line.</param>
        /// <exception cref="ArgumentNullException">Thrown when the given collection of arguments is null.</exception>
        protected ConsoleApplication(string[] arguments)
        {
            VerificationProvider.VerifyNotNull(arguments, "arguments");

            mArguments = arguments;
        }

        /// <summary>
        /// Allows the application to be configured by derived classes.
        /// </summary>
        /// <param name="configuration">An action used to configure the rule set being used by the application for
        /// defining the context parameters for the command-line argument parsing operation.</param>
        /// <exception cref="ArgumentNullException">Thrown when the given configuration action is null.</exception>
        protected ConsoleApplication Configure(Action<CommandLineRuleSet> configuration)
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
            EnsureArgumentsParsed();

            try
            {
                RunCore(mCommands);
            }
            catch (Exception ex)
            {
                ConsoleHelper.WriteLine(ConsoleMessageType.Error, "Application execution failed:\n{0}", ex.Message);
            }
        }

        /// <summary>
        /// Executes the operations contained within the console application implementation.
        /// </summary>
        /// <param name="commands">A set of commands parsed from the given command-line arguments.</param>
        protected abstract void RunCore(CommandResultSet commands);

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