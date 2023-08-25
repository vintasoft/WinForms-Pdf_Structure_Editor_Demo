﻿using System;
using System.Diagnostics;
using System.Windows.Forms;

using Vintasoft.Imaging.Pdf;
using Vintasoft.Imaging.Pdf.Tree;

namespace DemosCommonCode.Pdf
{
    /// <summary>
    /// Executor of "URI" actions that opens URL using the default internet browser.
    /// </summary>
    public class PdfUriActionExecutor : PdfActionExecutorBase
    {

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PdfUriActionExecutor"/> class.
        /// </summary>
        public PdfUriActionExecutor()
        {
        }

        #endregion



        #region Methods

        /// <summary>
        /// Executes the action.
        /// </summary>
        /// <param name="action">The action.</param>
        /// <param name="args">The <see cref="PdfTriggerEventArgs"/> instance
        /// containing the event data.</param>
        /// <returns><b>True</b> if action is executed; otherwise, <b>false</b>.</returns>
        public override bool ExecuteAction(PdfAction action, PdfTriggerEventArgs args)
        {
            PdfUriAction uriAction = action as PdfUriAction;
            if (uriAction != null)
            {
                if (uriAction.URI != "")
                {
                    if (MessageBox.Show(string.Format("Open URL '{0}' ?", uriAction.URI), "Open URL", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        try
                        {
                            DemosTools.OpenBrowser(uriAction.URI);
                        }
                        catch (Exception exc)
                        {
                            DemosTools.ShowErrorMessage(exc);
                            return false;
                        }
                    }
                    return true;
                }
            }
            return false;
        } 

        #endregion

    }
}
