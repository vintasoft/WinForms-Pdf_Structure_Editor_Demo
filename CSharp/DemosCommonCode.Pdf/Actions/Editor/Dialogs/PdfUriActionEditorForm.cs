using System;
using System.Windows.Forms;

using Vintasoft.Imaging.Pdf.Tree;


namespace DemosCommonCode.Pdf
{
    /// <summary>
    /// A form that allows to view and edit PDF URI action.
    /// </summary>
    public partial class PdfUriActionEditorForm : Form
    {

        #region Fields

        /// <summary>
        /// The PDF URI action.
        /// </summary>
        PdfUriAction _action = null;

        #endregion



        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PdfUriActionEditorForm"/> class.
        /// </summary>
        /// <param name="action">The PDF URI action.</param>
        public PdfUriActionEditorForm(PdfUriAction action)
        {
            InitializeComponent();

            _action = action;
            uriTextBox.Text = _action.URI;
            okButton.Enabled = !string.IsNullOrEmpty(uriTextBox.Text);
        }

        #endregion



        #region Methods

        /// <summary>
        /// Handles the Click event of okButton object.
        /// </summary>
        private void okButton_Click(object sender, EventArgs e)
        {
            // update action URI
            _action.URI = uriTextBox.Text;
        }

        /// <summary>
        /// Handles the TextChanged event of uriTextBox object.
        /// </summary>
        private void uriTextBox_TextChanged(object sender, EventArgs e)
        {
            // if current form can not be closed
            if (string.IsNullOrEmpty(uriTextBox.Text))
                okButton.Enabled = false;
            else
                okButton.Enabled = true;
        }

        #endregion

    }
}
