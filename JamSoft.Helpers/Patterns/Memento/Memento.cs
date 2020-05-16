namespace JamSoft.Helpers.Patterns.Memento
{
    /// <summary>
    /// A simple object for storing state
    /// </summary>
    public abstract class Memento : IMemento
    {
        /// <summary>
        /// The stored state
        /// </summary>
        private object _state;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="state"></param>
        protected Memento(object state)
        {
            _state = state;
        }

        /// <summary>
        /// The the stored state from this instance
        /// </summary>
        /// <returns></returns>
        public virtual object GetState()
        {
            return _state;
        }
    }
}