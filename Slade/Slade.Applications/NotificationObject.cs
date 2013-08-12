using System;
using System.ComponentModel;

namespace Slade.Applications
{
    /// <summary>
    /// Represents an object that notifies when data-bound property values change by implementing the
    /// <see cref="INotifyPropertyChanged"/> interface and mechanism.
    /// </summary>
    public abstract class NotificationObject : INotifyPropertyChanged
    {
        /// <summary>
        /// Raised when the value of a property of the object changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// Changes the value of the referenced variable and notifies any listeners of the property change.
        /// </summary>
        /// <typeparam name="TValue">The type of value stored in the referenced variable.</typeparam>
        /// <param name="variable">A reference to the variable that is to be updated.</param>
        /// <param name="value">The new value to be given to the referenced variable.</param>
        /// <param name="propertyNames">An optional collection of names of properties that are affected
        /// by the change in the value of the referenced variable.</param>
        /// <returns>True if the value was changed and the notification propagated.</returns>
        public bool SetValue<TValue>(ref TValue variable, TValue value, params string[] propertyNames)
        {
            // Run a simple check to see if the value has actually changed
            if (Object.Equals(variable, value))
            {
                return false;
            }

            // Update the value of the referenced variable and notify listeners of affected properties
            variable = value;

            OnPropertyChanged(propertyNames);

            return true;
        }

        /// <summary>
        /// Triggers the mechanism of notifying interested parties that the values of the specified properties have been changed.
        /// </summary>
        /// <param name="propertyNames">A collection of names of properties that have had their values changed.</param>
        /// <remarks>
        /// <para>
        /// This method raises the <see cref="PropertyChanged"/> event for each of the given property names, so if the method
        /// is overridden, then a call should be made back to the base implementation to prevent loss of notification functionality.
        /// </para>
        /// </remarks>
        protected virtual void OnPropertyChanged(params string[] propertyNames)
        {
            var handler = PropertyChanged;
            if (handler == null)
            {
                // We have no listeners, so there is no point raising any event
                return;
            }

            foreach (string propertyName in propertyNames)
            {
                // Only raise the appropriate event if the property name is a valid string
                if (!String.IsNullOrWhiteSpace(propertyName))
                {
                    handler.Invoke(this, new PropertyChangedEventArgs(propertyName));
                }
            }
        }
    }
}