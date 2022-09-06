using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using EmptyLineExtension22.Localization;
using EmptyLineExtension22.Options.Ressources;

namespace EmptyLineExtension22.Options
{
    /// <summary>
    /// Interaction logic for OptionPage.xaml
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
        /// Ignore first lines enabled
        /// </summary>
        private bool ignoreFirstLinesEnabled;


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

        /// <summary>
        /// Regec description
        /// </summary>
        public string RegexDesc { get { return Labels.SettingsControl_RegexDesc; } }
        /// <summary>
        /// Apply translation
        /// </summary>
        public string ApplyLabel { get { return Labels.SettingsControl_ApplyLabel; } }

        /// <summary>
        /// Ignore first lines translation
        /// </summary>
        public string IgnoreFirstLinesContent { get { return Labels.SettingsControl_IgnoreFirstLinesContent; } }

        /// <summary>
        /// Move up label
        /// </summary>
        public string MoveUpLabel { get { return Labels.SettingsControl_MoveUp; } }

        /// <summary>
        /// Move down label
        /// </summary>
        public string MoveDownLabel { get { return Labels.SettingsControl_MoveDown; } }

        /// <summary>
        /// Regex grid remove label
        /// </summary>
        public string RegexGridRemoveLabel { get { return Labels.SettingsControl_RegexGridRemove; } }

        /// <summary>
        /// Regex label
        /// </summary>
        public string RegexLabel { get { return Labels.SettingsControl_RegexLabel; } }

        /// <summary>
        /// regex grid label
        /// </summary>
        public string RegexGridLabel { get { return Labels.SettingsControl_RegexGridContent; } }

        /// <summary>
        /// Regex grid value
        /// </summary>
        public string RegexGridValueLabel { get { return Labels.SettingsControl_RegexGridValue; } }

        /// <summary>
        /// regex add new label
        /// </summary>
        public string AddNewRegexLabel { get { return Labels.SettingsControl_AddNewRegex; } }

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
        /// Allowed lines
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

        /// <summary>
        /// Ignore first lines enabled
        /// </summary>
        public bool IgnoreFirstLinesEnabled
        {
            get { return ignoreFirstLinesEnabled; }
            set
            {
                ignoreFirstLinesEnabled = value;
                optionsPage.IgnoreStartingLines = ignoreFirstLinesEnabled;
            }
        }

        /// <summary>
        /// List settings item
        /// </summary>
        public ObservableCollection<SettingItem> SettingsItems { get; set; } = new ObservableCollection<SettingItem>();

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
                item.PropertyUpdated += OnPropertyUpdated;
                item.ItemMoved += OnItemMoved;
                item.Deleted += OnDeleted;
                SettingsItems.Add(item);
            }

            this.optionsPage = optionsPage;
            autoSaveEnabled = optionsPage.IsAutoSaveEnabled;
            allowedLines = optionsPage.AllowedLines;
            ignoreFirstLinesEnabled = optionsPage.IgnoreStartingLines;
            this.DataContext = this;

            SettingsItems.CollectionChanged += OnSettingsListUpdated;
        }

        #endregion

        #region Events

        /// <summary>
        /// On settings list updated
        /// <para>Allow to manage updates on settings list for add or remove update events</para>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnSettingsListUpdated(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (var item in e.NewItems)
                {
                    (item as SettingItem).PropertyUpdated += OnPropertyUpdated;
                    (item as SettingItem).ItemMoved += OnItemMoved;
                    (item as SettingItem).Deleted += OnDeleted;
                }
            }

            if (e.OldItems != null)
            {
                foreach (var item in e.OldItems)
                {
                    (item as SettingItem).PropertyUpdated -= OnPropertyUpdated;
                    (item as SettingItem).ItemMoved -= OnItemMoved;
                    (item as SettingItem).Deleted -= OnDeleted;
                }
            }

            SaveState();
        }

        /// <summary>
        /// On <see cref="SettingItem"/> updated
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnPropertyUpdated(object sender, EventArgs e)
        {
            SaveState();
        }

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

        /// <summary>
        /// Apply : save current state
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Apply_Click(object sender, RoutedEventArgs e)
        {
            SaveState();
        }

        /// <summary>
        /// Save current state on lost focus
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserControl_LostFocus(object sender, RoutedEventArgs e)
        {
            SaveState();
        }

        /// <summary>
        /// Onitem moved
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnItemMoved(object sender, MoveEventArgs e)
        {
            var item = sender as SettingItem;
            if (item == null)
            {
                return;
            }

            int index = SettingsItems.IndexOf(item);

            if (e.moveAction == MoveEnum.Up && index > 0)
            {
                SettingsItems.Move(index, index - 1);
            }
            else if (e.moveAction == MoveEnum.Down && item != SettingsItems.Last())
            {
                SettingsItems.Move(index, index + 1);
            }

            SaveState();
        }

        private void OnDeleted(object sender, EventArgs e)
        {
            var item = sender as SettingItem;
            if (item == null)
            {
                return;
            }
            try
            {
                item.Deleted -= OnDeleted;
                item.ItemMoved -= OnItemMoved;
                item.PropertyUpdated -= OnPropertyUpdated;

                SettingsItems.Remove(item);
            }
            finally
            {
                if (!SettingsItems.Any())
                {
                    btn_AddRegexRule_Click(this, new RoutedEventArgs());
                }

                SaveState();
            }
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Save current state
        /// </summary>
        private void SaveState()
        {
            optionsPage.SetSettingItems(SettingsItems.ToList());
        }

        private void btn_AddRegexRule_Click(object sender, RoutedEventArgs e)
        {
            SettingItem item = new SettingItem();
            SettingsItems.Add(item);
        }
        #endregion
    }
}
