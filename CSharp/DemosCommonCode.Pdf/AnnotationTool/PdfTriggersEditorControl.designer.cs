namespace DemosCommonCode.Pdf
{
    partial class PdfTriggersEditorControl
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.triggersListBox = new System.Windows.Forms.ListBox();
            this.triggerActionsGroupBox = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.pdfActionEditorControl = new DemosCommonCode.Pdf.PdfActionEditorControl();
            this.groupBox1.SuspendLayout();
            this.triggerActionsGroupBox.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.triggersListBox);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(312, 133);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Triggers";
            // 
            // triggersListBox
            // 
            this.triggersListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.triggersListBox.FormattingEnabled = true;
            this.triggersListBox.Location = new System.Drawing.Point(3, 16);
            this.triggersListBox.Name = "triggersListBox";
            this.triggersListBox.Size = new System.Drawing.Size(306, 108);
            this.triggersListBox.TabIndex = 0;
            this.triggersListBox.SelectedIndexChanged += new System.EventHandler(this.triggersListBox_SelectedIndexChanged);
            // 
            // triggerActionsGroupBox
            // 
            this.triggerActionsGroupBox.Controls.Add(this.pdfActionEditorControl);
            this.triggerActionsGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.triggerActionsGroupBox.Location = new System.Drawing.Point(3, 142);
            this.triggerActionsGroupBox.Name = "triggerActionsGroupBox";
            this.triggerActionsGroupBox.Size = new System.Drawing.Size(312, 203);
            this.triggerActionsGroupBox.TabIndex = 0;
            this.triggerActionsGroupBox.TabStop = false;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.triggerActionsGroupBox, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(318, 348);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // pdfActionEditorControl
            // 
            this.pdfActionEditorControl.Action = null;
            this.pdfActionEditorControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pdfActionEditorControl.Document = null;
            this.pdfActionEditorControl.ImageCollection = null;
            this.pdfActionEditorControl.Location = new System.Drawing.Point(3, 16);
            this.pdfActionEditorControl.MinimumSize = new System.Drawing.Size(165, 138);
            this.pdfActionEditorControl.Name = "pdfActionEditorControl";
            this.pdfActionEditorControl.Size = new System.Drawing.Size(306, 184);
            this.pdfActionEditorControl.TabIndex = 0;
            this.pdfActionEditorControl.ActionChanged += new System.EventHandler(this.pdfActionEditorControl_ActionChanged);
            // 
            // PdfTriggersEditorControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.MinimumSize = new System.Drawing.Size(200, 275);
            this.Name = "PdfTriggersEditorControl";
            this.Size = new System.Drawing.Size(318, 348);
            this.groupBox1.ResumeLayout(false);
            this.triggerActionsGroupBox.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListBox triggersListBox;
        private System.Windows.Forms.GroupBox triggerActionsGroupBox;
        private global::DemosCommonCode.Pdf.PdfActionEditorControl pdfActionEditorControl;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}