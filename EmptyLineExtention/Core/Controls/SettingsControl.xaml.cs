using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
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
        private readonly ObservableCollection<SettingItem> settingsItems = new ObservableCollection<SettingItem>();

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
        /// ApplyLabel
        /// </summary>
        public string ApplyLabel { get { return Labels.SettingsControl_ApplyLabel; } }

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
                item.PropertyUpdated += OnPropertyUpdated;
                item.ItemMoved += OnItemMoved;
                settingsItems.Add(item);
            }

            this.optionsPage = optionsPage;
            autoSaveEnabled = optionsPage.IsAutoSaveEnabled;
            allowedLines = optionsPage.AllowedLines;
            SettingsGrid.ItemsSource = settingsItems;
            SettingsGrid.AutoGenerateColumns = false;
            SettingsGrid.CanUserAddRows = true;

            RegexColumn.Header = Labels.SettingsControl_RegexLabel;
            ValueColumn.Header = Labels.SettingsControl_ValueLabel;
            this.DataContext = this;

            settingsItems.CollectionChanged += OnSettingsListUpdated;
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
                }
            }

            if (e.OldItems != null)
            {
                foreach (var item in e.OldItems)
                {
                    (item as SettingItem).PropertyUpdated -= OnPropertyUpdated;
                    (item as SettingItem).ItemMoved -= OnItemMoved;
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

            int index = settingsItems.IndexOf(item);

            if (e.moveAction == MoveEnum.Up && index > 0)
            {
                settingsItems.Move(index, index - 1);
            }
            else if (e.moveAction == MoveEnum.Down && SettingsGrid.SelectedIndex < (settingsItems.Count - 1))
            {
                settingsItems.Move(index, index + 1);
            }

            SaveState();
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Save current state
        /// </summary>
        private void SaveState()
        {
            optionsPage.SetSettingItems(settingsItems.ToList());
        }

        #endregion

    }
}
