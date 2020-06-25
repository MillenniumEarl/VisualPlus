#region License

// -----------------------------------------------------------------------------------------------------------
//
// Name: VisualListViewItem.cs
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

#endregion License

#region Namespace

using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;

using VisualPlus.Collections.CollectionsBase;
using VisualPlus.Delegates;
using VisualPlus.Enumerators;
using VisualPlus.Events;
using VisualPlus.Localization;
using VisualPlus.Toolkit.Controls.DataManagement;
using VisualPlus.TypeConverters;

#endregion Namespace

namespace VisualPlus.Toolkit.Child
{
    [DebuggerStepThrough]
    [DesignTimeVisible(true)]
    [TypeConverter(typeof(VisualListViewItemConverter))]
    public class VisualListViewItem : ICloneable
    {
        #region Fields

        private Color _backColor;
        private Font _font;
        private Color _foreColor;
        private ImageList _imageList;
        private int _lastIndex;
        private VisualListView _listView;
        private Color _rowBorderColor;
        private int _rowBorderSize;
        private bool _selected;
        private VisualListViewSubItemCollection _subItemCollection;
        private object _tag;

        #endregion Fields

        #region Constructors and Destructors

        /// <summary>Initializes a new instance of the <see cref="VisualListViewItem" /> class.</summary>
        public VisualListViewItem()
        {
            _selected = false;
            _tag = null;
            _foreColor = Color.Black;
            _rowBorderColor = Color.Black;
            _rowBorderSize = 0;
            _backColor = Color.White;
            _font = SystemFonts.DefaultFont;
            _imageList = new ImageList();
            _lastIndex = -1;

            if (_listView != null)
            {
                _subItemCollection = new VisualListViewSubItemCollection(_listView);
            }
            else
            {
                _subItemCollection = new VisualListViewSubItemCollection();
            }

            _subItemCollection.ChangedEvent += SubItemCollection_Changed;
        }

        /// <summary>Initializes a new instance of the <see cref="VisualListViewItem" /> class.</summary>
        /// <param name="listView">The list View.</param>
        public VisualListViewItem(VisualListView listView) : this()
        {
            _subItemCollection = new VisualListViewSubItemCollection(listView) { ListView = listView };

            _listView = listView;
            _subItemCollection.ListView = _listView;

            _subItemCollection.ChangedEvent += SubItemCollection_Changed;
        }

        /// <summary>Initializes a new instance of the <see cref="VisualListViewItem" /> class.</summary>
        /// <param name="subItems">The array of type <see cref="VisualListViewSubItem" /> that represents the subitems of the item.</param>
        /// <param name="imageIndex">
        ///     The zero-based index of the image within the <see cref="ImageList" /> associated with the
        ///     <see cref="VisualListView" /> that contains the item.
        /// </param>
        public VisualListViewItem(VisualListViewSubItem[] subItems, int imageIndex) : this()
        {
            _subItemCollection[0].ImageIndex = imageIndex;

            foreach (VisualListViewSubItem subItem in subItems)
            {
                subItem.Owner = this;
                _subItemCollection.Add(subItem);
            }
        }

        /// <summary>Initializes a new instance of the <see cref="VisualListViewItem" /> class.</summary>
        /// <param name="subItems">
        ///     The collection of type <see cref="VisualListViewSubItem" /> that represents the subitems of the
        ///     item.
        /// </param>
        /// <param name="imageIndex">
        ///     The zero-based index of the image within the <see cref="ImageList" /> associated with the
        ///     <see cref="VisualListView" /> that contains the item.
        /// </param>
        public VisualListViewItem(VisualListViewSubItemCollection subItems, int imageIndex) : this()
        {
            _subItemCollection[0].ImageIndex = imageIndex;

            foreach (VisualListViewSubItem subItem in subItems)
            {
                subItem.Owner = this;
                _subItemCollection.Add(subItem);
            }
        }

        /// <summary>Initializes a new instance of the <see cref="VisualListViewItem" /> class.</summary>
        /// <param name="text">The text to display for the item. This should not exceed 259 characters.</param>
        /// <param name="imageIndex">
        ///     The zero-based index of the image within the <see cref="ImageList" /> associated with the
        ///     <see cref="VisualListView" /> that contains the item.
        /// </param>
        public VisualListViewItem(string text, int imageIndex) : this()
        {
            _subItemCollection[0].ImageIndex = imageIndex;
            _subItemCollection[0].Text = text;
        }

        /// <summary>Initializes a new instance of the <see cref="VisualListViewItem" /> class.</summary>
        /// <param name="text">The text to display for the item. This should not exceed 259 characters.</param>
        public VisualListViewItem(string text) : this(text, -1)
        {
        }

        /// <summary>Initializes a new instance of the <see cref="VisualListViewItem" /> class.</summary>
        /// <param name="items">An array of string that represent the subitems of the new item.</param>
        public VisualListViewItem(string[] items) : this(items, -1)
        {
        }

        /// <summary>Initializes a new instance of the <see cref="VisualListViewItem" /> class.</summary>
        /// <param name="items">An array of string that represent the subitems of the new item.</param>
        /// <param name="imageIndex">
        ///     The zero-based index of the image within the <see cref="ImageList" /> associated with the
        ///     <see cref="VisualListView" /> that contains the item.
        /// </param>
        public VisualListViewItem(string[] items, int imageIndex) : this()
        {
            _subItemCollection[0].ImageIndex = imageIndex;

            if ((items != null) && (items.Length > 0))
            {
                foreach (string _item in items)
                {
                    _subItemCollection.Add(_item);
                }
            }
        }

        /// <summary>Initializes a new instance of the <see cref="VisualListViewItem" /> class.</summary>
        /// <param name="items">An array of string that represent the subitems of the new item.</param>
        /// <param name="imageIndex">
        ///     The zero-based index of the image within the <see cref="ImageList" /> associated with the
        ///     <see cref="VisualListView" /> that contains the item.
        /// </param>
        /// <param name="foreColor">A <see cref="Color" /> that represents the foreground color of the item.</param>
        /// <param name="backColor">A <see cref="Color" /> that represents the background color of the item.</param>
        /// <param name="font">A <see cref="Font" /> that represents the font to display the item's text in.</param>
        public VisualListViewItem(string[] items, int imageIndex, Color foreColor, Color backColor, Font font) : this(items, imageIndex)
        {
            if (items == null)
            {
                throw new ArgumentNullException("items");
            }

            _foreColor = foreColor;
            _backColor = backColor;
            _font = font;
        }

        #endregion Constructors and Destructors

        #region Public Events

        [Category(EventCategory.PropertyChanged)]
        [Description(EventDescription.PropertyEventChanged)]
        public event ListViewChangedEventHandler ChangedEvent;

        #endregion Public Events

        #region Public Properties

        [Browsable(true)]
        [Category(PropertyCategory.Appearance)]
        [Description(PropertyDescription.Color)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color BackColor
        {
            get
            {
                if (_subItemCollection.Count == 0)
                {
                    if (_listView != null)
                    {
                        return _listView.BackColor;
                    }

                    return SystemColors.Window;
                }
                else
                {
                    return _backColor;
                }
            }

            set
            {
                _backColor = value;
            }
        }

        [Browsable(false)]
        [Description(PropertyDescription.Toggle)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public bool CheckBox
        {
            get
            {
                return _subItemCollection[0].CheckBox;
            }

            set
            {
                _subItemCollection[0].CheckBox = value;
            }
        }

        [Browsable(false)]
        [Description(PropertyDescription.Toggle)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public bool Checked
        {
            get
            {
                return _subItemCollection[0].Checked;
            }

            set
            {
                _subItemCollection[0].Checked = value;
            }
        }

        [Browsable(true)]
        [Category(PropertyCategory.Appearance)]
        [Description(PropertyDescription.Font)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Font Font
        {
            get
            {
                if (_subItemCollection.Count == 0)
                {
                    if (_listView != null)
                    {
                        return _listView.Font;
                    }

                    return Control.DefaultFont;
                }
                else
                {
                    return _subItemCollection[0].Font;
                }
            }

            set
            {
                _subItemCollection[0].Font = value;
            }
        }

        [Browsable(true)]
        [Category(PropertyCategory.Appearance)]
        [Description(PropertyDescription.Color)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color ForeColor
        {
            get
            {
                if (_subItemCollection.Count == 0)
                {
                    if (_listView != null)
                    {
                        return _listView.ForeColor;
                    }

                    return SystemColors.WindowText;
                }
                else
                {
                    return _foreColor;
                }
            }

            set
            {
                _foreColor = value;
            }
        }

        [Category(EventCategory.Behavior)]
        [Description(PropertyDescription.ImageIndex)]
        [TypeConverter(typeof(ImageIndexConverter))]
        public int ImageIndex
        {
            get
            {
                return _subItemCollection[0].ImageIndex;
            }

            set
            {
                _subItemCollection[0].ImageIndex = value;
            }
        }

        [Browsable(true)]
        [Category(EventCategory.Behavior)]
        [Description("ImageList to be used for sub items.")]
        [RefreshProperties(RefreshProperties.Repaint)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public ImageList ImageList
        {
            get
            {
                return _imageList;
            }

            set
            {
                _imageList = value;
            }
        }

        [Browsable(false)]
        public int Index
        {
            get
            {
                if (_listView != null)
                {
                    return _lastIndex;
                }
                else
                {
                    return -1;
                }
            }
        }

        [Browsable(false)]
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
                _subItemCollection.ListView = value;
            }
        }

        [Browsable(true)]
        [Category(PropertyCategory.Design)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Name
        {
            get
            {
                return _subItemCollection[0].Name;
            }

            set
            {
                _subItemCollection[0].Name = value;
                ChangedEvent?.Invoke(this, new ListViewChangedEventArgs(ListViewChangedTypes.ItemChanged, null, this, null));
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color RowBorderColor
        {
            get
            {
                return _rowBorderColor;
            }

            set
            {
                _rowBorderColor = value;
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public int RowBorderSize
        {
            get
            {
                return _rowBorderSize;
            }

            set
            {
                _rowBorderSize = value;
            }
        }

        [Browsable(false)]
        [DefaultValue(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool Selected
        {
            get
            {
                return _selected;
            }

            set
            {
                if (_selected != value)
                {
                    if ((_listView != null) && _listView.MultiSelect && _listView.Items.SuspendEvents)
                    {
                        _listView.Items.SuspendEvents = true;
                        _listView.Items.ClearSelection();
                        _listView.Items.SuspendEvents = false;
                    }

                    _selected = value;
                    ChangedEvent?.Invoke(this, new ListViewChangedEventArgs(ListViewChangedTypes.SelectionChanged, null, this, null));
                }
            }
        }

        // Fix: Throws unable to cast error on custom collection editor.
        // [Editor(typeof(VisualListViewSubItemCollectionEditor), typeof(UITypeEditor))]
        [Browsable(true)]
        [Category(PropertyCategory.Data)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Editor(typeof(CollectionEditor), typeof(UITypeEditor))]
        public VisualListViewSubItemCollection SubItems
        {
            get
            {
                return _subItemCollection;
            }
        }

        [Bindable(true)]
        [Browsable(false)]
        [Category(PropertyCategory.Data)]
        [DefaultValue(null)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [TypeConverter(typeof(StringConverter))]
        public object Tag
        {
            get
            {
                return _tag;
            }

            set
            {
                _tag = value;
            }
        }

        // [Browsable(false)]
        // [Category(PropertyCategory.Appearance)]
        // [Description(PropertyDescription.Text)]
        // [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string Text
        {
            get
            {
                return _subItemCollection[0].Text;
            }

            set
            {
                _subItemCollection[0].Text = value;
                ChangedEvent?.Invoke(this, new ListViewChangedEventArgs(ListViewChangedTypes.ItemChanged, null, this, null));
            }
        }

        #endregion Public Properties

        #region Public Methods and Operators

        /// <summary>
        ///     Creates an identical copy of the current <see cref="VisualListViewItem" /> that is not attached to any list
        ///     view control.
        /// </summary>
        /// <returns>The <see cref="Object" />.</returns>
        public virtual object Clone()
        {
            VisualListViewSubItemCollection _clonedSubItemCollection = new VisualListViewSubItemCollection();
            for (var i = 0; i < _subItemCollection.Count; i++)
            {
                VisualListViewSubItem _subItem = _subItemCollection[i];

                _clonedSubItemCollection.Add(new VisualListViewSubItem(null, _subItem.Text, _subItem.ForeColor, _subItem.BackColor, _subItem.Font) { Tag = _subItem.Tag });
            }

            Type _clonedType = GetType();
            VisualListViewItem _listViewItem;

            if (_clonedType == typeof(VisualListViewItem))
            {
                _listViewItem = new VisualListViewItem(_clonedSubItemCollection, _subItemCollection[0].ImageIndex);
            }
            else
            {
                _listViewItem = (VisualListViewItem)Activator.CreateInstance(_clonedType);
            }

            _listViewItem.SubItems.Clear();
            foreach (VisualListViewSubItem subItem in _clonedSubItemCollection)
            {
                _listViewItem.SubItems.Add(subItem);
            }

            _listViewItem.Checked = _subItemCollection[0].Checked;
            _listViewItem.ImageIndex = _subItemCollection[0].ImageIndex;
            _listViewItem.Tag = _tag;
            _listViewItem.BackColor = _backColor;
            _listViewItem.ForeColor = _foreColor;
            _listViewItem.Font = _font;
            _listViewItem.Text = Text;
            return _listViewItem;
        }

        /// <summary>Removes the item from its associated <see cref="VisualListView" /> control.</summary>
        public virtual void Remove()
        {
            _listView?.Items.Remove(this);
        }

        /// <summary>Occurs when the sub-item collection has changed.</summary>
        /// <param name="source">The source.</param>
        /// <param name="e">The event args.</param>
        public void SubItemCollection_Changed(object source, ListViewChangedEventArgs e)
        {
            if (ChangedEvent != null)
            {
                e.Item = this;
                ChangedEvent(this, e);
            }

            if (_listView != null)
            {
                _listView.Invalidate();
            }
        }

        public override string ToString()
        {
            return GetType().Name + ": {" + Text + "} { SubItems: " + SubItems.Count + "}";
        }

        #endregion Public Methods and Operators
    }
}