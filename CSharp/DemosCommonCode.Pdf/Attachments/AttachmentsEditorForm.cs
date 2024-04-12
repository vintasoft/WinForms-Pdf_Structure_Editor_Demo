using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

using Vintasoft.Imaging;
using Vintasoft.Imaging.Codecs;
using Vintasoft.Imaging.Pdf;
using Vintasoft.Imaging.Pdf.Tree;
using Vintasoft.Imaging.Pdf.Tree.FileAttachments;

using DemosCommonCode.Imaging;
using DemosCommonCode.Imaging.Codecs;


namespace DemosCommonCode.Pdf
{
    /// <summary>
    /// A form that allows to view and edit PDF attachments (portfolio).
    /// </summary>
    public partial class AttachmentsEditorForm : Form
    {

        #region Fields

        /// <summary>
        /// The PDF document.
        /// </summary>
        PdfDocument _document;

        /// <summary>
        /// The action controller.
        /// </summary>
        StatusStripActionController _actionController;

        /// <summary>
        /// Indicates when UI is updating.
        /// </summary>
        bool _updatingUI = false;

        #endregion



        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AttachmentsEditorForm"/> class.
        /// </summary>
        public AttachmentsEditorForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AttachmentsEditorForm"/> class.
        /// </summary>
        /// <param name="document">The document.</param>
        public AttachmentsEditorForm(PdfDocument document)
            : this()
        {
            _actionController = new StatusStripActionController(statusStrip1, actionStatusLabel, actionProgressBar);

            if (document.Attachments == null)
            {
                document.CreateAttachments(true);
                document.Attachments.View = AttachmentCollectionViewMode.TileMode;
            }

            attachmentViewer.Document = document;
            viewModeComboBox.Items.Add(View.LargeIcon);
            viewModeComboBox.Items.Add(View.Tile);
            viewModeComboBox.Items.Add(View.Details);
            if (document.Attachments.View == AttachmentCollectionViewMode.DetailsMode)
                viewModeComboBox.SelectedItem = View.Details;
            else
                viewModeComboBox.SelectedItem = View.LargeIcon;

            initialViewModeToolStripComboBox.Items.Add(AttachmentCollectionViewMode.TileMode);
            initialViewModeToolStripComboBox.Items.Add(AttachmentCollectionViewMode.DetailsMode);
            initialViewModeToolStripComboBox.Items.Add(AttachmentCollectionViewMode.Hidden);
            initialViewModeToolStripComboBox.Items.Add(AttachmentCollectionViewMode.Custom);
            initialViewModeToolStripComboBox.SelectedItem = document.Attachments.View;

            fileCompressionToolStripComboBox.Items.Add(PdfCompression.None);
            fileCompressionToolStripComboBox.Items.Add(PdfCompression.Zip);
            fileCompressionToolStripComboBox.Items.Add(PdfCompression.Lzw);
            fileCompressionToolStripComboBox.SelectedItem = PdfCompression.None;

            encodeFilesImmediatelyToolStripMenuItem.Checked = true;

            _document = document;
            UpdateUI();
        }

        #endregion



        #region Properties

        /// <summary>
        /// Gets or sets a value indicating whether this editor is read only.
        /// </summary>
        /// <value>
        /// <b>true</b> - editor does NOT allow to edit attachments (portfolio);
        /// <b>false</b> - editor allows to edit attachments (portfolio).
        /// </value>
        [DefaultValue(false)]
        public bool IsReadOnly
        {
            get
            {
                return !addFilesToolStripButton.Visible;
            }
            set
            {
                bool isEditor = !value;
                addFilesToolStripButton.Visible = isEditor;
                addFilesToolStripMenuItem.Visible = isEditor;
                createNewFolderToolStripButton.Visible = isEditor;
                createNewFolderToolStripMenuItem.Visible = isEditor;
                addExistingFolderStripButton.Visible = isEditor;
                addExistingFolderStripMenuItem.Visible = isEditor;
                compressionOfNewFilesToolStripMenuItem.Visible = isEditor;
                initialViewModeToolStripMenuItem.Visible = isEditor;
                sortToolStripMenuItem.Visible = isEditor;
                colorsToolStripMenuItem.Visible = isEditor;
                resetColorsToolStripMenuItem.Visible = isEditor;
                setThumbnailForAllFoldersToolStripMenuItem.Visible = isEditor;
                setThumbnailForSelectedItemsToolStripMenuItem.Visible = isEditor;
                generateThumbnailsForAllFilesToolStripMenuItem.Visible = isEditor;
                generateThumbnailsForlSelectedFilesToolStripMenuItem.Visible = isEditor;
                editToolStripMenuItem.Visible = isEditor;
                schemaToolStripMenuItem.Visible = isEditor;
                attachmentViewer.LabelEdit = isEditor;
                toolStripSeparator5.Visible = isEditor;
                toolStripSeparator7.Visible = isEditor;
                toolStripSeparator9.Visible = isEditor;
                itemsPropertyGrid.Enabled = isEditor;
            }
        }

        #endregion



        #region Methods

        #region UI

        #region 'File' menu

        /// <summary>
        /// Handles the Click event of addFilesToolStripMenuItem object.
        /// </summary>
        private void addFilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFile = new OpenFileDialog())
            {
                openFile.Multiselect = true;
                openFile.FileName = "";
                openFile.Filter = "All Files (*.*)|*.*";
                openFile.FilterIndex = 0;
                // if file must be opened
                if (openFile.ShowDialog() == DialogResult.OK)
                {
                    List<PdfEmbeddedFileSpecification> addedFiles = new List<PdfEmbeddedFileSpecification>();
                    _actionController.StartAction("Add files", openFile.FileNames.Length);
                    foreach (string filename in openFile.FileNames)
                    {
                        // start action
                        _actionController.NextSubAction(Path.GetFileName(filename));
                        try
                        {
                            // add file
                            addedFiles.Add(attachmentViewer.AddFile(filename, (PdfCompression)fileCompressionToolStripComboBox.SelectedItem));
                        }
                        catch (OverflowException ex)
                        {
                            DemosTools.ShowErrorMessage(string.Format("{0}: {1}.\nDisable 'Encode Files Immediately' option in 'File' menu.", Path.GetFileName(filename), ex.Message));
                        }
                        catch (Exception ex)
                        {
                            DemosTools.ShowErrorMessage(string.Format("{0}: {1}", Path.GetFileName(filename), ex.Message));
                        }
                    }
                    _actionController.EndAction();
                    attachmentViewer.SetSelectedFiles(addedFiles.ToArray());
                    attachmentViewer.SetSelectedFolders(null);
                }
            }
        }
        /// <summary>
        /// Handles the CheckedChanged event of encodeFilesImmediatelyToolStripMenuItem object.
        /// </summary>
        private void encodeFilesImmediatelyToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            attachmentViewer.EncodeFileImmediately = encodeFilesImmediatelyToolStripMenuItem.Checked;
        }

        /// <summary>
        /// Handles the Click event of createNewFolderToolStripMenuItem object.
        /// </summary>
        private void createNewFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // create folder
            attachmentViewer.AddNewFolder("NewFolder");
        }

        /// <summary>
        /// Handles the Click event of importToolStripMenuItem object.
        /// </summary>
        private void importToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog openFolder = new FolderBrowserDialog())
            {
                // if file must be opened
                if (openFolder.ShowDialog() == DialogResult.OK)
                {
                    // start action
                    _actionController.StartAction("Add path");
                    // add file
                    attachmentViewer.AddPath(openFolder.SelectedPath, (PdfCompression)fileCompressionToolStripComboBox.SelectedItem, _actionController);
                    _actionController.EndAction();
                }
            }
        }

        /// <summary>
        /// Handles the Click event of saveSelectedFilesToToolStripMenuItem object.
        /// </summary>
        private void saveSelectedFilesToToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog openFolder = new FolderBrowserDialog())
            {
                // if file must be saved
                if (openFolder.ShowDialog() == DialogResult.OK)
                {
                    // start action
                    _actionController.StartAction("Save selected items");
                    // save selected files to specified path
                    bool result = attachmentViewer.SaveSelectionTo(openFolder.SelectedPath, _actionController);
                    _actionController.EndAction();
                    // if attachment files is saved
                    if (result)
                        MessageBox.Show("Item(s) saved successfully.");
                    else
                        MessageBox.Show("Item(s) does not saved.");
                }
            }
        }

        /// <summary>
        /// Handles the Click event of closeToolStripMenuItem object.
        /// </summary>
        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        #endregion


        #region 'View' menu

        /// <summary>
        /// Handles the SelectedIndexChanged event of viewModeComboBox object.
        /// </summary>
        private void viewModeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            attachmentViewer.BeginUpdate();
            // update view mode of attachment viewer
            attachmentViewer.View = (View)viewModeComboBox.SelectedItem;
            ColumnsAutoResize();
            attachmentViewer.EndUpdate();
        }

        /// <summary>
        /// Handles the Click event of iconViewModeToolStripButton object.
        /// </summary>
        private void iconViewModeToolStripButton_Click(object sender, EventArgs e)
        {
            // set attachment viewer view mode to large icons mode
            viewModeComboBox.SelectedItem = View.LargeIcon;
            UpdateUI();
        }

        /// <summary>
        /// Handles the Click event of detailViewModeToolStripButton object.
        /// </summary>
        private void detailViewModeToolStripButton_Click(object sender, EventArgs e)
        {
            // sets attachment viewer view mode to details (table) mode
            viewModeComboBox.SelectedItem = View.Details;
            UpdateUI();
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of initialViewModeToolStripComboBox object.
        /// </summary>
        private void initialViewModeToolStripComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_document != null)
                // change initial view mode of attachments
                _document.Attachments.View = (AttachmentCollectionViewMode)initialViewModeToolStripComboBox.SelectedItem;
        }

        /// <summary>
        /// Handles the TextChanged event of sortFieldNameToolStripComboBox object.
        /// </summary>
        private void sortFieldNameToolStripComboBox_TextChanged(object sender, EventArgs e)
        {
            if (!_updatingUI)
            {
                if (_document.Attachments.Sort == null)
                    // set the attachments sort parameters
                    _document.Attachments.Sort = new PdfAttachmentCollectionSort(_document);

                // if attachments can not be sorted
                if (sortFieldNameToolStripComboBox.Text == "")
                    _document.Attachments.Sort.FieldNames = null;
                else
                    _document.Attachments.Sort.FieldNames = new string[] { sortFieldNameToolStripComboBox.Text };
                // update current folder
                attachmentViewer.UpdateCurrentFolder();
            }
        }

        /// <summary>
        /// Handles the CheckedChanged event of ascendingOrderToolStripMenuItem object.
        /// </summary>
        private void ascendingOrderToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            if (!_updatingUI)
            {
                if (_document.Attachments.Sort == null)
                    // set the attachments sort parameters
                    _document.Attachments.Sort = new PdfAttachmentCollectionSort(_document);

                // change sort ascending order
                _document.Attachments.Sort.AscendingOrders = new bool[] { ascendingOrderToolStripMenuItem.Checked };
                attachmentViewer.UpdateCurrentFolder();
            }
        }

        /// <summary>
        /// Handles the Click event of removeSortInformationToolStripMenuItem object.
        /// </summary>
        private void removeSortInformationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // if sort information must be removed
            if (MessageBox.Show(
                "Do you want to remove sort information, which specifies the order" +
                " in which items in the attachment collection should be sorted" +
                " in the user interface?", "",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.Yes)
            {
                // remove sort information
                _document.Attachments.Sort = null;
                UpdateUI();
            }
        }

        /// <summary>
        /// Handles the Click event of levelUpToolStripMenuItem object.
        /// </summary>
        private void levelUpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // navigate to up level (parent folder)
            attachmentViewer.CurrentFolder = attachmentViewer.CurrentFolder.Parent;
        }

        /// <summary>
        /// Handles the Click event of moveToRootToolStripMenuItem object.
        /// </summary>
        private void moveToRootToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // navigate to root folder
            attachmentViewer.CurrentFolder = attachmentViewer.RootFolder;
        }

        /// <summary>
        /// Handles the Click event of colorsToolStripMenuItem object.
        /// </summary>
        private void colorsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_document.Attachments.Colors == null)
            {
                // if attachment colors must be created
                if (MessageBox.Show(
                    "Colors are not specified. Do you want to create information about colors?",
                    "",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) == DialogResult.Yes)
                    // create attachment colors
                    _document.Attachments.Colors = new PdfPresentationColors(_document);
            }
            if (_document.Attachments.Colors != null)
            {
                // create property grid
                using (PropertyGridForm dialog = new PropertyGridForm(
                    _document.Attachments.Colors,
                    "Portfolio Colors"))
                {
                    // show dialog
                    dialog.ShowDialog();
                    // update colors
                    attachmentViewer.UpdateColors();
                }
            }
        }

        /// <summary>
        /// Handles the Click event of resetColorsToolStripMenuItem object.
        /// </summary>
        private void resetColorsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // if attachment colors must be deleted
            if (MessageBox.Show(
                "Do you want to delete information about portfolio colors?",
                "",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.Yes)
            {
                // remove attachment colors
                _document.Attachments.Colors = null;
                attachmentViewer.UpdateColors();
                UpdateUI();
            }
        }

        /// <summary>
        /// Handles the Click event of generateThumbnailsForAllFilesToolStripMenuItem object.
        /// </summary>
        private void generateThumbnailsForAllFilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // if thumbnails must be created
            if (MessageBox.Show("Do you want to generate thumbnails for all files?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                // start action
                _actionController.StartAction("Generate thumbnails");
                // if attachment root folder specified
                if (_document.Attachments.RootFolder != null)
                {
                    // get folder names
                    string[] folderFullNames = _document.Attachments.GetFolderFullNames();
                    foreach (string folderFullName in folderFullNames)
                        // generate thumbnails
                        GenerateThumbnails(_document.Attachments.GetFiles(folderFullName));
                }
                else
                {
                    // generate thumbnails
                    GenerateThumbnails(_document.Attachments.GetFiles(""));
                }
                // end action
                _actionController.EndAction();
                attachmentViewer.UpdateFileView(attachmentViewer.CurrentFolder.Files);
            }
        }

        /// <summary>
        /// Handles the Click event of generateThumbnailsForSelectedFilesToolStripMenuItem object.
        /// </summary>
        private void generateThumbnailsForSelectedFilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // get selected folders
            PdfAttachmentFolder[] folders = attachmentViewer.GetSelectedFolders();
            // get selected files
            PdfEmbeddedFileSpecification[] files = attachmentViewer.GetSelectedFiles();

            // if folders and files is not selected
            if (files.Length == 0 && folders.Length == 0)
                return;

            if (files.Length > 0 && folders.Length > 0)
            {
                if (MessageBox.Show("Do you want to generate thumbnails for selected files and files in all selected folders?",
                    "",
                    MessageBoxButtons.YesNo) == DialogResult.No)
                    return;
            }
            else if (files.Length > 0)
            {
                if (MessageBox.Show("Do you want to generate thumbnails for selected files?", "",
                    MessageBoxButtons.YesNo) == DialogResult.No)
                    return;
            }
            else if (folders.Length > 0)
            {
                if (MessageBox.Show("Do you want to generate thumbnails for files in all selected folders?", "",
                    MessageBoxButtons.YesNo) == DialogResult.No)
                    return;
            }

            // start action
            _actionController.StartAction("Generate thumbnails");
            // generate thumbnails
            GenerateThumbnails(files);
            // for each folder in folders
            foreach (PdfAttachmentFolder folder in folders)
            {
                // get folder name
                string folderFullName = _document.Attachments.GetFolderFullName(folder);
                // generate thumbnail
                GenerateThumbnails(_document.Attachments.GetFiles(folderFullName));
                // get sub folders
                string[] subFolderFullNames = _document.Attachments.GetSubFolderFullNames(folderFullName, false);
                foreach (string subFolderFullName in subFolderFullNames)
                    // generate thumbnails
                    GenerateThumbnails(_document.Attachments.GetFiles(subFolderFullName));
            }
            // end action
            _actionController.EndAction();

            attachmentViewer.UpdateFileView(files);
        }

        /// <summary>
        /// Handles the Click event of setThumbnailForAllFoldersToolStripMenuItem object.
        /// </summary>
        private void setThumbnailForAllFoldersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                // if root folder is specified
                if (_document.Attachments.RootFolder != null)
                {
                    using (OpenFileDialog openImageFile = new OpenFileDialog())
                    {
                        openImageFile.Title = "Open thumbnail image";
                        // set file dialog filters
                        CodecsFileFilters.SetOpenFileDialogFilter(openImageFile);
                        // if thumbnail image is selected
                        if (openImageFile.ShowDialog() == DialogResult.OK)
                        {
                            _actionController.StartAction("Set thumbnail");
                            // open thumbnail image
                            using (VintasoftImage image = new VintasoftImage(openImageFile.FileName))
                            {
                                // create PDF image resource
                                PdfImageResource thumbnailResource = CreateThumbnailImageResource(image);
                                // set thumbnails
                                SetThumbnailRecursive(_document.Attachments.RootFolder, thumbnailResource);
                            }
                            _actionController.EndAction();
                            // update current folder view
                            attachmentViewer.UpdateFolderView(attachmentViewer.CurrentFolder.Folders);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                DemosTools.ShowErrorMessage(ex);
            }
        }

        /// <summary>
        /// Handles the Click event of setThumbnailForSelectedItemsToolStripMenuItem object.
        /// </summary>
        private void setThumbnailForSelectedItemsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // get selected folders
            PdfAttachmentFolder[] folders = attachmentViewer.GetSelectedFolders();
            // get selected files
            PdfEmbeddedFileSpecification[] files = attachmentViewer.GetSelectedFiles();

            // if folders or files is selected
            if (folders.Length > 0 || files.Length > 0)
            {
                using (OpenFileDialog openImageFile = new OpenFileDialog())
                {
                    openImageFile.Title = "Open thumbnail image";
                    // set file dialog filters
                    CodecsFileFilters.SetOpenFileDialogFilter(openImageFile);
                    // if thumbnail image is selected
                    if (openImageFile.ShowDialog() == DialogResult.OK)
                    {
                        _actionController.StartAction("Set thumbnail");
                        // open thumbnail image
                        using (VintasoftImage image = new VintasoftImage(openImageFile.FileName))
                        {
                            // create PDF image resource
                            PdfImageResource thumbnailResource = CreateThumbnailImageResource(image);

                            // for each folder in selected folders
                            foreach (PdfAttachmentFolder folder in folders)
                                // update thumbnail
                                folder.Thumbnail = thumbnailResource;

                            // for each file in selected files
                            foreach (PdfEmbeddedFileSpecification file in files)
                                // update thumbnail
                                file.Thumbnail = thumbnailResource;

                            // update folders view
                            attachmentViewer.UpdateFolderView(folders);
                            // update files view
                            attachmentViewer.UpdateFileView(files);
                        }
                        _actionController.EndAction();
                    }
                }
            }
        }

        #endregion


        #region 'Edit' menu

        /// <summary>
        /// Handles the Click event of deleteSelectedToolStripMenuItem object.
        /// </summary>
        private void deleteSelectedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // delete selected files and folders in current folder
            attachmentViewer.DeleteSelectedItems();
        }

        /// <summary>
        /// Handles the Click event of selectAllToolStripMenuItem object.
        /// </summary>
        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // select all files in current folder
            attachmentViewer.SetSelectedFiles(attachmentViewer.FilesInCurrentFolder);
            // select all sub folders in current folder
            attachmentViewer.SetSelectedFolders(attachmentViewer.FoldersInCurrentFolder);
            attachmentViewer.Focus();
        }

        #endregion


        #region 'Schema' menu

        /// <summary>
        /// Handles the Click event of schemaEditorToolStripMenuItem object.
        /// </summary>
        private void schemaEditorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_document.Attachments.Schema == null)
            {
                // if attachment schema must be created
                if (MessageBox.Show("Attachments does not have schema. Do you want to create schema?",
                    "", 
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) == DialogResult.No)
                    return;
                // create attachment schema
                _document.Attachments.Schema = new PdfAttachmentCollectionSchema(_document);
            }
            PdfNamedDictionaryItemSet<PdfAttachmentCollectionSchemaField> itemSet =
                    new PdfNamedDictionaryItemSet<PdfAttachmentCollectionSchemaField>(
                        _document.Attachments.Schema, AddNewAttachmentsSchemaField);
            // create item set editor form
            using (ItemSetEditorForm editorForm = new ItemSetEditorForm(itemSet))
            {
                editorForm.Text = "Attachments Schema Editor";
                editorForm.ShowDialog();

                attachmentViewer.BeginUpdate();
                try
                {
                    // update attachment schema
                    attachmentViewer.UpdateSchema();
                    // update field view
                    attachmentViewer.UpdateFileView(attachmentViewer.FilesInCurrentFolder);
                }
                finally
                {
                    attachmentViewer.EndUpdate();
                }
            }
        }

        /// <summary>
        /// Handles the Click event of selectedFileDataFieldsToolStripMenuItem object.
        /// </summary>
        private void selectedFileDataFieldsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (attachmentViewer.GetSelectedFiles().Length == 1)
            {
                // get selected files
                PdfEmbeddedFileSpecification selectedFile = attachmentViewer.GetSelectedFiles()[0];
                if (selectedFile.DataFields == null)
                {
                    // if the data fields can not be created
                    if (MessageBox.Show(string.Format("File '{0}' does not have data fields. Do you want to create data fields?", Path.GetFileName(selectedFile.Filename)), "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                        return;
                    // create data fields
                    selectedFile.DataFields = new PdfAttachmentDataFieldCollection(_document);
                }
                PdfNamedDictionaryItemSet<PdfAttachmentDataField> itemSet =
                    new PdfNamedDictionaryItemSet<PdfAttachmentDataField>(selectedFile.DataFields, AddNewAttachmentDataField);
                using (ItemSetEditorForm editorForm = new ItemSetEditorForm(itemSet))
                {
                    editorForm.Text = "Embedded File Data Fields Editor";
                    editorForm.ShowDialog();

                    if (string.IsNullOrEmpty(sortFieldNameToolStripComboBox.Text))
                        // update selected file view
                        attachmentViewer.UpdateFileView(selectedFile);
                    else
                        // update current folder
                        attachmentViewer.UpdateCurrentFolder();
                }
            }
            else if (attachmentViewer.GetSelectedFolders().Length == 1)
            {
                // get selected folders
                PdfAttachmentFolder selectedFolder = attachmentViewer.GetSelectedFolders()[0];
                if (selectedFolder.DataFields == null)
                {
                    // if the data fields can not be created
                    if (MessageBox.Show(string.Format("Folder '{0}' does not have data fields. Do you want to create data fields?", selectedFolder.Name), "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                        return;
                    // create data fields
                    selectedFolder.DataFields = new PdfAttachmentDataFieldCollection(_document);
                }
                PdfNamedDictionaryItemSet<PdfAttachmentDataField> itemSet =
                    new PdfNamedDictionaryItemSet<PdfAttachmentDataField>(selectedFolder.DataFields, AddNewAttachmentDataField);

                using (ItemSetEditorForm editorForm = new ItemSetEditorForm(itemSet))
                {
                    editorForm.Text = "Attachment Folder Data Fields Editor";
                    editorForm.ShowDialog();

                    if (string.IsNullOrEmpty(sortFieldNameToolStripComboBox.Text))
                        // update selected file view
                        attachmentViewer.UpdateFolderView(selectedFolder);
                    else
                        // update current folder
                        attachmentViewer.UpdateCurrentFolder();
                }
            }
        }

        #endregion


        #region Attachment viewer

        /// <summary>
        /// Handles the ItemActivate event of attachmentViewer object.
        /// </summary>
        private void attachmentViewer_ItemActivate(object sender, EventArgs e)
        {
            PdfAttachmentFolder[] selectedFolders = attachmentViewer.GetSelectedFolders();
            // if folder activated
            if (selectedFolders.Length > 0)
            {
                // change current folder
                attachmentViewer.CurrentFolder = selectedFolders[0];
                return;
            }

            PdfEmbeddedFileSpecification[] files = attachmentViewer.GetSelectedFiles();
            // if file activated
            if (files.Length > 0)
            {
                // open file
                try
                {
                    PdfEmbeddedFileSpecification fileSpec = files[0];
                    // if embedded file selected
                    if (fileSpec.EmbeddedFile != null)
                    {
                        DialogResult result = MessageBox.Show(string.Format(
                            "Open file '{0}' using the default program, or save file?\n\n" +
                            "Press 'Yes' to open file using the default program.\n" +
                            "Press 'No' to save file to disk.", 
                            fileSpec.Filename), "", MessageBoxButtons.YesNoCancel);
                        if (result == DialogResult.Cancel)
                            return;
                        if (result == DialogResult.Yes)
                        {
                            // get path to current application
                            string path = Path.GetDirectoryName(Application.ExecutablePath);
                            string filename = Path.Combine(path, fileSpec.Filename);
                            // save embedded file
                            fileSpec.EmbeddedFile.Save(filename);

                            ProcessStartInfo processInfo = new ProcessStartInfo(filename);
                            processInfo.UseShellExecute = true;
                            Process process = Process.Start(processInfo);
                            if (process != null)
                            {
                                try
                                {
                                    process.WaitForExit();
                                    File.Delete(filename);
                                }
                                catch
                                {
                                }
                            }
                        }
                        else
                        {
                            // create save dialog
                            using (SaveFileDialog saveDialog = new SaveFileDialog())
                            {
                                saveDialog.FileName = fileSpec.Filename;
                                // if embedded file must be saved
                                if (saveDialog.ShowDialog() == DialogResult.OK)
                                    fileSpec.EmbeddedFile.Save(saveDialog.FileName);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    DemosTools.ShowErrorMessage(ex);
                }
            }
        }

        /// <summary>
        /// Handles the CurrentFolderChanged event of attachmentViewer object.
        /// </summary>
        private void attachmentViewer_CurrentFolderChanged(object sender, EventArgs e)
        {
            UpdateUI();
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of attachmentViewer object.
        /// </summary>
        private void attachmentViewer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (attachmentViewer.SelectedIndices.Count == 1)
            {
                // get selected files
                PdfEmbeddedFileSpecification[] files = attachmentViewer.GetSelectedFiles();
                if (files.Length > 0)
                {
                    itemsPropertyGrid.SelectedObject = files[0];
                }
                else
                {
                    // get selected folders
                    PdfAttachmentFolder[] folders = attachmentViewer.GetSelectedFolders();
                    if (folders.Length > 0)
                        itemsPropertyGrid.SelectedObject = folders[0];
                }
            }
            else
            {
                // remove selected items
                itemsPropertyGrid.SelectedObject = null;
            }
            UpdateUI();
        }

        /// <summary>
        /// Handles the ItemRenamed event of attachmentViewer object.
        /// </summary>
        private void attachmentViewer_ItemRenamed(object sender, EventArgs e)
        {
            // update selected object
            itemsPropertyGrid.SelectedObject = itemsPropertyGrid.SelectedObject;
        }

        #endregion


        #region Items property grid

        /// <summary>
        /// Handles the PropertyValueChanged event of itemsPropertyGrid object.
        /// </summary>
        private void itemsPropertyGrid_PropertyValueChanged(object sender, PropertyValueChangedEventArgs e)
        {
            if (itemsPropertyGrid.SelectedObject != null)
            {
                // if selected is attachment folder
                if (itemsPropertyGrid.SelectedObject is PdfAttachmentFolder)
                    attachmentViewer.UpdateFolderView((PdfAttachmentFolder)itemsPropertyGrid.SelectedObject);
                else
                    attachmentViewer.UpdateFileView((PdfEmbeddedFileSpecification)itemsPropertyGrid.SelectedObject);
            }
        }

        #endregion 

        #endregion


        /// <summary>
        /// Updates the User Interface.
        /// </summary>
        private void UpdateUI()
        {
            _updatingUI = true;
            try
            {
                if (_document != null)
                {
                    bool canLevelUp = attachmentViewer.CurrentFolder != null && attachmentViewer.CurrentFolder.Parent != null;
                    bool canMoveToRoot = attachmentViewer.RootFolder != attachmentViewer.CurrentFolder;
                    levelUpToolStripButton.Enabled = canLevelUp;
                    levelUpToolStripMenuItem.Enabled = canLevelUp;
                    moveToRootToolStripMenuItem.Enabled = canMoveToRoot;
                    moveToRootToolStripButton.Enabled = canMoveToRoot;

                    detailViewModeToolStripButton.Enabled = attachmentViewer.View != View.Details;
                    iconViewModeToolStripButton.Enabled = attachmentViewer.View != View.LargeIcon;

                    bool isItemsSelected = attachmentViewer.SelectedItems.Count > 0;
                    saveSelectedFilesToToolStripMenuItem.Enabled = isItemsSelected;
                    saveSelectedItemsToolStripButton.Enabled = isItemsSelected;
                    deleteSelectedToolStripButton.Enabled = isItemsSelected;
                    deleteSelectedToolStripMenuItem.Enabled = isItemsSelected;

                    selectedFileDataFieldsToolStripMenuItem.Enabled = attachmentViewer.SelectedItems.Count == 1;

                    resetColorsToolStripMenuItem.Enabled = _document.Attachments.Colors != null;

                    removeSortInformationToolStripMenuItem.Enabled = _document.Attachments.Sort != null;

                    generateThumbnailsForlSelectedFilesToolStripMenuItem.Enabled = attachmentViewer.GetSelectedFiles().Length > 0;
                    setThumbnailForSelectedItemsToolStripMenuItem.Enabled = attachmentViewer.SelectedItems.Count > 0;

                    sortFieldNameToolStripComboBox.Items.Clear();
                    if (_document.Attachments.Schema != null)
                    {
                        foreach (string fieldName in _document.Attachments.Schema.Keys)
                            sortFieldNameToolStripComboBox.Items.Add(fieldName);
                    }
                    sortFieldNameToolStripComboBox.SelectedText = "";
                    ascendingOrderToolStripMenuItem.Checked = true;
                    if (_document.Attachments.Sort != null)
                    {
                        string[] sortFieldNames = _document.Attachments.Sort.FieldNames;
                        if (sortFieldNames != null && sortFieldNames.Length > 0)
                            sortFieldNameToolStripComboBox.Text = sortFieldNames[0];
                        bool[] ascendingOrders = _document.Attachments.Sort.AscendingOrders;
                        if (ascendingOrders != null && ascendingOrders.Length > 0)
                            ascendingOrderToolStripMenuItem.Checked = ascendingOrders[0];
                    }
                }
            }
            finally
            {
                _updatingUI = false;
            }
        }

        /// <summary>
        /// Resizes columns automatically.
        /// </summary>
        private void ColumnsAutoResize()
        {
            if (attachmentViewer.View == View.Details || attachmentViewer.View == View.Tile)
            {
                for (int i = 0; i < attachmentViewer.Columns.Count; i++)
                {
                    ColumnHeader columnHeader = attachmentViewer.Columns[i];
                    columnHeader.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
                    if (columnHeader.Width < 100)
                        columnHeader.Width = 100;
                }
            }
        }


        /// <summary>
        /// Sets the thumbnail for specified folder (recursive).
        /// </summary>
        /// <param name="folder">The folder.</param>
        /// <param name="thumbnailResource">The thumbnail resource.</param>
        private void SetThumbnailRecursive(PdfAttachmentFolder folder, PdfImageResource thumbnailResource)
        {
            folder.Thumbnail = thumbnailResource;
            PdfAttachmentFolder[] subFolders = folder.Folders;
            if (subFolders != null)
            {
                foreach (PdfAttachmentFolder subFolder in subFolders)
                    SetThumbnailRecursive(subFolder, thumbnailResource);
            }
        }

        /// <summary>
        /// Creates the thumbnail of image resource.
        /// </summary>
        /// <param name="image">The image.</param>
        /// <returns>Thumbnail of image resource.</returns>
        private PdfImageResource CreateThumbnailImageResource(VintasoftImage image)
        {
            using (VintasoftImage thumbnail = image.Thumbnail.GetThumbnailImage(100, 100))
            {
                PdfCompressionSettings compressionSettings = new PdfCompressionSettings();
                compressionSettings.JpegQuality = 90;
                return new PdfImageResource(_document, thumbnail, PdfCompression.Jpeg, compressionSettings);
            }
        }

        /// <summary>
        /// Generates the thumbnails for specified embedded files.
        /// </summary>
        /// <param name="fileSpecifications">The file specifications.</param>
        private void GenerateThumbnails(params PdfEmbeddedFileSpecification[] fileSpecifications)
        {
            if (fileSpecifications != null)
            {
                foreach (PdfEmbeddedFileSpecification fileSpecification in fileSpecifications)
                {
                    _actionController.NextSubAction(fileSpecification.Filename);
                    try
                    {
                        if (fileSpecification.EmbeddedFile != null)
                        {
                            Codec codec = AvailableCodecs.GetCodecByExtension(Path.GetExtension(fileSpecification.Filename));
                            if (codec != null && codec.CanCreateDecoder)
                            {
                                using (Stream imageDataStream = fileSpecification.EmbeddedFile.GetAsStream())
                                {
                                    try
                                    {
                                        using (VintasoftImage image = new VintasoftImage(imageDataStream, false))
                                            fileSpecification.Thumbnail = CreateThumbnailImageResource(image);
                                    }
                                    catch
                                    {
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        DemosTools.ShowErrorMessage(ex);
                    }
                }
            }
        }


        /// <summary>
        /// Adds the new attachments shema field.
        /// </summary>
        /// <param name="dictionary">The dictionary to which attachments schema field must be added.</param>
        /// <returns>Name of shema field.</returns>
        private string AddNewAttachmentsSchemaField(
            PdfNamedDictionary<PdfAttachmentCollectionSchemaField> dictionary)
        {
            string name = null;
            PdfAttachmentCollectionSchemaField field = PdfAttachmentSchemaFieldFactoryForm.CreateSchemaField(_document, out name);
            if (field != null)
            {
                field.Order = _document.Attachments.Schema.GetMaxOrder() + 1;
                try
                {
                    dictionary.Add(name, field);
                    return name;
                }
                catch (Exception ex)
                {
                    DemosTools.ShowErrorMessage(ex);
                }
            }
            return null;
        }

        /// <summary>
        /// Adds the new attachment data field.
        /// </summary>
        /// <param name="dictionary">The dictionary to which attachment data field must be added.</param>
        /// <returns>Field name.</returns>
        private string AddNewAttachmentDataField(PdfNamedDictionary<PdfAttachmentDataField> dictionary)
        {
            string name = null;
            PdfAttachmentDataField field = PdfAttachmentDataFieldFactoryForm.CreateDataField(_document, out name);
            if (field != null)
            {
                try
                {
                    dictionary.Add(name, field);
                    return name;
                }
                catch (Exception ex)
                {
                    DemosTools.ShowErrorMessage(ex);
                }
            }
            return null;
        }

        #endregion

    }
}
