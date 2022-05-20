using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace EmptyLineExtension22.Options.Ressources
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
            try
            {
                return JsonConvert.DeserializeObject<List<SettingItem>>(optionPage.FilesConfigurations);
            }
            catch
            {
                return new List<SettingItem>();
            }
        }

        /// <summary>
        /// Set setting items
        /// </summary>
        /// <param name="optionPage"></param>
        /// <param name="items"></param>
        public static void SetSettingItems(this OptionPage optionPage, List<SettingItem> items)
        {
            try
            {
                optionPage.FilesConfigurations = JsonConvert.SerializeObject(items);
            }
            catch { }
        }
    }
}
