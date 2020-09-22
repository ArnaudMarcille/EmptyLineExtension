using System;
using System.Windows.Input;
using Newtonsoft.Json;

namespace EmptyLineExtention.Core.Settings
{
    /// <summary>
    /// Setting item
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class SettingItem
    {
        #region Event
        /// <summary>
        /// Property updated
        /// </summary>
        public event EventHandler PropertyUpdated;

        /// <summary>
        /// Item Moved
        /// </summary>
        public event EventHandler<MoveEventArgs> ItemMoved;

        /// <summary>
        /// Delete event
        /// </summary>
        public event EventHandler Deleted;

        #endregion

        #region Fields

        /// <summary>
        /// key
        /// </summary>
        private string key;

        /// <summary>
        /// value
        /// </summary>
        private int value;

        #endregion

        #region Properties

        /// <summary>
        /// Key
        /// </summary>
        [JsonProperty]
        public string Key
        {
            get { return key; }
            set
            {
                key = value;
                PropertyUpdated?.Invoke(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Value
        /// </summary>
        [JsonProperty]
        public int Value
        {
            get { return value; }
            set
            {
                this.value = value;
                PropertyUpdated?.Invoke(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// UpCommand
        /// </summary>
        public ICommand UpCommand { get; set; }

        /// <summary>
        /// Down command
        /// </summary>
        public ICommand DownCommand { get; set; }

        /// <summary>
        /// Delete command
        /// </summary>
        public ICommand DeleteCommand { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public SettingItem()
        {
            UpCommand = new Command(OnUpPress);
            DownCommand = new Command(OnDownpress);
            DeleteCommand = new Command(OnDelete);
        }

        #endregion

        #region Commands

        /// <summary>
        /// Down button press
        /// </summary>
        private void OnDownpress()
        {
            ItemMoved?.Invoke(this, new MoveEventArgs(MoveEnum.Down));
        }

        /// <summary>
        /// Up button press
        /// </summary>
        private void OnUpPress()
        {
            ItemMoved?.Invoke(this, new MoveEventArgs(MoveEnum.Up));
        }

        /// <summary>
        /// On delete
        /// </summary>
        private void OnDelete()
        {
            Deleted?.Invoke(this, EventArgs.Empty);
        }

        #endregion
    }
}
