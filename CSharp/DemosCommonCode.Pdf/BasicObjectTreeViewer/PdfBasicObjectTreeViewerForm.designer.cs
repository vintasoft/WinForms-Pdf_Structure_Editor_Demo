namespace DemosCommonCode.Pdf
{
    partial class PdfBasicObjectTreeViewerForm
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
            this.basicObjectPropertyGrid = new System.Windows.Forms.PropertyGrid();
            this.closeButton = new System.Windows.Forms.Button();
            this.propertiesGroupBox = new System.Windows.Forms.GroupBox();
            this.basicObjectTreeView = new DemosCommonCode.Pdf.PdfBasicObjectTreeView();
            this.label1 = new System.Windows.Forms.Label();
            this.propertiesGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // basicObjectPropertyGrid
            // 
            this.basicObjectPropertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.basicObjectPropertyGrid.Location = new System.Drawing.Point(3, 16);
            this.basicObjectPropertyGrid.Name = "basicObjectPropertyGrid";
            this.basicObjectPropertyGrid.Size = new System.Drawing.Size(290, 375);
            this.basicObjectPropertyGrid.TabIndex = 1;
            this.basicObjectPropertyGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.basicObjectPropertyGrid_PropertyValueChanged);
            // 
            // closeButton
            // 
            this.closeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.closeButton.Location = new System.Drawing.Point(504, 405);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(75, 23);
            this.closeButton.TabIndex = 2;
            this.closeButton.Text = "Close";
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // propertiesGroupBox
            // 
            this.propertiesGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.propertiesGroupBox.Controls.Add(this.basicObjectPropertyGrid);
            this.propertiesGroupBox.Location = new System.Drawing.Point(283, 5);
            this.propertiesGroupBox.Name = "propertiesGroupBox";
            this.propertiesGroupBox.Size = new System.Drawing.Size(296, 394);
            this.propertiesGroupBox.TabIndex = 3;
            this.propertiesGroupBox.TabStop = false;
            // 
            // basicObjectTreeView
            // 
            this.basicObjectTreeView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.basicObjectTreeView.Location = new System.Drawing.Point(9, 12);
            this.basicObjectTreeView.Name = "basicObjectTreeView";
            this.basicObjectTreeView.RootObject = null;
            this.basicObjectTreeView.Size = new System.Drawing.Size(268, 387);
            this.basicObjectTreeView.TabIndex = 0;
            this.basicObjectTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.basicObjectTreeView_AfterSelect);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label1.Location = new System.Drawing.Point(6, 405);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(455, 26);
            this.label1.TabIndex = 4;
            this.label1.Text = "The changes in the tree of basic objects of PDF document are not controlled in an" +
                "y way. \r\nA PDF document can be corrupted if the mandatory nodes of PDF tree are " +
                "changed or deleted.";
            // 
            // PdfBasicObjectTreeViewerForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(587, 440);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.propertiesGroupBox);
            this.Controls.Add(this.basicObjectTreeView);
            this.Name = "PdfBasicObjectTreeViewerForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Basic Object Tree Viewer (Low-Level PDF Tree)";
            this.propertiesGroupBox.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private PdfBasicObjectTreeView basicObjectTreeView;
        private System.Windows.Forms.PropertyGrid basicObjectPropertyGrid;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.GroupBox propertiesGroupBox;
        private System.Windows.Forms.Label label1;
    }
}