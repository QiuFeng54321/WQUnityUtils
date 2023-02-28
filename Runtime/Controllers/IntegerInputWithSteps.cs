namespace WilliamQiufeng.UnityUtils.Controllers
{
    public class IntegerInputWithSteps : GenericInputWithSteps<int>
    {
        public override int Parse(string text)
        {
            if (int.TryParse(text, out var res)) return res;
            return invalidDefaultValue;
        }

        public override int Increment(int val, int increment)
        {
            return val + increment;
        }
    }
}