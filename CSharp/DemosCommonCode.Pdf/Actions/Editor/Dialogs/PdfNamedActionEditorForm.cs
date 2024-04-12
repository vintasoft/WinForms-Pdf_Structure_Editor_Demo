using System;
using System.Windows.Forms;

using Vintasoft.Imaging.Pdf.Tree;


namespace DemosCommonCode.Pdf
{
    /// <summary>
    /// A form that allows to view and edit PDF named action.
    /// </summary>
    public partial class PdfNamedActionEditorForm : Form
    {

        #region Fields

        /// <summary>
        /// The PDF named action.
        /// </summary>
        PdfNamedAction _action;

        #endregion



        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PdfNamedActionEditorForm"/> class.
        /// </summary>
        /// <param name="action">The PDF named action.</param>
        public PdfNamedActionEditorForm(PdfNamedAction action)
        {
            InitializeComponent();

            _action = action;

            nameComboBox.Text = _action.ActionName;
            okButton.Enabled = !string.IsNullOrEmpty(nameComboBox.Text);
        }

        #endregion



        #region Methods

        /// <summary>
        /// Handles the Click event of okButton object.
        /// </summary>
        private void okButton_Click(object sender, EventArgs e)
        {
            // update action name
            _action.ActionName = nameComboBox.Text;
        }

        /// <summary>
        /// Handles the TextChanged event of nameComboBox object.
        /// </summary>
        private void nameComboBox_TextChanged(object sender, EventArgs e)
        {
            // if current form can not be closed
            if (string.IsNullOrEmpty(nameComboBox.Text))
                okButton.Enabled = false;
            else
                okButton.Enabled = true;
        }

        #endregion

    }
}
