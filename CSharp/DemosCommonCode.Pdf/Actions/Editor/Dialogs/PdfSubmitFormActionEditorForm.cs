using System;
using System.Windows.Forms;

using Vintasoft.Imaging.Pdf.Tree.InteractiveForms;


namespace DemosCommonCode.Pdf
{
    /// <summary>
    /// A form that allows to view and edit PDF form submit action.
    /// </summary>
    public partial class PdfSubmitFormActionEditorForm : Form
    {

        #region Fields

        /// <summary>
        /// The PDF form submit action.
        /// </summary>
        PdfSubmitFormAction _action;

        #endregion



        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PdfSubmitFormActionEditorForm"/> class.
        /// </summary>
        /// <param name="action">The PDF form submit action.</param>
        public PdfSubmitFormActionEditorForm(PdfSubmitFormAction action)
        {
            InitializeComponent();

            foreach (PdfInteractiveFormFieldSubmitFormat format in Enum.GetValues(typeof(PdfInteractiveFormFieldSubmitFormat)))
                submitFormatComboBox.Items.Add(format);

            _action = action;

            submitFormatComboBox.SelectedItem = action.SubmitFormat;
            submitUrlTextBox.Text = action.Url;
            okButton.Enabled = !string.IsNullOrEmpty(submitUrlTextBox.Text);
            pdfInteractiveFormFieldListEditorControl.InteractiveForm = action.Document.InteractiveForm;
            excludeSelectedFieldsCheckBox.Checked = action.FieldsIsExclude;

            if (_action.Fields != null)
            {
                selectedFieldsRadioButton.Checked = true;
                pdfInteractiveFormFieldListEditorControl.SelectedFields = _action.Fields.ToArray();
            }
        }

        #endregion



        #region Methods

        /// <summary>
        /// Handles the Click event of OkButton object.
        /// </summary>
        private void okButton_Click(object sender, EventArgs e)
        {
            // remove interactive form
            pdfInteractiveFormFieldListEditorControl.InteractiveForm = null;

            // update action submit format
            _action.SubmitFormat = (PdfInteractiveFormFieldSubmitFormat)submitFormatComboBox.SelectedItem;
            // update action url
            _action.Url = submitUrlTextBox.Text;

            // if all fields must be submit
            if (allFieldsRadioButton.Checked)
            {
                _action.Fields = null;
            }
            else
            {
                // if selected fields must be excluded
                if (excludeSelectedFieldsCheckBox.Checked)
                    _action.FieldsIsExclude = true;
                else
                    _action.FieldsIsExclude = false;

                // if action fields does not exist
                if (_action.Fields == null)
                    // create fields list
                    _action.Fields = new PdfInteractiveFormFieldList(_action.Document);
                else
                    // clear fields list
                    _action.Fields.Clear();

                // add selected fields to action
                _action.Fields.AddRange(pdfInteractiveFormFieldListEditorControl.SelectedFields);
            }
        }

        /// <summary>
        /// Handles the CheckedChanged event of RadioButton object.
        /// </summary>
        private void radioButton_CheckedChanged(object sender, EventArgs e)
        {
            // if action fields must be selected
            if (selectedFieldsRadioButton.Checked)
                selectedFieldsGroupBox.Enabled = true;
            else
                selectedFieldsGroupBox.Enabled = false;
        }

        /// <summary>
        /// Handles the TextChanged event of SubmitUrlTextBox object.
        /// </summary>
        private void submitUrlTextBox_TextChanged(object sender, EventArgs e)
        {
            // if current form can not be closed
            if (string.IsNullOrEmpty(submitUrlTextBox.Text))
                okButton.Enabled = false;
            else
                okButton.Enabled = true;
        }

        #endregion

    }
}
