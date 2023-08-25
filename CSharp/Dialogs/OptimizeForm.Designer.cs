namespace PdfStructureEditorDemo
{
    partial class OptimizeForm
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
            this.buttonCancel = new System.Windows.Forms.Button();
            this.okButton = new System.Windows.Forms.Button();
            this.defaultFilterParamsButton = new System.Windows.Forms.Button();
            this.documentFormatButton = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.bwJBIG2 = new System.Windows.Forms.RadioButton();
            this.bwLZW = new System.Windows.Forms.RadioButton();
            this.bwCCITTFax = new System.Windows.Forms.RadioButton();
            this.bwFlate = new System.Windows.Forms.RadioButton();
            this.bwNone = new System.Windows.Forms.RadioButton();
            this.bwUndefined = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dataLZW = new System.Windows.Forms.RadioButton();
            this.dataFlate = new System.Windows.Forms.RadioButton();
            this.dataNone = new System.Windows.Forms.RadioButton();
            this.dataUndefined = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.colorJpeg2000 = new System.Windows.Forms.RadioButton();
            this.colorJpeg = new System.Windows.Forms.RadioButton();
            this.colorLZW = new System.Windows.Forms.RadioButton();
            this.colorFlate = new System.Windows.Forms.RadioButton();
            this.colorNone = new System.Windows.Forms.RadioButton();
            this.colorUndefined = new System.Windows.Forms.RadioButton();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(196, 206);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 27);
            this.buttonCancel.TabIndex = 5;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(102, 206);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 27);
            this.okButton.TabIndex = 4;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // defaultFilterParamsButton
            // 
            this.defaultFilterParamsButton.Location = new System.Drawing.Point(9, 12);
            this.defaultFilterParamsButton.Name = "defaultFilterParamsButton";
            this.defaultFilterParamsButton.Size = new System.Drawing.Size(164, 23);
            this.defaultFilterParamsButton.TabIndex = 8;
            this.defaultFilterParamsButton.Text = "Default Filters Params...";
            this.defaultFilterParamsButton.UseVisualStyleBackColor = true;
            this.defaultFilterParamsButton.Click += new System.EventHandler(this.defaultFilterParamsButton_Click);
            // 
            // documentFormatButton
            // 
            this.documentFormatButton.Location = new System.Drawing.Point(196, 12);
            this.documentFormatButton.Name = "documentFormatButton";
            this.documentFormatButton.Size = new System.Drawing.Size(167, 23);
            this.documentFormatButton.TabIndex = 11;
            this.documentFormatButton.Text = "Document Format...";
            this.documentFormatButton.UseVisualStyleBackColor = true;
            this.documentFormatButton.Click += new System.EventHandler(this.documentFormatButton_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.bwJBIG2);
            this.groupBox3.Controls.Add(this.bwLZW);
            this.groupBox3.Controls.Add(this.bwCCITTFax);
            this.groupBox3.Controls.Add(this.bwFlate);
            this.groupBox3.Controls.Add(this.bwNone);
            this.groupBox3.Controls.Add(this.bwUndefined);
            this.groupBox3.Location = new System.Drawing.Point(135, 41);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(114, 158);
            this.groupBox3.TabIndex = 11;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "B/W Images Filter";
            // 
            // bwJBIG2
            // 
            this.bwJBIG2.AutoSize = true;
            this.bwJBIG2.Location = new System.Drawing.Point(6, 134);
            this.bwJBIG2.Name = "bwJBIG2";
            this.bwJBIG2.Size = new System.Drawing.Size(54, 17);
            this.bwJBIG2.TabIndex = 5;
            this.bwJBIG2.Text = "JBIG2";
            this.bwJBIG2.UseVisualStyleBackColor = true;
            // 
            // bwLZW
            // 
            this.bwLZW.AutoSize = true;
            this.bwLZW.Location = new System.Drawing.Point(6, 88);
            this.bwLZW.Name = "bwLZW";
            this.bwLZW.Size = new System.Drawing.Size(49, 17);
            this.bwLZW.TabIndex = 4;
            this.bwLZW.Text = "LZW";
            this.bwLZW.UseVisualStyleBackColor = true;
            // 
            // bwCCITTFax
            // 
            this.bwCCITTFax.AutoSize = true;
            this.bwCCITTFax.Location = new System.Drawing.Point(6, 111);
            this.bwCCITTFax.Name = "bwCCITTFax";
            this.bwCCITTFax.Size = new System.Drawing.Size(73, 17);
            this.bwCCITTFax.TabIndex = 3;
            this.bwCCITTFax.Text = "CCITTFax";
            this.bwCCITTFax.UseVisualStyleBackColor = true;
            // 
            // bwFlate
            // 
            this.bwFlate.AutoSize = true;
            this.bwFlate.Location = new System.Drawing.Point(6, 65);
            this.bwFlate.Name = "bwFlate";
            this.bwFlate.Size = new System.Drawing.Size(42, 17);
            this.bwFlate.TabIndex = 2;
            this.bwFlate.Text = "ZIP";
            this.bwFlate.UseVisualStyleBackColor = true;
            // 
            // bwNone
            // 
            this.bwNone.AutoSize = true;
            this.bwNone.Location = new System.Drawing.Point(6, 42);
            this.bwNone.Name = "bwNone";
            this.bwNone.Size = new System.Drawing.Size(51, 17);
            this.bwNone.TabIndex = 1;
            this.bwNone.Text = "None";
            this.bwNone.UseVisualStyleBackColor = true;
            // 
            // bwUndefined
            // 
            this.bwUndefined.AutoSize = true;
            this.bwUndefined.Checked = true;
            this.bwUndefined.Location = new System.Drawing.Point(6, 19);
            this.bwUndefined.Name = "bwUndefined";
            this.bwUndefined.Size = new System.Drawing.Size(82, 17);
            this.bwUndefined.TabIndex = 0;
            this.bwUndefined.TabStop = true;
            this.bwUndefined.Text = "Not Change";
            this.bwUndefined.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dataLZW);
            this.groupBox2.Controls.Add(this.dataFlate);
            this.groupBox2.Controls.Add(this.dataNone);
            this.groupBox2.Controls.Add(this.dataUndefined);
            this.groupBox2.Location = new System.Drawing.Point(255, 41);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(114, 158);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Data Filter";
            // 
            // dataLZW
            // 
            this.dataLZW.AutoSize = true;
            this.dataLZW.Location = new System.Drawing.Point(6, 88);
            this.dataLZW.Name = "dataLZW";
            this.dataLZW.Size = new System.Drawing.Size(49, 17);
            this.dataLZW.TabIndex = 3;
            this.dataLZW.Text = "LZW";
            this.dataLZW.UseVisualStyleBackColor = true;
            // 
            // dataFlate
            // 
            this.dataFlate.AutoSize = true;
            this.dataFlate.Location = new System.Drawing.Point(6, 65);
            this.dataFlate.Name = "dataFlate";
            this.dataFlate.Size = new System.Drawing.Size(42, 17);
            this.dataFlate.TabIndex = 2;
            this.dataFlate.Text = "ZIP";
            this.dataFlate.UseVisualStyleBackColor = true;
            // 
            // dataNone
            // 
            this.dataNone.AutoSize = true;
            this.dataNone.Location = new System.Drawing.Point(6, 42);
            this.dataNone.Name = "dataNone";
            this.dataNone.Size = new System.Drawing.Size(51, 17);
            this.dataNone.TabIndex = 1;
            this.dataNone.Text = "None";
            this.dataNone.UseVisualStyleBackColor = true;
            // 
            // dataUndefined
            // 
            this.dataUndefined.AutoSize = true;
            this.dataUndefined.Checked = true;
            this.dataUndefined.Location = new System.Drawing.Point(6, 19);
            this.dataUndefined.Name = "dataUndefined";
            this.dataUndefined.Size = new System.Drawing.Size(82, 17);
            this.dataUndefined.TabIndex = 0;
            this.dataUndefined.TabStop = true;
            this.dataUndefined.Text = "Not Change";
            this.dataUndefined.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.colorJpeg2000);
            this.groupBox1.Controls.Add(this.colorJpeg);
            this.groupBox1.Controls.Add(this.colorLZW);
            this.groupBox1.Controls.Add(this.colorFlate);
            this.groupBox1.Controls.Add(this.colorNone);
            this.groupBox1.Controls.Add(this.colorUndefined);
            this.groupBox1.Location = new System.Drawing.Point(9, 41);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(120, 158);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Color Images Filter";
            // 
            // colorJpeg2000
            // 
            this.colorJpeg2000.AutoSize = true;
            this.colorJpeg2000.Location = new System.Drawing.Point(6, 134);
            this.colorJpeg2000.Name = "colorJpeg2000";
            this.colorJpeg2000.Size = new System.Drawing.Size(79, 17);
            this.colorJpeg2000.TabIndex = 5;
            this.colorJpeg2000.Text = "JPEG 2000";
            this.colorJpeg2000.UseVisualStyleBackColor = true;
            // 
            // colorJpeg
            // 
            this.colorJpeg.AutoSize = true;
            this.colorJpeg.Location = new System.Drawing.Point(6, 111);
            this.colorJpeg.Name = "colorJpeg";
            this.colorJpeg.Size = new System.Drawing.Size(52, 17);
            this.colorJpeg.TabIndex = 4;
            this.colorJpeg.Text = "JPEG";
            this.colorJpeg.UseVisualStyleBackColor = true;
            // 
            // colorLZW
            // 
            this.colorLZW.AutoSize = true;
            this.colorLZW.Location = new System.Drawing.Point(6, 88);
            this.colorLZW.Name = "colorLZW";
            this.colorLZW.Size = new System.Drawing.Size(49, 17);
            this.colorLZW.TabIndex = 3;
            this.colorLZW.Text = "LZW";
            this.colorLZW.UseVisualStyleBackColor = true;
            // 
            // colorFlate
            // 
            this.colorFlate.AutoSize = true;
            this.colorFlate.Location = new System.Drawing.Point(6, 65);
            this.colorFlate.Name = "colorFlate";
            this.colorFlate.Size = new System.Drawing.Size(42, 17);
            this.colorFlate.TabIndex = 2;
            this.colorFlate.Text = "ZIP";
            this.colorFlate.UseVisualStyleBackColor = true;
            // 
            // colorNone
            // 
            this.colorNone.AutoSize = true;
            this.colorNone.Location = new System.Drawing.Point(6, 42);
            this.colorNone.Name = "colorNone";
            this.colorNone.Size = new System.Drawing.Size(51, 17);
            this.colorNone.TabIndex = 1;
            this.colorNone.Text = "None";
            this.colorNone.UseVisualStyleBackColor = true;
            // 
            // colorUndefined
            // 
            this.colorUndefined.AutoSize = true;
            this.colorUndefined.Checked = true;
            this.colorUndefined.Location = new System.Drawing.Point(6, 19);
            this.colorUndefined.Name = "colorUndefined";
            this.colorUndefined.Size = new System.Drawing.Size(82, 17);
            this.colorUndefined.TabIndex = 0;
            this.colorUndefined.TabStop = true;
            this.colorUndefined.Text = "Not Change";
            this.colorUndefined.UseVisualStyleBackColor = true;
            // 
            // OptimizeForm
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(383, 245);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.documentFormatButton);
            this.Controls.Add(this.defaultFilterParamsButton);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.okButton);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OptimizeForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Optimize PDF document";
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button defaultFilterParamsButton;
        private System.Windows.Forms.Button documentFormatButton;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton dataFlate;
        private System.Windows.Forms.RadioButton dataNone;
        private System.Windows.Forms.RadioButton dataUndefined;
        private System.Windows.Forms.RadioButton bwFlate;
        private System.Windows.Forms.RadioButton bwNone;
        private System.Windows.Forms.RadioButton bwUndefined;
        private System.Windows.Forms.RadioButton bwCCITTFax;
        private System.Windows.Forms.RadioButton dataLZW;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton colorJpeg;
        private System.Windows.Forms.RadioButton colorLZW;
        private System.Windows.Forms.RadioButton colorFlate;
        private System.Windows.Forms.RadioButton colorNone;
        private System.Windows.Forms.RadioButton colorUndefined;
        private System.Windows.Forms.RadioButton bwLZW;
        private System.Windows.Forms.RadioButton bwJBIG2;
        private System.Windows.Forms.RadioButton colorJpeg2000;
    }
}