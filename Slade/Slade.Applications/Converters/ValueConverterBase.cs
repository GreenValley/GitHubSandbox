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
			bool result = false;

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