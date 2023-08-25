namespace DemosCommonCode.Pdf.Security
{
    partial class DocumentSignaturesForm
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
            this.okButton = new System.Windows.Forms.Button();
            this.vefifyAllButton = new System.Windows.Forms.Button();
            this.signaturesTreeView = new System.Windows.Forms.TreeView();
            this.saveDocumentRevisionButton = new System.Windows.Forms.Button();
            this.useSigningCertificateChainToBuildChainCheckBox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.Location = new System.Drawing.Point(532, 411);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(80, 23);
            this.okButton.TabIndex = 2;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // vefifyAllButton
            // 
            this.vefifyAllButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.vefifyAllButton.Location = new System.Drawing.Point(11, 411);
            this.vefifyAllButton.Name = "vefifyAllButton";
            this.vefifyAllButton.Size = new System.Drawing.Size(146, 23);
            this.vefifyAllButton.TabIndex = 5;
            this.vefifyAllButton.Text = "Verify All Signatures";
            this.vefifyAllButton.UseVisualStyleBackColor = true;
            this.vefifyAllButton.Click += new System.EventHandler(this.verifyAllButton_Click);
            // 
            // signaturesTreeView
            // 
            this.signaturesTreeView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.signaturesTreeView.HideSelection = false;
            this.signaturesTreeView.Location = new System.Drawing.Point(12, 12);
            this.signaturesTreeView.Name = "signaturesTreeView";
            this.signaturesTreeView.Size = new System.Drawing.Size(600, 371);
            this.signaturesTreeView.TabIndex = 6;
            this.signaturesTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.signaturesTreeView_AfterSelect);
            this.signaturesTreeView.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.signaturesTreeView_NodeMouseClick);
            // 
            // saveDocumentRevisionButton
            // 
            this.saveDocumentRevisionButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.saveDocumentRevisionButton.Location = new System.Drawing.Point(163, 411);
            this.saveDocumentRevisionButton.Name = "saveDocumentRevisionButton";
            this.saveDocumentRevisionButton.Size = new System.Drawing.Size(183, 23);
            this.saveDocumentRevisionButton.TabIndex = 7;
            this.saveDocumentRevisionButton.Text = "Save Document Resivion As...";
            this.saveDocumentRevisionButton.UseVisualStyleBackColor = true;
            this.saveDocumentRevisionButton.Click += new System.EventHandler(this.saveDocumentRevisionButton_Click);
            // 
            // useSigningCertificateChainToBuildChainCheckBox
            // 
            this.useSigningCertificateChainToBuildChainCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.useSigningCertificateChainToBuildChainCheckBox.AutoSize = true;
            this.useSigningCertificateChainToBuildChainCheckBox.Location = new System.Drawing.Point(12, 391);
            this.useSigningCertificateChainToBuildChainCheckBox.Name = "useSigningCertificateChainToBuildChainCheckBox";
            this.useSigningCertificateChainToBuildChainCheckBox.Size = new System.Drawing.Size(463, 17);
            this.useSigningCertificateChainToBuildChainCheckBox.TabIndex = 8;
            this.useSigningCertificateChainToBuildChainCheckBox.Text = "Use certificate chain from signature to build and verify certificate chain (no re" +
    "vocation check)";
            this.useSigningCertificateChainToBuildChainCheckBox.UseVisualStyleBackColor = true;
            // 
            // DocumentSignaturesForm
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 442);
            this.Controls.Add(this.useSigningCertificateChainToBuildChainCheckBox);
            this.Controls.Add(this.saveDocumentRevisionButton);
            this.Controls.Add(this.signaturesTreeView);
            this.Controls.Add(this.vefifyAllButton);
            this.Controls.Add(this.okButton);
            this.Name = "DocumentSignaturesForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Document Signatures";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button vefifyAllButton;
        private System.Windows.Forms.TreeView signaturesTreeView;
        private System.Windows.Forms.Button saveDocumentRevisionButton;
        private System.Windows.Forms.CheckBox useSigningCertificateChainToBuildChainCheckBox;
    }
}