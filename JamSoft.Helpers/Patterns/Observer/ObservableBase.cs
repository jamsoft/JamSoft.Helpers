using System.Collections.Generic;

namespace JamSoft.Helpers.Patterns.Observer
{
    /// <summary>
    /// A base implementation of an observable object (Subject)
    /// </summary>
    /// <seealso cref="JamSoft.Helpers.Patterns.Observer.IObservable" />
    public abstract class ObservableBase : IObservable
    {
        /// <summary>
        /// The list of attached observers
        /// </summary>
        protected readonly IList<IObserver> Observers = new List<IObserver>();

        /// <summary>
        /// Attaches the specified observer.
        /// </summary>
        /// <param name="observer">The observer.</param>
        public virtual void Attach(IObserver observer)
        {
            Observers.Add(observer);
        }

        /// <summary>
        /// Detaches the specified observer.
        /// </summary>
        /// <param name="observer">The observer.</param>
        public virtual void Detach(IObserver observer)
        {
            Observers.Remove(observer);
        }

        /// <summary>
        /// Notifies all attached observers of a state change.
        /// </summary>
        public virtual void Notify()
        {
            foreach (var observer in Observers)
            {
                observer.Update(this);
            }
        }
    }
}