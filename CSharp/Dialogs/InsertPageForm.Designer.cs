namespace PdfStructureEditorDemo
{
    partial class InsertPageForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InsertPageForm));
            this.okButton = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.pageWithImageRadioButton = new System.Windows.Forms.RadioButton();
            this.emptyPageRadioButton = new System.Windows.Forms.RadioButton();
            this.selectImageButton = new System.Windows.Forms.Button();
            this.insertIndexLabel = new System.Windows.Forms.Label();
            this.insertIndexNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.openImageDialog = new System.Windows.Forms.OpenFileDialog();
            this.filterComboBox = new System.Windows.Forms.ComboBox();
            this.encoderLabel = new System.Windows.Forms.Label();
            this.jpegFilterParams = new System.Windows.Forms.Panel();
            this.qualityLabel = new System.Windows.Forms.Label();
            this.jpegQuality = new System.Windows.Forms.NumericUpDown();
            this.flateFilterParams = new System.Windows.Forms.Panel();
            this.levelLabel = new System.Windows.Forms.Label();
            this.flateCompressionLevel = new System.Windows.Forms.NumericUpDown();
            this.Jbig2CodecParams = new System.Windows.Forms.Panel();
            this.cbJbig2Lossy = new System.Windows.Forms.CheckBox();
            this.cbJbig2UseGlobals = new System.Windows.Forms.CheckBox();
            this.jpeg2000EncoderSettingsButton = new System.Windows.Forms.Button();
            this.sizeButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.insertIndexNumericUpDown)).BeginInit();
            this.jpegFilterParams.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.jpegQuality)).BeginInit();
            this.flateFilterParams.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.flateCompressionLevel)).BeginInit();
            this.Jbig2CodecParams.SuspendLayout();
            this.SuspendLayout();
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(274, 112);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 27);
            this.okButton.TabIndex = 0;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(355, 112);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 27);
            this.buttonCancel.TabIndex = 1;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // pageWithImageRadioButton
            // 
            this.pageWithImageRadioButton.AutoSize = true;
            this.pageWithImageRadioButton.Location = new System.Drawing.Point(9, 16);
            this.pageWithImageRadioButton.Name = "pageWithImageRadioButton";
            this.pageWithImageRadioButton.Size = new System.Drawing.Size(104, 17);
            this.pageWithImageRadioButton.TabIndex = 2;
            this.pageWithImageRadioButton.Text = "Page with Image";
            this.pageWithImageRadioButton.UseVisualStyleBackColor = true;
            this.pageWithImageRadioButton.CheckedChanged += new System.EventHandler(this.pageWithImageRadioButton_CheckedChanged);
            // 
            // emptyPageRadioButton
            // 
            this.emptyPageRadioButton.AutoSize = true;
            this.emptyPageRadioButton.Checked = true;
            this.emptyPageRadioButton.Location = new System.Drawing.Point(9, 56);
            this.emptyPageRadioButton.Name = "emptyPageRadioButton";
            this.emptyPageRadioButton.Size = new System.Drawing.Size(82, 17);
            this.emptyPageRadioButton.TabIndex = 3;
            this.emptyPageRadioButton.TabStop = true;
            this.emptyPageRadioButton.Text = "Empty Page";
            this.emptyPageRadioButton.UseVisualStyleBackColor = true;
            this.emptyPageRadioButton.CheckedChanged += new System.EventHandler(this.emptyPageRadioButton_CheckedChanged);
            // 
            // selectImageButton
            // 
            this.selectImageButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.selectImageButton.Location = new System.Drawing.Point(123, 16);
            this.selectImageButton.Name = "selectImageButton";
            this.selectImageButton.Size = new System.Drawing.Size(157, 23);
            this.selectImageButton.TabIndex = 8;
            this.selectImageButton.Text = "Select image...";
            this.selectImageButton.UseVisualStyleBackColor = true;
            this.selectImageButton.Click += new System.EventHandler(this.selectImageButton_Click);
            // 
            // insertIndexLabel
            // 
            this.insertIndexLabel.AutoSize = true;
            this.insertIndexLabel.Location = new System.Drawing.Point(23, 114);
            this.insertIndexLabel.Name = "insertIndexLabel";
            this.insertIndexLabel.Size = new System.Drawing.Size(68, 13);
            this.insertIndexLabel.TabIndex = 11;
            this.insertIndexLabel.Text = "Insert Index: ";
            // 
            // insertIndexNumericUpDown
            // 
            this.insertIndexNumericUpDown.Location = new System.Drawing.Point(123, 112);
            this.insertIndexNumericUpDown.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.insertIndexNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.insertIndexNumericUpDown.Name = "insertIndexNumericUpDown";
            this.insertIndexNumericUpDown.Size = new System.Drawing.Size(60, 20);
            this.insertIndexNumericUpDown.TabIndex = 12;
            this.insertIndexNumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // openImageDialog
            // 
            this.openImageDialog.Filter = resources.GetString("openImageDialog.Filter");
            this.openImageDialog.FilterIndex = 0;
            // 
            // filterComboBox
            // 
            this.filterComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.filterComboBox.FormattingEnabled = true;
            this.filterComboBox.Location = new System.Drawing.Point(351, 16);
            this.filterComboBox.Name = "filterComboBox";
            this.filterComboBox.Size = new System.Drawing.Size(79, 21);
            this.filterComboBox.TabIndex = 13;
            this.filterComboBox.SelectedIndexChanged += new System.EventHandler(this.filterComboBox_SelectedIndexChanged);
            // 
            // encoderLabel
            // 
            this.encoderLabel.AutoSize = true;
            this.encoderLabel.Location = new System.Drawing.Point(298, 23);
            this.encoderLabel.Name = "encoderLabel";
            this.encoderLabel.Size = new System.Drawing.Size(47, 13);
            this.encoderLabel.TabIndex = 14;
            this.encoderLabel.Text = "Encoder";
            // 
            // jpegFilterParams
            // 
            this.jpegFilterParams.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.jpegFilterParams.Controls.Add(this.qualityLabel);
            this.jpegFilterParams.Controls.Add(this.jpegQuality);
            this.jpegFilterParams.Location = new System.Drawing.Point(301, 43);
            this.jpegFilterParams.Name = "jpegFilterParams";
            this.jpegFilterParams.Size = new System.Drawing.Size(129, 45);
            this.jpegFilterParams.TabIndex = 15;
            // 
            // qualityLabel
            // 
            this.qualityLabel.AutoSize = true;
            this.qualityLabel.Location = new System.Drawing.Point(8, 14);
            this.qualityLabel.Name = "qualityLabel";
            this.qualityLabel.Size = new System.Drawing.Size(39, 13);
            this.qualityLabel.TabIndex = 11;
            this.qualityLabel.Text = "Quality";
            // 
            // jpegQuality
            // 
            this.jpegQuality.Location = new System.Drawing.Point(64, 9);
            this.jpegQuality.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.jpegQuality.Name = "jpegQuality";
            this.jpegQuality.Size = new System.Drawing.Size(60, 20);
            this.jpegQuality.TabIndex = 10;
            this.jpegQuality.Value = new decimal(new int[] {
            95,
            0,
            0,
            0});
            // 
            // flateFilterParams
            // 
            this.flateFilterParams.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flateFilterParams.Controls.Add(this.levelLabel);
            this.flateFilterParams.Controls.Add(this.flateCompressionLevel);
            this.flateFilterParams.Location = new System.Drawing.Point(301, 43);
            this.flateFilterParams.Name = "flateFilterParams";
            this.flateFilterParams.Size = new System.Drawing.Size(129, 45);
            this.flateFilterParams.TabIndex = 16;
            // 
            // levelLabel
            // 
            this.levelLabel.AutoSize = true;
            this.levelLabel.Location = new System.Drawing.Point(14, 14);
            this.levelLabel.Name = "levelLabel";
            this.levelLabel.Size = new System.Drawing.Size(33, 13);
            this.levelLabel.TabIndex = 11;
            this.levelLabel.Text = "Level";
            // 
            // flateCompressionLevel
            // 
            this.flateCompressionLevel.Location = new System.Drawing.Point(64, 9);
            this.flateCompressionLevel.Maximum = new decimal(new int[] {
            9,
            0,
            0,
            0});
            this.flateCompressionLevel.Name = "flateCompressionLevel";
            this.flateCompressionLevel.Size = new System.Drawing.Size(60, 20);
            this.flateCompressionLevel.TabIndex = 10;
            this.flateCompressionLevel.Value = new decimal(new int[] {
            6,
            0,
            0,
            0});
            // 
            // Jbig2CodecParams
            // 
            this.Jbig2CodecParams.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Jbig2CodecParams.Controls.Add(this.cbJbig2Lossy);
            this.Jbig2CodecParams.Controls.Add(this.cbJbig2UseGlobals);
            this.Jbig2CodecParams.Location = new System.Drawing.Point(301, 43);
            this.Jbig2CodecParams.Name = "Jbig2CodecParams";
            this.Jbig2CodecParams.Size = new System.Drawing.Size(129, 45);
            this.Jbig2CodecParams.TabIndex = 16;
            // 
            // cbJbig2Lossy
            // 
            this.cbJbig2Lossy.AutoSize = true;
            this.cbJbig2Lossy.Location = new System.Drawing.Point(3, 23);
            this.cbJbig2Lossy.Name = "cbJbig2Lossy";
            this.cbJbig2Lossy.Size = new System.Drawing.Size(53, 17);
            this.cbJbig2Lossy.TabIndex = 12;
            this.cbJbig2Lossy.Text = "Lossy";
            this.cbJbig2Lossy.UseVisualStyleBackColor = true;
            // 
            // cbJbig2UseGlobals
            // 
            this.cbJbig2UseGlobals.AutoSize = true;
            this.cbJbig2UseGlobals.Checked = true;
            this.cbJbig2UseGlobals.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbJbig2UseGlobals.Location = new System.Drawing.Point(3, 3);
            this.cbJbig2UseGlobals.Name = "cbJbig2UseGlobals";
            this.cbJbig2UseGlobals.Size = new System.Drawing.Size(83, 17);
            this.cbJbig2UseGlobals.TabIndex = 11;
            this.cbJbig2UseGlobals.Text = "Use Globals";
            this.cbJbig2UseGlobals.UseVisualStyleBackColor = true;
            // 
            // jpeg2000EncoderSettingsButton
            // 
            this.jpeg2000EncoderSettingsButton.Location = new System.Drawing.Point(301, 40);
            this.jpeg2000EncoderSettingsButton.Name = "jpeg2000EncoderSettingsButton";
            this.jpeg2000EncoderSettingsButton.Size = new System.Drawing.Size(129, 23);
            this.jpeg2000EncoderSettingsButton.TabIndex = 17;
            this.jpeg2000EncoderSettingsButton.Text = "Settings...";
            this.jpeg2000EncoderSettingsButton.UseVisualStyleBackColor = true;
            this.jpeg2000EncoderSettingsButton.Visible = false;
            this.jpeg2000EncoderSettingsButton.Click += new System.EventHandler(this.jpeg2000EncoderSettingsButton_Click);
            // 
            // sizeButton
            // 
            this.sizeButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.sizeButton.Location = new System.Drawing.Point(123, 50);
            this.sizeButton.Name = "sizeButton";
            this.sizeButton.Size = new System.Drawing.Size(157, 23);
            this.sizeButton.TabIndex = 18;
            this.sizeButton.Text = "Size...";
            this.sizeButton.UseVisualStyleBackColor = true;
            this.sizeButton.Click += new System.EventHandler(this.editSizeButton_Click);
            // 
            // InsertPageForm
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(442, 145);
            this.Controls.Add(this.sizeButton);
            this.Controls.Add(this.jpeg2000EncoderSettingsButton);
            this.Controls.Add(this.encoderLabel);
            this.Controls.Add(this.filterComboBox);
            this.Controls.Add(this.insertIndexNumericUpDown);
            this.Controls.Add(this.insertIndexLabel);
            this.Controls.Add(this.selectImageButton);
            this.Controls.Add(this.emptyPageRadioButton);
            this.Controls.Add(this.pageWithImageRadioButton);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.Jbig2CodecParams);
            this.Controls.Add(this.jpegFilterParams);
            this.Controls.Add(this.flateFilterParams);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "InsertPageForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Insert Page";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.insertIndexNumericUpDown)).EndInit();
            this.jpegFilterParams.ResumeLayout(false);
            this.jpegFilterParams.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.jpegQuality)).EndInit();
            this.flateFilterParams.ResumeLayout(false);
            this.flateFilterParams.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.flateCompressionLevel)).EndInit();
            this.Jbig2CodecParams.ResumeLayout(false);
            this.Jbig2CodecParams.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.RadioButton pageWithImageRadioButton;
        private System.Windows.Forms.RadioButton emptyPageRadioButton;
        private System.Windows.Forms.Button selectImageButton;
        private System.Windows.Forms.Label insertIndexLabel;
        private System.Windows.Forms.NumericUpDown insertIndexNumericUpDown;
        private System.Windows.Forms.OpenFileDialog openImageDialog;
        private System.Windows.Forms.ComboBox filterComboBox;
        private System.Windows.Forms.Label encoderLabel;
        private System.Windows.Forms.Panel jpegFilterParams;
        private System.Windows.Forms.Label qualityLabel;
        private System.Windows.Forms.NumericUpDown jpegQuality;
        private System.Windows.Forms.Panel flateFilterParams;
        private System.Windows.Forms.Label levelLabel;
        private System.Windows.Forms.NumericUpDown flateCompressionLevel;
        private System.Windows.Forms.Panel Jbig2CodecParams;
        private System.Windows.Forms.CheckBox cbJbig2Lossy;
        private System.Windows.Forms.CheckBox cbJbig2UseGlobals;
        private System.Windows.Forms.Button jpeg2000EncoderSettingsButton;
        private System.Windows.Forms.Button sizeButton;
    }
}