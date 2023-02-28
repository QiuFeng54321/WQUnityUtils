namespace WilliamQiufeng.UnityUtils.Misc
{
    public class ByRef<T>
    {
        public delegate ref T Get();

        public Get GetRef;

        public ByRef(Get getRef)
        {
            GetRef = getRef;
        }
    }
}