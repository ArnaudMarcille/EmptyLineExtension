using System.Windows.Controls;
using EmptyLineExtention.Core.Localization;
using EmptyLineExtention.Core.Settings;

namespace EmptyLineExtention.Core.Controls
{
    /// <summary>
    /// Interaction logic for SettingsControl.xaml
    /// </summary>
    public partial class SettingsControl : UserControl
    {
        #region Fields

        /// <summary>
        /// Auto save enabled values
        /// </summary>
        private bool autoSaveEnabled;

        /// <summary>
        /// Option page instance
        /// </summary>
        internal OptionPage optionsPage;

        #endregion

        #region Translations

        /// <summary>
        /// Header name translation
        /// </summary>
        public string HeaderName { get { return Labels.SettingsControl_Header; } }

        /// <summary>
        /// Auto save content translation
        /// </summary>
        public string AutoSaveContent { get { return Labels.SettingsControl_AutoFormat_Content; } }

        #endregion

        #region Properties

        /// <summary>
        /// Auto save value accessors
        /// </summary>
        public bool AutoSaveEnabled
        {
            get
            {
                return autoSaveEnabled;
            }
            set
            {
                autoSaveEnabled = value;
                optionsPage.IsAutoSaveEnabled = autoSaveEnabled;
            }
        }


        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public SettingsControl(OptionPage optionsPage)
        {
            this.optionsPage = optionsPage;
            autoSaveEnabled = optionsPage.IsAutoSaveEnabled;
            InitializeComponent();
            this.DataContext = this;
        }

        #endregion

    }
}
