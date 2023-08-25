using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

using Vintasoft.Imaging;
using Vintasoft.Imaging.Codecs.Decoders;
using Vintasoft.Imaging.Codecs.ImageFiles;
using Vintasoft.Imaging.ColorManagement;
using Vintasoft.Imaging.Pdf;
using Vintasoft.Imaging.Pdf.BasicTypes;
using Vintasoft.Imaging.Pdf.Processing.PdfA;
using Vintasoft.Imaging.Pdf.Tree;
using Vintasoft.Imaging.Pdf.Tree.FileAttachments;
using Vintasoft.Imaging.Pdf.Tree.Fonts;
using Vintasoft.Imaging.Processing;
using Vintasoft.Imaging.UI;

using DemosCommonCode;
using DemosCommonCode.Imaging;
using DemosCommonCode.Imaging.ColorManagement;
using DemosCommonCode.Pdf;
using DemosCommonCode.Pdf.Security;
using Vintasoft.Imaging.Pdf.Processing;
using System.Globalization;

namespace PdfStructureEditorDemo
{
    /// <summary>
    /// Main form of PDF Structure Editor Demo.
    /// </summary>
    public partial class MainForm : Form
    {

        #region Fields

        /// <summary>
        /// Template of the application title.
        /// </summary>
        string _titlePrefix = "VintaSoft PDF Structure Editor Demo v" + ImagingGlobalSettings.ProductVersion + " {0}";

        /// <summary>
        /// Opened PDF document.
        /// </summary>
        PdfDocument _document = null;

        /// <summary>
        /// The temporary PDF document.
        /// </summary>
        PdfDocument _tempDocument = new PdfDocument();

        /// <summary>
        /// Reference to a PDF page copied into "clipboard" buffer.
        /// </summary>
        PdfPage _pageBuffer = null;

        /// <summary>
        /// Action name.
        /// </summary>
        string _actionName = "";

        /// <summary>
        /// Action start time.
        /// </summary>
        DateTime _actionStartTime;

        /// <summary>
        /// The color management settings.
        /// </summary>
        ColorManagementDecodeSettings _colorManagementSettings = null;

        #endregion



        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MainForm"/> class.
        /// </summary>
        public MainForm()
        {
            // register the evaluation license for VintaSoft Imaging .NET SDK
            Vintasoft.Imaging.ImagingGlobalSettings.Register("REG_USER", "REG_EMAIL", "EXPIRATION_DATE", "REG_CODE");

            InitializeComponent();
            
            Jbig2AssemblyLoader.Load();
            Jpeg2000AssemblyLoader.Load();

            Filename = null;
            IsNewDocument = false;

            pageModeComboBox.Items.AddRange(Enum.GetNames(typeof(PdfDocumentViewMode)));
            pageModeComboBox.SelectedIndex = 0;

            rendererImage.SizeMode = ImageSizeMode.BestFit;

            // set the initial directory in open file dialog
            DemosTools.SetTestFilesFolder(openImageFileDialog);
            DemosTools.SetTestFilesFolder(openPdfFileDialog);

            // initialize color management in viewer
            _colorManagementSettings = ColorManagementHelper.InitColorManagement(_colorManagementSettings);

            // update the UI
            UpdateUI();
        }

        #endregion



        #region Properties

        string _fileName;
        /// <summary>
        /// Gets or sets the filename of PDF document.
        /// </summary>
        private string Filename
        {
            get
            {
                return _fileName;
            }
            set
            {
                _fileName = value;
                UpdateUI();
            }
        }

        bool _isNewDocument;
        /// <summary>
        /// Gets or sets a value indicating whether the PDF document is a new document.
        /// </summary>
        private bool IsNewDocument
        {
            get
            {
                return _isNewDocument;
            }
            set
            {
                _isNewDocument = value;
                UpdateUI();
            }
        }

        bool _isPdfFileReadOnlyMode = false;
        /// <summary>
        /// Gets or sets a value indicating whether PDF file is opened in read-only mode.
        /// </summary>
        private bool IsPdfFileReadOnlyMode
        {
            get
            {
                return _isPdfFileReadOnlyMode;
            }
            set
            {
                _isPdfFileReadOnlyMode = value;
                UpdateUI();
            }
        }

        bool _isPdfFileOpening = false;
        /// <summary>
        /// Gets or sets a value indicating whether PDF document is opening.
        /// </summary>
        private bool IsPdfFileOpening
        {
            get
            {
                return _isPdfFileOpening;
            }
            set
            {
                _isPdfFileOpening = value;
                UpdateUI();
            }
        }

        bool _isPdfFileSaving = false;
        /// <summary>
        /// Gets or sets a value indicating whether PDF document is saving.
        /// </summary>
        private bool IsPdfFileSaving
        {
            get
            {
                return _isPdfFileSaving;
            }
            set
            {
                _isPdfFileSaving = value;
                UpdateUI();
            }
        }

        bool _isPagesInserting = false;
        /// <summary>
        /// Gets or sets a value indicating whether the PDF page is inserting into PDF document.
        /// </summary>
        private bool IsPageInserting
        {
            get
            {
                return _isPagesInserting;
            }
            set
            {
                _isPagesInserting = value;
                UpdateUI();
            }
        }

        int _focusedPageIndex = -1;
        /// <summary>
        /// Gets or sets the zero-based index of focused PDF page.
        /// </summary>
        private int FocusedPageIndex
        {
            get
            {
                return _focusedPageIndex;
            }
            set
            {
                if (value != _focusedPageIndex)
                    SetFocusedPageIndex(value, true);

                // update the UI
                UpdateUI();
            }
        }

        /// <summary>
        /// Gets the focused PDF page.
        /// </summary>
        private PdfPage FocusedPage
        {
            get
            {
                if (FocusedPageIndex < 0)
                    return null;
                return Pages[FocusedPageIndex];
            }
        }

        /// <summary>
        /// Gets the document page collection.
        /// </summary>
        private PdfPageCollection Pages
        {
            get
            {
                return _document.Pages;
            }
        }

        #endregion



        #region Methods

        #region UI

        #region 'File' menu

        /// <summary>
        /// Handles the Click event of NewToolStripMenuItem object.
        /// </summary>
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IsPdfFileOpening = true;

            SelectPdfFormatForm selectFormat = new SelectPdfFormatForm(PdfFormat.Pdf_14, null);
            if (selectFormat.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                // close PDF document
                ClosePdfDocument();

                // create a new PDF document
                _document = new PdfDocument(selectFormat.Format, selectFormat.NewEncryptionSettings);
                _document.Progress += new EventHandler<ImageFileProgressEventArgs>(Document_Progress);
                IsNewDocument = true;
                Filename = GetNewDocumentFilename();
            }

            IsPdfFileOpening = false;
        }

        /// <summary>
        /// Handles the Click event of OpenToolStripMenuItem object.
        /// </summary>
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (openPdfFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    IsPdfFileOpening = true;

                    // open PDF document
                    OpenPdfDocument(openPdfFileDialog.FileName);
                    IsPdfFileOpening = false;
                    if (_document != null)
                    {
                        // if document has visible attachments
                        if (_document.Attachments != null && _document.Attachments.View != AttachmentCollectionViewMode.Hidden)
                        {
                            // show attachments dialog
                            using (AttachmentsEditorForm dialog = new AttachmentsEditorForm(_document))
                            {
                                dialog.ShowDialog();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                IsPdfFileOpening = false;
                DemosTools.ShowErrorMessage(ex);
            }
        }

        /// <summary>
        /// Handles the Click event of CloseToolStripMenuItem object.
        /// </summary>
        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClosePdfDocument();

            // update the UI
            UpdateUI();
        }


        /// <summary>
        /// Handles the Click event of AddToolStripMenuItem object.
        /// </summary>
        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IsPdfFileOpening = true;
            openPdfFileDialog.Multiselect = true;
            if (openPdfFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                // add pages from other PDF documents
                foreach (string documentFilename in openPdfFileDialog.FileNames)
                {
                    AddPages(documentFilename);
                }
            }

            openPdfFileDialog.Multiselect = false;

            IsPdfFileOpening = false;
        }


        /// <summary>
        /// Handles the Click event of SaveToolStripMenuItem object.
        /// </summary>
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IsPdfFileSaving = true;

            // save PDF document
            SavePdfDocument();

            IsPdfFileSaving = false;
        }

        /// <summary>
        /// Handles the Click event of SaveAsToolStripMenuItem object.
        /// </summary>
        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IsPdfFileSaving = true;

            // save PDF file to new source and switch to new source
            SavePdfDocumentAs();

            IsPdfFileSaving = false;
        }


        /// <summary>
        /// Handles the Click event of PackToolStripMenuItem object.
        /// </summary>
        private void packToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IsPdfFileSaving = true;

            // show convert/pack dialog
            using (SelectPdfFormatForm dlg = new SelectPdfFormatForm(_document.Format, _document.EncryptionSystem))
            {
                if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    if (SavePdfDocumentAs() == System.Windows.Forms.DialogResult.OK)
                    {
                        ClearRuntimeMessages();
                        try
                        {
                            // pack PDF document with specified format
                            long oldSize = _document.StreamLength;
                            StartAction("Pack", true);
                            _document.Pack(dlg.Format, dlg.NewEncryptionSettings);
                            EndAction();
                            Filename = Filename;
                            MessageBox.Show(string.Format("{0} -> {1} bytes", oldSize, _document.StreamLength), "Convert/Pack results");
                        }
                        catch (Exception ex)
                        {
                            DemosTools.ShowErrorMessage(ex);
                        }
                        ShowRuntimeMessages();
                    }
                }
            }

            IsPdfFileSaving = false;
        }

        /// <summary>
        /// Handles the Click event of OptimizeToolStripMenuItem object.
        /// </summary>
        private void optimizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IsPdfFileSaving = true;

            // show optimize dialog
            using (OptimizeForm dlg = new OptimizeForm(_document.Format, _document.EncryptionSystem))
            {
                if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    if (SavePdfDocumentAs() == System.Windows.Forms.DialogResult.OK)
                    {
                        try
                        {
                            // optimize PDF document
                            ClearRuntimeMessages();
                            long oldSize = _document.StreamLength;
                            StartAction("Optimize", true);
                            PdfOptimizeSettings settings = new PdfOptimizeSettings(dlg.FilterForColorImages, dlg.FilterForBWImages, dlg.FilterForData);
                            _document.Optimize(dlg.Format, dlg.NewEncryptionSettings, settings);
                            EndAction();
                            ShowRuntimeMessages();
                            MessageBox.Show(string.Format("{0} -> {1} bytes", oldSize, _document.StreamLength), "Optimize results");
                        }
                        catch (Exception ex)
                        {
                            DemosTools.ShowErrorMessage(ex);
                        }
                    }
                }
            }

            IsPdfFileSaving = false;
        }


        /// <summary>
        /// Handles the Click event of ExitToolStripMenuItem object.
        /// </summary>
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // close PDF document
            ClosePdfDocument();

            // exit application
            Close();
        }

        #endregion


        #region 'View' menu

        /// <summary>
        /// Handles the Click event of RendererSettingsToolStripMenuItem object.
        /// </summary>
        private void rendererSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // show PDF renderer settings dialog
            using (PdfRenderingSettingsForm dlg = new PdfRenderingSettingsForm(_document.RenderingSettings))
            {
                dlg.ShowDialog();
            }
            DrawFocusedPage();
        }

        /// <summary>
        /// Handles the Click event of ImageViewerSettingsToolStripMenuItem object.
        /// </summary>
        private void imageViewerSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // show image viewer settings dialog 
            using (ImageViewerSettingsForm dlg = new ImageViewerSettingsForm(rendererImage))
            {
                dlg.CanEditMultipageSettings = false;
                dlg.ShowDialog();
            }
        }


        /// <summary>
        /// Handles the Click event of ColorManagementToolStripMenuItem object.
        /// </summary>
        private void colorManagementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // show a dialog that allows to edit the color management settings
            using (ColorManagementSettingsForm dialog = new ColorManagementSettingsForm())
            {
                if (_colorManagementSettings != null)
                    dialog.ColorManagementSettings = _colorManagementSettings;

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    _colorManagementSettings = dialog.ColorManagementSettings;

                    if (_document != null)
                    {
                        if (_document.DecodingSettings == null)
                            _document.DecodingSettings = new DecodingSettings();
                        _document.DecodingSettings.ColorManagement = _colorManagementSettings;

                        DrawFocusedPage();
                    }
                }
            }
        }

        #endregion


        #region 'Document' menu

        #region Document information

        /// <summary>
        /// Handles the Click event of DocumentInformationToolStripMenuItem object.
        /// </summary>
        private void documentInformationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // show PDF document information dialog
            PdfDemosTools.ShowDocumentInformation(_document, true, null);
        }

        #endregion


        #region Verification

        /// <summary>
        /// Handles the Click event of PdfA1bVerifierToolStripMenuItem object.
        /// </summary>
        private void pdfA1bVerifierToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VerifyPdfDocumentForCompatibilityWithPdfA(new PdfA1bVerifier());
        }

        /// <summary>
        /// Handles the Click event of PdfA2bVerifierToolStripMenuItem object.
        /// </summary>
        private void pdfA2bVerifierToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VerifyPdfDocumentForCompatibilityWithPdfA(new PdfA2bVerifier());
        }

        /// <summary>
        /// Handles the Click event of PdfA3bVerifierToolStripMenuItem object.
        /// </summary>
        private void pdfA3bVerifierToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VerifyPdfDocumentForCompatibilityWithPdfA(new PdfA3bVerifier());
        }

        /// <summary>
        /// Handles the Click event of PdfA1aVerifierToolStripMenuItem object.
        /// </summary>
        private void pdfA1aVerifierToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VerifyPdfDocumentForCompatibilityWithPdfA(new PdfA1aVerifier());
        }

        /// <summary>
        /// Handles the Click event of PdfA2aVerifierToolStripMenuItem object.
        /// </summary>
        private void pdfA2aVerifierToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VerifyPdfDocumentForCompatibilityWithPdfA(new PdfA2aVerifier());
        }

        /// <summary>
        /// Handles the Click event of PdfA3aVerifierToolStripMenuItem object.
        /// </summary>
        private void pdfA3aVerifierToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VerifyPdfDocumentForCompatibilityWithPdfA(new PdfA3aVerifier());
        }

        /// <summary>
        /// Handles the Click event of PdfA2uVerifierToolStripMenuItem object.
        /// </summary>
        private void pdfA2uVerifierToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VerifyPdfDocumentForCompatibilityWithPdfA(new PdfA2uVerifier());
        }

        /// <summary>
        /// Handles the Click event of PdfA3uVerifierToolStripMenuItem object.
        /// </summary>
        private void pdfA3uVerifierToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VerifyPdfDocumentForCompatibilityWithPdfA(new PdfA3uVerifier());
        }

        #endregion


        #region Conversion

        /// <summary>
        /// Handles the Click event of PdfA1bConverterToolStripMenuItem object.
        /// </summary>
        private void pdfA1bConverterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConvertPdfDocumentToPdfA(new PdfA1bConverter());
        }

        /// <summary>
        /// Handles the Click event of PdfA2bConverterToolStripMenuItem object.
        /// </summary>
        private void pdfA2bConverterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConvertPdfDocumentToPdfA(new PdfA2bConverter());
        }

        /// <summary>
        /// Handles the Click event of PdfA3bConverterToolStripMenuItem object.
        /// </summary>
        private void pdfA3bConverterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConvertPdfDocumentToPdfA(new PdfA3bConverter());
        }

        /// <summary>
        /// Handles the Click event of PdfA1aConverterToolStripMenuItem object.
        /// </summary>
        private void pdfA1aConverterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConvertPdfDocumentToPdfA(new PdfA1aConverter());
        }

        /// <summary>
        /// Handles the Click event of PdfA2aConverterToolStripMenuItem object.
        /// </summary>
        private void pdfA2aConverterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConvertPdfDocumentToPdfA(new PdfA2aConverter());
        }

        /// <summary>
        /// Handles the Click event of PdfA3aConverterToolStripMenuItem object.
        /// </summary>
        private void pdfA3aConverterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConvertPdfDocumentToPdfA(new PdfA3aConverter());
        }

        /// <summary>
        /// Handles the Click event of PdfA2uConverterToolStripMenuItem object.
        /// </summary>
        private void pdfA2uConverterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConvertPdfDocumentToPdfA(new PdfA2uConverter());
        }

        /// <summary>
        /// Handles the Click event of PdfA3uConverterToolStripMenuItem object.
        /// </summary>
        private void pdfA3uConverterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConvertPdfDocumentToPdfA(new PdfA3uConverter());
        }


        #endregion


        #region Security

        /// <summary>
        /// Handles the Click event of SecurityPropertiesToolStripMenuItem object.
        /// </summary>
        private void securityPropertiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // show PDF document security properties dialog
            using (SecurityPropertiesForm dlg = new SecurityPropertiesForm(_document))
            {
                dlg.ShowDialog();
            }
        }

        /// <summary>
        /// Handles the Click event of SecuritySettingsToolStripMenuItem object.
        /// </summary>
        private void securitySettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IsPdfFileSaving = true;

            // show a dialog with PDF document security settings
            using (SecuritySettingsForm dlg = new SecuritySettingsForm(_document.EncryptionSystem))
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    if (dlg.NewEncryptionSettings != _document.EncryptionSystem)
                    {
                        if (SavePdfDocumentAs() == System.Windows.Forms.DialogResult.OK)
                        {
                            ClearRuntimeMessages();
                            try
                            {
                                long oldSize = _document.StreamLength;
                                StartAction("Pack (Change Security Settings)", true);
                                _document.Pack(_document.Format, dlg.NewEncryptionSettings);
                                EndAction();
                                Filename = Filename;
                            }
                            catch (Exception ex)
                            {
                                DemosTools.ShowErrorMessage(ex);
                            }
                            ShowRuntimeMessages();
                        }
                    }
                }
            }

            IsPdfFileSaving = false;
        }

        #endregion


        #region Digital signatures

        /// <summary>
        /// Handles the Click event of DigitalSignaturesToolStripMenuItem object.
        /// </summary>
        private void digitalSignaturesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                using (DocumentSignaturesForm dialog = new DocumentSignaturesForm(_document))
                {
                    dialog.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                DemosTools.ShowErrorMessage(ex);
            }
        }

        #endregion


        #region Bookmarks

        /// <summary>
        /// Handles the Click event of AddBookmarkToolStripMenuItem1 object.
        /// </summary>
        private void addBookmarkToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (documentBookmarks.Document == null)
                documentBookmarks.Document = _document;

            // add new bookmark
            documentBookmarks.AddBookmark(FocusedPageIndex);
        }

        /// <summary>
        /// Handles the Click event of DeleteBookmarkNodeToolStripMenuItem object.
        /// </summary>
        private void deleteBookmarkNodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (documentBookmarks.Document == null)
                documentBookmarks.Document = _document;

            // delete bookmark
            documentBookmarks.DeleteSelectedBookmark();
        }

        /// <summary>
        /// Handles the Click event of EditBookmarkToolStripMenuItem object.
        /// </summary>
        private void editBookmarkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (documentBookmarks.Document == null)
                documentBookmarks.Document = _document;

            // edit bookmark
            documentBookmarks.EditSelectedBookmark();
        }

        #endregion


        #region Document view mode

        /// <summary>
        /// Handles the SelectedIndexChanged event of DocumentViewModeComboBox object.
        /// </summary>
        private void documentViewModeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Filename != null)
            {
                // change document view mode 
                _document.DocumentViewMode = (PdfDocumentViewMode)Enum.Parse(typeof(PdfDocumentViewMode), pageModeComboBox.SelectedItem.ToString());
            }
        }

        #endregion


        #region Thumbnails

        /// <summary>
        /// Handles the Click event of GenerateToolStripMenuItem object.
        /// </summary>
        private void generateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StartAction("Generate thumbnails", true);
            try
            {
                // create embedded thumbnails in PDF document
                _document.Pages.CreateThumbnails(128, 128);
            }
            catch (Exception ex)
            {
                DemosTools.ShowErrorMessage(ex);
            }
            EndAction();
        }

        /// <summary>
        /// Handles the Click event of RemoveToolStripMenuItem object.
        /// </summary>
        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // remove embedded thumbnails from PDF document
            _document.Pages.RemoveThumbnails();
        }

        /// <summary>
        /// Handles the Click event of ViewToolStripMenuItem1 object.
        /// </summary>
        private void viewToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            List<PdfImageResource> thumbs = new List<PdfImageResource>();
            for (int i = 0; i < _document.Pages.Count; i++)
            {
                if (_document.Pages[i].Thumbnail != null)
                    thumbs.Add(_document.Pages[i].Thumbnail);
            }

            if (thumbs.Count > 0)
            {
                // show a dialog with embedded thumbnails of PDF document
                using (PdfResourcesViewerForm dlg = new PdfResourcesViewerForm(_document, thumbs.ToArray()))
                {
                    dlg.StartPosition = FormStartPosition.CenterParent;
                    dlg.Owner = this;
                    dlg.ShowDialog();
                }
            }
            else
            {
                MessageBox.Show("PDF document does not have embedded thumbnails.");
            }
        }

        #endregion


        #region Actions

        /// <summary>
        /// Handles the Click event of DocumentActionsToolStripMenuItem object.
        /// </summary>
        private void documentActionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // show a form that allows to view and edit actions of PDF document
            using (PdfTriggersEditorForm dlg = new PdfTriggersEditorForm(_document))
            {
                dlg.Owner = this;
                dlg.StartPosition = FormStartPosition.CenterParent;

                dlg.ShowDialog();
            }
        }

        #endregion


        #region Attachments and embedded files

        /// <summary>
        /// Handles the Click event of AttachmentsPortfolioToolStripMenuItem object.
        /// </summary>
        private void attachmentsPortfolioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_document.Attachments == null)
            {
                if (MessageBox.Show("Document does not have attachments, create attachments?", "", MessageBoxButtons.YesNo) == DialogResult.No)
                    return;
            }

            // show attachments editor form
            using (AttachmentsEditorForm dlg = new AttachmentsEditorForm(_document))
            {
                dlg.ShowDialog();
                UpdateUI();
            }
        }

        /// <summary>
        /// Handles the Click event of RemoveAttachmentsPortfolioToolStripMenuItem object.
        /// </summary>
        private void removeAttachmentsPortfolioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Remove all attachments?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                // remove all attachments
                _document.RemoveAttachments(true);
                UpdateUI();
            }
        }

        /// <summary>
        /// Handles the Click event of EmbeddedFilesToolStripMenuItem object.
        /// </summary>
        private void embeddedFilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // show a dialog with embedded files of PDF document
            using (EmbeddedFilesForm dlg = new EmbeddedFilesForm())
            {
                dlg.Document = _document;
                dlg.ShowDialog();
            }
        }

        #endregion


        #region Resources

        /// <summary>
        /// Handles the Click event of DocumentResourcesToolStripMenuItem object.
        /// </summary>
        private void documentResourcesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // show a dialog with PDF document resources
            using (PdfResourcesViewerForm dlg = new PdfResourcesViewerForm(_document))
            {
                dlg.StartPosition = FormStartPosition.CenterParent;
                dlg.Owner = this;
                dlg.ShowDialog();

                if (dlg.PropertyValueChanged)
                {
                    _document.ClearCache();
                    DrawFocusedPage();
                }
            }
        }

        #endregion


        #region Fonts

        /// <summary>
        /// Handles the Click event of FontsToolStripMenuItem object.
        /// </summary>
        private void fontsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                PdfFont[] fonts = _document.GetFonts();
                if (fonts.Length == 0)
                {
                    MessageBox.Show("This document does not contain fonts.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // show a dialog with PDF document fonts
                    using (ViewDocumentFontsForm dlg = new ViewDocumentFontsForm(fonts))
                    {
                        dlg.ShowDialog();
                    }
                }
            }
            catch (Exception ex)
            {
                DemosTools.ShowErrorMessage(ex);
            }
        }

        /// <summary>
        /// Handles the Click event of EmbedAllFontsToolStripMenuItem object.
        /// </summary>
        private void embedAllFontsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string message = "Do you want to embed all external fonts of the document?";
            if (MessageBox.Show(message, "Warning", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                try
                {
                    // embed all document fonts into PDF document
                    _document.FontManager.EmbedAllFonts();
                    DemosTools.ShowInfoMessage("Embed fonts", "Done.");
                }
                catch (Exception ex)
                {
                    DemosTools.ShowErrorMessage(ex);
                }
            }
        }

        /// <summary>
        /// Handles the Click event of SubsetAllFontsToolStripMenuItem object.
        /// </summary>
        private void subsetAllFontsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string message = "Do you want to subset all fonts (remove all unused symbols from the fonts) of the document?";
            if (MessageBox.Show(message, "Warning", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                try
                {
                    // subset (pack) all document fonts of PDF document
                    _document.FontManager.PackAllFonts();
                    DemosTools.ShowInfoMessage("Subset fonts", "Done.");
                }
                catch (Exception ex)
                {
                    DemosTools.ShowErrorMessage(ex);
                }
            }
        }

        #endregion


        #region Basic object tree

        /// <summary>
        /// Handles the Click event of BasicObjectsTreeToolStripMenuItem object.
        /// </summary>
        private void basicObjectsTreeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // show PDF basic object tree
            using (PdfBasicObjectTreeViewerForm dlg = new PdfBasicObjectTreeViewerForm())
            {
                dlg.RootObject = PdfIndirectObject.GetByReference(_document.Catalog.IndirectReference);
                dlg.ShowDialog();
            }
        }

        /// <summary>
        /// Handles the Click event of PDFTreeToolStripMenuItem object.
        /// </summary>
        private void pDFTreeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // show PDF tree
            using (PdfTreeViewerForm dlg = new PdfTreeViewerForm())
            {
                dlg.RootObject = _document;
                dlg.ShowDialog();
            }
        }

        #endregion

        #endregion


        #region 'Page' menu

        /// <summary>
        /// Handles the Click event of PropertiesToolStripMenuItem object.
        /// </summary>
        private void propertiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // show dialog with properties of focused PDF page
            using (PropertyGridForm dlg = new PropertyGridForm(FocusedPage, "Page properties"))
            {
                dlg.PropertyGrid.PropertyValueChanged += new PropertyValueChangedEventHandler(propertyGrid_PropertyValueChanged);
                dlg.ShowDialog();
            }
            UpdateUI();
        }

        private void propertyGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            DrawFocusedPage();
        }

        /// <summary>
        /// Handles the Click event of PageResourcesToolStripMenuItem object.
        /// </summary>
        private void pageResourcesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // show a dialog with resources of focused PDF page
            using (PdfResourcesViewerForm dlg = new PdfResourcesViewerForm(FocusedPage))
            {
                dlg.StartPosition = FormStartPosition.CenterParent;
                dlg.Owner = this;
                dlg.ShowDialog();

                if (dlg.PropertyValueChanged)
                {
                    _document.ClearCache();
                    DrawFocusedPage();
                }
            }
        }

        /// <summary>
        /// Handles the Click event of DeletePageToolStripMenuItem object.
        /// </summary>
        private void deletePageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // delete focused PDF page
            DeleteFocusedPage();

            // update the UI
            UpdateUI();
        }

        /// <summary>
        /// Handles the Click event of CopyPageToolStripMenuItem object.
        /// </summary>
        private void copyPageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // copy focused PDF page
            CopyFocusedPage();

            // update the UI
            UpdateUI();
        }

        /// <summary>
        /// Handles the Click event of PasteToolStripMenuItem object.
        /// </summary>
        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PdfPage pastePage = _pageBuffer;
            if (_pageBuffer.Document.Equals(_document))
                pastePage = (PdfPage)pastePage.Clone();

            int pageIndex = FocusedPageIndex;
            if (FocusedPageIndex < 0)
                pageIndex = 0;

            // insert PDF page
            Pages.Insert(pageIndex, pastePage);

            // update the UI
            UpdateUI();

            SetFocusedPageIndex(pageIndex, true);
        }

        /// <summary>
        /// Handles the Click event of CutPageToolStripMenuItem object.
        /// </summary>
        private void cutPageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // copy and delete focused PDF page
            CopyFocusedPage();
            DeleteFocusedPage();

            // update the UI
            UpdateUI();
        }

        /// <summary>
        /// Handles the Click event of InsertPageToolStripMenuItem object.
        /// </summary>
        private void insertPageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IsPageInserting = true;

            using (InsertPageForm dlg = new InsertPageForm(_document, FocusedPageIndex))
            {
                // show a dialog for inserting PDF page
                DialogResult result = dlg.ShowDialog();
                if (result == DialogResult.OK)
                {
                    UpdateUI();
                    SetFocusedPageIndex(dlg.PageIndex, true);
                }
            }

            IsPageInserting = false;
        }

        /// <summary>
        /// Handles the Click event of SavePageAsImageToolStripMenuItem object.
        /// </summary>
        private void savePageAsImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // show image saving dialog
            if (saveImageFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                // get page rendering settings
                PdfRenderingSettings settingsBak = _document.RenderingSettings;
                PdfRenderingSettings settings = (PdfRenderingSettings)_document.RenderingSettings.Clone();
                settings.Zoom = 1f;
                _document.RenderingSettings = settings;
                PdfRenderingSettingsForm rendererSettings = new PdfRenderingSettingsForm(settings);
                rendererSettings.ShowDialog();
                StartAction("Rendering", false);
                // render page
                VintasoftImage img = FocusedPage.Render();
                EndAction();
                // save a page as a PNG file
                img.Save(saveImageFileDialog.FileName);
                img.Dispose();
                _document.RenderingSettings = settingsBak;
            }
        }

        #endregion


        #region 'Help' menu

        /// <summary>
        /// Handles the Click event of AboutToolStripMenuItem object.
        /// </summary>
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (AboutBoxForm dlg = new AboutBoxForm())
            {
                dlg.ShowDialog();
            }
        }

        #endregion


        #region PDF bookmarks

        /// <summary>
        /// Handles the AfterSelect event of DocumentBookmarks object.
        /// </summary>
        private void documentBookmarks_AfterSelect(object sender, TreeViewEventArgs e)
        {
            // get bookmark destination
            PdfDestinationBase dest = ((PdfBookmark)documentBookmarks.SelectedNode.Tag).Destination;
            // if destination exists
            if (dest != null)
            {
                int pageIndex = _document.Pages.IndexOf(dest.Page);
                if (pageIndex >= 0 && pageIndex < Pages.Count)
                {
                    // switch to destination page in image viewer
                    viewerToolStrip.SelectedPageIndex = pageIndex;
                }
            }
        }

        #endregion


        /// <summary>
        /// Handles the Click event of ButtonShowRuntimeMessages object.
        /// </summary>
        private void buttonShowRuntimeMessages_Click(object sender, EventArgs e)
        {
            // show document runtime messages
            PdfDocumentMessagesForm dlg = new PdfDocumentMessagesForm(_document.RuntimeMessages);
            dlg.ShowDialog();
        }

        /// <summary>
        /// Handles the PageIndexChanged event of ViewerToolStrip1 object.
        /// </summary>
        private void viewerToolStrip1_PageIndexChanged(object sender, PageIndexChangedEventArgs e)
        {
            SetFocusedPageIndex(viewerToolStrip.SelectedPageIndex, false);
        }

        #endregion


        #region UI state

        /// <summary>
        /// Updates the user interface of this form.
        /// </summary>
        private void UpdateUI()
        {
            // get the current status of application

            bool isPdfFileOpening = IsPdfFileOpening;
            bool isPdfFileLoaded = _document != null;
            bool isPdfFileNewDocument = IsNewDocument;
            bool isPdfFileNameEmpty = _fileName == null;
            bool isPdfFileReadOnlyMode = IsPdfFileReadOnlyMode;
            bool isPdfFileEmpty = true;
            bool isPdfFileSaving = IsPdfFileSaving;
            bool isPageInserting = IsPageInserting;
            bool isInvalidFocusedPageIndex = FocusedPageIndex < 0;
            bool isPageBufferEmpty = _pageBuffer == null;

            if (isPdfFileLoaded)
            {
                isPdfFileEmpty = _document.Pages.Count <= 0;
                isInvalidFocusedPageIndex = isInvalidFocusedPageIndex && FocusedPageIndex >= Pages.Count;
            }
            bool isContainsDigitalSignatures = isPdfFileLoaded && _document.InteractiveForm != null &&
                _document.InteractiveForm.GetTerminalFields().Length > 0;


            // "File" menu

            fileToolStripMenuItem.Enabled = !isPdfFileOpening && !isPdfFileSaving && !isPageInserting;

            addToolStripMenuItem.Enabled = isPdfFileLoaded;
            saveToolStripMenuItem.Enabled = isPdfFileLoaded && !isPdfFileEmpty && !isPdfFileReadOnlyMode &&
                                            _document.IsChanged && !isPdfFileNewDocument;
            saveAsToolStripMenuItem.Enabled = isPdfFileLoaded && !isPdfFileEmpty;
            closeToolStripMenuItem.Enabled = isPdfFileLoaded;
            convertToolStripMenuItem.Enabled = isPdfFileLoaded && !isPdfFileEmpty;
            optimizeToolStripMenuItem.Enabled = isPdfFileLoaded && !isPdfFileEmpty;
            securitySettingsToolStripMenuItem.Enabled = isPdfFileLoaded && !isPdfFileReadOnlyMode &&
                                                        !isPdfFileNameEmpty;


            // "View" menu

            rendererSettingsToolStripMenuItem.Enabled = isPdfFileLoaded && !isPdfFileOpening && !isPdfFileSaving;


            // "Document" menu

            documentToolStripMenuItem.Enabled = isPdfFileLoaded && !isPdfFileOpening && !isPdfFileSaving;

            bookmarksToolStripMenuItem.Enabled = !isPdfFileEmpty;
            pageThumbnailsToolStripMenuItem.Enabled = !isPdfFileReadOnlyMode && !isPdfFileEmpty;
            documentActionsToolStripMenuItem.Enabled = !isPdfFileEmpty;
            documentInformationToolStripMenuItem.Enabled = !isPdfFileNameEmpty;
            documentResourcesToolStripMenuItem.Enabled = !isPdfFileEmpty;
            fontsToolStripMenuItem.Enabled = !isPdfFileEmpty;
            digitalSignaturesToolStripMenuItem.Enabled = isContainsDigitalSignatures;
            removeAttachmentsPortfolioToolStripMenuItem.Enabled = isPdfFileLoaded && _document.Attachments != null;


            // "Page" menu

            pageToolStripMenuItem.Enabled = isPdfFileLoaded && !isPdfFileOpening && !isPdfFileSaving;

            cutPageToolStripMenuItem.Enabled = !isPageInserting && !isInvalidFocusedPageIndex && !isPdfFileEmpty;
            copyPageToolStripMenuItem.Enabled = !isPdfFileEmpty;
            pasteToolStripMenuItem.Enabled = !isPageInserting && !isPageBufferEmpty;
            deletePageToolStripMenuItem.Enabled = !isPageInserting && !isInvalidFocusedPageIndex && !isPdfFileEmpty;
            insertPageToolStripMenuItem.Enabled = !isPageInserting;
            savePageAsImageToolStripMenuItem.Enabled = !isPdfFileEmpty;
            pageResourcesToolStripMenuItem.Enabled = !isPdfFileEmpty;


            // document bookmarks & render image
            panel11.Enabled = isPdfFileLoaded && !isPdfFileOpening && !isPdfFileSaving && !isPdfFileEmpty;

            // viewer tool
            viewerToolStrip.Enabled = !IsPdfFileOpening && !isPdfFileSaving;

            // update form title
            UpdateFormTitle();

            // update pages count
            if (isPdfFileLoaded)
            {
                if (isPdfFileEmpty)
                {
                    _focusedPageIndex = -1;
                    DrawFocusedPage();
                }
                if (viewerToolStrip.PageCount != Pages.Count)
                    viewerToolStrip.PageCount = Pages.Count;
            }
        }

        /// <summary>
        /// Updates the form title.
        /// </summary>
        private void UpdateFormTitle()
        {
            bool isPdfFileNameEmpty = _fileName == null;
            bool isPdfFileReadOnlyMode = IsPdfFileReadOnlyMode;

            if (!isPdfFileNameEmpty)
            {
                Text = string.Format(_titlePrefix, " - " + Path.GetFileName(_fileName));
                if (_document.IsEncrypted)
                    Text += " (SECURED)";
                if (isPdfFileReadOnlyMode)
                    Text += " (Read Only)";
            }
            else
            {
                Text = string.Format(_titlePrefix, "");
            }
        }

        #endregion


        #region PDF document

        /// <summary>
        /// Opens PDF document.
        /// </summary>
        /// <param name="documentFilename">Document file name.</param>
        private void OpenPdfDocument(string documentFilename)
        {
            ClosePdfDocument();

            IsPdfFileReadOnlyMode = false;

            PdfRenderingSettings settings = null;
            if (_document != null)
                settings = _document.RenderingSettings;

            try
            {
                // open a PDF document from file
                _document = new PdfDocument(documentFilename);
            }
            catch (Exception)
            {
                // open a PDF document from file in read-only mode
                _document = new PdfDocument(documentFilename, true);
                IsPdfFileReadOnlyMode = true;
            }

            if (_colorManagementSettings != null)
            {
                if (_document.DecodingSettings == null)
                    _document.DecodingSettings = new DecodingSettings();

                // get document color management settings
                _document.DecodingSettings.ColorManagement = _colorManagementSettings;
            }

            if (!PdfDocumentPasswordForm.Authenticate(_document, documentFilename))
            {
                ClosePdfDocument();
                return;
            }

            if (settings != null)
            {
                // get document rendering settings
                _document.RenderingSettings = settings;
            }
            else
            {
                _document.RenderingSettings.DrawVintasoftAnnotations = true;
            }

            _document.FontProgramsController = new CustomFontProgramsController();

            _document.Progress += new EventHandler<ImageFileProgressEventArgs>(Document_Progress);

            if (_document.RuntimeMessages.Count > 0)
            {
                for (int i = 0; i < _document.RuntimeMessages.Count; i++)
                {
                    PdfRuntimeMessage message = _document.RuntimeMessages[i];
                    MessageBoxIcon icon = MessageBoxIcon.Information;
                    string caption = "Message";
                    if (message is PdfRuntimeError)
                    {
                        icon = MessageBoxIcon.Error;
                        caption = "Error";
                    }
                    else if (message is PdfRuntimeWarning)
                    {
                        icon = MessageBoxIcon.Warning;
                        caption = "Warning";
                    }
                    MessageBox.Show(message.Message, caption, MessageBoxButtons.OK, icon);
                }
            }

            _document.DocumentChanged += new EventHandler(Document_DocumentChanged);

            IsNewDocument = false;
            Filename = documentFilename;
            viewerToolStrip.SelectedPageIndex = 0;
            documentBookmarks.Document = _document;
            pageModeComboBox.SelectedItem = _document.DocumentViewMode.ToString();
        }

        /// <summary>
        /// Closes PDF document.
        /// </summary>
        private void ClosePdfDocument()
        {
            ClearRuntimeMessages();
            if (_pageBuffer != null && !_pageBuffer.Document.Equals(_tempDocument))
            {
                _tempDocument.Pages.Clear();
                _tempDocument.Pages.Add(_pageBuffer);
                _pageBuffer = _tempDocument.Pages[0];
            }
            if (Filename != null)
            {
                documentBookmarks.Document = null;
                _focusedPageIndex = -1;
                rendererImage.Image = null;
                Filename = null;
            }
            if (_document != null)
            {
                _document.DocumentChanged -= Document_DocumentChanged;
                _document.Dispose();
                _document = null;
            }
            Text = string.Format(_titlePrefix, "");
            viewerToolStrip.PageCount = 0;
        }

        /// <summary>
        /// Returns a filename for new PDF document.
        /// </summary>
        /// <returns>Filename for new PDF document.</returns>
        private string GetNewDocumentFilename()
        {
            return "Document1.pdf";
        }

        /// <summary>
        /// Handler of the PdfDocument.DocumentChanged event.
        /// </summary>
        private void Document_DocumentChanged(object sender, EventArgs e)
        {
            UpdateUI();
        }

        /// <summary>
        /// Handler of the PdfDocument.Progress event.
        /// </summary>
        private void Document_Progress(object sender, ImageFileProgressEventArgs e)
        {
            progressBar.Value = e.Progress;
            Application.DoEvents();
            progressBar.Visible = e.Progress != 100;
        }

        /// <summary>
        /// Clears PDF document runtime messages.
        /// </summary>
        private void ClearRuntimeMessages()
        {
            if (_document != null)
                _document.ClearRuntimeMessages();
            ShowRuntimeMessages();
        }

        /// <summary>
        /// Shows PDF document runtime messages.
        /// </summary>
        private void ShowRuntimeMessages()
        {
            int count = 0;
            if (_document != null)
                count = _document.RuntimeMessages.Count;

            buttonShowRuntimeMessages.Enabled = count > 0;
            buttonShowRuntimeMessages.Text = string.Format("Messages ({0})", count);
        }

        #endregion


        #region Save PDF document

        /// <summary>
        /// Saves the PDF document.
        /// </summary>
        private void SavePdfDocument()
        {
            StartAction("Save", true);
            try
            {
                _document.SaveChanges();
            }
            catch (Exception e)
            {
                DemosTools.ShowErrorMessage("Saving error", e);
            }
            EndAction();
        }

        /// <summary>
        /// Shows "Save As" dialog and saves PDF document.
        /// </summary>
        /// <returns>Saving result.</returns>
        private DialogResult SavePdfDocumentAs()
        {
            savePdfFileDialog.InitialDirectory = Path.GetDirectoryName(Filename);
            savePdfFileDialog.FileName = Filename;
            DialogResult result = savePdfFileDialog.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                if (savePdfFileDialog.FileName.ToLowerInvariant() == Filename.ToLowerInvariant())
                {
                    // save changes to the source PDF file
                    SavePdfDocument();
                }
                else
                {
                    IsNewDocument = false;
                    StartAction("Save As", true);
                    try
                    {
                        // save changes to a new PDF file
                        _document.SaveChanges(savePdfFileDialog.FileName);
                    }
                    catch (Exception e)
                    {
                        DemosTools.ShowErrorMessage("Saving error", e);
                    }
                    EndAction();

                    // switch to the new PDF document
                    Filename = savePdfFileDialog.FileName;
                }
            }
            return result;
        }

        #endregion


        #region Verify PDF/A document

        /// <summary>
        /// Verifies PDF document for compatibility with PDF/A format.
        /// </summary>
        /// <param name="pdfAVerifier">PDF/A verifier.</param>
        private void VerifyPdfDocumentForCompatibilityWithPdfA(PdfAVerifier pdfAVerifier)
        {
            DocumentProcessingCommandForm.ExecuteDocumentProcessing(_document, pdfAVerifier);
        }

        #endregion


        #region Convert PDF document to PDF/A

        /// <summary>
        /// Converts PDF document to PDF/A.
        /// </summary>
        /// <param name="pdfAConverter">PDF/A converter.</param>
        private void ConvertPdfDocumentToPdfA(PdfDocumentConverter pdfAConverter)
        {
            pdfAConverter.Started += new EventHandler<ProcessingEventArgs>(PdfAConverter_Started);
            pdfAConverter.Finished += new EventHandler<ProcessingFinishedEventArgs>(PdfAConverter_Finished);

            // convert PDF document to the PDF/A-1b format
            DocumentProcessingCommandForm.ExecuteDocumentProcessing(_document, pdfAConverter);
        }

        /// <summary>
        /// PDF/A conversion is started.
        /// </summary>
        private void PdfAConverter_Started(object sender, ProcessingEventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new EventHandler<ProcessingEventArgs>(PdfAConverter_Started), sender, e);
            }
            else
            {
                PdfDocument document = (PdfDocument)e.Target;

                string filename;
                if (document.SourceStream != null)
                {
                    // get name from the source file
                    filename = ((FileStream)document.SourceStream).Name;
                }
                else
                {
                    filename = "document1.pdf";
                }

                savePdfFileDialog.FileName = string.Format("{0}_Converted.pdf", Path.GetFileNameWithoutExtension(filename));

                // show document saving dialog
                if (savePdfFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string newFilename = Path.GetFullPath(savePdfFileDialog.FileName);

                    // if saving in the same file
                    if (Path.GetFullPath(filename).ToUpperInvariant() != newFilename.ToUpperInvariant())
                    {
                        document.Save(newFilename);
                        Stream documentStream = File.Open(newFilename, FileMode.Open, FileAccess.ReadWrite);
                        document = PdfDocumentController.OpenDocument(documentStream);
                        e.Target = document;
                    }
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }

        /// <summary>
        /// PDF/A conversion is finished.
        /// </summary>
        private void PdfAConverter_Finished(object sender, ProcessingFinishedEventArgs e)
        {
            PdfDocument document = (PdfDocument)e.Target;
            // if processed PDF document differs from PDF document opened in demo
            if (document != _document)
            {
                // get the stream of processed PDF document
                Stream documentStream = document.SourceStream;
                // closes the source of processed PDF document
                PdfDocumentController.CloseDocument(document);
                documentStream.Dispose();
            }
        }

        #endregion


        #region PDF pages

        /// <summary>
        /// Sets focused page index.
        /// </summary>
        /// <param name="index">New focused page index.</param>
        /// <param name="setInToolbar">A value indicating whether to set the index in the toolbar.</param>
        private void SetFocusedPageIndex(int index, bool setInToolbar)
        {
            if (_document != null && index < Pages.Count && index >= 0)
            {
                _focusedPageIndex = index;
                if (setInToolbar)
                    viewerToolStrip.SelectedPageIndex = _focusedPageIndex;
                DrawFocusedPage();
            }

            // update the UI
            UpdateUI();
        }

        /// <summary>
        /// Draws focused PDF page.
        /// </summary>
        private void DrawFocusedPage()
        {
            try
            {
                ClearRuntimeMessages();
                VintasoftImage image = rendererImage.Image;

                if (_focusedPageIndex >= 0)
                {
                    // render focused PDF page
                    StartAction("Rendering", false);
                    VintasoftImage renderedImage = FocusedPage.Render();
                    EndAction();
                    rendererImage.Image = renderedImage;
                }
                else
                {
                    rendererImage.Image = null;
                }

                if (image != null)
                    image.Dispose();

                ShowRuntimeMessages();
            }
            catch (Exception ex)
            {
                DemosTools.ShowErrorMessage(ex);
            }
        }

        /// <summary>
        /// Deletes focused PDF page.
        /// </summary>
        private void DeleteFocusedPage()
        {
            Pages.RemoveAt(FocusedPageIndex);
            if (FocusedPageIndex >= Pages.Count)
                FocusedPageIndex--;
            if (Pages.Count == 0)
                rendererImage.Image = null;

            DrawFocusedPage();
        }

        /// <summary>
        /// Copies the focused PDF page.
        /// </summary>
        private void CopyFocusedPage()
        {
            _pageBuffer = FocusedPage;
        }

        /// <summary>
        /// Adds pages from other PDF document.
        /// </summary>
        private void AddPages(string documentFilename)
        {
            try
            {
                // open other PDF document and add pages from it
                PdfDocument doc = new PdfDocument(documentFilename);
                Pages.AddRange(doc.Pages.ToArray());
                doc.Dispose();
                UpdateUI();
            }
            catch (Exception ex)
            {
                DemosTools.ShowErrorMessage(ex);
            }

            if (FocusedPageIndex == -1)
                FocusedPageIndex = 0;
        }

        #endregion


        #region Utils

        /// <summary>
        /// Starts the action.
        /// </summary>
        /// <param name="actionName">Name of the action.</param>
        /// <param name="progress">Action progress.</param>
        private void StartAction(string actionName, bool progress)
        {
            _actionStartTime = DateTime.Now;
            _actionName = actionName;

            if (progress)
                actionName += ":";
            else
                actionName += "...";

            actionTime.Text = actionName;
            statusStrip1.Refresh();
        }

        /// <summary>
        /// Ends the action.
        /// </summary>
        private void EndAction()
        {
            actionTime.Text = string.Format("{0}: {1} ms", _actionName, (DateTime.Now - _actionStartTime).TotalMilliseconds);
            progressBar.Visible = false;
        }

        #endregion

        #endregion

        
    }
}
