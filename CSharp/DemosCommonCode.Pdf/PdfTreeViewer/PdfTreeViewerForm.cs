using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace DemosCommonCode.Pdf
{
    /// <summary>
    /// A form that displays PDF tree.
    /// </summary>
    public partial class PdfTreeViewerForm : Form
    {

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PdfTreeViewerForm"/> class.
        /// </summary>
        public PdfTreeViewerForm()
        {
            InitializeComponent();
        }

        #endregion



        #region Properties

        /// <summary>
        /// Gets or sets the root object of PDF tree viewer.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public object RootObject
        {
            get
            {
                return pdfTreeView.RootObject;
            }
            set
            {
                pdfTreeView.RootObject = value;
            }
        }

        #endregion



        #region Methods

        /// <summary>
        /// Handles the Click event of closeButton object.
        /// </summary>
        private void closeButton_Click(object sender, EventArgs e)
        {
            // close this form
            Close();
        }

        /// <summary>
        /// Handles the AfterSelect event of pdfTreeView object.
        /// </summary>
        private void pdfTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            // create tree node object
           PdfTreeView.TreeNodeObject nodeObject = new PdfTreeView.TreeNodeObject();
            if (e.Node.Tag is PdfTreeView.TreeNodeObject)
                // get tree node object selected
                nodeObject = (PdfTreeView.TreeNodeObject)e.Node.Tag;

            // if tree node object is selected
            if (nodeObject.PropertyObject != null)
            {
                // update property grid group box text
                propertyGridGroupBox.Text = nodeObject.PropertyObject.GetType().Name;
                // update selected object
                pdfTreeNodePropertyGrid.SelectedObject = nodeObject.PropertyObject;
            }
            else
            {
                // if tree node object exist
                if (e.Node.Tag != null)
                    // update property grid group box text
                    propertyGridGroupBox.Text = e.Node.Tag.GetType().Name;
                else
                    // clear property grid group box text
                    propertyGridGroupBox.Text = "";

                // update selected object
                pdfTreeNodePropertyGrid.SelectedObject = e.Node.Tag;
            }
        }

        #endregion

    }
}
