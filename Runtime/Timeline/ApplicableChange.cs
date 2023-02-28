namespace WilliamQiufeng.UnityUtils.Timeline
{
    public class ApplicableChange<TObject, TValueType>
    {
        public TValueType After;
        public TValueType Before;
        public TObject Target;

        public ApplicableChange(TObject target, TValueType before, TValueType after)
        {
            Target = target;
            Before = before;
            After = after;
        }
    }
}