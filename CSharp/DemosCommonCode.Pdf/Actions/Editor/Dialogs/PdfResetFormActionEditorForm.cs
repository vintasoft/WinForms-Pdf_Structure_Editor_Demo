using System;
using System.Windows.Forms;

using Vintasoft.Imaging.Pdf.Tree.InteractiveForms;


namespace DemosCommonCode.Pdf
{
    /// <summary>
    /// A form that allows to view and edit PDF reset form action.
    /// </summary>
    public partial class PdfResetFormActionEditorForm : Form
    {

        #region Fields

        /// <summary>
        /// The PDF reset form action.
        /// </summary>
        PdfResetFormAction _action;

        #endregion



        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PdfResetFormActionEditorForm"/> class.
        /// </summary>
        /// <param name="action">The PDF reset form action.</param>
        public PdfResetFormActionEditorForm(PdfResetFormAction action)
        {
            InitializeComponent();

            _action = action;

            pdfInteractiveFormFieldListEditorControl.InteractiveForm = action.Document.InteractiveForm;
            excludeSelectedFieldsCheckBox.Checked = action.FieldsIsExclude;

            if (action.Fields != null)
            {
                selectedFieldsRadioButton.Checked = true;
                pdfInteractiveFormFieldListEditorControl.SelectedFields = action.Fields.ToArray();
            }
        }

        #endregion



        #region Methods

        /// <summary>
        /// Handles the Click event of OkButton object.
        /// </summary>
        private void okButton_Click(object sender, EventArgs e)
        {
            // if all fields must be reset
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
        private void RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            // if action fields must be selected
            if (selectedFieldsRadioButton.Checked)
                selectedFieldsGroupBox.Enabled = true;
            else
                selectedFieldsGroupBox.Enabled = false;
        }

        #endregion

    }
}
