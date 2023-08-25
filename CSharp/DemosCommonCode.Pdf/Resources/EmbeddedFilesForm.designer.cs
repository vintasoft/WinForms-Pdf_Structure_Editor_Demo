namespace DemosCommonCode.Pdf
{
    partial class EmbeddedFilesForm
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
            this.controlButtonsPanel = new System.Windows.Forms.Panel();
            this.saveAsButton = new System.Windows.Forms.Button();
            this.removeButton = new System.Windows.Forms.Button();
            this.addButton = new System.Windows.Forms.Button();
            this.okButton = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.embeddedFilesDataGridView = new System.Windows.Forms.DataGridView();
            this.filenameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sizeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.compressedSizeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel3 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel6 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.descriptionTextBox = new System.Windows.Forms.TextBox();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.createDateLabel = new System.Windows.Forms.Label();
            this.modifyDateLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.compressionComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.encodeFilesImmediatelyCheckBox = new System.Windows.Forms.CheckBox();
            this.controlButtonsPanel.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.embeddedFilesDataGridView)).BeginInit();
            this.panel3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel6.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // controlButtonsPanel
            // 
            this.controlButtonsPanel.Controls.Add(this.encodeFilesImmediatelyCheckBox);
            this.controlButtonsPanel.Controls.Add(this.saveAsButton);
            this.controlButtonsPanel.Controls.Add(this.removeButton);
            this.controlButtonsPanel.Controls.Add(this.addButton);
            this.controlButtonsPanel.Controls.Add(this.okButton);
            this.controlButtonsPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.controlButtonsPanel.Location = new System.Drawing.Point(0, 235);
            this.controlButtonsPanel.Name = "controlButtonsPanel";
            this.controlButtonsPanel.Size = new System.Drawing.Size(748, 47);
            this.controlButtonsPanel.TabIndex = 0;
            // 
            // saveAsButton
            // 
            this.saveAsButton.Location = new System.Drawing.Point(12, 12);
            this.saveAsButton.Name = "saveAsButton";
            this.saveAsButton.Size = new System.Drawing.Size(75, 23);
            this.saveAsButton.TabIndex = 3;
            this.saveAsButton.Text = "Save As...";
            this.saveAsButton.UseVisualStyleBackColor = true;
            this.saveAsButton.Click += new System.EventHandler(this.saveAsButton_Click);
            // 
            // removeButton
            // 
            this.removeButton.Location = new System.Drawing.Point(188, 12);
            this.removeButton.Name = "removeButton";
            this.removeButton.Size = new System.Drawing.Size(75, 23);
            this.removeButton.TabIndex = 2;
            this.removeButton.Text = "Remove";
            this.removeButton.UseVisualStyleBackColor = true;
            this.removeButton.Click += new System.EventHandler(this.removeButton_Click);
            // 
            // addButton
            // 
            this.addButton.Location = new System.Drawing.Point(107, 12);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(75, 23);
            this.addButton.TabIndex = 1;
            this.addButton.Text = "Add...";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.okButton.Location = new System.Drawing.Point(661, 12);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 0;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(3);
            this.panel2.Size = new System.Drawing.Size(748, 235);
            this.panel2.TabIndex = 1;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.embeddedFilesDataGridView);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(3, 3);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(524, 229);
            this.panel4.TabIndex = 1;
            // 
            // embeddedFilesDataGridView
            // 
            this.embeddedFilesDataGridView.AllowUserToAddRows = false;
            this.embeddedFilesDataGridView.AllowUserToDeleteRows = false;
            this.embeddedFilesDataGridView.AllowUserToResizeRows = false;
            this.embeddedFilesDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.embeddedFilesDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.filenameColumn,
            this.sizeColumn,
            this.compressedSizeColumn});
            this.embeddedFilesDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.embeddedFilesDataGridView.Location = new System.Drawing.Point(0, 0);
            this.embeddedFilesDataGridView.MultiSelect = false;
            this.embeddedFilesDataGridView.Name = "embeddedFilesDataGridView";
            this.embeddedFilesDataGridView.ReadOnly = true;
            this.embeddedFilesDataGridView.RowHeadersVisible = false;
            this.embeddedFilesDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.embeddedFilesDataGridView.ShowEditingIcon = false;
            this.embeddedFilesDataGridView.Size = new System.Drawing.Size(524, 229);
            this.embeddedFilesDataGridView.TabIndex = 1;
            this.embeddedFilesDataGridView.SelectionChanged += new System.EventHandler(this.embeddedFilesDataGridView_SelectionChanged);
            // 
            // filenameColumn
            // 
            this.filenameColumn.HeaderText = "Filename";
            this.filenameColumn.Name = "filenameColumn";
            this.filenameColumn.ReadOnly = true;
            this.filenameColumn.Width = 330;
            // 
            // sizeColumn
            // 
            this.sizeColumn.HeaderText = "Size";
            this.sizeColumn.Name = "sizeColumn";
            this.sizeColumn.ReadOnly = true;
            this.sizeColumn.Width = 70;
            // 
            // compressedSizeColumn
            // 
            this.compressedSizeColumn.HeaderText = "Compressed Size";
            this.compressedSizeColumn.Name = "compressedSizeColumn";
            this.compressedSizeColumn.ReadOnly = true;
            this.compressedSizeColumn.Width = 120;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.groupBox1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel3.Location = new System.Drawing.Point(527, 3);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding(3);
            this.panel3.Size = new System.Drawing.Size(218, 229);
            this.panel3.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.panel6);
            this.groupBox1.Controls.Add(this.panel5);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(212, 223);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Properties";
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.groupBox2);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel6.Location = new System.Drawing.Point(3, 96);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(206, 124);
            this.panel6.TabIndex = 1;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.descriptionTextBox);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(206, 124);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Description";
            // 
            // descriptionTextBox
            // 
            this.descriptionTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.descriptionTextBox.Location = new System.Drawing.Point(3, 16);
            this.descriptionTextBox.Multiline = true;
            this.descriptionTextBox.Name = "descriptionTextBox";
            this.descriptionTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.descriptionTextBox.Size = new System.Drawing.Size(200, 105);
            this.descriptionTextBox.TabIndex = 0;
            this.descriptionTextBox.TextChanged += new System.EventHandler(this.descriptionTextBox_TextChanged);
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.label4);
            this.panel5.Controls.Add(this.createDateLabel);
            this.panel5.Controls.Add(this.modifyDateLabel);
            this.panel5.Controls.Add(this.label2);
            this.panel5.Controls.Add(this.compressionComboBox);
            this.panel5.Controls.Add(this.label1);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(3, 16);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(206, 80);
            this.panel5.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 52);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Modify Date";
            // 
            // createDateLabel
            // 
            this.createDateLabel.AutoSize = true;
            this.createDateLabel.Location = new System.Drawing.Point(73, 30);
            this.createDateLabel.Name = "createDateLabel";
            this.createDateLabel.Size = new System.Drawing.Size(106, 13);
            this.createDateLabel.TabIndex = 9;
            this.createDateLabel.Text = "00.00.0000 00:00:00";
            // 
            // modifyDateLabel
            // 
            this.modifyDateLabel.AutoSize = true;
            this.modifyDateLabel.Location = new System.Drawing.Point(73, 52);
            this.modifyDateLabel.Name = "modifyDateLabel";
            this.modifyDateLabel.Size = new System.Drawing.Size(106, 13);
            this.modifyDateLabel.TabIndex = 8;
            this.modifyDateLabel.Text = "00.00.0000 00:00:00";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Create Date";
            // 
            // compressionComboBox
            // 
            this.compressionComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.compressionComboBox.FormattingEnabled = true;
            this.compressionComboBox.Location = new System.Drawing.Point(76, 0);
            this.compressionComboBox.Name = "compressionComboBox";
            this.compressionComboBox.Size = new System.Drawing.Size(130, 21);
            this.compressionComboBox.TabIndex = 6;
            this.compressionComboBox.SelectedIndexChanged += new System.EventHandler(this.compressionComboBox_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Compression";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Multiselect = true;
            // 
            // encodeFilesImmediatelyCheckBox
            // 
            this.encodeFilesImmediatelyCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.encodeFilesImmediatelyCheckBox.AutoSize = true;
            this.encodeFilesImmediatelyCheckBox.Checked = true;
            this.encodeFilesImmediatelyCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.encodeFilesImmediatelyCheckBox.Location = new System.Drawing.Point(510, 16);
            this.encodeFilesImmediatelyCheckBox.Name = "encodeFilesImmediatelyCheckBox";
            this.encodeFilesImmediatelyCheckBox.Size = new System.Drawing.Size(145, 17);
            this.encodeFilesImmediatelyCheckBox.TabIndex = 4;
            this.encodeFilesImmediatelyCheckBox.Text = "Encode Files Immediately";
            this.encodeFilesImmediatelyCheckBox.UseVisualStyleBackColor = true;
            // 
            // EmbeddedFilesForm
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.okButton;
            this.ClientSize = new System.Drawing.Size(748, 282);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.controlButtonsPanel);
            this.Name = "EmbeddedFilesForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Embedded Files";
            this.controlButtonsPanel.ResumeLayout(false);
            this.controlButtonsPanel.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.embeddedFilesDataGridView)).EndInit();
            this.panel3.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel controlButtonsPanel;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button removeButton;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button saveAsButton;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.DataGridView embeddedFilesDataGridView;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label modifyDateLabel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox compressionComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox descriptionTextBox;
        private System.Windows.Forms.Label createDateLabel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.DataGridViewTextBoxColumn filenameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn sizeColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn compressedSizeColumn;
        private System.Windows.Forms.CheckBox encodeFilesImmediatelyCheckBox;
    }
}