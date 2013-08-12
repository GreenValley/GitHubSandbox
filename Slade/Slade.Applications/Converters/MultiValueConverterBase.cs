using System;
using System.Globalization;
using System.Windows.Data;

namespace Slade.Applications.Converters
{
    /// <summary>
    /// Provides an abstract implementation of the <see cref="IMultiValueConverter"/> interface.
    /// </summary>
    public abstract class MultiValueConverterBase : ConverterBase, IMultiValueConverter
    {
        /// <summary>
        /// Gets the culture information provided to the current conversation pass.
        /// </summary>
        protected CultureInfo Culture { get; private set; }

        /// <summary>
        /// Converts the given values into the expecting resulting type.
        /// </summary>
        /// <param name="values">A collection of values to undergo the conversion process.</param>
        /// <param name="targetType">The expected resulting type of the conversion.</param>
        /// <param name="parameter">An optional parameter that may affect the conversion process.</param>
        /// <param name="culture">Information pertaining to the culture required for use under the conversion.</param>
        /// <returns>The resulting value from the conversion process, which is an aggregate of the input values.</returns>
        /// <exception cref="NotSupportedException">Thrown when the converter does not support this direction of conversion.</exception>
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            Culture = culture;

            return Convert(values, targetType, parameter);
        }

        /// <summary>
        /// Converts the given values into the expecting resulting type.
        /// </summary>
        /// <param name="values">A collection of values to undergo the conversion process.</param>
        /// <param name="targetType">The expected resulting type of the conversion.</param>
        /// <param name="parameter">An optional parameter that may affect the conversion process.</param>
        /// <returns>The resulting value from the conversion process, which is an aggregate of the input values.</returns>
        /// <exception cref="NotSupportedException">Thrown when the converter does not support this direction of conversion.</exception>
        protected virtual object Convert(object[] values, Type targetType, object parameter)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Converts the given value back to the original types.
        /// </summary>
        /// <param name="value">The value to undergo the backward conversion process.</param>
        /// <param name="targetTypes">The types of the original values before conversion.</param>
        /// <param name="parameter">An optional parameter that may affect the backward conversion process.</param>
        /// <param name="culture">Information pertaining to the culture required for use under the backward conversion.</param>
        /// <returns>The resulting values from the conversion process.</returns>
        /// <exception cref="NotSupportedException">Thrown when the converter does not support this direction of conversion.</exception>
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            Culture = culture;

            return ConvertBack(value, targetTypes, parameter);
        }

        /// <summary>
        /// Converts the given value back to the original types.
        /// </summary>
        /// <param name="value">The value to undergo the backward conversion process.</param>
        /// <param name="targetTypes">The types of the original values before conversion.</param>
        /// <param name="parameter">An optional parameter that may affect the backward conversion process.</param>
        /// <returns>The resulting values from the conversion process.</returns>
        /// <exception cref="NotSupportedException">Thrown when the converter does not support this direction of conversion.</exception>
        protected virtual object[] ConvertBack(object value, Type[] targetTypes, object parameter)
        {
            throw new NotSupportedException();
        }
    }
}