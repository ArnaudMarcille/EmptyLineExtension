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
        /// Minimum line to remove
        /// </summary>
        private int? allowedLines;

        /// <summary>
        /// SettingItems
        /// </summary>
        private string filesConfigurations;

        #endregion

        #region Properties

        [Category(Constants.AppName)]
        [DisplayName("d")]
        [Description("d")]
        public string FilesConfigurations
        {
            get { return filesConfigurations; }
            set { filesConfigurations = value; }
        }

        [Category(Constants.AppName)]
        [DisplayName(Constants.AutoSavePropertyName)]
        [Description(Constants.AutoSavePropertyDesc)]
        public bool IsAutoSaveEnabled
        {
            get { return isAutoSaveEnabled; }
            set { isAutoSaveEnabled = value; }
        }

        [Category(Constants.AppName)]
        [DisplayName(Constants.AllowedLines)]
        [Description(Constants.AllowedLinesDesc)]
        public int? AllowedLines
        {
            get { return allowedLines; }
            set { allowedLines = value; }
        }

        public int DefaultAllowedLines
        {
            get { return allowedLines.HasValue ? allowedLines.Value : Core.Constants.DefaultAllowedLines; }
        }

        protected override UIElement Child
        {
            get
            {
                return new SettingsControl(this);
            }
        }

        #endregion

        #region Contructor

        public OptionPage()
        {

        }

        #endregion

    }
}
