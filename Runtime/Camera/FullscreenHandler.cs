using UnityEngine;

namespace WilliamQiufeng.UnityUtils.Camera
{
    public class FullscreenHandler : MonoBehaviour
    {
        private bool lastFullscreen;
        private int lastResolutionHeight;
        private int lastResolutionWidth;

        protected void Awake()
        {
            lastResolutionWidth = Screen.currentResolution.width;
            lastResolutionHeight = Screen.currentResolution.height;
            lastFullscreen = Screen.fullScreen;
        }

        private void Update()
        {
            if (Screen.fullScreen != lastFullscreen)
            {
                if (Screen.fullScreen) Screen.SetResolution(lastResolutionWidth, lastResolutionHeight, true);
                lastFullscreen = Screen.fullScreen;
            }

            if (!Screen.fullScreen && (Screen.currentResolution.width != lastResolutionWidth ||
                                       Screen.currentResolution.height != lastResolutionHeight))
            {
                lastResolutionWidth = Screen.currentResolution.width;
                lastResolutionHeight = Screen.currentResolution.height;
            }
        }
    }
}