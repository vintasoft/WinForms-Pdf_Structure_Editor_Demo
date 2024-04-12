using System;
using System.Collections.Generic;

using System.Windows.Forms;


namespace DemosCommonCode
{
    /// <summary>
    /// Represents an editor form for item set.
    /// </summary>
    public partial class ItemSetEditorForm : Form
    {

        #region Fields

        /// <summary>
        /// Cached item names.
        /// </summary>
        string[] _itemNames;

        /// <summary>
        /// Indicates that selected item is changing.
        /// </summary>
        bool _selectedItemChanging = false;

        #endregion



        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ItemSetEditorForm"/> class.
        /// </summary>
        public ItemSetEditorForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ItemSetEditorForm"/> class.
        /// </summary>
        /// <param name="items">The items.</param>
        public ItemSetEditorForm(ItemSet items)
            : this()
        {
            Items = items;
        }

        #endregion



        #region Properties

        ItemSet _items = null;
        /// <summary>
        /// Gets or sets the items.
        /// </summary>
        /// <value>
        /// Default value is <b>null</b>.
        /// </value>
        public ItemSet Items
        {
            get
            {
                return _items;
            }
            set
            {
                _items = value;
                mainPanel.Enabled = _items != null;
            }
        }

        #endregion



        #region Methods

        #region PROTECTED

        /// <summary>
        /// Raizes the <see cref="E:System.Windows.Forms.Form.Shown" /> event.
        /// </summary>
        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            if (!DesignMode)
                UpdateItemsListBox();
        }

        #endregion


        #region PRIVATE

        #region UI

        /// <summary>
        /// Handles the Click event of deleteButton object.
        /// </summary>
        private void deleteButton_Click(object sender, EventArgs e)
        {
            // get selected item index
            int index = itemListBox.SelectedIndex;
            // if selected item can be removed
            if (index >= 0)
            {
                // remove selected item
                _items.RemoveItem(_itemNames[index]);
                // update user interface
                UpdateItemsListBox();
            }
        }

        /// <summary>
        /// Handles the Click event of addButton object.
        /// </summary>
        private void addButton_Click(object sender, EventArgs e)
        {
            // add new item
            string name = _items.AddNewItem();
            // if item is created
            if (name != null)
            {
                // update user interface
                UpdateItemsListBox();
                // update selected index
                itemListBox.SelectedIndex = Array.IndexOf(_itemNames, name);
            }
        }

        /// <summary>
        /// Handles the TextChanged event of nameTextBox object.
        /// </summary>
        private void nameTextBox_TextChanged(object sender, EventArgs e)
        {
            if (_selectedItemChanging)
                return;

            // get selected item index
            int index = itemListBox.SelectedIndex;
            // if selected item name must be changed
            if (index >= 0 && _itemNames[index] != nameTextBox.Text)
            {
                try
                {
                    // rename item
                    _items.RenameItem(_itemNames[index], nameTextBox.Text);
                    // update item name
                    _itemNames[index] = nameTextBox.Text;
                    _selectedItemChanging = true;
                    // update selected item
                    itemListBox.Items[index] = _itemNames[index];
                }
                catch (Exception ex)
                {
                    DemosTools.ShowErrorMessage(ex);
                }
                _selectedItemChanging = false;
            }
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of itemsListBox object.
        /// </summary>
        private void itemsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // if selected item is changing
            if (_selectedItemChanging)
                return;

            _selectedItemChanging = true;
            // get current selected index
            int index = itemListBox.SelectedIndex;
            if (index >= 0)
            {
                // update selected object
                itemPropertyGrid.SelectedObject = _items.GetItem(_itemNames[index]);
                if (nameTextBox.Text != _itemNames[index])
                    nameTextBox.Text = _itemNames[index];
            }
            else
            {
                // remove selected object
                itemPropertyGrid.SelectedObject = null;
                nameTextBox.Text = "";
            }
            _selectedItemChanging = false;
        }

        /// <summary>
        /// Handles the Click event of okButton object.
        /// </summary>
        private void okButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            // close dialog
            Close();
        }

        #endregion


        /// <summary>
        /// Updates the items list box.
        /// </summary>
        private void UpdateItemsListBox()
        {
            // if items value is not specified
            if (_items == null)
                return;

            // get item names
            ICollection<string> itemNames = _items.GetItemNames();
            _itemNames = new string[itemNames.Count];
            itemNames.CopyTo(_itemNames, 0);
            // get selected index
            int selectedIndex = itemListBox.SelectedIndex;


            // update list box

            itemListBox.BeginUpdate();
            itemListBox.Items.Clear();
            itemListBox.Items.AddRange(_itemNames);
            if (selectedIndex >= 0)
                // update selected item index
                itemListBox.SelectedIndex = Math.Min(_itemNames.Length - 1, selectedIndex);
            itemListBox.EndUpdate();
        }

        #endregion

        #endregion

    }
}
