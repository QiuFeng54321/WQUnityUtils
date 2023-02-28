using System;
using System.Collections.Generic;

namespace WilliamQiufeng.UnityUtils.Timeline
{
    /// <summary>
    ///     Make a group of changes subject to one action only.
    /// </summary>
    /// <typeparam name="TObject">Type of the object to change</typeparam>
    /// <typeparam name="TValueType">Type of the value</typeparam>
    public class MonotonousFunctionalAction<TObject, TValueType> : IRevertibleAction
    {
        protected readonly List<ApplicableChange<TObject, TValueType>> Changes;

        protected MonotonousFunctionalAction(ApplicableChange<TObject, TValueType> change) : this(
            new List<ApplicableChange<TObject, TValueType>> { change })
        {
        }

        protected MonotonousFunctionalAction(List<ApplicableChange<TObject, TValueType>> changes)
        {
            Changes = changes;
        }

        protected MonotonousFunctionalAction(string name,
            List<ApplicableChange<TObject, TValueType>> changes) : this(changes)
        {
            Name = name;
        }

        protected MonotonousFunctionalAction(string name,
            ApplicableChange<TObject, TValueType> change) :
            this(name, new List<ApplicableChange<TObject, TValueType>> { change })
        {
        }

        protected virtual string Name { get; } = "Unknown";
        protected virtual Action<TObject, TValueType> Action { get; }

        public void Do()
        {
            Changes.ForEach(change => Action(change.Target, change.After));
        }

        public void Undo()
        {
            Changes.ForEach(change => Action(change.Target, change.Before));
        }

        public override string ToString()
        {
            return $"Set value of '{Name}' for {Changes.Count} objects";
        }
    }
}