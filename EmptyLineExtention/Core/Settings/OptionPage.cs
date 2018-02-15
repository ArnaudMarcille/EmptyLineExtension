using System.ComponentModel;
using System.Windows;
using EmptyLineExtention.Core.Controls;
using Microsoft.VisualStudio.Shell;

namespace EmptyLineExtention.Core.Settings
{
    public class OptionPage : UIElementDialogPage
    {
        #region Fields

        /// <summary>
        /// Allow to check if autoSave is enable by user
        /// </summary>
        private bool isAutoSaveEnabled;

        /// <summary>
        /// Control
        /// </summary>
        private SettingsControl _control;


        #endregion

        #region Properties

        [Category(Constants.AppName)]
        [DisplayName(Constants.AutoSavePropertyName)]
        [Description(Constants.AutoSavePropertyDesc)]
        public bool IsAutoSaveEnabled
        {
            get { return isAutoSaveEnabled; }
            set { isAutoSaveEnabled = value; }
        }

        protected override UIElement Child
        {
            get
            {
                return _control;
            }
        }

        #endregion

        #region Contructor

        public OptionPage()
        {
            _control = new SettingsControl(this);
        }

        #endregion

        #region Methods


        #endregion
    }
}
