﻿namespace EmptyLineExtention.Core
{
    /// <summary>
    /// Constants class
    /// </summary>
    public class Constants
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

        #endregion

    }
}
