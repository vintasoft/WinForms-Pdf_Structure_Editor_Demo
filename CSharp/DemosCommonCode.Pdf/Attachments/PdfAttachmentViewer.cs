using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

using Vintasoft.Imaging;
using Vintasoft.Imaging.ImageProcessing;
using Vintasoft.Imaging.Pdf;
using Vintasoft.Imaging.Pdf.Tree;
using Vintasoft.Imaging.Pdf.Tree.FileAttachments;

namespace DemosCommonCode.Pdf
{
    /// <summary>
    /// Represents the PDF attachment viewer.
    /// </summary>
    public partial class PdfAttachmentViewer : ListView
    {

        #region Constants

        /// <summary>
        /// The count of standard icons.
        /// </summary>
        const int STANDARD_ICON_COUNT = 4; 

        #endregion



        #region Fields

        /// <summary>
        /// The dictionary that provides a mapping from list items to PDF attachment folders.
        /// </summary>
        Dictionary<ListViewItem, PdfAttachmentFolder> _listItemToFolder = new Dictionary<ListViewItem, PdfAttachmentFolder>();

        /// <summary>
        /// The dictionary that provides a mapping from PDF attachment folders to list items.
        /// </summary>
        Dictionary<PdfAttachmentFolder, ListViewItem> _folderToListItem = new Dictionary<PdfAttachmentFolder, ListViewItem>();

        /// <summary>
        /// The dictionary that provides a mapping from list items to PDF embedded files.
        /// </summary>
        Dictionary<ListViewItem, PdfEmbeddedFileSpecification> _listItemToFile = new Dictionary<ListViewItem, PdfEmbeddedFileSpecification>();

        /// <summary>
        /// The dictionary that provides a mapping from PDF embedded files to list items.
        /// </summary>
        Dictionary<PdfEmbeddedFileSpecification, ListViewItem> _fileToListItem = new Dictionary<PdfEmbeddedFileSpecification, ListViewItem>();

        /// <summary>
        /// Determines that schema from PDF document must be used for
        /// displaying properties of embedded files.
        /// </summary>
        bool _useSchema;

        #endregion



        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PdfAttachmentViewer"/> class.
        /// </summary>
        public PdfAttachmentViewer()
            : base()
        {
            InitializeComponent();

            // create large image list
            LargeImageList = new ImageList();
            LargeImageList.ImageSize = new Size(100, 100);
            LargeImageList.ColorDepth = ColorDepth.Depth32Bit;
            if (!IsDesignMode)
            {
                // add standard icons
                using (VintasoftImage image = DemosResourcesManager.GetResourceAsImage("Icon_EmptyFolder_100x100.png"))
                {
                    AddImage(LargeImageList, image);
                }
                using (VintasoftImage image = DemosResourcesManager.GetResourceAsImage("Icon_Folder_100x100.png"))
                {
                    AddImage(LargeImageList, image);
                }
                using (VintasoftImage image = DemosResourcesManager.GetResourceAsImage("Icon_PDF_100x100.png"))
                {
                    AddImage(LargeImageList, image);
                }
                using (VintasoftImage image = DemosResourcesManager.GetResourceAsImage("Icon_File_100x100.png"))
                {
                    AddImage(LargeImageList, image);
                }
            }

            // create small image list
            SmallImageList = new ImageList();
            SmallImageList.ImageSize = new Size(40, 40);
            SmallImageList.ColorDepth = ColorDepth.Depth32Bit;
            if (!IsDesignMode)
            {
                // add standard icons
                using (VintasoftImage image = DemosResourcesManager.GetResourceAsImage("Icon_EmptyFolder_40x40.png"))
                {
                    AddImage(SmallImageList, image);
                }
                using (VintasoftImage image = DemosResourcesManager.GetResourceAsImage("Icon_Folder_40x40.png"))
                {
                    AddImage(SmallImageList, image);
                }
                using (VintasoftImage image = DemosResourcesManager.GetResourceAsImage("Icon_PDF_40x40.png"))
                {
                    AddImage(SmallImageList, image);
                }
                using (VintasoftImage image = DemosResourcesManager.GetResourceAsImage("Icon_File_40x40.png"))
                {
                    AddImage(SmallImageList, image);
                }
            }
        }

        #endregion



        #region Properties

        bool _encodeFileImmediately = true;
        /// <summary>
        /// Gets or sets a value indicating whether added file must be encoded immediately and stored in memory.
        /// </summary>
        /// <value>
        /// <b>True</b> - added file must be encoded immediately and stored in memory, file stream will be closed after file encoding;<br />
        /// <b>false</b> - added file must be encoded when document is saved or packed, file stream will be closed after document saving or packing.<br />
        /// Default value is <b>true</b>.
        /// </value>
        [Browsable(false)]
        public bool EncodeFileImmediately
        {
            get
            {
                return _encodeFileImmediately;
            }
            set
            {
                _encodeFileImmediately = value;
            }
        }

        /// <summary>
        /// Gets an array that contains files in current folder.
        /// </summary>
        [Browsable(false)]
        public PdfEmbeddedFileSpecification[] FilesInCurrentFolder
        {
            get
            {
                if (CurrentFolder == null)
                    return _document.Attachments.GetFiles("");
                return CurrentFolder.Files;
            }
        }

        /// <summary>
        /// Gets an array that contains sub folders of current folder.
        /// </summary>
        [Browsable(false)]
        public PdfAttachmentFolder[] FoldersInCurrentFolder
        {
            get
            {
                if (CurrentFolder == null)
                    return null;
                return CurrentFolder.Folders;
            }
        }

        PdfDocument _document;
        /// <summary>
        /// Gets or sets the PDF document.
        /// </summary>
        [Browsable(false)]
        public PdfDocument Document
        {
            get
            {
                return _document;
            }
            set
            {
                _document = value;
                ResetUI();
            }
        }

        /// <summary>
        /// Gets the root folder of attachment collection.
        /// </summary>
        [Browsable(false)]
        public PdfAttachmentFolder RootFolder
        {
            get
            {
                if (_document == null)
                    return null;
                if (_document.Attachments == null)
                    return null;
                return _document.Attachments.RootFolder;
            }
        }

        PdfAttachmentFolder _currentFolder;
        /// <summary>
        /// Gets or sets the current folder of attachment collection.
        /// </summary>
        [Browsable(false)]
        public PdfAttachmentFolder CurrentFolder
        {
            get
            {
                return _currentFolder;
            }
            set
            {
                _currentFolder = value;
                UpdateCurrentFolder();
            }
        }


        /// <summary>
        /// Gets a value indicating whether the control is used in Design mode.
        /// </summary>
        private bool IsDesignMode
        {
            get
            {
                return ImagingEnvironment.IsInDesignMode;
            }
        }
        
        #endregion



        #region Methods

        #region PUBLIC

        /// <summary>
        /// Returns the selected embedded files.
        /// </summary>
        public PdfEmbeddedFileSpecification[] GetSelectedFiles()
        {
            List<PdfEmbeddedFileSpecification> files = new List<PdfEmbeddedFileSpecification>();
            for (int i = 0; i < SelectedItems.Count; i++)
            {
                if (_listItemToFile.ContainsKey(SelectedItems[i]))
                    files.Add(_listItemToFile[SelectedItems[i]]);
            }
            return files.ToArray();
        }

        /// <summary>
        /// Sets the selected embedded files.
        /// </summary>
        /// <param name="files">The files.</param>
        public void SetSelectedFiles(PdfEmbeddedFileSpecification[] files)
        {
            int i = 0;
            while (i < SelectedItems.Count)
            {
                if (_listItemToFile.ContainsKey(SelectedItems[i]))
                    SelectedIndices.Remove(SelectedIndices[i]);
                else
                    i++;
            }
            if (files != null && files.Length > 0)
            {
                foreach (ListViewItem item in _listItemToFile.Keys)
                {
                    if (Array.IndexOf(files, _listItemToFile[item]) >= 0)
                        SelectedIndices.Add(Items.IndexOf(item));
                }
                EnsureVisible(Items.IndexOf(_fileToListItem[files[0]]));
            }
        }

        /// <summary>
        /// Returns the selected folders.
        /// </summary>
        public PdfAttachmentFolder[] GetSelectedFolders()
        {
            List<PdfAttachmentFolder> folders = new List<PdfAttachmentFolder>();
            for (int i = 0; i < SelectedItems.Count; i++)
            {
                if (_listItemToFolder.ContainsKey(SelectedItems[i]))
                    folders.Add(_listItemToFolder[SelectedItems[i]]);
            }
            return folders.ToArray();
        }

        /// <summary>
        /// Sets the selected folders.
        /// </summary>
        /// <param name="folders">The folders.</param>
        public void SetSelectedFolders(PdfAttachmentFolder[] folders)
        {
            int i = 0;
            while (i < SelectedItems.Count)
            {
                if (_listItemToFolder.ContainsKey(SelectedItems[i]))
                    SelectedIndices.Remove(SelectedIndices[i]);
                else
                    i++;
            }
            if (folders != null && folders.Length > 0)
            {
                foreach (ListViewItem item in _listItemToFolder.Keys)
                {
                    if (Array.IndexOf(folders, _listItemToFolder[item]) >= 0)
                        SelectedIndices.Add(Items.IndexOf(item));
                }
                EnsureVisible(Items.IndexOf(_folderToListItem[folders[0]]));
            }
        }

        /// <summary>
        /// Updates the current folder view.
        /// </summary>
        public void UpdateCurrentFolder()
        {
            if (_document == null)
                return;

            BeginUpdate();
            try
            {
                _folderToListItem.Clear();
                _fileToListItem.Clear();
                _listItemToFile.Clear();
                _listItemToFolder.Clear();
                PdfAttachmentFolder[] folders = null;
                PdfEmbeddedFileSpecification[] files = null;
                // get files and sub folders of current folder
                GetFilesAndFolders(_currentFolder, out folders, out files);
                Items.Clear();


                // remove not standard icons

                while (LargeImageList.Images.Count > STANDARD_ICON_COUNT)
                {
                    LargeImageList.Images[STANDARD_ICON_COUNT].Dispose();
                    LargeImageList.Images.RemoveAt(STANDARD_ICON_COUNT);
                }
                while (SmallImageList.Images.Count > STANDARD_ICON_COUNT)
                {
                    SmallImageList.Images[STANDARD_ICON_COUNT].Dispose();
                    SmallImageList.Images.RemoveAt(STANDARD_ICON_COUNT);
                }


                List<ListViewItem> items = new List<ListViewItem>();
                if (folders != null)
                {
                    // for each folder in forlders
                    foreach (PdfAttachmentFolder folder in folders)
                        // create folder list item
                        items.Add(CreateItem(folder));
                }
                if (files != null)
                {
                    // for each file in files
                    foreach (PdfEmbeddedFileSpecification file in files)
                        // create file list
                        items.Add(CreateItem(file));
                }

                // if attachment schema or sort is specified
                if (_document.Attachments.Sort != null && _document.Attachments.Schema != null)
                {
                    // get sort field names
                    string[] sortFieldNames = _document.Attachments.Sort.FieldNames;
                    if (sortFieldNames != null && sortFieldNames.Length > 0)
                    {
                        string sortFieldName = sortFieldNames[0];
                        string[] sortKeys = new string[items.Count];
                        for (int i = 0; i < items.Count; i++)
                        {
                            // if current item is folder
                            if (_listItemToFolder.ContainsKey(items[i]))
                                sortKeys[i] = Document.Attachments.Schema.GetDataAsString(sortFieldName, _listItemToFolder[items[i]]);
                            else
                                sortKeys[i] = Document.Attachments.Schema.GetDataAsString(sortFieldName, _listItemToFile[items[i]]);
                        }
                        ListViewItem[] itemsArray = items.ToArray();
                        // sort items
                        Array.Sort(sortKeys, itemsArray);
                        items.Clear();
                        bool[] ascendingOrders = _document.Attachments.Sort.AscendingOrders;
                        if (ascendingOrders != null && ascendingOrders.Length > 0)
                        {
                            if (!ascendingOrders[0])
                                Array.Reverse(itemsArray);
                        }
                        items.AddRange(itemsArray);
                    }
                }
                Items.AddRange(items.ToArray());
            }
            finally
            {
                EndUpdate();
            }

            OnCurrentFolderChanged(new EventArgs());
        }

        /// <summary>
        /// Updates the colors of attachment viewer.
        /// </summary>
        public void UpdateColors()
        {
            if (_document.Attachments.Colors != null)
            {
                BackColor = _document.Attachments.Colors.Background;
                ForeColor = _document.Attachments.Colors.PrimaryText;
            }
            else
            {
                BackColor = SystemColors.Window;
                ForeColor = SystemColors.WindowText;
            }
            foreach (ListViewItem item in Items)
            {
                SetProperties(item);
                foreach (ListViewItem.ListViewSubItem subItem in item.SubItems)
                    SetProperties(subItem);
            }
        }

        /// <summary>
        /// Adds the new folder to current folder.
        /// </summary>
        /// <param name="name">The name of new folder.</param>
        /// <returns>Newly created folder.</returns>
        public PdfAttachmentFolder AddNewFolder(string name)
        {
            if (RootFolder == null)
            {
                _document.Attachments.RootFolder = new PdfAttachmentFolder(_document);
                _document.Attachments.RootFolder.CreationDate = DateTime.Now;
                CurrentFolder = RootFolder;
            }
            PdfAttachmentFolder newFolder = new PdfAttachmentFolder(CurrentFolder, GetFreeName(name));
            newFolder.CreationDate = DateTime.Now;
            newFolder.ModificationDate = newFolder.CreationDate;
            CurrentFolder.ModificationDate = newFolder.CreationDate;
            UpdateCurrentFolder();
            SetSelectedFiles(null);
            SetSelectedFolders(new PdfAttachmentFolder[] { newFolder });
            return newFolder;
        }

        /// <summary>
        /// Adds the file to the current folder.
        /// </summary>
        /// <param name="filename">The filename.</param>
        /// <param name="compression">The file compression.</param>
        /// <returns>The newly created embedded file.</returns>
        public PdfEmbeddedFileSpecification AddFile(string filename, PdfCompression compression)
        {
            if (CurrentFolder == null)
            {
                _document.Attachments.RootFolder = new PdfAttachmentFolder(_document);
                RootFolder.CreationDate = DateTime.Now;
                CurrentFolder = RootFolder;
            }
            PdfEmbeddedFileSpecification result = AddFile(CurrentFolder, filename, compression);
            CurrentFolder.ModificationDate = DateTime.Now;
            UpdateCurrentFolder();
            return result;
        }
     
        /// <summary>
        /// Adds the path to the current folder.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="compression">The compression.</param>
        /// <param name="actionController">The action controller.</param>
        /// <returns>The newly created attachment folder.</returns>
        public PdfAttachmentFolder AddPath(
            string path,
            PdfCompression compression,
            StatusStripActionController actionController)
        {
            PdfAttachmentFolder result = null;
            if (CurrentFolder == null)
            {
                _document.Attachments.RootFolder = new PdfAttachmentFolder(_document);
                RootFolder.CreationDate = DateTime.Now;
                CurrentFolder = RootFolder;
            }
            try
            {
                result = AddPathRecursive(CurrentFolder, path, compression, actionController);
                CurrentFolder.ModificationDate = DateTime.Now;
                UpdateCurrentFolder();
                SetSelectedFiles(null);
                SetSelectedFolders(new PdfAttachmentFolder[] { result });
            }
            catch (Exception ex)
            {
                DemosTools.ShowErrorMessage(string.Format("{0}: {1}", path, ex.Message));
            }
            return result;
        }

        /// <summary>
        /// Deletes the selected items (files and folders).
        /// </summary>
        public void DeleteSelectedItems()
        {
            PdfEmbeddedFileSpecification[] files = GetSelectedFiles();
            if (files != null && files.Length > 0)
            {
                if (CurrentFolder == null)
                {
                    for (int i = 0; i < files.Length; i++)
                        _document.Attachments.DeleteFile("", files[i].Filename);
                }
                else
                {
                    for (int i = 0; i < files.Length; i++)
                        CurrentFolder.DeleteFile(files[i]);
                }

            }
            PdfAttachmentFolder[] folders = GetSelectedFolders();
            if (folders != null && folders.Length > 0)
            {
                for (int i = 0; i < folders.Length; i++)
                    CurrentFolder.DeleteFolder(folders[i]);
            }
            UpdateCurrentFolder();
        }

        /// <summary>
        /// Saves the selection (files and folders) to the specified path.
        /// </summary>
        /// <param name="path">The path to save.</param>
        /// <param name="actionController">The action controller.</param>
        /// <returns><b>true</b> if all files and folers saved; otherwise <b>false</b>.</returns>
        public bool SaveSelectionTo(string path, StatusStripActionController actionController)
        {
            PdfEmbeddedFileSpecification[] files = GetSelectedFiles();
            if (files != null)
            {
                foreach (PdfEmbeddedFileSpecification file in files)
                {
                    if (actionController != null)
                        actionController.NextSubAction(Path.GetFileName(file.Filename));
                    if (!SaveFile(file, Path.Combine(path, file.Filename)))
                        return false;
                }
            }
            PdfAttachmentFolder[] folders = GetSelectedFolders();
            if (folders != null)
            {
                foreach (PdfAttachmentFolder folder in folders)
                {
                    if (!SaveFolderRecursive(folder, path, actionController))
                        return false;
                }
            }
            return true;
        }


        /// <summary>
        /// Updates view of specified folders.
        /// </summary>
        /// <param name="folders">The folders.</param>
        public void UpdateFolderView(params PdfAttachmentFolder[] folders)
        {
            if (folders != null && folders.Length > 0)
            {
                foreach (PdfAttachmentFolder folder in folders)
                    if (_folderToListItem.ContainsKey(folder))
                        SetFolderItemProperties(_folderToListItem[folder], folder);
                Update();
            }
        }

        /// <summary>
        /// Updates view of specified files.
        /// </summary>
        /// <param name="fileSpecifications">The file specifications.</param>
        public void UpdateFileView(params PdfEmbeddedFileSpecification[] fileSpecifications)
        {
            if (fileSpecifications != null && fileSpecifications.Length > 0)
            {
                foreach (PdfEmbeddedFileSpecification fileSpecification in fileSpecifications)
                {
                    if (_fileToListItem.ContainsKey(fileSpecification))
                        SetFileItemProperties(_fileToListItem[fileSpecification], fileSpecification);
                }
                Update();
            }
        }

        /// <summary>
        /// Updates the attachments schema.
        /// </summary>
        public void UpdateSchema()
        {
            Dictionary<string, int> columnsWidth = new Dictionary<string, int>();
            foreach (ColumnHeader column in Columns)
                columnsWidth[column.Name] = column.Width;

            Columns.Clear();
            // file name / folder name
            Columns.Add("Name");
            if (_document.Attachments.Schema != null)
            {
                List<string> columnDisplayedNameList = new List<string>();
                List<string> columnNameList = new List<string>();
                List<int> columnOrderList = new List<int>();
                foreach (string schemaFieldName in _document.Attachments.Schema.Keys)
                {
                    PdfAttachmentCollectionSchemaField schemaField = _document.Attachments.Schema[schemaFieldName];
                    if (schemaField.IsVisible)
                    {
                        columnOrderList.Add(_document.Attachments.Schema[schemaFieldName].Order);
                        columnNameList.Add(schemaFieldName);
                        if (schemaField.DisplayedName != "")
                        {
                            columnDisplayedNameList.Add(schemaField.DisplayedName);
                        }
                        else
                        {
                            switch (schemaField.DataType)
                            {
                                case AttachmentCollectionSchemaFieldDataType.CompressedSize:
                                    columnDisplayedNameList.Add("Compressed Size");
                                    break;
                                case AttachmentCollectionSchemaFieldDataType.UncompressedSize:
                                    columnDisplayedNameList.Add("Uncompressed Size");
                                    break;
                                case AttachmentCollectionSchemaFieldDataType.ModificationDate:
                                    columnDisplayedNameList.Add("Modification Date");
                                    break;
                                case AttachmentCollectionSchemaFieldDataType.CreationDate:
                                    columnDisplayedNameList.Add("Creation Date");
                                    break;
                                case AttachmentCollectionSchemaFieldDataType.Filename:
                                    columnDisplayedNameList.Add("File name");
                                    break;
                                case AttachmentCollectionSchemaFieldDataType.FileDescription:
                                    columnDisplayedNameList.Add("Description");
                                    break;
                                default:
                                    columnDisplayedNameList.Add(schemaFieldName);
                                    break;
                            }
                        }
                    }
                }
                string[] columnDisplayedNames = columnDisplayedNameList.ToArray();
                string[] columnNames = columnNameList.ToArray();
                int[] columnOrders = columnOrderList.ToArray();
                Array.Sort(columnOrders, columnDisplayedNames);
                columnOrders = columnOrderList.ToArray();
                Array.Sort(columnOrders, columnNames);
                for (int i = 0; i < columnOrders.Length; i++)
                {
                    ColumnHeader columnHeader = new ColumnHeader();
                    columnHeader.Name = columnNames[i];
                    columnHeader.Text = columnDisplayedNames[i];
                    Columns.Add(columnHeader);
                }
            }

            _useSchema = Columns.Count > 1;
            if (_useSchema)
            {
            }
            else
            {
                Columns.Add("Compressed Size");
                Columns.Add("Uncompressed Size");
                Columns.Add("Modified date");
            }

            for (int i = 0; i < Columns.Count; i++)
            {
                if (columnsWidth.ContainsKey(Columns[i].Name))
                    Columns[i].Width = columnsWidth[Columns[i].Name];
                else
                    Columns[i].Width = 100;
            }
        }

        #endregion


        #region PROTECTED

        /// <summary>
        /// Raises the <see cref="E:CurrentFolderChanged" /> event.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected virtual void OnCurrentFolderChanged(EventArgs e)
        {
            if (CurrentFolderChanged != null)
                CurrentFolderChanged(this, e);
        }

        /// <summary>
        /// Raises the <see cref="E:AfterLabelEdit" /> event.
        /// </summary>
        /// <param name="e">The <see cref="LabelEditEventArgs"/> instance containing the event data.</param>
        protected override void OnAfterLabelEdit(LabelEditEventArgs e)
        {
            base.OnAfterLabelEdit(e);

            if (e.Label == null)
                return;

            ListViewItem item = Items[e.Item];

            // if item is embedded file
            if (_listItemToFile.ContainsKey(item))
            {
                // rename embedded file
                if (CurrentFolder == null)
                    _listItemToFile[item].Filename = e.Label;
                else
                    CurrentFolder.RenameFile(_listItemToFile[item].Filename, e.Label);
            }
            else if (_listItemToFolder.ContainsKey(item))
            {
                // rename attachment folder
                _listItemToFolder[item].Name = e.Label;
            }
            else
            {
                return;
            }

            OnItemRenamed(EventArgs.Empty);
        }

        /// <summary>
        /// Raises the <see cref="E:ItemRenamed" /> event.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected virtual void OnItemRenamed(EventArgs e)
        {
            if (ItemRenamed != null)
                ItemRenamed(this, e);
        }

        #endregion


        #region PRIVATE

        /// <summary>
        /// Creates the list item for attachment folder.
        /// </summary>
        /// <param name="folder">The folder.</param>
        /// <returns><see cref="ListViewItem"/> instance.</returns>
        private ListViewItem CreateItem(PdfAttachmentFolder folder)
        {
            ListViewItem item = new ListViewItem();
            _listItemToFolder[item] = folder;
            _folderToListItem[folder] = item;
            SetFolderItemProperties(item, folder);
            return item;
        }

        /// <summary>
        /// Creates the list item for embedded file.
        /// </summary>
        /// <param name="file">The embedded file.</param>
        /// <returns><see cref="ListViewItem"/> instance.</returns>
        private ListViewItem CreateItem(PdfEmbeddedFileSpecification file)
        {
            ListViewItem item = new ListViewItem();
            SetFileItemProperties(item, file);
            _listItemToFile[item] = file;
            _fileToListItem[file] = item;
            return item;
        }

        /// <summary>
        /// Sets the file item properties.
        /// </summary>
        /// <param name="item">The list item.</param>
        /// <param name="file">The embedded file.</param>
        private void SetFileItemProperties(ListViewItem item, PdfEmbeddedFileSpecification file)
        {
            // set a thumbnail

            if (file.Thumbnail != null)
            {
                if (item.ImageIndex > STANDARD_ICON_COUNT)
                {
                    SetImage(LargeImageList, item.ImageIndex, file.Thumbnail);
                    SetImage(SmallImageList, item.ImageIndex, file.Thumbnail);
                }
                else
                {
                    int newIndex = LargeImageList.Images.Count;
                    SetImage(LargeImageList, newIndex, file.Thumbnail);
                    SetImage(SmallImageList, newIndex, file.Thumbnail);
                    item.ImageIndex = newIndex;
                }
            }
            else
            {
                if (Path.GetExtension(file.Filename).ToUpperInvariant() == ".PDF")
                    item.ImageIndex = 2;
                else
                    item.ImageIndex = 3;
            }


            // set the properties

            item.SubItems.Clear();
            item.Text = file.Filename;
            // if schema from PDF is used
            if (_useSchema)
            {
                // add field values of schema
                for (int i = 1; i < Columns.Count; i++)
                {
                    string schemaFieldName = Columns[i].Name;
                    PdfAttachmentCollectionSchemaField schemaField = _document.Attachments.Schema[schemaFieldName];
                    if (schemaField.IsVisible)
                    {
                        if (schemaField.DataType == AttachmentCollectionSchemaFieldDataType.Date ||
                            schemaField.DataType == AttachmentCollectionSchemaFieldDataType.Number ||
                            schemaField.DataType == AttachmentCollectionSchemaFieldDataType.String)
                        {
                            string fieldDataAsString = _document.Attachments.Schema.GetDataAsString(schemaFieldName, file);
                            PdfAttachmentDataField dataField = _document.Attachments.Schema.GetDataField(schemaFieldName, file);
                            if (dataField != null && dataField.Prefix != null)
                                fieldDataAsString = dataField.Prefix + fieldDataAsString;
                            item.SubItems.Add(fieldDataAsString);
                            SetProperties(item.SubItems[item.SubItems.Count - 1]);
                        }
                        else
                        {
                            AddStandardFieldValue(item, schemaField.DataType, file);
                        }
                    }
                }
            }
            else
            {
                // Compressed Size
                AddStandardFieldValue(item, AttachmentCollectionSchemaFieldDataType.CompressedSize, file);
                // Uncompressed Size
                AddStandardFieldValue(item, AttachmentCollectionSchemaFieldDataType.UncompressedSize, file);
                // Modified date
                AddStandardFieldValue(item, AttachmentCollectionSchemaFieldDataType.ModificationDate, file);
            }

            SetProperties(item);
        }

        /// <summary>
        /// Sets the folder item properties.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="folder">The folder.</param>
        private void SetFolderItemProperties(ListViewItem item, PdfAttachmentFolder folder)
        {
            if (folder.Thumbnail != null)
            {
                if (item.ImageIndex > STANDARD_ICON_COUNT)
                {
                    SetImage(LargeImageList, item.ImageIndex, folder.Thumbnail);
                    SetImage(SmallImageList, item.ImageIndex, folder.Thumbnail);
                }
                else
                {
                    int newIndex = LargeImageList.Images.Count;
                    SetImage(LargeImageList, newIndex, folder.Thumbnail);
                    SetImage(SmallImageList, newIndex, folder.Thumbnail);
                    item.ImageIndex = newIndex;
                }
            }
            else
            {
                if (folder.IsContainsFiles || folder.IsContainsFolders)
                    item.ImageIndex = 1;
                else
                    item.ImageIndex = 0;
            }

            // set the properties

            item.SubItems.Clear();
            item.Text = folder.Name;
            // if schema from PDF is used
            if (_useSchema)
            {
                // add field values of schema
                for (int i = 1; i < Columns.Count; i++)
                {
                    string schemaFieldName = Columns[i].Name;
                    PdfAttachmentCollectionSchemaField schemaField = _document.Attachments.Schema[schemaFieldName];
                    if (schemaField.IsVisible)
                    {
                        if (schemaField.DataType == AttachmentCollectionSchemaFieldDataType.Date ||
                            schemaField.DataType == AttachmentCollectionSchemaFieldDataType.Number ||
                            schemaField.DataType == AttachmentCollectionSchemaFieldDataType.String)
                        {
                            string fieldDataAsString = _document.Attachments.Schema.GetDataAsString(schemaFieldName, folder);
                            PdfAttachmentDataField dataField = _document.Attachments.Schema.GetDataField(schemaFieldName, folder);
                            if (dataField != null && dataField.Prefix != null)
                                fieldDataAsString = dataField.Prefix + fieldDataAsString;
                            item.SubItems.Add(fieldDataAsString);
                            SetProperties(item.SubItems[item.SubItems.Count - 1]);
                        }
                        else
                        {
                            AddStandardFieldValue(item, schemaField.DataType, folder);
                        }
                    }
                }
            }
            else
            {
                // Compressed Size
                item.SubItems.Add("");
                // Uncompressed Size
                item.SubItems.Add("");
                // Modified date
                AddStandardFieldValue(item, AttachmentCollectionSchemaFieldDataType.ModificationDate, folder);
            }

            SetProperties(item);
        }

        /// <summary>
        /// Sets the properties of list item.
        /// </summary>
        /// <param name="item">The item.</param>
        private void SetProperties(ListViewItem item)
        {
            if (_document.Attachments.Colors != null)
            {
                item.BackColor = _document.Attachments.Colors.CardBackground;
            }
            else
            {
                item.BackColor = Color.Transparent;
            }
        }

        /// <summary>
        /// Sets the properties of list sub item (data field).
        /// </summary>
        /// <param name="item">The sub item.</param>
        private void SetProperties(ListViewItem.ListViewSubItem item)
        {
            if (_document.Attachments.Colors != null)
            {
                item.BackColor = _document.Attachments.Colors.CardBackground;
                item.ForeColor = _document.Attachments.Colors.SecondaryText;
            }
            else
            {
                item.BackColor = Color.Transparent;
                item.ForeColor = SystemColors.WindowText;
            }
        }

        /// <summary>
        /// Adds the standard field value to list item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="dataType">Type of the data.</param>
        /// <param name="fileSpecification">The file specification.</param>
        private void AddStandardFieldValue(
            ListViewItem item,
            AttachmentCollectionSchemaFieldDataType dataType,
            PdfEmbeddedFileSpecification fileSpecification)
        {
            string textValue = "N/A";
            switch (dataType)
            {
                case AttachmentCollectionSchemaFieldDataType.CompressedSize:
                    if (fileSpecification.EmbeddedFile != null)
                        textValue = FileSizeToString(fileSpecification.EmbeddedFile.Length);
                    break;
                case AttachmentCollectionSchemaFieldDataType.CreationDate:
                    if (fileSpecification.EmbeddedFile != null)
                        textValue = ToString(fileSpecification.EmbeddedFile.CreationDate);
                    break;
                case AttachmentCollectionSchemaFieldDataType.ModificationDate:
                    if (fileSpecification.EmbeddedFile != null)
                        textValue = ToString(fileSpecification.EmbeddedFile.ModifyDate);
                    break;
                case AttachmentCollectionSchemaFieldDataType.FileDescription:
                    textValue = fileSpecification.Description;
                    break;
                case AttachmentCollectionSchemaFieldDataType.Filename:
                    textValue = fileSpecification.Filename;
                    break;
                case AttachmentCollectionSchemaFieldDataType.UncompressedSize:
                    if (fileSpecification.EmbeddedFile != null)
                        textValue = FileSizeToString(fileSpecification.EmbeddedFile.UncompressedLength);
                    break;
            }
            item.SubItems.Add(textValue);
            SetProperties(item.SubItems[item.SubItems.Count - 1]);
        }

        /// <summary>
        /// Adds the standard field value to the list item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="dataType">Type of the data.</param>
        /// <param name="folder">The file specification.</param>
        private void AddStandardFieldValue(
            ListViewItem item,
            AttachmentCollectionSchemaFieldDataType dataType,
            PdfAttachmentFolder folder)
        {
            string textValue = "N/A";
            switch (dataType)
            {
                case AttachmentCollectionSchemaFieldDataType.CreationDate:
                    textValue = ToString(folder.CreationDate);
                    break;
                case AttachmentCollectionSchemaFieldDataType.ModificationDate:
                    textValue = ToString(folder.ModificationDate);
                    break;
                case AttachmentCollectionSchemaFieldDataType.FileDescription:
                    textValue = folder.Description;
                    break;
                case AttachmentCollectionSchemaFieldDataType.Filename:
                    textValue = folder.Name;
                    break;
            }
            item.SubItems.Add(textValue);
            SetProperties(item.SubItems[item.SubItems.Count - 1]);
        }

        /// <summary>
        /// Converts file size to a string.
        /// </summary>
        /// <param name="sizeInBytes">The size in bytes.</param>
        /// <returns>String with file size representation.</returns>
        private static string FileSizeToString(long sizeInBytes)
        {
            if (sizeInBytes < 1024)
                return string.Format("{0} bytes", sizeInBytes);
            if (sizeInBytes < 1024 * 1024)
                return string.Format("{0:f1} KB", sizeInBytes / 1024f);
            return string.Format("{0:f1} MB", sizeInBytes / (1024f * 1024f));
        }

        /// <summary>
        /// Converts date time structure to a string.
        /// </summary>
        /// <param name="dateTime">The date time.</param>
        /// <returns>String with DateTime representation.</returns>
        private static string ToString(DateTime dateTime)
        {
            if (dateTime == DateTime.MinValue)
                return "N/A";
            return dateTime.ToString();
        }

        /// <summary>
        /// Adds the image to the specified image list.
        /// </summary>
        /// <param name="imageList">The image list.</param>
        /// <param name="sourceImage">The source image.</param>
        private void AddImage(ImageList imageList, VintasoftImage sourceImage)
        {
            SetImage(imageList, imageList.Images.Count, sourceImage);
        }

        /// <summary>
        /// Sets the image with specified index in image list.
        /// </summary>
        /// <param name="imageList">The image list.</param>
        /// <param name="imageIndex">Index of the image.</param>
        /// <param name="sourceImage">The source image.</param>
        private void SetImage(ImageList imageList, int imageIndex, VintasoftImage sourceImage)
        {
            using (VintasoftImage resultImage = new VintasoftImage(
                imageList.ImageSize.Width, imageList.ImageSize.Height, PixelFormat.Bgra32))
            {
                ClearImageCommand clearImage = new ClearImageCommand(Color.Transparent);
                clearImage.ExecuteInPlace(resultImage);
                Graphics g = resultImage.OpenGraphics();
                float scale = Math.Min(
                    resultImage.Width / (float)sourceImage.Width,
                    resultImage.Height / (float)sourceImage.Height);
                float width = sourceImage.Width * scale;
                float height = sourceImage.Height * scale;
                float x = (resultImage.Width - width) / 2;
                float y = (resultImage.Height - height) / 2;
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBilinear;
                sourceImage.Draw(g, new RectangleF(x, y, width, height));
                resultImage.CloseGraphics();
                if (imageIndex == imageList.Images.Count)
                {
                    imageList.Images.Add(resultImage.GetAsBitmap());
                }
                else
                {
                    Image oldImage = imageList.Images[imageIndex];
                    imageList.Images[imageIndex] = resultImage.GetAsBitmap();
                    oldImage.Dispose();
                }
            }
        }

        /// <summary>
        /// Sets the image resource with specified index in image list.
        /// </summary>
        /// <param name="imageList">The image list.</param>
        /// <param name="imageIndex">Index of the image.</param>
        /// <param name="imageResource">The image resource.</param>
        private void SetImage(ImageList imageList, int imageIndex, PdfImageResource imageResource)
        {
            using (VintasoftImage sourceImage = imageResource.GetImage())
                SetImage(imageList, imageIndex, sourceImage);
        }

        /// <summary>
        /// Gets the files and sub folders of the specified folder.
        /// </summary>
        /// <param name="folder">The folder.</param>
        /// <param name="subFolders">The sub folders.</param>
        /// <param name="files">The files.</param>
        private void GetFilesAndFolders(
            PdfAttachmentFolder folder,
            out PdfAttachmentFolder[] subFolders,
            out PdfEmbeddedFileSpecification[] files)
        {
            if (folder == null && _document.Attachments.RootFolder == null)
            {
                subFolders = null;
                files = _document.Attachments.GetFiles("");
                return;
            }
            if (folder == null)
                folder = _document.Attachments.RootFolder;

            subFolders = folder.Folders;
            files = folder.Files;
        }

        /// <summary>
        /// Resets the User Interface.
        /// </summary>
        private void ResetUI()
        {

            if (_document == null)
            {
                Columns.Clear();
                Enabled = false;
            }
            else
            {
                Enabled = true;

                if (_document.Attachments != null)
                {
                    UpdateSchema();
                    UpdateColors();
                    CurrentFolder = _document.Attachments.RootFolder;
                }
                else
                {
                    Columns.Clear();
                }
            }
        }

        /// <summary>
        /// Initializes the component.
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // PdfAttachmentViewer
            // 
            this.FullRowSelect = true;
            this.GridLines = true;
            this.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.LabelEdit = true;
            this.ShowGroups = false;
            this.ResumeLayout(false);
        }

        /// <summary>
        /// Returns free name in the current folder.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>Free name.</returns>
        private string GetFreeName(string name)
        {
            List<string> names = new List<string>();
            names.AddRange(CurrentFolder.GetFilenames());
            names.AddRange(CurrentFolder.GetFolderNames());
            if (!names.Contains(name))
                return name;
            int i = 1;
            while (i < int.MaxValue)
            {
                string newName = string.Format("{0}{1}", name, i);
                if (!names.Contains(newName))
                    return newName;
                i++;
            }
            return null;
        }

        /// <summary>
        /// Adds the path (all files and sub folders) to the specified folder.
        /// </summary>
        /// <param name="folder">The folder.</param>
        /// <param name="path">The path.</param>
        /// <param name="compression">The compression.</param>
        /// <param name="actionController">The action controller.</param>
        /// <returns>Added folder.</returns>
        private PdfAttachmentFolder AddPathRecursive(
            PdfAttachmentFolder folder,
            string path,
            PdfCompression compression,
            StatusStripActionController actionController)
        {
            // add folder

            PdfAttachmentFolder subFolder = folder.AddFolder(Path.GetFileName(path));
            subFolder.CreationDate = DateTime.Now;


            // add files

            string[] files = Directory.GetFiles(path, "*", SearchOption.TopDirectoryOnly);
            if (files.Length > 0)
            {
                foreach (string filename in files)
                {
                    try
                    {
                        if ((File.GetAttributes(filename) & FileAttributes.Hidden) == 0)
                        {
                            if (actionController != null)
                                actionController.NextSubAction(Path.GetFileName(filename));
                            PdfEmbeddedFileSpecification file = AddFile(subFolder, filename, compression);
                            file.EmbeddedFile.CreationDate = DateTime.Now;
                        }
                    }
                    catch (Exception ex)
                    {
                        DemosTools.ShowErrorMessage(string.Format("{0}: {1}", filename, ex.Message));
                    }
                }
                subFolder.ModificationDate = DateTime.Now;
            }


            // add sub folders

            string[] paths = Directory.GetDirectories(path, "*", SearchOption.TopDirectoryOnly);
            foreach (string subPath in paths)
            {
                try
                {
                    if ((File.GetAttributes(subPath) & FileAttributes.Hidden) == 0)
                        AddPathRecursive(subFolder, subPath, compression, actionController);
                }
                catch (Exception ex)
                {
                    DemosTools.ShowErrorMessage(string.Format("{0}: {1}", subFolder, ex.Message));
                }
            }

            return subFolder;
        }

        /// <summary>
        /// Saves the file to the specified file path.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="filename">The filename.</param>
        /// <returns><b>true</b> if file is saved; otherwise <b>false</b>.</returns>
        private bool SaveFile(PdfEmbeddedFileSpecification file, string filename)
        {
            try
            {
                if (file.EmbeddedFile != null)
                {
                    if (File.Exists(filename))
                    {
                        DialogResult dialogResult = MessageBox.Show(string.Format(
                            "File '{0}' already exists, override it?", 
                            filename), "", MessageBoxButtons.YesNoCancel);
                        if (dialogResult == DialogResult.Cancel)
                            return false;
                        if (dialogResult == DialogResult.No)
                            return true;
                    }
                    file.EmbeddedFile.Save(filename);
                }
            }
            catch (Exception ex)
            {
                DemosTools.ShowErrorMessage(ex);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Saves the folder (all sub folders and embedded files) to the specified path.
        /// </summary>
        /// <param name="folder">The folder.</param>
        /// <param name="path">The path.</param>
        /// <param name="actionController">The action controller.</param>
        /// <returns><b>true</b> if folder saved; otherwise <b>false</b>.</returns>
        private bool SaveFolderRecursive(PdfAttachmentFolder folder, string path, StatusStripActionController actionController)
        {
            // create path
            try
            {
                path = Path.Combine(path, folder.Name);
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
            }
            catch (Exception ex)
            {
                DemosTools.ShowErrorMessage(ex);
                return false;
            }

            // save embedded files
            PdfEmbeddedFileSpecification[] files = folder.Files;
            if (files != null)
            {
                foreach (PdfEmbeddedFileSpecification file in files)
                {
                    if (actionController != null)
                        actionController.NextSubAction(Path.GetFileName(file.Filename));
                    if (!SaveFile(file, Path.Combine(path, file.Filename)))
                        return false;
                }
            }

            // save sub folders
            PdfAttachmentFolder[] subFolders = folder.Folders;
            if (subFolders != null)
            {
                foreach (PdfAttachmentFolder subFolder in subFolders)
                {
                    if (!SaveFolderRecursive(subFolder, path, actionController))
                        return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Adds the file to to specified folder.
        /// </summary>
        /// <param name="folder">The folder.</param>
        /// <param name="filename">The filename.</param>
        /// <param name="compression">The file compression.</param>
        /// <returns>A new instance of <see cref="PdfEmbeddedFileSpecification"/>.</returns>
        private PdfEmbeddedFileSpecification AddFile(PdfAttachmentFolder folder, string filename, PdfCompression compression)
        {
            folder.EncodeFileImmediately = EncodeFileImmediately;
            return folder.AddFile(filename, compression);
        }

        #endregion

        #endregion



        #region Events

        /// <summary>
        /// Occurs when item is renamed.
        /// </summary>
        public event EventHandler ItemRenamed;

        /// <summary>
        /// Occurs when current folder is changed.
        /// </summary>
        public event EventHandler CurrentFolderChanged;

        #endregion

    }
}
