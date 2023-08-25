using System;
using System.Windows.Forms;

using Vintasoft.Imaging.Pdf.Tree.InteractiveForms;
using System.ComponentModel;


namespace DemosCommonCode.Pdf
{
    /// <summary>
    /// A control that allows to view and edit a list of PDF interactive form fields.
    /// </summary>
    public partial class PdfInteractiveFormFieldListEditorControl : UserControl
    {

        #region Nested classes

        /// <summary>
        /// List box item.
        /// </summary>
        class ListBoxItem
        {

            /// <summary>
            /// Initializes a new instance of the <see cref="ListBoxItem"/> class.
            /// </summary>
            /// <param name="field">The field.</param>
            public ListBoxItem(PdfInteractiveFormField field)
            {
                Field = field;
            }


            PdfInteractiveFormField _field = null;
            /// <summary>
            /// Gets or sets the field.
            /// </summary>
            public PdfInteractiveFormField Field
            {
                get
                {
                    return _field;
                }
                set
                {
                    _field = value;
                }
            }



            /// <summary>
            /// Returns a <see cref="System.String" /> that represents this instance.
            /// </summary>
            /// <returns>
            /// A <see cref="System.String" /> that represents this instance.
            /// </returns>
            public override string ToString()
            {
                return Field.FullyQualifiedName;
            }

        }

        #endregion



        #region Constructors

        /// <summary>
        /// Initializes a new instance of
        /// the <see cref="PdfInteractiveFormFieldListEditorControl"/> class.
        /// </summary>
        public PdfInteractiveFormFieldListEditorControl()
        {
            InitializeComponent();
        }

        #endregion



        #region Properties

        /// <summary>
        /// Gets or sets the PDF interactive form.
        /// </summary>
        public PdfDocumentInteractiveForm InteractiveForm
        {
            get
            {
                return pdfInteractiveFormFieldTree.InteractiveForm;
            }
            set
            {
                pdfInteractiveFormFieldTree.InteractiveForm = value;
                pdfInteractiveFormFieldTree.RefreshInteractiveFormTree();
            }
        }

        /// <summary>
        /// Gets or sets an array of selected PDF interactive form fields.
        /// </summary>
        [DefaultValue((object)null)]
        public PdfInteractiveFormField[] SelectedFields
        {
            get
            {
                if (DesignMode)
                    return null;
                ListBox.ObjectCollection items = selectedFieldsListBox.Items;
                PdfInteractiveFormField[] result =
                    new PdfInteractiveFormField[items.Count];

                for (int i = 0; i < result.Length; i++)
                    result[i] = ((ListBoxItem)items[i]).Field;

                return result;
            }
            set
            {
                selectedFieldsListBox.BeginUpdate();
                try
                {
                    ListBox.ObjectCollection items = selectedFieldsListBox.Items;
                    items.Clear();

                    if (value != null)
                    {
                        foreach (PdfInteractiveFormField field in value)
                        {
                            items.Add(new ListBoxItem(field));
                        }
                    }
                }
                finally
                {
                    selectedFieldsListBox.EndUpdate();
                }

                UpdateUI();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the tree view must show only form fields, which can export their values.
        /// </summary>
        [System.ComponentModel.DefaultValue(false)]
        public bool ShowOnlyExportableFields
        {
            get
            {
                return pdfInteractiveFormFieldTree.ShowOnlyExportableFields;
            }
            set
            {
                pdfInteractiveFormFieldTree.ShowOnlyExportableFields = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the tree view must show only form fields, which can reset their values.
        /// </summary>
        [System.ComponentModel.DefaultValue(false)]
        public bool ShowOnlyResettableFields
        {
            get
            {
                return pdfInteractiveFormFieldTree.ShowOnlyResettableFields;
            }
            set
            {
                pdfInteractiveFormFieldTree.ShowOnlyResettableFields = value;
            }
        }

        #endregion



        #region Methods

        #region UI 

        #region PdfInteractiveFormFieldTree

        /// <summary>
        /// Handles the NodeMouseDoubleClick event of PdfInteractiveFormFieldTree object.
        /// </summary>
        private void pdfInteractiveFormFieldTree_NodeMouseDoubleClick(
            object sender,
            TreeNodeMouseClickEventArgs e)
        {
            if (pdfInteractiveFormFieldTree.SelectedField != null && !SelectedFieldContainedListBox())
                AddFormFieldToSelectedFields();
        }

        /// <summary>
        /// Handles the AfterSelect event of the PdfInteractiveFormFieldTree control.
        /// </summary>
        private void pdfInteractiveFormFieldTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            UpdateUI();
        }

        #endregion


        #region SelectedFieldsListBox

        /// <summary>
        /// Selected PDF interactive form field is changed.
        /// </summary>
        private void selectedFieldsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateUI();
        }

        /// <summary>
        /// Mouse is double click in SelectedFieldsListBox.
        /// </summary>
        private void selectedFieldsListBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (selectedFieldsListBox.SelectedItem != null)
                removeButton_Click(sender, EventArgs.Empty);
        }

        #endregion


        /// <summary>
        /// Handles the Click event of AddButton object.
        /// </summary>
        private void addButton_Click(object sender, EventArgs e)
        {
            AddFormFieldToSelectedFields();
        }

        /// <summary>
        /// Handles the Click event of RemoveButton object.
        /// </summary>
        private void removeButton_Click(object sender, EventArgs e)
        {
            // get selected index
            int newIndex = selectedFieldsListBox.SelectedIndex;
            // if the last item will be removed
            if (newIndex == selectedFieldsListBox.Items.Count - 1)
                newIndex--;

            // remove selected item
            selectedFieldsListBox.Items.Remove(selectedFieldsListBox.SelectedItem);
            // update selected index
            selectedFieldsListBox.SelectedIndex = newIndex;

            UpdateUI();
        }

        /// <summary>
        /// Handles the Click event of RemoveAllButton object.
        /// </summary>
        private void removeAllButton_Click(object sender, EventArgs e)
        {
            // remove all PDF interactive form fields
            selectedFieldsListBox.Items.Clear();

            UpdateUI();
        }

        #endregion


        #region UI State

        /// <summary>
        /// Updates the user interface of this control.
        /// </summary>
        private void UpdateUI()
        {
            addButton.Enabled = pdfInteractiveFormFieldTree.SelectedField != null && !SelectedFieldContainedListBox();
            removeButton.Enabled = selectedFieldsListBox.SelectedItem != null;
            removeAllButton.Enabled = selectedFieldsListBox.Items.Count > 0;
        }

        #endregion


        /// <summary>
        /// Determines that SelectedFieldsListBox contains field from  PdfInteractiveFormFieldTree.
        /// </summary>
        /// <returns>
        /// <b>True</b> - SelectedFieldsListBox contains field from PdfInteractiveFormFieldTree;
        /// <b>false</b> - SelectedFieldsListBox does NOT contain field from PdfInteractiveFormFieldTree.
        /// </returns>
        private bool SelectedFieldContainedListBox()
        {
            // get selected field
            PdfInteractiveFormField selectedField = pdfInteractiveFormFieldTree.SelectedField;
            // if selected fiels exist
            if (selectedField != null)
            {
                // get items
                ListBox.ObjectCollection items = selectedFieldsListBox.Items;
                // find selected field
                foreach (ListBoxItem item in items)
                {
                    if (item.Field == selectedField)
                        return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Adds the selected form field to selected form field list.
        /// </summary>
        private void AddFormFieldToSelectedFields()
        {
            // suspend control invalidate
            selectedFieldsListBox.BeginUpdate();
            try
            {
                // get selected field
                PdfInteractiveFormField selectedField = pdfInteractiveFormFieldTree.SelectedField;

                // create list box item
                ListBoxItem item = new ListBoxItem(selectedField);
                // add item
                selectedFieldsListBox.Items.Add(item);
                // update selected item
                selectedFieldsListBox.SelectedIndex = selectedFieldsListBox.Items.Count - 1;

                // update user interface
                UpdateUI();
            }
            finally
            {
                // resume control invalidate
                selectedFieldsListBox.EndUpdate();
            }
        }

        #endregion

    }
}
