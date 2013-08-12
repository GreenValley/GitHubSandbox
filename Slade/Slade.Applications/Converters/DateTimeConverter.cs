using System;

namespace Slade.Applications.Converters
{
    /// <summary>
    /// Provides support for converting between string and <see cref="DateTime"/> values.
    /// </summary>
    public class DateTimeConverter : ValueConverterBase
    {
        private const string DEFAULT_DATE_TIME_FORMAT = "dd/MM/yyyy hh:mm";

        private static DateTimeConverter sInstance;

        /// <summary>
        /// Provides access to the shared instance of the <see cref="DateTimeConverter"/> class.
        /// </summary>
        public static DateTimeConverter Instance
        {
            get { return sInstance ?? (sInstance = new DateTimeConverter()); }
        }

        /// <summary>
        /// Converts the given <see cref="DateTime"/> value into a formatted string value.
        /// </summary>
        /// <param name="value">The <see cref="DateTime"/> value to be formatted as a string.</param>
        /// <param name="parameter">An optional parameter that can be used to specify a custom date/time string format.</param>
        /// <returns>A string representation of the given <see cref="DateTime"/> value.</returns>
        /// <remarks>
        /// <para>
        /// When no date/time format is specified, the default format "dd/MM/yyyy hh:mm" will be used instead.
        /// </para>
        /// </remarks>
        protected override object Convert(object value, object parameter)
        {
            var dateTime = (DateTime)value;
            string format = GetDateTimeFormat(parameter);

            // Format the date/time value as a string using the selected format
            return dateTime.ToString(format, Culture);
        }

        /// <summary>
        /// Converts the string value into a <see cref="DateTime"/> value by parsing using a known format.
        /// </summary>
        /// <param name="value">The string value to be parsed into a <see cref="DateTime"/> format.</param>
        /// <param name="parameter">An optional parameter that can be used to specify the format of the given string value,
        /// which will be used during the value parsing operation.</param>
        /// <returns>A <see cref="DateTime"/> value parsed from the given string value.</returns>
        /// <remarks>
        /// <para>
        /// When no date/time format is specified, the default format "dd/MM/yyyy hh:mm" will be used instead.
        /// </para>
        /// </remarks>
        protected override object ConvertBack(object value, object parameter)
        {
            string dateTime = (string)value;
            string format = GetDateTimeFormat(parameter);

            // Attempt an exact parse of the date/time string value using the selected format
            return DateTime.ParseExact(dateTime, format, Culture);
        }

        private string GetDateTimeFormat(object parameter)
        {
            // Determine what format to use
            string format = GetAsString(parameter);
            if (String.IsNullOrWhiteSpace(format))
            {
                format = DEFAULT_DATE_TIME_FORMAT;
            }

            return format;
        }
    }
}
