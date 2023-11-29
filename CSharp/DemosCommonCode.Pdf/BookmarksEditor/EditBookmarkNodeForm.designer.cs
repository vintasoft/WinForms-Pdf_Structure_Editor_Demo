namespace DemosCommonCode.Pdf
{
    partial class EditBookmarkNodeForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.bookmarkTitle = new System.Windows.Forms.TextBox();
            this.bookmarkTextBold = new System.Windows.Forms.CheckBox();
            this.bookmarkTextItalic = new System.Windows.Forms.CheckBox();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.colorButton = new System.Windows.Forms.Button();
            this.bookmarkExpanded = new System.Windows.Forms.CheckBox();
            this.pageNumber = new System.Windows.Forms.NumericUpDown();
            this.addToRootCheckBox = new System.Windows.Forms.CheckBox();
            this.destRadioButton = new System.Windows.Forms.RadioButton();
            this.actionRadioButton = new System.Windows.Forms.RadioButton();
            this.editActionButton = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.pageNumber)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(109, 3);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 27);
            this.buttonCancel.TabIndex = 5;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.Location = new System.Drawing.Point(28, 3);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 27);
            this.okButton.TabIndex = 4;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(27, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Title";
            // 
            // bookmarkTitle
            // 
            this.bookmarkTitle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.SetColumnSpan(this.bookmarkTitle, 3);
            this.bookmarkTitle.Location = new System.Drawing.Point(115, 3);
            this.bookmarkTitle.MinimumSize = new System.Drawing.Size(120, 0);
            this.bookmarkTitle.Name = "bookmarkTitle";
            this.bookmarkTitle.Size = new System.Drawing.Size(181, 20);
            this.bookmarkTitle.TabIndex = 7;
            this.bookmarkTitle.Text = "Bookmark1";
            // 
            // bookmarkTextBold
            // 
            this.bookmarkTextBold.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.bookmarkTextBold.AutoSize = true;
            this.bookmarkTextBold.Location = new System.Drawing.Point(169, 87);
            this.bookmarkTextBold.Name = "bookmarkTextBold";
            this.bookmarkTextBold.Size = new System.Drawing.Size(47, 17);
            this.bookmarkTextBold.TabIndex = 8;
            this.bookmarkTextBold.Text = "Bold";
            this.bookmarkTextBold.UseVisualStyleBackColor = true;
            // 
            // bookmarkTextItalic
            // 
            this.bookmarkTextItalic.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.bookmarkTextItalic.AutoSize = true;
            this.bookmarkTextItalic.Location = new System.Drawing.Point(115, 87);
            this.bookmarkTextItalic.Name = "bookmarkTextItalic";
            this.bookmarkTextItalic.Size = new System.Drawing.Size(48, 17);
            this.bookmarkTextItalic.TabIndex = 9;
            this.bookmarkTextItalic.Text = "Italic";
            this.bookmarkTextItalic.UseVisualStyleBackColor = true;
            // 
            // colorButton
            // 
            this.colorButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.colorButton.Location = new System.Drawing.Point(3, 84);
            this.colorButton.Name = "colorButton";
            this.colorButton.Size = new System.Drawing.Size(106, 23);
            this.colorButton.TabIndex = 10;
            this.colorButton.Text = "Color...";
            this.colorButton.UseVisualStyleBackColor = true;
            this.colorButton.Click += new System.EventHandler(this.colorButton_Click);
            // 
            // bookmarkExpanded
            // 
            this.bookmarkExpanded.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.bookmarkExpanded.AutoSize = true;
            this.bookmarkExpanded.Location = new System.Drawing.Point(222, 87);
            this.bookmarkExpanded.Name = "bookmarkExpanded";
            this.bookmarkExpanded.Size = new System.Drawing.Size(74, 17);
            this.bookmarkExpanded.TabIndex = 11;
            this.bookmarkExpanded.Text = "Expanded";
            this.bookmarkExpanded.UseVisualStyleBackColor = true;
            // 
            // pageNumber
            // 
            this.pageNumber.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.SetColumnSpan(this.pageNumber, 3);
            this.pageNumber.Location = new System.Drawing.Point(115, 29);
            this.pageNumber.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.pageNumber.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.pageNumber.MinimumSize = new System.Drawing.Size(120, 0);
            this.pageNumber.Name = "pageNumber";
            this.pageNumber.Size = new System.Drawing.Size(181, 20);
            this.pageNumber.TabIndex = 13;
            this.pageNumber.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // addToRootCheckBox
            // 
            this.addToRootCheckBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.addToRootCheckBox.AutoSize = true;
            this.addToRootCheckBox.Location = new System.Drawing.Point(3, 118);
            this.addToRootCheckBox.Name = "addToRootCheckBox";
            this.addToRootCheckBox.Size = new System.Drawing.Size(87, 17);
            this.addToRootCheckBox.TabIndex = 14;
            this.addToRootCheckBox.Text = "Add To Root";
            this.addToRootCheckBox.UseVisualStyleBackColor = true;
            // 
            // destRadioButton
            // 
            this.destRadioButton.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.destRadioButton.AutoSize = true;
            this.destRadioButton.Location = new System.Drawing.Point(3, 30);
            this.destRadioButton.Name = "destRadioButton";
            this.destRadioButton.Size = new System.Drawing.Size(106, 17);
            this.destRadioButton.TabIndex = 15;
            this.destRadioButton.TabStop = true;
            this.destRadioButton.Text = "Destination Page";
            this.destRadioButton.UseVisualStyleBackColor = true;
            // 
            // actionRadioButton
            // 
            this.actionRadioButton.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.actionRadioButton.AutoSize = true;
            this.actionRadioButton.Location = new System.Drawing.Point(3, 58);
            this.actionRadioButton.Name = "actionRadioButton";
            this.actionRadioButton.Size = new System.Drawing.Size(55, 17);
            this.actionRadioButton.TabIndex = 16;
            this.actionRadioButton.TabStop = true;
            this.actionRadioButton.Text = "Action";
            this.actionRadioButton.UseVisualStyleBackColor = true;
            this.actionRadioButton.CheckedChanged += new System.EventHandler(this.actionRadioButton_CheckedChanged);
            // 
            // editActionButton
            // 
            this.editActionButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.editActionButton.AutoSize = true;
            this.editActionButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.SetColumnSpan(this.editActionButton, 3);
            this.editActionButton.Enabled = false;
            this.editActionButton.Location = new System.Drawing.Point(115, 55);
            this.editActionButton.Name = "editActionButton";
            this.editActionButton.Size = new System.Drawing.Size(181, 23);
            this.editActionButton.TabIndex = 17;
            this.editActionButton.Text = "Edit Action...";
            this.editActionButton.UseVisualStyleBackColor = true;
            this.editActionButton.Click += new System.EventHandler(this.editActionButton_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.actionRadioButton, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.addToRootCheckBox, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.bookmarkExpanded, 3, 3);
            this.tableLayoutPanel1.Controls.Add(this.editActionButton, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.bookmarkTextBold, 2, 3);
            this.tableLayoutPanel1.Controls.Add(this.bookmarkTextItalic, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.colorButton, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.destRadioButton, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.bookmarkTitle, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.pageNumber, 1, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 12);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(299, 143);
            this.tableLayoutPanel1.TabIndex = 18;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.AutoSize = true;
            this.tableLayoutPanel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel1.SetColumnSpan(this.tableLayoutPanel2, 3);
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.Controls.Add(this.okButton, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.buttonCancel, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(112, 110);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.Size = new System.Drawing.Size(187, 33);
            this.tableLayoutPanel2.TabIndex = 19;
            // 
            // EditBookmarkNodeForm
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSize = true;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(324, 166);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EditBookmarkNodeForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Bookmark";
            ((System.ComponentModel.ISupportInitialize)(this.pageNumber)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox bookmarkTitle;
        private System.Windows.Forms.CheckBox bookmarkTextBold;
        private System.Windows.Forms.CheckBox bookmarkTextItalic;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.Button colorButton;
        private System.Windows.Forms.CheckBox bookmarkExpanded;
        private System.Windows.Forms.NumericUpDown pageNumber;
        private System.Windows.Forms.CheckBox addToRootCheckBox;
        private System.Windows.Forms.RadioButton destRadioButton;
        private System.Windows.Forms.RadioButton actionRadioButton;
        private System.Windows.Forms.Button editActionButton;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
    }
}