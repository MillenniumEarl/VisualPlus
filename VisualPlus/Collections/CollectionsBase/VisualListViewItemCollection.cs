#region License

// -----------------------------------------------------------------------------------------------------------
// 
// Name: VisualListViewItemCollection.cs
// 
// Copyright (c) 2016 - 2019 VisualPlus <https://darkbyte7.github.io/VisualPlus/>
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
using System.Linq;

using VisualPlus.Attributes;
using VisualPlus.Delegates;
using VisualPlus.Enumerators;
using VisualPlus.Events;
using VisualPlus.Extensibility;
using VisualPlus.Localization;
using VisualPlus.Toolkit.Child;
using VisualPlus.Toolkit.Controls.DataManagement;
using VisualPlus.Utilities;

#endregion

namespace VisualPlus.Collections.CollectionsBase
{
    public class VisualListViewItemCollection : CollectionBase, ICloneable, IList
    {
        #region Fields

        private VisualListView _listView;
        private bool _suspendEvents;

        #endregion

        #region Constructors and Destructors

        /// <summary>Initializes a new instance of the <see cref="VisualListViewItemCollection" /> class.</summary>
        /// <param name="listView">The new Parent.</param>
        public VisualListViewItemCollection(VisualListView listView)
        {
            ListView = listView;
        }

        #endregion

        #region Public Events

        [Category(EventCategory.PropertyChanged)]
        [Description(EventDescription.PropertyEventChanged)]
        public event ListViewChangedEventHandler ChangedEvent;

        #endregion

        #region Public Properties

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
                foreach (VisualListViewItem _item in List)
                {
                    _item.ListView = _listView;
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
                    VisualListViewItem _item = this[i];
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
        public List<VisualListViewItem> SelectedItems
        {
            get
            {
                var _selectedItems = new List<VisualListViewItem>();
                for (var i = 0; i < Count; i++)
                {
                    VisualListViewItem _item = this[i];
                    if (_item.Selected)
                    {
                        _selectedItems.Add(_item);
                    }
                }

                return _selectedItems;
            }
        }

        /// <summary>
        ///     Used for operations when multiple items are changed consecutively and don't want to send a larger number of
        ///     change events than necessary.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public bool SuspendEvents
        {
            get
            {
                return _suspendEvents;
            }

            set
            {
                _suspendEvents = value;
            }
        }

        #endregion

        #region Public Indexers

        /// <summary>Gets or sets the item at the specified index within the collection.</summary>
        /// <param name="index">The index of the item in the collection to get or set.</param>
        /// <returns>The <see cref="VisualListViewItem" />.</returns>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public virtual VisualListViewItem this[int index]
        {
            get
            {
                return (VisualListViewItem)List[index];
            }

            set
            {
                List[index] = value;
            }
        }

        /// <summary>Retrieves the item with the specified key.</summary>
        /// <param name="key">Retrieves the child control with the specified key.</param>
        /// <returns>The <see cref="VisualListViewItem" />.</returns>
        public VisualListViewItem this[string key]
        {
            get
            {
                if (string.IsNullOrEmpty(key))
                {
                    return null;
                }

                return (VisualListViewItem)List[IndexOfKey(key)];
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>Creates an item with the specified text and adds it to the collection.</summary>
        /// <param name="text">The text to display for the item.</param>
        /// <returns>The <see cref="VisualListViewItem" />.</returns>
        public virtual VisualListViewItem Add(string text)
        {
            VisualListViewItem _item = new VisualListViewItem(text) { ListView = _listView };

            Add(_item);
            return _item;
        }

        /// <summary>Adds an existing <see cref="VisualListViewItem" /> to the collection.</summary>
        /// <param name="value">The <see cref="VisualListViewItem" /> to add to the collection.</param>
        /// <returns>The <see cref="VisualListViewItem" />.</returns>
        public virtual VisualListViewItem Add(VisualListViewItem value)
        {
            value.ListView = _listView;
            value.ChangedEvent += Item_Changed;

            List.Add(value);
            ChangedEvent?.Invoke(this, new ListViewChangedEventArgs(ListViewChangedTypes.ItemCollectionChanged, null, null, null));
            return value;
        }

        /// <summary>Creates an item with the specified text and image and adds it to the collection.</summary>
        /// <param name="text">The text of the item.</param>
        /// <param name="imageIndex">The index of the image to display for the item.</param>
        /// <returns>The <see cref="VisualListViewItem" />.</returns>
        public virtual VisualListViewItem Add(string text, int imageIndex)
        {
            VisualListViewItem _item = new VisualListViewItem(text, imageIndex);
            Add(_item);
            return _item;
        }

        /// <summary>Creates an item with the specified key, text and image and adds an item to the collection.</summary>
        /// <param name="key">The name of the item.</param>
        /// <param name="text">The text of the item.</param>
        /// <param name="imageIndex">The index of the image to display for the item.</param>
        /// <returns>The <see cref="VisualListViewItem" />.</returns>
        public virtual VisualListViewItem Add(string key, string text, int imageIndex)
        {
            VisualListViewItem _item = new VisualListViewItem(text, imageIndex) { Name = key };

            Add(_item);
            return _item;
        }

        /// <summary>Adds an array of <see cref="VisualListViewItem" /> objects to the collection.</summary>
        /// <param name="items">An array of <see cref="VisualListViewItem" /> objects to add to the collection.</param>
        public void AddRange(VisualListViewItem[] items)
        {
            if (items == null)
            {
                throw new ArgumentNullException("items");
            }

            foreach (VisualListViewItem _item in items)
            {
                Add(_item);
            }
        }

        /// <summary>Adds a collection of items to the collection.</summary>
        /// <param name="items">The <see cref="VisualListViewItemCollection" /> to add to the collection.</param>
        public void AddRange(VisualListViewItemCollection items)
        {
            if (items == null)
            {
                throw new ArgumentNullException("items");
            }

            VisualListViewItem[] _items = { };

            for (var i = 0; i < items.Count; i++)
            {
                _items[i] = items[i];
            }

            AddRange(_items);
        }

        /// <summary>Clears all selection bits in the item structure.</summary>
        /// <param name="itemIgnore">This overload is an optimization to stop a redraw on a re-selection.</param>
        public void ClearSelection(VisualListViewItem itemIgnore)
        {
            for (var index = 0; index < List.Count; index++)
            {
                VisualListViewItem _item = this[index];
                if (_item != itemIgnore)
                {
                    _item.Selected = false;
                }
            }
        }

        /// <summary>Clears all selection bits in the item structure.</summary>
        public void ClearSelection()
        {
            for (var i = 0; i < List.Count; i++)
            {
                this[i].Selected = false;
            }
        }

        /// <summary>Creates a shallow copy of the current object.</summary>
        /// <returns>The <see cref="object" />.</returns>
        public object Clone()
        {
            return MemberwiseClone();
        }

        /// <summary>Determines whether the specified item is located in the collection.</summary>
        /// <param name="value">A <see cref="VisualListViewItem" /> representing the item to locate in the collection.</param>
        /// <returns>The <see cref="bool" />.</returns>
        public bool Contains(VisualListViewItem value)
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

            return List.Cast<VisualListViewItem>().Any(_item => _item.Name == key);
        }

        /// <summary>Determines whether the specified item is located in the collection.</summary>
        /// <param name="value">A <see cref="VisualListViewItem" /> representing the item to locate in the collection.</param>
        /// <returns>The <see cref="bool" />.</returns>
        [Test]
        public bool ContainsItem(VisualListViewItem value)
        {
            foreach (VisualListViewItem _item in List)
            {
                var _subItemFlag = true;
                for (var i = 0; i < _item.SubItems.Count; i++)
                {
                    string _subItem1 = _item.SubItems[i].Text;
                    string _subItem2 = value.SubItems[i].Text;

                    if (_subItem1 != _subItem2)
                    {
                        _subItemFlag = false;
                    }
                }

                if (_subItemFlag)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>Copies the entire collection into an existing array at a specified location within the array.</summary>
        /// <param name="destination">An <see cref="Array" /> representing the array to copy the contents of the collection to.</param>
        /// <param name="index">The location within the destination array to copy the items from the collection to.</param>
        public void CopyTo(Array destination, int index)
        {
            List.CopyTo(destination, index);
        }

        /// <summary>Searches for items whose name matches the specified key, optionally searching subitems.</summary>
        /// <param name="key">The item name to search for.</param>
        /// <param name="searchAllSubItems">To search subitem.</param>
        /// <returns>
        ///     An array of <see cref="VisualListViewItem" /> objects containing the matching items, or an empty array if no
        ///     items matched.
        /// </returns>
        public VisualListViewItem[] Find(string key, bool searchAllSubItems)
        {
            ArrayList _foundItems = FindInternal(key, searchAllSubItems, this, new ArrayList());
            var _stronglyTypedFoundItems = new VisualListViewItem[_foundItems.Count];
            _foundItems.CopyTo(_stronglyTypedFoundItems, 0);
            return _stronglyTypedFoundItems;
        }

        /// <summary>Find the index of a specified item.</summary>
        /// <param name="item">The item.</param>
        /// <returns>The <see cref="int" />.</returns>
        public int FindItemIndex(VisualListViewItem item)
        {
            for (var index = 0; index < Count; index++)
            {
                if (item == this[index])
                {
                    return index;
                }
            }

            return -1;
        }

        /// <summary>Find the next item index.</summary>
        /// <param name="startIndex">The start index.</param>
        /// <param name="column">The column.</param>
        /// <param name="text">The text.</param>
        /// <returns>The <see cref="int" />.</returns>
        public int FindNextItemIndex(int startIndex, int column, string text)
        {
            if ((startIndex < 0) || (startIndex > Count))
            {
                startIndex = 0;
            }

            for (int index = startIndex; index < Count; index++)
            {
                if (text == this[index].SubItems[column].Text)
                {
                    return index;
                }
            }

            // Unable to find it.
            return -1;
        }

        /// <summary>Gets the next selected item index.</summary>
        /// <param name="startIndex">The start index.</param>
        /// <returns>The <see cref="int" />.</returns>
        public int GetNextSelectedItemIndex(int startIndex)
        {
            if ((startIndex < 0) || (startIndex > Count))
            {
                startIndex = -1;
            }

            for (int index = startIndex + 1; index < Count; index++)
            {
                if (this[index].Selected)
                {
                    return index;
                }
            }

            // Unable to find it.
            return -1;
        }

        /// <summary>Return the index within the collection of the specified item.</summary>
        /// <param name="item">A <see cref="VisualListViewItem" /> representing the item to locate in the collection.</param>
        /// <returns>The <see cref="int" />.</returns>
        public int IndexOf(VisualListViewItem item)
        {
            return List.IndexOf(item);
        }

        /// <summary>Retrieves the index of the item with the specified key.</summary>
        /// <param name="key">The name of the item to find in the collection.</param>
        /// <returns>The <see cref="int" />.</returns>
        public virtual int IndexOfKey(string key)
        {
            for (var index = 0; index < List.Count; index++)
            {
                VisualListViewItem _item = (VisualListViewItem)List[index];
                if (key == _item.Name)
                {
                    return index;
                }
            }

            return -1;
        }

        /// <summary>Creates a new item and inserts it into the collection at the specified index.</summary>
        /// <param name="index">The zero-based index location where the item is inserted.</param>
        /// <param name="key">The name of the item.</param>
        /// <param name="text">The text to display for the item.</param>
        /// <param name="imageIndex">The index of the image to display for the item.</param>
        /// <returns>The <see cref="VisualListViewItem" />.</returns>
        public virtual VisualListViewItem Insert(int index, string key, string text, int imageIndex)
        {
            VisualListViewItem _item = new VisualListViewItem { Name = key, Text = text, ImageIndex = imageIndex };

            return Insert(index, _item);
        }

        /// <summary>Creates a new item and inserts it into the collection at the specified index.</summary>
        /// <param name="index">The zero-based index location where the item is inserted.</param>
        /// <param name="text">The text to display for the item.</param>
        /// <returns>The <see cref="VisualListViewItem" />.</returns>
        public VisualListViewItem Insert(int index, string text)
        {
            VisualListViewItem _item = new VisualListViewItem { Text = text };

            return Insert(index, _item);
        }

        /// <summary>Creates a new item and inserts it into the collection at the specified index.</summary>
        /// <param name="index">The zero-based index location where the item is inserted.</param>
        /// <param name="text">The text to display for the item.</param>
        /// <param name="imageIndex">The index of the image to display for the item.</param>
        /// <returns>The <see cref="VisualListViewItem" />.</returns>
        public VisualListViewItem Insert(int index, string text, int imageIndex)
        {
            VisualListViewItem _item = new VisualListViewItem { Text = text, ImageIndex = imageIndex };

            return Insert(index, _item);
        }

        /// <summary>Inserts an existing <see cref="VisualListViewItem" /> into the collection at the specified index.</summary>
        /// <param name="index">The zero-based index location where the item is inserted.</param>
        /// <param name="item">The <see cref="VisualListViewItem" /> that represents the item to insert.</param>
        /// <returns>The <see cref="VisualListViewItem" />.</returns>
        public VisualListViewItem Insert(int index, VisualListViewItem item)
        {
            if ((index < 0) || (index > Count))
            {
                throw new ArgumentOutOfRangeException("index=" + index);
            }

            item.ListView = _listView;
            item.ChangedEvent += Item_Changed;

            if (index < 0)
            {
                List.Add(item);
            }
            else
            {
                List.Insert(index, item);
            }

            ChangedEvent?.Invoke(this, new ListViewChangedEventArgs(ListViewChangedTypes.ItemCollectionChanged, null, null, null));
            return item;
        }

        /// <summary>The item has changed.</summary>
        /// <param name="source">The source.</param>
        /// <param name="e">The event args.</param>
        public void Item_Changed(object source, ListViewChangedEventArgs e)
        {
            if ((ChangedEvent != null) && !SuspendEvents)
            {
                ChangedEvent(source, e);
            }
        }

        /// <summary>Removes the specified item from the collection.</summary>
        /// <param name="item">The <see cref="VisualListViewItem" /> representing the item to remove from the collection.</param>
        public virtual void Remove(VisualListViewItem item)
        {
            List.Remove(item);
            ChangedEvent?.Invoke(this, new ListViewChangedEventArgs(ListViewChangedTypes.ItemCollectionChanged, null, null, null));
        }

        /// <summary>Removes the item at the specified index within the collection.</summary>
        /// <param name="index">The zero-based index of the item to remove.</param>
        public new virtual void RemoveAt(int index)
        {
            if (List.IsValidIndex(index))
            {
                List.RemoveAt(index);
                ChangedEvent?.Invoke(this, new ListViewChangedEventArgs(ListViewChangedTypes.ItemCollectionChanged, null, null, null));
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

        #endregion

        #region Methods

        /// <summary>The item collection has been cleared event.</summary>
        protected override void OnClear()
        {
            ChangedEvent?.Invoke(this, new ListViewChangedEventArgs(ListViewChangedTypes.ItemCollectionChanged, null, null, null));
        }

        /// <summary>Searches for controls by their name property, builds an array list.</summary>
        /// <param name="key">The key name to search for.</param>
        /// <param name="searchAllSubItems">Search all subitems.</param>
        /// <param name="listViewItems">The listViewItems collections.</param>
        /// <param name="foundItems">The found items array.</param>
        /// <returns>The <see cref="ArrayList" />.</returns>
        private static ArrayList FindInternal(string key, bool searchAllSubItems, VisualListViewItemCollection listViewItems, ArrayList foundItems)
        {
            if ((listViewItems == null) || (foundItems == null))
            {
                return null;
            }

            for (var i = 0; i < listViewItems.Count; i++)
            {
                if (TextManager.SafeCompareStrings(listViewItems[i].Name, key, true))
                {
                    foundItems.Add(listViewItems[i]);
                }
                else
                {
                    if (searchAllSubItems)
                    {
                        // Start from 1, as we've already compare subitems[0].
                        for (var j = 1; j < listViewItems[i].SubItems.Count; j++)
                        {
                            if (TextManager.SafeCompareStrings(listViewItems[i].SubItems[j].Name, key, true))
                            {
                                foundItems.Add(listViewItems[i]);
                                break;
                            }
                        }
                    }
                }
            }

            return foundItems;
        }

        #endregion
    }
}