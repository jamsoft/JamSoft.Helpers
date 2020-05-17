using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace JamSoft.Helpers.Patterns.Mvvm
{
    /// <summary>
    /// A sortable ObservableCollection&lt;T&gt; class with AddRange features
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="System.Collections.ObjectModel.ObservableCollection{T}" />
    public class SuperObservableCollection<T> : ObservableCollection<T>
    {
        /// <summary>
        /// The suppress notification flag when set to true prevents any change notifications.
        /// </summary>
        public bool SuppressNotification { private get; set; }

        /// <summary>Initialises a new instance of the <see cref="SuperObservableCollection{T}"/> class.</summary>
        public SuperObservableCollection()
        {
        }

        /// <summary>Initialises a new instance of the <see cref="SuperObservableCollection{T}"/> class.</summary>
        /// <param name="coll">The coll.</param>
        public SuperObservableCollection(IEnumerable<T> coll)
        {
            AddRange(coll);
        }

        /// <summary>
        /// The on collection changed.
        /// </summary>
        /// <param name="e">
        /// The e.
        /// </param>
        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            if (!SuppressNotification)
            {
                base.OnCollectionChanged(e);
            }
        }

        /// <summary>
        /// The add range.
        /// </summary>
        /// <param name="list">
        /// The list.
        /// </param>
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
        /// The sort.
        /// </summary>
        public void Sort()
        {
            Sort(Comparer<T>.Default);
        }

        /// <summary>
        /// The sort.
        /// </summary>
        /// <param name="comparer">
        /// The comparer.
        /// </param>
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
