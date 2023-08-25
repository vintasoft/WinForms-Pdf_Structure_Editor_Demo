﻿using System.Collections.Generic;

namespace DemosCommonCode
{
    /// <summary>
    /// Provides the <b>abstract</b> base class that can work with data set.
    /// </summary>
    public abstract class ItemSet
    {

        /// <summary>
        /// Gets the collection that contains names of the items.
        /// </summary>
        /// <returns>Collection that contains item names.</returns>
        public abstract ICollection<string> GetItemNames();

        /// <summary>
        /// Gets the item by name.
        /// </summary>
        /// <param name="name">The item name.</param>
        /// <returns>The item.</returns>
        public abstract object GetItem(string name);

        /// <summary>
        /// Renames the item.
        /// </summary>
        /// <param name="oldName">The old name of item.</param>
        /// <param name="newName">The new name of item.</param>
        public abstract void RenameItem(string oldName, string newName);

        /// <summary>
        /// Adds the new item.
        /// </summary>
        /// <returns>The item name.</returns>
        public abstract string AddNewItem();

        /// <summary>
        /// Removes the item.
        /// </summary>
        /// <param name="name">The item name.</param>
        public abstract void RemoveItem(string name);

    }
}
