using System;
using System.Windows.Forms;

using Vintasoft.Imaging.Pdf.Tree;


namespace DemosCommonCode.Pdf
{
    /// <summary>
    /// A form that allows to view and edit PDF JavaScript action.
    /// </summary>
    public partial class PdfJavaScriptActionEditorForm : Form
    {

        #region Fields

        /// <summary>
        /// The JavaScript action.
        /// </summary>
        PdfJavaScriptAction _action;

        #endregion



        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PdfJavaScriptActionEditorForm"/> class.
        /// </summary>
        /// <param name="action">The JavaScript action.</param>
        public PdfJavaScriptActionEditorForm(PdfJavaScriptAction action)
        {
            InitializeComponent();

            _action = action;

            javaScriptTextBox.Text = PreprocessJavaScriptCode(action.JavaScript);
        }

        #endregion



        #region Methods

        /// <summary>
        /// Handles the Click event of OkButton object.
        /// </summary>
        private void okButton_Click(object sender, EventArgs e)
        {
            // update JavaScript code
            _action.JavaScript = javaScriptTextBox.Text;
        }


        /// <summary>
        /// Executes preprocessing of JavaScript code.
        /// </summary>
        /// <param name="jsCode">The JavaScript code.</param>
        private string PreprocessJavaScriptCode(string jsCode)
        {
            jsCode = jsCode.Replace("\r\n", "\n");
            jsCode = jsCode.Replace("\r", "\n");
            jsCode = jsCode.Replace("\n", "\r\n");
            return jsCode;
        }

        #endregion

    }
}
