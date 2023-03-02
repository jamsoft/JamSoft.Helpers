using JamSoft.Helpers.Patterns.Observer;
using Xunit.Abstractions;

namespace JamSoft.Helpers.Tests.Patterns
{
    class ConcreteObserverB : IObserver
    {
        private readonly ITestOutputHelper _outputHelper;

        public int NotificationCount { get; set; }

        public ConcreteObserverB(ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
        }

        public void Update(IObservable observable)
        {
            NotificationCount++;
            if (((Observed) observable).State == 0 || ((Observed) observable).State >= 2)
            {
                _outputHelper.WriteLine("ConcreteObserverB: Reacted to the event.");
            }
        }
    }
}