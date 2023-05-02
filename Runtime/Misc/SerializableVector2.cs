using Newtonsoft.Json;
using UnityEngine;

namespace WilliamQiufeng.UnityUtils.Misc
{
    public struct SerializableVector2
    {
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Include)]
        public float X { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Include)]
        public float Y { get; set; }


        public SerializableVector2(float x, float y)
        {
            X = x;
            Y = y;
        }

        public static implicit operator Vector2(SerializableVector2 v)
        {
            return new Vector2(v.X, v.Y);
        }

        public static implicit operator SerializableVector2(Vector2 v)
        {
            return new SerializableVector2
            {
                X = v.x,
                Y = v.y
            };
        }
    }
}