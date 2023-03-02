using System;
using System.Threading;
using JamSoft.Helpers.Patterns.Observer;
using Xunit.Abstractions;

namespace JamSoft.Helpers.Tests.Patterns
{
    public class Observed : ObservableBase
    {
        private readonly ITestOutputHelper _outputHelper;

        // For the sake of simplicity, the Observed's state, essential to all
        // subscribers, is stored in this variable.
        public int State { get; set; } = -0;

        public int ObserverCount => Observers.Count;

        public Observed(ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
        }

        public override void Attach(IObserver observer)
        {
            base.Attach(observer);
            _outputHelper.WriteLine("Observed: Attached an observer.");
        }

        public override void Detach(IObserver observer)
        {
            base.Detach(observer);
            _outputHelper.WriteLine("Observed: Detached an observer.");
        }

        public override void Notify()
        {
            _outputHelper.WriteLine("Observed: Notifying observers...");
            base.Notify();
        }

        // Usually, the subscription logic is only a fraction of what a Observed
        // can really do. Subjects commonly hold some important business logic,
        // that triggers a notification method whenever something important is
        // about to happen (or after it).
        public void SomeBusinessLogic()
        {
            _outputHelper.WriteLine("\nSubject: I'm doing something important.");
            State = new Random().Next(0, 10);

            Thread.Sleep(15);

            _outputHelper.WriteLine("Observed: My state has just changed to: " + State);
            Notify();
        }
    }
}