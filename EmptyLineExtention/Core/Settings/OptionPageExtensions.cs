using System.Collections.Generic;
using Newtonsoft.Json;

namespace EmptyLineExtention.Core.Settings
{
    /// <summary>
    /// Option page extension
    /// </summary>
    public static class OptionPageExtensions
    {
        /// <summary>
        /// Get setting items
        /// </summary>
        /// <param name="optionPage"></param>
        /// <returns></returns>
        public static List<SettingItem> GetSettingItems(this OptionPage optionPage)
        {
            return JsonConvert.DeserializeObject<List<SettingItem>>(optionPage.FilesConfigurations);
        }

        /// <summary>
        /// Set setting items
        /// </summary>
        /// <param name="optionPage"></param>
        /// <param name="items"></param>
        public static void SetSettingItems(this OptionPage optionPage, List<SettingItem> items)
        {
            optionPage.FilesConfigurations = JsonConvert.SerializeObject(items);
        }
    }
}
