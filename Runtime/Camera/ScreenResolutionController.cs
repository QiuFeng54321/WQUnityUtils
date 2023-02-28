using UnityEngine;
using WilliamQiufeng.UnityUtils.Misc;

namespace WilliamQiufeng.UnityUtils.Camera
{
    public class ScreenResolutionController : MonoBehaviour
    {
        private Vector2 savedScreenRes;

        private void FixedUpdate()
        {
            if (!savedScreenRes.x.ApproxEqual(Screen.width) || !savedScreenRes.y.ApproxEqual(Screen.height))
            {
                Screen.SetResolution(Screen.width, Screen.height, false);
                savedScreenRes.x = Screen.width;
                savedScreenRes.y = Screen.height;
            }
        }
    }
}