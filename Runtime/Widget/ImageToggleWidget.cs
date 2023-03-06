using UnityEngine;
using UnityEngine.UI;

namespace WilliamQiufeng.UnityUtils.Widget
{
    public class ImageToggleWidget : ToggleWidget
    {
        public Image image;
        public Sprite on, off;

        public override void UpdateVisual()
        {
            image.color = Color.white;
            image.sprite = isOn ? on : off;
        }
    }
}