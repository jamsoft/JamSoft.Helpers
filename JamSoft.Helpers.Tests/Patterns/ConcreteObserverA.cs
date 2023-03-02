using JamSoft.Helpers.Patterns.Observer;
using Xunit.Abstractions;

namespace JamSoft.Helpers.Tests.Patterns
{
    class ConcreteObserverA : IObserver
    {
        private readonly ITestOutputHelper _outputHelper;

        public int NotificationCount { get; set; }

        public ConcreteObserverA(ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
        }

        public void Update(IObservable observable)
        {
            NotificationCount++;
            if (((Observed) observable).State < 3)
            {
                _outputHelper.WriteLine("ConcreteObserverA: Reacted to the event.");
            }
        }
    }
}