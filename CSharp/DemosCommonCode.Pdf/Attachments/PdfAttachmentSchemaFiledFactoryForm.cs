using System;
using System.Windows.Forms;

using Vintasoft.Imaging.Pdf;
using Vintasoft.Imaging.Pdf.Tree.FileAttachments;

namespace DemosCommonCode.Pdf
{
    /// <summary>
    /// A form that allows to create a new field of attachment collection schema.
    /// </summary>
    public partial class PdfAttachmentSchemaFieldFactoryForm : Form
    {

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PdfAttachmentSchemaFieldFactoryForm"/> class.
        /// </summary>
        public PdfAttachmentSchemaFieldFactoryForm()
        {
            InitializeComponent();
            fieldTypeComboBox.Items.Add(AttachmentCollectionSchemaFieldDataType.Filename);
            fieldTypeComboBox.Items.Add(AttachmentCollectionSchemaFieldDataType.CompressedSize);
            fieldTypeComboBox.Items.Add(AttachmentCollectionSchemaFieldDataType.UncompressedSize);
            fieldTypeComboBox.Items.Add(AttachmentCollectionSchemaFieldDataType.FileDescription);
            fieldTypeComboBox.Items.Add(AttachmentCollectionSchemaFieldDataType.CreationDate);
            fieldTypeComboBox.Items.Add(AttachmentCollectionSchemaFieldDataType.ModificationDate);
            fieldTypeComboBox.Items.Add(AttachmentCollectionSchemaFieldDataType.String);
            fieldTypeComboBox.Items.Add(AttachmentCollectionSchemaFieldDataType.Number);
            fieldTypeComboBox.Items.Add(AttachmentCollectionSchemaFieldDataType.Date);
            fieldTypeComboBox.SelectedItem = AttachmentCollectionSchemaFieldDataType.String;
        }

        #endregion



        #region Properties

        /// <summary>
        /// Gets the data type of new field.
        /// </summary>
        public AttachmentCollectionSchemaFieldDataType DataType
        {
            get
            {
                return (AttachmentCollectionSchemaFieldDataType)fieldTypeComboBox.SelectedItem;
            }
        }

        /// <summary>
        /// Gets the displayed name of new field.
        /// </summary>
        public string DisplayedName
        {
            get
            {
                return displayedNameTextBox.Text;
            }
        }

        /// <summary>
        /// Gets the name of new field.
        /// </summary>
        public string FieldName
        {
            get
            {
                return nameTextBox.Text;
            }
        }

        #endregion



        #region Methods

        #region PUBLIC

        /// <summary>
        /// Creates new schema field.
        /// </summary>
        /// <param name="document">The document.</param>
        /// <param name="name">The name.</param>
        /// <returns>A new instance of PdfAttachmentCollectionSchemaField class.</returns>
        public static PdfAttachmentCollectionSchemaField CreateSchemaField(PdfDocument document, out string name)
        {
            name = "";
            PdfAttachmentSchemaFieldFactoryForm dialog = new PdfAttachmentSchemaFieldFactoryForm();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                name = dialog.FieldName;
                return new PdfAttachmentCollectionSchemaField(document, dialog.DisplayedName, dialog.DataType);
            }
            return null;
        }

        #endregion


        #region PRIVATE

        /// <summary>
        /// Handles the Click event of cancelButton object.
        /// </summary>
        private void cancelButton_Click(object sender, EventArgs e)
        {
            // cancels new field creation and closes this form
            DialogResult = DialogResult.Cancel;
        }

        /// <summary>
        /// Handles the Click event of okButton object.
        /// </summary>
        private void okButton_Click(object sender, EventArgs e)
        {
            // closes this form
            DialogResult = DialogResult.OK;
        }

        /// <summary>
        /// Handles the TextChanged event of displayedNameTextBox object.
        /// </summary>
        private void displayedNameTextBox_TextChanged(object sender, EventArgs e)
        {
            UpdateUI();
        }

        /// <summary>
        /// Handles the TextChanged event of nameTextBox object.
        /// </summary>
        private void nameTextBox_TextChanged(object sender, EventArgs e)
        {
            UpdateUI();
        }



        /// <summary>
        /// Updates the User Interface.
        /// </summary>
        private void UpdateUI()
        {
            okButton.Enabled = DisplayedName != "" && FieldName != null;
        }

        #endregion

        #endregion

    }
}
