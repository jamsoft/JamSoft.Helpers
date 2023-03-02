using JamSoft.Helpers.Patterns.Memento;
using Xunit;
using Xunit.Abstractions;

namespace JamSoft.Helpers.Tests.Patterns
{
    public class MementoTests
    {
        private readonly ITestOutputHelper _outputHelper;

        public MementoTests(ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
        }

        [Fact]
        public void MementoManager_Undo_Without_Snapshots()
        {
            var originalState = "Super-duper-super-duper-super.";

            // Client code.
            var originator = new Originator(originalState, _outputHelper);
            var caretaker = new MementoManager(originator);
            caretaker.Undo();
            Assert.Equal(originalState, originator.WhatIsMyState());
        }

        [Fact]
        public void MementoManager_State_Management()
        {
            var originalState = "Super-duper-super-duper-super.";

            // Client code.
            var originator = new Originator(originalState, _outputHelper);
            var caretaker = new MementoManager(originator);

            caretaker.Snapshot();
            originator.DoSomething();

            var firstEditState = originator.WhatIsMyState();
            Assert.NotEqual(originalState, firstEditState);

            caretaker.Snapshot();
            originator.DoSomething();

            var secondEditState = originator.WhatIsMyState();
            Assert.NotEqual(originalState, secondEditState);
            Assert.NotEqual(firstEditState, secondEditState);

            caretaker.Snapshot();
            originator.DoSomething();

            var thirdEditState = originator.WhatIsMyState();
            Assert.NotEqual(originalState, thirdEditState);
            Assert.NotEqual(secondEditState, thirdEditState);
            Assert.NotEqual(firstEditState, thirdEditState);

            caretaker.Undo();
            Assert.Equal(secondEditState, originator.WhatIsMyState());

            caretaker.Undo();
            Assert.Equal(firstEditState, originator.WhatIsMyState());

            caretaker.Undo();
            Assert.Equal(originalState, originator.WhatIsMyState());
        }
    }
}
