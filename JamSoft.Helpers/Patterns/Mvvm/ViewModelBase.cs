using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace JamSoft.Helpers.Patterns.Mvvm
{
    /// <summary>
    /// An implementation of a Base View Model for use in Mvvm
    /// </summary>
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        private bool _isEditable;

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Sets the property value only if the provided value is different from the stored value.<para />
        /// If set to a new value, then the <seealso cref="System.ComponentModel.INotifyPropertyChanged" /> event will be fired
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="storage">The storage.</param>
        /// <param name="value">The value.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns></returns>
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
        /// Raises the <see cref="E:PropertyChanged" /> event. Allows the use of a specfic PropertyChangedEventArgs object.  
        /// Is the most performant implementation
        /// </summary>
        /// <param name="prop">The <see cref="PropertyChangedEventArgs"/> instance containing the event data.</param>
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs prop)
        {
            PropertyChanged?.Invoke(this, prop);
        }

        /// <summary>
        /// Fires the property changed event using either the <seealso cref="CallerMemberNameAttribute" /> or the provded string property name.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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
    }
}
