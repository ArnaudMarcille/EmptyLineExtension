using System;

namespace EmptyLineExtention.Core.Settings
{
    public class SettingItem
    {
        public event EventHandler PropertyUpdated;

        private string key;

        public string Key
        {
            get { return key; }
            set
            {
                key = value;
                PropertyUpdated?.Invoke(this, EventArgs.Empty);
            }
        }

        private int value;

        public int Value
        {
            get { return value; }
            set
            {
                this.value = value;
                PropertyUpdated?.Invoke(this, EventArgs.Empty);
            }
        }

    }
}
