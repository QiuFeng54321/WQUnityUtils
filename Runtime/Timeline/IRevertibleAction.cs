namespace WilliamQiufeng.UnityUtils.Timeline
{
    public interface IRevertibleAction
    {
        /// <summary>
        ///     Things to do for the first time
        /// </summary>
        public void Do();

        public void Redo()
        {
            Do();
        }

        public void Undo();

        /// <summary>
        ///     This is called when this action is undid and then a new action is appended
        /// </summary>
        public void DestroyFuture()
        {
        }

        /// <summary>
        ///     This is called when this action is so far behind the PastActions
        /// </summary>
        public void DestroyPast()
        {
        }
    }
}