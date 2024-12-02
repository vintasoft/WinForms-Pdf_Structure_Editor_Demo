using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

using Vintasoft.Imaging;
using Vintasoft.Imaging.Pdf;
using Vintasoft.Imaging.Pdf.Tree;


namespace DemosCommonCode.Pdf
{
    /// <summary>
    /// A control that allows to view and edit actions of PDF document.
    /// </summary>
    public partial class PdfActionEditorControl : UserControl
    {

        #region Nested classes

        /// <summary>
        /// List box item.
        /// </summary>
        class ListBoxItem
        {

            /// <summary>
            /// Initializes a new instance of the <see cref="ListBoxItem"/> class.
            /// </summary>
            /// <param name="action">The PDF action.</param>
            public ListBoxItem(PdfAction action)
            {
                Action = action;
            }

            PdfAction _action = null;
            /// <summary>
            /// Gets or sets the PDF action.
            /// </summary>
            public PdfAction Action
            {
                get
                {
                    return _action;
                }
                set
                {
                    _action = value;
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
                return PdfActionsTools.GetActionDescription(Action);
            }

        }

        #endregion



        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PdfActionEditorControl"/> class.
        /// </summary>
        public PdfActionEditorControl()
        {
            InitializeComponent();

            UpdateUI();
        }

        #endregion



        #region Properties

        PdfDocument _document = null;
        /// <summary>
        /// Gets or sets the PDF document.
        /// </summary>
        /// <value>
        /// Default value is <b>null</b>.
        /// </value>
        /// <exception cref="System.InvalidOperationException"></exception>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public PdfDocument Document
        {
            get
            {
                if (_action != null)
                    return _action.Document;

                return _document;
            }
            set
            {
                if (_action != null)
                    throw new InvalidOperationException();

                _document = value;
            }
        }

        PdfAction _action = null;
        /// <summary>
        /// Gets or sets the PDF action.
        /// </summary>
        /// <value>
        /// Default value is <b>null</b>.
        /// </value>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public PdfAction Action
        {
            get
            {
                return _action;
            }
            set
            {
                _action = value;

                UpdateListBox();
            }
        }

        ImageCollection _imageCollection = null;
        /// <summary>
        /// Gets or sets the image collection, which is associated with PDF document.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public ImageCollection ImageCollection
        {
            get
            {
                return _imageCollection;
            }
            set
            {
                _imageCollection = value;
            }
        }

        #endregion



        #region Methods

        #region UI

        /// <summary>
        /// Handles the SelectedIndexChanged event of actionsListBox object.
        /// </summary>
        private void actionsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // update user interface
            UpdateUI();
        }

        /// <summary>
        /// Handles the MouseDoubleClick event of actionsListBox object.
        /// </summary>
        private void actionsListBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            // if item is not selected
            if (actionsListBox.SelectedIndex == -1)
                return;

            // show edit dialog
            EditSelectedAction();
        }

        /// <summary>
        /// Handles the Click event of addButton object.
        /// </summary>
        private void addButton_Click(object sender, EventArgs e)
        {
            // show dialog for creation of action
            PdfAction newAction = CreatePdfActionForm.CreateAction(Document, ParentForm);
            // if new action is created
            if (newAction != null &&
                EditAction(newAction))
            {
                // create and add an list box item for action
                ListBoxItem newItem = new ListBoxItem(newAction);
                actionsListBox.Items.Add(newItem);
                actionsListBox.SelectedItem = newItem;

                if (_action == null)
                    _action = newAction;
                else
                {
                    List<PdfAction> nextActions = new List<PdfAction>();
                    if (_action.NextActions != null)
                        nextActions.AddRange(_action.NextActions);
                    nextActions.Add(newAction);
                    _action.NextActions = nextActions.AsReadOnly();
                }

                OnActionChanged();
            }
        }

        /// <summary>
        /// Handles the Click event of editButton object.
        /// </summary>
        private void editButton_Click(object sender, EventArgs e)
        {
            // edit the selected action
            EditSelectedAction();
        }

        /// <summary>
        /// Handles the Click event of removeButton object.
        /// </summary>
        private void removeButton_Click(object sender, EventArgs e)
        {
            // get selected index
            int selectedIndex = actionsListBox.SelectedIndex;

            // get selected action
            PdfAction selectedAction = ((ListBoxItem)actionsListBox.SelectedItem).Action;
            // remove selected action from actions list box
            actionsListBox.Items.RemoveAt(selectedIndex);
            // if list box is empty
            if (actionsListBox.Items.Count == 0)
                selectedIndex = -1;
            // if removed item is last
            else if (actionsListBox.Items.Count == selectedIndex)
                selectedIndex--;
            // set selected indef in actions list box
            actionsListBox.SelectedIndex = selectedIndex;

            // if remove source action
            if (_action == selectedAction)
            {
                // if next actions is not empty
                if (_action.NextActions != null &&
                    _action.NextActions.Count > 0)
                {
                    // create list
                    List<PdfAction> nextActions = new List<PdfAction>(_action.NextActions);
                    // get first action of next actions
                    PdfAction firstAction = nextActions[0];
                    // remove first action
                    nextActions.Remove(firstAction);
                    // if next actions is not empty
                    if (nextActions.Count > 0)
                        // set next actions of first action
                        firstAction.NextActions = nextActions.AsReadOnly();
                    // set first action
                    _action = firstAction;
                }
                else
                    // remove action
                    _action = null;
            }
            else
            {
                if (_action.NextActions.Count == 1)
                    _action.NextActions = null;
                else
                {
                    // create list
                    List<PdfAction> nextActions = new List<PdfAction>(_action.NextActions);
                    // remove selected action
                    nextActions.Remove(selectedAction);
                    // set next actions
                    _action.NextActions = nextActions.AsReadOnly();
                }
            }

            OnActionChanged();
        }

        /// <summary>
        /// Handles the Click event of moveUpButton object.
        /// </summary>
        private void moveUpButton_Click(object sender, EventArgs e)
        {
            // get selected index
            int selectedIndex = actionsListBox.SelectedIndex;
            // get selected item
            ListBoxItem selectedItem = (ListBoxItem)actionsListBox.SelectedItem;

            // remove selected item
            actionsListBox.Items.Remove(selectedItem);
            // insert selected item
            actionsListBox.Items.Insert(selectedIndex - 1, selectedItem);
            // select item
            actionsListBox.SelectedIndex = selectedIndex - 1;

            // get selected action
            PdfAction selectedAction = selectedItem.Action;
            // create next actions list
            List<PdfAction> nextActions = new List<PdfAction>(_action.NextActions);
            // clear next actions
            _action.NextActions = null;
            // find selected action in next actions
            int selectedActionIndex = nextActions.IndexOf(selectedAction);
            // if move first action
            if (selectedActionIndex == 0)
            {
                // insert current action
                nextActions.Insert(0, _action);
                // remove selected action
                nextActions.Remove(selectedAction);
                // set current action
                _action = selectedAction;
            }
            else
            {
                // remove selected action
                nextActions.Remove(selectedAction);
                // insert selected action
                nextActions.Insert(selectedActionIndex - 1, selectedAction);
            }
            // set next actions
            _action.NextActions = nextActions.AsReadOnly();
            OnActionChanged();
        }

        /// <summary>
        /// Handles the Click event of moveDownButton object.
        /// </summary>
        private void moveDownButton_Click(object sender, EventArgs e)
        {
            // get selected index
            int selectedIndex = actionsListBox.SelectedIndex;
            // get selected item
            ListBoxItem selectedItem = (ListBoxItem)actionsListBox.SelectedItem;

            // remove selected item
            actionsListBox.Items.Remove(selectedItem);
            // insert selected item
            actionsListBox.Items.Insert(selectedIndex + 1, selectedItem);
            // select item
            actionsListBox.SelectedIndex = selectedIndex + 1;

            // get selected action
            PdfAction selectedAction = selectedItem.Action;
            // create next actions list
            List<PdfAction> nextActions = new List<PdfAction>(_action.NextActions);
            // clear next actions
            _action.NextActions = null;
            // if current action is moved
            if (_action == selectedAction)
            {
                // set current action
                _action = nextActions[0];
                // remove current action
                nextActions.Remove(_action);
                // insert selected action
                nextActions.Insert(0, selectedAction);
            }
            else
            {
                // get index of selected action
                int selectedActionIndex = nextActions.IndexOf(selectedAction);
                // remove selected action
                nextActions.Remove(selectedAction);
                // insert selected action
                nextActions.Insert(selectedActionIndex + 1, selectedAction);
            }
            // set next actions
            _action.NextActions = nextActions.AsReadOnly();
            OnActionChanged();
        }

        #endregion


        /// <summary>
        /// Updates the user interface of this control.
        /// </summary>
        private void UpdateUI()
        {
            bool isSelectedAction = actionsListBox.SelectedIndex != -1;
            bool isSelectedActionCanEdit = false;
            if (isSelectedAction)
            {
                PdfActionType actionType = ((ListBoxItem)actionsListBox.SelectedItem).Action.ActionType;
                isSelectedActionCanEdit =
                    actionType == PdfActionType.GoTo ||
                    actionType == PdfActionType.Hide ||
                    actionType == PdfActionType.JavaScript ||
                    actionType == PdfActionType.Launch ||
                    actionType == PdfActionType.Named ||
                    actionType == PdfActionType.ResetForm ||
                    actionType == PdfActionType.SubmitForm ||
                    actionType == PdfActionType.URI;
            }

            editButton.Enabled = isSelectedAction;
            removeButton.Enabled = isSelectedAction;
            moveUpButton.Enabled = isSelectedAction && (actionsListBox.SelectedIndex > 0);
            moveDownButton.Enabled = isSelectedAction &&
                (actionsListBox.SelectedIndex < (actionsListBox.Items.Count - 1));
        }


        /// <summary>
        /// Updates the ActionsListBox.
        /// </summary>
        private void UpdateListBox()
        {
            // clear actions list
            actionsListBox.Items.Clear();

            if (_action != null)
            {
                actionsListBox.BeginUpdate();
                try
                {
                    // add current action
                    actionsListBox.Items.Add(new ListBoxItem(_action));

                    // if current action contains actions
                    if (_action.NextActions != null)
                    {
                        // add actions
                        foreach (PdfAction action in _action.NextActions)
                        {
                            actionsListBox.Items.Add(new ListBoxItem(action));
                        }
                    }

                    if (actionsListBox.Items.Count > 0)
                        actionsListBox.SelectedIndex = 0;
                }
                finally
                {
                    actionsListBox.EndUpdate();
                }
            }
            UpdateUI();
        }


        /// <summary>
        /// Edits the PDF action.
        /// </summary>
        private void EditSelectedAction()
        {
            // get selected action
            PdfAction action = ((ListBoxItem)actionsListBox.SelectedItem).Action;

            // show dialog for annotation editing
            if (EditAction(action))
                OnActionChanged();
        }

        /// <summary>
        /// Edits the PDF action.
        /// </summary>
        /// <param name="action">The PDF action.</param>
        private bool EditAction(PdfAction action)
        {
            if (PdfActionsEditorTool.EditAction(action, _imageCollection, ParentForm))
            {
                if (actionsListBox.SelectedItem != null)
                {
                    // update item text of list box
                    ListBoxItem item = (ListBoxItem)actionsListBox.SelectedItem;
                    actionsListBox.Items[actionsListBox.SelectedIndex] = item;
                }
                return true;
            }
            return false;
        }

        /// <summary>
        /// Raises the <see cref="ActionChanged"/> event.
        /// </summary>
        private void OnActionChanged()
        {
            if (ActionChanged != null)
                ActionChanged(this, EventArgs.Empty);
        }

        #endregion



        #region Events

        /// <summary>
        /// Occurs when PDF action is changed.
        /// </summary>
        public event EventHandler ActionChanged;

        #endregion

    }
}
