using System;
using System.Diagnostics;
using System.Windows.Forms;

using Vintasoft.Imaging.Pdf;
using Vintasoft.Imaging.Pdf.Tree;

namespace DemosCommonCode.Pdf
{
    /// <summary>
    /// Executor of PDF launch action that launch an application.
    /// </summary>
    public class PdfLaunchActionExecutor : PdfActionExecutorBase
    {

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PdfLaunchActionExecutor"/> class.
        /// </summary>
        public PdfLaunchActionExecutor()
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
        /// <returns>
        /// <b>True</b> if action is executed; otherwise, <b>false</b>.
        /// </returns>
        public override bool ExecuteAction(PdfAction action, PdfTriggerEventArgs args)
        {
            PdfLaunchAction launchAction = action as PdfLaunchAction;
            if (launchAction != null)
            {
                if (launchAction.WinCommandLine != "")
                {
                    if (MessageBox.Show(string.Format("Start application '{0}' ?", launchAction.WinCommandLine), "Execute an application", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        try
                        {
                            Process.Start(launchAction.WinCommandLine);
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
