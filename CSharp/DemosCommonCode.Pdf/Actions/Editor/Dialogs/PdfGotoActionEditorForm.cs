using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

using Vintasoft.Imaging;
using Vintasoft.Imaging.Pdf;
using Vintasoft.Imaging.Pdf.Tree;


namespace DemosCommonCode.Pdf
{
    /// <summary>
    /// A form that allows to view and edit PDF goto action.
    /// </summary>
    public partial class PdfGotoActionEditorForm : Form
    {

        #region Fields

        /// <summary>
        /// The PDF goto action.
        /// </summary>
        PdfGotoAction _action;

        /// <summary>
        /// The image collection, which is associated with PDF document.
        /// </summary>
        ImageCollection _imageCollection = null;

        #endregion



        #region Constructors

        #region PUBLIC

        /// <summary>
        /// Initializes a new instance of the <see cref="PdfGotoActionEditorForm"/> class.
        /// </summary>
        /// <param name="action">The PDF goto action.</param>
        public PdfGotoActionEditorForm(PdfGotoAction action)
            : this(action, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PdfGotoActionEditorForm"/> class.
        /// </summary>
        /// <param name="action">The PDF goto action.</param>
        /// <param name="imageCollection">The image collection,
        /// which is associated with PDF document.</param>
        public PdfGotoActionEditorForm(PdfGotoAction action, ImageCollection imageCollection)
            : this()
        {
            _imageCollection = imageCollection;
            _action = action;

            PdfDocument document = action.Document;

            // add destination types
            positionComboBox.Items.Add(PdfDestinationType.XYZoom);
            positionComboBox.Items.Add(PdfDestinationType.Fit);
            positionComboBox.Items.Add(PdfDestinationType.FitHorizontal);
            positionComboBox.Items.Add(PdfDestinationType.FitVertical);
            positionComboBox.Items.Add(PdfDestinationType.FitRectangle);
            positionComboBox.SelectedItem = PdfDestinationType.Fit;


            // update count of PDF pages

            pageNumberNumericUpDown.Minimum = 1;
            // if image collection oes NOT exist
            if (_imageCollection == null)
                // get page count from PDF document
                pageNumberNumericUpDown.Maximum = document.Pages.Count;
            else
                // get page count from image collection
                pageNumberNumericUpDown.Maximum = _imageCollection.Count;

            // if action has destination
            if (_action.Destination != null)
            {
                // get the destination type
                PdfDestinationType type = _action.Destination.DestinationType;
                positionComboBox.SelectedItem = type;
                // get index of PDF page associated with goto action
                int pdfPageIndex = document.Pages.IndexOf(_action.Destination.Page);
                // if image collection does NOT exist
                if (_imageCollection == null)
                    pageNumberNumericUpDown.Value = pdfPageIndex + 1;
                else
                {
                    // find image, which is associated with PDF page

                    for (int i = 0; i < _imageCollection.Count; i++)
                    {
                        VintasoftImage image = _imageCollection[i];
                        if (image.SourceInfo.PageIndex == pdfPageIndex)
                        {
                            pageNumberNumericUpDown.Value = i + 1;
                            break;
                        }
                    }
                }

                switch (type)
                {
                    case PdfDestinationType.XYZoom:
                        PdfDestinationXYZ destinationXyz = (PdfDestinationXYZ)_action.Destination;
                        // get destination location
                        PointF location = destinationXyz.Location;
                        // update values of text boxes
                        if (location.X == float.MinValue)
                        {
                            destinationXTextBox.Text = "0";
                            destinationXCheckBox.Checked = false;
                        }
                        else
                        {
                            destinationXTextBox.Text = location.X.ToString(CultureInfo.InvariantCulture);
                            destinationXCheckBox.Checked = true;
                        }
                        if (location.Y == float.MinValue)
                        {
                            destinationYTextBox.Text = "0";
                            destinationYCheckBox.Checked = false;
                        }
                        else
                        {
                            destinationYTextBox.Text = location.Y.ToString(CultureInfo.InvariantCulture);
                            destinationYCheckBox.Checked = true;
                        }
                        // update value of numeric up down
                        if (destinationXyz.Zoom <= 0)
                        {
                            destinationZoomNumericUpDown.Value = 100;
                            destinationZoomCheckBox.Checked = false;
                        }
                        else
                        {
                            destinationZoomNumericUpDown.Value = (decimal)Math.Round(destinationXyz.Zoom * 100.0);
                            destinationZoomCheckBox.Checked = true;
                        }
                        break;

                    case PdfDestinationType.FitRectangle:
                        PdfDestinationFitRectangle destinationRectangle = (PdfDestinationFitRectangle)_action.Destination;
                        float rectX = destinationRectangle.Left;
                        float rectY = destinationRectangle.Bottom;
                        float rectWidth = destinationRectangle.Right - destinationRectangle.Left;
                        float rectHeight = destinationRectangle.Top - destinationRectangle.Bottom;
                        // update values of text boxes
                        destinationFitRectangleXTextBox.Text = rectX.ToString(CultureInfo.InvariantCulture);
                        destinationFitRectangleYTextBox.Text = rectY.ToString(CultureInfo.InvariantCulture);
                        destinationFitRectangleWidthTextBox.Text = rectWidth.ToString(CultureInfo.InvariantCulture);
                        destinationFitRectangleHeightTextBox.Text = rectHeight.ToString(CultureInfo.InvariantCulture);
                        break;
                }
            }
        }

        #endregion


        #region PRIVATE

        /// <summary>
        /// Prevents a default instance of the <see cref="PdfGotoActionEditorForm"/> class
        /// from being created.
        /// </summary>
        private PdfGotoActionEditorForm()
        {
            InitializeComponent();
        }

        #endregion

        #endregion



        #region Methods

        /// <summary>
        /// Handles the Click event of OkButton object.
        /// </summary>
        private void okButton_Click(object sender, EventArgs e)
        {
            // get PDF document
            PdfDocument document = _action.Document;
            // get PDF page
            PdfPage page = GetSelectedPage();

            // create destination of action

            switch ((PdfDestinationType)positionComboBox.SelectedItem)
            {
                case PdfDestinationType.Fit:
                    _action.Destination = new PdfDestinationFit(document, page);
                    break;

                case PdfDestinationType.FitHorizontal:
                    _action.Destination = new PdfDestinationFitHorizontal(document, page);
                    break;

                case PdfDestinationType.FitVertical:
                    _action.Destination = new PdfDestinationFitVertical(document, page);
                    break;

                case PdfDestinationType.XYZoom:
                    float x;
                    if (destinationXCheckBox.Checked)
                    {
                        if (!float.TryParse(destinationXTextBox.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out x))
                        {
                            DemosTools.ShowErrorMessage("X coordinate is not a valid float number.");
                            destinationXTextBox.SelectAll();
                            destinationXTextBox.Focus();
                            return;
                        }
                    }
                    else
                    {
                        x = float.MinValue;
                    }
                    float y;
                    if (destinationYCheckBox.Checked)
                    {
                        if (!float.TryParse(destinationYTextBox.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out y))
                        {
                            DemosTools.ShowErrorMessage("Y coordinate is not a valid float number.");
                            destinationYTextBox.SelectAll();
                            destinationYTextBox.Focus();
                            return;
                        }
                    }
                    else
                    {
                        y = float.MinValue;
                    }

                    float zoom;
                    if (destinationZoomCheckBox.Checked)
                    {
                        zoom = (float)destinationZoomNumericUpDown.Value / 100f;
                    }
                    else
                    {
                        zoom = float.MinValue;
                    }

                    PointF location = new PointF(x, y);
                    _action.Destination = new PdfDestinationXYZ(document, page, location, zoom);
                    break;

                case PdfDestinationType.FitRectangle:
                    float left;
                    if (!float.TryParse(destinationFitRectangleXTextBox.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out left))
                    {
                        DemosTools.ShowErrorMessage("X coordinate is not a valid float number.");
                        destinationFitRectangleXTextBox.SelectAll();
                        destinationFitRectangleXTextBox.Focus();
                        return;
                    }

                    float bottom;
                    if (!float.TryParse(destinationFitRectangleYTextBox.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out bottom))
                    {
                        DemosTools.ShowErrorMessage("Y coordinate is not a valid float number.");
                        destinationFitRectangleYTextBox.SelectAll();
                        destinationFitRectangleYTextBox.Focus();
                        return;
                    }

                    float width;
                    if (!float.TryParse(destinationFitRectangleWidthTextBox.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out width))
                    {
                        DemosTools.ShowErrorMessage("Width is not a valid float number.");
                        destinationFitRectangleWidthTextBox.SelectAll();
                        destinationFitRectangleWidthTextBox.Focus();
                        return;
                    }

                    float height;
                    if (!float.TryParse(destinationFitRectangleHeightTextBox.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out height))
                    {
                        DemosTools.ShowErrorMessage("Height is not a valid float number.");
                        destinationFitRectangleHeightTextBox.SelectAll();
                        destinationFitRectangleHeightTextBox.Focus();
                        return;
                    }

                    if (width <= 0)
                    {
                        DemosTools.ShowErrorMessage("Width must be positive.");
                        destinationFitRectangleWidthTextBox.SelectAll();
                        destinationFitRectangleWidthTextBox.Focus();
                        return;
                    }

                    if (height <= 0)
                    {
                        DemosTools.ShowErrorMessage("Height must be positive.");
                        destinationFitRectangleHeightTextBox.SelectAll();
                        destinationFitRectangleHeightTextBox.Focus();
                        return;
                    }

                    PdfDestinationFitRectangle destination = new PdfDestinationFitRectangle(document, page);
                    destination.Left = left;
                    destination.Bottom = bottom;
                    destination.Right = left + width;
                    destination.Top = bottom + height;
                    _action.Destination = destination;
                    break;
            }

            DialogResult = DialogResult.OK;
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of PositionComboBox object.
        /// </summary>
        private void positionComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // get destination type
            PdfDestinationType type = (PdfDestinationType)positionComboBox.SelectedItem;

            // hide panels
            destinationXyzPanel.Visible = false;
            destinationFitRectanglePanel.Visible = false;

            // if type is XYZoom
            if (type == PdfDestinationType.XYZoom)
                // show XYZoom panel
                destinationXyzPanel.Visible = true;
            // if type is FitRectangle
            else if (type == PdfDestinationType.FitRectangle)
                // show FitRectangle panel
                destinationFitRectanglePanel.Visible = true;
        }

        /// <summary>
        /// Handles the CheckedChanged event of DestinationXCheckBox object.
        /// </summary>
        private void destinationXCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            // if horizontal destination is enabled
            if (destinationXCheckBox.Checked)
                destinationXTextBox.Enabled = true;
            else
                destinationXTextBox.Enabled = false;
        }

        /// <summary>
        /// Handles the CheckedChanged event of DestinationYCheckBox object.
        /// </summary>
        private void destinationYCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            // if vertical destination is enabled
            if (destinationYCheckBox.Checked)
                destinationYTextBox.Enabled = true;
            else
                destinationYTextBox.Enabled = false;
        }

        /// <summary>
        /// Handles the CheckedChanged event of DestinationZoomCheckBox object.
        /// </summary>
        private void destinationZoomCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            // if destination zoom is enabled
            if (destinationZoomCheckBox.Checked)
                destinationZoomNumericUpDown.Enabled = true;
            else
                destinationZoomCheckBox.Checked = false;
        }


        /// <summary>
        /// Returns the selected PDF page.
        /// </summary>
        /// <returns>The selected PDF page.</returns>
        private PdfPage GetSelectedPage()
        {
            // get document
            PdfDocument document = _action.Document;
            // get index of selected page
            int imageIndex = (int)pageNumberNumericUpDown.Value - 1;
            PdfPage page = null;
            // if image collection exists
            if (_imageCollection != null)
            {
                // get image
                VintasoftImage image = _imageCollection[imageIndex];
                // get PDF page associated with image
                page = document.Pages[image.SourceInfo.PageIndex];
            }
            else
                // get page
                page = document.Pages[imageIndex];
            return page;
        }

        #endregion

    }
}
