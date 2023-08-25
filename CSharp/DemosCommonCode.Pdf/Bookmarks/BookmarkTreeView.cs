using System.Drawing;
using System.Windows.Forms;

using Vintasoft.Imaging.Pdf;
using Vintasoft.Imaging.Pdf.JavaScriptApi;
using Vintasoft.Imaging.Pdf.Tree;
using Vintasoft.Imaging.UI;

using DemosCommonCode.Pdf.JavaScript;

namespace DemosCommonCode.Pdf
{
    /// <summary>
    /// A tree view that allows to view bookmarks of PDF document.
    /// </summary>
    public class BookmarkTreeView : TreeView
    {

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BookmarkTreeView"/> class.
        /// </summary>
        public BookmarkTreeView()
        {
        }

        #endregion



        #region Properties

        PdfActionExecutorBase _actionExecutor;
        /// <summary>
        /// Gets or sets the action executor.
        /// </summary>
        public PdfActionExecutorBase ActionExecutor
        {
            get
            {
                return _actionExecutor;
            }
            set
            {
                _actionExecutor = value;
            }
        }

        PdfDocument _document;
        /// <summary>
        /// Gets or sets the source PDF document.
        /// </summary>
        public PdfDocument Document
        {
            get
            {
                return _document;
            }
            set
            {
                // if set a new document
                if (_document != value)
                {
                    _document = value;
                    // show bookmarks
                    ShowBookmarks();
                }
            }
        }

        ImageViewerBase _viewer;
        /// <summary>
        /// Gets or sets the image viewer.
        /// </summary>
        public ImageViewerBase Viewer
        {
            get
            {
                return _viewer;
            }
            set
            {
                _viewer = value;
            }
        }

        #endregion



        #region Methods

        #region PUBLIC

        /// <summary>
        /// Shows the bookmarks.
        /// </summary>
        public void ShowBookmarks()
        {
            // clear the current bookmarks tree
            Nodes.Clear();

            // if document is not empty
            if (_document != null)
            {
                lock (_document)
                {
                    BeginUpdate();
                    // add bookmars of the document to the bookmarks tree
                    AddBookmarks(Nodes, _document.Bookmarks);
                    EndUpdate();
                }
            }
        }

        #endregion


        #region PROTECTED
        
        /// <summary>
        /// Adds bookmark to the specified tree node collection.
        /// </summary>
        /// <param name="destination">A tree node collection.</param>
        /// <param name="bookmark">A PDF bookmark.</param>
        /// <returns>A tree node.</returns>
        protected TreeNode AddBookmark(TreeNodeCollection destination, PdfBookmark bookmark)
        {
            // create new tree node
            TreeNode node = new TreeNode();
            // set tag as bookmark
            node.Tag = bookmark;
            // if tree node collection is not empty
            if (destination != null)
            {
                // add bookmark to the collection
                destination.Add(node);
            }

            // set font
            node.NodeFont = Font;
            // if bookmark has flags
            if (bookmark.Flags != PdfBookmarkFlags.None)
            {
                // set new font
                FontStyle style = FontStyle.Regular;
                if ((bookmark.Flags & PdfBookmarkFlags.Bold) != 0)
                    style |= FontStyle.Bold;
                if ((bookmark.Flags & PdfBookmarkFlags.Italic) != 0)
                    style |= FontStyle.Italic;
                node.NodeFont = new Font(node.NodeFont, style);
            }
            // set bookmark title
            node.Text = bookmark.Title;
            // set bookmark color
            node.ForeColor = bookmark.Color;

            if (bookmark.ChildBookmarks != null)
                AddBookmarks(node.Nodes, bookmark.ChildBookmarks);

            return node;
        }

        /// <summary>
        /// Sets the current page index to bookmark destination page index.
        /// </summary>
        protected override void OnAfterSelect(TreeViewEventArgs e)
        {
            base.OnAfterSelect(e);

            // if viewer is not empty
            if (_viewer != null)
            {
                // if PDF document is empty
                if (_document == null)
                    return;

                // if PDF document is not empty
                // and PDF action executor is not empty
                if (_document != null && ActionExecutor != null)
                {
                    // get selected bookmark
                    PdfBookmark bookmark = (PdfBookmark)SelectedNode.Tag;

                    // create JavaScript event
                    PdfJsEvent jsEvent = null;
                    if (PdfJavaScriptManager.JsApp != null)
                        jsEvent = PdfJsEvent.CreateUndefinedEventObject(PdfJavaScriptManager.JsApp.GetDoc(bookmark.Document));
                    PdfTriggerEventArgs args = new PdfTriggerEventArgs(null, jsEvent);

                    // if bookmark action is not empty
                    if (bookmark.Action != null)
                    {
                        // execute action
                        ActionExecutor.ExecuteActionSequence(bookmark.Action, args);
                    }
                    // if bookmark destination is not empty
                    else if (bookmark.Destination != null)
                    {
                        // go to the destination of the bookmark
                        ActionExecutor.ExecuteActionSequence(new PdfGotoAction(bookmark.Destination, false), args);
                    }
                }
            }
        }

        /// <summary>
        /// Sets selection to the bookmark.
        /// </summary>
        protected override void OnNodeMouseClick(TreeNodeMouseClickEventArgs e)
        {
            base.OnNodeMouseClick(e);

            if (e.Button == MouseButtons.Right)
                SelectedNode = e.Node;
        }

        #endregion
               

        #region PRIVATE

        /// <summary>
        /// Adds bookmarks to the specified tree node collection.
        /// </summary>
        /// <param name="destination">A tree node collection.</param>
        /// <param name="source">A collection of bookmarks.</param>
        private void AddBookmarks(TreeNodeCollection destination, PdfBookmarkCollection source)
        {
            // if bookmark collection is not empty
            if (source != null)
            {
                // get the bookmark count
                int count = source.Count;
                // for each bookmark
                for (int i = 0; i < count; i++)
                {
                    // add bookmark to the tree node collection
                    AddBookmark(destination, source[i]);
                }
            }
        }

        #endregion

        #endregion

    }
}
