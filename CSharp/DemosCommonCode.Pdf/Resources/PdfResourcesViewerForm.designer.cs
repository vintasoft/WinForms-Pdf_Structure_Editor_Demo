namespace DemosCommonCode.Pdf
{
    partial class PdfResourcesViewerForm
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
            this.splitContainerMain = new System.Windows.Forms.SplitContainer();
            this.DocumentResourceViewer = new DemosCommonCode.Pdf.PdfDocumentResourceViewer();
            this.ResourceViewerControl = new DemosCommonCode.Pdf.PdfResourceViewerControl();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton1 = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.defaultCompressionParamsButton = new System.Windows.Forms.Button();
            this.propertyGrid = new System.Windows.Forms.PropertyGrid();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.resourcesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addFromDocumentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addFromDocumentToolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.createResourceFromImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createResourceFromSelectedPageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createResourceFromSelectedImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createResourcesToolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.saveAsBinaryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hierarchicalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.linearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openImageFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.splitContainerMain.Panel1.SuspendLayout();
            this.splitContainerMain.Panel2.SuspendLayout();
            this.splitContainerMain.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainerMain
            // 
            this.splitContainerMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerMain.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainerMain.Location = new System.Drawing.Point(0, 0);
            this.splitContainerMain.Name = "splitContainerMain";
            // 
            // splitContainerMain.Panel1
            // 
            this.splitContainerMain.Panel1.Controls.Add(this.DocumentResourceViewer);
            this.splitContainerMain.Panel1MinSize = 232;
            // 
            // splitContainerMain.Panel2
            // 
            this.splitContainerMain.Panel2.Controls.Add(this.ResourceViewerControl);
            this.splitContainerMain.Panel2MinSize = 130;
            this.splitContainerMain.Size = new System.Drawing.Size(578, 427);
            this.splitContainerMain.SplitterDistance = 239;
            this.splitContainerMain.TabIndex = 0;
            // 
            // DocumentResourceViewer
            // 
            this.DocumentResourceViewer.AdditionalResources = null;
            this.DocumentResourceViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DocumentResourceViewer.HideSelection = false;
            this.DocumentResourceViewer.Location = new System.Drawing.Point(0, 0);
            this.DocumentResourceViewer.Name = "DocumentResourceViewer";
            this.DocumentResourceViewer.SelectedObject = null;
            this.DocumentResourceViewer.SelectedResource = null;
            this.DocumentResourceViewer.Size = new System.Drawing.Size(239, 427);
            this.DocumentResourceViewer.Sorted = true;
            this.DocumentResourceViewer.TabIndex = 0;
            this.DocumentResourceViewer.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.pdfDocumentResourceViewer_AfterSelect);
            // 
            // ResourceViewerControl
            // 
            this.ResourceViewerControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ResourceViewerControl.ImageViewerAvailableZoomValues = new int[] {
        25,
        50,
        100,
        200,
        400};
            this.ResourceViewerControl.ImageViewerSizeMode = Vintasoft.Imaging.UI.ImageSizeMode.BestFit;
            this.ResourceViewerControl.Location = new System.Drawing.Point(0, 0);
            this.ResourceViewerControl.MinimumSize = new System.Drawing.Size(125, 65);
            this.ResourceViewerControl.Name = "ResourceViewerControl";
            this.ResourceViewerControl.Resource = null;
            this.ResourceViewerControl.Size = new System.Drawing.Size(335, 427);
            this.ResourceViewerControl.TabIndex = 0;
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(616, 460);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 1;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            // 
            // cancelButton1
            // 
            this.cancelButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton1.Location = new System.Drawing.Point(697, 460);
            this.cancelButton1.Name = "cancelButton1";
            this.cancelButton1.Size = new System.Drawing.Size(75, 23);
            this.cancelButton1.TabIndex = 2;
            this.cancelButton1.Text = "Cancel";
            this.cancelButton1.UseVisualStyleBackColor = true;
            // 
            // openFileDialog
            // 
            this.openFileDialog.Filter = "Pdf files|*.pdf";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.Location = new System.Drawing.Point(0, 27);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainerMain);
            this.splitContainer1.Panel1MinSize = 0;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.defaultCompressionParamsButton);
            this.splitContainer1.Panel2.Controls.Add(this.propertyGrid);
            this.splitContainer1.Panel2MinSize = 0;
            this.splitContainer1.Size = new System.Drawing.Size(772, 427);
            this.splitContainer1.SplitterDistance = 578;
            this.splitContainer1.TabIndex = 3;
            // 
            // defaultCompressionParamsButton
            // 
            this.defaultCompressionParamsButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.defaultCompressionParamsButton.Location = new System.Drawing.Point(3, 3);
            this.defaultCompressionParamsButton.Name = "defaultCompressionParamsButton";
            this.defaultCompressionParamsButton.Size = new System.Drawing.Size(184, 23);
            this.defaultCompressionParamsButton.TabIndex = 10;
            this.defaultCompressionParamsButton.Text = "Default Compression Params...";
            this.defaultCompressionParamsButton.UseVisualStyleBackColor = true;
            this.defaultCompressionParamsButton.Click += new System.EventHandler(this.defaultCompressionParamsButton_Click);
            // 
            // propertyGrid
            // 
            this.propertyGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.propertyGrid.Location = new System.Drawing.Point(3, 32);
            this.propertyGrid.Name = "propertyGrid";
            this.propertyGrid.Size = new System.Drawing.Size(184, 395);
            this.propertyGrid.TabIndex = 0;
            this.propertyGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.propertyGrid_PropertyValueChanged);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.resourcesToolStripMenuItem,
            this.viewToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(784, 24);
            this.menuStrip1.TabIndex = 5;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // resourcesToolStripMenuItem
            // 
            this.resourcesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addFromDocumentToolStripMenuItem,
            this.addFromDocumentToolStripSeparator,
            this.createResourceFromImageToolStripMenuItem,
            this.createResourceFromSelectedPageToolStripMenuItem,
            this.createResourceFromSelectedImageToolStripMenuItem,
            this.createResourcesToolStripSeparator,
            this.saveAsBinaryToolStripMenuItem,
            this.saveAsImageToolStripMenuItem});
            this.resourcesToolStripMenuItem.Name = "resourcesToolStripMenuItem";
            this.resourcesToolStripMenuItem.Size = new System.Drawing.Size(72, 20);
            this.resourcesToolStripMenuItem.Text = "Resources";
            // 
            // addFromDocumentToolStripMenuItem
            // 
            this.addFromDocumentToolStripMenuItem.Name = "addFromDocumentToolStripMenuItem";
            this.addFromDocumentToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
            this.addFromDocumentToolStripMenuItem.Text = "Add from Document...";
            this.addFromDocumentToolStripMenuItem.Click += new System.EventHandler(this.addFromDocumentToolStripMenuItem_Click);
            // 
            // addFromDocumentToolStripSeparator
            // 
            this.addFromDocumentToolStripSeparator.Name = "addFromDocumentToolStripSeparator";
            this.addFromDocumentToolStripSeparator.Size = new System.Drawing.Size(217, 6);
            // 
            // createResourceFromImageToolStripMenuItem
            // 
            this.createResourceFromImageToolStripMenuItem.Name = "createResourceFromImageToolStripMenuItem";
            this.createResourceFromImageToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
            this.createResourceFromImageToolStripMenuItem.Text = "Create from Image...";
            this.createResourceFromImageToolStripMenuItem.Click += new System.EventHandler(this.createResourceFromImageToolStripMenuItem_Click);
            // 
            // createResourceFromSelectedPageToolStripMenuItem
            // 
            this.createResourceFromSelectedPageToolStripMenuItem.Name = "createResourceFromSelectedPageToolStripMenuItem";
            this.createResourceFromSelectedPageToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
            this.createResourceFromSelectedPageToolStripMenuItem.Text = "Create from Selected Page";
            this.createResourceFromSelectedPageToolStripMenuItem.Click += new System.EventHandler(this.createResourceFromSelectedPageToolStripMenuItem_Click);
            // 
            // createResourceFromSelectedImageToolStripMenuItem
            // 
            this.createResourceFromSelectedImageToolStripMenuItem.Name = "createResourceFromSelectedImageToolStripMenuItem";
            this.createResourceFromSelectedImageToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
            this.createResourceFromSelectedImageToolStripMenuItem.Text = "Create from Selected Image";
            this.createResourceFromSelectedImageToolStripMenuItem.Click += new System.EventHandler(this.createResourceFromSelectedImageToolStripMenuItem_Click);
            // 
            // createResourcesToolStripSeparator
            // 
            this.createResourcesToolStripSeparator.Name = "createResourcesToolStripSeparator";
            this.createResourcesToolStripSeparator.Size = new System.Drawing.Size(217, 6);
            // 
            // saveAsBinaryToolStripMenuItem
            // 
            this.saveAsBinaryToolStripMenuItem.Name = "saveAsBinaryToolStripMenuItem";
            this.saveAsBinaryToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
            this.saveAsBinaryToolStripMenuItem.Text = "Save As Binary Data...";
            this.saveAsBinaryToolStripMenuItem.Click += new System.EventHandler(this.saveAsBinaryToolStripMenuItem_Click);
            // 
            // saveAsImageToolStripMenuItem
            // 
            this.saveAsImageToolStripMenuItem.Name = "saveAsImageToolStripMenuItem";
            this.saveAsImageToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
            this.saveAsImageToolStripMenuItem.Text = "Save As Image...";
            this.saveAsImageToolStripMenuItem.Click += new System.EventHandler(this.saveAsImageToolStripMenuItem_Click);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.hierarchicalToolStripMenuItem,
            this.linearToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem.Text = "View";
            // 
            // hierarchicalToolStripMenuItem
            // 
            this.hierarchicalToolStripMenuItem.Name = "hierarchicalToolStripMenuItem";
            this.hierarchicalToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.hierarchicalToolStripMenuItem.Text = "Hierarchical";
            this.hierarchicalToolStripMenuItem.Click += new System.EventHandler(this.treeViewTypeToolStripMenuItem_Click);
            // 
            // linearToolStripMenuItem
            // 
            this.linearToolStripMenuItem.Name = "linearToolStripMenuItem";
            this.linearToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.linearToolStripMenuItem.Text = "Linear";
            this.linearToolStripMenuItem.Click += new System.EventHandler(this.treeViewTypeToolStripMenuItem_Click);
            // 
            // PdfResourcesViewerForm
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton1;
            this.ClientSize = new System.Drawing.Size(784, 495);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.cancelButton1);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(614, 303);
            this.Name = "PdfResourcesViewerForm";
            this.ShowIcon = false;
            this.Text = "Resources Viewer";
            this.splitContainerMain.Panel1.ResumeLayout(false);
            this.splitContainerMain.Panel2.ResumeLayout(false);
            this.splitContainerMain.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainerMain;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton1;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.PropertyGrid propertyGrid;
        private System.Windows.Forms.Button defaultCompressionParamsButton;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem resourcesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hierarchicalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem linearToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addFromDocumentToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator addFromDocumentToolStripSeparator;
        private System.Windows.Forms.ToolStripMenuItem createResourceFromSelectedPageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createResourceFromImageToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator createResourcesToolStripSeparator;
        private System.Windows.Forms.ToolStripMenuItem saveAsBinaryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsImageToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openImageFileDialog;
        private System.Windows.Forms.ToolStripMenuItem createResourceFromSelectedImageToolStripMenuItem;
        private PdfResourceViewerControl ResourceViewerControl;
        internal PdfDocumentResourceViewer DocumentResourceViewer;
    }
}