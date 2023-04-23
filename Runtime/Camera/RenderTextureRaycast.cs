using UnityEngine;
using UnityEngine.EventSystems;

namespace WilliamQiufeng.UnityUtils.Camera
{
    public class RenderTextureRaycast : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler,
        IPointerClickHandler, IScrollHandler
    {
        public UnityEngine.Camera gridCamera; //Camera that renders to the texture
        public GameObject defaultProcessor;
        private RectTransform _textureRectTransform; //RawImage RectTransform that shows the RenderTexture on the UI

        private void Awake()
        {
            _textureRectTransform = GetComponent<RectTransform>(); //Get the RectTransform
        }

        public void OnDrag(PointerEventData eventData)
        {
            Execute(eventData, ExecuteEvents.dragHandler);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            Execute(eventData, ExecuteEvents.pointerClickHandler, false);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            Execute(eventData, ExecuteEvents.pointerDownHandler);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            Execute(eventData, ExecuteEvents.pointerUpHandler);
        }


        public void OnScroll(PointerEventData eventData)
        {
            Execute(eventData, ExecuteEvents.scrollHandler);
        }

        public void Execute<T>(PointerEventData eventData, ExecuteEvents.EventFunction<T> f,
            bool changePos = true) where T : IEventSystemHandler
        {
            var ray = GetActualPointed(eventData);
            if (changePos)
                eventData.position = ray.GetPoint(0);
            var hit = Physics2D.Raycast(ray.origin, ray.direction);
            Debug.Log($"Raw click {eventData.position}");
            ExecuteEvents.ExecuteHierarchy(hit.collider != null ? hit.collider.gameObject : defaultProcessor, eventData,
                f);
        }

        private Ray GetActualPointed(PointerEventData eventData)
        {
            //I get the point of the RawImage where I click
            RectTransformUtility.ScreenPointToLocalPointInRectangle(_textureRectTransform, eventData.position, null,
                out var localClick);
            //My RawImage is 700x700 and the click coordinates are in range (-350,350) so I transform it to (0,700) to then normalize
            var rect = _textureRectTransform.rect;
            localClick.x -= rect.xMin;
            localClick.y -= rect.yMin;

            //I normalize the click coordinates so I get the viewport point to cast a Ray
            var viewportClick = new Vector2(localClick.x / rect.size.x, localClick.y / rect.size.y);

            //I have a special layer for the objects I want to detect with my ray

            //I cast the ray from the camera which rends the texture
            var ray = gridCamera.ViewportPointToRay(new Vector3(viewportClick.x, viewportClick.y, 0));
            return ray;
        }
    }
}