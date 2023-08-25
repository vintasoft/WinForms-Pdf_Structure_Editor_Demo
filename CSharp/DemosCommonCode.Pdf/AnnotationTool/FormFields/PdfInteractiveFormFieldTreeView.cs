using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

using Vintasoft.Imaging;
using Vintasoft.Imaging.Pdf;
using Vintasoft.Imaging.Pdf.Tree;
using Vintasoft.Imaging.Pdf.Tree.InteractiveForms;
using Vintasoft.Imaging.Pdf.UI.Annotations;
using Vintasoft.Imaging.Utils;
using Vintasoft.Imaging.Pdf.Tree.Annotations;

namespace DemosCommonCode.Pdf
{
    /// <summary>
    /// A tree view for PDF interactive form.
    /// </summary>
    public class PdfInteractiveFormFieldTree : TreeView
    {

        #region Constants

        /// <summary>
        /// The format string for icon resources.
        /// </summary>
        public static readonly string RESOURCE_NAME_FORMAT_STRING = "DemosCommonCode.Pdf.AnnotationTool.FormFields.Resources.{0}.png"; 

        #endregion



        #region Fields

        /// <summary>
        /// Dictionary: PDF interactive field => image.
        /// </summary>
        Dictionary<PdfInteractiveFormField, VintasoftImage> _fieldToImageTable = new Dictionary<PdfInteractiveFormField, VintasoftImage>();

        /// <summary>
        /// Dictionary: PDF interactive field => TreeNode.
        /// </summary>
        Dictionary<PdfInteractiveFormField, TreeNode> _fieldToNodeTable = new Dictionary<PdfInteractiveFormField, TreeNode>();

        /// <summary>
        /// Indicates that control is subscribed to the InteractiveForm.FieldTreeChanged event.
        /// </summary>
        bool _isInteractiveFormChangedSubscribed = false; 

        #endregion



        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PdfInteractiveFormFieldTree"/> class.
        /// </summary>
        public PdfInteractiveFormFieldTree()
            : base()
        {
            DrawMode = TreeViewDrawMode.OwnerDrawText;

            if (!DesignMode)
            {
                ImageList = new ImageList();
                ImageList.ImageSize = new System.Drawing.Size(16, 16);
                ImageList.ColorDepth = ColorDepth.Depth32Bit;
                string[] resourceNames = new string[]{
                    "InteracitveField",
                    "CheckBoxField",
                    "BarcodeField",
                    "ButtonField",
                    "FieldGroup",
                    "ListBoxField",
                    "RadioButtonField",
                    "SignatureField",
                    "TextField",
                    "ComboBoxField",
                    "CheckBoxGroup",
                    "RadioButtonGroup",
                };
                for (int i = 0; i < resourceNames.Length; i++)
                {
                    ImageList.Images.Add(
                           resourceNames[i],
                           DemosResourcesManager.GetResourceAsBitmap(string.Format(RESOURCE_NAME_FORMAT_STRING, resourceNames[i])));
                }
            }
        }

        #endregion



        #region Properties

        PdfDocumentInteractiveForm _interactiveForm;
        /// <summary>
        /// Gets or sets the source PDF interactive form.
        /// </summary>
        public PdfDocumentInteractiveForm InteractiveForm
        {
            get
            {
                if (AnnotationTool != null)
                    return AnnotationTool.FocusedInteractiveForm;
                return _interactiveForm;
            }
            set
            {
                if (AnnotationTool != null)
                    throw new InvalidOperationException();

                if (_interactiveForm != null)
                {
                    _isInteractiveFormChangedSubscribed = false;
                    _interactiveForm.FieldTreeChanged -= new CollectionChangeEventHandler<PdfInteractiveFormField>(InteractiveForm_FieldTreeChanged);
                }

                _interactiveForm = value;

                if (_interactiveForm != null)
                {
                    _interactiveForm.FieldTreeChanged += new CollectionChangeEventHandler<PdfInteractiveFormField>(InteractiveForm_FieldTreeChanged);
                    _isInteractiveFormChangedSubscribed = true;
                }
            }
        }

        PdfAnnotationTool _annotationTool;
        /// <summary>
        /// Gets or sets the annotation tool that is used as a source of PDF interactive forms.
        /// </summary>
        [DefaultValue((object)null)]
        public PdfAnnotationTool AnnotationTool
        {
            get
            {
                return _annotationTool;
            }
            set
            {
                _interactiveForm = null;

                if (_annotationTool != null)
                {
                    foreach (PdfDocument document in _annotationTool.DocumentSet)
                    {
                        if (document.InteractiveForm != null)
                            UnsubscribeFromInteractiveFormEvents(document.InteractiveForm);
                    }
                    _annotationTool.DocumentSet.Changed -= new EventHandler<ObjectSetListenerEventArgs<PdfDocument>>(DocumentSet_Changed);
                }

                _annotationTool = value;

                if (_annotationTool != null)
                {
                    _annotationTool.DocumentSet.Changed += new EventHandler<ObjectSetListenerEventArgs<PdfDocument>>(DocumentSet_Changed);
                    foreach (PdfDocument document in _annotationTool.DocumentSet)
                    {
                        if (document.InteractiveForm != null)
                            SubscribeToInteractiveFormEvents(document.InteractiveForm);
                    }
                }
            }
        }

        bool _groupFormFieldsByPages = false;
        /// <summary>
        /// Gets or sets a  value indicating whether tree view must show the form fields grouped by pages.
        /// </summary>
        /// <value>
        /// Default value is <b>false</b>.
        /// </value>
        public bool GroupFormFieldsByPages
        {
            get
            {
                return _groupFormFieldsByPages;
            }
            set
            {
                if (_groupFormFieldsByPages != value)
                {
                    _groupFormFieldsByPages = value;
                    RefreshInteractiveFormTree();
                }
            }
        }

        /// <summary>
        /// Gets or sets the selected field.
        /// </summary>
        public PdfInteractiveFormField SelectedField
        {
            get
            {
                if (SelectedNode != null && SelectedNode.Tag is PdfInteractiveFormField)
                    return (PdfInteractiveFormField)SelectedNode.Tag;
                return null;
            }
            set
            {
                if (value == null)
                {
                    if (SelectedNode != null)
                        SelectedNode = null;
                }
                else
                {
                    if (!_fieldToNodeTable.ContainsKey(value))
                        RefreshInteractiveFormTree();
                    if (_fieldToNodeTable.ContainsKey(value))
                    {
                        if (SelectedNode != _fieldToNodeTable[value])
                            SelectedNode = _fieldToNodeTable[value];
                    }
                    else
                    {
                        if (SelectedNode != null)
                            SelectedNode = null;
                    }
                }
            }
        }

        bool _showOnlyExportableFields = false;
        /// <summary>
        /// Gets or sets a value indicating whether the tree view must show only form fields, which can export their values.
        /// </summary>
        /// <value>
        /// Default value is <b>false</b>.
        /// </value>
        [DefaultValue(false)]
        public bool ShowOnlyExportableFields
        {
            get
            {
                return _showOnlyExportableFields;
            }
            set
            {
                _showOnlyExportableFields = value;
            }
        }

        bool _showOnlyResettableFields = false;
        /// <summary>
        /// Gets or sets a value indicating whether the tree view must show only form fields, which can reset their values.
        /// </summary>
        /// <value>
        /// Default value is <b>false</b>.
        /// </value>
        [DefaultValue(false)]
        public bool ShowOnlyResettableFields
        {
            get
            {
                return _showOnlyResettableFields;
            }
            set
            {
                _showOnlyResettableFields = value;
            }
        }

        #endregion



        #region Methods

        #region PUBLIC

        /// <summary>
        /// Refreshes the interactive form tree.
        /// </summary>
        public void RefreshInteractiveFormTree()
        {
            BeginUpdate();
            try
            {
                // clear the tree
                Nodes.Clear();
                _fieldToNodeTable.Clear();
                _fieldToImageTable.Clear();

                // if annotation tool is used as a source of interactive form
                if (AnnotationTool != null && AnnotationTool.ImageViewer != null)
                {
                    // get images
                    ImageCollection images = AnnotationTool.ImageViewer.Images;
                    // for each image
                    for (int i = 0; i < images.Count; i++)
                    {
                        // get PDF page, which is associated with image
                        PdfPage page = PdfDocumentController.GetPageAssociatedWithImage(images[i]);
                        // if PDF page exists
                        if (page != null)
                        {
                            // add interactive fields of PDF page to the tree control
                            AddFields(page, images[i], string.Format("Page {0}", i + 1));
                        }
                    }
                }
                // if PDF document has interactive form
                else if (InteractiveForm != null)
                {
                    // get PDF document
                    PdfDocument document = InteractiveForm.Document;
                    // for each PDF page
                    for (int i = 0; i < document.Pages.Count; i++)
                        // add interactive fields of PDF page to the tree control
                        AddFields(document.Pages[i], null, string.Format("Page {0}", i + 1));
                }
            }
            finally
            {
                EndUpdate();
            }
        }

        /// <summary>
        /// Updates name of the field. 
        /// </summary>
        /// <param name="field">The field.</param>
        public void UpdateField(PdfInteractiveFormField field)
        {
            TreeNode node = null;

            if (_fieldToNodeTable.TryGetValue(field, out node))
                node.Text = GetFieldName(field);
        }

        #endregion


        #region PROTECTED

        /// <summary>
        /// Disposes the PdfInteractiveFormFieldTree.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (_interactiveForm != null)
            {
                _isInteractiveFormChangedSubscribed = false;
                _interactiveForm.FieldTreeChanged -= new CollectionChangeEventHandler<PdfInteractiveFormField>(InteractiveForm_FieldTreeChanged);
            }
            else if (_annotationTool != null)
            {
                foreach (PdfDocument document in _annotationTool.DocumentSet)
                {
                    if (document.InteractiveForm != null)
                        UnsubscribeFromInteractiveFormEvents(document.InteractiveForm);
                }
                _annotationTool.DocumentSet.Changed -= new EventHandler<ObjectSetListenerEventArgs<PdfDocument>>(DocumentSet_Changed);
            }
        }

        /// <summary>
        /// Raises the <see cref="E:AfterSelect" /> event.
        /// </summary>
        protected override void OnAfterSelect(TreeViewEventArgs e)
        {
            // if tree view is enabled
            if (Enabled)
            {
                base.OnAfterSelect(e);
                // update focused field
                SetFocusedFieldInAnnotationTool(SelectedField);
            }
        }

        /// <summary>
        /// Raises the <see cref="E:NodeMouseClick" /> event.
        /// </summary>
        protected override void OnNodeMouseClick(TreeNodeMouseClickEventArgs e)
        {
            // if node must be selected
            if (e.Button == MouseButtons.Right)
                SelectedNode = e.Node;
            
            // if interactive must be selected
            if (e.Button == MouseButtons.Left && e.Node != null)
            {
                // get interactive field
                PdfInteractiveFormField field = e.Node.Tag as PdfInteractiveFormField;
                // if interactive fild can be selected
                if (field != null && AnnotationTool != null && AnnotationTool.AllowMultipleSelection)
                {
                    SetFocusedFieldInAnnotationTool(field);
                    AnnotationTool.PerformSelection(AnnotationTool.GetAnnotationViewsAssociatedWith(field));
                }
            }

            base.OnNodeMouseClick(e);
        }

        /// <summary>
        /// Raises the <see cref="E:DrawNode" /> event.
        /// </summary>
        /// <param name="e">The <see cref="DrawTreeNodeEventArgs"/> instance
        /// containing the event data.</param>
        protected override void OnDrawNode(DrawTreeNodeEventArgs e)
        {
            e.DrawDefault = true;
            base.OnDrawNode(e);
        }

        #endregion


        #region INTERNAL

        /// <summary>
        /// Returns the image key, which is associated with the specified field.
        /// </summary>
        /// <param name="field">The field.</param>
        /// <returns>The image key, which is associated with the specified field.</returns>
        internal static string GetImageKey(PdfInteractiveFormField field)
        {
            if (field.IsTerminalField)
            {
                if (field is PdfInteractiveFormCheckBoxField)
                    return "CheckBoxField";
                if (field is PdfInteractiveFormBarcodeField)
                    return "BarcodeField";
                if (field is PdfInteractiveFormPushButtonField)
                    return "ButtonField";
                if (field is PdfInteractiveFormListBoxField)
                    return "ListBoxField";
                if (field is PdfInteractiveFormRadioButtonField)
                    return "RadioButtonField";
                if (field is PdfInteractiveFormSignatureField)
                    return "SignatureField";
                if (field is PdfInteractiveFormTextField)
                    return "TextField";
                if (field is PdfInteractiveFormComboBoxField)
                    return "ComboBoxField";
                return "InteractiveField";
            }
            if (field is PdfInteractiveFormCheckBoxGroupField)
                return "CheckBoxGroup";
            if (field is PdfInteractiveFormRadioButtonGroupField)
                return "RadioButtonGroup";
            return "FieldGroup";
        }

        #endregion


        #region PRIVATE

        /// <summary>
        /// Sets the focused field in annotation tool.
        /// </summary>
        /// <param name="field">The field.</param>
        private void SetFocusedFieldInAnnotationTool(PdfInteractiveFormField field)
        {
            if (AnnotationTool != null && field != null)
            {
                if (!field.IsTerminalField)
                {
                    PdfAnnotation[] annotations = field.GetAnnotations();
                    if (annotations.Length > 0)
                        field = ((PdfWidgetAnnotation)annotations[0]).Field;
                    else
                        return;
                }
                if (_fieldToImageTable.ContainsKey(field))
                {
                    if (AnnotationTool.ImageViewer.Image != _fieldToImageTable[field])
                    {
                        int index = AnnotationTool.ImageViewer.Images.IndexOf(_fieldToImageTable[field]);
                        if (index < 0)
                            return;
                        AnnotationTool.ImageViewer.FocusedIndex = index;
                    }
                    if (AnnotationTool.FocusedField != field)
                    {
                        AnnotationTool.FocusedField = field;
                        AnnotationTool.ScrollToFocusedItem();
                    }
                }
                else
                {
                    AnnotationTool.FocusedField = field;
                }
            }
        }

        /// <summary>
        /// Handles the Changed event of the DocumentSet object.
        /// </summary>
        private void DocumentSet_Changed(
            object sender,
            ObjectSetListenerEventArgs<PdfDocument> e)
        {
            RefreshInteractiveFormTree();
            foreach (PdfDocument document in e.RemovedElements)
            {
                if (document.InteractiveForm != null)
                    UnsubscribeFromInteractiveFormEvents(document.InteractiveForm);
            }
            foreach (PdfDocument document in e.NewElements)
            {
                if (document.InteractiveForm != null)
                    SubscribeToInteractiveFormEvents(document.InteractiveForm);
            }
        }

        /// <summary>
        /// Adds the fields that locates on specified image.
        /// </summary>
        /// <param name="page">A PDF page, which contains interactive fields.</param>
        /// <param name="image">An image, which is associated with PDF page.</param>
        /// <param name="imageName">An image name.</param>
        private void AddFields(PdfPage page, VintasoftImage image, string imageName)
        {
            // if PDF document has interactive form
            if (page.Document.InteractiveForm != null)
            {
                // if control not subscribed to the Changed event of interactive form
                if (!_isInteractiveFormChangedSubscribed)
                    // subscribe to the Changed event of interactive form
                    SubscribeToInteractiveFormEvents(page.Document.InteractiveForm);

                TreeNodeCollection rootCollection = Nodes;
                // if form fields must be grouped by pages
                if (_groupFormFieldsByPages)
                {
                    TreeNode pageNode = new TreeNode(imageName);
                    pageNode.ImageKey = "FieldGroup";
                    pageNode.SelectedImageKey = pageNode.ImageKey;
                    Nodes.Add(pageNode);
                    rootCollection = pageNode.Nodes;
                }

                // get interactive fields located on PDF page
                PdfInteractiveFormField[] fields = page.Document.InteractiveForm.GetFieldsLocatedOnPage(page);
                // for each interactive field
                foreach (PdfInteractiveFormField field in fields)
                {
                    // if field is associated with image
                    if (image != null)
                        // save information about image
                        _fieldToImageTable[field] = image;
                    // add field to the tree view
                    AddField(rootCollection, field);
                }
            }
        }

        /// <summary>
        /// Adds the field to the tree view.
        /// </summary>
        /// <param name="rootCollection">The root collection.</param>
        /// <param name="field">The field.</param>
        private TreeNode AddField(TreeNodeCollection rootCollection, PdfInteractiveFormField field)
        {
            if (_fieldToNodeTable.ContainsKey(field))
                return _fieldToNodeTable[field];

            TreeNodeCollection parentNodes;
            // get field name
            string name = GetFieldName(field);
            // if form field must be grouped by page
            if (_groupFormFieldsByPages)
            {
                if (!field.IsTerminalField && !(field is PdfInteractiveFormSwitchableButtonGroupField))
                    return null;

                parentNodes = rootCollection;
                if (field is PdfInteractiveFormSwitchableButtonField && field.Parent != null)
                {
                    // add field
                    TreeNode parentNode = AddField(rootCollection, field.Parent);
                    if (parentNode != null)
                        parentNodes = parentNode.Nodes;
                }

                // if field must be suppressed
                if (SuppressFieldAddition(field))
                    return null;
            }
            else
            {
                if (field.Parent != null)
                {
                    // add field
                    TreeNode parentNode = AddField(rootCollection, field.Parent);
                    if (parentNode == null)
                        return null;
                    parentNodes = parentNode.Nodes;
                }
                else
                {
                    parentNodes = rootCollection;
                }

                // if field must be suppressed
                if (SuppressFieldAddition(field))
                    return null;
            }

            // create node
            TreeNode node = new TreeNode(name);
            // get tree node image
            node.ImageKey = GetImageKey(field);
            node.SelectedImageKey = node.ImageKey;
            node.Tag = field;
            _fieldToNodeTable[field] = node;
            parentNodes.Add(node);
            return node;
        }

        /// <summary>
        /// Returns the name of the field.
        /// </summary>
        /// <param name="field">The field.</param>
        /// <returns>The name of the field.</returns>
        private string GetFieldName(PdfInteractiveFormField field)
        {
            string name;
            if (_groupFormFieldsByPages)
            {
                if (field.PartialName == "" && field is PdfInteractiveFormSwitchableButtonField)
                    name = ((PdfInteractiveFormSwitchableButtonField)field).ButtonValue;
                else
                    name = field.FullyQualifiedName;
            }
            else
            {
                name = field.PartialName;
                if (name == "")
                {
                    if (field is PdfInteractiveFormSwitchableButtonField)
                        name = ((PdfInteractiveFormSwitchableButtonField)field).ButtonValue;
                }
            }

            return name;
        }

        /// <summary>
        /// Determines that the field addition must be suppressed.
        /// </summary>
        /// <param name="field">The field.</param>
        private bool SuppressFieldAddition(PdfInteractiveFormField field)
        {
            if (ShowOnlyExportableFields)
            {
                if (IsElementOfSwitchableGroup(field))
                    return true;
                if (field.IsNoExport)
                    return true;
            }

            if (ShowOnlyResettableFields)
            {
                if (IsElementOfSwitchableGroup(field))
                    return true;
                if (field.GetType() == typeof(PdfInteractiveFormPushButtonField))
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Determines that the specified field is an element of a switchable group,
        /// e.g., a single radio button in a group of radio buttons.
        /// </summary>
        /// <param name="field">The field.</param>
        private bool IsElementOfSwitchableGroup(PdfInteractiveFormField field)
        {
            return
                field is PdfInteractiveFormSwitchableButtonField &&
                field.Parent is PdfInteractiveFormSwitchableButtonGroupField;
        }

        /// <summary>
        /// Subscribes to interactive form events.
        /// </summary>
        /// <param name="interactiveForm">The interactive form.</param>
        private void SubscribeToInteractiveFormEvents(PdfDocumentInteractiveForm interactiveForm)
        {
            interactiveForm.FieldTreeChanged += new CollectionChangeEventHandler<PdfInteractiveFormField>(InteractiveForm_FieldTreeChanged);
            _isInteractiveFormChangedSubscribed = true;
        }

        /// <summary>
        /// Unsubscribes from interactive form events.
        /// </summary>
        /// <param name="interactiveForm">The interactive form.</param>
        private void UnsubscribeFromInteractiveFormEvents(PdfDocumentInteractiveForm interactiveForm)
        {
            _isInteractiveFormChangedSubscribed = false;
            interactiveForm.FieldTreeChanged -= new CollectionChangeEventHandler<PdfInteractiveFormField>(InteractiveForm_FieldTreeChanged);
        }

        /// <summary>
        /// Updates interactive form field tree.
        /// </summary>
        private void InteractiveForm_FieldTreeChanged(object sender, CollectionChangeEventArgs<PdfInteractiveFormField> e)
        {
            RefreshInteractiveFormTree();
        }

        #endregion

        #endregion

    }
}
