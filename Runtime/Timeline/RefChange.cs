using WilliamQiufeng.UnityUtils.Misc;

namespace WilliamQiufeng.UnityUtils.Timeline
{
    public class RefChange<T> : ApplicableChange<ByRef<T>, T>
    {
        public RefChange(ByRef<T> target, T val) : base(target, target.GetRef(), val)
        {
        }
    }
}