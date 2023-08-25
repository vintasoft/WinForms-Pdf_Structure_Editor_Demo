using System.Windows.Forms;

using Vintasoft.Imaging.Pdf;
using Vintasoft.Imaging.Pdf.Tree;
using Vintasoft.Imaging.Pdf.Tree.InteractiveForms;


namespace DemosCommonCode.Pdf
{
    /// <summary>
    /// A form that allows to create PDF action of PDF document.
    /// </summary>
    public partial class CreatePdfActionForm : Form
    {

        #region Constructors

        /// <summary>
        /// Prevents a default instance of the <see cref="CreatePdfActionForm"/> class
        /// from being created.
        /// </summary>
        private CreatePdfActionForm()
        {
            InitializeComponent();

            actionsListBox.Items.Add("Goto Action");
            actionsListBox.Items.Add("Uri Action");
            actionsListBox.Items.Add("Launch Action");
            actionsListBox.Items.Add("JavaScript Action");
            actionsListBox.Items.Add("Annotation Hide Action");
            actionsListBox.Items.Add("Submit Form Action");
            actionsListBox.Items.Add("Reset Form Action");
            actionsListBox.Items.Add("Named Action");

            actionsListBox.SelectedIndex = 0;
        }

        #endregion



        #region Methods

        /// <summary>
        /// Creates the action of PDF document.
        /// </summary>
        /// <param name="document">The PDF document.</param>
        /// <param name="owner">The owner of the dialog.</param>
        public static PdfAction CreateAction(PdfDocument document, Form owner)
        {
            // create dialog
            using (CreatePdfActionForm dialog = new CreatePdfActionForm())
            {
                if (owner != null)
                    dialog.Owner = owner;
                // show dialog
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    switch (dialog.actionsListBox.SelectedIndex)
                    {
                        case 0:
                            // create goto action
                            return new PdfGotoAction(document);

                        case 1:
                            // create uri action
                            return new PdfUriAction(document);

                        case 2:
                            // create launch action
                            return new PdfLaunchAction(document);

                        case 3:
                            // create java script action
                            return new PdfJavaScriptAction(document);

                        case 4:
                            // create annotation hide action
                            return new PdfAnnotationHideAction(document);

                        case 5:
                            // create submit form action
                            return new PdfSubmitFormAction(document);

                        case 6:
                            // create reset form action
                            return new PdfResetFormAction(document);

                        case 7:
                            // create named action
                            return new PdfNamedAction(document);
                    }
                }
            }

            return null;
        }

        #endregion

    }
}
