namespace DemosCommonCode.Pdf
{
    partial class PdfTreeViewerForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.closeButton = new System.Windows.Forms.Button();
            this.pdfTreeNodePropertyGrid = new System.Windows.Forms.PropertyGrid();
            this.propertyGridGroupBox = new System.Windows.Forms.GroupBox();
            this.pdfTreeView = new DemosCommonCode.Pdf.PdfTreeView();
            this.propertyGridGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label1.Location = new System.Drawing.Point(9, 405);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(455, 26);
            this.label1.TabIndex = 8;
            this.label1.Text = "The changes in the tree of PDF document are not controlled in any way. \r\nA PDF do" +
                "cument can be corrupted if the mandatory nodes of PDF tree are changed or delete" +
                "d.";
            // 
            // closeButton
            // 
            this.closeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.closeButton.Location = new System.Drawing.Point(507, 405);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(75, 23);
            this.closeButton.TabIndex = 7;
            this.closeButton.Text = "Close";
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // pdfTreeNodePropertyGrid
            // 
            this.pdfTreeNodePropertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pdfTreeNodePropertyGrid.Location = new System.Drawing.Point(3, 16);
            this.pdfTreeNodePropertyGrid.Name = "pdfTreeNodePropertyGrid";
            this.pdfTreeNodePropertyGrid.Size = new System.Drawing.Size(290, 368);
            this.pdfTreeNodePropertyGrid.TabIndex = 6;
            // 
            // propertyGridGroupBox
            // 
            this.propertyGridGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.propertyGridGroupBox.Controls.Add(this.pdfTreeNodePropertyGrid);
            this.propertyGridGroupBox.Location = new System.Drawing.Point(286, 12);
            this.propertyGridGroupBox.Name = "propertyGridGroupBox";
            this.propertyGridGroupBox.Size = new System.Drawing.Size(296, 387);
            this.propertyGridGroupBox.TabIndex = 10;
            this.propertyGridGroupBox.TabStop = false;
            // 
            // pdfTreeView
            // 
            this.pdfTreeView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pdfTreeView.Location = new System.Drawing.Point(12, 12);
            this.pdfTreeView.Name = "pdfTreeView";
            this.pdfTreeView.RootObject = null;
            this.pdfTreeView.Size = new System.Drawing.Size(268, 387);
            this.pdfTreeView.TabIndex = 9;
            this.pdfTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.pdfTreeView_AfterSelect);
            // 
            // PdfTreeViewer
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(591, 444);
            this.Controls.Add(this.propertyGridGroupBox);
            this.Controls.Add(this.pdfTreeView);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.closeButton);
            this.Name = "PdfTreeViewer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PDF Tree Viewer";
            this.propertyGridGroupBox.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.PropertyGrid pdfTreeNodePropertyGrid;
        private PdfTreeView pdfTreeView;
        private System.Windows.Forms.GroupBox propertyGridGroupBox;
    }
}