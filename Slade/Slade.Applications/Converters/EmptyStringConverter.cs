using System;

namespace Slade.Applications.Converters
{
    /// <summary>
    /// Provides support for determining whether a given string contains no value.
    /// </summary>
    public class EmptyStringConverter : ValueConverterBase
    {
        private static EmptyStringConverter sInstance;

        /// <summary>
        /// Provides access to the shared instance of the <see cref="EmptyStringConverter"/> class.
        /// </summary>
        public static EmptyStringConverter Instance
        {
            get { return sInstance ?? (sInstance = new EmptyStringConverter()); }
        }

        /// <summary>
        /// Determines whether the given value is a empty string value and returns a boolean value to represent that.
        /// </summary>
        /// <param name="value">The value to check for an empty string.</param>
        /// <param name="parameter">An optional converter that can be used to reverse the boolean result.</param>
        /// <returns>True if the given value is an empty string; false otherwise.</returns>
        protected override object Convert(object value, object parameter)
        {
            string stringValue = GetAsString(value);
            bool result = String.IsNullOrWhiteSpace(stringValue);

            return IsReversed(parameter) ? !result : result;
        }
    }
}