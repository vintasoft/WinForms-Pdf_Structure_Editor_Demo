namespace DemosCommonCode.Pdf
{
    partial class PdfSubmitFormActionEditorForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.submitUrlTextBox = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.submitFormatComboBox = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.okButton = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.allFieldsRadioButton = new System.Windows.Forms.RadioButton();
            this.selectedFieldsRadioButton = new System.Windows.Forms.RadioButton();
            this.selectedFieldsGroupBox = new System.Windows.Forms.GroupBox();
            this.excludeSelectedFieldsCheckBox = new System.Windows.Forms.CheckBox();
            this.pdfInteractiveFormFieldListEditorControl = new DemosCommonCode.Pdf.PdfInteractiveFormFieldListEditorControl();
            this.selectedFieldsGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // submitUrlTextBox
            // 
            this.submitUrlTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.submitUrlTextBox.Location = new System.Drawing.Point(55, 39);
            this.submitUrlTextBox.Name = "submitUrlTextBox";
            this.submitUrlTextBox.Size = new System.Drawing.Size(577, 20);
            this.submitUrlTextBox.TabIndex = 7;
            this.submitUrlTextBox.TextChanged += new System.EventHandler(this.submitUrlTextBox_TextChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(10, 42);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(29, 13);
            this.label8.TabIndex = 6;
            this.label8.Text = "URL";
            // 
            // submitFormatComboBox
            // 
            this.submitFormatComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.submitFormatComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.submitFormatComboBox.FormattingEnabled = true;
            this.submitFormatComboBox.Location = new System.Drawing.Point(55, 12);
            this.submitFormatComboBox.Name = "submitFormatComboBox";
            this.submitFormatComboBox.Size = new System.Drawing.Size(577, 21);
            this.submitFormatComboBox.TabIndex = 5;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(10, 15);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(39, 13);
            this.label7.TabIndex = 4;
            this.label7.Text = "Format";
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(476, 341);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 8;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(557, 341);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 9;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // allFieldsRadioButton
            // 
            this.allFieldsRadioButton.AutoSize = true;
            this.allFieldsRadioButton.Checked = true;
            this.allFieldsRadioButton.Location = new System.Drawing.Point(19, 65);
            this.allFieldsRadioButton.Name = "allFieldsRadioButton";
            this.allFieldsRadioButton.Size = new System.Drawing.Size(66, 17);
            this.allFieldsRadioButton.TabIndex = 10;
            this.allFieldsRadioButton.TabStop = true;
            this.allFieldsRadioButton.Text = "All Fields";
            this.allFieldsRadioButton.UseVisualStyleBackColor = true;
            this.allFieldsRadioButton.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // selectedFieldsRadioButton
            // 
            this.selectedFieldsRadioButton.AutoSize = true;
            this.selectedFieldsRadioButton.Location = new System.Drawing.Point(19, 88);
            this.selectedFieldsRadioButton.Name = "selectedFieldsRadioButton";
            this.selectedFieldsRadioButton.Size = new System.Drawing.Size(97, 17);
            this.selectedFieldsRadioButton.TabIndex = 11;
            this.selectedFieldsRadioButton.Text = "Selected Fields";
            this.selectedFieldsRadioButton.UseVisualStyleBackColor = true;
            this.selectedFieldsRadioButton.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // selectedFieldsGroupBox
            // 
            this.selectedFieldsGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.selectedFieldsGroupBox.Controls.Add(this.excludeSelectedFieldsCheckBox);
            this.selectedFieldsGroupBox.Controls.Add(this.pdfInteractiveFormFieldListEditorControl);
            this.selectedFieldsGroupBox.Enabled = false;
            this.selectedFieldsGroupBox.Location = new System.Drawing.Point(13, 97);
            this.selectedFieldsGroupBox.Name = "selectedFieldsGroupBox";
            this.selectedFieldsGroupBox.Size = new System.Drawing.Size(619, 238);
            this.selectedFieldsGroupBox.TabIndex = 13;
            this.selectedFieldsGroupBox.TabStop = false;
            // 
            // excludeSelectedFieldsCheckBox
            // 
            this.excludeSelectedFieldsCheckBox.AutoSize = true;
            this.excludeSelectedFieldsCheckBox.Location = new System.Drawing.Point(6, 14);
            this.excludeSelectedFieldsCheckBox.Name = "excludeSelectedFieldsCheckBox";
            this.excludeSelectedFieldsCheckBox.Size = new System.Drawing.Size(134, 17);
            this.excludeSelectedFieldsCheckBox.TabIndex = 13;
            this.excludeSelectedFieldsCheckBox.Text = "Exclude selected fields";
            this.excludeSelectedFieldsCheckBox.UseVisualStyleBackColor = true;
            // 
            // pdfInteractiveFormFieldListEditorControl
            // 
            this.pdfInteractiveFormFieldListEditorControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pdfInteractiveFormFieldListEditorControl.InteractiveForm = null;
            this.pdfInteractiveFormFieldListEditorControl.Location = new System.Drawing.Point(6, 37);
            this.pdfInteractiveFormFieldListEditorControl.MinimumSize = new System.Drawing.Size(273, 115);
            this.pdfInteractiveFormFieldListEditorControl.Name = "pdfInteractiveFormFieldListEditorControl";
            this.pdfInteractiveFormFieldListEditorControl.ShowOnlyExportableFields = true;
            this.pdfInteractiveFormFieldListEditorControl.Size = new System.Drawing.Size(607, 195);
            this.pdfInteractiveFormFieldListEditorControl.TabIndex = 12;
            // 
            // PdfSubmitFormActionEditorForm
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(644, 376);
            this.Controls.Add(this.selectedFieldsRadioButton);
            this.Controls.Add(this.selectedFieldsGroupBox);
            this.Controls.Add(this.allFieldsRadioButton);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.submitUrlTextBox);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.submitFormatComboBox);
            this.Controls.Add(this.label7);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "PdfSubmitFormActionEditorForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Submit Form Action Editor";
            this.selectedFieldsGroupBox.ResumeLayout(false);
            this.selectedFieldsGroupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox submitUrlTextBox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox submitFormatComboBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.RadioButton allFieldsRadioButton;
        private System.Windows.Forms.RadioButton selectedFieldsRadioButton;
        private PdfInteractiveFormFieldListEditorControl pdfInteractiveFormFieldListEditorControl;
        private System.Windows.Forms.GroupBox selectedFieldsGroupBox;
        private System.Windows.Forms.CheckBox excludeSelectedFieldsCheckBox;
    }
}