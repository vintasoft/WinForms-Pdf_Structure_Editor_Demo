namespace DemosCommonCode.Pdf
{
    partial class PdfGotoActionEditorForm
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
            this.positionComboBox = new System.Windows.Forms.ComboBox();
            this.pageNumberNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.okButton = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.destinationXyzPanel = new System.Windows.Forms.Panel();
            this.destinationZoomCheckBox = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.destinationYTextBox = new System.Windows.Forms.TextBox();
            this.destinationXTextBox = new System.Windows.Forms.TextBox();
            this.destinationYCheckBox = new System.Windows.Forms.CheckBox();
            this.destinationXCheckBox = new System.Windows.Forms.CheckBox();
            this.destinationZoomNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.destinationFitRectanglePanel = new System.Windows.Forms.Panel();
            this.destinationFitRectangleHeightTextBox = new System.Windows.Forms.TextBox();
            this.destinationFitRectangleWidthTextBox = new System.Windows.Forms.TextBox();
            this.destinationFitRectangleYTextBox = new System.Windows.Forms.TextBox();
            this.destinationFitRectangleXTextBox = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.pageNumberNumericUpDown)).BeginInit();
            this.destinationXyzPanel.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.destinationZoomNumericUpDown)).BeginInit();
            this.destinationFitRectanglePanel.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // positionComboBox
            // 
            this.positionComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.positionComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.positionComboBox.FormattingEnabled = true;
            this.positionComboBox.Location = new System.Drawing.Point(12, 69);
            this.positionComboBox.Name = "positionComboBox";
            this.positionComboBox.Size = new System.Drawing.Size(115, 21);
            this.positionComboBox.TabIndex = 7;
            this.positionComboBox.SelectedIndexChanged += new System.EventHandler(this.positionComboBox_SelectedIndexChanged);
            // 
            // pageNumberNumericUpDown
            // 
            this.pageNumberNumericUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pageNumberNumericUpDown.Location = new System.Drawing.Point(12, 30);
            this.pageNumberNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.pageNumberNumericUpDown.Name = "pageNumberNumericUpDown";
            this.pageNumberNumericUpDown.Size = new System.Drawing.Size(115, 20);
            this.pageNumberNumericUpDown.TabIndex = 6;
            this.pageNumberNumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Position";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Page Number";
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.Location = new System.Drawing.Point(92, 251);
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
            this.buttonCancel.Location = new System.Drawing.Point(173, 251);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 9;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // destinationXyzPanel
            // 
            this.destinationXyzPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.destinationXyzPanel.Controls.Add(this.destinationZoomCheckBox);
            this.destinationXyzPanel.Controls.Add(this.destinationZoomNumericUpDown);
            this.destinationXyzPanel.Controls.Add(this.groupBox1);
            this.destinationXyzPanel.Location = new System.Drawing.Point(12, 96);
            this.destinationXyzPanel.Name = "destinationXyzPanel";
            this.destinationXyzPanel.Size = new System.Drawing.Size(233, 137);
            this.destinationXyzPanel.TabIndex = 10;
            this.destinationXyzPanel.Visible = false;
            // 
            // destinationZoomCheckBox
            // 
            this.destinationZoomCheckBox.AutoSize = true;
            this.destinationZoomCheckBox.Checked = true;
            this.destinationZoomCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.destinationZoomCheckBox.Location = new System.Drawing.Point(12, 84);
            this.destinationZoomCheckBox.Name = "destinationZoomCheckBox";
            this.destinationZoomCheckBox.Size = new System.Drawing.Size(100, 17);
            this.destinationZoomCheckBox.TabIndex = 7;
            this.destinationZoomCheckBox.Text = "Zoom Factor, %";
            this.destinationZoomCheckBox.UseVisualStyleBackColor = true;
            this.destinationZoomCheckBox.CheckedChanged += new System.EventHandler(this.destinationZoomCheckBox_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.destinationYTextBox);
            this.groupBox1.Controls.Add(this.destinationXTextBox);
            this.groupBox1.Controls.Add(this.destinationYCheckBox);
            this.groupBox1.Controls.Add(this.destinationXCheckBox);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(227, 74);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Location";
            // 
            // destinationYTextBox
            // 
            this.destinationYTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.destinationYTextBox.Location = new System.Drawing.Point(72, 44);
            this.destinationYTextBox.Name = "destinationYTextBox";
            this.destinationYTextBox.Size = new System.Drawing.Size(149, 20);
            this.destinationYTextBox.TabIndex = 7;
            // 
            // destinationXTextBox
            // 
            this.destinationXTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.destinationXTextBox.Location = new System.Drawing.Point(71, 18);
            this.destinationXTextBox.Name = "destinationXTextBox";
            this.destinationXTextBox.Size = new System.Drawing.Size(150, 20);
            this.destinationXTextBox.TabIndex = 6;
            // 
            // destinationYCheckBox
            // 
            this.destinationYCheckBox.AutoSize = true;
            this.destinationYCheckBox.Checked = true;
            this.destinationYCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.destinationYCheckBox.Location = new System.Drawing.Point(9, 46);
            this.destinationYCheckBox.Name = "destinationYCheckBox";
            this.destinationYCheckBox.Size = new System.Drawing.Size(33, 17);
            this.destinationYCheckBox.TabIndex = 5;
            this.destinationYCheckBox.Text = "Y";
            this.destinationYCheckBox.UseVisualStyleBackColor = true;
            this.destinationYCheckBox.CheckedChanged += new System.EventHandler(this.destinationYCheckBox_CheckedChanged);
            // 
            // destinationXCheckBox
            // 
            this.destinationXCheckBox.AutoSize = true;
            this.destinationXCheckBox.Checked = true;
            this.destinationXCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.destinationXCheckBox.Location = new System.Drawing.Point(9, 20);
            this.destinationXCheckBox.Name = "destinationXCheckBox";
            this.destinationXCheckBox.Size = new System.Drawing.Size(33, 17);
            this.destinationXCheckBox.TabIndex = 4;
            this.destinationXCheckBox.Text = "X";
            this.destinationXCheckBox.UseVisualStyleBackColor = true;
            this.destinationXCheckBox.CheckedChanged += new System.EventHandler(this.destinationXCheckBox_CheckedChanged);
            // 
            // destinationZoomNumericUpDown
            // 
            this.destinationZoomNumericUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.destinationZoomNumericUpDown.Location = new System.Drawing.Point(12, 106);
            this.destinationZoomNumericUpDown.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.destinationZoomNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.destinationZoomNumericUpDown.Name = "destinationZoomNumericUpDown";
            this.destinationZoomNumericUpDown.Size = new System.Drawing.Size(106, 20);
            this.destinationZoomNumericUpDown.TabIndex = 5;
            this.destinationZoomNumericUpDown.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // destinationFitRectanglePanel
            // 
            this.destinationFitRectanglePanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.destinationFitRectanglePanel.Controls.Add(this.tableLayoutPanel1);
            this.destinationFitRectanglePanel.Location = new System.Drawing.Point(12, 96);
            this.destinationFitRectanglePanel.Name = "destinationFitRectanglePanel";
            this.destinationFitRectanglePanel.Size = new System.Drawing.Size(233, 137);
            this.destinationFitRectanglePanel.TabIndex = 11;
            this.destinationFitRectanglePanel.Visible = false;
            // 
            // destinationFitRectangleHeightTextBox
            // 
            this.destinationFitRectangleHeightTextBox.Location = new System.Drawing.Point(47, 81);
            this.destinationFitRectangleHeightTextBox.Name = "destinationFitRectangleHeightTextBox";
            this.destinationFitRectangleHeightTextBox.Size = new System.Drawing.Size(183, 20);
            this.destinationFitRectangleHeightTextBox.TabIndex = 11;
            // 
            // destinationFitRectangleWidthTextBox
            // 
            this.destinationFitRectangleWidthTextBox.Location = new System.Drawing.Point(47, 55);
            this.destinationFitRectangleWidthTextBox.Name = "destinationFitRectangleWidthTextBox";
            this.destinationFitRectangleWidthTextBox.Size = new System.Drawing.Size(183, 20);
            this.destinationFitRectangleWidthTextBox.TabIndex = 10;
            // 
            // destinationFitRectangleYTextBox
            // 
            this.destinationFitRectangleYTextBox.Location = new System.Drawing.Point(47, 29);
            this.destinationFitRectangleYTextBox.Name = "destinationFitRectangleYTextBox";
            this.destinationFitRectangleYTextBox.Size = new System.Drawing.Size(183, 20);
            this.destinationFitRectangleYTextBox.TabIndex = 9;
            // 
            // destinationFitRectangleXTextBox
            // 
            this.destinationFitRectangleXTextBox.Location = new System.Drawing.Point(47, 3);
            this.destinationFitRectangleXTextBox.Name = "destinationFitRectangleXTextBox";
            this.destinationFitRectangleXTextBox.Size = new System.Drawing.Size(183, 20);
            this.destinationFitRectangleXTextBox.TabIndex = 8;
            // 
            // label9
            // 
            this.label9.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(3, 84);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(38, 13);
            this.label9.TabIndex = 3;
            this.label9.Text = "Height";
            // 
            // label8
            // 
            this.label8.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(3, 58);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(35, 13);
            this.label8.TabIndex = 2;
            this.label8.Text = "Width";
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 32);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(14, 13);
            this.label7.TabIndex = 1;
            this.label7.Text = "Y";
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 6);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(14, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "X";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.label6, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.destinationFitRectangleHeightTextBox, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.destinationFitRectangleXTextBox, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.destinationFitRectangleWidthTextBox, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.label7, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.destinationFitRectangleYTextBox, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label8, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label9, 0, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(233, 137);
            this.tableLayoutPanel1.TabIndex = 12;
            // 
            // PdfGotoActionEditorForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(260, 279);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.positionComboBox);
            this.Controls.Add(this.pageNumberNumericUpDown);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.destinationXyzPanel);
            this.Controls.Add(this.destinationFitRectanglePanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PdfGotoActionEditorForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Goto Action Editor";
            ((System.ComponentModel.ISupportInitialize)(this.pageNumberNumericUpDown)).EndInit();
            this.destinationXyzPanel.ResumeLayout(false);
            this.destinationXyzPanel.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.destinationZoomNumericUpDown)).EndInit();
            this.destinationFitRectanglePanel.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox positionComboBox;
        private System.Windows.Forms.NumericUpDown pageNumberNumericUpDown;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Panel destinationXyzPanel;
        private System.Windows.Forms.NumericUpDown destinationZoomNumericUpDown;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel destinationFitRectanglePanel;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox destinationZoomCheckBox;
        private System.Windows.Forms.CheckBox destinationYCheckBox;
        private System.Windows.Forms.CheckBox destinationXCheckBox;
        private System.Windows.Forms.TextBox destinationYTextBox;
        private System.Windows.Forms.TextBox destinationXTextBox;
        private System.Windows.Forms.TextBox destinationFitRectangleHeightTextBox;
        private System.Windows.Forms.TextBox destinationFitRectangleWidthTextBox;
        private System.Windows.Forms.TextBox destinationFitRectangleYTextBox;
        private System.Windows.Forms.TextBox destinationFitRectangleXTextBox;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}