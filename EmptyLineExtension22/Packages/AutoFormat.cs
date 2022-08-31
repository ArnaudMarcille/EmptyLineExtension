using System;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using EmptyLineExtension22.Options;
using EmptyLineExtension22.Services;
using EnvDTE;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.OLE.Interop;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.Win32;

namespace EmptyLineExtension22.Packages
{
    /// <summary>
    /// This is the class that implements the package exposed by this assembly.
    /// </summary>
    [PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true)]
    [ProvideAutoLoad(UIContextGuids80.NoSolution, PackageAutoLoadFlags.BackgroundLoad)]
    [ProvideAutoLoad(UIContextGuids80.SolutionExists, PackageAutoLoadFlags.BackgroundLoad)]
    [ProvideAutoLoad(UIContextGuids80.SolutionHasSingleProject, PackageAutoLoadFlags.BackgroundLoad)]
    [ProvideAutoLoad(UIContextGuids80.SolutionHasMultipleProjects, PackageAutoLoadFlags.BackgroundLoad)]
    [Guid(AutoFormat.PackageGuidString)]
    public sealed class AutoFormat : AsyncPackage
    {

        /// <summary>
        /// Auto formatter instance
        /// </summary>
        private RunningDocTableEvents plugin;

        /// <summary>
        /// Is a document update in progress
        /// </summary>
        private bool working;

        /// <summary>
        /// AutoFormat GUID string.
        /// </summary>
        public const string PackageGuidString = "9b6fbe0a-76b8-4961-a738-9d0e0111cfb0";

        /// <summary>
        /// Initializes a new instance of the <see cref="AutoFormat"/> class.
        /// </summary>
        public AutoFormat()
        {
        }

        #region Package Members

        /// <summary>
        /// Initialization of the package; this method is called right after the package is sited, so this is the place
        /// where you can put all the initialization code that rely on services provided by VisualStudio.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token to monitor for initialization cancellation, which can occur when VS is shutting down.</param>
        /// <param name="progress">A provider for progress updates.</param>
        /// <returns>A task representing the async work of package initialization, or an already completed task if there is none. Do not return null from this method.</returns>
        protected override async Task InitializeAsync(CancellationToken cancellationToken, IProgress<ServiceProgressData> progress)
        {
            await this.JoinableTaskFactory.SwitchToMainThreadAsync(cancellationToken);

            RunningDocumentTable runningDocumentTable = new RunningDocumentTable();
            plugin = new RunningDocTableEvents(this);
            runningDocumentTable.Advise(plugin);
            plugin.BeforeSave += OnBeforeSaveAsync;
        }

        /// <summary>
        /// Executed before save async
        /// </summary>
        /// <param name="arg1"></param>
        /// <param name="arg2"></param>
        /// <returns></returns>
        private async Task OnBeforeSaveAsync(object arg1, EventArgs arg2)
        {
            if (working || !IsAutoFormatEnabled())
            {
                return;
            }

            working = true;

            try
            {
                DocumentView docView = await VS.Documents.GetActiveDocumentViewAsync();
                if (docView == null)
                {
                    return;
                }

                int allowedLines = GetAllowedLinesValue(docView) ?? 1;
                EmptyLineService.FormatDocument(docView, false, allowedLines, GetIgnoreStartingLines());
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
            finally
            {
                working = false;
            }
        }

        /// <summary>
        /// Return true if auto format is enabled
        /// </summary>
        /// <returns></returns>
        private bool IsAutoFormatEnabled()
        {
            try
            {
                OptionPage optionProperties = (OptionPage)GetDialogPage(typeof(OptionPage));
                return optionProperties.IsAutoSaveEnabled;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Get the value of AllowedLines property
        /// </summary>
        /// <returns></returns>
        private int? GetAllowedLinesValue(DocumentView document)
        {
            try
            {
                ThreadHelper.ThrowIfNotOnUIThread();
                OptionPage optionProperties = (OptionPage)GetDialogPage(typeof(OptionPage));
                return EmptyLineService.ComputeAllowedLines(document.FilePath, optionProperties);
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Get the value of IgnoreStartingLines property
        /// </summary>
        /// <returns></returns>
        private bool GetIgnoreStartingLines()
        {
            try
            {
                OptionPage optionProperties = (OptionPage)GetDialogPage(typeof(OptionPage));
                return optionProperties.IgnoreStartingLines;
            }
            catch (Exception)
            {
                return false;
            }
        }

        #endregion
    }
}
