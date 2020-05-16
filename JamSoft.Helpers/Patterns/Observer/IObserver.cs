namespace JamSoft.Helpers.Patterns.Observer
{
    /// <summary>
    /// A contract for an observer to implement for change notifications
    /// </summary>
    public interface IObserver
    {
        /// <summary>
        /// Receive update from a observable
        /// </summary>
        /// <param name="observable">The observable.</param>
        void Update(IObservable observable);
    }
}