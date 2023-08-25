using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using Vintasoft.Imaging.Pdf;
using Vintasoft.Imaging.Pdf.Tree;

namespace DemosCommonCode.Pdf
{
    /// <summary>
    /// A tree view that allows to view and edit bookmarks of PDF document.
    /// </summary>
    public partial class BookmarkTreeViewEditor : BookmarkTreeView
    {

        #region Constants

        /// <summary>
        /// The delta for insertion in the same level.
        /// </summary>
        const double DELTA_FOR_INSERTION_IN_THE_SAME_LEVEL = 0.3;

        #endregion



        #region Fields

        /// <summary>
        /// Rectangle which must be invalidated.
        /// </summary>
        Rectangle _invalidateDragOverRect = Rectangle.Empty;

        #endregion



        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BookmarkTreeViewEditor"/> class.
        /// </summary>
        public BookmarkTreeViewEditor()
        {
            InitializeComponent();

            this.AllowDrop = false;

            this.ItemDrag += new ItemDragEventHandler(BookmarkTreeView_ItemDrag);
            this.DragEnter += new DragEventHandler(BookmarkTreeView_DragEnter);
            this.DragLeave += new EventHandler(BookmarkTreeView_DragLeave);
            this.DragDrop += new DragEventHandler(BookmarkTreeView_DragDrop);
            this.DragOver += new DragEventHandler(BookmarkTreeView_DragOver);
        }

        #endregion



        #region Properties
        
        bool _canEditBookmarks = false;
        /// <summary>
        /// Gets or sets a value indicating whether bookmarks can be edited.
        /// </summary>
        [DefaultValue(false)]
        public bool CanEditBookmarks
        {
            get
            {
                return _canEditBookmarks;
            }
            set
            {
                _canEditBookmarks = value;
                this.AllowDrop = value;

                if (_canEditBookmarks)
                    ContextMenuStrip = bookmarkContextMenu;
                else
                    ContextMenuStrip = null;
            }
        }

        #endregion



        #region Methods

        /// <summary>
        /// Adds the bookmark to the PDF document.
        /// </summary>
        /// <param name="pageIndex">The zero-based index of PDF page.</param>
        public void AddBookmark(int pageIndex)
        {
            // create new PDF bookmark
            PdfBookmark outline = new PdfBookmark(Document);
            // create edit bookmark dialog
            EditBookmarkNodeForm dialog = new EditBookmarkNodeForm(Viewer, pageIndex, outline, true);
            // if dialog result is OK
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                PdfBookmarkCollection bookmarks = null;
                TreeNodeCollection nodes = null;
                // if there is selected bookmark
                // and bookmark must be added not to the root
                if (SelectedNode != null && !dialog.AddToRoot)
                {
                    // get child bookmarks
                    bookmarks = ((PdfBookmark)SelectedNode.Tag).ChildBookmarks;
                    nodes = SelectedNode.Nodes;
                }
                else
                {
                    // if document has bookmarks
                    if (Document.Bookmarks == null)
                    {
                        // create new bookmark collection
                        Document.Bookmarks = new PdfBookmarkCollection(Document);
                    }
                    bookmarks = Document.Bookmarks;
                    nodes = Nodes;
                }

                // add new bookmark
                bookmarks.Add(dialog.Bookmark);
                SelectedNode = AddBookmark(nodes, outline);

                Document.DocumentViewMode = PdfDocumentViewMode.UseOutlines;
            }
        }

        /// <summary>
        /// Deletes the selected bookmark.
        /// </summary>
        public void DeleteSelectedBookmark()
        {
            if (CanEditBookmarks)
            {
                if (SelectedNode != null)
                {
                    PdfBookmark current = (PdfBookmark)SelectedNode.Tag;
                    current.Remove();
                    SelectedNode.Remove();
                }
            }
        }

        /// <summary>
        /// Edits the selected bookmark.
        /// </summary>
        public void EditSelectedBookmark()
        {
            if (CanEditBookmarks)
            {
                if (SelectedNode != null)
                {
                    PdfBookmark outline = (PdfBookmark)SelectedNode.Tag;

                    int pageIndex = 0;
                    if (Viewer != null && Viewer.FocusedIndex >= 0)
                        pageIndex = Viewer.FocusedIndex;
                    if (outline.Destination != null)
                        pageIndex = Document.Pages.IndexOf(outline.Destination.Page);

                    EditBookmarkNodeForm dialog = new EditBookmarkNodeForm(Viewer, pageIndex, outline, false);
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        TreeNodeCollection treeNodes;
                        if (SelectedNode.Parent != null)
                            treeNodes = SelectedNode.Parent.Nodes;
                        else
                            treeNodes = Nodes;

                        int index = treeNodes.IndexOf(SelectedNode);
                        SelectedNode.Remove();
                        treeNodes.Insert(index, AddBookmark(null, dialog.Bookmark));
                        SelectedNode = treeNodes[index];
                    }
                }
            }
        }

        /// <summary>
        /// Moves bookmark at delta positions.
        /// </summary>
        private void MoveBookmark(TreeNode node, int delta)
        {
            TreeNodeCollection parentNodes;
            if (node.Parent != null)
                parentNodes = node.Parent.Nodes;
            else
                parentNodes = node.TreeView.Nodes;
            PdfBookmark outline = (PdfBookmark)node.Tag;
            PdfBookmarkCollection parentOutlines = outline.ParentBookmarks;

            int currentIndex = parentOutlines.IndexOf(outline);
            int newIndex = currentIndex + delta;
            if (newIndex < 0)
                newIndex = 0;
            if (newIndex >= parentOutlines.Count)
                newIndex = parentOutlines.Count - 1;
            if (newIndex == currentIndex)
                return;

            parentOutlines.RemoveAt(currentIndex);
            parentOutlines.Insert(newIndex, outline);

            node.TreeView.SelectedNode = null;

            parentNodes.RemoveAt(currentIndex);
            parentNodes.Insert(newIndex, node);

            node.TreeView.SelectedNode = node;
        }

        /// <summary>
        /// Adds new bookmark.
        /// </summary>
        private void addBookmarkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int pageIndex = 0;
            if (Viewer != null && Viewer.FocusedIndex >= 0)
                pageIndex = Viewer.FocusedIndex;
            AddBookmark(pageIndex);
        }

        /// <summary>
        /// Deletes bookmark.
        /// </summary>
        private void deleteBookmarkNodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeleteSelectedBookmark();
        }

        /// <summary>
        /// Edits selected bookmark.
        /// </summary>
        protected override void OnDoubleClick(EventArgs e)
        {
            base.OnDoubleClick(e);

            EditSelectedBookmark();
        }

        /// <summary>
        /// Edits selected bookmark.
        /// </summary>
        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditSelectedBookmark();
        }

        /// <summary>
        /// Moves down selected bookmark.
        /// </summary>
        private void moveDownBookmarkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SelectedNode != null)
                MoveBookmark(SelectedNode, 1);
        }

        /// <summary>
        /// Moves up selected bookmark.
        /// </summary>
        private void moveUpBookmarkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SelectedNode != null)
                MoveBookmark(SelectedNode, -1);
        }

        /// <summary>
        /// Context menu is opened.
        /// </summary>
        private void bookmarksMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            if (!CanEditBookmarks || Document == null)
            {
                e.Cancel = true;
            }
            else
            {
                bool canEdit = SelectedNode != null;
                moveDownToolStripMenuItem.Enabled = canEdit;
                moveUpToolStripMenuItem.Enabled = canEdit;
                editToolStripMenuItem.Enabled = canEdit;
                deleteOutlineNodeToolStripMenuItem.Enabled = canEdit;
            }
        }

        #region Drag & Drop

        /// <summary>
        /// Handles the ItemDrag event of the BookmarkTreeView control.
        /// </summary>
        private void BookmarkTreeView_ItemDrag(object sender, ItemDragEventArgs e)
        {
            // begin a drag and drop operation
            DoDragDrop(e.Item, DragDropEffects.Move);
        }

        /// <summary>
        /// Handles the DragEnter event of the BookmarkTreeView control.
        /// </summary>
        private void BookmarkTreeView_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        /// <summary>
        /// Handles the DragLeave event of the BookmarkTreeView control.
        /// </summary>
        private void BookmarkTreeView_DragLeave(object sender, EventArgs e)
        {
            // if BookmarkTreeView is not invalidated
            if (!_invalidateDragOverRect.IsEmpty)
                // invalidate BookmarkTreeView
                ((TreeView)sender).Invalidate(_invalidateDragOverRect);

            // specify that BookmarkTreeView was invalidated
            _invalidateDragOverRect = Rectangle.Empty;
        }

        /// <summary>
        /// Determines that <i>parentNode</i> contains <i>childNode</i>.
        /// </summary>
        /// <param name="source">The parent node.</param>
        /// <param name="destNode">The child node.</param>
        /// <returns>
        /// <b>true</b> - <i>parentNode</i> contains <i>childNode</i>;
        /// <b>false</b> - <i>parentNode</i> does NOT contain <i>childNode</i>.
        /// </returns>
        private bool NodeIsEmbedded(TreeNode parentNode, TreeNode childNode)
        {
            if (parentNode == childNode)
                return true;

            if (parentNode == null || childNode == null)
                return false;

            foreach (TreeNode node in parentNode.Nodes)
            {
                if (childNode == node || NodeIsEmbedded(node, childNode))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Handles the DragOver event of the BookmarkTreeView control.
        /// </summary>
        private void BookmarkTreeView_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = e.AllowedEffect;
            if (e.Data.GetDataPresent("System.Windows.Forms.TreeNode", false))
            {
                // get tree view
                TreeView treeView = (TreeView)sender;

                // get reference to the dragging TreeNode
                TreeNode sourceNode = (TreeNode)e.Data.GetData("System.Windows.Forms.TreeNode");

                // get reference to the destination TreeNode
                Point pt = treeView.PointToClient(new Point(e.X, e.Y));
                // calculate an inaccuracy in mouse position
                int delta = (int)Math.Round(ItemHeight * DELTA_FOR_INSERTION_IN_THE_SAME_LEVEL);
                // get tree node, which is located before the node under mouse cursor
                TreeNode destNode1 = treeView.GetNodeAt(pt.X, pt.Y - delta);
                // get tree node, which is located after the node under mouse cursor
                TreeNode destNode2 = treeView.GetNodeAt(pt.X, pt.Y + delta);

                if (destNode1 == destNode2 && destNode1 != null)
                {
                    // if BookmarkTreeView is not invalidated
                    if (!_invalidateDragOverRect.IsEmpty)
                        // invalidate BookmarkTreeView
                        treeView.Invalidate(_invalidateDragOverRect);
                    // specify that BookmarkTreeView is invalidated
                    _invalidateDragOverRect = Rectangle.Empty;

                    if (NodeIsEmbedded(sourceNode, destNode1))
                        e.Effect = DragDropEffects.None;
                }
                else
                {
                    // if dragging is possible
                    if (NodeIsEmbedded(sourceNode, destNode1) ||
                        NodeIsEmbedded(sourceNode, destNode2))
                        // disable drag drop
                        e.Effect = DragDropEffects.None;
                    else
                    {
                        // calculate bounding box of separator
                        Rectangle separatorBBox = CalculateSeparatorBoundingBox(destNode1, destNode2, treeView);
                        // draw separator
                        DrawSeparator(treeView, separatorBBox);
                    }
                }
            }
        }

        /// <summary>
        /// Calculates the bounding box of separator.
        /// </summary>
        /// <param name="destNode1">Tree node, which is located before the node under mouse cursor.</param>
        /// <param name="destNode2">Tree node, which is located after the node under mouse cursor.</param>
        /// <param name="treeView">The tree view.</param>
        /// <returns>The bounding box of separator.</returns>
        private Rectangle CalculateSeparatorBoundingBox(TreeNode destNode1, TreeNode destNode2, TreeView treeView)
        {
            // if tree node, which is located before the node under mouse cursor, exists
            if (destNode1 != null)
            {
                return new Rectangle(destNode1.Bounds.X, destNode1.Bounds.Bottom,
                    destNode1.Bounds.Width, 0);
            }
            // if tree node, which is located after the node under mouse cursor, exists
            else if (destNode2 != null)
            {
                return new Rectangle(destNode2.Bounds.X, destNode2.Bounds.Y + 2,
                    destNode2.Bounds.Width, 0);
            }
            else
            {
                // get the last node of root node
                TreeNode lastNode = treeView.Nodes[treeView.Nodes.Count - 1];
                return new Rectangle(lastNode.Bounds.X, lastNode.Bounds.Bottom,
                    lastNode.Bounds.Width, 0);
            }

            throw new NotImplementedException();
        }

        /// <summary>
        /// Draws the separator line.
        /// </summary>
        /// <param name="treeView">The tree view.</param>
        /// <param name="separatorBbox">The separator bounding box.</param>
        private void DrawSeparator(TreeView treeView, Rectangle separatorBbox)
        {
            // create graphics
            using (Graphics g = treeView.CreateGraphics())
            {
                // create invalidate rectangle of BookmarkTreeView
                Rectangle rect = new Rectangle(separatorBbox.X, separatorBbox.Y - 2, separatorBbox.Width, 2);
                // if BookmarkTreeView must be invalidated
                if (!_invalidateDragOverRect.IsEmpty && rect != _invalidateDragOverRect)
                    // invalidate BookmarkTreeView
                    treeView.Invalidate(_invalidateDragOverRect);

                // draw underlining of BookmarkTreeView
                g.DrawLine(new Pen(Color.Black, 2),
                    separatorBbox.X, separatorBbox.Y - 1,
                    separatorBbox.X + separatorBbox.Width, separatorBbox.Y - 1);

                // set invalidate rectangle
                _invalidateDragOverRect = rect;
            }
        }

        /// <summary>
        /// Handles the DragDrop event of the BookmarkTreeView control.
        /// </summary>
        private void BookmarkTreeView_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Effect == DragDropEffects.Move &&
                e.Data.GetDataPresent("System.Windows.Forms.TreeNode", false))
            {
                // get tree view
                TreeView treeView = (TreeView)sender;
                // begin update of BookmarkTreeView
                treeView.BeginUpdate();
                try
                {
                    // get reference to the dragging TreeNode
                    TreeNode sourceNode = (TreeNode)e.Data.GetData("System.Windows.Forms.TreeNode");

                    // get reference to the destination TreeNode
                    Point pt = treeView.PointToClient(new Point(e.X, e.Y));
                    // calculate inaccuracy of position of mouse
                    int delta = (int)Math.Round(ItemHeight * DELTA_FOR_INSERTION_IN_THE_SAME_LEVEL);
                    // get tree node, which is located before the node under mouse cursor
                    TreeNode destNode1 = treeView.GetNodeAt(pt.X, pt.Y - delta);
                    // get tree node, which is located after the node under mouse cursor
                    TreeNode destNode2 = treeView.GetNodeAt(pt.X, pt.Y + delta);

                    // update the bookmark tree in TreeView

                    if (sourceNode == destNode1)
                        return;

                    // clone of node
                    TreeNode sourceNodeClone = (TreeNode)sourceNode.Clone();
                    // get reference to the dragging bookmark
                    PdfBookmark sourceBookmark = (PdfBookmark)sourceNode.Tag;
                    // destination bookmark
                    PdfBookmark destBookmark = null;

                    // if dragging node must be added to the root node
                    if (destNode1 == null && destNode2 == null)
                    {
                        // remove source bookmark
                        sourceBookmark.Remove();
                        // add source bookmark to root of PDF document bookmarks
                        sourceBookmark.Document.Bookmarks.Add(sourceBookmark);
                        // add node to root of BookmarkTreeView
                        treeView.Nodes.Add(sourceNodeClone);
                    }
                    // if dragging node must be inserted between 2 nodes
                    else if (destNode1 != destNode2)
                    {
                        // parent node
                        TreeNode parent = null;
                        // if tree node, which is located before the node under mouse cursor, exists
                        if (destNode1 != null)
                            // get parent of tree view node
                            parent = destNode1.Parent;
                        else
                            // get parent of tree view node
                            parent = destNode2.Parent;

                        // nodes of parent node
                        TreeNodeCollection parentNodes = null;
                        // bookmarks of parent bookmark
                        PdfBookmarkCollection parentBookmark = null;
                        // if parent is empty
                        if (parent == null)
                        {
                            // get root nodes
                            parentNodes = treeView.Nodes;
                            // get root bookmarks
                            parentBookmark = sourceBookmark.Document.Bookmarks;
                        }
                        else
                        {
                            // get parent nodes
                            parentNodes = parent.Nodes;
                            // get parent bookmarks
                            parentBookmark = ((PdfBookmark)destNode1.Tag).ParentBookmarks;
                        }

                        // position of bookmark/node to insert
                        int index = 0;
                        // if tree node, which is located before the node under mouse cursor, exists
                        if (destNode1 != null)
                            // get index
                            index = parentNodes.IndexOf(destNode1) + 1;
                        else
                        {
                            // get index
                            index = parentNodes.IndexOf(destNode2) - 1;
                            if (index == -1)
                                index = 0;
                        }

                        // if insert to end
                        if (index == parentNodes.Count)
                        {
                            // add node
                            parentNodes.Add(sourceNodeClone);
                            // move bookmark
                            sourceBookmark.Move(parentBookmark);
                        }
                        else
                        {
                            // insert node
                            parentNodes.Insert(index, sourceNodeClone);
                            // remove bookmark
                            sourceBookmark.Remove();
                            // insert bookmark
                            parentBookmark.Insert(index, sourceBookmark);
                        }

                        // if parent exist
                        if (parent != null)
                            // expand parent
                            parent.Expand();
                    }
                    // if dragging node must be added as child of another node
                    else
                    {
                        // add node to destination node
                        destNode1.Nodes.Add(sourceNodeClone);
                        // expand destination node
                        destNode1.Expand();

                        // gete reference to the destination bookmark
                        destBookmark = (PdfBookmark)destNode1.Tag;
                        // add the dragging bookmark as a child of destination bookmark
                        sourceBookmark.Move(destBookmark.ChildBookmarks);
                    }

                    // remove source node
                    sourceNode.Remove();
                }
                finally
                {
                    // end update of BookmarkTreeView
                    treeView.EndUpdate();
                }
            }
        }

        #endregion

        #endregion

    }
}
