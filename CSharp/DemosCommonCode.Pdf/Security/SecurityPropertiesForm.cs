using System;
using System.Windows.Forms;

using Vintasoft.Imaging.Pdf;
using Vintasoft.Imaging.Pdf.Security;

namespace DemosCommonCode.Pdf.Security
{
    /// <summary>
    /// A form that shows the security properties of PDF document.
    /// </summary>
    public partial class SecurityPropertiesForm : Form
    {

        #region Constants

        /// <summary>
        /// A message that specifies that user access permission is not allowed.
        /// </summary>
        const string NOT_ALLOWED = "Not Allowed";

        #endregion



        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SecurityPropertiesForm"/> class.
        /// </summary>
        /// <param name="document">The PDF document.</param>
        public SecurityPropertiesForm(PdfDocument document)
        {
            InitializeComponent();

            // if document is encrypted
            if (document.IsEncrypted)
            {
                // init the encryption system

                encryptionLabel.Text = document.EncryptionSystem.ToString();

                compatibilityModeLabel.Text = document.EncryptionSystem.CompatibilityMode.ToString();

                authorizationResultLabel.Text = document.AuthorizationResult.ToString();

                if (document.EncryptionSystem.ContainsUserPassword)
                    userPasswordLabel.Text = "Yes";
                if (document.EncryptionSystem.ContainsOwnerPassword)
                    ownerPasswordLabel.Text = "Yes";


                // get the user access permissions
                UserAccessPermissions permissions = document.EncryptionSystem.UserPermissions;

                // init the user access permissions

                if ((permissions & UserAccessPermissions.AssembleDocument) == 0)
                    assembleDocumentLabel.Text = NOT_ALLOWED;

                if ((permissions & UserAccessPermissions.ExtractTextAndGraphics) == 0)
                    extractTextAndGraphicsLabel.Text = NOT_ALLOWED;

                if ((permissions & UserAccessPermissions.ExtractTextAndGraphicsForAccessibility) == 0)
                    extractTextAndGraphicsForAccessibilityLabel.Text = NOT_ALLOWED;

                if ((permissions & UserAccessPermissions.FillInteractiveFormFields) == 0)
                    fillInteractiveFormFieldsLabel.Text = NOT_ALLOWED;

                if ((permissions & UserAccessPermissions.ModifyAnnotations) == 0)
                    modifyAnnotationLabel.Text = NOT_ALLOWED;

                if ((permissions & UserAccessPermissions.ModifyContents) == 0)
                    modifyContentsLabel.Text = NOT_ALLOWED;

                if ((permissions & UserAccessPermissions.PrintDocumentInLowResolution) == 0)
                {
                    printingLabel.Text = NOT_ALLOWED;
                }
                else
                {
                    if ((permissions & UserAccessPermissions.PrintDocumentInHighResolution) == 0)
                        printingLabel.Text += " (Low Resolution)";
                    else
                        printingLabel.Text += " (High Resolution)";
                }
            }
            else
            {
                encryptionLabel.Text = "No Encryption";

                compatibilityModeLabel.Text = "";
            }
        }

        #endregion



        #region Methods

        /// <summary>
        /// "OK" button is clicked.
        /// </summary>
        private void okButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        #endregion

    }
}
