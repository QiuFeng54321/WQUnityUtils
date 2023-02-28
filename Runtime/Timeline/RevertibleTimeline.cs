using System;
using System.Collections.Generic;
using UnityEngine;

namespace WilliamQiufeng.UnityUtils.Timeline
{
    public class RevertibleTimeline
    {
        public const int MaximumPastActionCount = 30;
        public static readonly RevertibleTimeline Default = new();

        /// <summary>
        ///     Actions to be done after the current state.
        ///     When an action is undid it goes here
        /// </summary>
        public readonly LinkedList<IRevertibleAction> FutureActions = new();

        /// <summary>
        ///     Actions done before current state
        /// </summary>
        public readonly LinkedList<IRevertibleAction> PastActions = new();

        /// <summary>
        ///     When <see cref="Time" /> reaches 0 from negative, this will be set to false.
        ///     When an action is performed, overriding some future actions, this will be set to true.
        ///     We can then check if the current state is the same as the saved state by Time == 0 && !IsFutureOverriden
        /// </summary>
        public bool IsFutureOverriden;

        /// <summary>
        ///     Relative "offset" to the last saved state.
        ///     0 means no difference
        ///     Undo => -1
        ///     Redo => +1
        /// </summary>
        public int Time;

        public bool IsSameWithLastSave => Time == 0 && !IsFutureOverriden;

        /// <summary>
        ///     Called when an action is performed/undid/redid
        /// </summary>
        public event Action<IRevertibleAction> OnActionChange;

        public void SetCurrentAsSaved()
        {
            Time = 0;
            IsFutureOverriden = false;
        }

        public void Reset()
        {
            PastActions.Clear();
            FutureActions.Clear();
        }

        public void Undo()
        {
            Time--;
            if (PastActions.Count == 0) return;
            var action = PastActions.Last.Value;
            action.Undo();
            FutureActions.AddLast(action);
            PastActions.RemoveLast();
            OnActionChange?.Invoke(action);
        }

        public void Redo()
        {
            Time++;
            if (FutureActions.Count == 0) return;
            var action = FutureActions.Last.Value;
            action.Redo();
            PastActions.AddLast(action);
            FutureActions.RemoveLast();
            OnActionChange?.Invoke(action);
        }

        public bool TryPerformAction(IRevertibleAction action)
        {
            try
            {
                PerformAction(action);
                return true;
            }
            catch (Exception e)
            {
                Debug.LogError(e);
                return false;
            }
        }

        public void PerformAction(IRevertibleAction action)
        {
            Time++;
            action.Do();
            PastActions.AddLast(action);
            while (FutureActions.Count > 0)
            {
                FutureActions.Last.Value.DestroyFuture();
                FutureActions.RemoveLast();
                if (Time > 0) IsFutureOverriden = true;
            }

            while (PastActions.Count > MaximumPastActionCount)
            {
                PastActions.First.Value.DestroyPast();
                PastActions.RemoveFirst();
            }

            OnActionChange?.Invoke(action);
        }

        public void PerformSetValueAction<TValue>(string name, Action<TValue> action, TValue before, TValue after)
        {
            PerformAction(new FunctionalAction<TValue>(name, new FunctionalChange<TValue>(action, before, after)));
        }
    }
}