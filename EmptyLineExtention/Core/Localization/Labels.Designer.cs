﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EmptyLineExtention.Core.Localization {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Labels {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Labels() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("EmptyLineExtention.Core.Localization.Labels", typeof(Labels).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Allowed lines (default is 1) :.
        /// </summary>
        internal static string SettingsControl_AllowedLines {
            get {
                return ResourceManager.GetString("SettingsControl_AllowedLines", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Above, you can set the number of tolerated lines, if you want no line, just set it to 0. The default value is setted to 1, if you didn&apos;t set any value, the default value will be taken.
        ///Below, you can set the regex requests for your specific cases, if there is multiple match, the last one in the list will be taken..
        /// </summary>
        internal static string SettingsControl_AllowedLinesDesc {
            get {
                return ResourceManager.GetString("SettingsControl_AllowedLinesDesc", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Apply.
        /// </summary>
        internal static string SettingsControl_ApplyLabel {
            get {
                return ResourceManager.GetString("SettingsControl_ApplyLabel", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Enable auto format on document save..
        /// </summary>
        internal static string SettingsControl_AutoFormat_Content {
            get {
                return ResourceManager.GetString("SettingsControl_AutoFormat_Content", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Settings.
        /// </summary>
        internal static string SettingsControl_Header {
            get {
                return ResourceManager.GetString("SettingsControl_Header", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Regex.
        /// </summary>
        internal static string SettingsControl_RegexLabel {
            get {
                return ResourceManager.GetString("SettingsControl_RegexLabel", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Allowed lines.
        /// </summary>
        internal static string SettingsControl_ValueLabel {
            get {
                return ResourceManager.GetString("SettingsControl_ValueLabel", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Remove empty lines.
        /// </summary>
        internal static string Undo_Removed {
            get {
                return ResourceManager.GetString("Undo_Removed", resourceCulture);
            }
        }
    }
}
