namespace DemosCommonCode.Pdf
{
    partial class PdfResetFormActionEditorForm
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
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(482, 347);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 0;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(563, 347);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 1;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // allFieldsRadioButton
            // 
            this.allFieldsRadioButton.AutoSize = true;
            this.allFieldsRadioButton.Checked = true;
            this.allFieldsRadioButton.Location = new System.Drawing.Point(18, 12);
            this.allFieldsRadioButton.Name = "allFieldsRadioButton";
            this.allFieldsRadioButton.Size = new System.Drawing.Size(66, 17);
            this.allFieldsRadioButton.TabIndex = 2;
            this.allFieldsRadioButton.TabStop = true;
            this.allFieldsRadioButton.Text = "All Fields";
            this.allFieldsRadioButton.UseVisualStyleBackColor = true;
            this.allFieldsRadioButton.CheckedChanged += new System.EventHandler(this.RadioButton_CheckedChanged);
            // 
            // selectedFieldsRadioButton
            // 
            this.selectedFieldsRadioButton.AutoSize = true;
            this.selectedFieldsRadioButton.Location = new System.Drawing.Point(18, 35);
            this.selectedFieldsRadioButton.Name = "selectedFieldsRadioButton";
            this.selectedFieldsRadioButton.Size = new System.Drawing.Size(97, 17);
            this.selectedFieldsRadioButton.TabIndex = 3;
            this.selectedFieldsRadioButton.TabStop = true;
            this.selectedFieldsRadioButton.Text = "Selected Fields";
            this.selectedFieldsRadioButton.UseVisualStyleBackColor = true;
            this.selectedFieldsRadioButton.CheckedChanged += new System.EventHandler(this.RadioButton_CheckedChanged);
            // 
            // selectedFieldsGroupBox
            // 
            this.selectedFieldsGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.selectedFieldsGroupBox.Controls.Add(this.excludeSelectedFieldsCheckBox);
            this.selectedFieldsGroupBox.Controls.Add(this.pdfInteractiveFormFieldListEditorControl);
            this.selectedFieldsGroupBox.Enabled = false;
            this.selectedFieldsGroupBox.Location = new System.Drawing.Point(12, 44);
            this.selectedFieldsGroupBox.Name = "selectedFieldsGroupBox";
            this.selectedFieldsGroupBox.Size = new System.Drawing.Size(620, 297);
            this.selectedFieldsGroupBox.TabIndex = 14;
            this.selectedFieldsGroupBox.TabStop = false;
            // 
            // excludeSelectedFieldsCheckBox
            // 
            this.excludeSelectedFieldsCheckBox.AutoSize = true;
            this.excludeSelectedFieldsCheckBox.Location = new System.Drawing.Point(6, 14);
            this.excludeSelectedFieldsCheckBox.Name = "excludeSelectedFieldsCheckBox";
            this.excludeSelectedFieldsCheckBox.Size = new System.Drawing.Size(134, 17);
            this.excludeSelectedFieldsCheckBox.TabIndex = 14;
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
            this.pdfInteractiveFormFieldListEditorControl.ShowOnlyResettableFields = true;
            this.pdfInteractiveFormFieldListEditorControl.Size = new System.Drawing.Size(608, 254);
            this.pdfInteractiveFormFieldListEditorControl.TabIndex = 13;
            // 
            // PdfResetFormActionEditorForm
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
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PdfResetFormActionEditorForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Reset Form Action Editor";
            this.selectedFieldsGroupBox.ResumeLayout(false);
            this.selectedFieldsGroupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.RadioButton allFieldsRadioButton;
        private System.Windows.Forms.RadioButton selectedFieldsRadioButton;
        private PdfInteractiveFormFieldListEditorControl pdfInteractiveFormFieldListEditorControl;
        private System.Windows.Forms.GroupBox selectedFieldsGroupBox;
        private System.Windows.Forms.CheckBox excludeSelectedFieldsCheckBox;
    }
}