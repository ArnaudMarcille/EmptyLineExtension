using System.Collections.Generic;
using EmptyLineExtention.Core.Settings;
using EnvDTE;
using Newtonsoft.Json;

namespace EmptyLineExtention.Services
{
    /// <summary>
    /// Empty line methods
    /// </summary>
    public class EmptyLineService
    {
        /// <summary>
        /// Format document for remove empty line 
        /// </summary>
        /// <param name="document"></param>
        /// <param name="CanUseSelection"></param>
        /// <param name="AllowedLines"></param>
        public static void FormatDocument(Document document, bool CanUseSelection, int AllowedLines, _DTE dte, bool ignoreStartLines)
        {
            Microsoft.VisualStudio.Shell.ThreadHelper.ThrowIfNotOnUIThread();

            // Get the current Doc
            TextDocument activeDoc = document.Object() as TextDocument;

            // set default start point
            int startPoint = 1;
            int endPoint = activeDoc.EndPoint.Line;
            // initialise edit point
            var editPoint = activeDoc.CreateEditPoint(activeDoc.StartPoint);

            // Set is start line variable
            bool isStartLines = true;

            // check if there is a selection
            if (!activeDoc.Selection.IsEmpty && CanUseSelection)
            {
                startPoint = activeDoc.Selection.TopLine;
                endPoint = activeDoc.Selection.BottomLine;
                editPoint.LineDown(startPoint - 1);
                isStartLines = false;
            }

            // remove all multiple space between start and end point
            int numberOfEmptyLines = 0;
            for (int number = startPoint; number <= endPoint; number++)
            {
                bool lineDeleted = false;
                string line = editPoint.GetLines(number, number + 1);
                editPoint.CreateEditPoint();
                if (string.IsNullOrWhiteSpace(line))
                {
                    numberOfEmptyLines++;

                    // Verify if the managed lines are the first one and if trim is needed
                    bool manageFirstLines = ((ignoreStartLines && !isStartLines) || !ignoreStartLines);
                    if (numberOfEmptyLines > AllowedLines && manageFirstLines)
                    {
                        editPoint.StartOfLine();
                        // Delete "spaces"
                        editPoint.Delete(line.Length);
                        // Delete breakline
                        editPoint.Delete(-1);

                        lineDeleted = true;

                        if (!activeDoc.Selection.IsEmpty)
                            endPoint = activeDoc.Selection.BottomLine;
                        else
                            endPoint = activeDoc.EndPoint.Line;
                    }
                }
                else
                {
                    numberOfEmptyLines = 0;
                    isStartLines = false;
                }

                if (lineDeleted)
                {
                    number--;
                }
                editPoint.LineDown(1);
            }
        }

        /// <summary>
        /// Compute allowed lines
        /// </summary>
        /// <param name="documentName"></param>
        /// <param name="optionPage"></param>
        /// <returns></returns>
        public static int? ComputeAllowedLines(string documentName, OptionPage optionPage)
        {
            int? allowedLines = null;

            // try to read file configuration
            if (!string.IsNullOrEmpty(optionPage.FilesConfigurations))
            {
                // file configuration is saved in string, so it's needs to be deseriasiled
                List<SettingItem> items = JsonConvert.DeserializeObject<List<SettingItem>>(optionPage.FilesConfigurations);

                // Call Regex service to find which allowed line to use
                // If multiple match founds, the last one is taked
                var result = RegexService.FindAllowedLinesForDocument(documentName, items);

                if (result != null)
                {
                    allowedLines = result.Value;
                }
                /// if <see cref="optionPage.DefaultAllowedLines"/> is set to zero: keep null
                else if (optionPage.DefaultAllowedLines != 0)
                {
                    allowedLines = optionPage.DefaultAllowedLines;
                }
            }
            /// If there is no <see cref="OptionPage.FilesConfigurations"/>, then use default settings
            /// if <see cref="OptionPage.DefaultAllowedLines"/> is set to zero: keep null
            else if (optionPage.DefaultAllowedLines != 0)
            {
                allowedLines = optionPage.DefaultAllowedLines;
            }

            return allowedLines;
        }
    }
}
