namespace DemosCommonCode.Pdf
{
    partial class PdfInteractiveFormFieldListEditorControl
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
            this.components = new System.ComponentModel.Container();
            this.addButton = new System.Windows.Forms.Button();
            this.removeButton = new System.Windows.Forms.Button();
            this.removeAllButton = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.selectedFieldsListBox = new System.Windows.Forms.ListBox();
            this.pdfInteractiveFormFieldTree = new DemosCommonCode.Pdf.PdfInteractiveFormFieldTree();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // addButton
            // 
            this.addButton.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.addButton.Enabled = false;
            this.addButton.Location = new System.Drawing.Point(5, 16);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(70, 23);
            this.addButton.TabIndex = 2;
            this.addButton.Text = ">>";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // removeButton
            // 
            this.removeButton.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.removeButton.Enabled = false;
            this.removeButton.Location = new System.Drawing.Point(5, 45);
            this.removeButton.Name = "removeButton";
            this.removeButton.Size = new System.Drawing.Size(70, 23);
            this.removeButton.TabIndex = 3;
            this.removeButton.Text = "Remove";
            this.removeButton.UseVisualStyleBackColor = true;
            this.removeButton.Click += new System.EventHandler(this.removeButton_Click);
            // 
            // removeAllButton
            // 
            this.removeAllButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.removeAllButton.Enabled = false;
            this.removeAllButton.Location = new System.Drawing.Point(5, 255);
            this.removeAllButton.Name = "removeAllButton";
            this.removeAllButton.Size = new System.Drawing.Size(70, 23);
            this.removeAllButton.TabIndex = 4;
            this.removeAllButton.Text = "Remove All";
            this.removeAllButton.UseVisualStyleBackColor = true;
            this.removeAllButton.Click += new System.EventHandler(this.removeAllButton_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.selectedFieldsListBox, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.pdfInteractiveFormFieldTree, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(406, 296);
            this.tableLayoutPanel1.TabIndex = 5;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.removeAllButton);
            this.panel1.Controls.Add(this.addButton);
            this.panel1.Controls.Add(this.removeButton);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(161, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(84, 290);
            this.panel1.TabIndex = 0;
            // 
            // selectedFieldsListBox
            // 
            this.selectedFieldsListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.selectedFieldsListBox.FormattingEnabled = true;
            this.selectedFieldsListBox.Location = new System.Drawing.Point(251, 3);
            this.selectedFieldsListBox.Name = "selectedFieldsListBox";
            this.selectedFieldsListBox.Size = new System.Drawing.Size(152, 290);
            this.selectedFieldsListBox.TabIndex = 1;
            this.selectedFieldsListBox.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.selectedFieldsListBox_MouseDoubleClick);
            this.selectedFieldsListBox.SelectedIndexChanged += new System.EventHandler(this.selectedFieldsListBox_SelectedIndexChanged);
            // 
            // pdfInteractiveFormFieldTree
            // 
            this.pdfInteractiveFormFieldTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pdfInteractiveFormFieldTree.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawText;
            this.pdfInteractiveFormFieldTree.HideSelection = false;
            this.pdfInteractiveFormFieldTree.ImageIndex = 0;
            this.pdfInteractiveFormFieldTree.InteractiveForm = null;
            this.pdfInteractiveFormFieldTree.Location = new System.Drawing.Point(3, 3);
            this.pdfInteractiveFormFieldTree.Name = "pdfInteractiveFormFieldTree";
            this.pdfInteractiveFormFieldTree.SelectedField = null;
            this.pdfInteractiveFormFieldTree.SelectedImageIndex = 0;
            this.pdfInteractiveFormFieldTree.Size = new System.Drawing.Size(152, 290);
            this.pdfInteractiveFormFieldTree.GroupFormFieldsByPages = false;
            this.pdfInteractiveFormFieldTree.TabIndex = 0;
            this.pdfInteractiveFormFieldTree.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.pdfInteractiveFormFieldTree_NodeMouseDoubleClick);
            this.pdfInteractiveFormFieldTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.pdfInteractiveFormFieldTree_AfterSelect);
            // 
            // PdfInteractiveFormFieldListEditorControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.MinimumSize = new System.Drawing.Size(273, 115);
            this.Name = "PdfInteractiveFormFieldListEditorControl";
            this.Size = new System.Drawing.Size(406, 296);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private PdfInteractiveFormFieldTree pdfInteractiveFormFieldTree;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.Button removeButton;
        private System.Windows.Forms.Button removeAllButton;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ListBox selectedFieldsListBox;

    }
}