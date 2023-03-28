using System;
using System.Collections;
using UnityEngine;

namespace WilliamQiufeng.UnityUtils.Misc
{
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