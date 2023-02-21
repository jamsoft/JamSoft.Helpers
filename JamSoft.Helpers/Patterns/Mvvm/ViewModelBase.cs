using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace JamSoft.Helpers.Patterns.Mvvm
{
    /// <summary>
    /// An implementation of a Base View Model for use in MVVM
    /// </summary>
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        private bool _isEditable;
        private bool _isBusy;

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        /// <see cref="System.ComponentModel.INotifyPropertyChanged" />
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        /// <summary>
        /// Sets the property value only if the provided value is different from the stored value.<para />
        /// If set to a new value, then the <seealso cref="System.ComponentModel.INotifyPropertyChanged" /> event will be fired
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="storage">The storage field.</param>
        /// <param name="value">The value.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns>True when value changes</returns>
        protected virtual bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = "")
        {
            if (EqualityComparer<T>.Default.Equals(storage, value))
            {
                return false;
            }

            storage = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        /// <summary>
        /// Raises the <see cref="E:PropertyChanged" /> event. Allows the use of a specific PropertyChangedEventArgs object.  
        /// Is the most performant implementation
        /// </summary>
        /// <param name="prop">The <see cref="PropertyChangedEventArgs"/> instance containing the event data.</param>
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs prop)
        {
            PropertyChanged(this, prop);
        }

        /// <summary>
        /// Fires the property changed event using either the <seealso cref="CallerMemberNameAttribute" /> or the provided string property name.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is editable.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is editable; otherwise, <c>false</c>.
        /// </value>
        public bool IsEditable
        {
            get => _isEditable;
            set => SetProperty(ref _isEditable, value);
        }

        /// <summary>
        /// Gets or sets the busy state for the view model instance
        /// </summary>
        public bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value);
        }
    }
}
