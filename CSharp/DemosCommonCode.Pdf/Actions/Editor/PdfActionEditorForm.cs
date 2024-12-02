using System.ComponentModel;
using System.Windows.Forms;

using Vintasoft.Imaging;
using Vintasoft.Imaging.Pdf;
using Vintasoft.Imaging.Pdf.Tree;


namespace DemosCommonCode.Pdf
{
    /// <summary>
    /// A form that allows to view and edit action of PDF document.
    /// </summary>
    public partial class PdfActionEditorForm : Form
    {

        #region Constructors

        #region PUBLIC

        /// <summary>
        /// Initializes a new instance of the <see cref="PdfActionEditorForm"/> class.
        /// </summary>
        /// <param name="document">The PDF document.</param>
        public PdfActionEditorForm(PdfDocument document)
            : this()
        {
            pdfActionEditorControl1.Document = document;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PdfActionEditorForm"/> class.
        /// </summary>
        /// <param name="document">The PDF document.</param>
        /// <param name="action">The PDF action.</param>
        public PdfActionEditorForm(PdfDocument document, PdfAction action)
            : this(document)
        {
            pdfActionEditorControl1.Action = action;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PdfActionEditorForm"/> class.
        /// </summary>
        /// <param name="document">The PDF document.</param>
        /// <param name="action">The PDF action.</param>
        /// <param name="imageCollection">The image collection,
        /// which is associated with PDF document.</param>
        public PdfActionEditorForm(
            PdfDocument document,
            PdfAction action,
            ImageCollection imageCollection)
            : this(document, action)
        {
            pdfActionEditorControl1.ImageCollection = imageCollection;
        }

        #endregion


        #region PRIVATE

        /// <summary>
        /// Prevents a default instance of the <see cref="PdfActionEditorForm"/> class from being created.
        /// </summary>
        private PdfActionEditorForm()
        {
            InitializeComponent();
        }

        #endregion

        #endregion



        #region Properties

        /// <summary>
        /// Gets or sets the PDF action.
        /// </summary>
        /// <value>
        /// Default value is <b>null</b>.
        /// </value>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public PdfAction Action
        {
            get
            {
                return pdfActionEditorControl1.Action;
            }
            set
            {
                pdfActionEditorControl1.Action = value;
            }
        }

        /// <summary>
        /// Gets or sets the image collection, which is associated with PDF document.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ImageCollection ImageCollection
        {
            get
            {
                return pdfActionEditorControl1.ImageCollection;
            }
            set
            {
                pdfActionEditorControl1.ImageCollection = value;
            }
        }

        #endregion

    }
}
