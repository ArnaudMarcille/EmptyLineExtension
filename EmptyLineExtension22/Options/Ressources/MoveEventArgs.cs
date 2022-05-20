using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmptyLineExtension22.Options.Ressources
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
