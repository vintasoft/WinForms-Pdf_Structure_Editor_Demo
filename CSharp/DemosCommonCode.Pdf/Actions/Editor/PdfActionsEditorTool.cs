using System.Windows.Forms;

using Vintasoft.Imaging;
using Vintasoft.Imaging.Pdf.Tree;
using Vintasoft.Imaging.Pdf.Tree.InteractiveForms;

namespace DemosCommonCode.Pdf
{
    /// <summary>
    /// Contains common static functions for editing of PDF actions.
    /// </summary>
    public static class PdfActionsEditorTool
    {

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PdfActionsEditorTool"/> class.
        /// </summary>
        static PdfActionsEditorTool()
        {
        }

        #endregion



        #region Methods

        /// <summary>
        /// Edits the PDF action.
        /// </summary>
        /// <param name="action">The PDF action.</param>
        /// <param name="imageCollection">Image collection, which is associated with
        /// PDF document.</param>
        /// <param name="parentForm">A form that must be used as a parent form for dialog
        /// that edits PDF action.</param>
        public static bool EditAction(PdfAction action, ImageCollection imageCollection, Form parentForm)
        {
            Form dialog = null;

            switch (action.ActionType)
            {
                case PdfActionType.GoTo:
                    dialog = new PdfGotoActionEditorForm((PdfGotoAction)action, imageCollection);
                    break;

                case PdfActionType.Hide:
                    dialog = new PdfAnnotationHideActionEditorForm((PdfAnnotationHideAction)action);
                    break;

                case PdfActionType.JavaScript:
                    dialog = new PdfJavaScriptActionEditorForm((PdfJavaScriptAction)action);
                    break;

                case PdfActionType.Launch:
                    dialog = new PdfLaunchActionEditorForm((PdfLaunchAction)action);
                    break;

                case PdfActionType.Named:
                    dialog = new PdfNamedActionEditorForm((PdfNamedAction)action);
                    break;

                case PdfActionType.ResetForm:
                    dialog = new PdfResetFormActionEditorForm((PdfResetFormAction)action);
                    break;

                case PdfActionType.SubmitForm:
                    dialog = new PdfSubmitFormActionEditorForm((PdfSubmitFormAction)action);
                    break;

                case PdfActionType.URI:
                    dialog = new PdfUriActionEditorForm((PdfUriAction)action);
                    break;
            }

            bool result = false;

            if (dialog != null)
            {
                // set the dialog owner
                if (parentForm != null)
                    dialog.Owner = parentForm;
                // show dialog
                if (dialog.ShowDialog() == DialogResult.OK)
                    result = true;
                dialog.Dispose();
            }

            return result;
        } 

        #endregion

    }
}
