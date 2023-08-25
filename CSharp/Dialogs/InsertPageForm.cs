using System;
using System.Drawing;
using System.Windows.Forms;

using Vintasoft.Imaging;
using Vintasoft.Imaging.Codecs.Encoders;
using Vintasoft.Imaging.Pdf;
using Vintasoft.Imaging.Pdf.Tree;

using DemosCommonCode;
using DemosCommonCode.Imaging.Codecs.Dialogs;
using DemosCommonCode.Pdf;


namespace PdfStructureEditorDemo
{
    /// <summary>
    /// A form that allows to set parameters of page, which should be inserted into PDF document.
    /// </summary>
    public partial class InsertPageForm : Form
    {

        #region Fields

        /// <summary>
        /// Current PDF document.
        /// </summary>
        PdfDocument _pdfDocument;

        /// <summary>
        /// The image to be added as background of the page.
        /// </summary>
        VintasoftImage _image = null;

        /// <summary>
        /// Settings of the JPEG2000 encoder.
        /// </summary>
        Jpeg2000EncoderSettings _jpeg2000settings;

        /// <summary>
        /// Size of new PDF page.
        /// </summary>
        PaperSizeKind _paperSize = PaperSizeKind.Custom;

        /// <summary>
        /// Size of new PDF page in user units.
        /// </summary>
        SizeF _pageSizeInUserUnits;

        /// <summary>
        /// A value indicating whether this form can be closed.
        /// </summary>
        bool _canClose = false;

        #endregion



        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="InsertPageForm"/> class.
        /// </summary>
        /// <param name="document">Opened PDF document.</param>
        /// <param name="pageIndex">Zero-based page index to insert a new page.</param>
        public InsertPageForm(PdfDocument document, int pageIndex)
        {
            InitializeComponent();

            _pdfDocument = document;

            _pageIndex = pageIndex;

            filterComboBox.Items.Add(PdfCompression.Auto);
            filterComboBox.Items.Add(PdfCompression.None);
            filterComboBox.Items.Add(PdfCompression.Zip);
            filterComboBox.Items.Add(PdfCompression.Lzw);
            filterComboBox.Items.Add(PdfCompression.Jpeg);
            if (AvailableEncoders.IsEncoderAvailable("Jpeg2000"))
                filterComboBox.Items.Add(PdfCompression.Jpeg2000);
            filterComboBox.Items.Add(PdfCompression.CcittFax);
            if (AvailableEncoders.IsEncoderAvailable("Jbig2"))
                filterComboBox.Items.Add(PdfCompression.Jbig2);
            filterComboBox.SelectedIndex = 0;

            // default new(empty) page size
            _pageSizeInUserUnits = new SizeF();
            _pageSizeInUserUnits.Width = PdfPage.ConvertFromUnitOfMeasureToUserUnits(210, UnitOfMeasure.Millimeters);
            _pageSizeInUserUnits.Height = PdfPage.ConvertFromUnitOfMeasureToUserUnits(297, UnitOfMeasure.Millimeters);
        }

        #endregion



        #region Properties

        int _pageIndex;
        /// <summary>
        /// Gets or sets the zero-based index of PDF page.
        /// </summary>
        public int PageIndex
        {
            get { return _pageIndex; }
        }

        #endregion



        #region Methods

        #region PUBLIC

        /// <summary>
        /// Shows a form as a modal dialog box.
        /// </summary>
        /// <returns>One of the <see cref="DialogResult"/> values.</returns>
        public new DialogResult ShowDialog()
        {
            pageWithImageRadioButton.Checked = emptyPageRadioButton.Checked = false;
            insertIndexNumericUpDown.Maximum = _pdfDocument.Pages.Count + 1;
            insertIndexNumericUpDown.Value = Math.Max(1, _pageIndex + 1);
            return base.ShowDialog();
        }

        #endregion


        #region PRIVATE

        #region UI

        /// <summary>
        /// Handles the Click event of CancelButton object.
        /// </summary>
        private void cancelButton_Click(object sender, EventArgs e)
        {
            // specify that form can be closed
            _canClose = true;
            // close the form
            DialogResult = DialogResult.Cancel;
        }

        /// <summary>
        /// Handles the Click event of SelectImageButton object.
        /// </summary>
        private void selectImageButton_Click(object sender, EventArgs e)
        {
            if (openImageDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                _image = new VintasoftImage(openImageDialog.FileName);
                if (_image.Resolution.Horizontal == 0 || _image.Resolution.Vertical == 0)
                    _image.Resolution = new Resolution(96, 96);
            }
            UpdateUI();
        }

        /// <summary>
        /// Handles the Click event of OkButton object.
        /// </summary>
        private void okButton_Click(object sender, EventArgs e)
        {
            PdfPage page;
            if (pageWithImageRadioButton.Checked)
            {
                PdfCompression filter = (PdfCompression)filterComboBox.SelectedItem;
                PdfCompressionSettings filterParams = new PdfCompressionSettings();
                filterParams.JpegQuality = (int)jpegQuality.Value;
                filterParams.ZipCompressionLevel = (int)flateCompressionLevel.Value;
                filterParams.Jbig2Settings.Lossy = cbJbig2Lossy.Checked;
                filterParams.Jbig2UseGlobals = cbJbig2UseGlobals.Checked;
                filterParams.Jpeg2000Settings = _jpeg2000settings;
                try
                {
                    // create new page with image
                    page = new PdfPage(_pdfDocument, _image, filter, filterParams);
                }
                catch (Exception ex)
                {
                    DemosTools.ShowErrorMessage(ex);
                    return;
                }
                finally
                {
                    _image.Dispose();
                    _image = null;
                }
            }
            else
            {
                if (_paperSize == PaperSizeKind.Custom)
                {
                    // create empty page with custom size
                    page = new PdfPage(_pdfDocument, new RectangleF(Point.Empty, _pageSizeInUserUnits));
                }
                else
                {
                    // create empty page with specified size
                    page = new PdfPage(_pdfDocument, _paperSize);
                }
            }

            _pageIndex = (int)insertIndexNumericUpDown.Value - 1;
            _pdfDocument.Pages.Insert(_pageIndex, page);

            // specify that form can be closed
            _canClose = true;
            // close the form
            DialogResult = DialogResult.OK;
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of FilterComboBox object.
        /// </summary>
        private void filterComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateUI();
        }

        /// <summary>
        /// Handles the Click event of Jpeg2000EncoderSettingsButton object.
        /// </summary>
        private void jpeg2000EncoderSettingsButton_Click(object sender, EventArgs e)
        {
            using (Jpeg2000EncoderSettingsForm dlg = new Jpeg2000EncoderSettingsForm())
            {
                dlg.EncoderSettings = _jpeg2000settings;
                dlg.ShowDialog();

                _jpeg2000settings = dlg.EncoderSettings;
            }
        }

        /// <summary>
        /// Handles the CheckedChanged event of EmptyPageRadioButton object.
        /// </summary>
        private void emptyPageRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (emptyPageRadioButton.Checked)
                EditPageSize();

            UpdateUI();
        }

        /// <summary>
        /// Handles the CheckedChanged event of PageWithImageRadioButton object.
        /// </summary>
        private void pageWithImageRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            UpdateUI();
        }

        /// <summary>
        /// Handles the Click event of EditSizeButton object.
        /// </summary>
        private void editSizeButton_Click(object sender, EventArgs e)
        {
            EditPageSize();
        }

        /// <summary>
        /// Handles the FormClosing event of Form object.
        /// </summary>
        private void Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            // if form cannot be closed
            if (!_canClose)
                // cancel closing of this form
                e.Cancel = true;
        }

        #endregion


        /// <summary>
        /// Updates the user interface of this form.
        /// </summary>
        private void UpdateUI()
        {
            // get the current status of application
            bool isEmptyPage = emptyPageRadioButton.Checked;
            bool isPageWithImage = pageWithImageRadioButton.Checked;
            PdfCompression pdfCompression = (PdfCompression)filterComboBox.SelectedItem;
            bool isJpegFilterParams = pdfCompression == PdfCompression.Jpeg;
            bool isFlateFilterParams = pdfCompression == PdfCompression.Zip;
            bool isJbig2CodecParams = pdfCompression == PdfCompression.Jbig2;
            bool isJpeg2000EncoderSettings = pdfCompression == PdfCompression.Jpeg2000;

            // "Select image" button
            selectImageButton.Enabled = isPageWithImage;

            // "Size" button
            sizeButton.Enabled = isEmptyPage;

            // encoder
            filterComboBox.Enabled = isPageWithImage || isEmptyPage;

            // insert index
            insertIndexNumericUpDown.Enabled = isPageWithImage || isEmptyPage;

            // "OK" button
            okButton.Enabled = (isPageWithImage && _image != null) || isEmptyPage;

            // PDF compression settings
            jpegFilterParams.Visible = isJpegFilterParams;
            flateFilterParams.Visible = isFlateFilterParams;
            Jbig2CodecParams.Visible = isJbig2CodecParams;
            jpeg2000EncoderSettingsButton.Visible = isJpeg2000EncoderSettings;
        }

        /// <summary>
        /// Shows a form that allows to specify size of new empty page.
        /// </summary>
        private void EditPageSize()
        {
            using (AddEmptyPageForm dlg = new AddEmptyPageForm(_pageSizeInUserUnits, UnitOfMeasure.Centimeters))
            {
                // show "add empty page" dialog
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    // specify page size
                    _pageSizeInUserUnits = dlg.PageSizeInUserUnits;
                    _paperSize = dlg.PaperKind;
                }
            }
        }

        #endregion

        #endregion

    }
}
