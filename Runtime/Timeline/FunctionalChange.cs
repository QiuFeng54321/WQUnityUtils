using System;

namespace WilliamQiufeng.UnityUtils.Timeline
{
    public class FunctionalChange<TValue> : ApplicableChange<Action<TValue>, TValue>
    {
        public FunctionalChange(Action<TValue> target, TValue before, TValue after) : base(target, before, after)
        {
        }
    }
}