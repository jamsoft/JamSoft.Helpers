using System.Collections.Generic;
using System.Linq;

namespace JamSoft.Helpers.Patterns.Memento
{
    /// <summary>
    /// Provides a base implementation of the memento pattern
    /// </summary>
    // ReSharper disable once ClassWithVirtualMembersNeverInherited.Global
    public class MementoManager
    {
        /// <summary>
        /// The list of states
        /// </summary>
        protected readonly List<IMemento> Mementos = new List<IMemento>();

        /// <summary>
        /// The state owner instance
        /// </summary>
        protected readonly IMementoOwner Originator;

        /// <summary>
        /// Default ctor
        /// </summary>
        /// <param name="originator"></param>
        public MementoManager(IMementoOwner originator)
        {
            Originator = originator;
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual void Snapshot()
        {
            Mementos.Add(Originator.Save());
        }

        /// <summary>
        /// Rolls back to the last known state
        /// </summary>
        public virtual void Undo()
        {
            if (Mementos.Count == 0)
            {
                return;
            }

            var memento = Mementos.Last();
            Originator.Restore(memento);
            Mementos.Remove(memento);
        }
    }
}