using System.Collections.Generic;

namespace WilliamQiufeng.UnityUtils.Timeline
{
    public class FunctionalAction<TValue> : IRevertibleAction
    {
        private readonly List<FunctionalChange<TValue>> _changes;
        private readonly string _name;

        public FunctionalAction(string name, List<FunctionalChange<TValue>> changes)
        {
            _changes = changes;
            _name = name;
        }

        public FunctionalAction(string name, FunctionalChange<TValue> change) : this(name,
            new List<FunctionalChange<TValue>> { change })
        {
        }

        public void Do()
        {
            _changes.ForEach(change => change.Target(change.After));
        }

        public void Undo()
        {
            _changes.ForEach(change => change.Target(change.Before));
        }

        public override string ToString()
        {
            return $"Set value of '{_name}'";
        }
    }
}