using System;
using System.Collections.Generic;

namespace WilliamQiufeng.UnityUtils.Timeline
{
    public class GetSetAction<T, V> : MonotonousFunctionalAction<T, V>
    {
        public GetSetAction(T obj, V val, Func<T, V> getter, Action<T, V> setter) : base(
            new ApplicableChange<T, V>(obj, getter(obj), val))
        {
            Action = setter;
        }

        protected GetSetAction(ApplicableChange<T, V> change) : base(change)
        {
        }

        protected GetSetAction(List<ApplicableChange<T, V>> changes) : base(changes)
        {
        }

        protected override Action<T, V> Action { get; }
    }
}