#region License

// -----------------------------------------------------------------------------------------------------------
// 
// Name: VisualListViewSubItemCollection.cs
// VisualPlus - The VisualPlus Framework (VPF) for WinForms .NET development.
// 
// Created: 10/12/2018 - 11:45 PM
// Last Modified: 02/01/2019 - 1:22 AM
// 
// Copyright (c) 2016-2019 VisualPlus <https://darkbyte7.github.io/VisualPlus/>
// All Rights Reserved.
// 
// -----------------------------------------------------------------------------------------------------------
// 
// GNU General Public License v3.0 (GPL-3.0)
// 
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, EITHER
// EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED WARRANTIES OF
// MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.
//  
// This file is subject to the terms and conditions defined in the file 
// 'LICENSE.md', which should be in the root directory of the source code package.
// 
// -----------------------------------------------------------------------------------------------------------

#endregion

#region Namespace

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;

using VisualPlus.Delegates;
using VisualPlus.Enumerators;
using VisualPlus.Events;
using VisualPlus.Extensibility;
using VisualPlus.Localization;
using VisualPlus.Managers;
using VisualPlus.Toolkit.Child;
using VisualPlus.Toolkit.Controls.DataManagement;

#endregion

namespace VisualPlus.Collections.CollectionsBase
{
    public class VisualListViewSubItemCollection : CollectionBase, ICloneable, IList
    {
        #region Fields

        private int _lastAccessedIndex;
        private VisualListView _listView;
        private VisualListViewItem _owner;

        #endregion

        #region Constructors and Destructors

        /// <summary>Initializes a new instance of the <see cref="VisualListViewSubItemCollection" /> class.</summary>
        public VisualListViewSubItemCollection()
        {
            _lastAccessedIndex = -1;
            _listView = null;
            _owner = null;
        }

        /// <summary>Initializes a new instance of the <see cref="VisualListViewSubItemCollection" /> class.</summary>
        /// <param name="owner">The <see cref="VisualListViewItem" /> that owns the collection.</param>
        public VisualListViewSubItemCollection(VisualListViewItem owner) : this()
        {
            _owner = owner;
        }

        /// <summary>Initializes a new instance of the <see cref="VisualListViewSubItemCollection" /> class.</summary>
        /// <param name="listView">The <see cref="VisualListView" /> that owns the collection.</param>
        public VisualListViewSubItemCollection(VisualListView listView) : this()
        {
            _listView = listView;

            // Iterate through each item and send them to the parent.
            foreach (VisualListViewSubItem _subItem in List)
            {
                _subItem.ListView = _listView;
            }
        }

        #endregion

        #region Public Events

        public event ListViewChangedEventHandler ChangedEvent;

        #endregion

        #region Public Properties

        /// <summary>Gets the index of the currently checked items in the control.</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public List<int> CheckedIndicies
        {
            get
            {
                var _checkedItems = new List<int>();
                for (var i = 0; i < Count; i++)
                {
                    VisualListViewSubItem _item = this[i];
                    if (_item.Checked)
                    {
                        _checkedItems.Add(i);
                    }
                }

                return _checkedItems;
            }
        }

        /// <summary>Gets the currently checked items in the control.</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public List<VisualListViewSubItem> CheckedItems
        {
            get
            {
                var _checkedItems = new List<VisualListViewSubItem>();
                for (var i = 0; i < Count; i++)
                {
                    VisualListViewSubItem _item = this[i];
                    if (_item.Checked)
                    {
                        _checkedItems.Add(_item);
                    }
                }

                return _checkedItems;
            }
        }

        [Browsable(false)]
        [Description(PropertyDescription.Parent)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public VisualListView ListView
        {
            get
            {
                return _listView;
            }

            set
            {
                _listView = value;

                // Iterate through each item and send them to the parent.
                foreach (VisualListViewSubItem _subItem in List)
                {
                    _subItem.ListView = _listView;
                }
            }
        }

        /// <summary>Gets the currently selected indicies in the control.</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public List<int> SelectedIndicies
        {
            get
            {
                var _selectedIndicies = new List<int>();
                for (var i = 0; i < Count; i++)
                {
                    VisualListViewSubItem _item = this[i];
                    if (_item.Selected)
                    {
                        _selectedIndicies.Add(i);
                    }
                }

                return _selectedIndicies;
            }
        }

        /// <summary>Gets the currently selected items in the control.</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public List<VisualListViewSubItem> SelectedItems
        {
            get
            {
                var _selectedItems = new List<VisualListViewSubItem>();
                for (var i = 0; i < Count; i++)
                {
                    VisualListViewSubItem _item = this[i];
                    if (_item.Selected)
                    {
                        _selectedItems.Add(_item);
                    }
                }

                return _selectedItems;
            }
        }

        #endregion

        #region Public Indexers

        /// <summary>Gets or sets the subitem at the specified index within the collection.</summary>
        /// <param name="index">The index of the item in the collection to retrieve.</param>
        /// <returns>
        ///     A <see cref="VisualListViewSubItem" /> representing the subitem located at the specified index within the
        ///     collection.
        /// </returns>
        public VisualListViewSubItem this[int index]
        {
            get
            {
                int bailOut = 0;

                while (List.Count <= index)
                {
                    VisualListViewSubItem newItem = new VisualListViewSubItem();
                    newItem.ChangedEvent += SubItem_Changed;
                    newItem.ListView = _listView;

                    // If the index doesn't yet exist, fill in the subitems till it does.
                    List.Add(newItem);

                    if (bailOut++ > 25)
                    {
                        break;
                    }
                }

                return (VisualListViewSubItem)List[index];
            }
        }

        /// <summary>Gets an item with the specified key from the collection.</summary>
        /// <param name="key">The name of the <see cref="VisualListViewSubItem" /> to retrieve.</param>
        /// <returns>The <see cref="VisualListViewSubItem" /> with the specified key.</returns>
        public virtual VisualListViewSubItem this[string key]
        {
            get
            {
                if (string.IsNullOrEmpty(key))
                {
                    return null;
                }

                int _index = IndexOfKey(key);

                if (List.IsValidIndex(_index))
                {
                    return this[_index];
                }
                else
                {
                    return null;
                }
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>Adds a subitem to the collection with the specified text.</summary>
        /// <param name="text">The text to display.</param>
        /// <returns>The <see cref="VisualListViewSubItem" /> that was added to the collection.</returns>
        public VisualListViewSubItem Add(string text)
        {
            VisualListViewSubItem _item = new VisualListViewSubItem(_owner, text);
            Add(_item);
            return _item;
        }

        /// <summary>Adds an existing <see cref="VisualListViewSubItem" /> to the collection.</summary>
        /// <param name="item">The <see cref="VisualListViewSubItem" /> to add to the collection.</param>
        /// <returns>The <see cref="VisualListViewSubItem" /> that was added to the collection.</returns>
        public VisualListViewSubItem Add(VisualListViewSubItem item)
        {
            Insert(-1, item);
            return item;
        }

        /// <summary>
        ///     Adds a subitem to the collection with the specified text, foreground color, background color, and font
        ///     settings.
        /// </summary>
        /// <param name="text">The text to display for the subitem.</param>
        /// <param name="foreColor">A <see cref="Color" /> that represents the foreground color of the subitem.</param>
        /// <param name="backColor">A <see cref="Color" /> that represents the background color of the subitem.</param>
        /// <param name="font">A <see cref="Font" /> that represents the typeface to display the subitem's text in.</param>
        /// <returns>The <see cref="VisualListViewSubItem" /> that was added to the collection.</returns>
        public VisualListViewSubItem Add(string text, Color foreColor, Color backColor, Font font)
        {
            VisualListViewSubItem _item = new VisualListViewSubItem(_owner, text, foreColor, backColor, font);
            Add(_item);
            return _item;
        }

        /// <summary>Adds an array of <see cref="VisualListViewSubItem" /> objects to the collection.</summary>
        /// <param name="items">An array of <see cref="VisualListViewSubItem" /> objects to add to the collection.</param>
        public void AddRange(VisualListViewSubItem[] items)
        {
            if (items == null)
            {
                throw new ArgumentNullException("items");
            }

            foreach (VisualListViewSubItem _item in items)
            {
                Add(_item);
            }
        }

        /// <summary>
        ///     Creates new subitems based on an array and adds them to the collection with specified foreground color,
        ///     background color, and font.
        /// </summary>
        /// <param name="items">An array of string representing the text of each subitem to add to the collection.</param>
        /// <param name="foreColor">A <see cref="Color" /> that represents the foreground color of the subitem.</param>
        /// <param name="backColor">A <see cref="Color" /> that represents the background color of the subitem.</param>
        /// <param name="font">A <see cref="Font" /> that represents the typeface to display the subitem's text in.</param>
        public void AddRange(string[] items, Color foreColor, Color backColor, Font font)
        {
            if (items == null)
            {
                throw new ArgumentNullException("items");
            }

            VisualListViewSubItem[] _items = { };

            for (var i = 0; i < items.Length; i++)
            {
                _items[i].Text = items[i];
                _items[i].ForeColor = foreColor;
                _items[i].BackColor = backColor;
                _items[i].Font = font;
            }

            AddRange(_items);
        }

        /// <summary>Creates new subitems based on an array and adds them to the collection.</summary>
        /// <param name="items">An array of string representing the text of each subitem to add to the collection.</param>
        public void AddRange(string[] items)
        {
            if (items == null)
            {
                throw new ArgumentNullException("items");
            }

            VisualListViewSubItem[] _items = { };

            for (var i = 0; i < items.Length; i++)
            {
                _items[i].Text = items[i];
            }

            AddRange(_items);
        }

        /// <summary>Clears all selection bits in the item structure.</summary>
        /// <param name="itemIgnore">This overload is an optimization to stop a redraw on a re-selection.</param>
        public void ClearSelection(VisualListViewSubItem itemIgnore)
        {
            for (var index = 0; index < List.Count; index++)
            {
                VisualListViewSubItem _item = this[index];
                if (_item != itemIgnore)
                {
                    _item.Selected = false;
                }
            }
        }

        /// <summary>Clears all selection bits in the item structure.</summary>
        public void ClearSelection()
        {
            for (var index = 0; index < List.Count; index++)
            {
                this[index].Selected = false;
            }
        }

        /// <summary>Creates a shallow copy of the current object.</summary>
        /// <returns>The <see cref="object" />.</returns>
        public object Clone()
        {
            return MemberwiseClone();
        }

        /// <summary>Determines whether the specified item is located in the collection.</summary>
        /// <param name="value">A <see cref="VisualListViewSubItem" /> representing the item to locate in the collection.</param>
        /// <returns>The <see cref="bool" />.</returns>
        public bool Contains(VisualListViewSubItem value)
        {
            if (value == null)
            {
                return false;
            }

            return List.Contains(value);
        }

        /// <summary>Determines whether the collection contains an item with the specified key.</summary>
        /// <param name="key">The name of the item to search for.</param>
        /// <returns>The <see cref="bool" />.</returns>
        public bool Contains(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                return false;
            }

            return List.Cast<VisualListViewSubItem>().Any(_item => _item.Name == key);
        }

        /// <summary>Return the index within the collection of the specified item.</summary>
        /// <param name="subItem">A <see cref="VisualListViewSubItem" /> representing the item to locate in the collection.</param>
        /// <returns>
        ///     The zero-based index of the subitem's location in the collection. if the subitems is not located in the
        ///     collection, the return value is negative one(-1).
        /// </returns>
        public int IndexOf(VisualListViewSubItem subItem)
        {
            for (var index = 0; index < Count; index++)
            {
                if (List[index] == subItem)
                {
                    return index;
                }
            }

            return -1;
        }

        /// <summary>Retrieves the index of the item with the specified key.</summary>
        /// <param name="key">The name of the item to find in the collection.</param>
        /// <returns>The <see cref="int" />.</returns>
        public virtual int IndexOfKey(string key)
        {
            // Step 0 - Argument validation.
            if (string.IsNullOrEmpty(key))
            {
                return -1;
            }

            // Step 1 - Check the last cached item.
            if (List.IsValidIndex(_lastAccessedIndex))
            {
                if (TextManager.SafeCompareStrings(this[_lastAccessedIndex].Name, key, true))
                {
                    return _lastAccessedIndex;
                }
            }

            // Step 2 - Search for the item.
            for (var i = 0; i < List.Count; i++)
            {
                if (TextManager.SafeCompareStrings(this[i].Name, key, true))
                {
                    _lastAccessedIndex = i;
                    return i;
                }
            }

            _lastAccessedIndex = -1;
            return -1;
        }

        /// <summary>Creates a new item and inserts it into the collection at the specified index.</summary>
        /// <param name="index">The zero-based index location where the item is inserted.</param>
        /// <param name="text">The text to display for the item.</param>
        /// <returns>The <see cref="VisualListViewSubItem" />.</returns>
        public VisualListViewSubItem Insert(int index, string text)
        {
            VisualListViewSubItem _item = new VisualListViewSubItem { Text = text };

            return Insert(index, _item);
        }

        /// <summary>Inserts a subitem into the collection at the specified index.</summary>
        /// <param name="index">The zero-based index location where the item is inserted.</param>
        /// <param name="item">A <see cref="VisualListViewSubItem" /> representing the subitem to insert into the collection.</param>
        /// <returns>The <see cref="VisualListViewSubItem" />.</returns>
        public VisualListViewSubItem Insert(int index, VisualListViewSubItem item)
        {
            item.ListView = _listView;
            item.ChangedEvent += SubItem_Changed;

            if (index < 0)
            {
                List.Add(item);
            }
            else
            {
                List.Insert(index, item);
            }

            ChangedEvent?.Invoke(this, new ListViewChangedEventArgs(ListViewChangedTypes.SubItemCollectionChanged, null, null, item));
            return item;
        }

        /// <summary>Removes the specified item from the collection.</summary>
        /// <param name="item">The <see cref="VisualListViewSubItem" /> representing the item to remove from the collection.</param>
        public virtual void Remove(VisualListViewSubItem item)
        {
            RemoveByKey(item.Name);
        }

        /// <summary>Removes the item at the specified index within the collection.</summary>
        /// <param name="index">The zero-based index of the item to remove.</param>
        public new virtual void RemoveAt(int index)
        {
            if (List.IsValidIndex(index))
            {
                List.RemoveAt(index);
                ChangedEvent?.Invoke(this, new ListViewChangedEventArgs(ListViewChangedTypes.SubItemCollectionChanged, null, null, null));
            }
        }

        /// <summary>Removes the item with the specified key from the collection.</summary>
        /// <param name="key">The name of the item to remove from the collection.</param>
        public virtual void RemoveByKey(string key)
        {
            int _index = IndexOfKey(key);
            if (List.IsValidIndex(_index))
            {
                RemoveAt(_index);
            }
        }

        /// <summary>The sub item has changed.</summary>
        /// <param name="source">The source.</param>
        /// <param name="e">The event args.</param>
        public void SubItem_Changed(object source, ListViewChangedEventArgs e)
        {
            ChangedEvent?.Invoke(source, e);
        }

        #endregion

        #region Methods

        /// <summary>Items have been cleared event.</summary>
        protected override void OnClear()
        {
            ChangedEvent?.Invoke(this, new ListViewChangedEventArgs(ListViewChangedTypes.ItemCollectionChanged, null, null, null));
        }

        #endregion
    }
}