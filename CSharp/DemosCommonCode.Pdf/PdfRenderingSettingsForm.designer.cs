namespace DemosCommonCode.Pdf
{
    partial class PdfRenderingSettingsForm
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
            this.label2 = new System.Windows.Forms.Label();
            this.verticalResolutionNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.horizontalResolutionNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.okButton = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.renderingMode = new System.Windows.Forms.ComboBox();
            this.drawAnnotationsCheckBox = new System.Windows.Forms.CheckBox();
            this.cacheResourcesCheckBox = new System.Windows.Forms.CheckBox();
            this.cropPageAtCropBoxCheckBox = new System.Windows.Forms.CheckBox();
            this.defaultDpiCheckBox = new System.Windows.Forms.CheckBox();
            this.resolutionGroupBox = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.useRotatePropertyCheckBox = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.verticalResolutionNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.horizontalResolutionNumericUpDown)).BeginInit();
            this.resolutionGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(132, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(25, 15);
            this.label2.TabIndex = 12;
            this.label2.Text = "DPI";
            // 
            // verticalResolutionNumericUpDown
            // 
            this.verticalResolutionNumericUpDown.Location = new System.Drawing.Point(76, 19);
            this.verticalResolutionNumericUpDown.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.verticalResolutionNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.verticalResolutionNumericUpDown.Name = "verticalResolutionNumericUpDown";
            this.verticalResolutionNumericUpDown.Size = new System.Drawing.Size(50, 23);
            this.verticalResolutionNumericUpDown.TabIndex = 11;
            this.verticalResolutionNumericUpDown.Value = new decimal(new int[] {
            96,
            0,
            0,
            0});
            // 
            // horizontalResolutionNumericUpDown
            // 
            this.horizontalResolutionNumericUpDown.Location = new System.Drawing.Point(6, 19);
            this.horizontalResolutionNumericUpDown.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.horizontalResolutionNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.horizontalResolutionNumericUpDown.Name = "horizontalResolutionNumericUpDown";
            this.horizontalResolutionNumericUpDown.Size = new System.Drawing.Size(50, 23);
            this.horizontalResolutionNumericUpDown.TabIndex = 10;
            this.horizontalResolutionNumericUpDown.Value = new decimal(new int[] {
            96,
            0,
            0,
            0});
            this.horizontalResolutionNumericUpDown.ValueChanged += new System.EventHandler(this.horizontalResolution_ValueChanged);
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(65, 147);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(87, 23);
            this.okButton.TabIndex = 16;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 71);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(95, 15);
            this.label5.TabIndex = 17;
            this.label5.Text = "Rendering Mode";
            // 
            // renderingMode
            // 
            this.renderingMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.renderingMode.FormattingEnabled = true;
            this.renderingMode.Location = new System.Drawing.Point(124, 68);
            this.renderingMode.Name = "renderingMode";
            this.renderingMode.Size = new System.Drawing.Size(122, 23);
            this.renderingMode.TabIndex = 19;
            // 
            // drawAnnotationsCheckBox
            // 
            this.drawAnnotationsCheckBox.AutoSize = true;
            this.drawAnnotationsCheckBox.Location = new System.Drawing.Point(12, 96);
            this.drawAnnotationsCheckBox.Name = "drawAnnotationsCheckBox";
            this.drawAnnotationsCheckBox.Size = new System.Drawing.Size(121, 19);
            this.drawAnnotationsCheckBox.TabIndex = 21;
            this.drawAnnotationsCheckBox.Text = "Draw Annotations";
            this.drawAnnotationsCheckBox.UseVisualStyleBackColor = true;
            // 
            // cacheResourcesCheckBox
            // 
            this.cacheResourcesCheckBox.AutoSize = true;
            this.cacheResourcesCheckBox.Location = new System.Drawing.Point(12, 119);
            this.cacheResourcesCheckBox.Name = "cacheResourcesCheckBox";
            this.cacheResourcesCheckBox.Size = new System.Drawing.Size(115, 19);
            this.cacheResourcesCheckBox.TabIndex = 22;
            this.cacheResourcesCheckBox.Text = "Cache Resources";
            this.cacheResourcesCheckBox.UseVisualStyleBackColor = true;
            // 
            // cropPageAtCropBoxCheckBox
            // 
            this.cropPageAtCropBoxCheckBox.AutoSize = true;
            this.cropPageAtCropBoxCheckBox.Location = new System.Drawing.Point(139, 119);
            this.cropPageAtCropBoxCheckBox.Name = "cropPageAtCropBoxCheckBox";
            this.cropPageAtCropBoxCheckBox.Size = new System.Drawing.Size(143, 19);
            this.cropPageAtCropBoxCheckBox.TabIndex = 23;
            this.cropPageAtCropBoxCheckBox.Text = "Crop Page at CropBox";
            this.cropPageAtCropBoxCheckBox.UseVisualStyleBackColor = true;
            // 
            // defaultDpiCheckBox
            // 
            this.defaultDpiCheckBox.AutoSize = true;
            this.defaultDpiCheckBox.Location = new System.Drawing.Point(168, 22);
            this.defaultDpiCheckBox.Name = "defaultDpiCheckBox";
            this.defaultDpiCheckBox.Size = new System.Drawing.Size(64, 19);
            this.defaultDpiCheckBox.TabIndex = 24;
            this.defaultDpiCheckBox.Text = "Default";
            this.defaultDpiCheckBox.UseVisualStyleBackColor = true;
            this.defaultDpiCheckBox.CheckedChanged += new System.EventHandler(this.defaultDpiCheckBox_CheckedChanged);
            // 
            // resolutionGroupBox
            // 
            this.resolutionGroupBox.Controls.Add(this.label1);
            this.resolutionGroupBox.Controls.Add(this.horizontalResolutionNumericUpDown);
            this.resolutionGroupBox.Controls.Add(this.defaultDpiCheckBox);
            this.resolutionGroupBox.Controls.Add(this.verticalResolutionNumericUpDown);
            this.resolutionGroupBox.Controls.Add(this.label2);
            this.resolutionGroupBox.Location = new System.Drawing.Point(12, 12);
            this.resolutionGroupBox.Name = "resolutionGroupBox";
            this.resolutionGroupBox.Size = new System.Drawing.Size(234, 50);
            this.resolutionGroupBox.TabIndex = 25;
            this.resolutionGroupBox.TabStop = false;
            this.resolutionGroupBox.Text = "Resolution";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(60, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(14, 15);
            this.label1.TabIndex = 25;
            this.label1.Text = "X";
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(159, 147);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(87, 23);
            this.buttonCancel.TabIndex = 26;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // useRotatePropertyCheckBox
            // 
            this.useRotatePropertyCheckBox.AutoSize = true;
            this.useRotatePropertyCheckBox.Location = new System.Drawing.Point(139, 97);
            this.useRotatePropertyCheckBox.Name = "useRotatePropertyCheckBox";
            this.useRotatePropertyCheckBox.Size = new System.Drawing.Size(130, 19);
            this.useRotatePropertyCheckBox.TabIndex = 27;
            this.useRotatePropertyCheckBox.Text = "Use Rotate property";
            this.useRotatePropertyCheckBox.UseVisualStyleBackColor = true;
            // 
            // PdfRenderingSettingsForm
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(283, 183);
            this.Controls.Add(this.useRotatePropertyCheckBox);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.resolutionGroupBox);
            this.Controls.Add(this.cropPageAtCropBoxCheckBox);
            this.Controls.Add(this.cacheResourcesCheckBox);
            this.Controls.Add(this.drawAnnotationsCheckBox);
            this.Controls.Add(this.renderingMode);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.okButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PdfRenderingSettingsForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "PDF Rendering Settings";
            ((System.ComponentModel.ISupportInitialize)(this.verticalResolutionNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.horizontalResolutionNumericUpDown)).EndInit();
            this.resolutionGroupBox.ResumeLayout(false);
            this.resolutionGroupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        internal System.Windows.Forms.NumericUpDown verticalResolutionNumericUpDown;
        internal System.Windows.Forms.NumericUpDown horizontalResolutionNumericUpDown;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox renderingMode;
        private System.Windows.Forms.CheckBox drawAnnotationsCheckBox;
        private System.Windows.Forms.CheckBox cacheResourcesCheckBox;
        private System.Windows.Forms.CheckBox cropPageAtCropBoxCheckBox;
        private System.Windows.Forms.CheckBox defaultDpiCheckBox;
        private System.Windows.Forms.GroupBox resolutionGroupBox;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.CheckBox useRotatePropertyCheckBox;
        private System.Windows.Forms.Label label1;
    }
}