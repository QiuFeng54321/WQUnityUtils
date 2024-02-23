using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace WilliamQiufeng.UnityUtils.Controllers
{
    public class GenericInputWithSteps<T> : MonoBehaviour
    {
        public delegate string ValueFormatter(T value);

        public TMP_InputField inputField;

        public UnityEvent<T> onValueChanged;
        public T invalidDefaultValue;
        public T min, max;
        public T previousValue;

        public ValueFormatter CustomFormatter;

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
            SetValue(val);
        }

        public virtual void SetValue(T val, bool notify = true)
        {
            if (Comparer<T>.Default.Compare(val, max) == 1) val = max;
            if (Comparer<T>.Default.Compare(val, min) == -1) val = min;
            var displayText = (CustomFormatter ?? ValueToString)(val);
            inputField.SetTextWithoutNotify(displayText);
            if (EqualityComparer<T>.Default.Equals(val, previousValue)) return;
            if (notify) onValueChanged.Invoke(val);
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