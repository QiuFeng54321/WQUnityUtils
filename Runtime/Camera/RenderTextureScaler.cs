using UnityEngine;

namespace WilliamQiufeng.UnityUtils.Camera
{
    public class RenderTextureScaler : MonoBehaviour
    {
        public RenderTexture renderTexture;
        public RectTransform rectTransform;

        private void Update()
        {
            var rect = rectTransform.rect;
            var w = (int)rect.width;
            var h = (int)rect.height;
            if (renderTexture.width != w || renderTexture.height != h)
            {
                renderTexture.Release();

                renderTexture.width = (int)rect.width;
                renderTexture.height = (int)rect.height;
                renderTexture.Create();
            }
        }
    }
}