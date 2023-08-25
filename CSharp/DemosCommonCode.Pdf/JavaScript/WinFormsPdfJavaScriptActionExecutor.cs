using System;
using System.Windows.Forms;

using Vintasoft.Imaging.Pdf.JavaScriptApi;
using Vintasoft.Imaging.Pdf.JavaScript;

namespace DemosCommonCode.Pdf.JavaScript
{
    /// <summary>
    /// Executor of JavaScript actions that interprets JavaScript code.
    /// </summary>
    public class WinFormsPdfJavaScriptActionExecutor : PdfJavaScriptActionExecutor
    {

        #region Fields

        /// <summary>
        /// The JavaScript "console" object.
        /// </summary>
        TextBoxPdfJsConsole _textBoxPdfJsConsole;

        #endregion



        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PdfJavaScriptActionExecutor"/> class.
        /// </summary>
        /// <param name="jsApp">The JavaScript "app" object.</param>
        public WinFormsPdfJavaScriptActionExecutor(PdfJsApp jsApp)
            : this(jsApp, new TextBoxPdfJsConsole())
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PdfJavaScriptActionExecutor"/> class.
        /// </summary>
        /// <param name="jsApp">The JavaScript "app" object.</param>
        /// <param name="jsConsole">The JavaScript "console" object.</param>
        private WinFormsPdfJavaScriptActionExecutor(PdfJsApp jsApp, TextBoxPdfJsConsole jsConsole)
            : base(jsApp, jsConsole)
        {
            _textBoxPdfJsConsole = jsConsole;
        }

        #endregion



        #region Properties
     
        TextBox _logTextBox;
        /// <summary>
        /// Gets or sets the text box for log messages output.
        /// </summary>
        public TextBox LogTextBox
        {
            get
            {
                return _logTextBox;
            }
            set
            {
                _logTextBox = value;
            }
        }

        /// <summary>
        /// Gets or sets the text box for console output.
        /// </summary>
        /// <value>
        /// The console text box.
        /// </value>
        public TextBox ConsoleTextBox
        {
            get
            {
                return _textBoxPdfJsConsole.TextBox;
            }
            set
            {
                _textBoxPdfJsConsole.TextBox = value;
            }
        }

        #endregion



        #region Methods
     
        /// <summary>
        /// Adds message to a log output.
        /// </summary>
        /// <param name="text">The text.</param>
        protected override void PrintlnLog(string text)
        {
            if (_logTextBox != null)
                _logTextBox.AppendText(text + Environment.NewLine);
        }

        #endregion

    }
}
