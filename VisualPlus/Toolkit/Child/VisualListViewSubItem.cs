#region License

// -----------------------------------------------------------------------------------------------------------
//
// Name: VisualListViewSubItem.cs
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

using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

using VisualPlus.Delegates;
using VisualPlus.Enumerators;
using VisualPlus.Events;
using VisualPlus.Interfaces;
using VisualPlus.Localization;
using VisualPlus.Toolkit.Controls.DataManagement;
using VisualPlus.TypeConverters;

#endregion Namespace

namespace VisualPlus.Toolkit.Child
{
    [DesignTimeVisible(true)]
    [TypeConverter(typeof(VisualListViewSubItemConverter))]
    public class VisualListViewSubItem
    {
        #region Fields

        private Color _backColor;
        private bool _checkBox;
        private bool _checked;
        private Control _embeddedControl;
        private Hashtable _embeddedControlProperties;
        private Font _font;
        private bool _forceText;
        private Color _foreColor;
        private HorizontalAlignment _imageAlignment;
        private int _imageIndex;
        private Rectangle _lastCellRectangle;
        private VisualListView _listView;
        private string _name;
        private VisualListViewItem _owner;
        private bool _selected;
        private object _tag;
        private string _text;

        #endregion Fields

        #region Constructors and Destructors

        /// <summary>Initializes a new instance of the <see cref="VisualListViewSubItem" /> class.</summary>
        public VisualListViewSubItem()
        {
            _imageIndex = -1;
            _imageAlignment = HorizontalAlignment.Left;
            _backColor = Color.White;
            _selected = false;
            _tag = null;
            _forceText = false;
            _embeddedControl = null;
            _listView = null;
            _checked = false;
            _checkBox = false;
            _embeddedControlProperties = null;
            _lastCellRectangle = new Rectangle(0, 0, 0, 0);
            _foreColor = Color.Black;
            _font = SystemFonts.DefaultFont;
            _owner = null;
            _text = string.Empty;
            _name = string.Empty;
        }

        /// <summary>Initializes a new instance of the <see cref="VisualListViewSubItem" /> class.</summary>
        /// <param name="text">The text to display for the subitem.</param>
        public VisualListViewSubItem(string text) : this()
        {
            _text = text;
        }

        /// <summary>Initializes a new instance of the <see cref="VisualListViewSubItem" /> class.</summary>
        /// <param name="owner">The <see cref="VisualListViewItem" /> that represents the item that owns the subitem.</param>
        /// <param name="text">The text to display for the subitem.</param>
        public VisualListViewSubItem(VisualListViewItem owner, string text) : this()
        {
            _owner = owner;
            _text = text;
        }

        /// <summary>Initializes a new instance of the <see cref="VisualListViewSubItem" /> class.</summary>
        /// <param name="owner">The <see cref="VisualListViewItem" /> that represents the item that owns the subitem.</param>
        /// <param name="text">The text to display for the subitem.</param>
        /// <param name="foreColor">A <see cref="Color" /> that represents the foreground color of the item.</param>
        /// <param name="backColor">A <see cref="Color" /> that represents the background color of the item.</param>
        /// <param name="font">A <see cref="Font" /> that represents the font to display the item's text in.</param>
        public VisualListViewSubItem(VisualListViewItem owner, string text, Color foreColor, Color backColor, Font font) : this()
        {
            _owner = owner;
            _text = text;
            _foreColor = foreColor;
            _backColor = backColor;
            _font = font;
        }

        #endregion Constructors and Destructors

        #region Public Events

        public event ListViewChangedEventHandler ChangedEvent;

        #endregion Public Events

        #region Public Properties

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color BackColor
        {
            get
            {
                return _backColor;
            }

            set
            {
                if (_backColor != value)
                {
                    _backColor = value;
                    ChangedEvent?.Invoke(this, new ListViewChangedEventArgs(ListViewChangedTypes.SubItemChanged, null, null, this));
                }
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Description(PropertyDescription.Toggle)]
        public bool CheckBox
        {
            get
            {
                return _checkBox;
            }

            set
            {
                if (_checkBox != value)
                {
                    _checkBox = value;
                    ChangedEvent?.Invoke(this, new ListViewChangedEventArgs(ListViewChangedTypes.SubItemChanged, null, null, this));
                }
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Description(PropertyDescription.Toggle)]
        public bool Checked
        {
            get
            {
                return _checked;
            }

            set
            {
                if (_checked != value)
                {
                    _checked = value;
                    ChangedEvent?.Invoke(this, new ListViewChangedEventArgs(ListViewChangedTypes.SubItemChanged, null, null, this));
                }
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Control EmbeddedControl
        {
            get
            {
                return _embeddedControl;
            }

            set
            {
                if (_embeddedControl != value)
                {
                    _embeddedControl = value;
                    _embeddedControl.Visible = false;
                }
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Hashtable EmbeddedControlProperties
        {
            get
            {
                if (_embeddedControlProperties == null)
                {
                    _embeddedControlProperties = new Hashtable();
                }

                return _embeddedControlProperties;
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Font Font
        {
            get
            {
                return _font;
            }

            set
            {
                if (!Equals(_font, value))
                {
                    _font = value;
                    ChangedEvent?.Invoke(this, new ListViewChangedEventArgs(ListViewChangedTypes.SubItemChanged, null, null, this));
                }
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public bool ForceText
        {
            get
            {
                return _forceText;
            }

            set
            {
                if (_forceText != value)
                {
                    _forceText = value;
                    ChangedEvent?.Invoke(this, new ListViewChangedEventArgs(ListViewChangedTypes.SubItemChanged, null, null, null));
                }
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color ForeColor
        {
            get
            {
                return _foreColor;
            }

            set
            {
                if (_foreColor != value)
                {
                    _foreColor = value;
                    ChangedEvent?.Invoke(this, new ListViewChangedEventArgs(ListViewChangedTypes.SubItemChanged, null, null, this));
                }
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public HorizontalAlignment ImageAlignment
        {
            get
            {
                return _imageAlignment;
            }

            set
            {
                if (_imageAlignment != value)
                {
                    _imageAlignment = value;
                    ChangedEvent?.Invoke(this, new ListViewChangedEventArgs(ListViewChangedTypes.SubItemChanged, null, null, this));
                }
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public int ImageIndex
        {
            get
            {
                return _imageIndex;
            }

            set
            {
                if (_imageIndex != value)
                {
                    _imageIndex = value;
                    ChangedEvent?.Invoke(this, new ListViewChangedEventArgs(ListViewChangedTypes.SubItemChanged, null, null, null));
                }
            }
        }

        /// <summary>The last rectangle that text was drawn into.</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Rectangle LastCellRectangle
        {
            get
            {
                return _lastCellRectangle;
            }

            set
            {
                _lastCellRectangle = value;
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
            }
        }

        [Browsable(true)]
        [Category(PropertyCategory.Design)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string Name
        {
            get
            {
                return _name;
            }

            set
            {
                if (_name != value)
                {
                    _name = value;
                    ChangedEvent?.Invoke(this, new ListViewChangedEventArgs(ListViewChangedTypes.SubItemChanged, null, null, this));
                }
            }
        }

        /// <summary>The owner control.</summary>
        [Browsable(false)]
        public VisualListViewItem Owner
        {
            get
            {
                return _owner;
            }

            set
            {
                _owner = value;
            }
        }

        /// <summary>Indicates when an item is selected.</summary>
        [Browsable(false)]
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
                    _selected = value;
                    ChangedEvent?.Invoke(this, new ListViewChangedEventArgs(ListViewChangedTypes.ItemChanged, null, null, this));
                }
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string Text
        {
            get
            {
                if ((_listView != null) && (_listView.ActivatedEmbeddedControl != null))
                {
                    ILVEmbeddedControl _iLvEmbeddedControl = (ILVEmbeddedControl)_listView.ActivatedEmbeddedControl;
                    if ((_iLvEmbeddedControl != null) && (_iLvEmbeddedControl.SubItem == this))
                    {
                        Debug.WriteLine(_iLvEmbeddedControl.LVEmbeddedControlReturnText());
                        return _iLvEmbeddedControl.LVEmbeddedControlReturnText();
                    }
                }

                return _text;
            }

            set
            {
                if (_text != value)
                {
                    _text = value;
                    ChangedEvent?.Invoke(this, new ListViewChangedEventArgs(ListViewChangedTypes.SubItemChanged, null, null, this));
                }
            }
        }

        #endregion Public Properties

        #region Public Methods and Operators

        public override string ToString()
        {
            return GetType().Name + ": {" + _text + "}";
        }

        #endregion Public Methods and Operators
    }
}