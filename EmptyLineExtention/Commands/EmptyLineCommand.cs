using System;
using System.ComponentModel.Design;
using EnvDTE;
using Microsoft.VisualStudio.Shell;

namespace EmptyLineExtention.Commands
{
    /// <summary>
    /// Command handler
    /// </summary>
    internal sealed class EmptyLineCommand
    {
        /// <summary>
        /// Command ID.
        /// </summary>
        public const int CommandId = 0x0100;

        /// <summary>
        /// Command menu group (command set GUID).
        /// </summary>
        public static readonly Guid CommandSet = new Guid("40013f71-5734-44b6-ad7d-2de7ae21de78");

        /// <summary>
        /// VS Package that provides this command, not null.
        /// </summary>
        private readonly Package package;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmptyLineCommand"/> class.
        /// Adds our command handlers for menu (commands must exist in the command table file)
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        private EmptyLineCommand(Package package)
        {
            if (package == null)
            {
                throw new ArgumentNullException("package");
            }

            this.package = package;

            OleMenuCommandService commandService = this.ServiceProvider.GetService(typeof(IMenuCommandService)) as OleMenuCommandService;
            if (commandService != null)
            {
                var menuCommandID = new CommandID(CommandSet, CommandId);
                var menuItem = new MenuCommand(this.MenuItemCallback, menuCommandID);
                commandService.AddCommand(menuItem);
            }
        }

        /// <summary>
        /// Gets the instance of the command.
        /// </summary>
        public static EmptyLineCommand Instance
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the service provider from the owner package.
        /// </summary>
        private IServiceProvider ServiceProvider
        {
            get
            {
                return this.package;
            }
        }

        /// <summary>
        /// Initializes the singleton instance of the command.
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        public static void Initialize(Package package)
        {
            Instance = new EmptyLineCommand(package);
        }

        /// <summary>
        /// This function is the callback used to execute the command when the menu item is clicked.
        /// See the constructor to see how the menu item is associated with this function using
        /// OleMenuCommandService service and MenuCommand class.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Event args.</param>
        private void MenuItemCallback(object sender, EventArgs e)
        {
            // Get the current Doc
            EnvDTE80.DTE2 applicationObject = ServiceProvider.GetService(typeof(DTE)) as EnvDTE80.DTE2;
            TextDocument activeDoc = applicationObject.ActiveDocument.Object() as TextDocument;

            // set default start point
            int startPoint = 1;
            int endPoint = activeDoc.EndPoint.Line;

            // initialise edit point
            var editPoint = activeDoc.CreateEditPoint(activeDoc.StartPoint);

            // check if there is a selection
            if (!activeDoc.Selection.IsEmpty)
            {
                startPoint = activeDoc.Selection.TopLine;
                endPoint = activeDoc.Selection.BottomLine;
                editPoint.LineDown(startPoint - 1);
            }

            // remove all multiple space between start and end point
            bool isLastLineEmpty = false;
            for (int number = startPoint; number <= endPoint; number++)
            {
                bool lineDeleted = false;
                string line = editPoint.GetLines(number, number + 1);
                editPoint.CreateEditPoint();
                if (string.IsNullOrWhiteSpace(line))
                {
                    if (!isLastLineEmpty)
                    {
                        isLastLineEmpty = true;
                    }
                    else
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
                    isLastLineEmpty = false;
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
