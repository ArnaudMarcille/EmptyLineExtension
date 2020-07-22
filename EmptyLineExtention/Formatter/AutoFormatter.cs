using System;
using System.Collections.Generic;
using System.Linq;
using EmptyLineExtention.Core.Settings;
using EmptyLineExtention.Services;
using EnvDTE;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Newtonsoft.Json;

namespace EmptyLineExtention.Formatter
{
    /// <summary>
    /// Document Formatter
    /// </summary>
    internal class AutoFormatter : IVsRunningDocTableEvents3
    {
        #region Field

        /// <summary>
        /// DTE instance
        /// </summary>
        private readonly DTE dte;

        /// <summary>
        /// document table instance
        /// </summary>
        private readonly RunningDocumentTable runningDocumentTable;

        /// <summary>
        /// package
        /// </summary>
        private readonly Package package;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dte">DTE instance</param>
        /// <param name="runningDocumentTable">RunningDocumentTable instance</param>
        public AutoFormatter(DTE dte, RunningDocumentTable runningDocumentTable, Package package)
        {
            this.runningDocumentTable = runningDocumentTable;
            this.dte = dte;
            this.package = package;
        }

        #endregion

        #region Events

        /// <summary>
        /// Action before document save
        /// </summary>
        /// <param name="docCookie"></param>
        /// <returns></returns>
        public int OnBeforeSave(uint docCookie)
        {
            var document = FindDocument(docCookie);

            if (document == null)
                return VSConstants.S_OK;

            if (GetAutoSavePropertyValue())
            {
                int? allowedLines = GetAllowedLinesValue(document);
                if (allowedLines.HasValue)
                {
                    EmptyLineService.FormatDocument(document, false, allowedLines.Value, dte);
                }
            }

            return VSConstants.S_OK;
        }

        public int OnAfterFirstDocumentLock(uint docCookie, uint dwRdtLockType, uint dwReadLocksRemaining, uint dwEditLocksRemaining)
        {
            return VSConstants.S_OK;
        }

        public int OnBeforeLastDocumentUnlock(uint docCookie, uint dwRdtLockType, uint dwReadLocksRemaining, uint dwEditLocksRemaining)
        {
            return VSConstants.S_OK;
        }

        public int OnAfterSave(uint docCookie)
        {
            return VSConstants.S_OK;
        }

        public int OnAfterAttributeChange(uint docCookie, uint grfAttribs)
        {
            return VSConstants.S_OK;
        }

        public int OnBeforeDocumentWindowShow(uint docCookie, int fFirstShow, IVsWindowFrame pFrame)
        {
            return VSConstants.S_OK;
        }

        public int OnAfterDocumentWindowHide(uint docCookie, IVsWindowFrame pFrame)
        {
            return VSConstants.S_OK;
        }

        int IVsRunningDocTableEvents3.OnAfterAttributeChangeEx(uint docCookie, uint grfAttribs, IVsHierarchy pHierOld, uint itemidOld,
            string pszMkDocumentOld, IVsHierarchy pHierNew, uint itemidNew, string pszMkDocumentNew)
        {
            return VSConstants.S_OK;
        }

        int IVsRunningDocTableEvents2.OnAfterAttributeChangeEx(uint docCookie, uint grfAttribs, IVsHierarchy pHierOld, uint itemidOld,
            string pszMkDocumentOld, IVsHierarchy pHierNew, uint itemidNew, string pszMkDocumentNew)
        {
            return VSConstants.S_OK;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Find the correct document for a docCookie
        /// </summary>
        /// <param name="docCookie"></param>
        /// <returns></returns>
        private Document FindDocument(uint docCookie)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            var documentInfo = runningDocumentTable.GetDocumentInfo(docCookie);
            var documentPath = documentInfo.Moniker;

            return dte.Documents.Cast<Document>().FirstOrDefault(doc =>
            {
                Microsoft.VisualStudio.Shell.ThreadHelper.ThrowIfNotOnUIThread();
                return doc.FullName == documentPath;
            });
        }

        /// <summary>
        /// Get the value of IsAutoSaveEnabled property
        /// </summary>
        /// <returns></returns>
        private bool GetAutoSavePropertyValue()
        {
            OptionPage optionProperties = null;
            try
            {
                optionProperties = (OptionPage)package.GetDialogPage(typeof(OptionPage));
            }
            catch (Exception)
            {
                return false;
            }

            if (optionProperties == null)
                return false;

            return optionProperties.IsAutoSaveEnabled;
        }

        /// <summary>
        /// Get the value of AllowedLines property
        /// </summary>
        /// <returns></returns>
        private int? GetAllowedLinesValue(Document document)
        {
            OptionPage optionProperties = null;
            try
            {
                optionProperties = (OptionPage)package.GetDialogPage(typeof(OptionPage));
            }
            catch (Exception)
            {
                return null;
            }

            int? allowedLines = null;

            if (!string.IsNullOrEmpty(optionProperties?.FilesConfigurations))
            {
                List<SettingItem> items = JsonConvert.DeserializeObject<List<SettingItem>>(optionProperties.FilesConfigurations);
                var result = RegexService.FindAllowedLinesForDocument(document.FullName, items);

                if (result != null)
                {
                    allowedLines = result.Value;
                }
            }

            return allowedLines;
        }

        #endregion
    }
}
