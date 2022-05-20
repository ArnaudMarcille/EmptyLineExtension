using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using EmptyLineExtension22.Options.Ressources;

namespace EmptyLineExtension22.Services
{
    /// <summary>
    /// Regex utility service 
    /// </summary>
    public static class RegexService
    {
        /// <summary>
        /// Return allowed line for given document (take the last match)
        /// </summary>
        /// <returns></returns>
        public static SettingItem FindAllowedLinesForDocument(string documentFullName, IList<SettingItem> settingItems)
        {
            if (settingItems == null || !settingItems.Any())
            {
                return null;
            }

            int i = 0;

            SettingItem compatibleItem = null;

            while (i < settingItems.Count)
            {
                try
                {
                    Regex regex = new Regex(settingItems[i].Key);

                    if (regex.IsMatch(documentFullName))
                    {
                        compatibleItem = settingItems[i];
                    }
                }
                catch { }
                i++;
            }

            return compatibleItem;
        }
    }
}
