#region License

// -----------------------------------------------------------------------------------------------------------
// 
// Name: VisualListView.cs
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
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Forms;

using VisualPlus.Collections.CollectionsBase;
using VisualPlus.Collections.CollectionsEditor;
using VisualPlus.Constants;
using VisualPlus.Delegates;
using VisualPlus.Designer;
using VisualPlus.Enumerators;
using VisualPlus.Events;
using VisualPlus.Extensibility;
using VisualPlus.Interfaces;
using VisualPlus.Localization;
using VisualPlus.Native;
using VisualPlus.Renders;
using VisualPlus.Structure;
using VisualPlus.Toolkit.Child;
using VisualPlus.Toolkit.VisualBase;
using VisualPlus.TypeConverters;
using VisualPlus.Utilities.Debugging;

#endregion

namespace VisualPlus.Toolkit.Controls.DataManagement
{
    /// <summary>The visual list view.</summary>
    [ClassInterface(ClassInterfaceType.AutoDispatch)]
    [ComVisible(true)]
    [DefaultEvent("SelectedIndexChanged")]
    [DefaultProperty("Items")]
    [Description("The Visual ListView")]
    [Designer(typeof(VisualListViewDesigner))]
    [ToolboxBitmap(typeof(VisualListView), "VisualListView.bmp")]
    [ToolboxItem(true)]
    public class VisualListView : VisualStyleBase, IThemeSupport
    {
        #region Fields

        private Control _activatedEmbeddedControl;
        private bool _allowColumnResize;
        private bool _alternatingColors;
        private bool _autoHeight;
        private bool _backgroundStretchToFit;
        private Border _border;
        private Color _colorAlternateBackground;
        private Color _colorGridColor;
        private ColorState _colorState;
        private ControlColorState _columnColorState;
        private int _columnIndex;
        private VisualListViewColumnCollection _columns;
        private IContainer _components;
        private LVControlStyles _controlStyle;
        private BorderStrip _cornerBox;
        private string _displayText;
        private Color _displayTextColor;
        private Font _displayTextFont;
        private bool _displayTextOnEmpty;
        private VisualListViewItem _focusedItem;
        private int _focusedItemIndex;
        private bool _fullRowSelect;
        private GridLines _gridLines;
        private GridLineStyle _gridLineStyle;
        private GridTypes _gridType;
        private int _headerHeight;
        private bool _headerVisible;
        private bool _headerWordWrap;
        private ManagedHScrollBar _horizontalScrollBar;
        private int _hotColumnIndex;
        private bool _hoverColumnTracking;
        private VisualListViewColumn _hoveredColumn;
        private VisualListViewItem _hoveredItem;
        private bool _hoverEvents;
        private int _hoverItemIndex;
        private bool _hoverItemTracking;
        private bool _hoverLive;
        private int _hoverTime;
        private Color _hoverTrackingColor;
        private ImageList _imageListColumns;
        private ImageList _imageListItems;
        private int _itemHeight;
        private VisualListViewItemCollection _items;
        private Color _itemSelectedColor;
        private Color _itemSelectedTextColor;
        private bool _itemWordWrap;
        private Point _lastHoverSpot;
        private int _lastSelectionIndex;
        private int _lastSubSelectionIndex;
        private ArrayList _liveControls;
        private int _maxHeight;
        private bool _multiSelect;
        private ArrayList _newLiveControls;
        private Point _pointColumnResizeAnchor;
        private int _resizeColumnNumber;
        private bool _selectable;
        private bool _showFocusRectangle;
        private SortTypes _sortType;
        private ListStates _state;
        private IntPtr _theme;
        private bool _themesAvailable;
        private Timer _timer;
        private bool _updating;
        private ManagedVScrollBar _verticalScrollBar;
        private int internalPadding;

        #endregion

        #region Constructors and Destructors

        /// <summary>Initializes a new instance of the <see cref="VisualListView" /> class.</summary>
        public VisualListView()
        {
            internalPadding = 2;
            _columnColorState = new ControlColorState();
            _liveControls = new ArrayList();
            _allowColumnResize = true;
            _autoHeight = true;
            _backgroundStretchToFit = true;
            _fullRowSelect = true;
            _headerVisible = true;
            _selectable = true;
            _newLiveControls = new ArrayList();
            _sortType = SortTypes.InsertionSort;
            _itemSelectedTextColor = Color.White;
            _lastHoverSpot = new Point(0, 0);
            _state = ListStates.None;
            _itemHeight = 18;
            _hoverTime = 1;
            _hoverItemIndex = -1;
            _hotColumnIndex = -1;
            _columnIndex = -1;
            _headerHeight = 22;
            _theme = IntPtr.Zero;
            _gridType = GridTypes.Normal;
            _gridLineStyle = GridLineStyle.Solid;
            _gridLines = GridLines.Both;
            _controlStyle = LVControlStyles.SuperFlat;
            _multiSelect = false;
            _hoverItemTracking = true;
            _hoverColumnTracking = true;
            _hoveredItem = null;
            _hoveredColumn = null;
            _displayTextOnEmpty = true;
            _displayText = "The list is empty.";
            _displayTextFont = DefaultFont;
            _showFocusRectangle = false;
            _focusedItem = null;
            _focusedItemIndex = -1;

            _imageListColumns = null;
            _imageListItems = null;

            _columns = new VisualListViewColumnCollection(this);
            _columns.ChangedEvent += Columns_Changed;

            _items = new VisualListViewItemCollection(this);
            _items.ChangedEvent += Items_Changed;

            InitializeComponent();

            if (!DesignMode)
            {
                if (AreThemesAvailable())
                {
                    _themesAvailable = true;
                }
                else
                {
                    _themesAvailable = false;
                }
            }

            TabStop = true;

            SetStyle(
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.ResizeRedraw |
                ControlStyles.UserPaint |
                ControlStyles.DoubleBuffer |
                ControlStyles.Selectable |
                ControlStyles.UserMouse,
                true);

            SuspendLayout();

            _border = new Border { Type = ShapeTypes.Rectangle };

            _horizontalScrollBar = new ManagedHScrollBar
                {
                    Anchor = AnchorStyles.None,
                    CausesValidation = false,
                    Location = new Point(24, 0),
                    MHeight = 16,
                    MWidth = 120,
                    Name = "hPanelScrollBar",
                    Size = new Size(120, 16),
                    Parent = this
                };
            _horizontalScrollBar.Scroll += OnScroll;
            _horizontalScrollBar.Scroll += HorizontalPanelScrollBar_Scroll;
            Controls.Add(_horizontalScrollBar);

            _verticalScrollBar = new ManagedVScrollBar
                {
                    Anchor = AnchorStyles.None,
                    CausesValidation = false,
                    Location = new Point(0, 12),
                    MHeight = 120,
                    MWidth = 16,
                    Name = "vPanelScrollBar",
                    Size = new Size(16, 120),
                    Parent = this
                };
            _verticalScrollBar.Scroll += OnScroll;
            _verticalScrollBar.Scroll += VerticalPanelScrollBar_Scroll;
            Controls.Add(_verticalScrollBar);

            _cornerBox = new BorderStrip { BackColor = SystemColors.Control, BorderType = BorderStrip.BorderTypes.Square, Visible = false, Parent = this };
            _cornerBox.BringToFront();

            _colorState = new ColorState { Enabled = ThemeManager.Theme.ColorPalette.ControlEnabled };

            Size = new Size(121, 97);
            UpdateTheme(ThemeManager.Theme);

            ResumeLayout(false);
        }

        #endregion

        #region Delegates

        [Category(EventCategory.PropertyChanged)]
        [Description(EventDescription.PropertyEventChanged)]
        public delegate void ClickedEventHandler(object source, ListViewClickEventArgs e);

        [Category(EventCategory.PropertyChanged)]
        [Description(EventDescription.PropertyEventChanged)]
        public delegate void HoverEventDelegate(object source, ListViewHoverEventArgs e);

        #endregion

        #region Public Events

        [Category(EventCategory.PropertyChanged)]
        [Description(EventDescription.PropertyEventChanged)]
        public event ListViewChangedEventHandler ColumnChangedEvent;

        [Category(EventCategory.PropertyChanged)]
        [Description(EventDescription.PropertyEventChanged)]
        public event ClickedEventHandler ColumnClickedEvent;

        [Category(EventCategory.PropertyChanged)]
        [Description(EventDescription.PropertyEventChanged)]
        public event HoverEventDelegate HoverEvent;

        [Category(EventCategory.PropertyChanged)]
        [Description(EventDescription.PropertyEventChanged)]
        public event ListViewChangedEventHandler ItemChangedEvent;

        [Category(EventCategory.PropertyChanged)]
        [Description(EventDescription.PropertyEventChanged)]
        public event ItemCheckEventHandler ItemCheck;

        [Category(EventCategory.PropertyChanged)]
        [Description(EventDescription.PropertyEventChanged)]
        public event VisualItemCheckedEventHandler ItemChecked;

        [Category(EventCategory.PropertyChanged)]
        [Description(EventDescription.PropertyEventChanged)]
        public event VisualItemCheckedEventHandler ItemMouseHover;

        [Category(EventCategory.PropertyChanged)]
        [Description(EventDescription.PropertyEventChanged)]
        public event ScrollEventHandler Scroll;

        [Category(EventCategory.PropertyChanged)]
        [Description(EventDescription.PropertyEventChanged)]
        public event ScrollEventHandler ScrollHorizontal
        {
            add
            {
                _horizontalScrollBar.Scroll += value;
            }

            remove
            {
                _horizontalScrollBar.Scroll -= value;
            }
        }

        [Category(EventCategory.PropertyChanged)]
        [Description(EventDescription.PropertyEventChanged)]
        public event ScrollEventHandler ScrollVertical
        {
            add
            {
                _verticalScrollBar.Scroll += value;
            }

            remove
            {
                _verticalScrollBar.Scroll -= value;
            }
        }

        [Category(EventCategory.PropertyChanged)]
        [Description(EventDescription.PropertyEventChanged)]
        public event ClickedEventHandler SelectedIndexChanged;

        #endregion

        #region Public Properties

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Control ActivatedEmbeddedControl
        {
            get
            {
                return _activatedEmbeddedControl;
            }

            set
            {
                _activatedEmbeddedControl = value;
            }
        }

        [Browsable(true)]
        [Category(PropertyCategory.Behavior)]
        [Description(PropertyDescription.Toggle)]
        [RefreshProperties(RefreshProperties.Repaint)]
        public bool AllowColumnResize
        {
            get
            {
                return _allowColumnResize;
            }

            set
            {
                _allowColumnResize = value;
            }
        }

        [Browsable(true)]
        [Category(PropertyCategory.Behavior)]
        [Description(PropertyDescription.Color)]
        [RefreshProperties(RefreshProperties.Repaint)]
        public Color AlternateBackground
        {
            get
            {
                return _colorAlternateBackground;
            }

            set
            {
                _colorAlternateBackground = value;

                if (DesignMode && (Parent != null))
                {
                    Parent.Invalidate(true);
                }
            }
        }

        [Browsable(true)]
        [Category(PropertyCategory.Behavior)]
        [Description(PropertyDescription.Toggle)]
        [RefreshProperties(RefreshProperties.Repaint)]
        public bool AlternatingColors
        {
            get
            {
                return _alternatingColors;
            }

            set
            {
                _alternatingColors = value;

                if (DesignMode && (Parent != null))
                {
                    Parent.Invalidate(true);
                }
            }
        }

        [Browsable(true)]
        [Category(PropertyCategory.Behavior)]
        [Description(PropertyDescription.Toggle)]
        [RefreshProperties(RefreshProperties.Repaint)]
        public bool AutoHeight
        {
            get
            {
                return _autoHeight;
            }

            set
            {
                _autoHeight = value;
                if (DesignMode && (Parent != null))
                {
                    Parent.Invalidate(true);
                }
            }
        }

        [TypeConverter(typeof(VisualSettingsTypeConverter))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Category(PropertyCategory.Appearance)]
        public ColorState BackColorState
        {
            get
            {
                return _colorState;
            }

            set
            {
                if (value == _colorState)
                {
                    return;
                }

                _colorState = value;

                if (DesignMode && (Parent != null))
                {
                    Parent.Invalidate(true);
                }
            }
        }

        [Browsable(true)]
        [Category(EventCategory.Behavior)]
        [Description(PropertyDescription.Toggle)]
        [RefreshProperties(RefreshProperties.Repaint)]
        public bool BackgroundStretchToFit
        {
            get
            {
                return _backgroundStretchToFit;
            }

            set
            {
                _backgroundStretchToFit = value;
            }
        }

        [TypeConverter(typeof(VisualSettingsTypeConverter))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Category(PropertyCategory.Appearance)]
        public Border Border
        {
            get
            {
                return _border;
            }

            set
            {
                if (value == _border)
                {
                    return;
                }

                _border = value;

                if (DesignMode && (Parent != null))
                {
                    Parent.Invalidate(true);
                }
            }
        }

        /// <summary>The cell padding size.</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int CellPaddingSize
        {
            get
            {
                return 2;
            }
        }

        /// <summary>Gets the indexes of the currently checked items in the control.</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public List<int> CheckedIndicies
        {
            get
            {
                var _checkedItems = new List<int>();

                for (var i = 0; i < _items.Count; i++)
                {
                    if (_items[i].SubItems[0].Checked)
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
        public List<VisualListViewItem> CheckedItems
        {
            get
            {
                var _checkedItems = new List<VisualListViewItem>();

                foreach (VisualListViewItem _item in _items)
                {
                    if (_item.SubItems[0].Checked)
                    {
                        _checkedItems.Add(_item);
                    }
                }

                return _checkedItems;
            }
        }

        [Category(PropertyCategory.Appearance)]
        [Description(PropertyDescription.Color)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [TypeConverter(typeof(VisualSettingsTypeConverter))]
        public ControlColorState ColumnColorState
        {
            get
            {
                return _columnColorState;
            }

            set
            {
                if (value == _columnColorState)
                {
                    return;
                }

                _columnColorState = value;

                if (DesignMode && (Parent != null))
                {
                    Parent.Invalidate(true);
                }
            }
        }

        /// <summary>Gets the current column index.</summary>
        [Browsable(false)]
        public int ColumnIndex
        {
            get
            {
                return _columnIndex;
            }
        }

        [Browsable(true)]
        [Category(PropertyCategory.Behavior)]
        [Description("The columns shown in Details view.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Editor(typeof(VisualListViewColumnCollectionEditor), typeof(UITypeEditor))]
        public VisualListViewColumnCollection Columns
        {
            get
            {
                return _columns;
            }
        }

        [Browsable(true)]
        [Category(EventCategory.Behavior)]
        [Description("The control theme.")]
        [RefreshProperties(RefreshProperties.Repaint)]
        public LVControlStyles ControlStyle
        {
            get
            {
                return _controlStyle;
            }

            set
            {
                _controlStyle = value;

                if (DesignMode && (Parent != null))
                {
                    Parent.Invalidate(true);
                }
            }
        }

        /// <summary>Gets or sets the corner box properties.</summary>
        [Browsable(false)]
        public BorderStrip CornerBox
        {
            get
            {
                return _cornerBox;
            }

            set
            {
                if (value == _cornerBox)
                {
                    return;
                }

                _cornerBox = value;

                if (DesignMode && (Parent != null))
                {
                    Parent.Invalidate(true);
                }
            }
        }

        [Browsable(true)]
        [Category(PropertyCategory.Behavior)]
        [Description(PropertyDescription.Color)]
        [RefreshProperties(RefreshProperties.Repaint)]
        public Color CornerBoxColor
        {
            get
            {
                return _cornerBox.BackColor;
            }

            set
            {
                if (value == _cornerBox.BackColor)
                {
                    return;
                }

                _cornerBox.BackColor = value;

                if (DesignMode && (Parent != null))
                {
                    Parent.Invalidate(true);
                }
            }
        }

        /// <summary>Gets the number of elements contained in the <see cref="CollectionBase" /> instance.</summary>
        [Browsable(false)]
        [DefaultValue(0)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int Count
        {
            get
            {
                return _items.Count;
            }
        }

        [Browsable(true)]
        [Category(PropertyCategory.Appearance)]
        [Description(PropertyDescription.Text)]
        public string DisplayText
        {
            get
            {
                return _displayText;
            }

            set
            {
                _displayText = value;
                Invalidate();
            }
        }

        [Browsable(true)]
        [Category(PropertyCategory.Appearance)]
        [Description(PropertyDescription.Color)]
        public Color DisplayTextColor
        {
            get
            {
                return _displayTextColor;
            }

            set
            {
                _displayTextColor = value;
                Invalidate();
            }
        }

        [Browsable(true)]
        [Category(PropertyCategory.Appearance)]
        [Description(PropertyDescription.Font)]
        public Font DisplayTextFont
        {
            get
            {
                return _displayTextFont;
            }

            set
            {
                _displayTextFont = value;
                Invalidate();
            }
        }

        [Browsable(true)]
        [Category(PropertyCategory.Appearance)]
        [Description(PropertyDescription.Toggle)]
        public bool DisplayTextOnEmpty
        {
            get
            {
                return _displayTextOnEmpty;
            }

            set
            {
                _displayTextOnEmpty = value;
                Invalidate();
            }
        }

        /// <summary>The currently focused item.</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public VisualListViewItem FocusedItem
        {
            get
            {
                // Verify the focused item exists.
                if ((_focusedItem != null) && (_items.FindItemIndex(_focusedItem) < 0))
                {
                    _focusedItem = null; // even though there is a focused item, it doesn't actually exist anymore
                }

                return _focusedItem;
            }

            set
            {
                if (_focusedItem != value)
                {
                    _focusedItem = value;
                    if (!DesignMode)
                    {
                        Invalidate(true);
                    }

                    SelectedIndexChanged?.Invoke(this, new ListViewClickEventArgs(_items.FindItemIndex(value), -1)); // never a column sent with selection index change
                }
            }
        }

        /// <summary>Gets the current focused item index.</summary>
        [Browsable(false)]
        public int FocusedItemIndex
        {
            get
            {
                return _focusedItemIndex;
            }
        }

        [Browsable(true)]
        [Category(PropertyCategory.Behavior)]
        [Description(PropertyDescription.Toggle)]
        [RefreshProperties(RefreshProperties.Repaint)]
        public bool FullRowSelect
        {
            get
            {
                return _fullRowSelect;
            }

            set
            {
                _fullRowSelect = value;
            }
        }

        [Browsable(true)]
        [Category(PropertyCategory.Appearance)]
        [Description(PropertyDescription.Color)]
        [RefreshProperties(RefreshProperties.Repaint)]
        public Color GridColor
        {
            get
            {
                return _colorGridColor;
            }

            set
            {
                _colorGridColor = value;

                if (DesignMode && (Parent != null))
                {
                    Parent.Invalidate(true);
                }
            }
        }

        [Browsable(true)]
        [Category(PropertyCategory.Behavior)]
        [Description("Whether or not to draw gridlines.")]
        [RefreshProperties(RefreshProperties.Repaint)]
        public GridLines GridLines
        {
            get
            {
                return _gridLines;
            }

            set
            {
                _gridLines = value;

                if (DesignMode && (Parent != null))
                {
                    Parent.Invalidate(true);
                }
            }
        }

        [Browsable(true)]
        [Category(PropertyCategory.Behavior)]
        [Description("Whether or not to draw gridlines style.")]
        [RefreshProperties(RefreshProperties.Repaint)]
        public GridLineStyle GridLineStyle
        {
            get
            {
                return _gridLineStyle;
            }

            set
            {
                _gridLineStyle = value;

                if (DesignMode && (Parent != null))
                {
                    // Invalidate();
                    Parent.Invalidate(true);
                }
            }
        }

        [Browsable(true)]
        [Category(PropertyCategory.Behavior)]
        [Description("Whether or not to draw grid types.")]
        [RefreshProperties(RefreshProperties.Repaint)]
        public GridTypes GridTypes
        {
            get
            {
                return _gridType;
            }

            set
            {
                _gridType = value;

                if (DesignMode && (Parent != null))
                {
                    Parent.Invalidate(true);
                }
            }
        }

        [Browsable(true)]
        [Category(PropertyCategory.Appearance)]
        [Description(PropertyDescription.Size)]
        [RefreshProperties(RefreshProperties.Repaint)]
        public int HeaderHeight
        {
            get
            {
                if (HeaderVisible)
                {
                    return _headerHeight;
                }
                else
                {
                    return 0;
                }
            }

            set
            {
                _headerHeight = value;
                if (DesignMode && (Parent != null))
                {
                    Parent.Invalidate(true);
                }
            }
        }

        /// <summary>The header rectangle.</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Rectangle HeaderRectangle
        {
            get
            {
                return new Rectangle(_border.Thickness, _border.Thickness, Width - (_border.Thickness * 2), HeaderHeight);
            }
        }

        [RefreshProperties(RefreshProperties.Repaint)]
        [Description(PropertyDescription.Toggle)]
        [Category(PropertyCategory.Behavior)]
        [Browsable(true)]
        public bool HeaderVisible
        {
            get
            {
                return _headerVisible;
            }

            set
            {
                _headerVisible = value;
                if (DesignMode && (Parent != null))
                {
                    Parent.Invalidate(true);
                }
            }
        }

        [RefreshProperties(RefreshProperties.Repaint)]
        [Description(PropertyDescription.Toggle)]
        [Category(PropertyCategory.Behavior)]
        [Browsable(true)]
        public bool HeaderWordWrap
        {
            get
            {
                return _headerWordWrap;
            }

            set
            {
                _headerWordWrap = value;

                if (DesignMode && (Parent != null))
                {
                    Parent.Invalidate(true);
                }
            }
        }

        /// <summary>The horizontal scroll bar control.</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ManagedHScrollBar HorizontalScrollBar
        {
            get
            {
                return _horizontalScrollBar;
            }

            set
            {
                _horizontalScrollBar = value;
            }
        }

        /// <summary>The current hovering column.</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int HoverColumnIndex
        {
            get
            {
                return _hotColumnIndex;
            }

            set
            {
                if (_hoverColumnTracking)
                {
                    if (_hotColumnIndex != value)
                    {
                        _hoverItemIndex = -1;
                        _hotColumnIndex = value;

                        if (!DesignMode)
                        {
                            Invalidate(true);
                        }
                    }
                }
            }
        }

        [Browsable(true)]
        [Category(EventCategory.Behavior)]
        [Description(PropertyDescription.Toggle)]
        public bool HoverColumnTracking
        {
            get
            {
                return _hoverColumnTracking;
            }

            set
            {
                _hoverColumnTracking = value;
            }
        }

        /// <summary>Gets the currently hovered column.</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public VisualListViewColumn HoveredColumn
        {
            get
            {
                return _hoveredColumn;
            }
        }

        /// <summary>Gets the currently hovered item.</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public VisualListViewItem HoveredItem
        {
            get
            {
                return _hoveredItem;
            }
        }

        [Description("Enabling hover events slows the control some but allows you to be informed when a user has hovered over an item.")]
        [Category(EventCategory.Behavior)]
        [Browsable(true)]
        public bool HoverEvents
        {
            get
            {
                return _hoverEvents;
            }

            set
            {
                _hoverEvents = value;

                if (!DesignMode)
                {
                    if (_hoverEvents)
                    {
                        // turn the events off, so we need to add the events
                        _timer = new Timer();
                        _timer.Interval = _hoverTime * 1000; // convert to seconds
                        _timer.Tick += HoverTimer_TimerTick;
                        _timer.Start();
                    }
                    else if (_timer != null)
                    {
                        // turn the events off
                        _timer.Stop();
                        _timer = null;
                    }
                }
            }
        }

        /// <summary>The currently hovered item index.</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int HoverItemIndex
        {
            get
            {
                return _hoverItemIndex;
            }

            set
            {
                if (_hoverItemTracking)
                {
                    if (_hoverItemIndex != value)
                    {
                        _hotColumnIndex = -1;
                        _hoverItemIndex = value;

                        Invalidate(true);
                    }
                }
            }
        }

        [Browsable(true)]
        [Category(EventCategory.Behavior)]
        [Description(PropertyDescription.Toggle)]
        public bool HoverItemTracking
        {
            get
            {
                return _hoverItemTracking;
            }

            set
            {
                _hoverItemTracking = value;
            }
        }

        [Browsable(true)]
        [Category(PropertyCategory.Behavior)]
        [Description("Amount of time in seconds a user hovers before hover event is fired. Can NOT be zero.")]
        public int HoverTime
        {
            get
            {
                return _hoverTime;
            }

            set
            {
                if (_hoverTime < 1)
                {
                    _hoverTime = 1;
                }
                else
                {
                    _hoverTime = value;
                }
            }
        }

        [Browsable(true)]
        [Category(EventCategory.Appearance)]
        [Description(PropertyDescription.Color)]
        public Color HoverTrackingColor
        {
            get
            {
                return _hoverTrackingColor;
            }

            set
            {
                _hoverTrackingColor = value;
            }
        }

        [Browsable(true)]
        [Category(EventCategory.Behavior)]
        [Description("ImageList to be used in listview.")]
        [RefreshProperties(RefreshProperties.Repaint)]
        public ImageList ImageListColumns
        {
            get
            {
                return _imageListColumns;
            }

            set
            {
                _imageListColumns = value;
            }
        }

        [Browsable(true)]
        [Category(EventCategory.Behavior)]
        [Description("ImageList to be used in listview.")]
        [RefreshProperties(RefreshProperties.Repaint)]
        public ImageList ImageListItems
        {
            get
            {
                return _imageListItems;
            }

            set
            {
                _imageListItems = value;
            }
        }

        [Browsable(true)]
        [Category(PropertyCategory.Appearance)]
        [Description(PropertyDescription.Size)]
        [RefreshProperties(RefreshProperties.Repaint)]
        public int ItemHeight
        {
            get
            {
                return _itemHeight;
            }

            set
            {
                _itemHeight = value;
                if (DesignMode && (Parent != null))
                {
                    Parent.Invalidate(true);
                }
            }
        }

        [Category(PropertyCategory.Data)]
        [Description("The items in the collection.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Editor(typeof(CollectionEditor), typeof(UITypeEditor))]
        [Browsable(true)]
        public VisualListViewItemCollection Items
        {
            get
            {
                return _items;
            }
        }

        [Browsable(true)]
        [Category(PropertyCategory.Appearance)]
        [Description(PropertyDescription.Color)]
        [RefreshProperties(RefreshProperties.Repaint)]
        public Color ItemSelectedColor
        {
            get
            {
                return _itemSelectedColor;
            }

            set
            {
                _itemSelectedColor = value;
            }
        }

        [Browsable(true)]
        [Category(PropertyCategory.Appearance)]
        [Description(PropertyDescription.Color)]
        [RefreshProperties(RefreshProperties.Repaint)]
        public Color ItemSelectedTextColor
        {
            get
            {
                return _itemSelectedTextColor;
            }

            set
            {
                _itemSelectedTextColor = value;
            }
        }

        [RefreshProperties(RefreshProperties.Repaint)]
        [Description(PropertyDescription.Toggle)]
        [Category(PropertyCategory.Behavior)]
        [Browsable(true)]
        public bool ItemWordWrap
        {
            get
            {
                return _itemWordWrap;
            }

            set
            {
                _itemWordWrap = value;

                if (DesignMode && (Parent != null))
                {
                    Parent.Invalidate(true);
                }
            }
        }

        [Description(PropertyDescription.Size)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public int MaxHeight
        {
            get
            {
                return _maxHeight;
            }

            set
            {
                if (value > _maxHeight)
                {
                    _maxHeight = value;
                    if (AutoHeight)
                    {
                        ItemHeight = MaxHeight;

                        if (!DesignMode)
                        {
                            Invalidate(true);
                        }
                    }
                }
            }
        }

        [RefreshProperties(RefreshProperties.Repaint)]
        [Description(PropertyDescription.Toggle)]
        [Category(PropertyCategory.Behavior)]
        [Browsable(true)]
        public bool MultiSelect
        {
            get
            {
                return _multiSelect;
            }

            set
            {
                _multiSelect = value;
            }
        }

        /// <summary>The rectangle of the client inside the parent control.</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Rectangle RowsClientRectangle
        {
            get
            {
                // The size of the header and the top border.
                int _yHeaderHeight = HeaderHeight + _border.Thickness;

                // Total header height size including borders.
                int _totalHeaderHeight = Height - HeaderHeight - (_border.Thickness * 2);

                return new Rectangle(_border.Thickness, _yHeaderHeight, Width - (_border.Thickness * 2), _totalHeaderHeight);
            }
        }

        /// <summary>The inner rectangle of the client inside parent control including scroll bars.</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Rectangle RowsInnerClientRect
        {
            get
            {
                // The horizontal bar crosses vertical plane and vice versa.
                Rectangle _innerRectangle = RowsClientRectangle;

                // The width of the vertical scroll bar.
                _innerRectangle.Width -= _verticalScrollBar.MWidth;

                // The height of the horizontal scroll bar.
                _innerRectangle.Height -= _horizontalScrollBar.MHeight;

                if (_innerRectangle.Width < 0)
                {
                    _innerRectangle.Width = 0;
                }

                if (_innerRectangle.Height < 0)
                {
                    _innerRectangle.Height = 0;
                }

                return _innerRectangle;
            }
        }

        /// <summary>The full sized rectangle of all the columns total width.</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Rectangle RowsRectangle
        {
            get
            {
                Rectangle _rowsRect = new Rectangle { X = -_horizontalScrollBar.Value + _border.Thickness, Y = HeaderHeight + _border.Thickness, Width = _columns.Width, Height = RowsVisible * _itemHeight };

                return _rowsRect;
            }
        }

        /// <summary>The amount of rows currently visible in the <see cref="RowsInnerClientRect" />.</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int RowsVisible
        {
            get
            {
                return RowsInnerClientRect.Height / ItemHeight;
            }
        }

        [Browsable(true)]
        [Category(EventCategory.Behavior)]
        [Description(PropertyDescription.Toggle)]
        [RefreshProperties(RefreshProperties.Repaint)]
        public bool Selectable
        {
            get
            {
                return _selectable;
            }

            set
            {
                _selectable = value;
            }
        }

        /// <summary>Gets the currently selected indicies in the control.</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public List<int> SelectedIndicies
        {
            get
            {
                return _items.SelectedIndicies;
            }
        }

        /// <summary>Gets the currently selected items in the control.</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public List<VisualListViewItem> SelectedItems
        {
            get
            {
                return _items.SelectedItems;
            }
        }

        [Browsable(true)]
        [Category(PropertyCategory.Behavior)]
        [Description(PropertyDescription.Toggle)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool ShowFocusRectangle
        {
            get
            {
                return _showFocusRectangle;
            }

            set
            {
                _showFocusRectangle = value;
            }
        }

        [Browsable(true)]
        [Category(EventCategory.Behavior)]
        [Description("Type of sorting algorithm used.")]
        [RefreshProperties(RefreshProperties.Repaint)]
        public SortTypes SortType
        {
            get
            {
                return _sortType;
            }

            set
            {
                _sortType = value;
            }
        }

        [Browsable(false)]
        [Description(PropertyDescription.Toggle)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool ThemesAvailable
        {
            get
            {
                return _themesAvailable;
            }
        }

        /// <summary>Gets or sets the first visible item in the control.</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public VisualListViewItem TopItem
        {
            get
            {
                if (_items.Count <= 0)
                {
                    return null;
                }
                else
                {
                    return _items[0];
                }
            }

            set
            {
                _items[0] = value;
            }
        }

        /// <summary>Retrieve the total height of all the rows combined.</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int TotalRowsHeight
        {
            get
            {
                return ItemHeight * _items.Count;
            }
        }

        /// <summary>The vertical scroll bar control.</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ManagedVScrollBar VerticalScrollBar
        {
            get
            {
                return _verticalScrollBar;
            }

            set
            {
                _verticalScrollBar = value;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>Resizes the width of the given column as indicated by the resize style.</summary>
        /// <param name="columnIndex">The zero-based index of the column to resize.</param>
        /// <param name="headerAutoResize">One of the <see cref="ColumnHeaderAutoResizeStyle" /> values.</param>
        public void AutoResizeColumn(int columnIndex, ColumnHeaderAutoResizeStyle headerAutoResize)
        {
            if (!IsHandleCreated)
            {
                CreateHandle();
            }

            SetColumnWidth(columnIndex, headerAutoResize);
        }

        /// <summary>Resizes the width of the columns as indicated by the resize style.</summary>
        /// <param name="headerAutoResize">One of the <see cref="ColumnHeaderAutoResizeStyle" /> values.</param>
        public void AutoResizeColumns(ColumnHeaderAutoResizeStyle headerAutoResize)
        {
            if (!IsHandleCreated)
            {
                CreateHandle();
            }

            UpdateColumnWidths(headerAutoResize);
        }

        /// <summary>Ask paint to relax.</summary>
        public void BeginUpdate()
        {
            _updating = true;
        }

        /// <summary>Column has changed, fire event.</summary>
        /// <param name="source">The source.</param>
        /// <param name="e">The event args.</param>
        public void Columns_Changed(object source, ListViewChangedEventArgs e)
        {
            if (e.ChangedType != ListViewChangedTypes.ColumnStateChanged)
            {
                DestroyActivatedEmbedded();
            }

            ColumnChangedEvent?.Invoke(this, e);

            Invalidate();
        }

        /// <summary>Tell paint to start worrying about updates again and repaint while your at it.</summary>
        public void EndUpdate()
        {
            _updating = false;
            Invalidate();
        }

        /// <summary>Get return the X starting point of a particular column.</summary>
        /// <param name="column">The column.</param>
        /// <returns>The <see cref="int" />.</returns>
        public int GetColumnScreenX(int column)
        {
            // ConsoleEx.WriteDebug("Get Column Screen X");
            if (column >= Columns.Count)
            {
                return 0;
            }

            int nCurrentX = -_horizontalScrollBar.Value; // Offset the starting point by the current scroll point.
            int nColIndex = 0;
            foreach (VisualListViewColumn col in Columns)
            {
                if (nColIndex >= column)
                {
                    return nCurrentX;
                }

                nColIndex++;
                nCurrentX += col.Width;
            }

            return 0; // Should never reach.
        }

        /// <summary>
        ///     Interpret mouse coordinates, Do NOT put anything functional in this routine. It is ONLY for analyzing the
        ///     mouse coordinates.
        /// </summary>
        /// <param name="screenX">The screen x.</param>
        /// <param name="screenY">The screen y.</param>
        /// <param name="listRegion">The list region.</param>
        /// <param name="cellX">The cell x.</param>
        /// <param name="cellY">The cell y.</param>
        /// <param name="itemIndex">The item.</param>
        /// <param name="columnIndex">The column.</param>
        /// <param name="listStates">The state.</param>
        public void InterpretCoordinates(int screenX, int screenY, out ListViewRegion listRegion, out int cellX, out int cellY, out int itemIndex, out int columnIndex, out ListStates listStates)
        {
            listStates = ListStates.None;
            columnIndex = 0;
            itemIndex = 0;
            cellX = 0;
            cellY = 0;

            listRegion = ListViewRegion.NonClient;

            // Calculate horizontal subitem
            int _currentX = -_horizontalScrollBar.Value; // offset the starting point by the current scroll point

            for (columnIndex = 0; columnIndex < _columns.Count; columnIndex++)
            {
                VisualListViewColumn _column = _columns[columnIndex];

                // Find the inner X for the cell
                cellX = screenX - _currentX;

                if ((screenX > _currentX) && (screenX < (_currentX + _column.Width) - ListViewConstants.RESIZE_ARROW_PADDING))
                {
                    listStates = ListStates.ColumnSelect;
                    break;
                }

                if ((screenX >= (_currentX + _column.Width) - ListViewConstants.RESIZE_ARROW_PADDING) && (screenX <= _currentX + _column.Width + ListViewConstants.RESIZE_ARROW_PADDING))
                {
                    // Check see if this is a 0 length column (which we skip to next on) or if this is the last column (which we can't skip)
                    if ((columnIndex + 1 == _columns.Count) || (_columns[columnIndex + 1].Width != 0))
                    {
                        if (AllowColumnResize)
                        {
                            listStates = ListStates.ColumnResizing;
                        }

                        return;
                    }
                }

                _currentX += _column.Width;
            }

            if ((screenY >= RowsInnerClientRect.Y) && (screenY < RowsInnerClientRect.Bottom))
            {
                // We are in the client area
                listRegion = ListViewRegion.Client;

                _columns.ClearHotStates();
                _hotColumnIndex = -1;

                itemIndex = ((screenY - RowsInnerClientRect.Y) / ItemHeight) + _verticalScrollBar.Value;
                HoverItemIndex = itemIndex;

                // Get inner cell Y
                cellY = (screenY - RowsInnerClientRect.Y) % ItemHeight;

                if ((itemIndex >= _items.Count) || (itemIndex > _verticalScrollBar.Value + RowsVisible))
                {
                    listStates = ListStates.None;
                    listRegion = ListViewRegion.NonClient;
                }
                else
                {
                    listStates = ListStates.Selecting;

                    // Handle where FullRowSelect is OFF and we click on the second part of a spanned column
                    for (var subIndex = 0; subIndex < Columns.Count; subIndex++)
                    {
                        if (subIndex >= columnIndex)
                        {
                            columnIndex = subIndex;
                            return;
                        }
                    }
                }
            }
            else
            {
                if ((screenY >= HeaderRectangle.Y) && (screenY < HeaderRectangle.Bottom))
                {
                    listRegion = ListViewRegion.Header;

                    // In the header.
                    _hoverItemIndex = -1;
                    _hotColumnIndex = columnIndex;

                    if ((columnIndex > -1) && (columnIndex < Columns.Count) && !Columns.AnyPressed())
                    {
                        if (_columns[columnIndex].State == ColumnStates.None)
                        {
                            _columns.ClearHotStates();
                            _columns[columnIndex].State = ColumnStates.Hover;
                        }
                    }
                }
            }
        }

        /// <summary>
        ///     This is an OPTIMIZED routine to see if an item is visible.
        ///     The other method of just checking against the item index was slow because it had to walk the entire list, which
        ///     would massively slow down the control when large numbers of items were added.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>The <see cref="bool" />.</returns>
        public bool IsItemVisible(VisualListViewItem item)
        {
            int _itemIndex = _items.FindItemIndex(item);
            if ((_itemIndex >= _verticalScrollBar.Value) && (_itemIndex < _verticalScrollBar.Value + RowsVisible))
            {
                return true;
            }

            return false;
        }

        public override bool PreProcessMessage(ref Message msg)
        {
            if (msg.Msg == ListViewConstants.WM_KEYDOWN)
            {
                Keys _keyCode = (Keys)(int)msg.WParam; // this should turn the key data off because it will match selected keys to ORA them off

                if (_keyCode == Keys.Return)
                {
                    DestroyActivatedEmbedded();
                    return true;
                }

                Debug.WriteLine(_keyCode.ToString());

                if ((_focusedItem != null) && (Count > 0) && Selectable)
                {
                    int _itemIndex = _items.FindItemIndex(_focusedItem);
                    int _previousIndex = _itemIndex;

                    if (_itemIndex < 0)
                    {
                        // This can't move.
                        return true;
                    }

                    if ((_keyCode == Keys.A) && ((ModifierKeys & Keys.Control) == Keys.Control))
                    {
                        for (var index = 0; index < _items.Count; index++)
                        {
                            _items[index].Selected = true;
                        }

                        return base.PreProcessMessage(ref msg);
                    }

                    if (_keyCode == Keys.Escape)
                    {
                        // Clear selections.
                        _items.ClearSelection();
                        _focusedItemIndex = -1;
                        _focusedItem = null;

                        return base.PreProcessMessage(ref msg);
                    }

                    if (_keyCode == Keys.Down)
                    {
                        // Could be a switch
                        _itemIndex++;
                    }
                    else if (_keyCode == Keys.Up)
                    {
                        _itemIndex--;
                    }
                    else if (_keyCode == Keys.PageDown)
                    {
                        _itemIndex += RowsVisible;
                    }
                    else if (_keyCode == Keys.PageUp)
                    {
                        _itemIndex -= RowsVisible;
                    }
                    else if (_keyCode == Keys.Home)
                    {
                        _itemIndex = 0;
                    }
                    else if (_keyCode == Keys.End)
                    {
                        _itemIndex = Count - 1;
                    }
                    else if (_keyCode == Keys.Space)
                    {
                        if (!_multiSelect)
                        {
                            _items.ClearSelection(_items[_itemIndex]);
                        }

                        _items[_itemIndex].Selected = !_items[_itemIndex].Selected;

                        return base.PreProcessMessage(ref msg);
                    }
                    else
                    {
                        return base.PreProcessMessage(ref msg);
                    }

                    // Check the bounds.
                    if (_itemIndex > Count - 1)
                    {
                        _itemIndex = Count - 1;
                    }

                    if (_itemIndex < 0)
                    {
                        _itemIndex = 0;
                    }

                    // Move view - Need to move end -1 to take into account 0 based index.
                    if (_itemIndex < _verticalScrollBar.Value)
                    {
                        // Its out of viewable, move the surface
                        _verticalScrollBar.Value = _itemIndex;
                    }

                    if (_itemIndex > _verticalScrollBar.Value + (RowsVisible - 1))
                    {
                        _verticalScrollBar.Value = _itemIndex - (RowsVisible - 1);
                    }

                    if (_previousIndex != _itemIndex)
                    {
                        if (((ModifierKeys & Keys.Control) != Keys.Control) && ((ModifierKeys & Keys.Shift) != Keys.Shift))
                        {
                            // No control no shift
                            _lastSelectionIndex = _itemIndex;
                            _items[_itemIndex].Selected = true;
                            _items.ClearSelection(_items[_itemIndex]);
                        }
                        else if ((ModifierKeys & Keys.Shift) == Keys.Shift)
                        {
                            // Shift only
                            _items.ClearSelection();

                            // Gotta catch when the multi select is NOT set
                            if (!_multiSelect)
                            {
                                _items[_itemIndex].Selected = !_items[_itemIndex].Selected;
                            }
                            else
                            {
                                if (_lastSelectionIndex >= 0)
                                {
                                    // ie, non negative so that we have a starting point
                                    int index = _lastSelectionIndex;
                                    do
                                    {
                                        _items[index].Selected = true;
                                        if (index > _itemIndex)
                                        {
                                            index--;
                                        }

                                        if (index < _itemIndex)
                                        {
                                            index++;
                                        }
                                    }
                                    while (index != _itemIndex);

                                    _items[index].Selected = true;
                                }
                            }
                        }
                        else
                        {
                            // Control only
                            _lastSelectionIndex = _itemIndex;
                        }

                        // Bypass FocusedItem property, we always want to invalidate from this point
                        _focusedItemIndex = _itemIndex;
                        FocusedItem = _items[_itemIndex];
                    }
                }
                else
                {
                    // Only if non selectable
                    int _moveIndex = _verticalScrollBar.Value;

                    if (_keyCode == Keys.Down)
                    {
                        // Could be a switch
                        _moveIndex++;
                    }
                    else if (_keyCode == Keys.Up)
                    {
                        _moveIndex--;
                    }
                    else if (_keyCode == Keys.PageDown)
                    {
                        _moveIndex += RowsVisible;
                    }
                    else if (_keyCode == Keys.PageUp)
                    {
                        _moveIndex -= RowsVisible;
                    }
                    else if (_keyCode == Keys.Home)
                    {
                        _moveIndex = 0;
                    }
                    else if (_keyCode == Keys.End)
                    {
                        _moveIndex = Count - RowsVisible;
                    }
                    else
                    {
                        return base.PreProcessMessage(ref msg);
                    }

                    if (_moveIndex > Count - RowsVisible)
                    {
                        _moveIndex = Count - RowsVisible;
                    }

                    if (_moveIndex < 0)
                    {
                        _moveIndex = 0;
                    }

                    if (_verticalScrollBar.Value != _moveIndex)
                    {
                        _verticalScrollBar.Value = _moveIndex;

                        Invalidate();
                    }
                }
            }
            else
            {
                return base.PreProcessMessage(ref msg);
            }

            return true;
        }

        /// <summary>Resizes the width of the given column as indicated by the resize style.</summary>
        /// <param name="columnIndex">The zero-based index of the column to resize.</param>
        /// <param name="width">The width of the column.</param>
        public void SetColumnWidth(int columnIndex, int width)
        {
            if (IsHandleCreated)
            {
                _columns[columnIndex].Width = width;
            }
        }

        /// <summary>Sort a column. Set to virtual so you can write your own sorting.</summary>
        /// <param name="column">The column.</param>
        public virtual void SortColumn(int column)
        {
            if (Count < 2)
            {
                // nothing to sort
                return;
            }

            if (SortType == SortTypes.InsertionSort)
            {
                LVQuickSort sorter = new LVQuickSort { NumericCompare = _columns[column].NumericSort, SortDirection = _columns[column].LastSortState, SortColumn = column };

                sorter.LVInsertionSort(Items, 0, _items.Count - 1);
            }
            else if (SortType == SortTypes.MergeSort)
            {
                // this.SortIndex = nColumn;
                LVMergeSort mergeSort = new LVMergeSort { NumericCompare = _columns[column].NumericSort, SortDirection = _columns[column].LastSortState, SortColumn = column };

                mergeSort.Sort(Items, 0, _items.Count - 1);
            }
            else if (SortType == SortTypes.QuickSort)
            {
                LVQuickSort sorter = new LVQuickSort { NumericCompare = _columns[column].NumericSort, SortDirection = _columns[column].LastSortState, SortColumn = column };

                sorter.Sort(Items); // .QuickSort( Items, 0, Items.Count-1 );
            }

            if (_columns[column].LastSortState == SortDirections.Descending)
            {
                _columns[column].LastSortState = SortDirections.Ascending;
            }
            else
            {
                _columns[column].LastSortState = SortDirections.Descending;
            }

            // Items.Sort();
        }

        public void UpdateTheme(Theme theme)
        {
            try
            {
                _border.Color = theme.ColorPalette.BorderNormal;
                _border.HoverColor = theme.ColorPalette.BorderHover;

                ForeColor = theme.ColorPalette.TextEnabled;
                TextStyle.Enabled = theme.ColorPalette.TextEnabled;
                TextStyle.Disabled = theme.ColorPalette.TextDisabled;

                // Font = theme.ColorPalette.Font;
                _colorAlternateBackground = theme.ColorPalette.ItemAlternate;
                _colorGridColor = theme.ColorPalette.BorderNormal;

                _columnColorState.Enabled = theme.ColorPalette.ColumnHeader;
                _columnColorState.Disabled = theme.ColorPalette.ColumnHeader;
                _columnColorState.Hover = theme.ColorPalette.ItemHover;
                _columnColorState.Pressed = theme.ColorPalette.ColumnHeader;

                _hoverTrackingColor = theme.ColorPalette.ItemHover;
                _itemSelectedColor = theme.ColorPalette.ItemSelected;

                _itemSelectedTextColor = theme.ColorPalette.TextEnabled;

                _displayTextColor = theme.ColorPalette.TextDisabled;
                _displayTextFont = SystemFonts.DefaultFont;

                _colorState = new ColorState { Enabled = theme.ColorPalette.ControlEnabled, Disabled = theme.ColorPalette.ControlDisabled };

                _cornerBox.BackColor = theme.ColorPalette.ControlEnabled;
            }
            catch (Exception e)
            {
                ConsoleEx.WriteDebug(e);
            }

            Invalidate();
            OnThemeChanged(new ThemeEventArgs(theme));
        }

        #endregion

        #region Methods

        /// <summary>Cleanup any resources being used.</summary>
        /// <param name="disposing">Indicates whether the method call comes from a <see cref="Dispose" /> method or a finalizer.</param>
        protected override void Dispose(bool disposing)
        {
            if (_theme != IntPtr.Zero)
            {
                uxtheme.CloseThemeData(_theme);
            }

            if (disposing)
            {
                if (_components != null)
                {
                    _components.Dispose();
                }
            }

            base.Dispose(disposing);
        }

        protected override void OnDoubleClick(EventArgs e)
        {
            Point _pointerLocation = PointToClient(Cursor.Position);

            int _item;
            int _columnIndexer;
            int _cellX;
            int _cellY;
            ListStates _liveStates;
            ListViewRegion _listViewRegion;
            InterpretCoordinates(_pointerLocation.X, _pointerLocation.Y, out _listViewRegion, out _cellX, out _cellY, out _item, out _columnIndexer, out _liveStates);

            // Debug.WriteLine( "listRegion " + listRegion.ToString() );
            if ((_listViewRegion == ListViewRegion.Client) && (_columnIndexer < _columns.Count))
            {
                ActivateEmbeddedControl(_columnIndexer, _items[_item], _items[_item].SubItems[_columnIndexer]);
            }

            base.OnDoubleClick(e);
        }

        protected override void OnEnter(EventArgs e)
        {
            base.OnEnter(e);
            MouseState = MouseStates.Hover;
            Invalidate();
        }

        protected override void OnGotFocus(EventArgs e)
        {
            DestroyActivatedEmbedded();
            base.OnGotFocus(e);
        }

        protected override void OnLeave(EventArgs e)
        {
            base.OnLeave(e);
            MouseState = MouseStates.Normal;
            Invalidate();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            int _itemIndex;
            int _column;
            int _cellX;
            int _cellY;
            ListStates _liveStates;
            ListViewRegion _listViewRegion;
            InterpretCoordinates(e.X, e.Y, out _listViewRegion, out _cellX, out _cellY, out _itemIndex, out _column, out _liveStates);

            if (e.Button == MouseButtons.Right)
            {
                base.OnMouseDown(e);
                return;
            }

            // Column selected.
            if (_liveStates == ListStates.ColumnSelect)
            {
                // Reset state.
                _state = ListStates.None;

                bool _checkBoxClicked;

                if (_columns[_column].CheckBox)
                {
                    // Using MouseEvent.Location.Y instead of cellY since it's for the column.
                    if ((_cellX > CellPaddingSize) && (_cellX < CellPaddingSize + ListViewConstants.CHECKBOX_SIZE) && (e.Location.Y > CellPaddingSize) && (e.Location.Y < CellPaddingSize + ListViewConstants.CHECKBOX_SIZE))
                    {
                        // Toggle the column checkbox
                        if (_columns[_column].Checked)
                        {
                            _columns[_column].Checked = false;
                        }
                        else
                        {
                            _columns[_column].Checked = true;
                        }

                        // Clicked on the column CheckBox.
                        _checkBoxClicked = true;
                    }
                    else
                    {
                        _checkBoxClicked = false;
                    }
                }
                else
                {
                    _checkBoxClicked = false;
                }

                // Do not sort columns when checkbox clicked.
                if (!_checkBoxClicked)
                {
                    if (SortType != SortTypes.None)
                    {
                        _columns[_column].State = ColumnStates.Pressed;
                        SortColumn(_column);
                    }
                }

                ColumnClickedEvent?.Invoke(this, new ListViewClickEventArgs(_itemIndex, _column)); // fire the column clicked event

                base.OnMouseDown(e);
                return;
            }

            // Resizing column.
            if (_liveStates == ListStates.ColumnResizing)
            {
                // resizing
                Cursor.Current = Cursors.VSplit;
                _state = ListStates.ColumnResizing;

                _pointColumnResizeAnchor = new Point(GetColumnScreenX(_column), e.Y); // deal with moving column sizes
                _resizeColumnNumber = _column;

                base.OnMouseDown(e);
                return;
            }

            // Items selected.
            if (_liveStates == ListStates.Selecting)
            {
                // Control based multi select
                // Whatever else this does, it needs to first check to see if the state of the checkbox is changing
                if ((_column < _columns.Count) && _columns[_column].CheckBoxes)
                {
                    // Verify if clicked on the checkbox region in this control.
                    if ((_cellX > CellPaddingSize) && (_cellX < CellPaddingSize + ListViewConstants.CHECKBOX_SIZE) && (_cellY > CellPaddingSize) && (_cellY < CellPaddingSize + ListViewConstants.CHECKBOX_SIZE))
                    {
                        // Toggle the checkbox.
                        if (_items[_itemIndex].SubItems[_column].Checked)
                        {
                            _items[_itemIndex].SubItems[_column].Checked = false;
                            ItemCheck?.Invoke(this, new ItemCheckEventArgs(_itemIndex, CheckState.Unchecked, CheckState.Checked));
                        }
                        else
                        {
                            _items[_itemIndex].SubItems[_column].Checked = true;
                            ItemCheck?.Invoke(this, new ItemCheckEventArgs(_itemIndex, CheckState.Checked, CheckState.Unchecked));
                        }

                        ItemChecked?.Invoke(_items[_itemIndex]);
                    }
                }

                _state = ListStates.Selecting;
                _focusedItemIndex = _itemIndex;
                FocusedItem = _items[_itemIndex];

                if (((ModifierKeys & Keys.Control) == Keys.Control) && _multiSelect)
                {
                    _lastSelectionIndex = _itemIndex;

                    if (_items[_itemIndex].Selected)
                    {
                        _items[_itemIndex].Selected = false;
                    }
                    else
                    {
                        _items[_itemIndex].Selected = true;
                    }

                    base.OnMouseDown(e);
                    return;
                }

                // Shift based multi row select -------------------------------------------------------
                if (((ModifierKeys & Keys.Shift) == Keys.Shift) && _multiSelect)
                {
                    _items.ClearSelection();
                    if (_lastSelectionIndex >= 0)
                    {
                        // ie, non negative so that we have a starting point
                        int index = _lastSelectionIndex;
                        do
                        {
                            _items[index].Selected = true;
                            if (index > _itemIndex)
                            {
                                index--;
                            }

                            if (index < _itemIndex)
                            {
                                index++;
                            }
                        }
                        while (index != _itemIndex);

                        _items[index].Selected = true;
                    }

                    base.OnMouseDown(e);
                    return;
                }

                // Normal single select -----------------------------------------------------------
                _items.ClearSelection(_items[_itemIndex]);

                // Following two if statements deal ONLY with non multi=select where a single sub item is being selected
                if ((_lastSelectionIndex < Count) && (_lastSubSelectionIndex < _columns.Count))
                {
                    _items[_lastSelectionIndex].SubItems[_lastSubSelectionIndex].Selected = false;
                }

                if ((_fullRowSelect == false) && (_itemIndex < Count) && (_column < _columns.Count))
                {
                    _items[_itemIndex].SubItems[_column].Selected = true;
                }

                _lastSelectionIndex = _itemIndex;
                _lastSubSelectionIndex = _column;
                _items[_itemIndex].Selected = true;
            }

            base.OnMouseDown(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            // This is the HEADER hot state
            _columns.ClearHotStates();
            _hoverItemIndex = -1;
            _hotColumnIndex = -1;

            base.OnMouseLeave(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            try
            {
                if (_state == ListStates.ColumnResizing)
                {
                    Cursor.Current = Cursors.VSplit;
                    int _width = e.X - _pointColumnResizeAnchor.X;

                    if (_width <= ListViewConstants.MINIMUM_COLUMN_SIZE)
                    {
                        _width = ListViewConstants.MINIMUM_COLUMN_SIZE;
                    }

                    VisualListViewColumn _column = _columns[_resizeColumnNumber];
                    _column.Width = _width;

                    OnMove(e);
                    return;
                }

                int _itemIndex;
                int _columnIndexer;
                var _cellX = 0;
                var _cellY = 0;
                ListStates _listStates;
                ListViewRegion _listRegion;
                InterpretCoordinates(e.X, e.Y, out _listRegion, out _cellX, out _cellY, out _itemIndex, out _columnIndexer, out _listStates);

                // Update the column index.
                if (_columns.Count > _columnIndexer)
                {
                    _columnIndex = _columnIndexer;
                    _hoveredColumn = _columns[_columnIndex];
                }
                else
                {
                    // Outside of columns bounds.
                    _columnIndex = -1;
                    _hoveredColumn = null;
                }

                // Update the hovered item.

                // Test if out of bounds hover item.
                _hoveredItem = _items[_itemIndex];

                ItemMouseHover?.Invoke(_items[_itemIndex]);

                if (_listStates == ListStates.ColumnResizing)
                {
                    Cursor.Current = Cursors.VSplit;
                    OnMove(e);
                    return;
                }

                Cursor.Current = Cursors.Arrow;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception throw in GlobalList_MouseMove with text : " + ex);
            }

            OnMove(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            Cursor.Current = Cursors.Arrow;
            Columns.ClearStates();

            var item = 0;
            var column = 0;
            var cellX = 0;
            var cellY = 0;
            ListStates listStates;
            ListViewRegion listRegion;
            InterpretCoordinates(e.X, e.Y, out listRegion, out cellX, out cellY, out item, out column, out listStates);

            _state = ListStates.None;

            base.OnMouseUp(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            // TODO: Allow rounded type.
            _border.Type = ShapeTypes.Rectangle;

            if (!DesignMode && _updating)
            {
                return;
            }

            // Doesn't really belong in paint.
            RecalculateScroll();

            Graphics _graphics = e.Graphics;
            _graphics.SmoothingMode = SmoothingMode.HighQuality;
            _graphics.TextRenderingHint = TextStyle.TextRenderingHint;

            // int _insideWidth = _columns.Width > HeaderRectangle.Width ? _columns.Width : HeaderRectangle.Width;
            if (HeaderVisible)
            {
                _graphics.SetClip(HeaderRectangle);
                VisualListViewRenderer.DrawColumnHeaders(_graphics, new Size(HeaderRectangle.Width, HeaderRectangle.Height), this, _horizontalScrollBar, _theme);
            }

            _graphics.SetClip(RowsInnerClientRect);

            Color backColorState = ColorState.BackColorState(_colorState, Enabled, MouseState);
            VisualListViewRenderer.DrawRows(_graphics, this, backColorState, _verticalScrollBar, _horizontalScrollBar, _newLiveControls, _liveControls, ListViewConstants.CHECKBOX_SIZE);

            // Update embedded controls.
            foreach (Control control in _liveControls)
            {
                control.Visible = false;
            }

            _liveControls = _newLiveControls;
            _newLiveControls = new ArrayList();

            DrawDisplayText(_graphics);

            ControlGraphicsPath = VisualBorderRenderer.CreateBorderTypePath(ClientRectangle, _border);

            _graphics.SetClip(ClientRectangle);
            VisualBorderRenderer.DrawBorderStyle(_graphics, _border, ControlGraphicsPath, MouseState);
            base.OnPaint(e);
        }

        protected override void OnResize(EventArgs e)
        {
            Invalidate();
        }

        /// <summary>Calculates the longest width of the elements.</summary>
        /// <param name="column">The column.</param>
        /// <param name="textWidth">The text.</param>
        /// <returns>The <see cref="int" />.</returns>
        private static int CalculateLongestElementWidth(int column, int textWidth)
        {
            int width;
            if (column > textWidth)
            {
                width = column;
            }
            else
            {
                width = textWidth;
            }

            return width;
        }

        /// <summary>Instance the activated embedded control for this item/column.</summary>
        /// <param name="columnIndex">The column index.</param>
        /// <param name="item">The item.</param>
        /// <param name="subItem">The sub item.</param>
        private void ActivateEmbeddedControl(int columnIndex, VisualListViewItem item, VisualListViewSubItem subItem)
        {
            if (_activatedEmbeddedControl != null)
            {
                _activatedEmbeddedControl.Dispose();
                _activatedEmbeddedControl = null;
            }

            if (_columns[columnIndex].EmbeddedControlTemplate == null)
            {
                return;
            }

            Type _type = _columns[columnIndex].EmbeddedControlTemplate.GetType();
            Control _control = (Control)Activator.CreateInstance(_type);
            ILVEmbeddedControl _iEmbeddedControl = (ILVEmbeddedControl)_control;

            if (_iEmbeddedControl == null)
            {
                throw new Exception(@"Control does not implement the GLEmbeddedControl interface, can't start");
            }

            _iEmbeddedControl.LVEmbeddedControlLoad(item, subItem, this);

            _control.KeyPress += TextBox_KeyPress;
            _control.Parent = this;
            _activatedEmbeddedControl = _control;

            if (_activatedEmbeddedControl != null)
            {
                // int _yOffset = (subItem.LastCellRectangle.Height - _activatedEmbeddedControl.Bounds.Height) / 2;
            }

            Rectangle _controlBounds;

            if (GridLineStyle == GridLineStyle.None)
            {
                // Add 1 to x to give border, add 2 to Y because to account for possible grid that you must cover up
                _controlBounds = new Rectangle(subItem.LastCellRectangle.X + 1, subItem.LastCellRectangle.Y + 1, subItem.LastCellRectangle.Width - 3, subItem.LastCellRectangle.Height - 2);
            }
            else
            {
                // Add 1 to x to give border, add 2 to Y because to account for possible grid that you must cover up
                _controlBounds = new Rectangle(subItem.LastCellRectangle.X + 1, subItem.LastCellRectangle.Y + 2, subItem.LastCellRectangle.Width - 3, subItem.LastCellRectangle.Height - 3);
            }

            _control.Bounds = _controlBounds;
            _control.Show();
            _control.Focus();
        }

        /// <summary>Determines if themes are available.</summary>
        /// <returns>The <see cref="bool" />.</returns>
        private bool AreThemesAvailable()
        {
            // IntPtr hTheme = IntPtr.Zero;
            try
            {
                if ((uxtheme.IsThemeActive() == 1) && (_theme == IntPtr.Zero))
                {
                    _theme = uxtheme.OpenThemeData(_theme, "HEADER");

                    return true;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }

            return false;
        }

        /// <summary>Calculate the check box width.</summary>
        /// <param name="columnIndex">The column index.</param>
        /// <param name="width">The width.</param>
        /// <param name="padding">The padding.</param>
        /// <returns>The <see cref="int" />.</returns>
        private int CalculateCheckBoxWidth(int columnIndex, int width, int padding)
        {
            if (_columns[columnIndex].CheckBoxes)
            {
                int columnCheckBoxWidth = padding + ListViewConstants.CHECKBOX_SIZE;
                width += columnCheckBoxWidth;
            }

            return width;
        }

        /// <summary>Calculates the column text width.</summary>
        /// <param name="columnIndex">The column index.</param>
        /// <returns>The <see cref="int" />.</returns>
        private int CalculateColumnTextWidth(int columnIndex)
        {
            int columnWidth = _columns[columnIndex].Text.MeasureText(Font).Width;
            int width = RetrieveDecorationsWidth(columnIndex, columnWidth, internalPadding);
            columnWidth += width;
            return columnWidth;
        }

        /// <summary>Calculates the column width.</summary>
        /// <param name="columnIndex">The column index.</param>
        /// <returns>The <see cref="int" />.</returns>
        private int CalculateColumnWidth(int columnIndex)
        {
            int columnWidth = _columns[columnIndex].Width;
            int width = RetrieveDecorationsWidth(columnIndex, columnWidth, internalPadding);
            columnWidth += width;
            return columnWidth;
        }

        /// <summary>Calculate the image list index width.</summary>
        /// <param name="columnIndex">The column index.</param>
        /// <param name="width">The width.</param>
        /// <param name="padding">The padding.</param>
        /// <returns>The <see cref="int" />.</returns>
        private int CalculateImageWidth(int columnIndex, int width, int padding)
        {
            if (_imageListColumns != null)
            {
                int columnImageWidth = padding + padding + _imageListColumns.Images[columnIndex].Width + padding + padding;
                width += columnImageWidth;
            }

            return width;
        }

        /// <summary>Destroy activated embedded control exists, remove and unload it.</summary>
        private void DestroyActivatedEmbedded()
        {
            if (_activatedEmbeddedControl != null)
            {
                ILVEmbeddedControl control = (ILVEmbeddedControl)_activatedEmbeddedControl;
                control.LVEmbeddedControlUnload();

                // Must do this because the unload may call the changed callback from the items which would call this routine a second time
                if (_activatedEmbeddedControl != null)
                {
                    _activatedEmbeddedControl.Dispose();
                    _activatedEmbeddedControl = null;
                }
            }
        }

        /// <summary>Draws the display text.</summary>
        /// <param name="graphics">The specified graphics to draw on.</param>
        private void DrawDisplayText(Graphics graphics)
        {
            if ((_items.Count <= 0) && _displayTextOnEmpty)
            {
                Rectangle _layoutRectangle = new Rectangle(0, 0, RowsClientRectangle.Width, RowsClientRectangle.Height);
                VisualTextRenderer.RenderText(graphics, _layoutRectangle, _displayText, _displayTextFont, _displayTextColor, TextStyle.StringFormat);
            }
        }

        /// <summary>Handle horizontal scroll bar movement.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event args.</param>
        private void HorizontalPanelScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            // this.Focus();
            Invalidate();
        }

        /// <summary>Timer handler. This mostly deals with the hover technology with events firing.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event args.</param>
        private void HoverTimer_TimerTick(object sender, EventArgs e)
        {
            Point _pointLocalMouse;
            if (Cursor != null)
            {
                _pointLocalMouse = PointToClient(Cursor.Position);
            }
            else
            {
                _pointLocalMouse = new Point(9999, 9999);
            }

            int _item;
            int _column;
            var _cellX = 0;
            var _cellY = 0;
            ListStates _listStates;
            ListViewRegion _listRegion;
            InterpretCoordinates(_pointLocalMouse.X, _pointLocalMouse.Y, out _listRegion, out _cellX, out _cellY, out _item, out _column, out _listStates);

            if ((_pointLocalMouse == _lastHoverSpot) && !_hoverLive && (_listRegion != ListViewRegion.NonClient))
            {
                HoverEvent?.Invoke(this, new ListViewHoverEventArgs(ListViewHoverTypes.HoverStart, _item, _column, _listRegion));
                _hoverLive = true;
            }
            else if (_hoverLive && (_pointLocalMouse != _lastHoverSpot))
            {
                HoverEvent?.Invoke(this, new ListViewHoverEventArgs(ListViewHoverTypes.HoverEnd, -1, -1, ListViewRegion.NonClient));
                _hoverLive = false;
            }

            _lastHoverSpot = _pointLocalMouse;
        }

        /// <summary>Initializes the component.</summary>
        private void InitializeComponent()
        {
            if (ControlStyle == LVControlStyles.XP)
            {
                Application.EnableVisualStyles();
            }

            _components = new Container();
            DoubleBuffered = true;
        }

        /// <summary>The item has changed event.</summary>
        /// <param name="source">The source.</param>
        /// <param name="e">The event args.</param>
        private void Items_Changed(object source, ListViewChangedEventArgs e)
        {
            DestroyActivatedEmbedded();

            ItemChangedEvent?.Invoke(this, e);

            // Invalidate if an item that is within the visible area has changed
            if (e.Item != null)
            {
                if (IsItemVisible(e.Item))
                {
                    Invalidate();
                }
            }

            // Fixes: Invalidates list view on items changed.
            Invalidate();
        }

        private void OnMouseDownFromSubItem(object sender, MouseEventArgs e)
        {
            ConsoleEx.WriteDebug("OnMouseDown::SubItem");

            Point _clientPoint = PointToClient(new Point(MousePosition.X, MousePosition.Y));
            e = new MouseEventArgs(e.Button, e.Clicks, _clientPoint.X, _clientPoint.Y, e.Delta);
            OnMouseDown(e);
        }

        private void OnScroll(object sender, ScrollEventArgs e)
        {
            DestroyActivatedEmbedded();
            Invalidate();
            Scroll?.Invoke(sender, e);
        }

        /// <summary>Recalculate scroll bars and control size.</summary>
        private void RecalculateScroll()
        {
            var _exitCode = 0;
            bool _bSbChanged;
            do
            {
                // Loop handles changes and re-changes that happen when one or the other changes
                _bSbChanged = false;

                if ((_columns.Width > RowsInnerClientRect.Width) && (_horizontalScrollBar.Visible == false))
                {
                    // total width of all the rows is less than the visible rect
                    _horizontalScrollBar.MVisible = true;
                    _horizontalScrollBar.Value = 0;
                    _bSbChanged = true;

                    Invalidate();
                }

                if ((_columns.Width <= RowsInnerClientRect.Width) && _horizontalScrollBar.Visible)
                {
                    // total width of all the rows is less than the visible rect
                    _horizontalScrollBar.MVisible = false;
                    _horizontalScrollBar.Value = 0;
                    _bSbChanged = true;

                    Invalidate();
                }

                if ((TotalRowsHeight > RowsInnerClientRect.Height) && (_verticalScrollBar.Visible == false))
                {
                    // total height of all the rows is greater than the visible rect
                    _verticalScrollBar.MVisible = true;
                    _horizontalScrollBar.Value = 0;
                    _bSbChanged = true;

                    Invalidate();
                }

                if ((TotalRowsHeight <= RowsInnerClientRect.Height) && _verticalScrollBar.Visible)
                {
                    // total height of all rows is less than the visible rect
                    _verticalScrollBar.MVisible = false;
                    _verticalScrollBar.Value = 0;
                    _bSbChanged = true;

                    Invalidate();
                }

                if (++_exitCode > 4)
                {
                    break;
                }
            }
            while (_bSbChanged);

            Rectangle _rectangleClient = RowsInnerClientRect;

            if (_verticalScrollBar.Visible)
            {
                _verticalScrollBar.MTop = _rectangleClient.Y;
                _verticalScrollBar.MLeft = _rectangleClient.Right;
                _verticalScrollBar.MHeight = _rectangleClient.Height;
                _verticalScrollBar.MLargeChange = RowsVisible;
                _verticalScrollBar.MMaximum = Count - 1;

                if (_verticalScrollBar.Value + RowsVisible > Count)
                {
                    // Catch all to make sure the scrollbar isn't going farther than visible items
                    _verticalScrollBar.Value = Count - RowsVisible; // an item got deleted underneath somehow and scroll value is larger than can be displayed
                }
            }

            if (_horizontalScrollBar.Visible)
            {
                _horizontalScrollBar.MLeft = _rectangleClient.Left;
                _horizontalScrollBar.MTop = _rectangleClient.Bottom;
                _horizontalScrollBar.MWidth = _rectangleClient.Width;

                _horizontalScrollBar.MLargeChange = _rectangleClient.Width; // this re-all is the size we want to move
                _horizontalScrollBar.MMaximum = _columns.Width;

                if (_horizontalScrollBar.Value + _horizontalScrollBar.LargeChange > _horizontalScrollBar.Maximum)
                {
                    _horizontalScrollBar.Value = _horizontalScrollBar.Maximum - _horizontalScrollBar.LargeChange;
                }
            }

            if (_horizontalScrollBar.Visible && _verticalScrollBar.Visible)
            {
                if (!_cornerBox.Visible)
                {
                    _cornerBox.Visible = true;
                }

                _cornerBox.Bounds = new Rectangle(_horizontalScrollBar.Right, _verticalScrollBar.Bottom, _verticalScrollBar.Width, _horizontalScrollBar.Height);
            }
            else
            {
                if (_cornerBox.Visible)
                {
                    _cornerBox.Visible = false;
                }
            }
        }

        /// <summary>Retrieves the decorations width.</summary>
        /// <param name="columnIndex">The column index.</param>
        /// <param name="width">The width.</param>
        /// <param name="padding">The padding.</param>
        /// <returns>The <see cref="int" />.</returns>
        private int RetrieveDecorationsWidth(int columnIndex, int width, int padding)
        {
            width = CalculateCheckBoxWidth(columnIndex, width, padding);
            width = CalculateImageWidth(columnIndex, width, padding);
            return width;
        }

        /// <summary>Resizes the width of the given column as indicated by the resize style.</summary>
        /// <param name="columnIndex">The zero-based index of the column to resize.</param>
        /// <param name="headerAutoResize">One of the <see cref="ColumnHeaderAutoResizeStyle" /> values.</param>
        private void SetColumnWidth(int columnIndex, ColumnHeaderAutoResizeStyle headerAutoResize)
        {
            if ((columnIndex < 0) || ((columnIndex >= 0) && (_columns == null)) || (columnIndex >= _columns.Count))
            {
                throw new ArgumentOutOfRangeException(nameof(columnIndex));
            }

            int width;

            switch (headerAutoResize)
            {
                case ColumnHeaderAutoResizeStyle.None:
                    {
                        if (AutoSize)
                        {
                            int columnWidth = CalculateColumnWidth(columnIndex);
                            int columnTextWidth = CalculateColumnTextWidth(columnIndex);

                            width = CalculateLongestElementWidth(columnWidth, columnTextWidth);
                        }
                        else
                        {
                            width = CalculateColumnWidth(columnIndex);
                        }

                        break;
                    }

                case ColumnHeaderAutoResizeStyle.HeaderSize:
                    {
                        width = CalculateColumnWidth(columnIndex);
                        break;
                    }

                case ColumnHeaderAutoResizeStyle.ColumnContent:
                    {
                        width = CalculateColumnTextWidth(columnIndex);
                        break;
                    }

                default:
                    {
                        throw new ArgumentOutOfRangeException(nameof(headerAutoResize), headerAutoResize, null);
                    }
            }

            SetColumnWidth(columnIndex, width);

            if ((IsHandleCreated && (headerAutoResize == ColumnHeaderAutoResizeStyle.ColumnContent)) || (headerAutoResize == ColumnHeaderAutoResizeStyle.HeaderSize))
            {
                if (width != 0)
                {
                    _columns[columnIndex].Width = width;
                }
            }
        }

        /// <summary>Check for return (if we get it, deactivate).</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event args.</param>
        private void TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Return) || (e.KeyChar == (char)Keys.Escape))
            {
                DestroyActivatedEmbedded();
            }
        }

        /// <summary>Resizes the width of the given column as indicated by the resize style.</summary>
        /// <param name="headerAutoResize">One of the <see cref="ColumnHeaderAutoResizeStyle" /> values.</param>
        private void UpdateColumnWidths(ColumnHeaderAutoResizeStyle headerAutoResize)
        {
            if (_columns != null)
            {
                for (var i = 0; i < _columns.Count; i++)
                {
                    SetColumnWidth(i, headerAutoResize);
                }
            }
        }

        /// <summary>Handle vertical scroll bar movement.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event args.</param>
        private void VerticalPanelScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            // this.Focus();
            Parent.Invalidate(true);
        }

        #endregion
    }
}