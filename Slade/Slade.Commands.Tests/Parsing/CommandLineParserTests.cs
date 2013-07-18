using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Slade.Commands.Tests.Parsing
{
	[TestClass]
	public class CommandLineParserTests
	{
		/// <summary>
		/// Configuration:
		///		AllowSwitches		True
		///		Prefixes			DoubleHyphen
		///
		/// Input:
		///		/switch
		///
		/// Output:
		///		1 Command
		///			Key				"switch"
		///			HasValue		False
		/// </summary>
		[TestMethod]
		public void Parse_AllowSwitchesDoubleHyphenPrefix_OneSwitchCommand()
		{
		}

		/// <summary>
		/// Configuration:
		///		Prefixes			ForwardSlash
		///		Separators			Equals
		///
		/// Input:
		///		/key=value
		///
		/// Output:
		///		1 Command
		///			Key				"key"
		///			Value			"value"
		///			HasValue		True
		/// </summary>
		[TestMethod]
		public void Parse_ForwardSlashPrefixEqualsSeparator_OneCommandWithKeyAndValue()
		{
		}

		/// <summary>
		/// Configuration:
		///		AllowMultipleValues	True
		///		Prefixes			ForwardSlash
		///		Separators			Colon
		///
		/// Input:
		///		/key:val1;val2;val3
		///
		/// Output:
		///		1 Command
		///			Key				"key"
		///			Value			["val1", "val2", "val3"]
		///			HasValue		True
		/// </summary>
		[TestMethod]
		public void Parse_AllowMultipleValuesForwardSlashPrefixColonSeparator_OneCommandWithKeyAndThreeValues()
		{
		}

		/// <summary>
		/// Configuration:
		///		AllowCombinedValues	True
		///		Prefixes			SingleHyphen
		///
		/// Input:
		///		-ab
		///
		/// Output:
		///		2 Commands
		///			Key				"a"
		///			HasValue		False
		///			Key				"b"
		///			HasValue		False
		/// </summary>
		[TestMethod]
		public void Parse_AllowCombinedValuesSingleHyphenPrefix_TwoCommands()
		{
		}

		/// <summary>
		/// Configuration:
		///		AllowCombinedValues	True
		///		Prefixes			SingleHyphen
		///
		/// Input:
		///		-a -b
		///
		/// Output:
		///		2 Commands
		///			Key				"a"
		///			HasValue		False
		///			Key				"b"
		///			HasValue		False
		/// </summary>
		[TestMethod]
		public void Parse_SingleHyphenPrefix_TwoCommands()
		{
		}
	}
}