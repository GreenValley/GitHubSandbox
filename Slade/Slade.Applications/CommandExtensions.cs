using System.Windows.Input;

namespace Slade.Applications
{
    /// <summary>
    /// Provides support for working with implementations of the <see cref="ICommand"/> interface.
    /// </summary>
    public static class CommandExtensions
    {
        /// <summary>
        /// Performs execution on the command if it does not contain a null reference and can be validly executed.
        /// </summary>
        /// <param name="command">The command to be executed.</param>
        /// <param name="parameter">An optional parameter to pass through to the command execution.</param>
        public static void SafeExecute(this ICommand command, object parameter)
        {
            if (command != null && command.CanExecute(parameter))
            {
                command.Execute(parameter);
            }
        }
    }
}