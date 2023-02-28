using UnityEngine;

namespace WilliamQiufeng.UnityUtils.Camera
{
    [RequireComponent(typeof(UnityEngine.Camera))]
    public class CameraScaler : MonoBehaviour
    {
        [SerializeField] protected int targetWidth = 1920;
        [SerializeField] protected int targetHeight = 1080;

        [SerializeField] protected int dynamicMaxWidth = 2560;
        [SerializeField] protected int dynamicMaxHeight = 1440;

        [SerializeField] protected bool useDynamicWidth;
        [SerializeField] protected bool useDynamicHeight;

        private UnityEngine.Camera cam;
        private int lastHeight;
        private int lastWidth;

        private float orthoSize;

        protected void Awake()
        {
            cam = GetComponent<UnityEngine.Camera>();
            orthoSize = cam.orthographicSize;
        }

        protected void Update()
        {
            if (Screen.width == lastWidth && Screen.height == lastHeight) return;
            UpdateCamSize();
            lastWidth = Screen.width;
            lastHeight = Screen.height;
        }

        private void UpdateCamSize()
        {
            float targetAspect;
            var screenAspect = Screen.width / (float)Screen.height;
            var orthoScale = 1f;

            if (useDynamicWidth)
            {
                var minTargetAspect = targetWidth / (float)targetHeight;
                var maxTargetAspect = dynamicMaxWidth / (float)targetHeight;
                targetAspect = Mathf.Clamp(screenAspect, minTargetAspect, maxTargetAspect);
            }
            else
            {
                targetAspect = targetWidth / (float)targetHeight;
            }

            var scaleValue = screenAspect / targetAspect;

            Rect rect = new();
            if (scaleValue < 1f)
            {
                if (useDynamicHeight)
                {
                    var minTargetAspect = targetWidth / (float)dynamicMaxHeight;
                    if (screenAspect < minTargetAspect)
                    {
                        scaleValue = screenAspect / minTargetAspect;
                        orthoScale = minTargetAspect / targetAspect;
                    }
                    else
                    {
                        orthoScale = scaleValue;
                        scaleValue = 1f;
                    }
                }

                rect.width = 1;
                rect.height = scaleValue;
                rect.x = 0;
                rect.y = (1 - scaleValue) / 2;
            }
            else
            {
                scaleValue = 1 / scaleValue;
                rect.width = scaleValue;
                rect.height = 1;
                rect.x = (1 - scaleValue) / 2;
                rect.y = 0;
            }

            cam.orthographicSize = orthoSize / orthoScale;
            cam.rect = rect;
        }
    }
}