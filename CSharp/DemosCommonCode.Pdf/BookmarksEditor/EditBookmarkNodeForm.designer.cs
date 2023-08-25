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
            ((System.ComponentModel.ISupportInitialize)(this.pageNumber)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(216, 130);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 27);
            this.buttonCancel.TabIndex = 5;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(135, 130);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 27);
            this.okButton.TabIndex = 4;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 15);
            this.label1.TabIndex = 6;
            this.label1.Text = "Title";
            // 
            // bookmarkTitle
            // 
            this.bookmarkTitle.Location = new System.Drawing.Point(132, 12);
            this.bookmarkTitle.Name = "bookmarkTitle";
            this.bookmarkTitle.Size = new System.Drawing.Size(157, 23);
            this.bookmarkTitle.TabIndex = 7;
            this.bookmarkTitle.Text = "Bookmark1";
            // 
            // bookmarkTextBold
            // 
            this.bookmarkTextBold.AutoSize = true;
            this.bookmarkTextBold.Location = new System.Drawing.Point(161, 95);
            this.bookmarkTextBold.Name = "bookmarkTextBold";
            this.bookmarkTextBold.Size = new System.Drawing.Size(50, 19);
            this.bookmarkTextBold.TabIndex = 8;
            this.bookmarkTextBold.Text = "Bold";
            this.bookmarkTextBold.UseVisualStyleBackColor = true;
            // 
            // bookmarkTextItalic
            // 
            this.bookmarkTextItalic.AutoSize = true;
            this.bookmarkTextItalic.Location = new System.Drawing.Point(107, 95);
            this.bookmarkTextItalic.Name = "bookmarkTextItalic";
            this.bookmarkTextItalic.Size = new System.Drawing.Size(51, 19);
            this.bookmarkTextItalic.TabIndex = 9;
            this.bookmarkTextItalic.Text = "Italic";
            this.bookmarkTextItalic.UseVisualStyleBackColor = true;
            // 
            // colorButton
            // 
            this.colorButton.Location = new System.Drawing.Point(12, 91);
            this.colorButton.Name = "colorButton";
            this.colorButton.Size = new System.Drawing.Size(75, 23);
            this.colorButton.TabIndex = 10;
            this.colorButton.Text = "Color...";
            this.colorButton.UseVisualStyleBackColor = true;
            this.colorButton.Click += new System.EventHandler(this.colorButton_Click);
            // 
            // bookmarkExpanded
            // 
            this.bookmarkExpanded.AutoSize = true;
            this.bookmarkExpanded.Location = new System.Drawing.Point(214, 95);
            this.bookmarkExpanded.Name = "bookmarkExpanded";
            this.bookmarkExpanded.Size = new System.Drawing.Size(78, 19);
            this.bookmarkExpanded.TabIndex = 11;
            this.bookmarkExpanded.Text = "Expanded";
            this.bookmarkExpanded.UseVisualStyleBackColor = true;
            // 
            // pageNumber
            // 
            this.pageNumber.Location = new System.Drawing.Point(132, 37);
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
            this.pageNumber.Name = "pageNumber";
            this.pageNumber.Size = new System.Drawing.Size(89, 23);
            this.pageNumber.TabIndex = 13;
            this.pageNumber.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // addToRootCheckBox
            // 
            this.addToRootCheckBox.AutoSize = true;
            this.addToRootCheckBox.Location = new System.Drawing.Point(12, 132);
            this.addToRootCheckBox.Name = "addToRootCheckBox";
            this.addToRootCheckBox.Size = new System.Drawing.Size(91, 19);
            this.addToRootCheckBox.TabIndex = 14;
            this.addToRootCheckBox.Text = "Add To Root";
            this.addToRootCheckBox.UseVisualStyleBackColor = true;
            // 
            // destRadioButton
            // 
            this.destRadioButton.AutoSize = true;
            this.destRadioButton.Location = new System.Drawing.Point(12, 37);
            this.destRadioButton.Name = "destRadioButton";
            this.destRadioButton.Size = new System.Drawing.Size(114, 19);
            this.destRadioButton.TabIndex = 15;
            this.destRadioButton.TabStop = true;
            this.destRadioButton.Text = "Destination Page";
            this.destRadioButton.UseVisualStyleBackColor = true;
            // 
            // actionRadioButton
            // 
            this.actionRadioButton.AutoSize = true;
            this.actionRadioButton.Location = new System.Drawing.Point(12, 60);
            this.actionRadioButton.Name = "actionRadioButton";
            this.actionRadioButton.Size = new System.Drawing.Size(60, 19);
            this.actionRadioButton.TabIndex = 16;
            this.actionRadioButton.TabStop = true;
            this.actionRadioButton.Text = "Action";
            this.actionRadioButton.UseVisualStyleBackColor = true;
            this.actionRadioButton.CheckedChanged += new System.EventHandler(this.actionRadioButton_CheckedChanged);
            // 
            // editActionButton
            // 
            this.editActionButton.Enabled = false;
            this.editActionButton.Location = new System.Drawing.Point(132, 58);
            this.editActionButton.Name = "editActionButton";
            this.editActionButton.Size = new System.Drawing.Size(89, 23);
            this.editActionButton.TabIndex = 17;
            this.editActionButton.Text = "Edit Action...";
            this.editActionButton.UseVisualStyleBackColor = true;
            this.editActionButton.Click += new System.EventHandler(this.editActionButton_Click);
            // 
            // EditBookmarkNodeForm
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(301, 165);
            this.Controls.Add(this.editActionButton);
            this.Controls.Add(this.actionRadioButton);
            this.Controls.Add(this.destRadioButton);
            this.Controls.Add(this.addToRootCheckBox);
            this.Controls.Add(this.pageNumber);
            this.Controls.Add(this.bookmarkExpanded);
            this.Controls.Add(this.colorButton);
            this.Controls.Add(this.bookmarkTextItalic);
            this.Controls.Add(this.bookmarkTextBold);
            this.Controls.Add(this.bookmarkTitle);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.okButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EditBookmarkNodeForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Bookmark";
            ((System.ComponentModel.ISupportInitialize)(this.pageNumber)).EndInit();
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
    }
}