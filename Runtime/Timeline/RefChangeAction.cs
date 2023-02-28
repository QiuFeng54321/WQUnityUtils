using System.Collections.Generic;
using WilliamQiufeng.UnityUtils.Misc;

namespace WilliamQiufeng.UnityUtils.Timeline
{
    public class RefChangeAction<T> : MonotonousFunctionalAction<ByRef<T>, T>
    {
        public RefChangeAction(ApplicableChange<ByRef<T>, T> change) : base(change)
        {
        }

        public RefChangeAction(List<ApplicableChange<ByRef<T>, T>> changes) : base(changes)
        {
        }

        public RefChangeAction(string name, List<ApplicableChange<ByRef<T>, T>> changes) : base(name, changes)
        {
        }

        public RefChangeAction(string name, ApplicableChange<ByRef<T>, T> change) : base(name, change)
        {
        }
    }
}