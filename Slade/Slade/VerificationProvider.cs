using System;

namespace Slade
{
    /// <summary>
    /// Contains static methods used to perform simple verification on method parameters
    /// and throw appropriate exceptions when conditions are not satisfied.
    /// </summary>
    public static class VerificationProvider
    {
        /// <summary>
        /// Checks that the given parameter is not a null reference and throws an instance
        /// of the <see cref="ArgumentNullException" /> class if it is.
        /// </summary>
        /// <param name="parameter">The parameter value to be checked.</param>
        /// <param name="parameterName">The name of the parameter that is being checked.</param>
        public static void VerifyNotNull(object parameter, string parameterName)
        {
            if (parameter == null)
            {
                throw new ArgumentNullException(parameterName);
            }
        }
    }
}