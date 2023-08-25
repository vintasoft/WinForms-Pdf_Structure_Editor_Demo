namespace DemosCommonCode.Pdf
{
    partial class PdfTriggersEditorForm
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
            this.pdfTriggersEditorControl = new DemosCommonCode.Pdf.PdfTriggersEditorControl();
            this.okButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // pdfTriggersEditorControl
            // 
            this.pdfTriggersEditorControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pdfTriggersEditorControl.Location = new System.Drawing.Point(0, 0);
            this.pdfTriggersEditorControl.MinimumSize = new System.Drawing.Size(200, 275);
            this.pdfTriggersEditorControl.Name = "pdfTriggersEditorControl";
            this.pdfTriggersEditorControl.Size = new System.Drawing.Size(419, 275);
            this.pdfTriggersEditorControl.TabIndex = 0;
            this.pdfTriggersEditorControl.TreeNode = null;
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(331, 279);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 1;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            // 
            // PdfTriggersEditorWindow
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(418, 314);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.pdfTriggersEditorControl);
            this.MinimumSize = new System.Drawing.Size(269, 353);
            this.Name = "PdfTriggersEditorWindow";
            this.Text = "Triggers Editor";
            this.ResumeLayout(false);

        }

        #endregion

        private global::DemosCommonCode.Pdf.PdfTriggersEditorControl pdfTriggersEditorControl;
        private System.Windows.Forms.Button okButton;
    }
}