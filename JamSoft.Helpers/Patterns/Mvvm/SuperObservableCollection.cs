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
        public bool SuppressNotification { private get; set; }

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
            if (!SuppressNotification)
            {
                base.OnCollectionChanged(e);
            }
        }

        /// <summary>
        /// Adds the provided collection of <seealso cref="IEnumerable{T}"></seealso> and surpresses collection changed events until all objects have been added.<para />
        /// Once all objects have been added a single <seealso cref="NotifyCollectionChangedEventArgs"></seealso> is created and the OnCollectionChanged event is fired
        /// </summary>
        /// <param name="list">The collection of obejct to add</param>
        public void AddRange(IEnumerable<T> list)
        {
            SuppressNotification = true;

            foreach (T item in list)
            {
                Add(item);
            }

            SuppressNotification = false;
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        /// <summary>
        /// Sorts the collection using the the default <seealso cref="Comparer{T}"></seealso> comparer
        /// </summary>
        /// <seealso cref="IComparer{T}"></seealso>
        public void Sort()
        {
            Sort(Comparer<T>.Default);
        }

        /// <summary>
        /// Sorts the collection using the provided <seealso cref="IComparer{T}"></seealso>instance
        /// </summary>
        /// <param name="comparer">The comparer.</param>
        /// <seealso cref="IComparer{T}"></seealso>
        public void Sort(IComparer<T> comparer)
        {
            int i;
            for (i = 1; i < Count; i++)
            {
                T index = this[i];
                int j = i;
                while ((j > 0) && (comparer.Compare(this[j - 1], index) == 1))
                {
                    this[j] = this[j - 1];
                    j = j - 1;
                }
                this[j] = index;
            }
        }
    }
}
