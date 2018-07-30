using EnvDTE;

namespace EmptyLineExtention.Services
{
    public class EmptyLineService
    {
        public static void FormatDocument(Document document, bool CanUseSelection, int AllowedLines)
        {
            // Get the current Doc
            TextDocument activeDoc = document.Object() as TextDocument;

            // set default start point
            int startPoint = 1;
            int endPoint = activeDoc.EndPoint.Line;

            // initialise edit point
            var editPoint = activeDoc.CreateEditPoint(activeDoc.StartPoint);

            // check if there is a selection
            if (!activeDoc.Selection.IsEmpty && CanUseSelection)
            {
                startPoint = activeDoc.Selection.TopLine;
                endPoint = activeDoc.Selection.BottomLine;
                editPoint.LineDown(startPoint - 1);
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

                    if (numberOfEmptyLines > AllowedLines)
                    {
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
                }

                if (lineDeleted)
                {
                    number--;
                }
                editPoint.LineDown(1);
            }
        }

    }
}
