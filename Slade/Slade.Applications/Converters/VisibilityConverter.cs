using System.Windows;

namespace Slade.Applications.Converters
{
    /// <summary>
    /// Provides support for converting between boolean and <see cref="Visibility"/> values.
    /// </summary>
    public class VisibilityConverter : ValueConverterBase
    {
        private static VisibilityConverter sInstance;

        /// <summary>
        /// Provides access to the shared instance of the <see cref="VisibilityConverter"/> class.
        /// </summary>
        public static VisibilityConverter Instance
        {
            get { return sInstance ?? (sInstance = new VisibilityConverter()); }
        }

        /// <summary>
        /// Converts the given boolean value into a corresponding <see cref="Visibility"/> value.
        /// </summary>
        /// <param name="value">The boolean value to be converted.</param>
        /// <param name="parameter">An optional parameter that can be used to reverse the result of the conversion.</param>
        /// <returns><see cref="Visibility.Visible"/> if the given value is a true boolean; otherwise <see cref="Visibility.Collapsed"/>.</returns>
        protected override object Convert(object value, object parameter)
        {
            bool result = value is bool && (bool)value;

            if (IsReversed(parameter))
            {
                // Flip the switch on the result to reverse the value
                result ^= true;
            }

            return result ? Visibility.Visible : Visibility.Collapsed;
        }

        /// <summary>
        /// Converts the given <see cref="Visibility"/> value into a corresponding boolean value.
        /// </summary>
        /// <param name="value">The <see cref="Visibility"/> value to be converted.</param>
        /// <param name="parameter">An optional parameter that can be used to reverse the result of the conversion.</param>
        /// <returns>True if the given value is <see cref="Visibility.Visible"/>; otherwise false.</returns>
        protected override object ConvertBack(object value, object parameter)
        {
            bool visible = value is Visibility && (Visibility)value == Visibility.Visible;

            if (IsReversed(parameter))
            {
                // Flip the switch on the result to reverse the value
                visible ^= true;
            }

            return visible;
        }
    }
}