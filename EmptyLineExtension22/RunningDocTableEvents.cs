using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmptyLineExtension22.Services;
using EnvDTE;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell.Interop;

namespace EmptyLineExtension22
{
    public class RunningDocTableEvents : IVsRunningDocTableEvents3
    {
        #region Members

        private RunningDocumentTable mRunningDocumentTable;

        public event Func<object, EventArgs, Task> BeforeSave;

        #endregion

        #region Constructor

        public RunningDocTableEvents(Package aPackage)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            mRunningDocumentTable = new RunningDocumentTable(aPackage);
            mRunningDocumentTable.Advise(this);
        }

        #endregion

        #region IVsRunningDocTableEvents3 implementation

        public int OnAfterAttributeChange(uint docCookie, uint grfAttribs)
        {
            return VSConstants.S_OK;
        }

        public int OnAfterAttributeChangeEx(uint docCookie, uint grfAttribs, IVsHierarchy pHierOld, uint itemidOld, string pszMkDocumentOld, IVsHierarchy pHierNew, uint itemidNew, string pszMkDocumentNew)
        {
            return VSConstants.S_OK;
        }

        public int OnAfterDocumentWindowHide(uint docCookie, IVsWindowFrame pFrame)
        {
            return VSConstants.S_OK;
        }

        public int OnAfterFirstDocumentLock(uint docCookie, uint dwRDTLockType, uint dwReadLocksRemaining, uint dwEditLocksRemaining)
        {
            return VSConstants.S_OK;
        }

        public int OnAfterSave(uint docCookie)
        {
            return VSConstants.S_OK;
        }

        public int OnBeforeDocumentWindowShow(uint docCookie, int fFirstShow, IVsWindowFrame pFrame)
        {
            return VSConstants.S_OK;
        }

        public int OnBeforeLastDocumentUnlock(uint docCookie, uint dwRDTLockType, uint dwReadLocksRemaining, uint dwEditLocksRemaining)
        {
            return VSConstants.S_OK;
        }

        public int OnBeforeSave(uint docCookie)
        {
            BeforeSave?.Invoke(this, EventArgs.Empty);

            return VSConstants.S_OK;
        }

        #endregion

    }
}
