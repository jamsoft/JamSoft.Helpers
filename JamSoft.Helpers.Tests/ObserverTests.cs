using System;
using System.Threading;
using JamSoft.Helpers.Patterns.Observer;
using Xunit;
using Xunit.Abstractions;

namespace JamSoft.Helpers.Tests
{
    public class ObserverTests
    {
        private readonly ITestOutputHelper _outputHelper;

        public ObserverTests(ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
        }

        [Fact]
        public void Attach_Success()
        {
            // The client code.
            var subject = new Observed(_outputHelper);
            var observerA = new ConcreteObserverA(_outputHelper);
            subject.Attach(observerA);

            var observerB = new ConcreteObserverB(_outputHelper);
            subject.Attach(observerB);

            Assert.Equal(2, subject.ObserverCount);
        }

        [Fact]
        public void Detach_Success()
        {
            // The client code.
            var subject = new Observed(_outputHelper);
            var observerA = new ConcreteObserverA(_outputHelper);
            subject.Attach(observerA);

            var observerB = new ConcreteObserverB(_outputHelper);
            subject.Attach(observerB);

            subject.Detach(observerB);

            Assert.Equal(1, subject.ObserverCount);
        }

        [Fact]
        public void Notification_Success()
        {
            // The client code.
            var subject = new Observed(_outputHelper);
            var observerA = new ConcreteObserverA(_outputHelper);
            subject.Attach(observerA);

            var observerB = new ConcreteObserverB(_outputHelper);
            subject.Attach(observerB);

            subject.SomeBusinessLogic();
            subject.SomeBusinessLogic();

            subject.Detach(observerB);

            subject.SomeBusinessLogic();

            Assert.Equal(5, observerA.NotificationCount + observerB.NotificationCount);
        }
    }

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
