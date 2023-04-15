using System;
using System.Collections;
using UnityEngine;

namespace WilliamQiufeng.UnityUtils.Misc
{
    /// <summary>
    ///     Stores an action that, upon one or multiple callings, will be executed after a period of it not being called.
    ///     <example>
    ///         If <see cref="DelaySeconds" /> is 1f and <see cref="Queue" /> is called at
    ///         time 0f and 0.5f, <see cref="QueueAction" /> will be called at 1.5f seconds
    ///     </example>
    /// </summary>
    public class DelayedCallable
    {
        public DelayedCallable(Action delayedAction)
        {
            DelayedAction = delayedAction;
        }

        protected bool QueueAction { get; set; }
        public Action DelayedAction { get; }
        public float DelaySeconds { get; set; } = 1;

        public void Queue()
        {
            QueueAction = true;
        }

        public void StartCoroutine(MonoBehaviour monoBehaviour)
        {
            monoBehaviour.StartCoroutine(DelayedCoroutine());
        }

        public IEnumerator DelayedCoroutine()
        {
            while (true)
            {
                if (QueueAction)
                {
                    while (QueueAction)
                    {
                        QueueAction = false;
                        if (DelaySeconds == 0)
                            yield return new WaitForEndOfFrame();
                        else
                            yield return new WaitForSeconds(DelaySeconds);
                    }

                    DelayedAction();
                }

                yield return new WaitForSeconds(DelaySeconds);
            }
        }
    }
}