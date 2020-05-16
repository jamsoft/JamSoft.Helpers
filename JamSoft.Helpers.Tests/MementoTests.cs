using System;
using System.Threading;
using JamSoft.Helpers.Patterns.Memento;
using Xunit;
using Xunit.Abstractions;

namespace JamSoft.Helpers.Tests
{
    public class MementoTests
    {
        private readonly ITestOutputHelper _outputHelper;

        public MementoTests(ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
        }

        [Fact]
        public void Works()
        {
            var originalState = "Super-duper-super-puper-super.";

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

    class Originator : IMementoOwner
    {
        // For the sake of simplicity, the originator's state is stored inside a
        // single variable.
        private string _state;
        private readonly ITestOutputHelper _outputHelper;

        public Originator(string state, ITestOutputHelper outputHelper)
        {
            _state = state;
            _outputHelper = outputHelper;
            _outputHelper.WriteLine("Originator: My initial state is: " + state);
        }

        // The Originator's business logic may affect its internal state.
        // Therefore, the client should backup the state before launching
        // methods of the business logic via the save() method.
        public void DoSomething()
        {
            _outputHelper.WriteLine("Originator: I'm doing something important.");
            _state = GenerateRandomString(30);
            _outputHelper.WriteLine($"Originator: and my state has changed to: {_state}");
        }

        private string GenerateRandomString(int length = 10)
        {
            string allowedSymbols = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string result = string.Empty;

            while (length > 0)
            {
                result += allowedSymbols[new Random().Next(0, allowedSymbols.Length)];

                Thread.Sleep(12);

                length--;
            }

            return result;
        }

        public string WhatIsMyState()
        {
            return _state;
        }

        // Saves the current state inside a memento.
        public IMemento Save()
        {
            return new ConcreteMemento(_state);
        }

        // Restores the Originator's state from a memento object.
        public void Restore(IMemento memento)
        {
            if (!(memento is ConcreteMemento))
            {
                throw new Exception("Unknown memento class " + memento);
            }

            _state = memento.GetState().ToString();
            _outputHelper.WriteLine($"Originator: My state has changed to: {_state}");
        }
    }

    class ConcreteMemento : IMemento
    {
        private readonly string _state;

        public ConcreteMemento(string state)
        {
            _state = state;
        }

        // The Originator uses this method when restoring its state.
        public object GetState()
        {
            return _state;
        }
    }
}
