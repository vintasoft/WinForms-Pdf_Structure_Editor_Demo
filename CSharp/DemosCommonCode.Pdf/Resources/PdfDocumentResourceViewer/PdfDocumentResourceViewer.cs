using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

using Vintasoft.Imaging.Pdf;
using Vintasoft.Imaging.Pdf.Tree;
using Vintasoft.Imaging.Pdf.Tree.Annotations;
using Vintasoft.Imaging.Pdf.Tree.Patterns;


namespace DemosCommonCode.Pdf
{
    /// <summary>
    /// A tree view that allows to view resources of PDF document.
    /// </summary>
    public partial class PdfDocumentResourceViewer : TreeView
    {

        #region Nested class

        /// <summary>
        /// A tree node comparer.
        /// </summary>
        private class TreeNodeComparer : IComparer
        {

            /// <summary>
            /// The resource viewer;
            /// </summary>
            PdfDocumentResourceViewer _viewer;



            /// <summary>
            /// Initializes a new instance of the <see cref="TreeNodeComparer"/> class.
            /// </summary>
            /// <param name="viewer">The viewer.</param>
            public TreeNodeComparer(PdfDocumentResourceViewer viewer)
            {
                _viewer = viewer;
            }



            /// <summary>
            /// Compare tree nodes.
            /// </summary>
            /// <param name="x">First object.</param>
            /// <param name="y">Second object.</param>
            public int Compare(object x, object y)
            {
                TreeNode firstNode = (TreeNode)x;
                TreeNode secondNode = (TreeNode)y;

                PdfResource firstNodeResource = _viewer.GetResourceAsTreeNode<PdfResource>(firstNode);
                PdfResource secondNodeResource = _viewer.GetResourceAsTreeNode<PdfResource>(secondNode);

                if (firstNodeResource != null && secondNodeResource != null)
                    return firstNodeResource.ObjectNumber.CompareTo(secondNodeResource.ObjectNumber);

                return 0;
            }
        }

        #endregion



        #region Constants

        /// <summary>
        /// Name of the private node.
        /// </summary>
        const string PRIVATE_NODE_NAME = "PRIVATE";

        /// <summary>
        /// Name of the resource node.
        /// </summary>
        const string RESOURCES_NODE_NAME = "Resources";

        /// <summary>
        /// Name of the annotation node.
        /// </summary>
        const string ANNOTATIONS_NODE_NAME = "Annotations";

        #endregion



        #region Fields

        /// <summary>
        /// Indicates whether the resource can be removed from additional resources.
        /// </summary>
        bool _removeAdditionalResources = true;

        /// <summary>
        /// Dictionary: the private tree node => PDF resources.
        /// </summary>
        Dictionary<TreeNode, List<Object>> _privateTreeNodeToResource = new Dictionary<TreeNode, List<object>>();

        /// <summary>
        /// Dictionary: the tree node => PDF resource.
        /// </summary>
        Dictionary<TreeNode, object> _treeNodeToResource = new Dictionary<TreeNode, object>();
        /// <summary>
        /// Dictionary: PDF resource => the tree node.
        /// </summary>
        Dictionary<object, TreeNode> _resourceToTreeNode = new Dictionary<object, TreeNode>();

        /// <summary>
        /// Dictionary: tree node collection => tree node name => tree node.
        /// </summary>
        Dictionary<TreeNodeCollection, Dictionary<string, TreeNode>> _nameToTreeNode = new Dictionary<TreeNodeCollection, Dictionary<string, TreeNode>>();

        #endregion



        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PdfDocumentResourceViewer"/> class.
        /// </summary>
        public PdfDocumentResourceViewer()
        {
            InitializeComponent();

            this.TreeViewNodeSorter = new TreeNodeComparer(this);
        }

        #endregion



        #region Properties

        bool _showFormResources = true;
        /// <summary>
        /// Gets or sets a value indicating whether the resource viewer should
        /// show the image resources of PDF document.
        /// </summary>
        [Browsable(true)]
        [Description("Indicates whether the resource viewer should show the image resources of PDF document")]
        [DefaultValue(true)]
        public bool ShowFormResources
        {
            get
            {
                return _showFormResources;
            }
            set
            {
                if (_showFormResources != value)
                {
                    _showFormResources = value;

                    UpdateTreeView();
                }
            }
        }

        bool _showImageResources = true;
        /// <summary>
        /// Gets or sets a value indicating whether the resource viewer should
        /// show the form resources of PDF document.
        /// </summary>
        [Browsable(true)]
        [Description("Indicates whether the resource viewer should show the form resources of PDF document.")]
        [DefaultValue(true)]
        public bool ShowImageResources
        {
            get
            {
                return _showImageResources;
            }
            set
            {
                if (_showImageResources != value)
                {
                    _showImageResources = value;

                    UpdateTreeView();
                }
            }
        }

        object _selectedObject = null;
        /// <summary>
        /// Gets or sets the object, which should be viewed.
        /// </summary>
        /// <value>
        /// Default value is <b>null</b>.<br />
        /// <br />
        /// Supported value types: <see cref="PdfDocument"/>, <see cref="PdfPage"/>,
        /// <see cref="PdfImageResource"/>, <see cref="PdfFormXObjectResource"/>.
        /// </value>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public object SelectedObject
        {
            get
            {
                return _selectedObject;
            }
            set
            {
                _selectedObject = value;

                UpdateTreeView();
            }
        }

        /// <summary>
        /// Gets or sets the selected PDF resource.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public PdfResource SelectedResource
        {
            get
            {
                return GetResourceAsTreeNode<PdfResource>(SelectedNode);
            }
            set
            {
                if (SelectedResource != value)
                    SelectedNode = FindNode(value);
            }
        }

        /// <summary>
        /// Gets the selected PDF page.
        /// </summary>
        [Browsable(false)]
        public PdfPage SelectedPage
        {
            get
            {
                return GetResourceAsTreeNode<PdfPage>(SelectedNode);
            }
        }

        PdfResourceTreeViewType _treeType = PdfResourceTreeViewType.Hierarchical;
        /// <summary>
        /// Gets or sets a value that determines how the PDF resource tree must be shown.
        /// </summary>
        /// <value>
        /// Default value is <b>PdfResourceTreeType.Hierarchical</b>.
        /// </value>
        [Browsable(true)]
        [Description("A value that determines how the PDF resource tree must be shown.")]
        [DefaultValue(PdfResourceTreeViewType.Hierarchical)]
        public PdfResourceTreeViewType TreeType
        {
            get
            {
                return _treeType;
            }
            set
            {
                _treeType = value;

                UpdateTreeView();
            }
        }

        List<PdfResource> _additionalResources = null;
        /// <summary>
        /// Gets or sets the additional resources.
        /// </summary>
        /// <value>
        /// Default value is <b>null</b>.
        /// </value>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public List<PdfResource> AdditionalResources
        {
            get
            {
                return _additionalResources;
            }
            set
            {
                if (_additionalResources != value)
                {
                    _additionalResources = value;

                    UpdateTreeView();
                }
            }
        }

        #endregion



        #region Methods

        #region PUBLIC

        /// <summary>
        /// Updates the tree view.
        /// </summary>
        public void UpdateTreeView()
        {
            BeginUpdate();
            try
            {
                Nodes.Clear();
                _treeNodeToResource.Clear();
                _resourceToTreeNode.Clear();
                _nameToTreeNode.Clear();
                _privateTreeNodeToResource.Clear();

                Add(Nodes, _selectedObject);

                _removeAdditionalResources = false;
                Add(Nodes, _additionalResources);
                _removeAdditionalResources = true;
            }
            finally
            {
                EndUpdate();
            }
        }

        #endregion


        #region PROTECTED

        /// <summary>
        /// Builds new level of tree before node expands.
        /// </summary>
        protected override void OnBeforeExpand(TreeViewCancelEventArgs e)
        {
            base.OnBeforeExpand(e);

            ExpandPrivateNodes(e.Node, false);           
        }


        #endregion


        #region PRIVATE

        /// <summary>
        /// Expands the private nodes.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <param name="expandAll">if set to <c>true</c> [expand all].</param>
        private void ExpandPrivateNodes(TreeNode root, bool expandAll)
        {
            // if tree node contains private node
            if (root.Nodes.Count == 1 &&
                root.Nodes[0].Text == PRIVATE_NODE_NAME)
            {
                // get private tree node
                TreeNode node = root.Nodes[0];

                List<object> resources = null;
                // get private tree node PDF resources
                if (_privateTreeNodeToResource.TryGetValue(node, out resources))
                {
                    // remove private tree node
                    root.Nodes.Clear();
                    // remove private tree node from dictionary
                    _privateTreeNodeToResource.Remove(node);

                    // add PDF resources to parent node
                    Add(root.Nodes, resources);
                }
            }

            if(expandAll)
            {
                foreach (TreeNode node in root.Nodes)
                    ExpandPrivateNodes(node, true);
            }
        }


        /// <summary>
        /// Adds the object to the specified collection.
        /// </summary>
        /// <param name="rootCollection">The root collection.</param>
        /// <param name="obj">The object.</param>
        private void Add(TreeNodeCollection rootCollection, object obj)
        {
            // if root tree node contains the current object
            if (TreeType == PdfResourceTreeViewType.Linear && FindNode(obj) != null)
                return;

            // if PDF document resources must be added
            if (obj is PdfDocument)
            {
                PdfDocument document = obj as PdfDocument;

                int pageIndex = 1;
                // for ecah PDF page in PDF documetn
                foreach (PdfPage page in document.Pages)
                {
                    TreeNodeCollection pageNodeCollection = rootCollection;
                    TreeNode pageNode = null;
                    // if sub node must be added
                    if (TreeType == PdfResourceTreeViewType.Hierarchical)
                    {
                        // get node name
                        string nodeName = string.Format("Page {0}", pageIndex);
                        // add node
                        pageNode = AddTreeNode(rootCollection, nodeName, page);
                        pageNodeCollection = pageNode.Nodes;
                        pageIndex++;
                    }

                    // add page resources
                    AddPageResources(pageNodeCollection, page);

                    // if sub node must be removed
                    if (pageNode != null &&
                        pageNode.Nodes.Count == 0 &&
                        ShowImageResources &&
                        !ShowFormResources)
                    {
                        pageNode.Remove();
                    }
                }
            }
            // if PDF page resources must be added
            else if (obj is PdfPage)
            {
                AddPageResources(rootCollection, (PdfPage)obj);
            }
            // if PDF image resources must be added
            else if (obj is PdfImageResource)
            {
                PdfImageResource imageResource = (PdfImageResource)obj;
                // if image reosurces must be shown
                if (ShowImageResources)
                {
                    // add image resource node
                    TreeNode node = AddResourceTreeNode(rootCollection, imageResource);

                    // if image resource contains soft mask
                    if (imageResource.SoftMask != null)
                        AddPrivateTreeNode(node, imageResource.SoftMask);

                    // if image reosurce contains stencil mask
                    if (imageResource.StencilMask != null)
                        AddPrivateTreeNode(node, imageResource.StencilMask);
                }
            }
            // if PDF form XObject resource must be added
            else if (obj is PdfFormXObjectResource)
            {
                PdfFormXObjectResource formXObjectResource = (PdfFormXObjectResource)obj;

                // if form resources must be shown
                if (ShowFormResources)
                {
                    // add form source node
                    TreeNode node = AddResourceTreeNode(rootCollection, formXObjectResource);
                    // add form XObject resources
                    AddPrivateTreeNode(node, formXObjectResource.Resources);
                }
                else
                {
                    // add form XObject resources
                    Add(rootCollection, formXObjectResource.Resources);
                }
            }
            // if PDF annotation must be added
            else if (obj is PdfAnnotation)
            {
                PdfAnnotation annotationResource = (PdfAnnotation)obj;

                // if annotation contains appearance
                if (annotationResource.Appearances != null)
                {
                    // get annotation appearance
                    PdfFormXObjectResource[] appearances = annotationResource.Appearances.GetAllAppearances();
                    // add annotation apeearances
                    Add(rootCollection, appearances);
                }
            }
            // if PDF resource must be added
            else if (obj is PdfResources)
            {
                PdfResources resources = (PdfResources)obj;

                // if PDF resource contains XObject resources
                if (resources.XObjectResources != null &&
                    resources.XObjectResources.Count > 0)
                {
                    Add(rootCollection, resources.XObjectResources.Values);
                }
                // if PDF resource contains parent
                if (resources.Patterns != null &&
                    resources.Patterns.Count > 0)
                {
                    Add(rootCollection, resources.Patterns.Values);
                }
            }
            // if tilling pattern must be added
            else if (obj is TilingPattern)
            {
                TilingPattern tilingPattern = (TilingPattern)obj;
                Add(rootCollection, tilingPattern.Resources);
            }
            // if current object is collection
            else if (obj is IEnumerable)
            {
                IEnumerable array = (IEnumerable)obj;
                foreach (object item in array)
                    Add(rootCollection, item);
            }
        }

        /// <summary>
        /// Adds the private tree node.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <param name="resource">The resource.</param>
        private void AddPrivateTreeNode(TreeNode node, object resource)
        {
            // if resource viewer must be hierarchical
            if (TreeType == PdfResourceTreeViewType.Hierarchical)
            {
                // get private node
                TreeNode privateNode = FindNode(node.Nodes, PRIVATE_NODE_NAME);
                // if private node does not exist
                if (privateNode == null)
                {
                    // create pravete node
                    privateNode = AddTreeNode(node.Nodes, PRIVATE_NODE_NAME, resource);
                }

                List<Object> resources = null;
                if (!_privateTreeNodeToResource.TryGetValue(privateNode, out resources))
                {
                    resources = new List<object>();
                    _privateTreeNodeToResource.Add(privateNode, resources);
                }
                resources.Add(resource);
            }
            else if (TreeType == PdfResourceTreeViewType.Linear)
            {
                TreeNodeCollection nodeCollection = Nodes;
                if (node.Parent != null)
                    nodeCollection = node.Parent.Nodes;
                Add(nodeCollection, resource);
            }
            else
                throw new InvalidOperationException();
        }

        /// <summary>
        /// Adds the resource tree node.
        /// </summary>
        /// <param name="collection">The collection.</param>
        /// <param name="resource">The resource.</param>
        private TreeNode AddResourceTreeNode(TreeNodeCollection collection, PdfResource resource)
        {
            // get the resource name
            string nodeName = GetResourceName(resource);
            // find a resource by its name
            TreeNode node = FindNode(collection, nodeName);
            // if resource is not found
            if (node == null)
            {
                // add tree node to collection
                node = AddTreeNode(collection, nodeName, resource);
            }

            if (_removeAdditionalResources)
                PdfResourcesViewerForm.RemoveAdditionalResource(resource);

            return node;
        }

        /// <summary>
        /// Adds the tree node.
        /// </summary>
        /// <param name="collection">The collection.</param>
        /// <param name="node">The node.</param>
        private void AddTreeNode(TreeNodeCollection collection, TreeNode node, object resource)
        {
            collection.Add(node);

            Dictionary<string, TreeNode> nameToNode = null;
            if (!_nameToTreeNode.TryGetValue(collection, out nameToNode))
            {
                nameToNode = new Dictionary<string, TreeNode>();
                _nameToTreeNode.Add(collection, nameToNode);
            }
            nameToNode.Add(node.Text, node);

            if (resource != null)
            {
                _treeNodeToResource.Add(node, resource);
                if (!_resourceToTreeNode.ContainsKey(resource))
                    _resourceToTreeNode[resource] = node;
            }
        }

        /// <summary>
        /// Adds the tree node.
        /// </summary>
        /// <param name="collection">The collection.</param>
        /// <param name="node">The node.</param>
        private TreeNode AddTreeNode(TreeNodeCollection collection, string nodeName, object resource)
        {
            TreeNode node = new TreeNode(nodeName);
            AddTreeNode(collection, node, resource);
            return node;
        }

        /// <summary>
        /// Adds all resources of PDF page to the tree node collection.
        /// </summary>
        /// <param name="rootCollection">The root tree node collection.</param>
        /// <param name="page">A PDF page.</param>
        private void AddPageResources(TreeNodeCollection rootCollection, PdfPage page)
        {
            TreeNodeCollection resourcesNodeCollection = rootCollection;
            TreeNode resourcesNode = null;
            if (TreeType == PdfResourceTreeViewType.Hierarchical)
            {
                string resourcesNodeName = RESOURCES_NODE_NAME;
                resourcesNode = FindNode(rootCollection, resourcesNodeName);
                if (resourcesNode == null)
                    resourcesNode = AddTreeNode(rootCollection, resourcesNodeName, null);
                resourcesNodeCollection = resourcesNode.Nodes;
            }
            Add(resourcesNodeCollection, page.Resources);
            if (resourcesNodeCollection.Count == 0 && resourcesNode != null)
                resourcesNode.Remove();

            PdfAnnotationList annotationList = page.Annotations;
            if (ShowFormResources && annotationList != null && annotationList.Count > 0)
            {
                if (TreeType == PdfResourceTreeViewType.Hierarchical)
                {
                    string annotationsNodeName = ANNOTATIONS_NODE_NAME;
                    TreeNode annotationsNode = FindNode(rootCollection, annotationsNodeName);
                    if (annotationsNode == null)
                        annotationsNode = AddTreeNode(rootCollection, annotationsNodeName, null);
                    TreeNodeCollection annotationsNodeCollection = annotationsNode.Nodes;

                    foreach (PdfAnnotation annotation in annotationList)
                    {
                        if (annotation.Appearances != null)
                        {
                            PdfFormXObjectResource[] appearances = annotation.Appearances.GetAllAppearances();
                            if (appearances.Length != 0)
                            {
                                string annotationName = PdfDemosTools.GetAnnotationName(annotation);
                                string nodeName = GetResourceName(annotation.GetType(), annotation.ObjectNumber);
                                if (!string.IsNullOrEmpty(annotationName))
                                    nodeName += string.Format(" {0}", annotationName);
                                TreeNode node = AddTreeNode(annotationsNodeCollection, nodeName, annotation);

                                AddPrivateTreeNode(node, appearances);
                            }
                        }
                    }

                    if (annotationsNodeCollection.Count == 0)
                        annotationsNode.Remove();
                }
                else
                {
                    foreach (PdfAnnotation annotation in annotationList)
                        Add(rootCollection, annotation);
                }
            }
        }

        /// <summary>
        /// Returns the resource name.
        /// </summary>
        /// <param name="resource">The resource.</param>
        /// <returns>The resource name.</returns>
        private string GetResourceName(PdfResource resource)
        {
            return GetResourceName(resource.GetType(), resource.ObjectNumber);
        }

        /// <summary>
        /// Returns the resource name.
        /// </summary>
        /// <param name="type">The type of resource.</param>
        /// <param name="objectNumber">The number of resource.</param>
        private string GetResourceName(Type type, int objectNumber)
        {
            string resourceName;
            if (objectNumber > 0)
            {
                resourceName = string.Format("{0}: {1}",
                   type.Name, objectNumber.ToString());
            }
            else
            {
                resourceName = string.Format("{0}: Inline", type.Name);
            }

            if (resourceName.StartsWith("Pdf", StringComparison.InvariantCultureIgnoreCase))
                resourceName = resourceName.Substring(3);

            return resourceName;
        }

        /// <summary>
        /// Finds the node with the specified text in a tree node collection.
        /// </summary>
        /// <param name="nodes">A tree node collection, where node must be searched.</param>
        /// <param name="text">A text, which nust present in a node.</param>
        private TreeNode FindNode(TreeNodeCollection nodes, string text)
        {
            Dictionary<string, TreeNode> nameToNode = null;
            if (_nameToTreeNode.TryGetValue(nodes, out nameToNode))
                if (nameToNode.ContainsKey(text))
                    return nameToNode[text];

            return null;
        }

        /// <summary>
        /// Finds the node, which contains the resource.
        /// </summary>
        /// <param name="resource">The resource.</param>
        private TreeNode FindNode(object resource)
        {
            if (resource == null)
                return null;

            TreeNode result = null;
            if (_resourceToTreeNode.TryGetValue(resource, out result))
                return result;
            return null;
        }

        /// <summary>
        /// Returns the resource as tree node.
        /// </summary>
        /// <typeparam name="T">The resource type.</typeparam>
        /// <param name="node">The node.</param>
        private T GetResourceAsTreeNode<T>(TreeNode node)
        {
            if (node != null)
            {
                object resource = null;
                if (_treeNodeToResource.TryGetValue(node, out resource))
                {
                    if (resource is T)
                        return (T)resource;
                }
            }

            return default(T);
        }

        /// <summary>
        /// Sets the selected resource.
        /// </summary>
        /// <param name="page">The current page.</param>
        /// <param name="resource">The resource.</param>
        /// <exception cref="NotImplementedException"></exception>
        internal void SetSelectedResource(PdfPage page, PdfResource resource)
        {
            TreeNode pagesNode = FindNode(page);
            ExpandPrivateNodes(pagesNode, true);
            SelectedResource = resource;
        }

        #endregion

        #endregion

    }
}
