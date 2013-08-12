using System;
using System.Linq;

namespace Slade.Applications.Converters
{
    /// <summary>
    /// A converter that supports the aggregation of multiple boolean values into a combined
    /// boolean value controlled by the logical "Or" operation.
    /// </summary>
    public class OrOperator : MultiValueConverterBase
    {
        private static OrOperator sInstance;

        /// <summary>
        /// Provides access to the shared instance of the <see cref="OrOperator"/> class.
        /// </summary>
        public static OrOperator Instance
        {
            get { return sInstance ?? (sInstance = new OrOperator()); }
        }

        /// <summary>
        /// Combines the given boolean values to a single boolean result as determined by the
        /// logical "Or" operation.
        /// </summary>
        /// <param name="values">The boolean values to be aggregated.</param>
        /// <param name="targetType">The target type of the conversion process.</param>
        /// <param name="parameter">An optional parameter that may be used to reverse the
        /// result of the conversion process.</param>
        /// <returns>True if any of the given boolean values are true; otherwise false.</returns>
        protected override object Convert(object[] values, Type targetType, object parameter)
        {
            // The result should evaluate to true if any of the given values are true boolean values
            bool result = values.Any(x => x is bool && (bool)x);
            return IsReversed(parameter) ? !result : result;
        }
    }
}