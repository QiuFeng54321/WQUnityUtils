using UnityEngine;
using UnityEngine.Events;

namespace WilliamQiufeng.UnityUtils.Widget
{
    public class ToggleWidget : MonoBehaviour
    {
        public static readonly Color ActivatedColor = new(255, 215, 0);
        public UnityEvent<bool> onToggle;
        public bool isOn;

        private void Start()
        {
            UpdateVisual();
        }

        public void SetOn(bool state)
        {
            if (SetOnWithoutNotify(state)) onToggle.Invoke(isOn);
        }

        public bool SetOnWithoutNotify(bool state)
        {
            if (isOn == state) return false;
            isOn = state;
            UpdateVisual();
            return true;
        }

        public virtual void UpdateVisual()
        {
        }

        public void Toggle()
        {
            SetOn(!isOn);
        }
    }
}