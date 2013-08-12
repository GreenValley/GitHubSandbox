using System;

namespace Slade.Applications.Converters
{
    /// <summary>
    /// Provides a base class for all types of value converters.
    /// </summary>
    public abstract class ConverterBase
    {
        /// <summary>
        /// Determines whether the given parameter value specifies that the conversion result should be reversed, if possible.
        /// </summary>
        /// <param name="parameter">The parameter object passed through to the conversion process.</param>
        /// <returns>True if the given parameter denotes a reverse conversion result requirement.</returns>
        protected bool IsReversed(object parameter)
        {
            string result = GetAsString(parameter);
            return "reverse".Equals(result, StringComparison.InvariantCultureIgnoreCase);
        }

        /// <summary>
        /// Attempts to get a string representation of the given converter parameter.
        /// </summary>
        /// <param name="parameter">The parameter object passed through to the conversion process.</param>
        /// <returns>A string representation of the given converter parameter.</returns>
        protected static string GetAsString(object parameter)
        {
            string result = parameter as string;

            if (String.IsNullOrWhiteSpace(result) && parameter != null)
            {
                // The given parameter isn't natively typed as a string, but has a value, so we can represent it as a string
                result = parameter.ToString();
            }

            return result;
        }

        /// <summary>
        /// Attempts to extract a boolean value from the given converter parameter.
        /// </summary>
        /// <param name="parameter">The parameter object passed through to the conversion process.</param>
        /// <returns>True if the given parameter represents a positive boolean value; false otherwise.</returns>
        protected static bool GetAsBoolean(object parameter)
        {
            bool result;

            if (parameter is bool)
            {
                result = (bool)parameter;
            }
            else
            {
                // We don't have a native boolean type, so attempt to parse from a string form
                Boolean.TryParse(GetAsString(parameter), out result);
            }

            return result;
        }
    }
}