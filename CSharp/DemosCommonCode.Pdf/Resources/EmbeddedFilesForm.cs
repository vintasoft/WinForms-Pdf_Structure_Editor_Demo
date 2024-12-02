using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;

using Vintasoft.Imaging.Pdf;
using Vintasoft.Imaging.Pdf.Tree;
using Vintasoft.Imaging.Pdf.Tree.Annotations;

namespace DemosCommonCode.Pdf
{
    /// <summary>
    /// A form that allows to view, add and remove embedded files of PDF document.
    /// </summary>
    public partial class EmbeddedFilesForm : Form
    {

        #region Constants

        /// <summary>
        /// The date time format.
        /// </summary>
        const string DATE_TIME_FORMAT = "dd.MM.yyyy hh:mm:ss";

        /// <summary>
        /// The form title.
        /// </summary>
        const string FORM_TITLE = "Embedded Files";

        #endregion



        #region Fields

        /// <summary>
        /// Indicates whether the file properties is showing.
        /// </summary>
        bool _showingFileProperties;

        /// <summary>
        /// Dictionary: embedded file => embedded file path.
        /// </summary>
        Dictionary<PdfEmbeddedFileSpecification, string> _embeddedFiles;

        /// <summary>
        /// Dictionary: embedded file => file attachment annotation.
        /// </summary>
        Dictionary<PdfEmbeddedFileSpecification, PdfFileAttachmentAnnotation> _fileAttachments;

        #endregion



        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="EmbeddedFilesForm"/> class.
        /// </summary>
        public EmbeddedFilesForm()
        {
            InitializeComponent();
            Text = FORM_TITLE;
            compressionComboBox.Items.Add(PdfCompression.None);
            compressionComboBox.Items.Add(PdfCompression.Zip);
            compressionComboBox.Items.Add(PdfCompression.Zip | PdfCompression.Ascii85);
            compressionComboBox.Items.Add(PdfCompression.Lzw);
            compressionComboBox.Items.Add(PdfCompression.RunLength);
            compressionComboBox.Items.Add(PdfCompression.Ascii85);
            compressionComboBox.Items.Add(PdfCompression.AsciiHex);
        }

        #endregion



        #region Properties

        /// <summary>
        /// Gets or set a value indicating whether this editor can edit embedded files.
        /// </summary>
        /// <value>
        /// <b>True</b> if this editor can edit embedded files; otherwise, <b>false</b>.
        /// </value>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool CanEditEmbeddedFiles
        {
            get
            {
                return descriptionTextBox.Enabled;
            }
            set
            {
                descriptionTextBox.Enabled = value;
                removeButton.Visible = value;
                addButton.Visible = value;
                compressionComboBox.Enabled = value;
            }
        }

        PdfDocument _document;
        /// <summary>
        /// Gets or sets the PDF document.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public PdfDocument Document
        {
            get
            {
                return _document;
            }
            set
            {
                _document = value;
                embeddedFilesDataGridView.Rows.Clear();
                
                // process Document.EmbeddedFiles property
                _embeddedFiles = new Dictionary<PdfEmbeddedFileSpecification, string>();
                if (_document.EmbeddedFiles != null)
                {
                    foreach (string name in _document.EmbeddedFiles.Keys)
                    {
                        PdfEmbeddedFileSpecification embeddedFileSpecification = _document.EmbeddedFiles[name];
                        if (embeddedFileSpecification.EmbeddedFile != null)
                        {
                            _embeddedFiles.Add(embeddedFileSpecification, name);
                            AddRow(embeddedFileSpecification);
                        }
                    }
                }

                // process File Attachment Annotations
                _fileAttachments = new Dictionary<PdfEmbeddedFileSpecification, PdfFileAttachmentAnnotation>();
                foreach (PdfPage page in _document.Pages)
                {
                    if (page.Annotations != null)
                    {
                        foreach (PdfAnnotation annotation in page.Annotations)
                        {
                            if (annotation is PdfFileAttachmentAnnotation)
                            {
                                PdfFileAttachmentAnnotation fileAttachmentAnnotation = (PdfFileAttachmentAnnotation)annotation;
                                if (fileAttachmentAnnotation.FileSpecification != null)
                                {
                                    if (fileAttachmentAnnotation.FileSpecification.EmbeddedFile != null)
                                    {
                                        _fileAttachments.Add(fileAttachmentAnnotation.FileSpecification, fileAttachmentAnnotation);
                                        AddRow(fileAttachmentAnnotation.FileSpecification);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Gets the selected file specification.
        /// </summary>
        private PdfEmbeddedFileSpecification SelectedFileSpecification
        {
            get
            {
                if (embeddedFilesDataGridView.SelectedRows.Count > 0)
                {
                    return (PdfEmbeddedFileSpecification)embeddedFilesDataGridView.SelectedRows[0].Tag;
                }
                return null;
            }
        }

        #endregion



        #region Methods

        #region UI

        /// <summary>
        /// Handles the Click event of okButton object.
        /// </summary>
        private void okButton_Click(object sender, EventArgs e)
        {
            // close this form
            Close();
        }

        /// <summary>
        /// Handles the Click event of saveAsButton object.
        /// </summary>
        private void saveAsButton_Click(object sender, EventArgs e)
        {
            PdfEmbeddedFileSpecification fileSpecification = SelectedFileSpecification;
            // if file specification exists
            if (fileSpecification != null)
            {
                saveFileDialog1.FileName = fileSpecification.Filename;
                // if file must be saved
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                    // save embedded file
                    fileSpecification.EmbeddedFile.Save(saveFileDialog1.FileName);
            }
        }

        /// <summary>
        /// Handles the Click event of removeButton object.
        /// </summary>
        private void removeButton_Click(object sender, EventArgs e)
        {
            // if several embedded files are selected
            if (embeddedFilesDataGridView.SelectedRows.Count > 0)
            {
                // get selected files
                DataGridViewRow row = embeddedFilesDataGridView.SelectedRows[0];
                // get embedded file
                PdfEmbeddedFileSpecification embeddedFile = (PdfEmbeddedFileSpecification)row.Tag;

                if (_embeddedFiles.ContainsKey(embeddedFile))
                    // remove embedded file
                    Document.EmbeddedFiles.Remove(_embeddedFiles[embeddedFile]);
                else if (_fileAttachments[embeddedFile].Page != null && _fileAttachments[embeddedFile].Page.Annotations != null)
                    // remove annotation
                    _fileAttachments[embeddedFile].Page.Annotations.Remove(_fileAttachments[embeddedFile]);

                embeddedFilesDataGridView.Rows.Remove(row);
            }
        }

        /// <summary>
        /// Handles the Click event of addButton object.
        /// </summary>
        private void addButton_Click(object sender, EventArgs e)
        {
            // if file must be opened
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                // if dictionalry for embedded files does not exist
                if (Document.EmbeddedFiles == null)
                    // create dictionary for embedded files
                    Document.EmbeddedFiles = new PdfEmbeddedFileSpecificationDictionary(Document);

                // disable control buttons panel
                controlButtonsPanel.Enabled = false;
                controlButtonsPanel.Refresh();
                // for each file in selected file
                foreach (string filename in openFileDialog1.FileNames)
                {
                    try
                    {
                        // update form title
                        Text = string.Format("Add embedded file '{0}'...", Path.GetFileName(filename));

                        // create PDF embedded file
                        PdfCompression comression = PdfCompression.Auto;
                        PdfEmbeddedFile embeddedFile;
                        if (encodeFilesImmediatelyCheckBox.Checked)
                        {
                            try
                            {
                                embeddedFile = new PdfEmbeddedFile(Document, filename, comression);
                            }
                            catch (OverflowException ex)
                            {
                                throw new Exception(string.Format("{0}\nDisable 'Encode Files Immediately' option.", ex.Message), ex);
                            }
                        }
                        else
                        {
                            embeddedFile = new PdfEmbeddedFile(Document, File.OpenRead(filename), true, comression, PdfCompressionSettings.DefaultSettings);
                        }

                        // create PDF file specification
                        PdfEmbeddedFileSpecification fileSpecification = new PdfEmbeddedFileSpecification(filename, embeddedFile);

                        // add file to PDF document
                        string name = Document.EmbeddedFiles.Add(fileSpecification);
                        _embeddedFiles.Add(fileSpecification, name);

                        // add embedded file to data grid
                        AddRow(fileSpecification);
                        embeddedFilesDataGridView.Refresh();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                Text = FORM_TITLE;
                // enable control buttons panel
                controlButtonsPanel.Enabled = true;
            }
        }

        /// <summary>
        /// Handles the SelectionChanged event of embeddedFilesDataGridView object.
        /// </summary>
        private void embeddedFilesDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            PdfEmbeddedFileSpecification fileSpecification = SelectedFileSpecification;
            if (fileSpecification != null)
            {
                _showingFileProperties = true;
                // update compression
                compressionComboBox.SelectedItem = GetCompression(fileSpecification);
                // update description
                descriptionTextBox.Text = fileSpecification.Description;
                // update modify date
                modifyDateLabel.Text = fileSpecification.EmbeddedFile.ModifyDate.ToString(DATE_TIME_FORMAT);
                // update create date
                createDateLabel.Text = fileSpecification.EmbeddedFile.CreationDate.ToString(DATE_TIME_FORMAT);
                _showingFileProperties = false;
            }
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of compressionComboBox object.
        /// </summary>
        private void compressionComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_showingFileProperties)
            {
                // get embedded file
                PdfEmbeddedFileSpecification fileSpecification = SelectedFileSpecification;
                try
                {
                    if (fileSpecification != null)
                    {
                        // update file compression
                        fileSpecification.EmbeddedFile.Compression = (PdfCompression)compressionComboBox.SelectedItem;
                        UpdateRowInformation(embeddedFilesDataGridView.SelectedRows[0]);
                    }
                }
                catch (Exception ex)
                {
                    DemosTools.ShowErrorMessage(ex);
                }
            }
        }

        /// <summary>
        /// Handles the TextChanged event of descriptionTextBox object.
        /// </summary>
        private void descriptionTextBox_TextChanged(object sender, EventArgs e)
        {
            if (!_showingFileProperties)
            {
                // get embedded file
                PdfEmbeddedFileSpecification fileSpecification = SelectedFileSpecification;
                if (fileSpecification != null)
                {
                    // update file description
                    fileSpecification.Description = descriptionTextBox.Text;
                }
            }
        }

        #endregion


        /// <summary>
        /// Adds information about embedded file into data grid.
        /// </summary>
        /// <param name="fileSpecification">The file specification.</param>
        private DataGridViewRow AddRow(PdfEmbeddedFileSpecification fileSpecification)
        {
            DataGridViewRow row = new DataGridViewRow();
            row.Tag = fileSpecification;
            row.Cells.Add(new DataGridViewTextBoxCell());
            row.Cells.Add(new DataGridViewTextBoxCell());
            row.Cells.Add(new DataGridViewTextBoxCell());
            UpdateRowInformation(row);
            embeddedFilesDataGridView.Rows.Add(row);
            return row;
        }

        /// <summary>
        /// Updates the information about embedded file in data grid.
        /// </summary>
        private void UpdateRowInformation(DataGridViewRow row)
        {
            PdfEmbeddedFileSpecification fileSpecification = (PdfEmbeddedFileSpecification)row.Tag;
            row.Cells[0].Value = fileSpecification.Filename;
            long compressedSize = fileSpecification.EmbeddedFile.Length;
            long uncompressedSize = fileSpecification.EmbeddedFile.UncompressedLength;
            if (uncompressedSize == 0)
            {
                row.Cells[1].Value = "N/A";
                row.Cells[2].Value = GetDataSize(compressedSize);
            }
            else
            {
                row.Cells[1].Value = GetDataSize(uncompressedSize);
                if (compressedSize > 0)
                {
                    double compressionPercent = (1 - ((double)compressedSize) / uncompressedSize) * 100.0;
                    row.Cells[2].Value = string.Format("{0} ({1:F2}%)", GetDataSize(compressedSize), compressionPercent);
                }
                else
                {
                    row.Cells[2].Value = "";
                }

            }
        }

        /// <summary>
        /// Returns a string with formatted size of the embedded file data.
        /// </summary>
        /// <param name="size">The size of the embedded file data.</param>
        /// <returns>A string with formatted size of the embedded file data.</returns>
        private string GetDataSize(long size)
        {
            if (size < 10000)
                return string.Format("{0} Bytes", size);
            return string.Format("{0} KB", Math.Round(size / 1024.0));
        }        

        /// <summary>
        /// Returns the compression of embedded file.
        /// </summary>
        private PdfCompression GetCompression(PdfEmbeddedFileSpecification fileSpecification)
        {
            PdfCompression compression = fileSpecification.EmbeddedFile.Compression;
            if (compressionComboBox.Items.Contains(compression))
                return compression;
            if ((compression & PdfCompression.Zip) != 0)
                return PdfCompression.Zip;
            if ((compression & PdfCompression.Lzw) != 0)
                return PdfCompression.Lzw;
            if ((compression & PdfCompression.RunLength) != 0)
                return PdfCompression.RunLength;
            return PdfCompression.None;
        }
       
        #endregion

    }
}
