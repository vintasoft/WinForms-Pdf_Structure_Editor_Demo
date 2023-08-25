using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

using Vintasoft.Imaging.Pdf;
using Vintasoft.Imaging.Pdf.Tree;
using Vintasoft.Imaging.Pdf.Tree.InteractiveForms;
using Vintasoft.Imaging.Pdf.UI.Annotations;
using Vintasoft.Imaging.UI;
using Vintasoft.Imaging.UI.VisualTools;

namespace DemosCommonCode.Pdf
{
    /// <summary>
    /// Executor of "Form Submit" actions that transmits the names and values of 
    /// selected interactive form fields to a specified uniform 
    /// resource locator (URL), presumably the address of a Web 
    /// server that will process them and send back a response.
    /// </summary>
    public class PdfSubmitActionExecutor : PdfActionExecutorBase
    {

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PdfSubmitActionExecutor"/> class.
        /// </summary>
        /// <param name="viewer">The image viewer.</param>
        public PdfSubmitActionExecutor(ImageViewer viewer)
        {
            ImageViewer = viewer;
        }

        #endregion



        #region Properties

        ImageViewer _imageViewer;
        /// <summary>
        /// Gets or sets the image viewer.
        /// </summary>
        public ImageViewer ImageViewer
        {
            get
            {
                return _imageViewer;
            }
            set
            {
                _imageViewer = value;
            }
        }

        #endregion



        #region Methods

        #region PUBLIC

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
            PdfSubmitFormAction submitFormAction = action as PdfSubmitFormAction;
            if (submitFormAction != null)
            {
                string message = string.Format("Form submit: document must connect to '{0}'.\n Allow?", submitFormAction.Url);
                if (MessageBox.Show(message, "Submit action", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                    return false;

                // get fields to submit
                PdfInteractiveFormField[] fields = submitFormAction.GetActionFields();

                if (fields.Length == 0)
                {
                    string errorMessage = string.Format(
                        "The interactive form fields can not be submitted to '{0}', " +
                        "because the document does not contain the interactive form fields",
                        submitFormAction.Url);
                    DemosTools.ShowErrorMessage(errorMessage);
                    return false;
                }

                // check for required fields, which are not filled
                for (int i = 0; i < fields.Length; i++)
                {
                    PdfInteractiveFormField field = fields[i];
                    if (field.IsRequired && !field.HasFieldValue)
                    {
                        if (ImageViewer != null)
                        {
                            PdfAnnotationTool annotationTool = GetAnnotationTool(ImageViewer);
                            if (annotationTool != null)
                            {
                                annotationTool.FocusedField = field;
                                annotationTool.ScrollToFocusedItem();
                            }
                        }

                        DemosTools.ShowErrorMessage(string.Format("Field '{0}' is required but not filled.", field.FullyQualifiedName));
                        return false;
                    }
                }

                Stream stream = new MemoryStream();
                try
                {
                    string contentType = "";
                    switch (submitFormAction.SubmitFormat)
                    {
                        case PdfInteractiveFormFieldSubmitFormat.XFDF:
                            contentType = "application/vnd.adobe.xfdf; charset=utf-8";
                            // export form fields to XFDF
                            PdfInteractiveFormDataXfdfCodec xfdfCodec = new PdfInteractiveFormDataXfdfCodec();
                            xfdfCodec.PdfFilePath = "";
                            xfdfCodec.ExportFieldsWithoutValue = submitFormAction.IsIncludeNoValueFields;
                            xfdfCodec.Export(action.Document, fields, stream);
                            break;

                        case PdfInteractiveFormFieldSubmitFormat.PDF:
                            contentType = "application/pdf";
                            // save document to stream
                            lock (action.Document)
                                action.Document.Save(stream);
                            break;

                        case PdfInteractiveFormFieldSubmitFormat.FDF:
                            DemosTools.ShowErrorMessage("FDF submit format is not supported now.");
                            return false;

                        case PdfInteractiveFormFieldSubmitFormat.HTML:
                            DemosTools.ShowErrorMessage("HTML submit format is not supported now.");
                            return false;
                    }

                    using (WebUploaderForm dialog = new WebUploaderForm())
                    {
                        dialog.UploadAsync(submitFormAction.Url, contentType, stream);
                        dialog.ShowDialog();
                    }
                }
                catch (Exception ex)
                {
                    DemosTools.ShowErrorMessage(ex);
                }
                finally
                {
                    stream.Dispose();
                }
                return true;
            }
            return false;
        }

        #endregion


        #region PRIVATE

        /// <summary>
        /// Returns the PDF annotation tool, which is used by the specified image viewer.
        /// </summary>
        /// <param name="viewer">The image viewer.</param>
        /// <returns>
        /// The PDF annotation tool, which is used by the specified image viewer.
        /// </returns>
        private static PdfAnnotationTool GetAnnotationTool(ImageViewer viewer)
        {
            VisualTool currentVisualTool = viewer.VisualTool;
            PdfAnnotationTool annotationTool = null;
            if (currentVisualTool is PdfAnnotationTool)
            {
                annotationTool = (PdfAnnotationTool)currentVisualTool;
            }
            else if (currentVisualTool is IEnumerable<VisualTool>)
            {
                foreach (VisualTool visualTool in (IEnumerable<VisualTool>)currentVisualTool)
                {
                    if (visualTool is PdfAnnotationTool)
                    {
                        annotationTool = (PdfAnnotationTool)visualTool;
                        break;
                    }
                }
            }
            return annotationTool;
        }
        
        #endregion

        #endregion

    }
}
