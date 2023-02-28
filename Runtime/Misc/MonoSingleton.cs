using UnityEngine;

namespace WilliamQiufeng.UnityUtils.Misc
{
    public class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
    {
        public static T Singleton;

        protected virtual void OnEnable()
        {
            Singleton = (T)this;
        }

        protected virtual void OnDisable()
        {
            Singleton = null;
        }
    }
}