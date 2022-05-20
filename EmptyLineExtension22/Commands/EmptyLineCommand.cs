using EmptyLineExtension22.Options;
using EmptyLineExtension22.Services;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Operations;

namespace EmptyLineExtension22.Commands
{
    [Command(PackageGuids.guidEmptyLineCommandPackageCmdSetString, 0x0100)]
    internal sealed class EmptyLineCommand : BaseCommand<EmptyLineCommand>
    {
        protected override async Task ExecuteAsync(OleMenuCmdEventArgs e)
        {
            DocumentView docView = await VS.Documents.GetActiveDocumentViewAsync();
            if (docView == null)
            {
                return;
            }

            int allowedLines = GetAllowedLinesValue(docView) ?? 1;
            EmptyLineService.FormatDocument(docView, true, allowedLines, GetIgnoreStartingLines());
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
                OptionPage optionProperties = (OptionPage)Package.GetDialogPage(typeof(OptionPage));
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
                OptionPage optionProperties = (OptionPage)Package.GetDialogPage(typeof(OptionPage));
                return optionProperties.IgnoreStartingLines;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
