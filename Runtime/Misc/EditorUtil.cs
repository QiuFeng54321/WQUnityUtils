using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace WilliamQiufeng.UnityUtils.Misc
{
    public static class EditorUtil
    {
        public static string Clipboard
        {
            get => GUIUtility.systemCopyBuffer;
            set => GUIUtility.systemCopyBuffer = value;
        }

        public static Sprite MakeSprite(this Texture2D tex, int ppu = 100)
        {
            return tex.MakeSprite(new Vector2(0.5f, 0.5f), ppu);
        }

        public static Sprite MakeSprite(this Texture2D tex, Vector2 pivot, int ppu = 100)
        {
            return Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), pivot, ppu);
        }

        public static unsafe void ClearArray<T>(this NativeArray<T> toClear, int length) where T : struct
        {
            UnsafeUtility.MemClear(
                NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks(toClear),
                UnsafeUtility.SizeOf<T>() * length);
        }

        public static void ClearArray<T>(this NativeArray<T> toClear) where T : struct
        {
            toClear.ClearArray(toClear.Length);
        }

        public static void SetInteractable(this Selectable selectable, bool interactable)
        {
            if (selectable is TMP_InputField inputField)
                CoroutineStub.Singleton.StartCoroutine(DisableInput(inputField));
            else
                selectable.interactable = interactable;

            IEnumerator DisableInput(Selectable input)
            {
                yield return new WaitForEndOfFrame();
                input.interactable = interactable;
            }
        }

        public static void SetInteractableForTag(this string tag, bool interactable)
        {
            var objs = GameObject.FindGameObjectsWithTag(tag);
            foreach (var go in objs)
            foreach (var component in go.GetComponents<Component>())
            {
                if (component is not Selectable selectable) continue;
                selectable.SetInteractable(interactable);
            }
        }

        public static bool IsPointerOverUIObject(this GameObject gameObject)
        {
            var eventDataCurrentPosition = new PointerEventData(EventSystem.current);
            eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            var results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventDataCurrentPosition, results);

            foreach (var raycastResult in results)
                if (raycastResult.gameObject == gameObject)
                    return true;

            return false;
        }

        public static bool IsSelectedObjectInputField()
        {
            if (EventSystem.current.currentSelectedGameObject == null) return false;
            return EventSystem.current.currentSelectedGameObject.GetComponent<TMP_InputField>() != null ||
                   EventSystem.current.currentSelectedGameObject.GetComponent<InputField>() != null;
        }

        public static string NumberSuffix(this int num)
        {
            if (num / 10 % 10 == 1) return "th";

            return (num % 10) switch
            {
                1 => "st",
                2 => "nd",
                3 => "rd",
                _ => "th"
            };
        }
    }
}