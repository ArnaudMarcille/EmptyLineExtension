using System;
using System.Windows.Input;

namespace EmptyLineExtention.Core.Settings
{
    public class Command : ICommand
    {
        #region Handlers

        /// <summary>
        /// Action to execute
        /// </summary>
        private readonly Action handlerExecute;

        /// <summary>
        /// Action to check if we can execute
        /// </summary>
        private readonly Func<bool> handlerCanExecute;

        /// <summary>
        /// Can execute changed
        /// </summary>
        public event EventHandler CanExecuteChanged;

        #endregion

        /// <summary>
        /// Is the command enabled ?
        /// </summary>
        internal bool isEnabled;

        #region Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="handlerExecute"></param>
        /// <param name="handlerCanExecute"></param>
        public Command(Action handlerExecute, Func<bool> handlerCanExecute)
        {
            this.handlerExecute = handlerExecute;
            this.handlerCanExecute = handlerCanExecute;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="handlerExecute"></param>
        public Command(Action handlerExecute)
        {
            this.handlerExecute = handlerExecute;
            handlerCanExecute = default(Func<bool>);
        }

        #endregion

        #region Execute

        /// <summary>
        /// Raise the can execute event
        /// </summary>
        protected void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Can we execute this command ?
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool CanExecute(object parameter)
        {
            if (handlerCanExecute == default(Func<bool>))
                return true;

            var newCanExecute = handlerCanExecute();
            if (newCanExecute != isEnabled)
            {
                isEnabled = newCanExecute;
                RaiseCanExecuteChanged();
            }
            return isEnabled;
        }

        /// <summary>
        /// Execution method
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object parameter)
        {
            handlerExecute();
        }
        #endregion

    }
}
