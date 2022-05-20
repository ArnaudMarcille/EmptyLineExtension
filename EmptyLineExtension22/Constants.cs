using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmptyLineExtension22
{
    public static class Constants
    {
        #region App

        /// <summary>
        /// App name constant
        /// </summary>
        public const string AppName = "EmptyLine Extension";

        #endregion

        #region Settings

        /// <summary>
        /// Name of the tool settings page
        /// </summary>
        public const string SettingsPageName = "General";

        /// <summary>
        /// name of the auto save property
        /// </summary>
        public const string AutoSavePropertyName = "Format on save";

        /// <summary>
        /// description of the auto save property
        /// </summary>
        public const string AutoSavePropertyDesc = "Enable auto format at the current document save.";

        /// <summary>
        /// name of the auto save property
        /// </summary>
        public const string AllowedLines = "Number of allowed line";

        /// <summary>
        /// description of the auto save property
        /// </summary>
        public const string AllowedLinesDesc = "Settings for set how many lines are allowed to subsist";

        /// <summary>
        /// Allowed line default values
        /// </summary>
        public const int DefaultAllowedLines = 1;

        /// <summary>
        /// name of the managing first line property
        /// </summary>
        public const string IgnoreFirstLine = "Manage first line";

        /// <summary>
        /// description  of the managing first line property
        /// </summary>
        public const string IgnoreFirstLineDesc = "Allow to manage the first lines of the file";

        #endregion
    }
}
