using System;
using System.Windows.Forms;

using Vintasoft.Imaging.Pdf;
using Vintasoft.Imaging.Pdf.Tree;


namespace DemosCommonCode.Pdf
{
    /// <summary>
    /// A form that allows to view and edit triggers of PDF document or PDF page form field.
    /// </summary>
    public partial class PdfTriggersEditorForm : Form
    {

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PdfTriggersEditorForm"/> class.
        /// </summary>
        private PdfTriggersEditorForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PdfTriggersEditorForm"/> class.
        /// </summary>
        /// <param name="treeNode">The PDF tree node.</param>
        public PdfTriggersEditorForm(PdfTreeNodeBase treeNode)
            : this()
        {
            if (treeNode == null)
                throw new ArgumentNullException();

            pdfTriggersEditorControl.TreeNode = treeNode;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PdfTriggersEditorForm"/> class.
        /// </summary>
        /// <param name="document">The PDF document.</param>
        public PdfTriggersEditorForm(PdfDocument document)
            : this(document.Catalog)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PdfTriggersEditorForm"/> class.
        /// </summary>
        /// <param name="page">The PDF page.</param>
        public PdfTriggersEditorForm(PdfPage page)
            : this((PdfTreeNodeBase)page)
        {
            if (page.AdditionalActions == null)
                page.AdditionalActions = new PdfPageAdditionalActions(page.Document);
        }

        #endregion

    }
}
