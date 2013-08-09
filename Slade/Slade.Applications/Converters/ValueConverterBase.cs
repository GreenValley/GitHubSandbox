using System;
using System.Globalization;
using System.Windows.Data;

namespace Slade.Applications.Converters
{
    /// <summary>
    /// Provides an abstract implementation of the <see cref="IValueConverter"/> interface.
    /// </summary>
    public abstract class ValueConverterBase : IValueConverter
    {
        /// <summary>
        /// Gets the expected resulting type of the current conversation pass.
        /// </summary>
        protected Type TargetType { get; private set; }

        /// <summary>
        /// Gets the culture information provided to the current conversation pass.
        /// </summary>
        protected CultureInfo Culture { get; private set; }

        /// <summary>
        /// Converts the given value into the expected resulting type.
        /// </summary>
        /// <param name="value">The value to undergo the conversion process.</param>
        /// <param name="targetType">The expected resulting type of the conversion.</param>
        /// <param name="parameter">An optional parameter that may affect the conversion process.</param>
        /// <param name="culture">Information pertaining to the culture required for use under the conversion.</param>
        /// <returns>The resulting value from the conversion process.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            TargetType = targetType;
            Culture = culture;

            return Convert(value, parameter);
        }

        /// <summary>
        /// Converts the given value into the expected resulting type.
        /// </summary>
        /// <param name="value">The value to undergo the conversion process.</param>
        /// <param name="parameter">An optional parameter that may affect the conversion process.</param>
        /// <returns>The resulting value from the conversion process.</returns>
        /// <exception cref="NotSupportedException">Thrown when the converter does not support this direction of conversion.</exception>
        protected virtual object Convert(object value, object parameter)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Converts the given value back to the original type.
        /// </summary>
        /// <param name="value">The value to undergo the backward conversion process.</param>
        /// <param name="targetType">The type of the original value before conversion.</param>
        /// <param name="parameter">An optional parameter that may affect the backward conversion process.</param>
        /// <param name="culture">Information pertaining to the culture required for use under the backward conversion.</param>
        /// <returns>The resulting value from the conversion process.</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Converts the given value back to the original type.
        /// </summary>
        /// <param name="value">The value to undergo the backward conversion process.</param>
        /// <param name="parameter">An optional parameter that may affect the backward conversion process.</param>
        /// <exception cref="NotSupportedException">Thrown when the converter does not support this direction of conversion.</exception>
        protected virtual object ConvertBack(object value, object parameter)
        {
            throw new NotSupportedException();
        }
    }
}