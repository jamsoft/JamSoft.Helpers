namespace JamSoft.Helpers.Patterns.Memento
{
    /// <summary>
    /// Allows access to a stored state
    /// </summary>
    public interface IMemento
    {
        /// <summary>
        /// Gets the state.
        /// </summary>
        /// <returns></returns>
        object GetState();
    }
}