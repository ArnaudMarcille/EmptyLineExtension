using System;

namespace EmptyLineExtention.Core.Settings
{
    /// <summary>
    /// Move event args
    /// </summary>
    public class MoveEventArgs : EventArgs
    {
        /// <summary>
        /// Move action
        /// </summary>
        public MoveEnum moveAction { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="moveAction"></param>
        public MoveEventArgs(MoveEnum moveAction)
        {
            this.moveAction = moveAction;
        }
    }
}