using System;
using System.Windows.Forms;

using Vintasoft.Imaging.Codecs.Encoders;
using Vintasoft.Imaging.Pdf;
using Vintasoft.Imaging.Pdf.Security;

using DemosCommonCode.Imaging;
using DemosCommonCode.Pdf;

namespace PdfStructureEditorDemo
{
    /// <summary>
    /// A form that allows to set parameters for optimization of PDF document.
    /// </summary>
    public partial class OptimizeForm : Form
    {

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="OptimizeForm"/> class.
        /// </summary>
        /// <param name="format">Information about document version.</param>
        /// <param name="initialEncryptionSettings">Information about document encryption.</param>
        public OptimizeForm(PdfFormat format, EncryptionSystem initialEncryptionSettings)
        {
            InitializeComponent();
            _format = format;
            _newEncryptionSettings = initialEncryptionSettings;

            bwJBIG2.Enabled = AvailableEncoders.IsEncoderAvailable("Jbig2");
            colorJpeg2000.Enabled = AvailableEncoders.IsEncoderAvailable("Jpeg2000");
        }

        #endregion



        #region Properties

        PdfFormat _format;
        /// <summary>
        /// Gets the infomation about version of PDF document.
        /// </summary>
        public PdfFormat Format
        {
            get
            {
                return _format;
            }
        }

        EncryptionSystem _newEncryptionSettings;
        /// <summary>
        /// Gets the information about encryption system of PDF document.
        /// </summary>
        public EncryptionSystem NewEncryptionSettings
        {
            get
            {
                return _newEncryptionSettings;
            }
        }

        PdfCompression _colorImagesFilter = PdfCompression.Undefined;
        /// <summary>
        /// Gets the PDF compression type for color images.
        /// </summary>
        public PdfCompression FilterForColorImages
        {
            get
            {
                return _colorImagesFilter;
            }
        }

        PdfCompression _BWImagesFilter = PdfCompression.Undefined;
        /// <summary>
        /// Gets the PDF compression type for black-white images.
        /// </summary>
        public PdfCompression FilterForBWImages
        {
            get
            {
                return _BWImagesFilter;
            }
        }

        PdfCompression _dataFilter = PdfCompression.Undefined;
        /// <summary>
        /// Gets the PDF compression type for binary and text data.
        /// </summary>
        public PdfCompression FilterForData
        {
            get
            {
                return _dataFilter;
            }
        }

        #endregion



        #region Methods

        #region UI

        /// <summary>
        /// Handles the Click event of okButton object.
        /// </summary>
        private void okButton_Click(object sender, EventArgs e)
        {
            if (!colorUndefined.Checked)
            {
                // get selected compression for color images
                if (colorNone.Checked)
                    _colorImagesFilter = PdfCompression.None;
                else if (colorFlate.Checked)
                    _colorImagesFilter = PdfCompression.Zip;
                else if (colorLZW.Checked)
                    _colorImagesFilter = PdfCompression.Lzw;
                else if (colorJpeg.Checked)
                    _colorImagesFilter = PdfCompression.Jpeg;
                else if (colorJpeg2000.Checked)
                    _colorImagesFilter = PdfCompression.Jpeg2000;
            }
            if (!bwUndefined.Checked)
            {
                // get selected compression for black-white images
                if (bwNone.Checked)
                    _BWImagesFilter = PdfCompression.None;
                else if (bwFlate.Checked)
                    _BWImagesFilter = PdfCompression.Zip;
                else if (bwLZW.Checked)
                    _BWImagesFilter = PdfCompression.Lzw;
                else if (bwCCITTFax.Checked)
                    _BWImagesFilter = PdfCompression.CcittFax;
                else if (bwJBIG2.Checked)
                    _BWImagesFilter = PdfCompression.Jbig2;
            }
            if (!dataUndefined.Checked)
            {
                // get selected compression for data
                if (dataNone.Checked)
                    _dataFilter = PdfCompression.None;
                else if (dataFlate.Checked)
                    _dataFilter = PdfCompression.Zip;
                else if (dataLZW.Checked)
                    _dataFilter = PdfCompression.Lzw;
            }
            DialogResult = System.Windows.Forms.DialogResult.OK;
            Close();
        }

        /// <summary>
        /// Handles the Click event of cancelButton object.
        /// </summary>
        private void cancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
            Close();
        }

        /// <summary>
        /// Handles the Click event of defaultFilterParamsButton object.
        /// </summary>
        private void defaultFilterParamsButton_Click(object sender, EventArgs e)
        {
            using (PropertyGridForm dlg = new PropertyGridForm(PdfCompressionSettings.DefaultSettings, "Default Compression Settings"))
            {
                // show a form that allows to specify PDF compression settings.
                dlg.ShowDialog();
            }
        }

        /// <summary>
        /// Handles the Click event of documentFormatButton object.
        /// </summary>
        private void documentFormatButton_Click(object sender, EventArgs e)
        {
            using (SelectPdfFormatForm dlg = new SelectPdfFormatForm(_format, _newEncryptionSettings))
            {
                // show a form that allows to specify PDF document format
                if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    _format = dlg.Format;
                    _newEncryptionSettings = dlg.NewEncryptionSettings;
                }
            }
        }

        #endregion

        #endregion

    }
}
