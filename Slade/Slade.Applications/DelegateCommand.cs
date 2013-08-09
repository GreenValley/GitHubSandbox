using System;
using System.Windows.Input;

namespace Slade.Applications
{
    /// <summary>
    /// Provides an implementation of the <see cref="ICommand"/> interface to allow for custom
    /// execution of actions upon invocation of command data binding.
    /// </summary>
    public class DelegateCommand : DisposableObject, ICommand
    {
        /// <summary>
        /// Raised when the state of the command has been invalidated.
        /// </summary>
        public event EventHandler CanExecuteChanged;

        private readonly Action<object> mExecutionAction;
        private readonly Func<object, bool> mCanExecuteFunction;

        /// <summary>
        /// Initializes a new instance of the <see cref="DelegateCommand"/> class.
        /// </summary>
        /// <param name="executionAction">The action to invoke upon execution of the command.</param>
        /// <exception cref="ArgumentNullException">Thrown when the given execution action is null.</exception>
        public DelegateCommand(Action<object> executionAction)
            : this(executionAction, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DelegateCommand"/> class.
        /// </summary>
        /// <param name="executionAction">The action to invoke upon execution of the command.</param>
        /// <param name="canExecuteFunction">A function to invoke to perform custom validation of whether the command can be executed.</param>
        /// <exception cref="ArgumentNullException">Thrown when the given execution action is null.</exception>
        public DelegateCommand(Action<object> executionAction, Func<object, bool> canExecuteFunction)
        {
            VerificationProvider.VerifyNotNull(executionAction, "executionAction");

            mExecutionAction = executionAction;
            mCanExecuteFunction = canExecuteFunction ?? (x => true);

            // Hook up to the RequerySuggested event of the CommandManager
            // Note: By handling the RequerySuggested event of the CommandManager, we can manually invalidate the state
            //       of this command without having to bypass listeners through to a global invalidator.
            CommandManager.RequerySuggested += CommandManager_RequerySuggested;
        }

        /// <summary>
        /// Determines whether the command can be properly executed.
        /// </summary>
        /// <param name="parameter">A parameter passed through to the command from the binding.</param>
        /// <returns>True if the command can be properly executed; false otherwise.</returns>
        public bool CanExecute(object parameter)
        {
            return mCanExecuteFunction.Invoke(parameter);
        }

        /// <summary>
        /// Performs actual execution of the command by invoking the contains action.
        /// </summary>
        /// <param name="parameter">A parameter passed through to the command from the binding.</param>
        public void Execute(object parameter)
        {
            mExecutionAction.Invoke(parameter);
        }

        /// <summary>
        /// Invalidates the state of the command and raises the <see cref="CanExecuteChanged"/> event.
        /// </summary>
        public void Invalidate()
        {
            CanExecuteChanged.SafeInvoke(this);
        }

        private void CommandManager_RequerySuggested(object sender, EventArgs e)
        {
            Invalidate();
        }

        /// <summary>
        /// Performs cleanup of all event resources contained within the command.
        /// </summary>
        /// <param name="disposing">True if the proper disposal track is being undertaken.</param>
        protected override void Dispose(bool disposing)
        {
            // Unhook from the CommandManager's RequerySuggested event so the command can be freed for garbage collection
            CommandManager.RequerySuggested -= CommandManager_RequerySuggested;

            base.Dispose(disposing);
        }
    }
}
