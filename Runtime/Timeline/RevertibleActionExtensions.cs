namespace WilliamQiufeng.UnityUtils.Timeline
{
    public static class RevertibleActionExtensions
    {
        public static bool TryPerform(this IRevertibleAction revertibleAction)
        {
            return RevertibleTimeline.Default.TryPerformAction(revertibleAction);
        }

        public static void Perform(this IRevertibleAction revertibleAction)
        {
            revertibleAction.TryPerform();
        }
    }
}