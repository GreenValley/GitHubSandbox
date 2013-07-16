using System;

namespace Slade.Commands.Parsing
{
	/// <summary>
	/// Represents a combinable set of valid characters that may be used as a separator to split
	/// command keys from command values.
	/// </summary>
	[Flags]
	public enum CommandSeparators
	{
		/// <summary>
		/// Denotes no command separators are valid.
		/// </summary>
		None = 0,

		/// <summary>
		/// Represents
		/// </summary>
		Equals = 1 << 1,

		Colon = 1 << 2
	}
}