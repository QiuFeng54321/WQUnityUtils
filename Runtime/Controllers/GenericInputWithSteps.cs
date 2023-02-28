using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace WilliamQiufeng.UnityUtils.Controllers
{
    public class GenericInputWithSteps<T> : MonoBehaviour
    {
        public TMP_InputField inputField;

        public UnityEvent<T> onValueChanged;
        public T invalidDefaultValue;
        public T min, max;
        public T previousValue;

        public virtual T Parse(string text)
        {
            return invalidDefaultValue;
        }

        public virtual T Increment(T val, T increment)
        {
            return val;
        }

        public virtual string ValueToString(T val)
        {
            return $"{val}";
        }

        public virtual void GetValueAndUpdateField(T increment = default)
        {
            var val = EqualityComparer<T>.Default.Equals(increment, default)
                ? Parse(inputField.text)
                : Increment(Parse(inputField.text), increment);
            if (Comparer<T>.Default.Compare(val, max) == 1) val = max;
            if (Comparer<T>.Default.Compare(val, min) == -1) val = min;
            inputField.SetTextWithoutNotify(ValueToString(val));
            if (EqualityComparer<T>.Default.Equals(val, previousValue)) return;
            onValueChanged.Invoke(val);
            previousValue = val;
        }

        public void OnDirectInputEnd()
        {
            GetValueAndUpdateField();
        }

        public void OnStep(T increment)
        {
            GetValueAndUpdateField(increment);
        }
    }
}