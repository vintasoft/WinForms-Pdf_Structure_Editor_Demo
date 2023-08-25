using System.Windows.Forms;

using Vintasoft.Imaging;
using Vintasoft.Imaging.Codecs.Decoders;
using Vintasoft.Imaging.Pdf;
using Vintasoft.Imaging.Print;
using Vintasoft.Imaging.UI;

using DemosCommonCode.Imaging;

namespace DemosCommonCode.Pdf
{
    /// <summary>
    /// Print manager of a PDF image viewer.
    /// </summary>
    public class PdfViewerPrintManager : ImageViewerPrintManager
    {

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PdfViewerPrintManager"/> class.
        /// </summary>
        /// <param name="imageViewer">The image viewer.</param>
        /// <param name="printDocument">The ImagePrintDocument.</param>
        /// <param name="printDialog">The PrintDialog.</param>
        public PdfViewerPrintManager(
            ImageViewerBase imageViewer,
            ImagePrintDocument printDocument,
            PrintDialog printDialog)
            : base(imageViewer, printDocument, printDialog)
        {
        }

        #endregion



        #region Properties

        PdfAnnotationRenderingMode _annotationRenderingMode =
            PdfAnnotationRenderingMode.RenderPrintable | PdfAnnotationRenderingMode.Annotations;
        /// <summary>
        /// Gets or sets the annotation rendering mode.
        /// </summary>
        public PdfAnnotationRenderingMode AnnotationRenderingMode
        {
            get
            {
                return _annotationRenderingMode;
            }
            set
            {
                _annotationRenderingMode = value;
            }
        }

        #endregion



        #region Methods

        /// <summary>
        /// Returns the image that should be printed.
        /// </summary>
        /// <param name="imageIndex">Index of the image in image collection of the viewer.</param>
        /// <param name="disposeAfterUse">Determines whether the manager must dispose image
        /// after use.</param>
        /// <returns>Image that should be printed.</returns>
        protected override VintasoftImage GetImage(int imageIndex, out bool disposeAfterUse)
        {
            // get printing image
            VintasoftImage image = base.GetImage(imageIndex, out disposeAfterUse);
            RenderingSettings oldRenderingSettings = image.RenderingSettings;

            // create PDF rendering settings with specified annotation rendering mode

            PdfRenderingSettings renderingSettings = new PdfRenderingSettings();
            renderingSettings.AnnotationRenderingMode = AnnotationRenderingMode;
            renderingSettings.InterpolationMode = oldRenderingSettings.InterpolationMode;
            renderingSettings.Resolution = oldRenderingSettings.Resolution;
            renderingSettings.SmoothingMode = oldRenderingSettings.SmoothingMode;

            // set new rendering settings
            image.RenderingSettings = renderingSettings;

            return image;
        }

        #endregion

    }
}
