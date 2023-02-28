using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace WilliamQiufeng.UnityUtils.Controllers
{
    public class SliderController : MonoBehaviour
    {
        public UnityEvent<float> valueChanged;
        public TMP_Text text;
        public Slider slider;

        protected virtual string FormatText(float val)
        {
            return $"{val:0.##}";
        }

        protected virtual float UpdateCurrentValue()
        {
            return slider.value;
        }

        protected virtual float MapToActualValue(float val)
        {
            return val;
        }

        protected virtual float MapToVisualValue(float val)
        {
            return val;
        }

        public void Set(float actualVal)
        {
            var visualValue = MapToVisualValue(actualVal);
            SetWithoutNotify(actualVal);
            valueChanged.Invoke(actualVal);
        }

        public void SetWithoutNotify(float actualVal)
        {
            var visualValue = MapToVisualValue(actualVal);
            slider.SetValueWithoutNotify(visualValue);
            text.text = FormatText(visualValue);
        }

        public void ValueChange(float visualVal)
        {
            var actualValue = MapToActualValue(visualVal);
            text.text = FormatText(visualVal);
            valueChanged.Invoke(actualValue);
        }
    }
}