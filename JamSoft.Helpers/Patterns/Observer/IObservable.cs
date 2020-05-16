namespace JamSoft.Helpers.Patterns.Observer
{
    /// <summary>
    /// A contract for observable objects to implement
    /// </summary>
    public interface IObservable
    {
        /// <summary>
        /// Attach an observer to the observable.
        /// </summary>
        /// <param name="observer">The observer.</param>
        void Attach(IObserver observer);

        /// <summary>
        /// Detach an observer from the observable.
        /// </summary>
        /// <param name="observer">The observer.</param>
        void Detach(IObserver observer);

        /// <summary>
        /// Notify all observers about an event.
        /// </summary>
        void Notify();
    }
}