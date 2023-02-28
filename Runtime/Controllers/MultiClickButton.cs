using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using WilliamQiufeng.UnityUtils.Misc;

namespace WilliamQiufeng.UnityUtils.Controllers
{
    public class MultiClickButton : MonoBehaviour
    {
        public int clicksRequired = 4;

        public float resetSeconds;
        public UnityEvent<int, int> clickUpdate;
        public UnityEvent onClickDone;
        private int _clicksDone;
        private Coroutine _currentResetCoroutine;
        private bool _waiting;

        private int ClicksDone
        {
            get => _clicksDone;
            set
            {
                _clicksDone = value;
                if (_clicksDone == clicksRequired)
                {
                    onClickDone.Invoke();
                    _clicksDone = 0;
                    ResetRunningCoroutine();
                }
                else if (_clicksDone != 0)
                {
                    ResetRunningCoroutine();
                    _currentResetCoroutine = CoroutineStub.Singleton.StartCoroutine(ResetCoroutine());
                }

                clickUpdate.Invoke(value, clicksRequired);
            }
        }

        private void OnDestroy()
        {
            ResetRunningCoroutine();
        }

        public void OnClick()
        {
            ClicksDone++;
        }

        private void ResetRunningCoroutine()
        {
            if (_currentResetCoroutine != null) CoroutineStub.Singleton.StopCoroutine(_currentResetCoroutine);
            _currentResetCoroutine = null;
        }

        private IEnumerator ResetCoroutine()
        {
            yield return new WaitForSeconds(resetSeconds);
            ClicksDone = 0;
        }
    }
}