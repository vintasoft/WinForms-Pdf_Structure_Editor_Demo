﻿using Vintasoft.Imaging.Pdf;
using Vintasoft.Imaging.Pdf.Tree;
using Vintasoft.Imaging.Pdf.UI;
using Vintasoft.Imaging.UI;

namespace DemosCommonCode.Pdf
{
    /// <summary>
    /// Executor of "Named" action that performs execution of named viewer action in image viewer.
    /// </summary>
    public class PdfViewerNamedActionExecutor : PdfNamedActionExecutor
    {

        #region Fields

        /// <summary>
        /// Additional viewer handlers of named actions.
        /// </summary>
        PdfViewerNamedAction[] _viewerNamedActions;

        #endregion



        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PdfViewerNamedActionExecutor"/> class.
        /// </summary>
        /// <param name="viewer">The viewer.</param>
        /// <param name="viewerNamedActions">Additional viewer handlers of named actions.</param>
        public PdfViewerNamedActionExecutor(ImageViewer viewer, PdfViewerNamedAction[] viewerNamedActions)
            : base(viewer)
        {
            _viewerNamedActions = viewerNamedActions;
        }

        #endregion



        #region Methods

        /// <summary>
        /// Executes the action.
        /// </summary>
        /// <param name="action">The action.</param>
        /// <param name="args">The <see cref="PdfTriggerEventArgs" /> instance
        /// containing the event data.</param>
        /// <returns>
        /// <b>True</b> if action is executed; otherwise, <b>false</b>.
        /// </returns>
        public override bool ExecuteAction(PdfAction action, PdfTriggerEventArgs args)
        {
            if (!base.ExecuteAction(action, args))
            {
                PdfNamedAction namedAction = action as PdfNamedAction;
                if (namedAction == null)
                    return false;

                string actionName = namedAction.ActionName;
                for (int i = 0; i < _viewerNamedActions.Length; i++)
                {
                    if (_viewerNamedActions[i].ActionName == actionName)
                    {
                        _viewerNamedActions[i].Execute();
                        return true;
                    }
                }
            }

            return false;
        } 

        #endregion

    }
}
