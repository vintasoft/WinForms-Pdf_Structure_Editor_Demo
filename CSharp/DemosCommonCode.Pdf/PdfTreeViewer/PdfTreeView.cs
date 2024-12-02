using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Windows.Forms;

using Vintasoft.Imaging.Pdf;
using Vintasoft.Imaging.Pdf.Tree;
using Vintasoft.Imaging.Pdf.BasicTypes;

namespace DemosCommonCode.Pdf
{
    /// <summary>
    /// A tree view that displays tree of PDF document.
    /// </summary>
    public class PdfTreeView : TreeView
    {

        #region Nested Classes

        /// <summary>
        /// Object of tree node.
        /// </summary>
        public struct TreeNodeObject
        {
            TreeNode _parentNode;
            /// <summary>
            /// Gets or sets the parent node.
            /// </summary>
            public TreeNode ParentNode
            {
                get
                {
                    return _parentNode;
                }
                set
                {
                    _parentNode = value;
                }
            }

            object _parentObject;
            /// <summary>
            /// Gets or sets the parent object.
            /// </summary>
            public object ParentObject
            {
                get
                {
                    return _parentObject;
                }
                set
                {
                    _parentObject = value;
                }
            }

            object _propertyObject;
            /// <summary>
            /// Gets or sets the property object.
            /// </summary>
            public object PropertyObject
            {
                get
                {
                    return _propertyObject;
                }
                set
                {
                    _propertyObject = value;
                }
            }

            PropertyInfo _property;
            /// <summary>
            /// Gets or sets the property.
            /// </summary>
            public PropertyInfo Property
            {
                get
                {
                    return _property;
                }
                set
                {
                    _property = value;
                }
            }
        }

        #endregion



        #region Constants

        /// <summary>
        /// Name of the private node.
        /// </summary>
        private const string PRIVATE_TAG_NAME = "_PRIVATE";

        #endregion



        #region Fields

        /// <summary>
        /// The context menu.
        /// </summary>
        ContextMenuStrip _contextMenu;

        /// <summary>
        /// The container of context menu.
        /// </summary>
        IContainer _components;

        /// <summary>
        /// The "Set selected value to null" menu item of context menu.
        /// </summary>
        ToolStripMenuItem _setSelectedObjectValueToNullMenuItem;

        #endregion



        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PdfTreeView"/> class.
        /// </summary>
        public PdfTreeView()
            : base()
        {
            InitializeComponent();
        }

        #endregion



        #region Properties

        object _rootObject;
        /// <summary>
        /// Gets or sets the root object.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public object RootObject
        {
            get
            {
                return _rootObject;
            }
            set
            {
                // set new root object
                _rootObject = value;
                // clear node collection
                Nodes.Clear();
                // if new root object is not empty
                if (_rootObject != null)
                {
                    // create root node
                    TreeNode rootNode = CreatePdfTreeNode(new TreeNode("PdfDocument"), _rootObject);
                    rootNode.Tag = _rootObject;
                    // add root node
                    Nodes.Add(rootNode);
                }
            }
        }

        /// <summary>
        /// Gets the selected object.
        /// </summary>
        public object SelectedObject
        {
            get
            {
                if (SelectedNode != null)
                    return SelectedNode.Tag;
                return null;
            }
        }

        #endregion



        #region Designer

        private void InitializeComponent()
        {
            this._components = new System.ComponentModel.Container();
            this._contextMenu = new System.Windows.Forms.ContextMenuStrip(this._components);
            this._setSelectedObjectValueToNullMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._contextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // _contextMenu
            // 
            this._contextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._setSelectedObjectValueToNullMenuItem});
            this._contextMenu.Name = "contextMenu";
            this._contextMenu.Size = new System.Drawing.Size(241, 26);
            this._contextMenu.Opening += new CancelEventHandler(ContextMenu_Opening);
            // 
            // _setSelectedObjectValueToNullMenuItem
            // 
            this._setSelectedObjectValueToNullMenuItem.Name = "_setSelectedObjectValueToNullMenuItem";
            this._setSelectedObjectValueToNullMenuItem.Size = new System.Drawing.Size(240, 22);
            this._setSelectedObjectValueToNullMenuItem.Text = "Set selected object value to null";
            this._setSelectedObjectValueToNullMenuItem.Click += new System.EventHandler(this.SetSelectedObjectValueToNullMenuItem_Click);
            // 
            // PdfTreeView
            // 
            this.ContextMenuStrip = this._contextMenu;
            this._contextMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion



        #region Methods

        #region PROTECTED

        /// <summary>
        /// Builds new level of tree before node expands.
        /// </summary>
        protected override void OnBeforeExpand(TreeViewCancelEventArgs e)
        {
            base.OnBeforeExpand(e);

            // base node
            TreeNode baseNode = null;
            // indicate that class has properties from base class
            bool hasBaseProperties = false;

            object parentObject = e.Node.Tag;
            if (parentObject is TreeNodeObject)
                parentObject = ((TreeNodeObject)parentObject).PropertyObject;

            // if node is PDF tree node
            if (parentObject is PdfTreeNodeBase || parentObject is PdfDocument)
            {
                if (e.Node.Nodes[e.Node.Nodes.Count - 1].Text == PRIVATE_TAG_NAME)
                {
                    // remove private nodes
                    e.Node.Nodes.Clear();

                    // get PDF node properties
                    PropertyInfo[] properties = GetPublicAndNotEmptyNodeProperties(parentObject);

                    // names of added nodes
                    string[] propertyNodeNames = new string[properties.Length];
                    string[] basePropertyNodeNames = new string[properties.Length];
                    // added nodes
                    TreeNode[] propertyNodes = new TreeNode[properties.Length];
                    TreeNode[] basePropertyNodes = new TreeNode[properties.Length];
                    int nodeCount = 0;

                    // for each property
                    foreach (PropertyInfo property in properties)
                    {
                        // if property is declared in base type
                        if (property.DeclaringType != parentObject.GetType())
                        {
                            hasBaseProperties = true;
                            // create new node and set it to base nodes
                            basePropertyNodes[nodeCount] = CreateNewNode(parentObject, property, e.Node);
                            // if created node is not empty
                            if (basePropertyNodes[nodeCount] != null)
                                basePropertyNodeNames[nodeCount] = basePropertyNodes[nodeCount].Text;
                        }
                        else
                        {
                            // create new node
                            propertyNodes[nodeCount] = CreateNewNode(parentObject, property, e.Node);
                            // if created node is not empty
                            if (propertyNodes[nodeCount] != null)
                                propertyNodeNames[nodeCount] = propertyNodes[nodeCount].Text;
                        }
                        nodeCount++;
                    }

                    // if current node has properties declared in base type
                    if (parentObject is PdfTreeNodeBase && hasBaseProperties)
                    {
                        // create "Base" node
                        baseNode = new TreeNode("Base");
                        e.Node.Nodes.Add(baseNode);
                        // add and sort nodes created from base properties
                        AddAndSortNodes(baseNode.Nodes, basePropertyNodes, basePropertyNodeNames);
                    }

                    // add and sort created nodes
                    AddAndSortNodes(e.Node.Nodes, propertyNodes, propertyNodeNames);
                }
            }
        }

        /// <summary>
        /// Sets selection to the node.
        /// </summary>
        protected override void OnNodeMouseClick(TreeNodeMouseClickEventArgs e)
        {
            base.OnNodeMouseClick(e);

            if (e.Button == MouseButtons.Right)
                SelectedNode = e.Node;
        }

        #endregion


        #region PRIVATE

        #region Create Nodes

        /// <summary>
        /// Creates new node.
        /// </summary>
        /// <param name="parentObject">The parent object.</param>
        /// <param name="property">The property.</param>
        /// <param name="parentNode">The parent object node.</param>
        /// <returns>The new node.</returns>
        private TreeNode CreateNewNode(object parentObject, PropertyInfo property, TreeNode parentNode)
        {
            // create new node
            TreeNode newNode = new TreeNode(property.Name);
            TreeNodeObject newNodeObject = new TreeNodeObject();
            newNodeObject.ParentNode = parentNode;
            newNodeObject.ParentObject = parentObject;
            newNodeObject.Property = property;
            try
            {
                // get property value
                object pdfObject = property.GetValue(parentObject, null);
                newNodeObject.PropertyObject = pdfObject;
                // if value is not empty AND PDF object is not PDF document
                if (pdfObject != null && !(pdfObject is PdfDocument))
                {
                    // set value to new node
                    newNode.Tag = newNodeObject;
                    // if value is PdfTreeNodeBase
                    if (pdfObject is PdfTreeNodeBase)
                    {
                        newNode.Text = string.Format("{0} (Type: {1})", newNode.Text, pdfObject.GetType().Name);
                        // create PDF tree node
                        return CreatePdfTreeNode(newNode, pdfObject);
                    }
                    else
                    {
                        // create value node
                        return CreateValueNode(newNode, pdfObject);
                    }
                }
            }
            catch (Exception exception)
            {
                // create exception node
                return CreateExceptionNode(newNode, property, exception);
            }
            return null;
        }

        /// <summary>
        /// Creates the value node.
        /// </summary>
        /// <param name="newNode">The new node.</param>
        /// <param name="value">The node value.</param>
        /// <returns>Added node.</returns>
        private TreeNode CreateValueNode(TreeNode newNode, object value)
        {
            // if value is dictionary
            if (value is IDictionary)
            {
                AddNodesFromDictionary((IDictionary)value, newNode);
            }
            // if value is array
            else if (value is IEnumerable && !(value is String))
            {
                AddNodesFromArray((IEnumerable)value, newNode);
            }
            // if value is string
            else if (value is String)
            {
                newNode.Text = string.Format("{0} = \"{1}\"", newNode.Text, value.ToString());
            }
            // if value is enumeration
            else if (value is Enum)
            {
                newNode.Text = string.Format("{0} = \"{1}\"", newNode.Text, value.ToString());
            }
            // if value is primitive type
            else if (value.GetType().IsPrimitive)
            {
                newNode.Text = string.Format("{0} = {1}", newNode.Text, value.ToString());
            }
            // if value is not collection or string or enumeration or primitive type
            else
            {
                // create value node
                TreeNode valueNode = new TreeNode(value.ToString());
                valueNode.Tag = value;
                newNode.Nodes.Add(valueNode);
            }

            return newNode;
        }

        /// <summary>
        /// Creates the PDF tree node.
        /// </summary>
        /// <param name="newNode">The new node.</param>
        /// <param name="pdfObject">The PDF tree node.</param>
        /// <returns>Added node.</returns>
        private TreeNode CreatePdfTreeNode(TreeNode newNode, object pdfObject)
        {
            // if PDF object is enumerable
            if (pdfObject is IEnumerable)
            {
                AddNodesFromArray((IEnumerable)pdfObject, newNode);
            }
            else
            {
                AddPrivateNode(newNode);
            }

            return newNode;
        }

        /// <summary>
        /// Creates the exception node.
        /// </summary>
        /// <param name="errorNode">The error node.</param>
        /// <param name="property">A property, that throws exception.</param>
        /// <param name="exception">The exception.</param>
        /// <returns>Exception node.</returns>
        private TreeNode CreateExceptionNode(TreeNode errorNode, PropertyInfo property, Exception exception)
        {
            // if new node is not created
            if (errorNode == null)
                errorNode = new TreeNode(property.Name);
            errorNode.ForeColor = System.Drawing.Color.Red;

            // create exception node
            TreeNode excepionNode = new TreeNode(exception.GetType().Name);
            excepionNode.Tag = exception;
            excepionNode.ForeColor = System.Drawing.Color.Red;
            errorNode.Nodes.Add(excepionNode);

            return errorNode;
        }

        #endregion


        #region Add Nodes

        /// <summary>
        /// Adds nodes from dictionary.
        /// </summary>
        /// <param name="dictionary">The dictionary.</param>
        /// <param name="newNode">The node.</param>
        private void AddNodesFromDictionary(IDictionary dictionary, TreeNode newNode)
        {
            // determines that dictionary values use primitive types
            bool isPrimitive = false;
            // for each dictionary element
            foreach (KeyValuePair<object, object> keyValuePairObject in dictionary)
            {
                // if value is primitive type
                if (keyValuePairObject.Value.GetType().IsPrimitive)
                {
                    isPrimitive = true;
                    break;
                }

                // create key node
                TreeNode keyNode = new TreeNode(keyValuePairObject.Key.ToString());
                keyNode.Tag = keyValuePairObject.Value;
                newNode.Nodes.Add(keyNode);

                // create value node
                TreeNode valueNode = new TreeNode(keyValuePairObject.Value.ToString());
                if (keyValuePairObject.Value is PdfTreeNodeBase)
                    valueNode.Text = string.Format("{0} (Type: {1})", valueNode.Text, keyValuePairObject.Value.GetType().Name);
                valueNode.Tag = keyValuePairObject.Value;
                // if value if PdfTreeNodeBase
                if (keyValuePairObject.Value is PdfTreeNodeBase)
                {
                    // add private node
                    AddPrivateNode(valueNode);
                }
                keyNode.Nodes.Add(valueNode);
            }
            if (!isPrimitive)
                newNode.Text = string.Format("{0} (Count = {1})", newNode.Text, ((IDictionary)dictionary).Count);
        }

        /// <summary>
        /// Adds nodes from <see cref="IEnumerable"/> object.
        /// </summary>
        /// <param name="enumerableObject">IEnumerable object.</param>
        /// <param name="newNode">The node.</param>
        private void AddNodesFromArray(IEnumerable enumerableObject, TreeNode newNode)
        {
            // number of elements
            int count = 0;
            // determines that this is a dictionary
            bool isDictionary = false;
            // type of one element
            Type elementType = null;
            string keyNodeText = "Items[{0}]";

            // get enumerator
            IEnumerator enumerator = enumerableObject.GetEnumerator();
            // if enumerator is not empty
            if (enumerator.MoveNext())
            {
                // get type of element
                elementType = enumerator.Current.GetType();
                // if element is "KeyValuePair" type
                if (elementType.Name.StartsWith("KeyValuePair"))
                {
                    isDictionary = true;
                    keyNodeText = "{0}";
                }
            }

            // if element type is not empty AND element type is not primitive
            if (elementType != null && !elementType.IsPrimitive)
            {
                // for each element
                foreach (object element in enumerableObject)
                {
                    object nodeKey = count;
                    object nodeValue = element;
                    // if value is dictionary
                    if (isDictionary)
                    {
                        // get element properties
                        PropertyInfo[] elementProperties = elementType.GetProperties();
                        // get element key
                        nodeKey = elementProperties[0].GetValue(element, null);
                        // get element value
                        nodeValue = elementProperties[1].GetValue(element, null);
                    }

                    // create index node
                    TreeNode indexNode = new TreeNode(string.Format(keyNodeText, nodeKey));
                    indexNode.Tag = nodeValue;
                    newNode.Nodes.Add(indexNode);

                    // create value node
                    TreeNode valueNode = new TreeNode(nodeValue.ToString());
                    if (nodeValue is PdfTreeNodeBase)
                        valueNode.Text = string.Format("{0} (Type: {1})", valueNode.Text, nodeValue.GetType().Name);
                    valueNode.Tag = nodeValue;
                    // if value if PdfTreeNodeBase
                    if (nodeValue is PdfTreeNodeBase)
                    {
                        // add private node
                        AddPrivateNode(valueNode);
                    }
                    indexNode.Nodes.Add(valueNode);

                    count++;
                }
                newNode.Text = string.Format("{0} (Count = {1})", newNode.Text, count);
            }
        }

        /// <summary>
        /// Adds the private node to the specified node.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <returns>Private node.</returns>
        private TreeNode AddPrivateNode(TreeNode node)
        {
            // create private node
            TreeNode privateNode = new TreeNode(PRIVATE_TAG_NAME);
            node.Nodes.Add(privateNode);
            return privateNode;
        }

        /// <summary>
        /// Adds and sorts nodes to the specified node collection.
        /// </summary>
        /// <param name="nodeCollection">The node collection.</param>
        /// <param name="nodes">The nodes that must be added.</param>
        /// <param name="nodeNames">The node names.</param>
        private void AddAndSortNodes(TreeNodeCollection nodeCollection, TreeNode[] nodes, string[] nodeNames)
        {
            // sort nodes by name
            Array.Sort(nodeNames, nodes);
            // for each new node
            for (int i = 0; i < nodeNames.Length; i++)
            {
                // if node is not empty
                if (nodeNames[i] != null)
                {
                    // add node
                    nodeCollection.Add(nodes[i]);
                }
            }
        }

        #endregion


        /// <summary>
        /// Returns public and not empty properties of object.
        /// </summary>
        /// <param name="pdfObject">The object.</param>
        /// <returns>Properties array.</returns>
        private PropertyInfo[] GetPublicAndNotEmptyNodeProperties(object pdfObject)
        {
            // get object type
            Type pdfObjectType = pdfObject.GetType();
            // get all public properties
            PropertyInfo[] properties = pdfObjectType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            // get all public properties without null values
            PropertyInfo[] propertiesWithoutNullValues = Array.FindAll(properties, IsNotNull);

            return propertiesWithoutNullValues;
        }

        /// <summary>
        /// Returns a value, which determines that property is not null.
        /// </summary>
        /// <param name="property">The property.</param>
        /// <returns>A value which determines that property is not null.</returns>
        private bool IsNotNull(PropertyInfo property)
        {
            return property != null;
        }

        /// <summary>
        /// Context menu is opening.
        /// </summary>
        private void ContextMenu_Opening(object sender, CancelEventArgs e)
        {
            // if null can be setted to the selected object 
            if (!CanSelectedPropertyBeNull())
            {
                e.Cancel = true;
            }
        }

        /// <summary>
        /// Sets null to the selected object.
        /// </summary>
        private void SetSelectedObjectValueToNullMenuItem_Click(object sender, EventArgs e)
        {
            TreeNodeObject nodeObject = (TreeNodeObject)SelectedObject;

            if (nodeObject.ParentObject != null)
            {
                try
                {
                    // set null to the seleceted property
                    nodeObject.Property.SetValue(nodeObject.ParentObject, null, null);
                }
                catch (Exception ex)
                {
                    // show error message
                    DemosTools.ShowErrorMessage(ex);
                }
                nodeObject.ParentNode.Nodes.Clear();
                AddPrivateNode(nodeObject.ParentNode);
                OnBeforeExpand(new TreeViewCancelEventArgs(nodeObject.ParentNode, false, TreeViewAction.Expand));
            }
        }

        /// <summary>
        /// Determines that selected property can be null.
        /// </summary>
        /// <returns>
        /// <b>true</b> - if selected property can be null;
        /// <b>false</b> - if selected property cannot be null.
        /// </returns>
        private bool CanSelectedPropertyBeNull()
        {
            TreeNodeObject nodeObject;
            if (SelectedObject != null && SelectedObject is TreeNodeObject)
            {
                nodeObject = (TreeNodeObject)SelectedObject;
                Type selectedObjectType = nodeObject.PropertyObject.GetType();
                return
                    !selectedObjectType.IsPrimitive &&
                    !selectedObjectType.IsSubclassOf(typeof(PdfBasicObject)) &&
                    nodeObject.Property.CanWrite;
            }
            return false;
        }

        #endregion

        #endregion

    }
}
