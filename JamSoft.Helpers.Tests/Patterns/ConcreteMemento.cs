using JamSoft.Helpers.Patterns.Memento;

namespace JamSoft.Helpers.Tests.Patterns
{
    public class ConcreteMemento : Memento
    {
        public ConcreteMemento(string state)
            :base(state)
        {
        }
    }
}