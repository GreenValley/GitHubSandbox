namespace Slade.Applications.Converters
{
    /// <summary>
    /// Ensures the convertible values are in boolean form where possible.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This converter supports the "reverse" parameter for both conversions.
    /// </para>
    /// </remarks>
    public class BooleanConverter : ValueConverterBase
    {
        private static BooleanConverter sInstance;

        /// <summary>
        /// Provides access to the shared instance of the <see cref="BooleanConverter"/> class.
        /// </summary>
        public static BooleanConverter Instance
        {
            get { return sInstance ?? (sInstance = new BooleanConverter()); }
        }

        /// <summary>
        /// Ensures the given value is in a boolean form.
        /// </summary>
        /// <param name="value">The value to convert to a boolean value.</param>
        /// <param name="parameter">An optional converter parameter that may be used to reverse the result of the conversion.</param>
        /// <returns>True if the given value is a true boolean value; otherwise false.</returns>
        protected override object Convert(object value, object parameter)
        {
            return ConvertCore(value, parameter);
        }

        /// <summary>
        /// Ensures the given value is in a boolean form.
        /// </summary>
        /// <param name="value">The value to convert to a boolean value.</param>
        /// <param name="parameter">An optional converter parameter that may be used to reverse the result of the conversion.</param>
        /// <returns>True if the given value is a true boolean value; otherwise false.</returns>
        protected override object ConvertBack(object value, object parameter)
        {
            return ConvertCore(value, parameter);
        }

        private object ConvertCore(object value, object parameter)
        {
            bool booleanValue = GetAsBoolean(value);
            return IsReversed(parameter) ? !booleanValue : booleanValue;
        }
    }
}