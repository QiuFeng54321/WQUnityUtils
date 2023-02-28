using UnityEngine;
using WilliamQiufeng.UnityUtils.Misc;

namespace WilliamQiufeng.UnityUtils.Controllers
{
    public class VolumeSlider : SliderController
    {
        protected override string FormatText(float val)
        {
            return $"{Mathf.RoundToInt(val * 100)} %";
        }

        protected override float MapToVisualValue(float val)
        {
            if (val.ApproxEqual(-80)) return 0;
            return Mathf.Pow(10, val / 20);
        }

        protected override float MapToActualValue(float val)
        {
            if (val.ApproxEqual(0)) return -80;
            return 20 * Mathf.Log10(val);
        }
    }
}