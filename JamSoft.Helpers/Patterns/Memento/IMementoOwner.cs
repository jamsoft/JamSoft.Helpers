namespace JamSoft.Helpers.Patterns.Memento
{
    /// <summary>
    /// An interface to implement mementos (Undo etc)
    /// </summary>
    public interface IMementoOwner
    {
        /// <summary>
        /// Save a new memento instance
        /// </summary>
        /// <returns></returns>
        IMemento Save();

        /// <summary>
        /// Restore a memento instance
        /// </summary>
        /// <param name="memento"></param>
        void Restore(IMemento memento);
    }
}