using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Vintasoft.Imaging.Pdf.Content.TextExtraction;
using Vintasoft.Imaging.Pdf.Tree.Fonts;

namespace DemosCommonCode.Pdf
{
    /// <summary>
    /// A form that allows to view fonts of PDF document.
    /// </summary>
    public partial class ViewDocumentFontsForm : Form
    {

        #region Fields

        /// <summary>
        /// The PDF document fonts.
        /// </summary>
        IList<PdfFont> _fonts;

        #endregion



        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewDocumentFontsForm"/> class.
        /// </summary>
        /// <param name="fonts">The PDf document fonts.</param>
        public ViewDocumentFontsForm(IList<PdfFont> fonts)
        {
            InitializeComponent();

            _fonts = fonts;
            for (int i = 0; i < _fonts.Count; i++)
                fontComboBox.Items.Add(GetFontInformation(_fonts[i]));
            fontComboBox.SelectedIndex = 0;

            Text += " - " + fonts.Count.ToString();
        }

        #endregion



        #region Methods

        #region PUBLIC

        /// <summary>
        /// Returns the font information.
        /// </summary>
        /// <param name="font">The font.</param>
        public static string GetFontInformation(PdfFont font)
        {
            StringBuilder fontProgramType = new StringBuilder(font.FontType.ToString());
            if (font.StandardFontType != PdfStandardFontType.NotStandard)
            {
                fontProgramType.Append(", Standard");
            }
            else
            {
                if (font.IsFullyDefined)
                    fontProgramType.Append(string.Format(", Embedded - {0}", font.FontProgramType));
                else
                    fontProgramType.Append(", External");
                if (font.IsVertical)
                    fontProgramType.Append(", Vertical");
            }
            return string.Format("[{1}] {0} ({2})", font.FontName, font.ObjectNumber, fontProgramType);
        }

        #endregion


        #region PRIVATE

        /// <summary>
        /// Handles the SelectedIndexChanged event of FontComboBox object.
        /// </summary>
        private void fontComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // update selected font
            pdfFontViewerControl.PdfFont = _fonts[fontComboBox.SelectedIndex];
        }

        /// <summary>
        /// Handles the ValueChanged event of CellSizeNumericUpDown object.
        /// </summary>
        private void cellSizeNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            // get font viewer cell size
            int cellSize = (int)cellSizeNumericUpDown.Value;
            // update font viewer cell size
            pdfFontViewerControl.CellSize = new Size(cellSize, cellSize);
        }

        /// <summary>
        /// Handles the MouseMove event of PdfFontViewerControl1 object.
        /// </summary>
        private void pdfFontViewerControl1_MouseMove(object sender, MouseEventArgs e)
        {
            // get PDF font symbol
            PdfTextSymbol pdfTextSymbol = pdfFontViewerControl.GetTextSymbol(e.Location);

            // if symbol is not found
            if (pdfTextSymbol == null)
            {
                toolStripStatusLabel.Text = "";
            }
            else
            {
                // if the symbol has character sequence
                if (pdfTextSymbol.HasCharacterSequence)
                {
                    toolStripStatusLabel.Text = 
                        string.Format("Symbols: '{0}'; Content code: {1}; Width: {2}", pdfTextSymbol.Symbols, pdfTextSymbol.ContentSymbolCode, pdfTextSymbol.Width);
                }
                else
                {
                    toolStripStatusLabel.Text = 
                        string.Format(
                            "Symbol: '{0}'; Unicode: {1}; Content code: {2}; Width: {3}", 
                            pdfTextSymbol.Symbol, 
                            pdfTextSymbol.SymbolCode, 
                            pdfTextSymbol.ContentSymbolCode, 
                            pdfTextSymbol.Width);
                }
            }
        }

        /// <summary>
        /// Handles the MouseDoubleClick event of PdfFontViewerControl object.
        /// </summary>
        private void pdfFontViewerControl_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            // get PDF font symbol
            PdfTextSymbol pdfTextSymbol = pdfFontViewerControl.GetTextSymbol(e.Location);
            // if PDF font is found
            if (pdfTextSymbol != null)
                // copy selected symbol to clipboard
                Clipboard.SetText(pdfTextSymbol.Symbols);
        } 

        #endregion

        #endregion

    }
}
