using System.Windows.Forms;

using Vintasoft.Imaging;
using Vintasoft.Imaging.Pdf.Tree.Annotations;


namespace DemosCommonCode.Pdf
{
    /// <summary>
    /// A control that allows to show information about annotations of PDF Page.
    /// </summary>
    public partial class PdfPageAnnotationsControl : ListView
    {

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PdfPageAnnotationsControl"/> class.
        /// </summary>
        public PdfPageAnnotationsControl()
        {
            InitializeComponent();

            if (!DesignMode)
            {
                ImageList.ColorDepth = ColorDepth.Depth32Bit;
                ImageList.ImageSize = new System.Drawing.Size(16, 16);

                string[] widgetResourceNames = new string[]{
                    "InteracitveField",
                    "CheckBoxField",
                    "BarcodeField",
                    "ButtonField",
                    "FieldGroup",
                    "ListBoxField",
                    "RadioButtonField",
                    "SignatureField",
                    "TextField",
                    "ComboBoxField",
                    "CheckBoxGroup",
                    "RadioButtonGroup",
                 };
                string widgetResourceNameFormatString = "DemosCommonCode.Pdf.AnnotationTool.FormFields.Resources.{0}.png";
                // load resources
                for (int i = 0; i < widgetResourceNames.Length; i++)
                {
                    ImageList.Images.Add(
                           widgetResourceNames[i],
                           DemosResourcesManager.GetResourceAsBitmap(
                           string.Format(widgetResourceNameFormatString, widgetResourceNames[i])));
                }

                string[] annotationResourceNames = new string[] {
                     "Ellipse",
                     "FreeText",
                     "Highlight",
                     "StrikeOut",
                     "Underline",
                     "Squiggly",
                     "Line",
                     "Link",
                     "Polyline",
                     "Polygon",
                     "Screen",
                     "Rectangle",
                     "Stamp",
                     "Text_Comment",
                     "Ink",
                     "FileAttachment",
                     "Popup",
                     "Caret",
                };

                string annotationResourceNameFormatString = "DemosCommonCode.Pdf.AnnotationTool.Annotations.Resources.{0}.png";
                // load resources
                for (int i = 0; i < annotationResourceNames.Length; i++)
                {
                    ImageList.Images.Add(
                           annotationResourceNames[i],
                           DemosResourcesManager.GetResourceAsBitmap(
                           string.Format(annotationResourceNameFormatString, annotationResourceNames[i])));
                }
            }

            View = View.Details;
            GridLines = true;
            FullRowSelect = true;
            HeaderStyle = ColumnHeaderStyle.Nonclickable;
        }

        #endregion



        #region Properties

        PdfAnnotationList _annotationList = null;
        /// <summary>
        /// Gets or sets the annotation list.
        /// </summary>
        /// <value>
        /// Default value is <b>null</b>.
        /// </value>
        public virtual PdfAnnotationList AnnotationList
        {
            get
            {
                return _annotationList;
            }
            set
            {
                if (_annotationList != value)
                {
                    if (_annotationList != null)
                        _annotationList.Changed -= annotationList_Changed;

                    _annotationList = value;

                    if (_annotationList != null)
                        _annotationList.Changed += new CollectionChangeEventHandler<PdfAnnotation>(annotationList_Changed);

                    Update(_annotationList);
                }
            }
        }

        /// <summary>
        /// Gets or sets the selected annotation.
        /// </summary>
        public virtual PdfAnnotation SelectedAnnotation
        {
            get
            {
                if (base.SelectedItems.Count > 0)
                {
                    ListViewItem item = SelectedItems[0];
                    PdfAnnotation annotation = (PdfAnnotation)item.Tag;
                    return annotation;
                }
                return null;
            }
            set
            {
                BeginUpdate();
                try
                {
                    SelectedItems.Clear();

                    if (value != null)
                    {
                        foreach (ListViewItem item in this.Items)
                        {
                            if (item.Tag == value)
                            {
                                item.Selected = true;
                                item.EnsureVisible();
                                return;
                            }
                        }
                    }
                }
                finally
                {
                    EndUpdate();
                }
            }
        }

        #endregion



        #region Methods

        /// <summary>
        /// Updates the name of the annotation.
        /// </summary>
        /// <param name="annotation">The annotation.</param>
        public void UpdateAnnotation(PdfAnnotation annotation)
        {
            foreach (ListViewItem item in Items)
            {
                if ((PdfAnnotation)item.Tag == annotation)
                {
                    item.SubItems[1].Text = PdfDemosTools.GetAnnotationName(annotation);
                    item.SubItems[2].Text = annotation.Rectangle.ToString();
                    break;
                }
            }
        }

        /// <summary>
        /// Updates the control.
        /// </summary>
        /// <param name="annotations">The annotation list.</param>
        private void Update(PdfAnnotationList annotations)
        {
            // begin update
            BeginUpdate();
            try
            {
                // clear items
                Items.Clear();
                // clear columns
                Columns.Clear();

                // if annotation list contains annotations
                if (annotations != null && annotations.Count > 0)
                {
                    // add columns
                    Columns.Add("Type");
                    Columns.Add("Name");
                    Columns.Add("Rectangle");

                    foreach (PdfAnnotation annotation in annotations)
                    {
                        if (annotation != null)
                            AddAnnotation(annotation);
                    }

                    // update size of columns
                    AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
                }
            }
            finally
            {
                // end update
                EndUpdate();
            }
        }

        /// <summary>
        /// Annotation list is changed.
        /// </summary>
        private void annotationList_Changed(object sender, CollectionChangeEventArgs<PdfAnnotation> e)
        {
            if (InvokeRequired)
                Invoke(new UpdateDelegate(Update), _annotationList);
            else
                Update(_annotationList);
        }

        /// <summary>
        /// Adds the annotation.
        /// </summary>
        /// <param name="annotation">The annotation to add.</param>
        private void AddAnnotation(PdfAnnotation annotation)
        {
            // name of annotation
            string name = PdfDemosTools.GetAnnotationName(annotation);
            // image of annotation
            string imageKey = GetImageKey(annotation);

            // create item of list view
            ListViewItem item = CreateItem(annotation, name, imageKey);

            // add the item to list view
            Items.Add(item);
        }

        /// <summary>
        /// Returns the image key for the specified annotation.
        /// </summary>
        /// <param name="annotation">The annotation.</param>
        /// <returns>The image key for the specified annotation.</returns>
        private string GetImageKey(PdfAnnotation annotation)
        {
            string imageKey = string.Empty;

            switch (annotation.AnnotationType)
            {
                case PdfAnnotationType.Circle:
                    imageKey = "Ellipse";
                    break;

                case PdfAnnotationType.FreeText:
                    imageKey = "FreeText";
                    break;

                case PdfAnnotationType.Highlight:
                    imageKey = "Highlight";
                    break;

                case PdfAnnotationType.StrikeOut:
                    imageKey = "StrikeOut";
                    break;

                case PdfAnnotationType.Underline:
                    imageKey = "Underline";
                    break;

                case PdfAnnotationType.Squiggly:
                    imageKey = "Squiggly";
                    break;

                case PdfAnnotationType.Line:
                    imageKey = "Line";
                    break;

                case PdfAnnotationType.Link:
                    imageKey = "Link";
                    break;

                case PdfAnnotationType.PolyLine:
                    imageKey = "PolyLine";
                    break;

                case PdfAnnotationType.Polygon:
                    imageKey = "Polygon";
                    break;

                case PdfAnnotationType.Screen:
                    imageKey = "Screen";
                    break;

                case PdfAnnotationType.Square:
                    imageKey = "Rectangle";
                    break;

                case PdfAnnotationType.Stamp:
                    imageKey = "Stamp";
                    break;

                case PdfAnnotationType.Text:
                    imageKey = "Text_Comment";
                    break;

                case PdfAnnotationType.Ink:
                    imageKey = "Ink";
                    break;

                case PdfAnnotationType.FileAttachment:
                    imageKey = "FileAttachment";
                    break;

                case PdfAnnotationType.Popup:
                    imageKey = "Popup";
                    break;

                case PdfAnnotationType.Caret:
                    imageKey = "Caret";
                    break;

                case PdfAnnotationType.Widget:
                    PdfWidgetAnnotation widgetAnnotation = (PdfWidgetAnnotation)annotation;
                    // get image key for widget annotation
                    imageKey = PdfInteractiveFormFieldTree.GetImageKey(widgetAnnotation.Field);
                    break;
            }

            return imageKey;
        }

        /// <summary>
        /// Creates an item of a list view.
        /// </summary>
        /// <param name="annotation">The annotation.</param>
        /// <param name="name">The name of the annotation.</param>
        /// <param name="imageKey">The image key for the annotation.</param>
        private ListViewItem CreateItem(PdfAnnotation annotation, string name, string imageKey)
        {
            ListViewItem item = new ListViewItem(annotation.AnnotationType.ToString(), imageKey);
            // save information about annotation in the item of list view
            item.Tag = annotation;
            // add name of annotation
            item.SubItems.Add(name);
            // add rectangle of annotation
            item.SubItems.Add(annotation.Rectangle.ToString());
            return item;
        }

        #endregion



        #region Delegates

        /// <summary>
        /// The delegate for <see cref="Update(PdfAnnotationList)"/> method.
        /// </summary>
        private delegate void UpdateDelegate(PdfAnnotationList annotations);

        #endregion

    }
}
