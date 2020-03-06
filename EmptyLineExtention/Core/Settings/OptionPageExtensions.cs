using System.Collections.Generic;
using Newtonsoft.Json;

namespace EmptyLineExtention.Core.Settings
{
    public static class OptionPageExtensions
    {
        public static List<SettingItem> GetSettingItems(this OptionPage optionPage)
        {
            return JsonConvert.DeserializeObject<List<SettingItem>>(optionPage.FilesConfigurations);
        }

        public static void SetSettingItems(this OptionPage optionPage, List<SettingItem> items)
        {
            optionPage.FilesConfigurations = JsonConvert.SerializeObject(items);
        }
    }
}
