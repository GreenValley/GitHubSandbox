namespace Slade.Commands.Parsing
{
	/// <summary>
	/// Represents a configurable set of rules to be used when performing a parsing operation on provided command-line arguments.
	/// </summary>
	public class CommandLineRuleSet
	{
		private static readonly CommandLineRuleSet sWindowsProfile = new CommandLineRuleSet();
		private static readonly CommandLineRuleSet sUnixProfile = new CommandLineRuleSet();

		/// <summary>
		/// Initializes all static information for the <see cref="CommandLineRuleSet"/> class.
		/// </summary>
		static CommandLineRuleSet()
		{
			// Configure the Windows and Unix profile rule sets
			WindowsProfile.Prefixes = CommandPrefixes.ForwardSlash | CommandPrefixes.SingleHyphen;
			WindowsProfile.Separators = CommandSeparators.Equals | CommandSeparators.Colon;
			WindowsProfile.AllowCombinedSwitches = false;
			WindowsProfile.AllowMultipleValues = true;
			WindowsProfile.AllowSwitches = true;

			UnixProfile.Prefixes = CommandPrefixes.SingleHyphen | CommandPrefixes.DoubleHyphen;
			WindowsProfile.Separators = CommandSeparators.Equals | CommandSeparators.Colon;
			UnixProfile.AllowCombinedSwitches = true;
			UnixProfile.AllowMultipleValues = true;
			UnixProfile.AllowSwitches = true;
		}

		/// <summary>
		/// Provides access to the rule set used by most Windows-based command-line programs.
		/// </summary>
		public static CommandLineRuleSet WindowsProfile
		{
			get { return sWindowsProfile; }
		}

		/// <summary>
		/// Provides access to the rule set used by most Unix-based command-line programs.
		/// </summary>
		public static CommandLineRuleSet UnixProfile
		{
			get { return sUnixProfile; }
		}

		/// <summary>
		/// Gets or sets the type of characters that denote valid prefixes for command keys.
		/// </summary>
		public CommandPrefixes Prefixes { get; set; }

		/// <summary>
		/// Gets or sets the type of characters that denote valid separators for command keys and values.
		/// </summary>
		public CommandSeparators Separators { get; set; }

		/// <summary>
		/// Gets or sets a value that specifies whether a command may contain multiple values delimited by a valid separator character.
		/// </summary>
		public bool AllowMultipleValues { get; set; }

		/// <summary>
		/// Gets or sets a value that specifies whether a command may consist of a single unit with no key or value.
		/// </summary>
		public bool AllowSwitches { get; set; }

		/// <summary>
		/// Gets or sets a value that specifies whether a command may be comprised solely of a combination of single letter commands.
		/// </summary>
		/// <example>
		/// The following is a normally valid approach to supplying commands, where -a and -b represent single letter commands:
		///		program -a -b
		///
		/// Setting this property to true will allow the commands to be combined as follows:
		///		program -ab
		/// </example>
		public bool AllowCombinedSwitches { get; set; }

		/// <summary>
		/// Copies all configuration details from the given rule set into the current rule set.
		/// </summary>
		/// <param name="ruleSet">The rule set to copy the configuration details from.</param>
		/// <exception cref="ArgumentNullException">Thrown when the given rule set is null.</exception>
		public void Copy(CommandLineRuleSet ruleSet)
		{
			VerificationProvider.VerifyNotNull(ruleSet, "ruleSet");

			// Copy the configuration details
			AllowCombinedSwitches = ruleSet.AllowCombinedSwitches;
			AllowMultipleValues = ruleSet.AllowMultipleValues;
			AllowSwitches = ruleSet.AllowSwitches;
		}
	}
}