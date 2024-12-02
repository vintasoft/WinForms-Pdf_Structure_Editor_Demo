using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;

using Vintasoft.Imaging;
using Vintasoft.Imaging.Pdf;
using Vintasoft.Imaging.Pdf.Tree;

using DemosCommonCode.Imaging;
using DemosCommonCode.Imaging.Codecs;


namespace DemosCommonCode.Pdf
{
    /// <summary>
    /// A form that allows to display PDF resources and select PDF resource.
    /// </summary>
    public partial class PdfResourcesViewerForm : Form
    {

        #region Fields

        /// <summary>
        /// The source PDF document.
        /// </summary>
        PdfDocument _sourceDocument = null;

        /// <summary>
        /// Dictionary: PDF document => list of PDF resources, which are opened in this form.
        /// </summary>
        static Dictionary<PdfDocument, List<PdfResource>> _documentToNewResources =
            new Dictionary<PdfDocument, List<PdfResource>>();

        /// <summary>
        /// The ToolStripMenuItem, which is selected in "Preview" menu.
        /// </summary>
        ToolStripMenuItem _previewSelectedToolStripMenuItem = null;

        #endregion



        #region Constructors

        /// <summary>
        /// Initializes the <see cref="PdfResourcesViewerForm"/> class.
        /// </summary>
        static PdfResourcesViewerForm()
        {
            PdfDocumentController.DocumentClosed +=
                new EventHandler<PdfDocumentEventArgs>(PdfDocumentController_DocumentClosed);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PdfResourcesViewerForm"/> class.
        /// </summary>
        /// <param name="document">The source PDF document.</param>
        public PdfResourcesViewerForm(
            PdfDocument document)
            : this(document, false)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PdfResourcesViewerForm"/> class.
        /// </summary>
        /// <param name="document">The source PDF document.</param>
        /// <param name="canAddResources">Determines that
        /// form allows to add resources to the source PDF document.</param>
        public PdfResourcesViewerForm(
            PdfDocument document,
            bool canAddResources)
            : this(canAddResources, document)
        {
            if (document == null)
                throw new ArgumentNullException();

            DocumentResourceViewer.SelectedObject = document;
            UpdateViewMenu();

            if (_documentToNewResources.ContainsKey(document))
                DocumentResourceViewer.AdditionalResources = _documentToNewResources[document];

            SelectFirstItem();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PdfResourcesViewerForm"/> class.
        /// </summary>
        /// <param name="treeNode">The PDF tree node, which should be viewed.</param>
        public PdfResourcesViewerForm(PdfTreeNodeBase treeNode)
            : this(false, GetDocument(treeNode, "treeNode"))
        {
            DocumentResourceViewer.SelectedObject = treeNode;
            UpdateViewMenu();
            SelectFirstItem();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PdfResourcesViewerForm"/> class.
        /// </summary>
        /// <param name="document">A PDF document.</param>
        /// <param name="treeNodes">The PDF tree nodes, which should be viewed.</param>
        public PdfResourcesViewerForm(
            PdfDocument document,
            IEnumerable<PdfTreeNodeBase> treeNodes)
            : this(false, document)
        {
            DocumentResourceViewer.SelectedObject = treeNodes;
            UpdateViewMenu();
            SelectFirstItem();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PdfResourcesViewerForm"/> class.
        /// </summary>
        /// <param name="document">A PDF document.</param>
        /// <param name="treeNodes">The PDF tree nodes, which should be viewed.</param>
        public PdfResourcesViewerForm(
            PdfDocument document,
            params PdfTreeNodeBase[] treeNodes)
            : this(document, (IEnumerable<PdfTreeNodeBase>)treeNodes)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PdfResourcesViewerForm"/> class.
        /// </summary>
        /// <param name="canAddResources">Determines that
        /// form allows to add resources to the source PDF document.</param>
        /// <param name="document">The source PDF document.</param>
        private PdfResourcesViewerForm(
            bool canAddResources,
            PdfDocument document)
        {
            InitializeComponent();

            CodecsFileFilters.SetOpenFileDialogFilter(openImageFileDialog);

            if (document == null)
                throw new ArgumentException("sourceDocument");

            FileStream fileStream = document.SourceStream as FileStream;
            if (fileStream != null)
            {
                string fileName = Path.GetFileName(fileStream.Name);
                Text = string.Format("Resources of {0}", fileName);
            }

            CanAddResources = canAddResources;
            _sourceDocument = document;

            hierarchicalToolStripMenuItem.Tag = PdfResourceTreeViewType.Hierarchical;
            linearToolStripMenuItem.Tag = PdfResourceTreeViewType.Linear;

            switch (DocumentResourceViewer.TreeType)
            {
                case PdfResourceTreeViewType.Hierarchical:
                    _previewSelectedToolStripMenuItem = hierarchicalToolStripMenuItem;
                    break;

                case PdfResourceTreeViewType.Linear:
                    _previewSelectedToolStripMenuItem = linearToolStripMenuItem;
                    break;
            }
            _previewSelectedToolStripMenuItem.Checked = true;

            UpdateUI();
        }

        #endregion



        #region Properties

        /// <summary>
        /// Gets or sets a value indicating whether the resource viewer should
        /// show the image resources of PDF document.
        /// </summary>
        /// <value>
        /// Default value is <b>true</b>.
        /// </value>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool ShowImageResources
        {
            get
            {
                return DocumentResourceViewer.ShowImageResources;
            }
            set
            {
                DocumentResourceViewer.ShowImageResources = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the resource viewer should
        /// show the form resources of PDF document.
        /// </summary>
        /// <value>
        /// Default value is <b>true</b>.
        /// </value>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool ShowFormResources
        {
            get
            {
                return DocumentResourceViewer.ShowFormResources;
            }
            set
            {
                DocumentResourceViewer.ShowFormResources = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the resources can be added
        /// to the source PDF document.
        /// </summary>
        /// <value>
        /// <b>true</b> - the resources can be added to the source PDF document;<br />
        /// <b>false</b> - the resources can NOT be added to the source PDF document.
        /// </value>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool CanAddResources
        {
            get
            {
                return addFromDocumentToolStripMenuItem.Visible;
            }
            set
            {
                addFromDocumentToolStripMenuItem.Visible = value;
                addFromDocumentToolStripSeparator.Visible = value;

                createResourceFromSelectedImageToolStripMenuItem.Visible = value;
                createResourceFromImageToolStripMenuItem.Visible = value;
                createResourceFromSelectedPageToolStripMenuItem.Visible = value;
                createResourcesToolStripSeparator.Visible = value;
            }
        }

        PdfResource _selectedResource = null;
        /// <summary>
        /// Gets the selected PDF resource.
        /// </summary>
        public PdfResource SelectedResource
        {
            get
            {
                return _selectedResource;
            }
        }

        bool _propertyValueChanged = false;
        /// <summary>
        /// Gets a value indicating whether the resource property is changed.
        /// </summary>
        /// <value>
        /// <b>true</b> - at least one property of one of displayed resources is changed;
        /// <b>false</b> - properties of all displayed resources are NOT changed.<br />
        /// Default value is <b>false</b>.
        /// </value>
        public bool PropertyValueChanged
        {
            get
            {
                return _propertyValueChanged;
            }
        }

        #endregion



        #region Methods

        #region PROTECTED

        /// <summary>
        /// Form is shown.
        /// </summary>
        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            DocumentResourceViewer.Focus();
        }

        #endregion


        #region INTERNAL

        /// <summary>
        /// Removes the additional resource from PDF document.
        /// </summary>
        /// <param name="resource">The additional PDF resource.</param>
        internal static void RemoveAdditionalResource(PdfResource resource)
        {
            PdfDocument document = resource.Document;
            List<PdfResource> resources = null;
            if (_documentToNewResources.TryGetValue(document, out resources))
                resources.Remove(resource);
        }

        #endregion


        #region PRIVATE, STATIC

        /// <summary>
        /// Returns PDF document, which is associated with PDF tree node.
        /// </summary>
        /// <param name="pdfTreeNode">The PDF tree node.</param>
        /// <param name="exceptionMessage">The exception message, which must be shown
        /// if <i>pdfTreeNode</i> is <b>null</b>.</param>
        private static PdfDocument GetDocument(PdfTreeNodeBase pdfTreeNode, string exceptionMessage)
        {
            if (pdfTreeNode == null)
                throw new ArgumentNullException(exceptionMessage);

            return pdfTreeNode.Document;
        }

        /// <summary>
        /// PDF document is closed.
        /// </summary>
        private static void PdfDocumentController_DocumentClosed(
            object sender,
            PdfDocumentEventArgs e)
        {
            _documentToNewResources.Remove(e.Document);
        }

        #endregion


        #region PRIVATE

        /// <summary>
        /// Updates the user interface of this form.
        /// </summary>
        private void UpdateUI()
        {
            bool isResourceSelected = SelectedResource != null;
            bool isImageResourceSelected = SelectedResource is PdfImageResource;

            saveAsBinaryToolStripMenuItem.Enabled = isResourceSelected;
            saveAsImageToolStripMenuItem.Enabled = isImageResourceSelected;

            createResourceFromSelectedPageToolStripMenuItem.Enabled =
                DocumentResourceViewer.SelectedPage != null;
            createResourceFromSelectedImageToolStripMenuItem.Enabled =
                DocumentResourceViewer.SelectedResource is PdfImageResource;
        }

        /// <summary>
        /// Updates the view menu.
        /// </summary>
        private void UpdateViewMenu()
        {
            int nodeCount = DocumentResourceViewer.GetNodeCount(true);

            if (nodeCount == DocumentResourceViewer.Nodes.Count)
                viewToolStripMenuItem.Enabled = false;
            else
                viewToolStripMenuItem.Enabled = true;
        }

        /// <summary>
        /// Selects the first resource in this form.
        /// </summary>
        private void SelectFirstItem()
        {
            if (DocumentResourceViewer.Nodes.Count > 0)
            {
                DocumentResourceViewer.SelectedNode = DocumentResourceViewer.Nodes[0];
                DocumentResourceViewer.Focus();
            }
        }

        /// <summary>
        /// Changes the selected PDF resource.
        /// </summary>
        private void pdfDocumentResourceViewer_AfterSelect(object sender, TreeViewEventArgs e)
        {
            PdfResource resource = DocumentResourceViewer.SelectedResource;

            _selectedResource = resource;
            ResourceViewerControl.Resource = resource;
            propertyGrid.SelectedObject = resource;

            UpdateUI();
        }

        /// <summary>
        /// Button "Save As Binary..." is clicked.
        /// </summary>
        private void saveAsBinaryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog.DefaultExt = "bin";
            saveFileDialog.Filter = "Binary Files (*.bin)|*.bin";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                byte[] bytes = SelectedResource.GetBytes();
                File.WriteAllBytes(saveFileDialog.FileName, bytes);
            }
        }

        /// <summary>
        /// Button "Save As Image..." is clicked.
        /// </summary>
        private void saveAsImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PdfImageResource resourceStream = (PdfImageResource)SelectedResource;

            using (VintasoftImage image = resourceStream.GetImage())
            {
                SaveImageFileForm.SaveImageToFile(image, PluginsEncoderFactory.Default);
            }
        }

        /// <summary>
        /// Button "Default Compression Params..." is clicked.
        /// </summary>
        private void defaultCompressionParamsButton_Click(object sender, EventArgs e)
        {
            using (PropertyGridForm form =
                new PropertyGridForm(PdfCompressionSettings.DefaultSettings, "Compression Default Params"))
            {
                form.ShowDialog();
            }
        }

        /// <summary>
        /// Loads resources from the specified PDF document and
        /// adds selected PDF resources to this form.
        /// </summary>
        private void addFromDocumentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                AddResourcesFromDocument(openFileDialog.FileName);
            }
        }

        /// <summary>
        /// Adds the resources from specified PDF document.
        /// </summary>
        /// <param name="filename">The filename.</param>
        private void AddResourcesFromDocument(string filename)
        {
            try
            {
                using (Stream stream = File.Open(filename, FileMode.Open))
                {
                    PdfDocument document = PdfDocumentController.OpenDocument(stream);
                    try
                    {
                        using (PdfResourcesViewerForm dialog = new PdfResourcesViewerForm(document))
                        {
                            dialog.Owner = this;
                            dialog.StartPosition = FormStartPosition.CenterParent;
                            dialog.ShowImageResources = ShowImageResources;
                            dialog.ShowFormResources = ShowFormResources;
                            dialog.CanAddResources = true;

                            if (dialog.ShowDialog() == DialogResult.OK)
                            {
                                CopyAndAddResourceToSourceDocument(dialog.SelectedResource);
                            }
                        }
                    }
                    finally
                    {
                        PdfDocumentController.CloseDocument(document);
                        document.Dispose();
                    }
                }
            }
            catch (Exception exc)
            {
                DemosTools.ShowErrorMessage(exc);
            }
        }

        /// <summary>
        /// Creates a PDF resource from the selected PDF page.
        /// </summary>
        private void createResourceFromSelectedPageToolStripMenuItem_Click(
            object sender,
            EventArgs e)
        {
            PdfPage page = DocumentResourceViewer.SelectedPage;
            PdfFormXObjectResource dialog = new PdfFormXObjectResource(_sourceDocument, page);
            CopyAndAddResourceToSourceDocument(dialog);
        }


        /// <summary>
        /// Creates a PDF resource from the selected image-resource.
        /// </summary>
        private void createResourceFromSelectedImageToolStripMenuItem_Click(
            object sender,
            EventArgs e)
        {
            PdfImageResource image = (PdfImageResource)DocumentResourceViewer.SelectedResource;
            PdfFormXObjectResource dialog = new PdfFormXObjectResource(_sourceDocument, image);
            CopyAndAddResourceToSourceDocument(dialog);
        }


        /// <summary>
        /// Creates a PDF resource from the selected image.
        /// </summary>
        private void createResourceFromImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openImageFileDialog.ShowDialog() == DialogResult.OK)
            {
                AddResourceFromImageFile(openImageFileDialog.FileName);
            }
        }

        /// <summary>
        /// Adds the resource from image file.
        /// </summary>
        /// <param name="filename">The filename.</param>
        private void AddResourceFromImageFile(string filename)
        {
            try
            {
                // select image from file
                VintasoftImage selectedImage = SelectImageForm.SelectImageFromFile(filename);

                if (selectedImage != null)
                {
                    PdfPage page = PdfDocumentController.GetPageAssociatedWithImage(selectedImage);

                    PdfResource resource = null;
                    if (page != null)
                    {
                        resource = new PdfFormXObjectResource(_sourceDocument, page);
                    }
                    else
                    {
                        PdfImageResource imageResource = new PdfImageResource(_sourceDocument,
                            selectedImage, PdfCompression.Auto);

                        if (ShowFormResources && !ShowImageResources)
                            resource = new PdfFormXObjectResource(_sourceDocument, imageResource);
                        else
                            resource = imageResource;
                    }

                    CopyAndAddResourceToSourceDocument(resource);
                }
            }
            catch (Exception exc)
            {
                DemosTools.ShowErrorMessage(exc);
            }
        }

        /// <summary>
        /// Copies and adds the resource to the source PDF document.
        /// </summary>
        /// <param name="resource">The PDF resource.</param>
        private void CopyAndAddResourceToSourceDocument(PdfResource resource)
        {
            if (resource == null)
                return;

            if (resource.Document != _sourceDocument)
            {
                if (resource is PdfFormXObjectResource)
                {
                    PdfFormXObjectResource formResource =
                        (PdfFormXObjectResource)resource;
                    resource = formResource.CreateCopy(_sourceDocument);
                }
                else if (resource is PdfImageResource)
                {
                    PdfImageResource imageResource =
                        (PdfImageResource)resource;
                    resource = imageResource.CreateCopy(_sourceDocument);
                }
            }

            List<PdfResource> resources = null;
            if (!_documentToNewResources.TryGetValue(resource.Document, out resources))
            {
                resources = new List<PdfResource>();
                _documentToNewResources.Add(resource.Document, resources);
            }
            resources.Add(resource);

            if (DocumentResourceViewer.AdditionalResources == resources)
                DocumentResourceViewer.UpdateTreeView();
            else
                DocumentResourceViewer.AdditionalResources = resources;

            DocumentResourceViewer.SelectedResource = resource;
        }

        /// <summary>
        /// Tree view type is changed.
        /// </summary>
        private void treeViewTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem menuItem = (ToolStripMenuItem)sender;

            menuItem.Checked ^= true;
            _previewSelectedToolStripMenuItem.Checked ^= true;
            _previewSelectedToolStripMenuItem = menuItem;

            DocumentResourceViewer.TreeType = (PdfResourceTreeViewType)menuItem.Tag;
            SelectFirstItem();
        }

        /// <summary>
        /// Resource property is changed.
        /// </summary>
        private void propertyGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            ResourceViewerControl.ReloadResource();
            _propertyValueChanged = true;
        }

        #endregion

        #endregion

    }
}