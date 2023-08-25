namespace DemosCommonCode.Pdf
{
    partial class PdfResourceViewerControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

         #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.resourceImageViewer = new Vintasoft.Imaging.UI.ImageViewer();
            this.resourceTextBox = new System.Windows.Forms.TextBox();
            this.sizeModeComboBox = new System.Windows.Forms.ComboBox();
            this.textBoxPanel = new System.Windows.Forms.Panel();
            this.textBoxResourceNameLabel = new System.Windows.Forms.Label();
            this.imageViewerPanel = new System.Windows.Forms.Panel();
            this.imageViewerResourceNameLabel = new System.Windows.Forms.Label();
            this.textBoxPanel.SuspendLayout();
            this.imageViewerPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // resourceImageViewer
            // 
            this.resourceImageViewer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.resourceImageViewer.AutoScroll = true;
            this.resourceImageViewer.BackColor = System.Drawing.Color.White;
            this.resourceImageViewer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.resourceImageViewer.CenterImage = true;
            this.resourceImageViewer.FocusPointAnchor = Vintasoft.Imaging.AnchorType.None;
            this.resourceImageViewer.Location = new System.Drawing.Point(3, 27);
            this.resourceImageViewer.Margin = new System.Windows.Forms.Padding(0);
            this.resourceImageViewer.Name = "resourceImageViewer";
            this.resourceImageViewer.ShortcutCopy = System.Windows.Forms.Shortcut.None;
            this.resourceImageViewer.ShortcutCut = System.Windows.Forms.Shortcut.None;
            this.resourceImageViewer.ShortcutDelete = System.Windows.Forms.Shortcut.None;
            this.resourceImageViewer.ShortcutInsert = System.Windows.Forms.Shortcut.None;
            this.resourceImageViewer.ShortcutSelectAll = System.Windows.Forms.Shortcut.None;
            this.resourceImageViewer.Size = new System.Drawing.Size(224, 45);
            this.resourceImageViewer.TabIndex = 0;
            this.resourceImageViewer.Text = "imageViewer1";
            // 
            // resourceTextBox
            // 
            this.resourceTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.resourceTextBox.BackColor = System.Drawing.Color.White;
            this.resourceTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.resourceTextBox.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.resourceTextBox.Location = new System.Drawing.Point(3, 27);
            this.resourceTextBox.MaxLength = 16777216;
            this.resourceTextBox.Multiline = true;
            this.resourceTextBox.Name = "resourceTextBox";
            this.resourceTextBox.ReadOnly = true;
            this.resourceTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.resourceTextBox.Size = new System.Drawing.Size(224, 43);
            this.resourceTextBox.TabIndex = 1;
            this.resourceTextBox.Text = "Data";
            // 
            // sizeModeComboBox
            // 
            this.sizeModeComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.sizeModeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.sizeModeComboBox.FormattingEnabled = true;
            this.sizeModeComboBox.Location = new System.Drawing.Point(108, 3);
            this.sizeModeComboBox.Name = "sizeModeComboBox";
            this.sizeModeComboBox.Size = new System.Drawing.Size(119, 21);
            this.sizeModeComboBox.TabIndex = 2;
            this.sizeModeComboBox.SelectedIndexChanged += new System.EventHandler(this.sizeModeComboBox_SelectedIndexChanged);
            // 
            // textBoxPanel
            // 
            this.textBoxPanel.Controls.Add(this.textBoxResourceNameLabel);
            this.textBoxPanel.Controls.Add(this.resourceTextBox);
            this.textBoxPanel.Location = new System.Drawing.Point(3, 81);
            this.textBoxPanel.Name = "textBoxPanel";
            this.textBoxPanel.Size = new System.Drawing.Size(230, 70);
            this.textBoxPanel.TabIndex = 3;
            // 
            // textBoxResourceNameLabel
            // 
            this.textBoxResourceNameLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxResourceNameLabel.AutoEllipsis = true;
            this.textBoxResourceNameLabel.Location = new System.Drawing.Point(3, 6);
            this.textBoxResourceNameLabel.Name = "textBoxResourceNameLabel";
            this.textBoxResourceNameLabel.Size = new System.Drawing.Size(224, 21);
            this.textBoxResourceNameLabel.TabIndex = 2;
            this.textBoxResourceNameLabel.Text = "{Resource Name}";
            // 
            // imageViewerPanel
            // 
            this.imageViewerPanel.Controls.Add(this.imageViewerResourceNameLabel);
            this.imageViewerPanel.Controls.Add(this.sizeModeComboBox);
            this.imageViewerPanel.Controls.Add(this.resourceImageViewer);
            this.imageViewerPanel.Location = new System.Drawing.Point(3, 3);
            this.imageViewerPanel.Name = "imageViewerPanel";
            this.imageViewerPanel.Size = new System.Drawing.Size(230, 72);
            this.imageViewerPanel.TabIndex = 4;
            // 
            // imageViewerResourceNameLabel
            // 
            this.imageViewerResourceNameLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.imageViewerResourceNameLabel.AutoEllipsis = true;
            this.imageViewerResourceNameLabel.Location = new System.Drawing.Point(3, 6);
            this.imageViewerResourceNameLabel.Name = "imageViewerResourceNameLabel";
            this.imageViewerResourceNameLabel.Size = new System.Drawing.Size(99, 18);
            this.imageViewerResourceNameLabel.TabIndex = 3;
            this.imageViewerResourceNameLabel.Text = "{Resource Name}";
            // 
            // PdfResourceViewerControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.imageViewerPanel);
            this.Controls.Add(this.textBoxPanel);
            this.DoubleBuffered = true;
            this.MinimumSize = new System.Drawing.Size(120, 65);
            this.Name = "PdfResourceViewerControl";
            this.Size = new System.Drawing.Size(237, 161);
            this.textBoxPanel.ResumeLayout(false);
            this.textBoxPanel.PerformLayout();
            this.imageViewerPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Vintasoft.Imaging.UI.ImageViewer resourceImageViewer;
        private System.Windows.Forms.TextBox resourceTextBox;
        private System.Windows.Forms.ComboBox sizeModeComboBox;
        private System.Windows.Forms.Panel textBoxPanel;
        private System.Windows.Forms.Panel imageViewerPanel;
        private System.Windows.Forms.Label textBoxResourceNameLabel;
        private System.Windows.Forms.Label imageViewerResourceNameLabel;
    }
}