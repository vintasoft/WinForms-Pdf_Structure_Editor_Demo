namespace DemosCommonCode.Imaging
{
    partial class ProcessingCommandControlBase
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

         #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.mainTabControl = new System.Windows.Forms.TabControl();
            this.processingTabPage = new System.Windows.Forms.TabPage();
            this.viewProcessingTreeStructureCheckBox = new System.Windows.Forms.CheckBox();
            this.showResultsAfretExecuteCheckBox = new System.Windows.Forms.CheckBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.processingCommandViewer = new DemosCommonCode.Imaging.ProcessingCommandViewer();
            this.propertyGridGroupBox = new System.Windows.Forms.GroupBox();
            this.propertyGrid = new System.Windows.Forms.PropertyGrid();
            this.executeButton = new System.Windows.Forms.Button();
            this.resultTabPage = new System.Windows.Forms.TabPage();
            this.resultGroupBox = new System.Windows.Forms.GroupBox();
            this.processingResultViewer = new DemosCommonCode.Imaging.ProcessingResultViewer();
            this.mainTabControl.SuspendLayout();
            this.processingTabPage.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.propertyGridGroupBox.SuspendLayout();
            this.resultTabPage.SuspendLayout();
            this.resultGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainTabControl
            // 
            this.mainTabControl.Controls.Add(this.processingTabPage);
            this.mainTabControl.Controls.Add(this.resultTabPage);
            this.mainTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainTabControl.Location = new System.Drawing.Point(0, 0);
            this.mainTabControl.Name = "mainTabControl";
            this.mainTabControl.SelectedIndex = 0;
            this.mainTabControl.Size = new System.Drawing.Size(548, 499);
            this.mainTabControl.TabIndex = 1;
            this.mainTabControl.SelectedIndexChanged += new System.EventHandler(this.mainTabControl_SelectedIndexChanged);
            // 
            // processingTabPage
            // 
            this.processingTabPage.Controls.Add(this.viewProcessingTreeStructureCheckBox);
            this.processingTabPage.Controls.Add(this.showResultsAfretExecuteCheckBox);
            this.processingTabPage.Controls.Add(this.splitContainer1);
            this.processingTabPage.Controls.Add(this.executeButton);
            this.processingTabPage.Location = new System.Drawing.Point(4, 22);
            this.processingTabPage.Name = "processingTabPage";
            this.processingTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.processingTabPage.Size = new System.Drawing.Size(540, 473);
            this.processingTabPage.TabIndex = 0;
            this.processingTabPage.Text = "Processing";
            this.processingTabPage.UseVisualStyleBackColor = true;
            // 
            // viewProcessingTreeStructureCheckBox
            // 
            this.viewProcessingTreeStructureCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.viewProcessingTreeStructureCheckBox.AutoSize = true;
            this.viewProcessingTreeStructureCheckBox.Location = new System.Drawing.Point(8, 427);
            this.viewProcessingTreeStructureCheckBox.Name = "viewProcessingTreeStructureCheckBox";
            this.viewProcessingTreeStructureCheckBox.Size = new System.Drawing.Size(175, 17);
            this.viewProcessingTreeStructureCheckBox.TabIndex = 6;
            this.viewProcessingTreeStructureCheckBox.Text = "View Processing Tree Structure";
            this.viewProcessingTreeStructureCheckBox.UseVisualStyleBackColor = true;
            this.viewProcessingTreeStructureCheckBox.CheckedChanged += new System.EventHandler(this.viewProcessingTreeStructureCheckBox_CheckedChanged);
            // 
            // showResultsAfretExecuteCheckBox
            // 
            this.showResultsAfretExecuteCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.showResultsAfretExecuteCheckBox.AutoSize = true;
            this.showResultsAfretExecuteCheckBox.Checked = true;
            this.showResultsAfretExecuteCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showResultsAfretExecuteCheckBox.Location = new System.Drawing.Point(8, 450);
            this.showResultsAfretExecuteCheckBox.Name = "showResultsAfretExecuteCheckBox";
            this.showResultsAfretExecuteCheckBox.Size = new System.Drawing.Size(150, 17);
            this.showResultsAfretExecuteCheckBox.TabIndex = 5;
            this.showResultsAfretExecuteCheckBox.Text = "Open results after execute";
            this.showResultsAfretExecuteCheckBox.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.Location = new System.Drawing.Point(8, 6);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.processingCommandViewer);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.propertyGridGroupBox);
            this.splitContainer1.Size = new System.Drawing.Size(524, 415);
            this.splitContainer1.SplitterDistance = 249;
            this.splitContainer1.TabIndex = 4;
            // 
            // processingCommandViewer
            // 
            this.processingCommandViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.processingCommandViewer.Enabled = false;
            this.processingCommandViewer.HideSelection = false;
            this.processingCommandViewer.ImageIndex = 0;
            this.processingCommandViewer.Location = new System.Drawing.Point(0, 0);
            this.processingCommandViewer.MinimumSize = new System.Drawing.Size(157, 71);
            this.processingCommandViewer.Name = "processingCommandViewer";
            this.processingCommandViewer.ProcessingCommands = null;
            this.processingCommandViewer.SelectedImageIndex = 0;
            this.processingCommandViewer.SelectedProcessingCommand = null;
            this.processingCommandViewer.Size = new System.Drawing.Size(249, 415);
            this.processingCommandViewer.TabIndex = 0;
            this.processingCommandViewer.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.processingCommandViewer_AfterSelect);
            // 
            // propertyGridGroupBox
            // 
            this.propertyGridGroupBox.Controls.Add(this.propertyGrid);
            this.propertyGridGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGridGroupBox.Location = new System.Drawing.Point(0, 0);
            this.propertyGridGroupBox.Name = "propertyGridGroupBox";
            this.propertyGridGroupBox.Size = new System.Drawing.Size(271, 415);
            this.propertyGridGroupBox.TabIndex = 4;
            this.propertyGridGroupBox.TabStop = false;
            // 
            // propertyGrid
            // 
            this.propertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGrid.Location = new System.Drawing.Point(3, 16);
            this.propertyGrid.Name = "propertyGrid";
            this.propertyGrid.Size = new System.Drawing.Size(265, 396);
            this.propertyGrid.TabIndex = 3;
            this.propertyGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.propertyGrid_PropertyValueChanged);
            // 
            // executeButton
            // 
            this.executeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.executeButton.Location = new System.Drawing.Point(457, 444);
            this.executeButton.Name = "executeButton";
            this.executeButton.Size = new System.Drawing.Size(75, 23);
            this.executeButton.TabIndex = 2;
            this.executeButton.Text = "Execute";
            this.executeButton.UseVisualStyleBackColor = true;
            this.executeButton.Click += new System.EventHandler(this.executeButton_Click);
            // 
            // resultTabPage
            // 
            this.resultTabPage.Controls.Add(this.resultGroupBox);
            this.resultTabPage.Location = new System.Drawing.Point(4, 22);
            this.resultTabPage.Name = "resultTabPage";
            this.resultTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.resultTabPage.Size = new System.Drawing.Size(540, 473);
            this.resultTabPage.TabIndex = 1;
            this.resultTabPage.Text = "Result";
            this.resultTabPage.UseVisualStyleBackColor = true;
            // 
            // resultGroupBox
            // 
            this.resultGroupBox.Controls.Add(this.processingResultViewer);
            this.resultGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.resultGroupBox.Location = new System.Drawing.Point(3, 3);
            this.resultGroupBox.Name = "resultGroupBox";
            this.resultGroupBox.Size = new System.Drawing.Size(534, 467);
            this.resultGroupBox.TabIndex = 1;
            this.resultGroupBox.TabStop = false;
            // 
            // processingResultViewer
            // 
            this.processingResultViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.processingResultViewer.ImageIndex = 0;
            this.processingResultViewer.Location = new System.Drawing.Point(3, 16);
            this.processingResultViewer.MinimumSize = new System.Drawing.Size(116, 88);
            this.processingResultViewer.Name = "processingResultViewer";
            this.processingResultViewer.ProcessingResult = null;
            this.processingResultViewer.SelectedImageIndex = 0;
            this.processingResultViewer.Size = new System.Drawing.Size(528, 448);
            this.processingResultViewer.TabIndex = 0;
            // 
            // ProcessingCommandControlBase
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.mainTabControl);
            this.MinimumSize = new System.Drawing.Size(466, 187);
            this.Name = "ProcessingCommandControlBase";
            this.Size = new System.Drawing.Size(548, 499);
            this.mainTabControl.ResumeLayout(false);
            this.processingTabPage.ResumeLayout(false);
            this.processingTabPage.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.propertyGridGroupBox.ResumeLayout(false);
            this.resultTabPage.ResumeLayout(false);
            this.resultGroupBox.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl mainTabControl;
        private System.Windows.Forms.TabPage processingTabPage;
        private System.Windows.Forms.CheckBox viewProcessingTreeStructureCheckBox;
        private System.Windows.Forms.CheckBox showResultsAfretExecuteCheckBox;
        private System.Windows.Forms.SplitContainer splitContainer1;
        protected ProcessingCommandViewer processingCommandViewer;
        private System.Windows.Forms.GroupBox propertyGridGroupBox;
        private System.Windows.Forms.PropertyGrid propertyGrid;
        private System.Windows.Forms.Button executeButton;
        private System.Windows.Forms.TabPage resultTabPage;
        private System.Windows.Forms.GroupBox resultGroupBox;
        private ProcessingResultViewer processingResultViewer;
    }
}