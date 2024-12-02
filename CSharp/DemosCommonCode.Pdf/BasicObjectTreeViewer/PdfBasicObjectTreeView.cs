using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using Vintasoft.Imaging.Pdf.BasicTypes;

namespace DemosCommonCode.Pdf
{
    /// <summary>
    /// Provides the control that displays tree of PDF basic objects.
    /// </summary>
    public class PdfBasicObjectTreeView : TreeView
    {

        #region Constants

        /// <summary>
        /// The color of changed node.
        /// </summary>
        static readonly Color CHANGED_NODE_COLOR = Color.Red;

        /// <summary>
        /// The wrap node name.
        /// </summary>
        const string WRAP_NODE_NAME = "!WRAP";

        #endregion



        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PdfBasicObjectTreeView"/> class.
        /// </summary>
        public PdfBasicObjectTreeView()
            : base()
        {
        }

        #endregion



        #region Properties

        PdfBasicObject _rootObject;
        /// <summary>
        /// Gets or sets the root object.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public PdfBasicObject RootObject
        {
            get
            {
                return _rootObject;
            }
            set
            {
                _rootObject = value;
                Nodes.Clear();
                if (_rootObject != null)
                    AddBasicObjectNode(Nodes, _rootObject);
            }
        }

        #endregion



        #region Methods

        #region PUBLIC

        /// <summary>
        /// Invalidates the selected object.
        /// </summary>
        public void InvalidateSelectedObject()
        {
            TreeNode treeNode = SelectedNode;
            while (treeNode != null)
            {
                SetProperties(treeNode);
                treeNode = treeNode.Parent;
            }
        }

        #endregion


        #region PROTECTED

        /// <summary>
        /// Navigates to an indirect object.
        /// </summary>
        protected override void OnAfterSelect(TreeViewEventArgs e)
        {
            base.OnAfterSelect(e);
            if (e.Node.Parent != null && e.Node.Tag is PdfIndirectObject)
            {
                PdfIndirectReference reference = (PdfIndirectReference)e.Node.Parent.Tag;
                PdfIndirectObject indirectObject = (PdfIndirectObject)e.Node.Tag;
                if (indirectObject != null)
                {
                    for (int i = 0; i < Nodes.Count; i++)
                    {
                        PdfIndirectObject obj = Nodes[i].Tag as PdfIndirectObject;
                        if (obj != null)
                        {
                            if (obj.Number == indirectObject.Number && obj.Generation == indirectObject.Generation)
                            {
                                SelectedNode = Nodes[i];
                                return;
                            }
                        }
                    }
                    SelectedNode = AddBasicObjectNode(Nodes, indirectObject);
                    SelectedNode.Expand();
                }
            }
        }

        /// <summary>
        /// Builds new level of tree before node expands.
        /// </summary>
        protected override void OnBeforeExpand(TreeViewCancelEventArgs e)
        {
            base.OnBeforeExpand(e);
            PdfBasicObject obj = (PdfBasicObject)e.Node.Tag;
            if (GetIsExpandable(obj))
            {
                // if node must be expanded
                if (e.Node.Nodes.Count == 1 && e.Node.Nodes[0].Text == WRAP_NODE_NAME)
                {
                    e.Node.Nodes.Clear();

                    if (obj is PdfIndirectObject)
                    {
                        // add tree view item
                        AddBasicObjectToNode(e.Node, ((PdfIndirectObject)obj).Value);
                    }
                    else if (obj is PdfIndirectReference)
                    {
                        // add tree view item
                        AddBasicObjectToNode(e.Node, PdfIndirectObject.GetByReference((PdfIndirectReference)obj));
                    }
                    else if (obj is PdfStream)
                    {
                        // add tree view item
                        AddBasicObjectToNode(e.Node, ((PdfStream)obj).Dictionary);
                    }
                    else if (obj is PdfArray)
                    {
                        PdfArray array = (PdfArray)obj;
                        for (int i = 0; i < array.Count; i++)
                        {
                            TreeNode node = new TreeNode(string.Format("[{0}]", i));
                            e.Node.Nodes.Add(node);
                            // add tree view item
                            AddBasicObjectToNode(node, array[i]);
                            SetProperties(node);
                        }
                    }
                    else if (obj is PdfDictionary)
                    {
                        PdfDictionary dict = (PdfDictionary)obj;
                        foreach (string name in dict.Keys)
                        {
                            TreeNode node = new TreeNode(string.Format("/{0}", name));
                            e.Node.Nodes.Add(node);
                            // add tree view item
                            AddBasicObjectToNode(node, dict[name]);
                            SetProperties(node);
                        }
                    }
                    else
                    {
                        throw new NotImplementedException();
                    }
                }
            }
        }

        #endregion


        #region PRIVATE

        /// <summary>
        /// Adds the basic object to the specified node.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <param name="value">The value.</param>
        private void AddBasicObjectToNode(TreeNode node, PdfBasicObject value)
        {
            if (node == null)
                AddBasicObjectNode(Nodes, value);
            else
                AddBasicObjectNode(node.Nodes, value);
        }

        /// <summary>
        /// Adds the basic object node to the specified node collection.
        /// </summary>
        /// <param name="nodes">The nodes.</param>
        /// <param name="value">The value.</param>
        private TreeNode AddBasicObjectNode(TreeNodeCollection nodes, PdfBasicObject value)
        {
            TreeNode node = new TreeNode("");
            node.Tag = value;
            SetProperties(node);
            if (GetIsExpandable(value))
            {
                TreeNode wrap = new TreeNode(WRAP_NODE_NAME);
                node.Nodes.Add(wrap);
            }
            nodes.Add(node);
            return node;
        }

        /// <summary>
        /// Sets the properties of the specified tree node.
        /// </summary>
        /// <param name="node">The node.</param>
        private void SetProperties(TreeNode node)
        {
            PdfBasicObject obj = node.Tag as PdfBasicObject;
            if (obj != null)
            {
                node.Text = GetBasicObjectText(obj);
                if (obj is PdfCompoundBasicObject)
                {
                    if (((PdfCompoundBasicObject)obj).IsChanged)
                        node.ForeColor = CHANGED_NODE_COLOR;
                }
            }
            else
            {
                node.ForeColor = node.Nodes[0].ForeColor;
            }
        }

        /// <summary>
        /// Determines that the specified basic object is expandable.
        /// </summary>
        /// <param name="value">The basic object.</param>
        private bool GetIsExpandable(PdfBasicObject value)
        {
            return
                value is PdfArray ||
                value is PdfDictionary ||
                value is PdfIndirectObject ||
                value is PdfIndirectReference ||
                value is PdfStream;
        }


        /// <summary>
        /// Returns the text of basic object.
        /// </summary>
        /// <param name="value">The value.</param>
        private string GetBasicObjectText(PdfBasicObject value)
        {
            if (value is PdfIndirectObject)
            {
                PdfIndirectObject indirectObject = (PdfIndirectObject)value;
                return string.Format("Object: {0} ({1})", indirectObject.Number, indirectObject.Generation);
            }
            if (value is PdfStream)
            {
                PdfStream stream = (PdfStream)value;
                return string.Format("Stream: {0} bytes ({1})", stream.Length, stream.Compression);
            }
            if (value is PdfIndirectReference)
            {
                PdfIndirectReference indirectReference = (PdfIndirectReference)value;
                return string.Format("Reference to: Object {0} ({1})", indirectReference.Number, indirectReference.Generation);
            }
            return value.ToString();
        }

        #endregion

        #endregion

    }
}
