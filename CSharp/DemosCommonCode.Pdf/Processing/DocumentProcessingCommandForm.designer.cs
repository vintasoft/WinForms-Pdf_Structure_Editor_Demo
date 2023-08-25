namespace DemosCommonCode.Pdf
{
    partial class DocumentProcessingCommandForm
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resultToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.detailedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.byPageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.decreaseMemoryUsageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fastModeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.documentProcessingCommandControl = new DemosCommonCode.Imaging.DocumentProcessingCommandControl();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewToolStripMenuItem,
            this.settingsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(681, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.resultToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem.Text = "View";
            // 
            // resultToolStripMenuItem
            // 
            this.resultToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.detailedToolStripMenuItem,
            this.byPageToolStripMenuItem});
            this.resultToolStripMenuItem.Name = "resultToolStripMenuItem";
            this.resultToolStripMenuItem.Size = new System.Drawing.Size(106, 22);
            this.resultToolStripMenuItem.Text = "Result";
            // 
            // detailedToolStripMenuItem
            // 
            this.detailedToolStripMenuItem.Name = "detailedToolStripMenuItem";
            this.detailedToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.detailedToolStripMenuItem.Text = "Detailed";
            this.detailedToolStripMenuItem.Click += new System.EventHandler(this.ProcessingResultViewToolStripMenuItem_Click);
            // 
            // byPageToolStripMenuItem
            // 
            this.byPageToolStripMenuItem.Name = "byPageToolStripMenuItem";
            this.byPageToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.byPageToolStripMenuItem.Text = "By Page";
            this.byPageToolStripMenuItem.Click += new System.EventHandler(this.ProcessingResultViewToolStripMenuItem_Click);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.decreaseMemoryUsageToolStripMenuItem,
            this.fastModeToolStripMenuItem});
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.settingsToolStripMenuItem.Text = "Settings";
            // 
            // decreaseMemoryUsageToolStripMenuItem
            // 
            this.decreaseMemoryUsageToolStripMenuItem.CheckOnClick = true;
            this.decreaseMemoryUsageToolStripMenuItem.Name = "decreaseMemoryUsageToolStripMenuItem";
            this.decreaseMemoryUsageToolStripMenuItem.Size = new System.Drawing.Size(203, 22);
            this.decreaseMemoryUsageToolStripMenuItem.Text = "Decrease memory usage";
            this.decreaseMemoryUsageToolStripMenuItem.ToolTipText = "ProcessingState.StorePredicateResults - Disable collect results of predicates";
            this.decreaseMemoryUsageToolStripMenuItem.CheckedChanged += new System.EventHandler(this.decreaseMemoryUsageToolStripMenuItem_CheckedChanged);
            // 
            // fastModeToolStripMenuItem
            // 
            this.fastModeToolStripMenuItem.CheckOnClick = true;
            this.fastModeToolStripMenuItem.Name = "fastModeToolStripMenuItem";
            this.fastModeToolStripMenuItem.Size = new System.Drawing.Size(203, 22);
            this.fastModeToolStripMenuItem.Text = "Fast mode";
            this.fastModeToolStripMenuItem.ToolTipText = "ProcessingState.ThrowTriggerActivatedException - interrupt processing if trigger " +
                "activated";
            this.fastModeToolStripMenuItem.CheckedChanged += new System.EventHandler(this.fastModeToolStripMenuItem_CheckedChanged);
            // 
            // documentProcessingCommandControl
            // 
            this.documentProcessingCommandControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.documentProcessingCommandControl.Location = new System.Drawing.Point(0, 24);
            this.documentProcessingCommandControl.MinimumSize = new System.Drawing.Size(466, 187);
            this.documentProcessingCommandControl.Name = "documentProcessingCommandControl";
            this.documentProcessingCommandControl.ProcessingCommands = null;
            this.documentProcessingCommandControl.ProcessingTarget = null;
            this.documentProcessingCommandControl.Size = new System.Drawing.Size(681, 534);
            this.documentProcessingCommandControl.TabIndex = 0;
            this.documentProcessingCommandControl.ViewType = DemosCommonCode.Imaging.ProcessingResultTreeType.Detailed;
            // 
            // DocumentProcessingCommandForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(681, 558);
            this.Controls.Add(this.documentProcessingCommandControl);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(484, 263);
            this.Name = "DocumentProcessingCommandForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PdfDocument processing";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DemosCommonCode.Imaging.DocumentProcessingCommandControl documentProcessingCommandControl;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem resultToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem detailedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem byPageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem decreaseMemoryUsageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fastModeToolStripMenuItem;
    }
}