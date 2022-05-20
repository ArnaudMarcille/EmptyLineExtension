using System.Collections.Generic;
using System.Linq;
using EmptyLineExtension22.Options;
using EmptyLineExtension22.Options.Ressources;
using Microsoft.VisualStudio.Text;
using Newtonsoft.Json;

namespace EmptyLineExtension22.Services
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
        /// <param name="canUseSelection"></param>
        /// <param name="allowedLines"></param>
        public static void FormatDocument(DocumentView document, bool canUseSelection, int allowedLines, bool deleteAllEmptyStartLines)
        {
            if (document?.TextView == null) return;


            SnapshotPoint position = document.TextView.Caret.Position.BufferPosition;

            using (ITextEdit edit = document.TextBuffer.CreateEdit())
            {
                // Replace if there is no selection
                if (!canUseSelection || document.TextView.Selection.IsEmpty)
                {
                    ReplaceContentText(allowedLines, deleteAllEmptyStartLines, edit, document, null, null);
                }
                // Replace on the selection
                else
                {
                    // foreach selection, replace the content
                    foreach (var selection in document.TextView.Selection.SelectedSpans)
                    {
                        ReplaceContentText(allowedLines, deleteAllEmptyStartLines, edit, document, selection.Start.Position, selection.End.Position);
                    }

                }
                edit.Apply();
            }

            // Move caret to last position

            try
            {
                document.TextView.Caret.MoveTo(position);
            }
            catch
            {
                // TODO WATCH WHY
            }
        }

        /// <summary>
        /// Replace text content
        /// </summary>
        /// <param name="allowedLines"></param>
        /// <param name="deleteAllEmptyStartLines"></param>
        /// <param name="edit"></param>
        /// <param name="document"></param>
        /// <param name="startPosition"></param>
        /// <param name="endPosition"></param>
        private static void ReplaceContentText(int allowedLines, bool deleteAllEmptyStartLines, ITextEdit edit, DocumentView document, int? startPosition, int? endPosition)
        {
            var textView = document.TextView;
            var lines = textView.TextViewLines;
            int count = 0;
            foreach (var line in lines)
            {
                if (startPosition.HasValue && endPosition.HasValue && (startPosition.Value > line.Start.Position || endPosition.Value < line.Start.Position))
                {
                    continue;
                }

                var snapshot = new SnapshotSpan(line.Start, line.EndIncludingLineBreak);
                string text = snapshot.GetText();
                bool isEmpty = string.IsNullOrWhiteSpace(text);

                if (isEmpty)
                {
                    count++;
                }
                else
                {
                    deleteAllEmptyStartLines = false;
                    count = 0;
                }

                if (isEmpty && (count > allowedLines || deleteAllEmptyStartLines))
                {
                    if (line == lines.Last())
                    {
                        var lastLine = lines.ElementAt(lines.IndexOf(line) - 1);
                        snapshot = new SnapshotSpan(lastLine.Start, lastLine.EndIncludingLineBreak);
                    }

                    edit.Delete(snapshot.Start.Position, snapshot.Length);
                }

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
                else
                {
                    allowedLines = optionPage.DefaultAllowedLines;
                }
            }
            /// If there is no <see cref="OptionPage.FilesConfigurations"/>, then use default settings
            else
            {
                allowedLines = optionPage.DefaultAllowedLines;
            }

            return allowedLines;
        }
    }
}
