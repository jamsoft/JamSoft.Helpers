using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace JamSoft.Helpers.Patterns.Mvvm
{
    /// <summary>
    /// A sortable ObservableCollection&lt;T&gt; class with AddRange features
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SuperObservableCollection<T> : ObservableCollection<T>
    {
        /// <summary>
        /// The suppress notification flag when set to true prevents any change notifications.
        /// </summary>
        private bool _suppressNotification { get; set; }

        /// <summary>Initialises a new instance of the <see cref="SuperObservableCollection{T}"/> class.</summary>
        /// <seealso cref="System.Collections.ObjectModel.ObservableCollection{T}" />
        public SuperObservableCollection()
        {
        }

        /// <summary>Initialises a new instance of the <see cref="SuperObservableCollection{T}"/> class.</summary>
        /// <param name="coll">The coll.</param>
        /// <seealso cref="System.Collections.ObjectModel.ObservableCollection{T}" />
        public SuperObservableCollection(IEnumerable<T> coll)
        {
            AddRange(coll);
        }

        /// <summary>
        /// The on collection changed handler.
        /// </summary>
        /// <param name="e">The event args</param>
        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            if (!_suppressNotification)
            {
                base.OnCollectionChanged(e);
            }
        }

        /// <summary>
        /// Adds the provided collection of <seealso cref="IEnumerable{T}"></seealso> and suppresses collection changed events until all objects have been added, then optionally fires one event.<para />
        /// Once all objects have been added a single <seealso cref="NotifyCollectionChangedEventArgs"></seealso> is created and the OnCollectionChanged event is fired
        /// </summary>
        /// <param name="list">The collection of object to add</param>
        /// <param name="suppressNotifications">A flag to suppress change notifications</param>/// 
        /// <param name="notifiyOnceAllAdded"></param>
        public void AddRange(IEnumerable<T> list, bool suppressNotifications = true, bool notifiyOnceAllAdded = true)
        {
            foreach (var item in list)
            {
                Add(item, suppressNotifications);
            }

            _suppressNotification = false;

            if (notifiyOnceAllAdded)
            {
                OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            }
        }

        /// <summary>
        /// Sorts the collection using the default <seealso cref="Comparer{T}"></seealso> comparer
        /// </summary>
        /// <param name="suppressNotifications">A flag to suppress change notifications</param>
        /// <seealso cref="IComparer{T}"></seealso>
        public void Sort(bool suppressNotifications = false)
        {
            Sort(Comparer<T>.Default, suppressNotifications);
        }

        /// <summary>
        /// Adds an item to the collection and optionally suppresses the collection changed events
        /// </summary>
        /// <param name="item">The item to add to the collection</param>
        /// <param name="suppressNotifications">A flag to suppress change notifications</param>
        /// <seealso cref="IComparer{T}"></seealso>
        public void Add(T item, bool suppressNotifications = false)
        {
            _suppressNotification = suppressNotifications;
            base.Add(item);
            _suppressNotification = false;
        }

        /// <summary>
        /// Sorts the collection using the provided <seealso cref="IComparer{T}"></seealso>instance
        /// </summary>
        /// <param name="comparer">The comparer.</param>
        /// <param name="suppressNotifications">A flag to suppress change notifications</param>
        /// <seealso cref="IComparer{T}"></seealso>
        public void Sort(IComparer<T> comparer, bool suppressNotifications = false)
        {
            _suppressNotification = suppressNotifications;
            int i;

            for (i = 1; i < Count; i++)
            {
                var index = this[i];
                var j = i;
                while ((j > 0) && (comparer.Compare(this[j - 1], index) == 1))
                {
                    this[j] = this[j - 1];
                    j -= 1;
                }
                this[j] = index;
            }

            _suppressNotification = false;
        }
    }
}
