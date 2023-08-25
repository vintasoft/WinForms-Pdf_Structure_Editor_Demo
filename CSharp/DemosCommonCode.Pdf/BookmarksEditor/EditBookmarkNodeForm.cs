using System;
using System.Windows.Forms;

using Vintasoft.Imaging;
using Vintasoft.Imaging.Pdf;
using Vintasoft.Imaging.Pdf.Tree;
using Vintasoft.Imaging.UI;

namespace DemosCommonCode.Pdf
{
    /// <summary>
    /// A form that allows to view and edit parameters of PDF bookmark.
    /// </summary>
    public partial class EditBookmarkNodeForm : Form
    {

        #region Fields

        /// <summary>
        /// The PDF document.
        /// </summary>
        PdfDocument _document;

        /// <summary>
        /// The image viewer.
        /// </summary>
        ImageViewerBase _viewer;

        /// <summary>
        /// The PDF action.
        /// </summary>
        PdfAction _action;

        /// <summary>
        /// A value indicating whether this form is initialized.
        /// </summary>
        bool _isInitialized = true;

        #endregion



        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="EditBookmarkNodeForm"/> class.
        /// </summary>
        /// <param name="pageIndex">Zero-based index of page in PDF document.</param>
        /// <param name="bookmark">The PDF bookmark.</param>
        /// <param name="canAddToRoot">Indicates whether the bookmark can be added to the root.</param>
        public EditBookmarkNodeForm(int pageIndex, PdfBookmark bookmark, bool canAddToRoot)
            : this(null, pageIndex, bookmark, canAddToRoot)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EditBookmarkNodeForm"/> class.
        /// </summary>
        /// <param name="imageViewer">The image viewer.</param>
        /// <param name="pageIndex">Zero-based index of page in PDF document.</param>
        /// <param name="bookmark">The PDF bookmark.</param>
        /// <param name="canAddToRoot">Indicates whether the bookmark can be added to the root.</param>
        public EditBookmarkNodeForm(
            ImageViewerBase imageViewer,
            int pageIndex,
            PdfBookmark bookmark,
            bool canAddToRoot)
        {
            InitializeComponent();

            _viewer = imageViewer;

            addToRootCheckBox.Visible = canAddToRoot;

            _bookmark = bookmark;
            _document = bookmark.Document;

            if (_viewer == null)
                pageNumber.Maximum = _document.Pages.Count;
            else
                pageNumber.Maximum = _viewer.Images.Count;
            if (pageIndex < 0)
                pageIndex = 0;
            if (pageIndex > pageNumber.Maximum)
                pageIndex = (int)pageNumber.Maximum - 1;
            pageNumber.Value = pageIndex + 1;

            bookmarkTitle.Text = _bookmark.Title;
            bookmarkExpanded.Checked = _bookmark.IsExpanded;
            bookmarkTextBold.Checked = (_bookmark.Flags & PdfBookmarkFlags.Bold) != 0;
            bookmarkTextItalic.Checked = (_bookmark.Flags & PdfBookmarkFlags.Italic) != 0;
            colorDialog1.Color = _bookmark.Color;
            _action = _bookmark.Action;
            if (_action != null && _bookmark.Destination == null)
                actionRadioButton.Checked = true;
            else
                destRadioButton.Checked = true;

            _isInitialized = false;
        }

        #endregion



        #region Properties

        PdfBookmark _bookmark;
        /// <summary>
        /// Gets the bookmark.
        /// </summary>
        public PdfBookmark Bookmark
        {
            get
            {
                return _bookmark;
            }
        }

        /// <summary>
        /// A value indicating whether the bookmark can be added to the root.
        /// </summary>
        /// <value>
        /// <b>True</b> if bookmark can be added to the root; otherwise, <b>false</b>.
        /// </value>
        public bool AddToRoot
        {
            get
            {
                return addToRootCheckBox.Checked;
            }
        }

        #endregion



        #region Methods

        #region UI

        /// <summary>
        /// Handles the Click event of OkButton object.
        /// </summary>
        private void okButton_Click(object sender, EventArgs e)
        {
            // update bookmark properties
            _bookmark.Title = bookmarkTitle.Text;
            _bookmark.IsExpanded = bookmarkExpanded.Checked;
            _bookmark.Flags = PdfBookmarkFlags.None;
            if (bookmarkTextBold.Checked)
                _bookmark.Flags |= PdfBookmarkFlags.Bold;
            if (bookmarkTextItalic.Checked)
                _bookmark.Flags |= PdfBookmarkFlags.Italic;
            _bookmark.Color = colorDialog1.Color;

            // if bookmark destination action is specified
            if (destRadioButton.Checked)
            {
                // if page number is selected
                if (pageNumber.Value > 0)
                {
                    // get PDF page
                    PdfPage page = FindPage((int)pageNumber.Value - 1);
                    // if PDF page is found
                    if (page != null)
                    {
                        try
                        {
                            // set destination page in bookmark
                            _bookmark.Destination = new PdfDestinationFit(_document, page);
                        }
                        catch (Exception ex)
                        {
                            DemosTools.ShowErrorMessage(ex);
                            return;
                        }
                    }
                    else
                    {
                        DemosTools.ShowWarningMessage(
                            "Bookmarks",
                            string.Format("Page {0} is not from this PDF document. Save document and try again.", pageNumber.Value));
                        return;
                    }
                }
            }
            else
            {
                // update bookmark action
                _bookmark.Action = _action;
            }
            DialogResult = DialogResult.OK;
            Close();
        }

        /// <summary>
        /// Handles the Click event of CancelButton object.
        /// </summary>
        private void cancelButton_Click(object sender, EventArgs e)
        {
            // close this form
            DialogResult = DialogResult.Cancel;
            Close();
        }

        /// <summary>
        /// Handles the Click event of ColorButton object.
        /// </summary>
        private void colorButton_Click(object sender, EventArgs e)
        {
            // show color dialog
            colorDialog1.ShowDialog();
        }

        /// <summary>
        /// Handles the CheckedChanged event of ActionRadioButton object.
        /// </summary>
        private void actionRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            editActionButton.Enabled = actionRadioButton.Checked;
            pageNumber.Enabled = !actionRadioButton.Checked;
            // if action must be changed
            if (actionRadioButton.Checked)
            {
                // if form is initialized
                if (!_isInitialized)
                {
                    // is action is changed
                    if (!EditAction())
                        destRadioButton.Checked = true;
                }
            }
        }

        /// <summary>
        /// Handles the Click event of EditActionButton object.
        /// </summary>
        private void editActionButton_Click(object sender, EventArgs e)
        {
            // edit action
            EditAction();

            // if action is not specified
            if (_action == null)
                destRadioButton.Checked = true;
        }

        #endregion


        /// <summary>
        /// Returns the PDF page.
        /// </summary>
        /// <param name="pageIndex">The zero-based index of page in PDF document.</param>
        /// <returns>PDF page if page is found; otherwise, <b>null</b>.</returns>
        private PdfPage FindPage(int pageIndex)
        {
            if (_viewer == null)
                return _document.Pages[pageIndex];

            ImageSourceInfo imageSourceInfo = _viewer.Images[pageIndex].SourceInfo;
            if (imageSourceInfo.Stream == _document.SourceStream)
                return _document.Pages[imageSourceInfo.PageIndex];

            return null;
        }

        /// <summary>
        /// Edits the action.
        /// </summary>
        private bool EditAction()
        {
            ImageCollection images = null;
            if (_viewer != null)
                images = _viewer.Images;
            // create action editor dialog
            using (PdfActionEditorForm dialog = new PdfActionEditorForm(_document, _action, images))
            {
                // show dialog
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    // update action
                    _action = dialog.Action;

                    // if action is specified
                    if (_action != null)
                        return true;
                }
                return false;
            }
        }

        #endregion

    }
}
