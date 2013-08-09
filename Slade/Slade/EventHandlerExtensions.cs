using System;

namespace Slade
{
    /// <summary>
    /// Extends the functionality provided by the <see cref="EventHandler"/> and <see cref="EventHandler{TEventArgs}"/> classes.
    /// </summary>
    public static class EventHandlerExtensions
    {
        /// <summary>
        /// Invokes the event handler using the given parameters only if listeners are attached.
        /// </summary>
        /// <param name="handler">A generic event handler to be invoked.</param>
        /// <param name="sender">The source of the event handler invocation.</param>
        public static void SafeInvoke(this EventHandler handler, object sender)
        {
            SafeInvoke(handler, sender, EventArgs.Empty);
        }

        /// <summary>
        /// Invokes the event handler using the given parameters only if listeners are attached.
        /// </summary>
        /// <param name="handler">A generic event handler to be invoked.</param>
        /// <param name="sender">The source of the event handler invocation.</param>
        /// <param name="eventArguments">Generic arguments to supply to the event handler invocation.</param>
        public static void SafeInvoke(this EventHandler handler, object sender, EventArgs eventArguments)
        {
            if (handler != null)
            {
                handler.Invoke(sender, eventArguments);
            }
        }

        /// <summary>
        /// Invokes the event handler using the given parameters only if listeners are attached.
        /// </summary>
        /// <typeparam name="TEventArgs">The type of event arguments expected by the event handler.</typeparam>
        /// <param name="handler">A strongly-typed event handler to be invoked.</param>
        /// <param name="sender">The source of the event handler invocation.</param>
        /// <remarks>
        /// <para>
        /// The event arguments will be automatically instantiated for the invocation of the event handler.
        /// </para>
        /// </remarks>
        public static void SafeInvoke<TEventArgs>(this EventHandler<TEventArgs> handler, object sender)
            where TEventArgs : EventArgs, new()
        {
            SafeInvoke<TEventArgs>(handler, sender, new TEventArgs());
        }

        /// <summary>
        /// Invokes the event handler using the given parameters only if listeners are attached.
        /// </summary>
        /// <typeparam name="TEventArgs">The type of event arguments expected by the event handler.</typeparam>
        /// <param name="handler">A strongly-typed event handler to be invoked.</param>
        /// <param name="sender">The source of the event handler invocation.</param>
        /// <param name="eventArguments">Strongly-typed arguments to supply to the event handler invocation.</param>
        public static void SafeInvoke<TEventArgs>(this EventHandler<TEventArgs> handler, object sender, TEventArgs eventArguments)
            where TEventArgs : EventArgs
        {
            if (handler != null)
            {
                handler.Invoke(sender, eventArguments);
            }
        }
    }
}