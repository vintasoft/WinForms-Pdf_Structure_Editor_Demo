using System;
using System.Windows.Forms;
using Vintasoft.Imaging;
using Vintasoft.Imaging.Codecs.Decoders;
using Vintasoft.Imaging.Pdf;

namespace DemosCommonCode.Pdf
{
    /// <summary>
    /// A form that allows to view and edit the rendering settings of PDF document.
    /// </summary>
    public partial class PdfRenderingSettingsForm : Form
    {

        #region Constructor

        public PdfRenderingSettingsForm(PdfRenderingSettings settings)
        {
            InitializeComponent();

            okButton.Focus();

            _renderingSettings = settings;

            renderingMode.Items.Add(PdfRenderingMode.HighSpeed);
            renderingMode.Items.Add(PdfRenderingMode.Normal);
            renderingMode.Items.Add(PdfRenderingMode.HighQuality);
            renderingMode.SelectedItem = _renderingSettings.RenderingMode;

            if (settings.Resolution.IsEmpty())
            {
                defaultDpiCheckBox.Checked = true;
            }
            else
            {
                defaultDpiCheckBox.Checked = false;
                horizontalResolutionNumericUpDown.Value = (int)settings.Resolution.Horizontal;
                verticalResolutionNumericUpDown.Value = (int)settings.Resolution.Vertical;
            }
            drawAnnotationsCheckBox.Checked = _renderingSettings.DrawPdfAnnotations || _renderingSettings.DrawVintasoftAnnotations;
            cacheResourcesCheckBox.Checked = _renderingSettings.CacheResources;
            cropPageAtCropBoxCheckBox.Checked = _renderingSettings.UseCropBox;
            useRotatePropertyCheckBox.Checked = _renderingSettings.UsePageRotateProperty;
        }

        #endregion



        #region Properties

        PdfRenderingSettings _renderingSettings;
        public PdfRenderingSettings RenderingSettings
        {
            get
            {
                return _renderingSettings;
            }
        }

        #endregion



        #region Methods

        /// <summary>
        /// Handles the Click event of OkButton object.
        /// </summary>
        private void okButton_Click(object sender, EventArgs e)
        {
            if (defaultDpiCheckBox.Checked)
                _renderingSettings.Resolution = Resolution.Empty;
            else            
                _renderingSettings.Resolution = new Resolution((float)horizontalResolutionNumericUpDown.Value, (float)verticalResolutionNumericUpDown.Value);
            _renderingSettings.RenderingMode = (PdfRenderingMode)renderingMode.SelectedItem;
            _renderingSettings.DrawPdfAnnotations = drawAnnotationsCheckBox.Checked;
            _renderingSettings.DrawVintasoftAnnotations = drawAnnotationsCheckBox.Checked;
            _renderingSettings.CacheResources = cacheResourcesCheckBox.Checked;
            _renderingSettings.UseCropBox = cropPageAtCropBoxCheckBox.Checked;
            _renderingSettings.UsePageRotateProperty = useRotatePropertyCheckBox.Checked;
            DialogResult = DialogResult.OK;
        }

        /// <summary>
        /// Handles the CheckedChanged event of DefaultDpiCheckBox object.
        /// </summary>
        private void defaultDpiCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            horizontalResolutionNumericUpDown.Enabled = !defaultDpiCheckBox.Checked;
            verticalResolutionNumericUpDown.Enabled = !defaultDpiCheckBox.Checked;
        }

        /// <summary>
        /// Handles the Click event of CancelButton object.
        /// </summary>
        private void cancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        /// <summary>
        /// Handles the ValueChanged event of HorizontalResolution object.
        /// </summary>
        private void horizontalResolution_ValueChanged(object sender, EventArgs e)
        {
            verticalResolutionNumericUpDown.Value = horizontalResolutionNumericUpDown.Value;
        }

        #endregion

    }
}
