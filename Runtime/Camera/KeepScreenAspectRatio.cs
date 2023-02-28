using UnityEngine;

namespace WilliamQiufeng.UnityUtils.Camera
{
    public class KeepScreenAspectRatio : MonoBehaviour
    {
        private int _lastHeight;
        private int _lastWidth;

        private void Start()
        {
        }

        private void Update()
        {
            if (Screen.fullScreen) return;
            var width = Screen.width;
            var height = Screen.height;

            if (_lastWidth != width) // if the user is changing the width
            {
                // update the height
                var heightAccordingToWidth = width / 16.0f * 9.0f;
                Screen.SetResolution(width, Mathf.RoundToInt(heightAccordingToWidth), false, 0);
            }
            else if (_lastHeight != height) // if the user is changing the height
            {
                // update the width
                var widthAccordingToHeight = height / 9.0f * 16.0f;
                Screen.SetResolution(Mathf.RoundToInt(widthAccordingToHeight), height, false, 0);
            }

            _lastWidth = width;
            _lastHeight = height;
        }
    }
}