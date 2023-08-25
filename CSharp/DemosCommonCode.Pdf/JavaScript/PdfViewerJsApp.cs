using System.Drawing.Printing;
using System.Windows.Forms;

using Vintasoft.Imaging.Pdf;
using Vintasoft.Imaging.Pdf.JavaScriptApi;
using Vintasoft.Imaging.Pdf.Print;
using Vintasoft.Imaging.Print;
using Vintasoft.Imaging.UI;
using Vintasoft.Imaging.Pdf.UI.JavaScript;

namespace DemosCommonCode.Pdf.JavaScript
{
    /// <summary>
    /// An JavaScript API "app" object that represents the WinForms PDF Viewer application. 
    /// </summary>
    public class PdfViewerJsApp : WinFormsPdfJsApp
    {

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PdfViewerJsApp"/> class.
        /// </summary>
        public PdfViewerJsApp()
            : base()
        {
        }

        #endregion


        
        #region Methods

        /// <summary>
        /// Prints all or a specific number of pages of specified document.
        /// </summary>
        /// <param name="doc">The Document to print.</param>
        /// <param name="bUI">Indicates that the User Interface must be shown to the user
        /// to obtain printing information and confirm the action.</param>
        /// <param name="nStart">A zero-based index that defines the start of an inclusive range of pages.<br />
        /// If <i>nStart</i> and <i>nEnd</i> are not specified, all pages in the document are printed.<br />
        /// If only <i>nStart</i> is specified, the range of pages is the single page
        /// specified by <i>nStart</i>.<br />
        /// If <i>nStart</i> and <i>nEnd</i> parameters are used, <i>bUI</i> must be false.</param>
        /// <param name="nEnd">A zero-based index that defines the end of an inclusive page range.<br />
        /// If <i>nStart</i> and <i>nEnd</i> are not specified, all pages in the document are printed.<br />
        /// If only <i>nEnd</i> is specified, the range of a pages is 0 to <i>nEnd</i>.<br />
        /// If <i>nStart</i> and <i>nEnd</i> parameters are used, <i>bUI</i> must be <b>false</b>.</param>
        /// <param name="bSilent">If <b>true</b>, suppresses the cancel dialog box while the document is printing.</param>
        /// <param name="bShrinkToFit">If <b>true</b>, the page is shrunk (if necessary) to fit within the imageable area
        /// of the printed page. If <b>false</b>, it is not.</param>
        /// <param name="bPrintAsImage">If <b>true</b>, print pages as an image.</param>
        /// <param name="bReverse">If <b>true</b>, print from <i>nEnd</i> to <i>nStart</i>.</param>
        /// <param name="bAnnotations">If <b>true</b>, annotations are printed.</param>
        public override void PrintDoc(
            PdfJsDoc doc,
            bool bUI,
            int nStart,
            int nEnd,
            bool bSilent,
            bool bShrinkToFit,
            bool bPrintAsImage,
            bool bReverse,
            bool bAnnotations)
        {
            // get image viewer where document is displayed
            ImageViewer viewer = FindImageViewer(doc.Source);

            // page count of document
            int pageCount = viewer.Images.Count;
            int fromPage;
            int toPage;
            PrintRange printRange;
            // if page range is specified
            if (nStart >= 0 || nEnd >= 0)
            {
                // if page range is invalid
                if (nStart >= pageCount || nEnd >= pageCount)
                    return;

                // if start page is specified
                if (nStart >= 0)
                {
                    fromPage = nStart + 1;
                    // if end page is specified
                    if (nEnd >= 0)
                    {
                        if (nEnd >= nStart)
                            toPage = nEnd + 1;
                        else
                            toPage = pageCount;
                    }
                    else
                    {
                        toPage = fromPage;
                    }
                }
                else
                {
                    fromPage = 1;
                    toPage = nEnd + 1;
                }

                printRange = PrintRange.SomePages;
            }
            else
            {
                fromPage = 1;
                toPage = pageCount;
                printRange = PrintRange.AllPages;
            }

            // create print dialog
            using (PrintDialog printDialog = new PrintDialog())
            {
                // set dialog properties
                printDialog.AllowSomePages = true;
                printDialog.AllowCurrentPage = true;
                printDialog.AllowSelection = false;

                // create PrintDocument
                using (ImagePrintDocument printDocument = CreatePrintDocument(bPrintAsImage))
                {
                    // set scale mode
                    if (bShrinkToFit)
                        printDocument.PrintScaleMode = PrintScaleMode.BestFit;
                    else
                        printDocument.PrintScaleMode = PrintScaleMode.CropToPageSize;

                    // set print controller
                    if (bSilent)
                    {
                        if (printDocument.PrintController is PrintControllerWithStatusDialog)
                            printDocument.PrintController = new StandardPrintController();
                    }
                    else
                    {
                        if (printDocument.PrintController is StandardPrintController)
                            printDocument.PrintController = new PrintControllerWithStatusDialog(
                                printDocument.PrintController);
                    }

                    // link PrintDocument and PrintDialog
                    printDialog.Document = printDocument;

                    // set print range properties
                    printDialog.PrinterSettings.MinimumPage = 1;
                    printDialog.PrinterSettings.MaximumPage = pageCount;
                    printDialog.PrinterSettings.FromPage = fromPage;
                    printDialog.PrinterSettings.ToPage = toPage;
                    printDialog.PrinterSettings.PrintRange = printRange;

                    // create print manager
                    PdfViewerPrintManager printManager = new PdfViewerPrintManager(
                        viewer, printDocument, printDialog);

                    // set rendering mode of PDF annotations
                    if (bAnnotations)
                    {
                        // render all annotations that have the 'Print' flag set
                        printManager.AnnotationRenderingMode =
                            PdfAnnotationRenderingMode.RenderPrintable |
                            PdfAnnotationRenderingMode.Annotations;
                    }
                    else
                    {
                        // render only non-markup annotations that have the 'Print' flag set
                        printManager.AnnotationRenderingMode =
                            PdfAnnotationRenderingMode.RenderPrintable |
                            PdfAnnotationRenderingMode.NonMarkupAnnotations;
                    }

                    // start printing
                    printManager.Print(bUI, bReverse);
                }
            }
        }

        /// <summary>
        /// Creates an instance of the <see cref="ImagePrintDocument"/> class.
        /// </summary>
        /// <param name="bPrintAsImage">Indicates that PDF pages must be printed as images.</param>
        /// <returns></returns>
        private ImagePrintDocument CreatePrintDocument(bool bPrintAsImage)
        {
            if (bPrintAsImage)
                return new ImagePrintDocument();

            return new PdfPrintDocument();
        }

        #endregion

    }
}
