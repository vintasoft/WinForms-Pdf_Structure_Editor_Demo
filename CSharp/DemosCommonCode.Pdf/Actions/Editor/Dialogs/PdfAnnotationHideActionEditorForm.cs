using System;
using System.Collections.Generic;
using System.Windows.Forms;

using Vintasoft.Imaging.Pdf.Tree;
using Vintasoft.Imaging.Pdf.Tree.Annotations;
using Vintasoft.Imaging.Pdf.Tree.InteractiveForms;


namespace DemosCommonCode.Pdf
{
    /// <summary>
    /// A form that allows to view and edit the PDF hide action.
    /// </summary>
    public partial class PdfAnnotationHideActionEditorForm : Form
    {

        #region Nested classes

        /// <summary>
        /// List box item.
        /// </summary>
        class ListBoxItem
        {

            /// <summary>
            /// The PDF page index.
            /// </summary>
            int _pageIndex;



            /// <summary>
            /// Initializes a new instance of the <see cref="ListBoxItem"/> class.
            /// </summary>
            /// <param name="annotation">The PDF annotation.</param>
            /// <param name="pageIndex">Index of the PDF page.</param>
            public ListBoxItem(PdfAnnotation annotation, int pageIndex)
            {
                _pageIndex = pageIndex;
                Annotation = annotation;
            }


            PdfAnnotation _annotation = null;
            /// <summary>
            /// Gets or sets the PDF annotation.
            /// </summary>
            public PdfAnnotation Annotation
            {
                get
                {
                    return _annotation;
                }
                set
                {
                    _annotation = value;
                }
            }



            /// <summary>
            /// Returns a <see cref="System.String" /> that represents this instance.
            /// </summary>
            /// <returns>
            /// A <see cref="System.String" /> that represents this instance.
            /// </returns>
            public override string ToString()
            {
                string name = "Unknown";

                // if annotation is widget annotation
                if (Annotation is PdfWidgetAnnotation)
                {
                    PdfWidgetAnnotation widgetAnnotation = (PdfWidgetAnnotation)Annotation;
                    // get interactive form field
                    PdfInteractiveFormField field = widgetAnnotation.Field;

                    // get name of annotation
                    name = field.PartialName;
                    // if name is empty
                    if (string.IsNullOrEmpty(name))
                    {
                        // if field is switchable button
                        if (field is PdfInteractiveFormSwitchableButtonField)
                            // get value of button
                            name = ((PdfInteractiveFormSwitchableButtonField)field).ButtonValue;
                    }
                }
                else
                {
                    name = Annotation.Name;
                }
                return string.Format("(Page:{0,3}) {1}", _pageIndex + 1, name);
            }

        }

        #endregion



        #region Fields

        /// <summary>
        /// The PDF annotation hide action.
        /// </summary>
        PdfAnnotationHideAction _action;

        /// <summary>
        /// The PDF pages, which have annotations.
        /// </summary>
        List<PdfPage> _annotatedPages = new List<PdfPage>();

        #endregion



        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PdfAnnotationHideActionEditorForm"/> class.
        /// </summary>
        /// <param name="action">The action.</param>
        public PdfAnnotationHideActionEditorForm(PdfAnnotationHideAction action)
        {
            InitializeComponent();

            _action = action;

            hideCheckBox.Checked = action.Hide;

            PdfPageCollection pages = action.Document.Pages;
            pagesComboBox.BeginUpdate();
            for (int i = 0; i < pages.Count; i++)
            {
                PdfPage page = pages[i];
                if (page.Annotations != null && page.Annotations.Count > 0)
                {
                    _annotatedPages.Add(page);
                    pagesComboBox.Items.Add(string.Format("Page {0}", i + 1));
                }
            }
            if (pagesComboBox.Items.Count > 0)
                pagesComboBox.SelectedIndex = 0;
            pagesComboBox.EndUpdate();

            if (action.Annotations != null)
            {
                foreach (PdfAnnotation annotation in action.Annotations)
                    AddAnnotation(annotation);
            }
        }

        #endregion



        #region Methods

        #region UI

        /// <summary>
        /// Handles the Click event of AddButton object.
        /// </summary>
        private void addButton_Click(object sender, EventArgs e)
        {
            // add annotation
            AddAnnotation();
        }

        /// <summary>
        /// Handles the Click event of RemoveButton object.
        /// </summary>
        private void removeButton_Click(object sender, EventArgs e)
        {
            // remove annotation
            RemoveAnnotation();
        }

        /// <summary>
        /// Handles the Click event of RemoveAllButton object.
        /// </summary>
        private void removeAllButton_Click(object sender, EventArgs e)
        {
            // remove all annotations
            annotationsListBox.Items.Clear();
            // update user interface
            UpdateUI();
        }

        /// <summary>
        /// Handles the Click event of OkButton object.
        /// </summary>
        private void okButton_Click(object sender, EventArgs e)
        {
            // if action must be hidden
            if (hideCheckBox.Checked)
                _action.Hide = true;
            else
                _action.Hide = false;

            // get items
            ListBox.ObjectCollection items = annotationsListBox.Items;
            List<PdfAnnotation> annotations = new List<PdfAnnotation>();

            // for each list box item in items
            foreach (ListBoxItem item in items)
                annotations.Add(item.Annotation);

            // update action annotations
            _action.Annotations = annotations.ToArray();
        }

        /// <summary>
        /// Handles the MouseDoubleClick event of PdfPageAnnotationsControl object.
        /// </summary>
        private void pdfPageAnnotationsControl_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            // if the annotation from PdfPageAnnotationsControl can be added to the annotation list in AnnotationsListBox.
            if (CanAddAnnotationFromPdfPageAnnotationsControlToSelectedAnnotations())
                // add annotation from PdfPageAnnotationsControl to the annotation list in AnnotationsListBox
                AddAnnotation();
        }

        /// <summary>
        /// Handles the MouseDoubleClick event of AnnotationsListBox object.
        /// </summary>
        private void annotationsListBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            // if annotation can be removed
            if (annotationsListBox.SelectedItem != null)
                // remove annotation from annotation list in AnnotationsListBox
                RemoveAnnotation();
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of PagesComboBox object.
        /// </summary>
        private void pagesComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // if PDF page is not selected
            if (pagesComboBox.SelectedIndex == -1)
            {
                pdfPageAnnotationsControl.AnnotationList = null;
            }
            else
            {
                // get PDF page
                PdfPage page = _annotatedPages[pagesComboBox.SelectedIndex];
                // update annotations
                pdfPageAnnotationsControl.AnnotationList = page.Annotations;
            }
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of AnnotationsListBox object.
        /// </summary>
        private void annotationsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // update user interface
            UpdateUI();
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of PdfPageAnnotationsControl object.
        /// </summary>
        private void pdfPageAnnotationsControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            // update user interface
            UpdateUI();
        }

        #endregion


        /// <summary>
        /// Adds annotation from PdfPageAnnotationsControl to the annotation list in AnnotationsListBox.
        /// </summary>
        private void AddAnnotation()
        {
            // get PDF page annotation
            PdfAnnotation pageAnnotation = pdfPageAnnotationsControl.SelectedAnnotation;
            // adds PDF page annotation to a list of selected annotations
            AddAnnotation(pageAnnotation);

            UpdateUI();
        }

        /// <summary>
        /// Removes annotation from annotation list in AnnotationsListBox.
        /// </summary>
        private void RemoveAnnotation()
        {
            int newIndex = annotationsListBox.SelectedIndex;
            if (newIndex == annotationsListBox.Items.Count - 1)
                newIndex--;

            annotationsListBox.Items.Remove(annotationsListBox.SelectedItem);
            annotationsListBox.SelectedIndex = newIndex;

            UpdateUI();
        }

        /// <summary>
        /// Updates the user interface of this control.
        /// </summary>
        private void UpdateUI()
        {
            addButton.Enabled = CanAddAnnotationFromPdfPageAnnotationsControlToSelectedAnnotations();
            removeButton.Enabled = annotationsListBox.SelectedIndex != -1;
            removeAllButton.Enabled = annotationsListBox.Items.Count > 0;
        }

        /// <summary>
        /// Determines that the annotation from PdfPageAnnotationsControl can be added
        /// to the annotation list in AnnotationsListBox.
        /// </summary>
        private bool CanAddAnnotationFromPdfPageAnnotationsControlToSelectedAnnotations()
        {
            // get annotation from PdfPageAnnotationsControl
            PdfAnnotation selectedAnnotation = pdfPageAnnotationsControl.SelectedAnnotation;
            // if annotation exist
            if (selectedAnnotation != null)
            {
                // get list box items
                ListBox.ObjectCollection items = annotationsListBox.Items;
                // for each list box item
                foreach (ListBoxItem item in items)
                {
                    // if annotation, associated with list box item, is equal to the selected annotation
                    if (item.Annotation == selectedAnnotation)
                        return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Adds the annotation to a list box of selected annotations.
        /// </summary>
        /// <param name="annotation">The PDF annotation.</param>
        private void AddAnnotation(PdfAnnotation annotation)
        {
            annotationsListBox.Items.Add(
                new ListBoxItem(annotation, _annotatedPages.IndexOf(annotation.Page)));
        }

        #endregion

    }
}
