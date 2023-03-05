using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;
using Object = UnityEngine.Object;

namespace WilliamQiufeng.UnityUtils.Misc
{
    public static class Util
    {
        public delegate void WebRequestDelegate(UnityWebRequest unityWebRequest);

        public static float CyclicClamp(this float val, float lb, float ub)
        {
            var len = ub - lb + 1;
            while (val > ub) val -= len;
            while (val < lb) val += len;
            return val;
        }


        public static void Cycle(this ref int self, int inc, int lb, int ub)
        {
            if (inc > 0)
            {
                self = (self - lb + inc) % (ub - lb) + lb;
            }
            else
            {
                self += inc;
                while (self < lb) self += ub - lb;
            }
        }

        public static void Drift(this int self, int inc, int lb, int ub)
        {
            self = Math.Clamp(self + inc, lb, ub);
        }

        public static float Map(this float val, float olb, float oub, float nlb, float nub)
        {
            return nlb + (nub - nlb) * val / (oub - olb);
        }

        public static int Modulo(this int x, int m)
        {
            return (x % m + m) % m;
        }

        public static bool Flip(this bool self)
        {
            self = !self;
            return self;
        }

        public static float FloatFromText(string text, float defaultValue = 0f)
        {
            FloatFromText(out var value, text, defaultValue);
            return value;
        }

        public static void FloatFromText(out float output, string text, float defaultValue = 0f)
        {
            if (!float.TryParse(text, out output)) output = defaultValue;
        }

        public static void IntFromText(out int output, string text, int defaultValue = 0)
        {
            if (!int.TryParse(text, out output)) output = defaultValue;
        }

        public static Vector3 Vector3Scale(this Vector3 vec, Vector3 localScale)
        {
            return new Vector3(vec.x * localScale.x,
                vec.y * localScale.y,
                vec.z * localScale.z);
        }

        public static Vector2 Vector2Div(Vector2 vec, Vector2 localScale)
        {
            return new Vector2(vec.x / localScale.x,
                vec.y / localScale.y);
        }

        public static void ClearChildren(this Transform transform)
        {
            foreach (Transform t in transform) Object.Destroy(t.gameObject);
        }

        public static IEnumerable<T> GetUniqueFlags<T>(this T flags) where T : Enum
        {
            ulong flag = 1;
            foreach (var value in Enum.GetValues(flags.GetType()).Cast<T>())
            {
                var bits = Convert.ToUInt64(value);
                while (flag < bits) flag <<= 1;

                if (flag == bits && flags.HasFlag(value)) yield return value;
            }
        }

        public static bool JustPressed(this KeyCode keyCode)
        {
            return Input.GetKeyDown(keyCode);
        }

        public static bool Pressed(this KeyCode keyCode)
        {
            return Input.GetKey(keyCode);
        }

        public static int NextLarger<T>(this List<T> array, T element)
        {
            var index = array.BinarySearch(element);
            if (index < 0) return ~index;

            return index;
        }

        public static int NextLarger<T>(this List<T> array, T element, IComparer<T> comparer)
        {
            var index = array.BinarySearch(element, comparer);
            if (index < 0) return ~index;

            return index;
        }

        public static int PreviousLess<T>(this T[] array, T element, IComparer<T> comparer)
        {
            var index = Array.BinarySearch(array, element, comparer);
            if (index < 0) return ~index - 1;

            return index;
        }

        public static bool ApproxEqual(this float a, float b)
        {
            return Math.Abs(a - b) < 0.001f;
        }


        /// <summary>
        ///     Retrieves a clip from given url, then calls <see cref="WebRequestDelegate" />
        /// </summary>
        /// <param name="url">The url to retrieve the media file</param>
        /// <param name="webRequestDelegate">The function to call when retrieved successfully</param>
        /// <returns>A coroutine IEnumerator</returns>
        public static IEnumerator GetClip(string url, WebRequestDelegate webRequestDelegate)
        {
            var audioType = GetAudioType(url);
            Debug.Log($"{url}, {audioType}");
            var unityWebRequest = UnityWebRequestMultimedia.GetAudioClip(url, audioType);
            return SendWebRequest(unityWebRequest, webRequestDelegate);
        }

        public static IEnumerator SendWebRequest(UnityWebRequest unityWebRequest, WebRequestDelegate webRequestDelegate)
        {
            yield return unityWebRequest.SendWebRequest();
            if (unityWebRequest.result is UnityWebRequest.Result.ProtocolError
                or UnityWebRequest.Result.ConnectionError)
                Debug.Log(unityWebRequest.error);
            else
                webRequestDelegate(unityWebRequest);
        }

        public static IEnumerator GetTexture(string url, WebRequestDelegate webRequestDelegate)
        {
            var unityWebRequest = UnityWebRequestTexture.GetTexture(url);
            return SendWebRequest(unityWebRequest, webRequestDelegate);
        }

        public static IEnumerator LoadTexture(string url, Action<Texture2D> onFinish)
        {
            return GetTexture(url, request => { onFinish(DownloadHandlerTexture.GetContent(request)); });
        }

        private static AudioType GetAudioType(string path)
        {
            return Path.GetExtension(path)
                switch
                {
                    ".mp3" => AudioType.MPEG,
                    ".ogg" => AudioType.OGGVORBIS,
                    ".wav" => AudioType.WAV,
                    _ => AudioType.UNKNOWN
                };
        }
    }
}