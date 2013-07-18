using System;
using System.Collections.Generic;

namespace Slade.Commands.Parsing
{
	/// <summary>
	/// Handles parsing of arguments supplied by a command-line interface based on a given set of parsing rules.
	/// </summary>
	public sealed class CommandLineParser
	{
		private readonly CommandLineRuleSet mRuleSet = CommandLineRuleSet.WindowsProfile;

		/// <summary>
		/// Provides access to the rule set that will be used when performing a parse operation.
		/// </summary>
		public CommandLineRuleSet RuleSet
		{
			get { return mRuleSet; }
		}

		/// <summary>
		/// Parses the given collection of arguments based on the current parser rule set.
		/// </summary>
		/// <param name="arguments">A collection of all arguments passed through to the application
		/// from the command line.</param>
		/// <returns>A collection of commands resulting from the parse operation.</returns>
		/// <exception cref="ArgumentNullException">Thrown when the given collection of arguments is null.</exception>
		public IEnumerable<CommandResult> Parse(string[] arguments)
		{
			VerificationProvider.VerifyNotNull(arguments, "arguments");

			// TODO

			return EmptyArray<CommandResult>.Instance;
		}
	}
}