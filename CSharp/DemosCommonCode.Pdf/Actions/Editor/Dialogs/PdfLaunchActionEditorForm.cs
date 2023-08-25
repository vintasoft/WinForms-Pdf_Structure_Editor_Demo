using System;
using System.Windows.Forms;

using Vintasoft.Imaging.Pdf.Tree;


namespace DemosCommonCode.Pdf
{
    /// <summary>
    /// A form that allows to view and edit PDF launch action.
    /// </summary>
    public partial class PdfLaunchActionEditorForm : Form
    {

        #region Fields

        /// <summary>
        /// The PDF launch action.
        /// </summary>
        PdfLaunchAction _action;

        #endregion



        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PdfLaunchActionEditorForm"/> class.
        /// </summary>
        /// <param name="action">The PDF launch action.</param>
        public PdfLaunchActionEditorForm(PdfLaunchAction action)
        {
            InitializeComponent();

            _action = action;

            commandLineTextBox.Text = _action.WinCommandLine;
            okButton.Enabled = !string.IsNullOrEmpty(commandLineTextBox.Text);
        }

        #endregion



        #region Methods

        /// <summary>
        /// Handles the Click event of OkButton object.
        /// </summary>
        private void okButton_Click(object sender, EventArgs e)
        {
            // update Windows command line
            _action.WinCommandLine = commandLineTextBox.Text;
        }

        /// <summary>
        /// Handles the TextChanged event of CommandLineTextBox object.
        /// </summary>
        private void commandLineTextBox_TextChanged(object sender, EventArgs e)
        {
            // if current form can be closed
            if (string.IsNullOrEmpty(commandLineTextBox.Text))
                okButton.Enabled = false;
            else
                okButton.Enabled = true;
        }

        #endregion

    }
}
