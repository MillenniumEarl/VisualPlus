#region License

// -----------------------------------------------------------------------------------------------------------
// 
// Name: VisualListViewColumnCollection.cs
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
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

using VisualPlus.Delegates;
using VisualPlus.Enumerators;
using VisualPlus.Events;
using VisualPlus.Extensibility;
using VisualPlus.Localization;
using VisualPlus.Toolkit.Child;
using VisualPlus.Toolkit.Controls.DataManagement;

#endregion

namespace VisualPlus.Collections.CollectionsBase
{
    public class VisualListViewColumnCollection : CollectionBase, ICloneable, IList
    {
        #region Fields

        private VisualListView _listView;

        #endregion

        #region Constructors and Destructors

        /// <summary>Initializes a new instance of the <see cref="VisualListViewColumnCollection" /> class.</summary>
        /// <param name="owner">The <see cref="VisualListView" /> parent control.</param>
        public VisualListViewColumnCollection(VisualListView owner)
        {
            _listView = owner;
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
        public VisualListView ListView
        {
            get
            {
                return _listView;
            }

            set
            {
                _listView = value;
            }
        }

        [Description(PropertyDescription.Size)]
        public int Width
        {
            get
            {
                return List.Cast<VisualListViewColumn>().Sum(_column => _column.Width);
            }
        }

        #endregion

        #region Public Indexers

        /// <summary>Gets the column at the specified index within the collection.</summary>
        /// <param name="index">The index.</param>
        /// <returns>The <see cref="VisualListViewColumn" />.</returns>
        public virtual VisualListViewColumn this[int index]
        {
            get
            {
                return List[index] as VisualListViewColumn;
            }

            // set
            // {
            // List[index] = value;
            // }
        }

        /// <summary>Retrieves the column with the specified key.</summary>
        /// <param name="key">Retrieves the child control with the specified key.</param>
        /// <returns>The <see cref="VisualListViewColumn" />.</returns>
        public VisualListViewColumn this[string key]
        {
            get
            {
                if (string.IsNullOrEmpty(key))
                {
                    return null;
                }

                return (VisualListViewColumn)List[IndexOfKey(key)];
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>Adds an existing <see cref="VisualListViewColumn" /> to the collection.</summary>
        /// <param name="column">The <see cref="VisualListViewColumn" /> to add to the collection.</param>
        public virtual void Add(VisualListViewColumn column)
        {
            column.ListView = _listView;
            column.ChangedEvent += Column_Changed;

            List.Add(column);
            ChangedEvent?.Invoke(this, new ListViewChangedEventArgs(ListViewChangedTypes.ColumnCollectionChanged, column, null, null));
        }

        /// <summary>Adds a column to the collection with the specified text, width, and alignment settings.</summary>
        /// <param name="text">The text to display in the column header.</param>
        /// <returns>The <see cref="VisualListViewColumn" />.</returns>
        public virtual VisualListViewColumn Add(string text)
        {
            VisualListViewColumn _column = new VisualListViewColumn
                {
                    Name = nameof(VisualListViewColumn) + List.GetNextID(),
                    Text = text,
                    Width = Settings.DefaultValue.ColumnWidth,
                    State = ColumnStates.None,
                    TextAlignment = ContentAlignment.MiddleLeft,
                    ListView = _listView
                };

            Add(_column);
            return _column;
        }

        /// <summary>Creates and adds a column with the specified text, key, and width to the collection.</summary>
        /// <param name="key">The key of the column header.</param>
        /// <param name="text">The text to display in the column header.</param>
        /// <returns>The <see cref="VisualListViewColumn" />.</returns>
        public virtual VisualListViewColumn Add(string key, string text)
        {
            VisualListViewColumn _column = new VisualListViewColumn
                {
                    Name = key,
                    Text = text,
                    Width = Settings.DefaultValue.ColumnWidth,
                    State = ColumnStates.None,
                    TextAlignment = ContentAlignment.MiddleLeft,
                    ListView = _listView
                };

            Add(_column);
            return _column;
        }

        /// <summary>Creates and adds a column with the specified text, key, and width to the collection.</summary>
        /// <param name="text">The text to display in the column header.</param>
        /// <param name="width">The initial width of the <see cref="VisualListViewColumn" />.</param>
        /// <returns>The <see cref="VisualListViewColumn" />.</returns>
        public virtual VisualListViewColumn Add(string text, int width)
        {
            VisualListViewColumn _column = new VisualListViewColumn
                {
                    Name = nameof(VisualListViewColumn) + List.GetNextID(),
                    Text = text,
                    Width = width,
                    State = ColumnStates.None,
                    TextAlignment = ContentAlignment.MiddleLeft,
                    ListView = _listView
                };

            Add(_column);
            return _column;
        }

        /// <summary>Creates and adds a column with the specified text, key, and width to the collection.</summary>
        /// <param name="key">The key of the column header.</param>
        /// <param name="text">The text to display in the column header.</param>
        /// <param name="width">The initial width of the <see cref="VisualListViewColumn" />.</param>
        /// <returns>The <see cref="VisualListViewColumn" />.</returns>
        public virtual VisualListViewColumn Add(string key, string text, int width)
        {
            VisualListViewColumn _column = new VisualListViewColumn
                {
                    Name = key,
                    Text = text,
                    Width = width,
                    State = ColumnStates.None,
                    TextAlignment = ContentAlignment.MiddleLeft,
                    ListView = _listView
                };

            Add(_column);
            return _column;
        }

        /// <summary>Adds a column to the collection with the specified text, width, and alignment settings.</summary>
        /// <param name="key">The key of the column header.</param>
        /// <param name="text">The text to display in the column header.</param>
        /// <param name="width">The initial width of the column header.</param>
        /// <param name="textAlign">One of the <see cref="HorizontalAlignment" /> values.</param>
        /// <returns>The <see cref="VisualListViewColumn" />.</returns>
        public virtual VisualListViewColumn Add(string key, string text, int width, ContentAlignment textAlign)
        {
            VisualListViewColumn _column = new VisualListViewColumn
                {
                    Name = key,
                    Text = text,
                    Width = width,
                    State = ColumnStates.None,
                    TextAlignment = textAlign,
                    ListView = _listView
                };

            Add(_column);
            return _column;
        }

        /// <summary>Adds a column to the collection with the specified text, width, and alignment settings.</summary>
        /// <param name="text">The text to display in the column header.</param>
        /// <param name="width">The initial width of the column header.</param>
        /// <param name="textAlign">One of the <see cref="HorizontalAlignment" /> values.</param>
        /// <returns>The <see cref="VisualListViewColumn" />.</returns>
        public virtual VisualListViewColumn Add(string text, int width, ContentAlignment textAlign)
        {
            VisualListViewColumn _column = new VisualListViewColumn
                {
                    Name = nameof(VisualListViewColumn) + List.GetNextID(),
                    Text = text,
                    Width = width,
                    State = ColumnStates.None,
                    TextAlignment = textAlign,
                    ListView = _listView
                };

            Add(_column);
            return _column;
        }

        /// <summary>Adds an array of column headers to the collection.</summary>
        /// <param name="values">An array of <see cref="VisualListViewColumn" /> objects to to add to the collection.</param>
        public virtual void AddRange(VisualListViewColumn[] values)
        {
            lock (List.SyncRoot)
            {
                foreach (VisualListViewColumn _column in values)
                {
                    Add(_column);
                }
            }
        }

        /// <summary>Determines if any column headers are in a pressed state in the collection.</summary>
        /// <returns>The <see cref="bool" />.</returns>
        public virtual bool AnyPressed()
        {
            return List.Cast<VisualListViewColumn>().Any(_column => _column.State == ColumnStates.Pressed);
        }

        /// <summary>Clear all column headers hot states from the collection.</summary>
        public virtual void ClearHotStates()
        {
            foreach (VisualListViewColumn _column in List)
            {
                if (_column.State == ColumnStates.Hover)
                {
                    _column.State = ColumnStates.None;
                }
            }
        }

        /// <summary>Clear all column headers hot or pressed states.</summary>
        public virtual void ClearStates()
        {
            foreach (VisualListViewColumn _column in List)
            {
                _column.State = ColumnStates.None;
            }
        }

        /// <summary>Creates a shallow copy of the current object.</summary>
        /// <returns>The <see cref="object" />.</returns>
        public object Clone()
        {
            return MemberwiseClone();
        }

        /// <summary>The column has changed. Pass event up the chain.</summary>
        /// <param name="source">The source.</param>
        /// <param name="e">The event args.</param>
        public void Column_Changed(object source, ListViewChangedEventArgs e)
        {
            ChangedEvent?.Invoke(source, e);
        }

        /// <summary>Determines whether the specified column header is located in the collection.</summary>
        /// <param name="value">A <see cref="VisualListViewColumn" /> representing the column header to locate in the collection.</param>
        /// <returns>The <see cref="bool" />.</returns>
        public bool Contains(VisualListViewColumn value)
        {
            if (value == null)
            {
                return false;
            }

            return List.Contains(value);
        }

        /// <summary>Determines if a column with the specified key is contained in the collection.</summary>
        /// <param name="key">The key of the column header.</param>
        /// <returns>The <see cref="bool" />.</returns>
        public bool Contains(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                return false;
            }

            return List.Cast<VisualListViewColumn>().Any(_column => _column.Name == key);
        }

        /// <summary>Get the span size for column spanning.</summary>
        /// <param name="startColumnName">The start column name.</param>
        /// <param name="columnsSpanned">The columns spanned.</param>
        /// <returns>The <see cref="int" />.</returns>
        public int GetSpanSize(string startColumnName, int columnsSpanned)
        {
            var _spanSize = 0;
            int _startColumn = IndexOfKey(startColumnName);

            if (columnsSpanned + _startColumn > Count)
            {
                columnsSpanned = Count - _startColumn;
            }

            for (int index = _startColumn; index < _startColumn + columnsSpanned; index++)
            {
                _spanSize += this[index].Width;
            }

            return _spanSize;
        }

        /// <summary>Return the index, within the collection, of the specified column header.</summary>
        /// <param name="value">A <see cref="VisualListViewColumn" /> representing the column header to locate in the collection.</param>
        /// <returns>The <see cref="int" />.</returns>
        public virtual int IndexOf(VisualListViewColumn value)
        {
            return List.IndexOf(value);
        }

        /// <summary>Determines the index for a column with the specified key.</summary>
        /// <param name="key">The name of the column to retrieve the index for.</param>
        /// <returns>The <see cref="int" />.</returns>
        public virtual int IndexOfKey(string key)
        {
            for (var index = 0; index < List.Count; index++)
            {
                VisualListViewColumn _column = (VisualListViewColumn)List[index];
                if (key == _column.Name)
                {
                    return index;
                }
            }

            return -1;
        }

        /// <summary>Inserts an existing column header into the collection at the specified index.</summary>
        /// <param name="index">The zero-based index location where the column header is inserted.</param>
        /// <param name="value">The <see cref="VisualListViewColumn" /> to insert into the collection.</param>
        public void Insert(int index, VisualListViewColumn value)
        {
            value.ListView = _listView;
            value.ChangedEvent += Column_Changed;
            value.Name = nameof(VisualListViewColumn) + List.GetNextID();

            if (index < 0)
            {
                List.Add(value);
            }
            else
            {
                List.Insert(index, value);
            }

            ChangedEvent?.Invoke(this, new ListViewChangedEventArgs(ListViewChangedTypes.ColumnCollectionChanged, value, null, null));
        }

        /// <summary>
        ///     Creates a new column header with the specified text and key, and inserts the header into the collection at the
        ///     specified index.
        /// </summary>
        /// <param name="index">The zero-based index location where the column header is inserted.</param>
        /// <param name="text">The text to display in the column header.</param>
        public void Insert(int index, string text)
        {
            VisualListViewColumn _column = new VisualListViewColumn
                {
                    Name = nameof(VisualListViewColumn) + List.GetNextID(),
                    Text = text,
                    Width = Settings.DefaultValue.ColumnWidth,
                    State = ColumnStates.None,
                    TextAlignment = ContentAlignment.MiddleLeft,
                    ListView = _listView
                };

            Insert(index, _column);
        }

        /// <summary>
        ///     Creates a new column header with the specified text and initial width, and inserts the header into the
        ///     collection at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index location where the column header is inserted.</param>
        /// <param name="text">The text to display in the column header.</param>
        /// <param name="width">The initial width of the <see cref="VisualListViewColumn" />.</param>
        public void Insert(int index, string text, int width)
        {
            VisualListViewColumn _column = new VisualListViewColumn
                {
                    Name = nameof(VisualListViewColumn) + List.GetNextID(),
                    Text = text,
                    Width = width,
                    State = ColumnStates.None,
                    TextAlignment = ContentAlignment.MiddleLeft,
                    ListView = _listView
                };

            Insert(index, _column);
        }

        /// <summary>Creates a new column header and inserts it into the collection at the specified index.</summary>
        /// <param name="index">The zero-based index location where the column header is inserted.</param>
        /// <param name="text">The text to display in the column header.</param>
        /// <param name="width">The initial width of the <see cref="VisualListViewColumn" />.</param>
        /// <param name="textAlign">One of the <see cref="HorizontalAlignment" /> values.</param>
        public void Insert(int index, string text, int width, ContentAlignment textAlign)
        {
            VisualListViewColumn _column = new VisualListViewColumn
                {
                    Name = nameof(VisualListViewColumn) + List.GetNextID(),
                    Text = text,
                    Width = width,
                    State = ColumnStates.None,
                    TextAlignment = textAlign,
                    ListView = _listView
                };

            Insert(index, _column);
        }

        /// <summary>
        ///     Creates a new column header with the specified text and key, and inserts the header into the collection at the
        ///     specified index.
        /// </summary>
        /// <param name="index">The zero-based index location where the column header is inserted.</param>
        /// <param name="key">The name of the column header.</param>
        /// <param name="text">The text to display in the column header.</param>
        public void Insert(int index, string key, string text)
        {
            VisualListViewColumn _column = new VisualListViewColumn
                {
                    Name = key,
                    Text = text,
                    Width = Settings.DefaultValue.ColumnWidth,
                    State = ColumnStates.None,
                    TextAlignment = ContentAlignment.MiddleLeft,
                    ListView = _listView
                };

            Insert(index, _column);
        }

        /// <summary>
        ///     Creates a new column header with the specified text, key, and initial width, and inserts the header into the
        ///     collection at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index location where the column header is inserted.</param>
        /// <param name="key">The name of the column header.</param>
        /// <param name="text">The text to display in the column header.</param>
        /// <param name="width">The initial width of the <see cref="VisualListViewColumn" />.</param>
        public void Insert(int index, string key, string text, int width)
        {
            VisualListViewColumn _column = new VisualListViewColumn
                {
                    Name = key,
                    Text = text,
                    Width = width,
                    State = ColumnStates.None,
                    TextAlignment = ContentAlignment.MiddleLeft,
                    ListView = _listView
                };

            Insert(index, _column);
        }

        /// <summary>
        ///     Creates a new column header with the specified text, key, and initial width, and inserts the header into the
        ///     collection at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index location where the column header is inserted.</param>
        /// <param name="key">The name of the column header.</param>
        /// <param name="text">The text to display in the column header.</param>
        /// <param name="width">The initial width of the <see cref="VisualListViewColumn" />.</param>
        /// <param name="textAlign">One of the <see cref="HorizontalAlignment" /> values.</param>
        public void Insert(int index, string key, string text, int width, ContentAlignment textAlign)
        {
            VisualListViewColumn _column = new VisualListViewColumn
                {
                    Name = key,
                    Text = text,
                    Width = width,
                    State = ColumnStates.None,
                    TextAlignment = textAlign,
                    ListView = _listView
                };

            Insert(index, _column);
        }

        /// <summary>Removes the specified column header from the collection.</summary>
        /// <param name="column">A <see cref="VisualListViewColumn" /> representing the item to remove from the collection.</param>
        public virtual void Remove(VisualListViewColumn column)
        {
            RemoveByKey(column.Name);
        }

        /// <summary>Removes the item at the specified index within the collection.</summary>
        /// <param name="index">The zero-based index of the item to remove.</param>
        public new virtual void RemoveAt(int index)
        {
            if (List.IsValidIndex(index))
            {
                List.RemoveAt(index);
                ChangedEvent?.Invoke(this, new ListViewChangedEventArgs(ListViewChangedTypes.ColumnCollectionChanged, null, null, null));
            }
        }

        /// <summary>Removes the column with the specified key from the collection.</summary>
        /// <param name="key">The name of the column to remove from the collection.</param>
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

        /// <summary>The columns header collection has been cleared event.</summary>
        protected override void OnClear()
        {
            ChangedEvent?.Invoke(this, new ListViewChangedEventArgs(ListViewChangedTypes.ColumnCollectionChanged, null, null, null));
        }

        #endregion
    }
}