﻿using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using EmptyLineExtention.Core.Settings;
using Microsoft.VisualStudio.Shell;

namespace EmptyLineExtention.Settings
{
    /// <summary>
    /// This is the class that implements the package exposed by this assembly.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The minimum requirement for a class to be considered a valid package for Visual Studio
    /// is to implement the IVsPackage interface and register itself with the shell.
    /// This package uses the helper classes defined inside the Managed Package Framework (MPF)
    /// to do it: it derives from the Package class that provides the implementation of the
    /// IVsPackage interface and uses the registration attributes defined in the framework to
    /// register itself and its components with the shell. These attributes tell the pkgdef creation
    /// utility what data to put into .pkgdef file.
    /// </para>
    /// <para>
    /// To get loaded into VS, the package must be referred by &lt;Asset Type="Microsoft.VisualStudio.VsPackage" ...&gt; in .vsixmanifest file.
    /// </para>
    /// </remarks>
    [PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true)]
    [InstalledProductRegistration("#1110", "#1112", "1.0", IconResourceID = 1400)] // Info on this package for Help/About
    [ProvideOptionPage(typeof(OptionPage), Core.Constants.AppName, Core.Constants.SettingsPageName, 0, 0, true)]
    [Guid(SettingsPackage.PackageGuidString)]
    public sealed class SettingsPackage : AsyncPackage
    {
        /// <summary>
        /// SettingsPackage GUID string.
        /// </summary>
        public const string PackageGuidString = "4c669564-48ec-4813-9df6-609b4b286ace";

        /// <summary>
        /// Initializes a new instance of the <see cref="SettingsPackage"/> class.
        /// </summary>
        public SettingsPackage()
        {
            // Inside this method you can place any initialization code that does not require
            // any Visual Studio service because at this point the package object is created but
            // not sited yet inside Visual Studio environment. The place to do all the other
            // initialization is the Initialize method.
        }

        #region Properties

        /// <summary>
        /// IsAutoSaveEnabled accessor
        /// </summary>
        public bool IsAutoSaveEnabled
        {
            get
            {
                OptionPage page = (OptionPage)GetDialogPage(typeof(OptionPage));
                return (page != null ? page.IsAutoSaveEnabled : false);
            }
        }

        #endregion

        #region Package Members

        /// <summary>
        /// Initialization of the package; this method is called right after the package is sited, so this is the place
        /// where you can put all the initialization code that rely on services provided by VisualStudio.
        /// </summary>
        protected override System.Threading.Tasks.Task InitializeAsync(CancellationToken cancellationToken, IProgress<ServiceProgressData> progress)
        {
            return base.InitializeAsync(cancellationToken, progress);
        }

        #endregion
    }
}
