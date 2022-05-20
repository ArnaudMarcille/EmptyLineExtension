using System.Runtime.InteropServices;
using System.Threading;

namespace EmptyLineExtension22.Options
{
    /// <summary>
    /// This is the class that implements the package exposed by this assembly.
    /// </summary>
    [PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true)]
    [InstalledProductRegistration("#1110", "#1112", "1.0", IconResourceID = 1400)] // Info on this package for Help/About
    [ProvideOptionPage(typeof(OptionPage), "Empty Line extension", "General", 0, 0, true)]
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
        protected override Task InitializeAsync(CancellationToken cancellationToken, IProgress<ServiceProgressData> progress)
        {
            return base.InitializeAsync(cancellationToken, progress);
        }

        #endregion
    }
}
