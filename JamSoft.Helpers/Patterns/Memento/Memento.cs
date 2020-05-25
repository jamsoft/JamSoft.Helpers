namespace JamSoft.Helpers.Patterns.Memento
{
    /// <summary>
    /// A simple object for storing state
    /// </summary>
    public abstract class Memento : IMemento
    {
        /// <summary>
        /// The stored state object instance
        /// </summary>
        protected readonly object State;

        /// <summary>
        /// Initializes a new instance of the <see cref="Memento"/> class.
        /// </summary>
        /// <param name="state">The state.</param>
        protected Memento(object state)
        {
            State = state;
        }

        /// <summary>
        /// Gets the stored state from this instance
        /// </summary>
        /// <returns></returns>
        public virtual object GetState()
        {
            return State;
        }
    }
}