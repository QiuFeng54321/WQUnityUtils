using UnityEngine;
using UnityEngine.UI;

namespace WilliamQiufeng.UnityUtils.Widget
{
    public class ColorToggleWidget : ToggleWidget
    {
        public Graphic graphics;

        public override void UpdateVisual()
        {
            graphics.color = isOn ? ActivatedColor : Color.white;
        }
    }
}