using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

using Vintasoft.Imaging;
using Vintasoft.Imaging.Pdf;
using Vintasoft.Imaging.Pdf.Security;
using Vintasoft.Imaging.Pdf.Tree;
using Vintasoft.Imaging.Pdf.Tree.Annotations;
using Vintasoft.Imaging.Pdf.Tree.InteractiveForms;
using Vintasoft.Imaging.UI;
using Vintasoft.Imaging.UI.VisualTools;
using Vintasoft.Imaging.UIActions;
using Vintasoft.Imaging.Utils;

using DemosCommonCode.Imaging;
using DemosCommonCode.Pdf.Security;


namespace DemosCommonCode.Pdf
{
    /// <summary>
    /// Contains common static functions for PDF demos.
    /// </summary>
    public static class PdfDemosTools
    {

        #region Properties

        static bool _needGenerateInteractiveFormsAppearance = false;
        /// <summary>
        /// Gets or sets a value indicating whether
        /// the interactive form fields appearances must be generated.
        /// </summary>
        public static bool NeedGenerateInteractiveFormFieldsAppearance
        {
            get
            {
                return _needGenerateInteractiveFormsAppearance;
            }
            set
            {
                if (_needGenerateInteractiveFormsAppearance != value)
                {
                    _needGenerateInteractiveFormsAppearance = value;
                    if (value)
                        PdfDocumentController.DocumentOpened += new EventHandler<PdfDocumentEventArgs>(GenerateInteractiveFormFieldsAppearance);
                    else
                        PdfDocumentController.DocumentOpened -= GenerateInteractiveFormFieldsAppearance;
                }
            }
        }

        #endregion



        #region Methods

        #region PUBLIC

        /// <summary>
        /// Adds the long-term validation information (LTV) to the PDF document.
        /// </summary>
        /// <param name="document">The PDF document.</param>
        public static void AddLongTimeValidationInfo(PdfDocument document)
        {
            if (document.IsChanged)
            {
                DemosTools.ShowInfoMessage("Document is changed. First please sign and save document.");
                return;
            }
            try
            {
                int count = Vintasoft.Imaging.Pdf.Tree.DigitalSignatures.PdfDocumentLtv.AddLtvInfo(document);
                if (count == 0)
                    DemosTools.ShowInfoMessage("LTV information is not required for this document.");
                else
                    DemosTools.ShowInfoMessage("LTV information is added.");
            }
            catch (Exception ex)
            {
                DemosTools.ShowErrorMessage(ex);
                return;
            }
        }

        /// <summary>
        /// Returns a value indicating whether all specified images are contained in specified PDF document.
        /// </summary>
        /// <param name="images">An image collection.</param>
        /// <param name="document">PDF document.</param>
        /// <returns>
        /// A value indicating whether all specified images are contained in specified PDF document.</returns>
        public static bool CheckAllPagesFromDocument(ImageCollection images, PdfDocument document)
        {
            bool result = true;
            foreach (VintasoftImage image in images)
            {
                PdfPage page = PdfDocumentController.GetPageAssociatedWithImage(image);
                if (page == null)
                {
                    result = false;
                    break;
                }
                if (page.Document != document)
                {
                    result = false;
                    break;
                }
            }
            if (!result)
            {
                DemosTools.ShowWarningMessage("One or several pages are not saved in PDF document. Save document and try again.");
            }
            return result;
        }        

        /// <summary>
        /// Determines whether image is valid.
        /// </summary>
        /// <param name="image">The image.</param>
        public static bool IsValidImage(VintasoftImage image)
        {
            if (image == null)
                return false;

            if (image.IsDisposed)
                return false;

            if (image.IsBad)
                return false;

            PdfPage page = PdfDocumentController.GetPageAssociatedWithImage(image);
            if (page != null && page.Document.AuthorizationResult == AuthorizationResult.IncorrectPassword)
                return false;

            return true;
        }

        /// <summary>
        /// Finds the image by page.
        /// </summary>
        /// <param name="page">Source PDF page.</param>
        /// <param name="images">Image collection, where image must be searched.</param>
        /// <returns>Image if image is found; otherwise, <b>null</b>.</returns>
        public static VintasoftImage FindImageByPage(PdfPage page, ImageCollection images)
        {
            if (page != null)
            {
                for (int i = 0; i < images.Count; i++)
                {
                    if (PdfDocumentController.GetPageAssociatedWithImage(images[i]) == page)
                        return images[i];
                }
            }
            return null;
        }

        /// <summary>
        /// Returns the UI action of the visual tool.
        /// </summary>
        /// <param name="visualTool">Visual tool.</param>
        /// <returns>The UI action of the visual tool.</returns>
        public static T GetUIAction<T>(VisualTool visualTool)
            where T : UIAction
        {
            IList<UIAction> actions = null;
            if (TryGetCurrentToolActions(visualTool, out actions))
            {
                foreach (UIAction action in actions)
                {
                    if (action is T)
                        return (T)action;
                }
            }
            return default(T);
        }

        /// <summary>
        /// Shows the document information in a Property Grid.
        /// </summary>
        /// <param name="document">The PDF document.</param>
        /// <param name="suggestToCreateDocumentInformationDictionary">
        /// Indicates that applicatiom must suggest to create the document information dictionary
        /// if PDF document does not have the document information dictionary.
        /// </param>
        /// <param name="propertyValueChangedEventHandler">
        /// The PropertyValueChanged event handler of the PropertyGrid.
        /// </param>
        public static void ShowDocumentInformation(
            PdfDocument document,
            bool suggestToCreateDocumentInformationDictionary,
            PropertyValueChangedEventHandler propertyValueChangedEventHandler)
        {
            if (!document.HasDocumentInformation)
            {
                if (suggestToCreateDocumentInformationDictionary)
                {
                    if (MessageBox.Show("PDF document does not have the Information Dictionary. Do you want to create the Information Dictionary?", "", MessageBoxButtons.YesNo) == DialogResult.No)
                        return;
                }
                else
                {
                    DemosTools.ShowInfoMessage("PDF document does not have the Information Dictionary.");
                    return;
                }
            }

            try
            {
                using (PropertyGridForm dialog = new PropertyGridForm(document.DocumentInformation, "Document Information"))
                {
                    if (propertyValueChangedEventHandler != null)
                        dialog.PropertyGrid.PropertyValueChanged += propertyValueChangedEventHandler;
                    dialog.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                DemosTools.ShowErrorMessage(ex);
            }
        }

        /// <summary>
        /// Converts PDF document page layout mode to the image viewer display mode.
        /// </summary>
        /// <param name="pageLayoutMode">PDF document page layout mode.</param>
        /// <returns>Image viewer display mode.</returns>
        public static ImageViewerDisplayMode ConvertPageLayoutModeToImageViewerDisplayMode(PdfDocumentPageLayoutMode pageLayoutMode)
        {
            switch (pageLayoutMode)
            {
                case (PdfDocumentPageLayoutMode.OneColumn):
                    return ImageViewerDisplayMode.SingleContinuousColumn;
                case (PdfDocumentPageLayoutMode.SinglePage):
                    return ImageViewerDisplayMode.SinglePage;
                case (PdfDocumentPageLayoutMode.TwoColumnLeft):
                    return ImageViewerDisplayMode.TwoContinuousColumns;
                case (PdfDocumentPageLayoutMode.TwoColumnRight):
                    return ImageViewerDisplayMode.TwoContinuousColumns;
                case (PdfDocumentPageLayoutMode.TwoPageLeft):
                    return ImageViewerDisplayMode.TwoColumns;
                case (PdfDocumentPageLayoutMode.TwoPageRight):
                    return ImageViewerDisplayMode.TwoColumns;
                default:
                    return ImageViewerDisplayMode.SinglePage;
            }
        }


        /// <summary>
        /// Returns the name of the annotation.
        /// </summary>
        /// <param name="annotation">The annotation.</param>
        public static string GetAnnotationName(PdfAnnotation annotation)
        {
            string name = annotation.Name;

            // if annotation is widget annotation
            if (annotation is PdfWidgetAnnotation)
            {
                PdfWidgetAnnotation widgetAnnotation = (PdfWidgetAnnotation)annotation;
                // get interactive form field
                PdfInteractiveFormField field = widgetAnnotation.Field;

                // get name of annotation
                name = field.PartialName;
                // if name is empty
                if (string.IsNullOrEmpty(name))
                {
                    // if field is switchable button
                    if (field is PdfInteractiveFormSwitchableButtonField)
                        // get value of button
                        name = ((PdfInteractiveFormSwitchableButtonField)field).ButtonValue;
                }
            }

            return name;
        }

        /// <summary>
        /// Returns a description of PDF annotation.
        /// </summary>
        /// <param name="annotation">The PDF annotation.</param>
        /// <returns>A description of PDF annotation.</returns>
        public static string GetAnnotationDescription(PdfAnnotation annotation)
        {
            PdfWidgetAnnotation widgetAnnotation = annotation as PdfWidgetAnnotation;
            if (widgetAnnotation != null)
            {
                return string.Format("Form field: {0} ({1})",
                    widgetAnnotation.Field.FullyQualifiedName,
                    widgetAnnotation.Field.GetType().Name);
            }
            return string.Format("{0} annotation", annotation.AnnotationType);
        }

        #endregion


        #region PRIVATE

        /// <summary>
        /// Generates the interactive form fields appearance.
        /// </summary>
        private static void GenerateInteractiveFormFieldsAppearance(
            object sender,
            PdfDocumentEventArgs e)
        {
            e.Document.AutoUpdateInteractiveFormAppearances = _needGenerateInteractiveFormsAppearance;
        }

        /// <summary>
        /// Returns the UI actions of visual tool.
        /// </summary>
        /// <param name="visualTool">The visual tool.</param>
        /// <param name="actions">The list of actions supported by the current visual tool.</param>
        /// <returns>
        /// <b>true</b> - UI actions are found; otherwise, <b>false</b>.
        /// </returns>
        private static bool TryGetCurrentToolActions(
            VisualTool visualTool,
            out IList<UIAction> actions)
        {
            actions = null;
            ISupportUIActions currentToolWithUIActions = visualTool as ISupportUIActions;
            if (currentToolWithUIActions != null)
                actions = currentToolWithUIActions.GetSupportedUIActions();

            return actions != null;
        }

        #endregion

        #endregion

    }
}
