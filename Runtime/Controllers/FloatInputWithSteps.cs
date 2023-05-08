using System;
using System.Globalization;

namespace WilliamQiufeng.UnityUtils.Controllers
{
    public class FloatInputWithSteps : GenericInputWithSteps<float>
    {
        public int decimalPlaces = 2;

        public override float Parse(string text)
        {
            if (float.TryParse(text, out var o)) return o;
            return invalidDefaultValue;
        }

        public override string ValueToString(float val)
        {
            return MathF.Round(val, decimalPlaces).ToString(CultureInfo.InvariantCulture);
        }

        public override float Increment(float val, float increment)
        {
            return val + increment;
        }
    }
}