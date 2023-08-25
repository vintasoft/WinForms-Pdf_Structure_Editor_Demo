using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

using Vintasoft.Imaging;
using Vintasoft.Imaging.Pdf.Tree;

namespace DemosCommonCode.Pdf
{
    /// <summary>
    /// A form that allows to specify parameters of page, which should be added to a PDF document.
    /// </summary>
    public partial class AddEmptyPageForm : Form
    {

        #region Fields

        bool _updateValues = false;

        #endregion



        #region Constructor

        public AddEmptyPageForm(SizeF initialPageSizeInUserUnits, UnitOfMeasure initialUnits)
        {
            InitializeComponent();
            _pageSizeInUserUnits = initialPageSizeInUserUnits;
            unitsComboBox.Items.Add(UnitOfMeasure.Inches);
            unitsComboBox.Items.Add(UnitOfMeasure.Centimeters);
            unitsComboBox.Items.Add(UnitOfMeasure.Millimeters);
            unitsComboBox.Items.Add(UnitOfMeasure.Pixels);
            unitsComboBox.Items.Add(UnitOfMeasure.Points);
            unitsComboBox.Items.Add(UnitOfMeasure.DeviceIndependentPixels);
            unitsComboBox.SelectedItem = initialUnits;

            paperKindComboBox.Items.AddRange(GetEnumValues(typeof(PaperSizeKind)));
            paperKindComboBox.Items.Remove(PaperSizeKind.Custom);
            paperKindComboBox.SelectedItem = PaperSizeKind.A4;

            standardSizeRadioButton.Checked = true;
        }

        #endregion



        #region Properties

        SizeF _pageSizeInUserUnits;
        public SizeF PageSizeInUserUnits
        {
            get
            {
                return _pageSizeInUserUnits;
            }
        }

        public UnitOfMeasure Units
        {
            get
            {
                return (UnitOfMeasure)unitsComboBox.SelectedItem;
            }
        }

        PaperSizeKind _paperKind = PaperSizeKind.Custom;
        public PaperSizeKind PaperKind
        {
            get
            {
                return _paperKind;
            }
        }

        public bool Rotated
        {
            get
            {
                return rotatedCheckBox.Checked;
            }
        }

        #endregion



        #region Methods

        private object[] GetEnumValues(Type type)
        {
            Array ar = Enum.GetValues(type);
            object[] result = new object[ar.Length];
            ar.CopyTo(result, 0);
            return result;
        }

        private void UpdateValues()
        {
            _updateValues = true;
            widthTextBox.Text = FloatToString(PdfPage.ConvertFromUserUnitsToUnitOfMeasure(PageSizeInUserUnits.Width, Units));
            heightTextBox.Text = FloatToString(PdfPage.ConvertFromUserUnitsToUnitOfMeasure(PageSizeInUserUnits.Height, Units));
            _updateValues = false;
        }


        static string FloatToString(float value)
        {
            return value.ToString("f2", CultureInfo.InvariantCulture);
        }


        /// <summary>
        /// Handles the Click event of OkButton object.
        /// </summary>
        private void okButton_Click(object sender, EventArgs e)
        {
            if (customSizeRadioButton.Checked)
            {
                _paperKind = PaperSizeKind.Custom;
            }
            else
            {
                _paperKind = (PaperSizeKind)paperKindComboBox.SelectedItem;
            }
            DialogResult = DialogResult.OK;
        }

        /// <summary>
        /// Handles the Click event of CancelButton object.
        /// </summary>
        private void cancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of UnitsComboBox object.
        /// </summary>
        private void unitsComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateValues();
        }

        /// <summary>
        /// Handles the TextChanged event of HeightTextBox object.
        /// </summary>
        private void heightTextBox_TextChanged(object sender, EventArgs e)
        {
            if (_updateValues)
                return;
            try
            {
                _pageSizeInUserUnits.Height = PdfPage.ConvertFromUnitOfMeasureToUserUnits(float.Parse(heightTextBox.Text, CultureInfo.InvariantCulture), Units);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Error");
            }
        }

        /// <summary>
        /// Handles the TextChanged event of WidthTextBox object.
        /// </summary>
        private void widthTextBox_TextChanged(object sender, EventArgs e)
        {
            if (_updateValues)
                return;
            try
            {
                _pageSizeInUserUnits.Width = PdfPage.ConvertFromUnitOfMeasureToUserUnits(float.Parse(widthTextBox.Text, CultureInfo.InvariantCulture), Units);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Error");
            }
        }

        /// <summary>
        /// Handles the CheckedChanged event of CustomSizeRadioButton object.
        /// </summary>
        private void customSizeRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            groupBox1.Enabled = customSizeRadioButton.Checked;
            groupBox2.Enabled = !groupBox1.Enabled;
        }

        /// <summary>
        /// Handles the CheckedChanged event of StandardSizeRadioButton object.
        /// </summary>
        private void standardSizeRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            groupBox2.Enabled = standardSizeRadioButton.Checked;
            groupBox1.Enabled = !groupBox2.Enabled;
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of PaperKindComboBox object.
        /// </summary>
        private void paperKindComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (standardSizeRadioButton.Checked)
            {
                ImageSize size = ImageSize.FromPaperKind((PaperSizeKind)paperKindComboBox.SelectedItem, ImagingEnvironment.ScreenResolution);
                float width = (float)PdfPage.ConvertFromUnitOfMeasureToUserUnits((float)size.WidthInInch, UnitOfMeasure.Inches);
                width = PdfPage.ConvertFromUserUnitsToUnitOfMeasure(width, (UnitOfMeasure)unitsComboBox.SelectedItem);
                float height = (float)PdfPage.ConvertFromUnitOfMeasureToUserUnits((float)size.HeightInInch, UnitOfMeasure.Inches);
                height = PdfPage.ConvertFromUserUnitsToUnitOfMeasure(height, (UnitOfMeasure)unitsComboBox.SelectedItem);
                widthTextBox.Text = width.ToString(CultureInfo.InvariantCulture);
                heightTextBox.Text = height.ToString(CultureInfo.InvariantCulture);
            }
        }

        #endregion

    }
}
