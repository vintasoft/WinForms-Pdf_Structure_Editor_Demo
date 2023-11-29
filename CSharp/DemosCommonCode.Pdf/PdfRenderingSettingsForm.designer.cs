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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.verticalResolutionNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.horizontalResolutionNumericUpDown)).BeginInit();
            this.resolutionGroupBox.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(135, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(25, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "DPI";
            // 
            // verticalResolutionNumericUpDown
            // 
            this.verticalResolutionNumericUpDown.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.verticalResolutionNumericUpDown.Location = new System.Drawing.Point(79, 3);
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
            this.verticalResolutionNumericUpDown.Size = new System.Drawing.Size(50, 20);
            this.verticalResolutionNumericUpDown.TabIndex = 11;
            this.verticalResolutionNumericUpDown.Value = new decimal(new int[] {
            96,
            0,
            0,
            0});
            // 
            // horizontalResolutionNumericUpDown
            // 
            this.horizontalResolutionNumericUpDown.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.horizontalResolutionNumericUpDown.Location = new System.Drawing.Point(3, 3);
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
            this.horizontalResolutionNumericUpDown.Size = new System.Drawing.Size(50, 20);
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
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 58);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(86, 13);
            this.label5.TabIndex = 17;
            this.label5.Text = "Rendering Mode";
            // 
            // renderingMode
            // 
            this.renderingMode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.renderingMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.renderingMode.FormattingEnabled = true;
            this.renderingMode.Location = new System.Drawing.Point(147, 54);
            this.renderingMode.Name = "renderingMode";
            this.renderingMode.Size = new System.Drawing.Size(138, 21);
            this.renderingMode.TabIndex = 19;
            // 
            // drawAnnotationsCheckBox
            // 
            this.drawAnnotationsCheckBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.drawAnnotationsCheckBox.AutoSize = true;
            this.drawAnnotationsCheckBox.Location = new System.Drawing.Point(3, 81);
            this.drawAnnotationsCheckBox.Name = "drawAnnotationsCheckBox";
            this.drawAnnotationsCheckBox.Size = new System.Drawing.Size(110, 17);
            this.drawAnnotationsCheckBox.TabIndex = 21;
            this.drawAnnotationsCheckBox.Text = "Draw Annotations";
            this.drawAnnotationsCheckBox.UseVisualStyleBackColor = true;
            // 
            // cacheResourcesCheckBox
            // 
            this.cacheResourcesCheckBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cacheResourcesCheckBox.AutoSize = true;
            this.cacheResourcesCheckBox.Location = new System.Drawing.Point(3, 104);
            this.cacheResourcesCheckBox.Name = "cacheResourcesCheckBox";
            this.cacheResourcesCheckBox.Size = new System.Drawing.Size(111, 17);
            this.cacheResourcesCheckBox.TabIndex = 22;
            this.cacheResourcesCheckBox.Text = "Cache Resources";
            this.cacheResourcesCheckBox.UseVisualStyleBackColor = true;
            // 
            // cropPageAtCropBoxCheckBox
            // 
            this.cropPageAtCropBoxCheckBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cropPageAtCropBoxCheckBox.AutoSize = true;
            this.cropPageAtCropBoxCheckBox.Location = new System.Drawing.Point(147, 104);
            this.cropPageAtCropBoxCheckBox.Name = "cropPageAtCropBoxCheckBox";
            this.cropPageAtCropBoxCheckBox.Size = new System.Drawing.Size(131, 17);
            this.cropPageAtCropBoxCheckBox.TabIndex = 23;
            this.cropPageAtCropBoxCheckBox.Text = "Crop Page at CropBox";
            this.cropPageAtCropBoxCheckBox.UseVisualStyleBackColor = true;
            // 
            // defaultDpiCheckBox
            // 
            this.defaultDpiCheckBox.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.defaultDpiCheckBox.AutoSize = true;
            this.defaultDpiCheckBox.Location = new System.Drawing.Point(213, 4);
            this.defaultDpiCheckBox.Name = "defaultDpiCheckBox";
            this.defaultDpiCheckBox.Size = new System.Drawing.Size(60, 17);
            this.defaultDpiCheckBox.TabIndex = 24;
            this.defaultDpiCheckBox.Text = "Default";
            this.defaultDpiCheckBox.UseVisualStyleBackColor = true;
            this.defaultDpiCheckBox.CheckedChanged += new System.EventHandler(this.defaultDpiCheckBox_CheckedChanged);
            // 
            // resolutionGroupBox
            // 
            this.resolutionGroupBox.AutoSize = true;
            this.resolutionGroupBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel2.SetColumnSpan(this.resolutionGroupBox, 2);
            this.resolutionGroupBox.Controls.Add(this.tableLayoutPanel1);
            this.resolutionGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.resolutionGroupBox.Location = new System.Drawing.Point(3, 3);
            this.resolutionGroupBox.Name = "resolutionGroupBox";
            this.resolutionGroupBox.Size = new System.Drawing.Size(282, 45);
            this.resolutionGroupBox.TabIndex = 25;
            this.resolutionGroupBox.TabStop = false;
            this.resolutionGroupBox.Text = "Resolution";
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(59, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(14, 13);
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
            this.useRotatePropertyCheckBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.useRotatePropertyCheckBox.AutoSize = true;
            this.useRotatePropertyCheckBox.Location = new System.Drawing.Point(147, 81);
            this.useRotatePropertyCheckBox.Name = "useRotatePropertyCheckBox";
            this.useRotatePropertyCheckBox.Size = new System.Drawing.Size(121, 17);
            this.useRotatePropertyCheckBox.TabIndex = 27;
            this.useRotatePropertyCheckBox.Text = "Use Rotate property";
            this.useRotatePropertyCheckBox.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.ColumnCount = 5;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.horizontalResolutionNumericUpDown, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.defaultDpiCheckBox, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.label1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.verticalResolutionNumericUpDown, 2, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(276, 26);
            this.tableLayoutPanel1.TabIndex = 28;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel2.AutoSize = true;
            this.tableLayoutPanel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.resolutionGroupBox, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.useRotatePropertyCheckBox, 1, 2);
            this.tableLayoutPanel2.Controls.Add(this.cropPageAtCropBoxCheckBox, 1, 3);
            this.tableLayoutPanel2.Controls.Add(this.label5, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.cacheResourcesCheckBox, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.renderingMode, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.drawAnnotationsCheckBox, 0, 2);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(9, 9);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 4;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.Size = new System.Drawing.Size(288, 124);
            this.tableLayoutPanel2.TabIndex = 28;
            // 
            // PdfRenderingSettingsForm
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSize = true;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(307, 183);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.buttonCancel);
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
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
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
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}