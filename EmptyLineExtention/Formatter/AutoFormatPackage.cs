
using System;
using System.Runtime.InteropServices;
using System.Threading;
using EmptyLineExtention.Core.Settings;
using EnvDTE;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Task = System.Threading.Tasks.Task;

namespace EmptyLineExtention.Formatter
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
    [InstalledProductRegistration("#2110", "#2112", "1.0", IconResourceID = 2400)] // Info on this package for Help/About
    [Guid(AutoFormatPackage.PackageGuidString)]
    [ProvideAutoLoad(UIContextGuids80.NoSolution, PackageAutoLoadFlags.BackgroundLoad)]
    [ProvideAutoLoad(UIContextGuids80.SolutionExists, PackageAutoLoadFlags.BackgroundLoad)]
    [ProvideAutoLoad(UIContextGuids80.SolutionHasSingleProject, PackageAutoLoadFlags.BackgroundLoad)]
    [ProvideAutoLoad(UIContextGuids80.SolutionHasMultipleProjects, PackageAutoLoadFlags.BackgroundLoad)]
    public sealed class AutoFormatPackage : AsyncPackage
    {
        /// <summary>
        /// Auto formatter instance
        /// </summary>
        private AutoFormatter plugin;

        /// <summary>
        /// AutoFormatPackage GUID string.
        /// </summary>
        public const string PackageGuidString = "37a11c54-7af9-46bf-8788-0e77cb514bd7";

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

        /// <summary>
        /// Initializes a new instance of the <see cref="AutoFormatPackage"/> class.
        /// </summary>
        public AutoFormatPackage()
        {
            // Inside this method you can place any initialization code that does not require
            // any Visual Studio service because at this point the package object is created but
            // not sited yet inside Visual Studio environment. The place to do all the other
            // initialization is the Initialize method.
        }

        #region Package Members

        protected override async Task InitializeAsync(CancellationToken cancellationToken, IProgress<ServiceProgressData> progress)
        {
            // Switches to the UI thread in order to consume some services used in command initialization
            await JoinableTaskFactory.SwitchToMainThreadAsync(cancellationToken);

            var dte = await GetServiceAsync(typeof(DTE)) as DTE;
            RunningDocumentTable runningDocumentTable = new RunningDocumentTable();
            plugin = new AutoFormatter(dte, runningDocumentTable, this);
            runningDocumentTable.Advise(plugin);

            base.Initialize();
        }
       

        #endregion
    }
}
