using System.ComponentModel;
using JamSoft.Helpers.Patterns.Mvvm;

namespace JamSoft.Helpers.Tests.Patterns
{
    internal sealed class MyTestViewModel : ViewModelBase
    {
        private string _callsSetProperty;
        private string _callsSetPropertyWithName;
        private string _callsOnPropertyChanged;
        private string _callsOnPropertyChangedWithName;
        private string _callsOnPropertyChangedWithEventArgs;

        public string CallsSetProperty
        {
            get => _callsSetProperty;
            set => SetProperty(ref _callsSetProperty, value);
        }

        public string CallsSetPropertyWithName
        {
            get => _callsSetPropertyWithName;
            set => SetProperty(ref _callsSetPropertyWithName, value, "DifferentName");
        }

        public string CallsOnPropertyChanged
        {
            get { return _callsOnPropertyChanged; }
            set
            {
                _callsOnPropertyChanged = value;
                OnPropertyChanged();
            }
        }

        public string CallsOnPropertyChangedWithName
        {
            get { return _callsOnPropertyChangedWithName; }
            set
            {
                _callsOnPropertyChangedWithName = value;
                OnPropertyChanged("DifferentName");
            }
        }

        public string CallsOnPropertyChangedWithEventArgs
        {
            get { return _callsOnPropertyChangedWithEventArgs; }
            set
            {
                _callsOnPropertyChangedWithEventArgs = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(CallsOnPropertyChangedWithEventArgs)));
            }
        }
    }
}
