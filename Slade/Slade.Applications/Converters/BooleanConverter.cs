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
		/// <summary>
		/// Ensures the given value is in a boolean form.
		/// </summary>
		/// <param name="value">The value to convert to a boolean value.</param>
		/// <param name="parameter">An optional converter parameter that may </param>
		/// <returns></returns>
		protected override object Convert(object value, object parameter)
		{
			return ConvertCore(value, parameter);
		}

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