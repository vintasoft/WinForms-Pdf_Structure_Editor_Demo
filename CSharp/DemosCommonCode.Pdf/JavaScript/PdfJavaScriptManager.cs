namespace DemosCommonCode.Pdf.JavaScript
{
    /// <summary>
    /// PDF JavaScript engine manager.
    /// </summary>
    public static class PdfJavaScriptManager
    {
     
        #region Constructors

        /// <summary>
        /// Initializes the <see cref="PdfJavaScriptManager"/> class.
        /// </summary>
        static PdfJavaScriptManager()
        {
            _jsApp = new PdfViewerJsApp();
            _javaScriptActionExecutor = new WinFormsPdfJavaScriptActionExecutor(_jsApp);
            _jsApp.ActionExecutor = _javaScriptActionExecutor;
            _javaScriptActionExecutor.IsEnabled = IsJavaScriptEnabled;
        }

        #endregion



        #region Properties

        /// <summary>
        /// Gets or sets a value indicating whether JavaScript is enabled.
        /// </summary>
        public static bool IsJavaScriptEnabled
        {
            get
            {
                return _javaScriptActionExecutor.IsEnabled;
            }
            set
            {
                _javaScriptActionExecutor.IsEnabled = value;
            }
        }

        static WinFormsPdfJavaScriptActionExecutor _javaScriptActionExecutor;
        /// <summary>
        /// Gets the JavaScript action executor.
        /// </summary>
        public static WinFormsPdfJavaScriptActionExecutor JavaScriptActionExecutor
        {
            get
            {
                return _javaScriptActionExecutor;
            }
        }

        static PdfViewerJsApp _jsApp;
        /// <summary>
        /// Gets the global JavaScript API "app" object.
        /// </summary>
        public static PdfViewerJsApp JsApp
        {
            get
            {
                return _jsApp;
            }
        }

        #endregion

    }
}

