using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
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
        /// Allowed lines number
        /// </summary>
        private int? allowedLines;

        /// <summary>
        /// Option page instance
        /// </summary>
        internal OptionPage optionsPage;

        /// <summary>
        /// List settings item
        /// </summary>
        private ObservableCollection<SettingItem> settingsItems = new ObservableCollection<SettingItem>();

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

        /// <summary>
        /// Allowed lines translation
        /// </summary>
        public string AllowedLinesContent { get { return Labels.SettingsControl_AllowedLines; } }

        /// <summary>
        /// Allowed lines description
        /// </summary>
        public string AllowedLinesDesc { get { return Labels.SettingsControl_AllowedLinesDesc; } }

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

        /// <summary>
        ///  Option page instance
        /// </summary>
        public string AllowedLines
        {
            get { return allowedLines.HasValue ? allowedLines.ToString() : string.Empty; }
            set
            {
                int formatedValue;
                if (int.TryParse(value, out formatedValue))
                {
                    allowedLines = formatedValue;
                }
                else
                {
                    allowedLines = null;
                }
                optionsPage.AllowedLines = allowedLines;
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public SettingsControl(OptionPage optionsPage)
        {
            InitializeComponent();
            foreach (var item in optionsPage.GetSettingItems())
            {
                settingsItems.Add(item);
            }

            this.optionsPage = optionsPage;
            autoSaveEnabled = optionsPage.IsAutoSaveEnabled;
            allowedLines = optionsPage.AllowedLines;
            SettingsGrid.ItemsSource = settingsItems;
            SettingsGrid.AutoGenerateColumns = false;
            SettingsGrid.CanUserAddRows = true;

            this.DataContext = this;
        }

        #endregion

        #region Events

        /// <summary>
        /// Event for allow numeric values only
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbx_NumericValue_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            int value;
            e.Handled = !int.TryParse(e.Text, out value);
        }

        #endregion

        private void btn_Apply_Click(object sender, RoutedEventArgs e)
        {
            optionsPage.SetSettingItems(settingsItems.ToList());
        }
    }
}
