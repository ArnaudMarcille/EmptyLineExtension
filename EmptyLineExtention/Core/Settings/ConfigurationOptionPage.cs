using System.Runtime.InteropServices;
using Microsoft.VisualStudio.Shell;

namespace EmptyLineExtention.Core.Settings
{
    [Guid("41237535-0E8A-4C06-944C-32DE00CE3A50")]
    public class ConfigurationOptionPage : DialogPage
    {
        private SettingItem[] filesConfigurations;

        public SettingItem[] FilesConfigurations
        {
            get { return filesConfigurations; }
            set { filesConfigurations = value; }
        }

        public ConfigurationOptionPage()
        {
            filesConfigurations = new SettingItem[] { };
        }
    }
}
