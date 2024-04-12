using System;
using System.Windows.Forms;

using Vintasoft.Imaging.Pdf;
using Vintasoft.Imaging.Pdf.Tree.FileAttachments;

namespace DemosCommonCode.Pdf
{
    /// <summary>
    /// A form that allows to create a new field for attachment data field.
    /// </summary>
    public partial class PdfAttachmentDataFieldFactoryForm : Form
    {

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PdfAttachmentDataFieldFactoryForm"/> class.
        /// </summary>
        public PdfAttachmentDataFieldFactoryForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PdfAttachmentDataFieldFactoryForm"/> class.
        /// </summary>
        /// <param name="schema">PDF attachment collection schema.</param>
        public PdfAttachmentDataFieldFactoryForm(PdfAttachmentCollectionSchema schema)
            : this()
        {
            if (schema != null)
            {
                foreach (String fieldName in schema.Keys)
                {
                    AttachmentCollectionSchemaFieldDataType dataType = schema[fieldName].DataType;
                    if (dataType == AttachmentCollectionSchemaFieldDataType.String ||
                        dataType == AttachmentCollectionSchemaFieldDataType.Number ||
                        dataType == AttachmentCollectionSchemaFieldDataType.Date)
                    {
                        fieldNameComboBox.Items.Add(fieldName);
                    }
                }
            }
        }

        #endregion



        #region Properties

        /// <summary>
        /// Gets the name of new field.
        /// </summary>
        public string FieldName
        {
            get
            {
                return fieldNameComboBox.Text;
            }
        }

        #endregion



        #region Methods

        #region PUBLIC

        /// <summary>
        /// Creates new data field.
        /// </summary>
        /// <param name="document">The document.</param>
        /// <param name="name">The name.</param>
        /// <returns>A new instance of PdfAttachmentDataField class.</returns>
        public static PdfAttachmentDataField CreateDataField(PdfDocument document, out string name)
        {
            name = "";
            PdfAttachmentDataFieldFactoryForm dialog = new PdfAttachmentDataFieldFactoryForm(document.Attachments.Schema);
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                name = dialog.FieldName;
                return new PdfAttachmentDataField(document);
            }
            return null;
        }

        #endregion


        #region PRIVATE

        /// <summary>
        /// Handles the Click event of okButton object.
        /// </summary>
        private void okButton_Click(object sender, EventArgs e)
        {
            // close this form
            DialogResult = DialogResult.OK;
        }

        /// <summary>
        /// Handles the Click event of cancelButton object.
        /// </summary>
        private void cancelButton_Click(object sender, EventArgs e)
        {
            // close this form
            DialogResult = DialogResult.Cancel;
        }

        /// <summary>
        /// Handles the SelectedValueChanged event of fieldNameComboBox object.
        /// </summary>
        private void fieldNameComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            UpdateUI();
        }


        
        /// <summary>
        /// Updates the User Interface.
        /// </summary>
        private void UpdateUI()
        {
            okButton.Enabled = FieldName != null;
        }

        #endregion

        #endregion

    }
}
