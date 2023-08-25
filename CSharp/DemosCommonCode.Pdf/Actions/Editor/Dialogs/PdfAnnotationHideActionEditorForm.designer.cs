namespace DemosCommonCode.Pdf
{
    partial class PdfAnnotationHideActionEditorForm
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
            this.components = new System.ComponentModel.Container();
            this.okButton = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.pdfPageAnnotationsControl = new DemosCommonCode.Pdf.PdfPageAnnotationsControl();
            this.annotationsListBox = new System.Windows.Forms.ListBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.removeAllButton = new System.Windows.Forms.Button();
            this.removeButton = new System.Windows.Forms.Button();
            this.addButton = new System.Windows.Forms.Button();
            this.hideCheckBox = new System.Windows.Forms.CheckBox();
            this.pagesComboBox = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(476, 344);
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
            this.buttonCancel.Location = new System.Drawing.Point(557, 344);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 1;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel1.Controls.Add(this.pdfPageAnnotationsControl, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.annotationsListBox, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 1, 0);
            this.tableLayoutPanel1.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 62);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(620, 275);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // pdfPageAnnotationsControl
            // 
            this.pdfPageAnnotationsControl.AnnotationList = null;
            this.pdfPageAnnotationsControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pdfPageAnnotationsControl.FullRowSelect = true;
            this.pdfPageAnnotationsControl.GridLines = true;
            this.pdfPageAnnotationsControl.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.pdfPageAnnotationsControl.HideSelection = false;
            this.pdfPageAnnotationsControl.Location = new System.Drawing.Point(3, 3);
            this.pdfPageAnnotationsControl.MultiSelect = false;
            this.pdfPageAnnotationsControl.Name = "pdfPageAnnotationsControl";
            this.pdfPageAnnotationsControl.SelectedAnnotation = null;
            this.pdfPageAnnotationsControl.Size = new System.Drawing.Size(312, 269);
            this.pdfPageAnnotationsControl.TabIndex = 0;
            this.pdfPageAnnotationsControl.UseCompatibleStateImageBehavior = false;
            this.pdfPageAnnotationsControl.View = System.Windows.Forms.View.Details;
            this.pdfPageAnnotationsControl.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.pdfPageAnnotationsControl_MouseDoubleClick);
            this.pdfPageAnnotationsControl.SelectedIndexChanged += new System.EventHandler(this.pdfPageAnnotationsControl_SelectedIndexChanged);
            // 
            // annotationsListBox
            // 
            this.annotationsListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.annotationsListBox.FormattingEnabled = true;
            this.annotationsListBox.Location = new System.Drawing.Point(411, 3);
            this.annotationsListBox.Name = "annotationsListBox";
            this.annotationsListBox.Size = new System.Drawing.Size(206, 264);
            this.annotationsListBox.TabIndex = 1;
            this.annotationsListBox.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.annotationsListBox_MouseDoubleClick);
            this.annotationsListBox.SelectedIndexChanged += new System.EventHandler(this.annotationsListBox_SelectedIndexChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.removeAllButton);
            this.panel1.Controls.Add(this.removeButton);
            this.panel1.Controls.Add(this.addButton);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(321, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(84, 269);
            this.panel1.TabIndex = 2;
            // 
            // removeAllButton
            // 
            this.removeAllButton.Enabled = false;
            this.removeAllButton.Location = new System.Drawing.Point(5, 241);
            this.removeAllButton.Name = "removeAllButton";
            this.removeAllButton.Size = new System.Drawing.Size(75, 23);
            this.removeAllButton.TabIndex = 2;
            this.removeAllButton.Text = "Remove All";
            this.removeAllButton.UseVisualStyleBackColor = true;
            this.removeAllButton.Click += new System.EventHandler(this.removeAllButton_Click);
            // 
            // removeButton
            // 
            this.removeButton.Enabled = false;
            this.removeButton.Location = new System.Drawing.Point(5, 45);
            this.removeButton.Name = "removeButton";
            this.removeButton.Size = new System.Drawing.Size(75, 23);
            this.removeButton.TabIndex = 1;
            this.removeButton.Text = "Remove";
            this.removeButton.UseVisualStyleBackColor = true;
            this.removeButton.Click += new System.EventHandler(this.removeButton_Click);
            // 
            // addButton
            // 
            this.addButton.Enabled = false;
            this.addButton.Location = new System.Drawing.Point(5, 16);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(75, 23);
            this.addButton.TabIndex = 0;
            this.addButton.Text = ">>";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // hideCheckBox
            // 
            this.hideCheckBox.AutoSize = true;
            this.hideCheckBox.Location = new System.Drawing.Point(12, 12);
            this.hideCheckBox.Name = "hideCheckBox";
            this.hideCheckBox.Size = new System.Drawing.Size(106, 17);
            this.hideCheckBox.TabIndex = 3;
            this.hideCheckBox.Text = "Hide annotations";
            this.hideCheckBox.UseVisualStyleBackColor = true;
            // 
            // pagesComboBox
            // 
            this.pagesComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.pagesComboBox.FormattingEnabled = true;
            this.pagesComboBox.Location = new System.Drawing.Point(12, 35);
            this.pagesComboBox.Name = "pagesComboBox";
            this.pagesComboBox.Size = new System.Drawing.Size(315, 21);
            this.pagesComboBox.TabIndex = 4;
            this.pagesComboBox.SelectedIndexChanged += new System.EventHandler(this.pagesComboBox_SelectedIndexChanged);
            // 
            // PdfAnnotationHideActionEditorForm
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(644, 376);
            this.Controls.Add(this.pagesComboBox);
            this.Controls.Add(this.hideCheckBox);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.okButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PdfAnnotationHideActionEditorForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Annotation Hide Action Editor";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private PdfPageAnnotationsControl pdfPageAnnotationsControl;
        private System.Windows.Forms.CheckBox hideCheckBox;
        private System.Windows.Forms.ComboBox pagesComboBox;
        private System.Windows.Forms.ListBox annotationsListBox;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button removeAllButton;
        private System.Windows.Forms.Button removeButton;
        private System.Windows.Forms.Button addButton;
    }
}