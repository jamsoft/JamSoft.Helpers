using Xunit;
using Xunit.Abstractions;

namespace JamSoft.Helpers.Tests.Patterns
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
}
