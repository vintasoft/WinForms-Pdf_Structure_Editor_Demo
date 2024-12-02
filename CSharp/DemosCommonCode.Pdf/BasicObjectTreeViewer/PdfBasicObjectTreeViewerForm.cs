using System;
using System.ComponentModel;
using System.Windows.Forms;

using Vintasoft.Imaging.Pdf.BasicTypes;

namespace DemosCommonCode.Pdf
{
    /// <summary>
    /// A form that displays PDF basic object tree.
    /// </summary>
    public partial class PdfBasicObjectTreeViewerForm : Form
    {

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PdfBasicObjectTreeViewerForm"/> class.
        /// </summary>
        public PdfBasicObjectTreeViewerForm()
        {
            InitializeComponent();
        }

        #endregion



        #region Properties

        /// <summary>
        /// Gets or sets the root object of tree viewer.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public PdfBasicObject RootObject
        {
            get
            {
                return basicObjectTreeView.RootObject;
            }
            set
            {
                basicObjectTreeView.RootObject = value;
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
        /// Handles the AfterSelect event of basicObjectTreeView object.
        /// </summary>
        private void basicObjectTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Tag != null)
                propertiesGroupBox.Text = e.Node.Tag.GetType().Name;
            else
                propertiesGroupBox.Text = "";
            basicObjectPropertyGrid.SelectedObject = e.Node.Tag;
        }

        /// <summary>
        /// Handles the PropertyValueChanged event of basicObjectPropertyGrid object.
        /// </summary>
        private void basicObjectPropertyGrid_PropertyValueChanged(
            object s,
            PropertyValueChangedEventArgs e)
        {
            // invalidate selected object
            basicObjectTreeView.InvalidateSelectedObject();
        }

        #endregion

    }
}
