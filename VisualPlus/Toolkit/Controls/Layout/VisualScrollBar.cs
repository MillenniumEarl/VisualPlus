#region License

// -----------------------------------------------------------------------------------------------------------
// 
// Name: VisualScrollBar.cs
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
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

using VisualPlus.Delegates;
using VisualPlus.Designer;
using VisualPlus.Enumerators;
using VisualPlus.Events;
using VisualPlus.Interfaces;
using VisualPlus.Localization;
using VisualPlus.Models;
using VisualPlus.Native;
using VisualPlus.Renders;
using VisualPlus.Toolkit.VisualBase;
using VisualPlus.TypeConverters;
using VisualPlus.Utilities.Debugging;

#endregion

namespace VisualPlus.Toolkit.Controls.Layout
{
    [ClassInterface(ClassInterfaceType.AutoDispatch)]
    [ComVisible(true)]
    [DefaultEvent("Scroll")]
    [DefaultProperty("Value")]
    [Description("The Visual ScrollBar")]
    [Designer(typeof(VisualScrollBarDesigner))]
    [ToolboxBitmap(typeof(VisualScrollBar), "VisualScrollBar.bmp")]
    [ToolboxItem(true)]
    public class VisualScrollBar : VisualStyleBase, IThemeSupport
    {
        #region Constants

        private const int WM_SETREDRAW = 11;

        #endregion

        #region Fields

        private int _arrowHeight;
        private int _arrowWidth;
        private ColorState _backColorState;
        private Border _border;
        private bool _bottomArrowClicked;
        private Rectangle _bottomArrowRectangle;
        private bool _bottomBarClicked;
        private Border _buttonBorder;
        private MouseStates _buttonBottomState;
        private ControlColorState _buttonColorState;
        private MouseStates _buttonTopState;
        private Rectangle _channelRectangle;
        private Rectangle _clickedBarRectangle;
        private IContainer _components;
        private ContextMenuStrip _contextMenu;
        private int _defaultWidth;
        private bool _inUpdate;
        private int _largeChange;
        private int _maximum;
        private int _minimum;
        private Orientation _orientation;
        private Timer _progressTimer;
        private ScrollOrientation _scrollOrientation;
        private int _smallChange;
        private Border _thumbBorder;
        private int _thumbBottomLimitBottom;
        private int _thumbBottomLimitTop;
        private bool _thumbClicked;
        private ControlColorState _thumbColorState;
        private bool _thumbGripVisible;
        private int _thumbHeight;
        private int _thumbPosition;
        private Rectangle _thumbRectangle;
        private MouseStates _thumbState;
        private int _thumbTopLimit;
        private int _thumbWidth;
        private ToolStripSeparator _toolStripSeparator1;
        private ToolStripSeparator _toolStripSeparator2;
        private ToolStripSeparator _toolStripSeparator3;
        private bool _topArrowClicked;
        private Rectangle _topArrowRectangle;
        private bool _topBarClicked;
        private int _trackPosition;
        private Color _trackPressed;
        private ToolStripMenuItem _tsmiBottom;
        private ToolStripMenuItem _tsmiLargeDown;
        private ToolStripMenuItem _tsmiLargeUp;
        private ToolStripMenuItem _tsmiScrollHere;
        private ToolStripMenuItem _tsmiSmallDown;
        private ToolStripMenuItem _tsmiSmallUp;
        private ToolStripMenuItem _tsmiTop;
        private int _value;

        #endregion

        #region Constructors and Destructors

        /// <summary>Initializes a new instance of the <see cref="VisualScrollBar" /> class.</summary>
        public VisualScrollBar()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.Selectable | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);

            InitializeComponent();

            _defaultWidth = 20;

            Width = _defaultWidth;
            Height = 200;

            _thumbWidth = 15;
            _smallChange = 1;
            _scrollOrientation = ScrollOrientation.VerticalScroll;
            _progressTimer = new Timer();
            _orientation = Orientation.Vertical;
            _maximum = 100;
            _largeChange = 10;
            _arrowWidth = 15;
            _arrowHeight = 17;

            _thumbGripVisible = true;

            _thumbState = MouseStates.Normal;
            _buttonTopState = MouseStates.Normal;
            _buttonBottomState = MouseStates.Normal;

            _border = new Border();
            _thumbBorder = new Border();
            _buttonBorder = new Border();
            _backColorState = new ColorState();
            _thumbColorState = new ControlColorState();
            _buttonColorState = new ControlColorState();

            // Timer for clicking and holding the mouse button over/below the thumb and on the arrow buttons
            _progressTimer.Interval = 20;
            _progressTimer.Tick += ProgressTimerTick;

            _contextMenu.ShowImageMargin = false;

            // this.ContextMenuStrip = contextMenu;
            UpdateTheme(ThemeManager.Theme);
            ConfigureScrollBar();
        }

        #endregion

        #region Public Events

        [Category(EventCategory.Behavior)]
        [Description(EventDescription.PropertyEventChanged)]
        public event ElementClickedEventHandler ButtonBottomClicked;

        [Category(EventCategory.Behavior)]
        [Description(EventDescription.PropertyEventChanged)]
        public event ElementClickedEventHandler ButtonTopClicked;

        [Category(EventCategory.Behavior)]
        [Description(PropertyDescription.ScrollBars)]
        public event ScrollEventHandler Scroll;

        [Category(EventCategory.Behavior)]
        [Description(EventDescription.PropertyEventChanged)]
        public event ElementClickedEventHandler ThumbClicked;

        #endregion

        #region Public Properties

        [TypeConverter(typeof(VisualSettingsTypeConverter))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public ColorState BackColorState
        {
            get
            {
                return _backColorState;
            }

            set
            {
                if (value == _backColorState)
                {
                    return;
                }

                _backColorState = value;
                Invalidate();
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
                _border = value;
                Invalidate();
            }
        }

        [TypeConverter(typeof(VisualSettingsTypeConverter))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Category(PropertyCategory.Appearance)]
        public Border ButtonBorder
        {
            get
            {
                return _buttonBorder;
            }

            set
            {
                _buttonBorder = value;
                Invalidate();
            }
        }

        [Category(PropertyCategory.Appearance)]
        [Description(PropertyDescription.MouseState)]
        public MouseStates ButtonBottomMouseState
        {
            get
            {
                return _buttonBottomState;
            }

            set
            {
                _buttonBottomState = value;
                Invalidate();
            }
        }

        [TypeConverter(typeof(VisualSettingsTypeConverter))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public ControlColorState ButtonColorState
        {
            get
            {
                return _buttonColorState;
            }

            set
            {
                if (value == _buttonColorState)
                {
                    return;
                }

                _buttonColorState = value;
                Invalidate();
            }
        }

        [Category(PropertyCategory.Appearance)]
        [Description(PropertyDescription.MouseState)]
        public MouseStates ButtonTopMouseState
        {
            get
            {
                return _buttonTopState;
            }

            set
            {
                _buttonTopState = value;
                Invalidate();
            }
        }

        [Category(PropertyCategory.Appearance)]
        [Description(PropertyDescription.Opacity)]
        [DefaultValue(typeof(double), "1")]
        public double ContextMenuOpacity
        {
            get
            {
                return _contextMenu.Opacity;
            }

            set
            {
                if (value == _contextMenu.Opacity)
                {
                    return;
                }

                _contextMenu.AllowTransparency = value != 1;
                _contextMenu.Opacity = value;
            }
        }

        [Category(PropertyCategory.Behavior)]
        [Description(PropertyDescription.LargeChange)]
        [DefaultValue(10)]
        public int LargeChange
        {
            get
            {
                return _largeChange;
            }

            set
            {
                // no change or new large change value is invalid - return
                if ((value == _largeChange) || (value < _smallChange) || (value < 2))
                {
                    return;
                }

                // if value is greater than scroll area - adjust
                if (value > _maximum - _minimum)
                {
                    _largeChange = _maximum - _minimum;
                }
                else
                {
                    // set new value
                    _largeChange = value;
                }

                ConfigureScrollBar();
            }
        }

        [Category(PropertyCategory.Behavior)]
        [Description(PropertyDescription.Maximum)]
        [DefaultValue(100)]
        public int Maximum
        {
            get
            {
                return _maximum;
            }

            set
            {
                // no change or new max. value invalid - return
                if ((value == _maximum) || (value < 1) || (value <= _minimum))
                {
                    return;
                }

                _maximum = value;

                // is large change value invalid - adjust
                if (_largeChange > _maximum - _minimum)
                {
                    _largeChange = _maximum - _minimum;
                }

                ConfigureScrollBar();

                // is current value greater than new maximum value - adjust
                if (_value > value)
                {
                    Value = _maximum;
                }
                else
                {
                    // current value is valid - adjust thumb position
                    ChangeThumbPosition(GetThumbPosition());

                    Refresh();
                }
            }
        }

        [Category(PropertyCategory.Behavior)]
        [Description(PropertyDescription.Minimum)]
        [DefaultValue(0)]
        public int Minimum
        {
            get
            {
                return _minimum;
            }

            set
            {
                // no change or value invalid - return
                if ((_minimum == value) || (value < 0) || (value >= _maximum))
                {
                    return;
                }

                _minimum = value;

                // current value less than new _minimum value - adjust
                if (_value < value)
                {
                    _value = value;
                }

                // is current large change value invalid - adjust
                if (_largeChange > _maximum - _minimum)
                {
                    _largeChange = _maximum - _minimum;
                }

                ConfigureScrollBar();

                // current value less than new _minimum value - adjust
                if (_value < value)
                {
                    Value = value;
                }
                else
                {
                    // current value is valid - adjust thumb position
                    ChangeThumbPosition(GetThumbPosition());

                    Refresh();
                }
            }
        }

        [Category(PropertyCategory.Layout)]
        [Description(PropertyDescription.Orientation)]
        [DefaultValue(Orientation.Vertical)]
        public Orientation Orientation
        {
            get
            {
                return _orientation;
            }

            set
            {
                // no change - return
                if (value == _orientation)
                {
                    return;
                }

                _orientation = value;

                // change text of context menu entries
                ChangeContextMenuItems();

                // save scroll orientation for scroll event
                _scrollOrientation = value == Orientation.Vertical ? ScrollOrientation.VerticalScroll : ScrollOrientation.HorizontalScroll;

                // only in DesignMode switch width and height
                if (DesignMode)
                {
                    Size = new Size(Height, Width);
                }

                // sets the scrollbar up
                ConfigureScrollBar();
            }
        }

        [Category(PropertyCategory.Behavior)]
        [Description(PropertyDescription.SmallChange)]
        [DefaultValue(1)]
        public int SmallChange
        {
            get
            {
                return _smallChange;
            }

            set
            {
                // no change or new small change value invalid - return
                if ((value == _smallChange) || (value < 1) || (value >= _largeChange))
                {
                    return;
                }

                _smallChange = value;

                ConfigureScrollBar();
            }
        }

        [TypeConverter(typeof(VisualSettingsTypeConverter))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Category(PropertyCategory.Appearance)]
        public Border ThumbBorder
        {
            get
            {
                return _thumbBorder;
            }

            set
            {
                _thumbBorder = value;
                Invalidate();
            }
        }

        [TypeConverter(typeof(VisualSettingsTypeConverter))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public ControlColorState ThumbColorState
        {
            get
            {
                return _thumbColorState;
            }

            set
            {
                if (value == _thumbColorState)
                {
                    return;
                }

                _thumbColorState = value;
                Invalidate();
            }
        }

        [Description(PropertyDescription.Toggle)]
        [Category(PropertyCategory.Appearance)]
        public bool ThumbGripVisible
        {
            get
            {
                return _thumbGripVisible;
            }

            set
            {
                _thumbGripVisible = value;
                Invalidate();
            }
        }

        [Category(PropertyCategory.Appearance)]
        [Description(PropertyDescription.MouseState)]
        public MouseStates ThumbMouseState
        {
            get
            {
                return _thumbState;
            }

            set
            {
                _thumbState = value;
                Invalidate();
            }
        }

        [Category(PropertyCategory.Appearance)]
        [Description(PropertyDescription.Color)]
        public Color TrackPressed
        {
            get
            {
                return _trackPressed;
            }

            set
            {
                _trackPressed = value;
                Invalidate();
            }
        }

        [Category(PropertyCategory.Behavior)]
        [Description(PropertyDescription.Value)]
        [DefaultValue(0)]
        public int Value
        {
            get
            {
                return _value;
            }

            set
            {
                // no change or invalid value - return
                if ((_value == value) || (value < _minimum) || (value > _maximum))
                {
                    return;
                }

                _value = value;

                // adjust thumb position
                ChangeThumbPosition(GetThumbPosition());

                // raise scroll event
                OnScroll(new ScrollEventArgs(ScrollEventType.ThumbPosition, -1, _value, _scrollOrientation));

                Refresh();
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>Prevents the drawing of the control until <see cref="EndUpdate" /> is called.</summary>
        public void BeginUpdate()
        {
            user32.SendMessage(Handle, WM_SETREDRAW, Convert.ToInt32(false), new IntPtr(0));
            _inUpdate = true;
        }

        /// <summary>Ends the updating process and the control can draw itself again.</summary>
        public void EndUpdate()
        {
            user32.SendMessage(Handle, WM_SETREDRAW, Convert.ToInt32(true), new IntPtr(0));
            _inUpdate = false;
            ConfigureScrollBar();
            Refresh();
        }

        public void UpdateTheme(Theme theme)
        {
            try
            {
                _border.Color = theme.ColorPalette.BorderNormal;
                _border.HoverColor = theme.ColorPalette.BorderHover;

                _buttonBorder.Color = theme.ColorPalette.BorderNormal;
                _buttonBorder.HoverColor = theme.ColorPalette.BorderHover;

                _thumbBorder.Color = theme.ColorPalette.BorderNormal;
                _thumbBorder.HoverColor = theme.ColorPalette.BorderHover;

                _backColorState.Disabled = theme.ColorPalette.ScrollBarDisabled;
                _backColorState.Enabled = theme.ColorPalette.ScrollBarEnabled;

                _thumbColorState.Disabled = theme.ColorPalette.ScrollThumbEnabled;
                _thumbColorState.Enabled = theme.ColorPalette.ScrollThumbDisabled;
                _thumbColorState.Hover = theme.ColorPalette.ScrollThumbHover;
                _thumbColorState.Pressed = theme.ColorPalette.ScrollThumbPressed;

                _buttonColorState.Disabled = theme.ColorPalette.ScrollButtonEnabled;
                _buttonColorState.Enabled = theme.ColorPalette.ScrollButtonDisabled;
                _buttonColorState.Hover = theme.ColorPalette.ScrollButtonHover;
                _buttonColorState.Pressed = theme.ColorPalette.ScrollButtonPressed;

                _trackPressed = Color.FromArgb(10, 0, 0, 0);
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

        protected override void CreateHandle()
        {
            base.CreateHandle();
            ContextMenuStrip = _contextMenu;
        }

        protected virtual void OnButtonBottomClicked(EventArgs e)
        {
            ButtonBottomClicked?.Invoke(e);
        }

        protected virtual void OnButtonTopClicked(EventArgs e)
        {
            ButtonTopClicked?.Invoke(e);
        }

        protected override void OnEnabledChanged(EventArgs e)
        {
            base.OnEnabledChanged(e);

            if (Enabled)
            {
                _buttonTopState = MouseStates.Normal;
                _buttonBottomState = MouseStates.Normal;
            }
            else
            {
                _buttonTopState = MouseStates.Normal;
                _buttonBottomState = MouseStates.Normal;
            }

            Refresh();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            MouseState = MouseStates.Pressed;
            Invalidate();
            Focus();

            if (e.Button == MouseButtons.Left)
            {
                // Prevents showing the context menu if pressing the right mouse button while holding the left/
                ContextMenuStrip = null;

                Point _mouseLocation = e.Location;

                if (_thumbRectangle.Contains(_mouseLocation))
                {
                    _thumbClicked = true;
                    _thumbPosition = _orientation == Orientation.Vertical ? _mouseLocation.Y - _thumbRectangle.Y : _mouseLocation.X - _thumbRectangle.X;
                    _thumbState = MouseStates.Pressed;
                    Invalidate(_thumbRectangle);
                    OnThumbClicked(EventArgs.Empty);
                }
                else if (_topArrowRectangle.Contains(_mouseLocation))
                {
                    _topArrowClicked = true;
                    _buttonTopState = MouseStates.Pressed;
                    Invalidate(_topArrowRectangle);
                    ProgressThumb(true);
                    OnButtonTopClicked(EventArgs.Empty);
                }
                else if (_bottomArrowRectangle.Contains(_mouseLocation))
                {
                    _bottomArrowClicked = true;
                    _buttonBottomState = MouseStates.Pressed;
                    Invalidate(_bottomArrowRectangle);
                    ProgressThumb(true);
                    OnButtonBottomClicked(EventArgs.Empty);
                }
                else
                {
                    _trackPosition = _orientation == Orientation.Vertical ? _mouseLocation.Y : _mouseLocation.X;

                    if (_trackPosition < (_orientation == Orientation.Vertical ? _thumbRectangle.Y : _thumbRectangle.X))
                    {
                        _topBarClicked = true;
                    }
                    else
                    {
                        _bottomBarClicked = true;
                    }

                    ProgressThumb(true);
                }
            }
            else if (e.Button == MouseButtons.Right)
            {
                _trackPosition = _orientation == Orientation.Vertical ? e.Y : e.X;
            }
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            _buttonTopState = MouseStates.Hover;
            _buttonBottomState = MouseStates.Hover;
            _thumbState = MouseStates.Normal;
            Invalidate();
        }

        protected override void OnMouseHover(EventArgs e)
        {
            base.OnMouseHover(e);
            MouseState = MouseStates.Hover;
            Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            MouseState = MouseStates.Normal;
            Invalidate();

            ResetScrollStatus();
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (e.Button == MouseButtons.Left)
            {
                if (_thumbClicked)
                {
                    int _oldScrollValue = _value;

                    _buttonTopState = MouseStates.Normal;
                    _buttonBottomState = MouseStates.Normal;
                    int pos = _orientation == Orientation.Vertical ? e.Location.Y : e.Location.X;

                    if (pos <= _thumbTopLimit + _thumbPosition)
                    {
                        ChangeThumbPosition(_thumbTopLimit);
                        _value = _minimum;
                    }
                    else if (pos >= _thumbBottomLimitTop + _thumbPosition)
                    {
                        ChangeThumbPosition(_thumbBottomLimitTop);
                        _value = _maximum;
                    }
                    else
                    {
                        ChangeThumbPosition(pos - _thumbPosition);

                        int _pixelRange;
                        int _thumbLocation;
                        int _arrowSize;

                        if (_orientation == Orientation.Vertical)
                        {
                            _pixelRange = Height - (2 * _arrowHeight) - _thumbHeight;
                            _thumbLocation = _thumbRectangle.Y;
                            _arrowSize = _arrowHeight;
                        }
                        else
                        {
                            _pixelRange = Width - (2 * _arrowWidth) - _thumbWidth;
                            _thumbLocation = _thumbRectangle.X;
                            _arrowSize = _arrowWidth;
                        }

                        var _percent = 0f;

                        if (_pixelRange != 0)
                        {
                            _percent = (_thumbLocation - _arrowSize) / (float)_pixelRange;
                        }

                        _value = Convert.ToInt32((_percent * (_maximum - _minimum)) + _minimum);
                    }

                    if (_oldScrollValue != _value)
                    {
                        OnScroll(new ScrollEventArgs(ScrollEventType.ThumbTrack, _oldScrollValue, _value, _scrollOrientation));
                        Refresh();
                    }
                }
            }
            else if (!ClientRectangle.Contains(e.Location))
            {
                ResetScrollStatus();
            }
            else if (e.Button == MouseButtons.None)
            {
                if (_topArrowRectangle.Contains(e.Location))
                {
                    _buttonTopState = MouseStates.Hover;
                    _buttonBottomState = MouseStates.Normal;
                    Invalidate(_topArrowRectangle);
                }
                else if (_bottomArrowRectangle.Contains(e.Location))
                {
                    _buttonTopState = MouseStates.Normal;
                    _buttonBottomState = MouseStates.Hover;
                    Invalidate(_bottomArrowRectangle);
                }
                else if (_thumbRectangle.Contains(e.Location))
                {
                    _thumbState = MouseStates.Hover;
                    Invalidate(_thumbRectangle);
                }
                else if (ClientRectangle.Contains(e.Location))
                {
                    _buttonTopState = MouseStates.Normal;
                    _buttonBottomState = MouseStates.Normal;
                    _thumbState = MouseStates.Normal;
                    Invalidate();
                }
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            MouseState = MouseState = MouseStates.Hover;
            Invalidate();

            if (e.Button == MouseButtons.Left)
            {
                ContextMenuStrip = _contextMenu;

                if (_thumbClicked)
                {
                    _thumbClicked = false;

                    _thumbState = MouseStates.Normal;
                    OnScroll(new ScrollEventArgs(ScrollEventType.EndScroll, -1, _value, _scrollOrientation));
                }
                else if (_topArrowClicked)
                {
                    _topArrowClicked = false;
                    _buttonTopState = MouseStates.Normal;
                    StopTimer();
                }
                else if (_bottomArrowClicked)
                {
                    _bottomArrowClicked = false;
                    _buttonBottomState = MouseStates.Normal;
                    StopTimer();
                }
                else if (_topBarClicked)
                {
                    _topBarClicked = false;
                    StopTimer();
                }
                else if (_bottomBarClicked)
                {
                    _bottomBarClicked = false;
                    StopTimer();
                }

                Invalidate();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            try
            {
                Rectangle _rectangle;
                if (_border.Type == ShapeTypes.Rounded)
                {
                    _rectangle = new Rectangle(ClientRectangle.X, ClientRectangle.Y, ClientRectangle.Width - 1, ClientRectangle.Height - 1);
                }
                else
                {
                    _rectangle = new Rectangle(ClientRectangle.X, ClientRectangle.Y, ClientRectangle.Width, ClientRectangle.Height);
                }

                VisualElementRenderer.DrawElement(e.Graphics, BackgroundImage, _border, _backColorState, Enabled, MouseState, _rectangle);
                VisualElementRenderer.DrawElement(e.Graphics, null, _thumbBorder, _thumbColorState, Enabled, _thumbState, _thumbRectangle);

                if (_thumbGripVisible)
                {
                    VisualScrollBarRenderer.DrawThumbGrip(e.Graphics, _thumbRectangle, _orientation);
                }

                VisualScrollBarRenderer.DrawArrowButton(e.Graphics, true, _buttonBorder, _buttonColorState, Enabled, _orientation, _topArrowRectangle, _buttonTopState);
                VisualScrollBarRenderer.DrawArrowButton(e.Graphics, false, _buttonBorder, _buttonColorState, Enabled, _orientation, _bottomArrowRectangle, _buttonBottomState);

                DrawClickedBar(e);
            }
            catch (Exception exception)
            {
                ConsoleEx.WriteDebug(exception);
            }
        }

        /// <summary>Raises the <see cref="Scroll" /> event.</summary>
        /// <param name="e">The <see cref="ScrollEventArgs" /> that contains the event data.</param>
        protected virtual void OnScroll(ScrollEventArgs e)
        {
            Scroll?.Invoke(this, e);
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            ConfigureScrollBar();
        }

        protected virtual void OnThumbClicked(EventArgs e)
        {
            ThumbClicked?.Invoke(e);
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            // key handling is here - keys recognized by the control
            // Up&Down or Left&Right, PageUp, PageDown, Home, End
            Keys keyUp = Keys.Up;
            Keys keyDown = Keys.Down;

            if (_orientation == Orientation.Horizontal)
            {
                keyUp = Keys.Left;
                keyDown = Keys.Right;
            }

            if (keyData == keyUp)
            {
                Value -= _smallChange;

                return true;
            }

            if (keyData == keyDown)
            {
                Value += _smallChange;

                return true;
            }

            if (keyData == Keys.PageUp)
            {
                Value = GetValue(false, true);

                return true;
            }

            if (keyData == Keys.PageDown)
            {
                if (_value + _largeChange > _maximum)
                {
                    Value = _maximum;
                }
                else
                {
                    Value += _largeChange;
                }

                return true;
            }

            if (keyData == Keys.Home)
            {
                Value = _minimum;

                return true;
            }

            if (keyData == Keys.End)
            {
                Value = _maximum;

                return true;
            }

            return base.ProcessDialogKey(keyData);
        }

        protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
        {
            // only in design mode - constrain size
            if (DesignMode)
            {
                if (_orientation == Orientation.Vertical)
                {
                    if (height < (2 * _arrowHeight) + 10)
                    {
                        height = (2 * _arrowHeight) + 10;
                    }

                    width = _defaultWidth;
                }
                else
                {
                    if (width < (2 * _arrowWidth) + 10)
                    {
                        width = (2 * _arrowWidth) + 10;
                    }

                    height = 19;
                }
            }

            base.SetBoundsCore(x, y, width, height, specified);

            if (DesignMode)
            {
                ConfigureScrollBar();
            }
        }

        /// <summary>Context menu handler.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments.</param>
        private void BottomClick(object sender, EventArgs e)
        {
            Value = _maximum;
        }

        /// <summary>Changes the displayed text of the context menu items dependent of the current <see cref="Orientation" />.</summary>
        private void ChangeContextMenuItems()
        {
            if (_orientation == Orientation.Vertical)
            {
                _tsmiTop.Text = @"Top";
                _tsmiBottom.Text = @"Bottom";
                _tsmiLargeDown.Text = @"Page down";
                _tsmiLargeUp.Text = @"Page up";
                _tsmiSmallDown.Text = @"Scroll down";
                _tsmiSmallUp.Text = @"Scroll up";
                _tsmiScrollHere.Text = @"Scroll here";
            }
            else
            {
                _tsmiTop.Text = @"Left";
                _tsmiBottom.Text = @"Right";
                _tsmiLargeDown.Text = @"Page left";
                _tsmiLargeUp.Text = @"Page right";
                _tsmiSmallDown.Text = @"Scroll right";
                _tsmiSmallUp.Text = @"Scroll left";
                _tsmiScrollHere.Text = @"Scroll here";
            }
        }

        /// <summary>Changes the position of the thumb.</summary>
        /// <param name="position">The new position.</param>
        private void ChangeThumbPosition(int position)
        {
            if (_orientation == Orientation.Vertical)
            {
                _thumbRectangle.Y = position;
            }
            else
            {
                _thumbRectangle.X = position;
            }
        }

        /// <summary>Sets up the scrollbar.</summary>
        private void ConfigureScrollBar()
        {
            if (_inUpdate)
            {
                return;
            }

            if (_orientation == Orientation.Vertical)
            {
                _arrowHeight = 17;
                _arrowWidth = 15;
                _thumbWidth = 15;
                _thumbHeight = GetThumbSize();

                _clickedBarRectangle = ClientRectangle;
                _clickedBarRectangle.Inflate(-1, -1);
                _clickedBarRectangle.Y += _arrowHeight;
                _clickedBarRectangle.Height -= _arrowHeight * 2;

                _channelRectangle = _clickedBarRectangle;

                _thumbRectangle = new Rectangle(ClientRectangle.X + 2, ClientRectangle.Y + _arrowHeight + 1, _thumbWidth, _thumbHeight);
                _topArrowRectangle = new Rectangle(ClientRectangle.X + 2, ClientRectangle.Y + 1, _arrowWidth, _arrowHeight);
                _bottomArrowRectangle = new Rectangle(ClientRectangle.X + 2, ClientRectangle.Bottom - _arrowHeight - 2, _arrowWidth, _arrowHeight);

                // Set the default starting thumb position.
                _thumbPosition = _thumbRectangle.Height / 2;

                // Set the bottom limit of the thumb's bottom border.
                _thumbBottomLimitBottom = ClientRectangle.Bottom - _arrowHeight - 2;

                // Set the bottom limit of the thumb's top border.
                _thumbBottomLimitTop = _thumbBottomLimitBottom - _thumbRectangle.Height;

                // Set the top limit of the thumb's top border.
                _thumbTopLimit = ClientRectangle.Y + _arrowHeight + 1;
            }
            else
            {
                _arrowHeight = 15;
                _arrowWidth = 17;
                _thumbHeight = 15;
                _thumbWidth = GetThumbSize();

                _clickedBarRectangle = ClientRectangle;
                _clickedBarRectangle.Inflate(-1, -1);
                _clickedBarRectangle.X += _arrowWidth;
                _clickedBarRectangle.Width -= _arrowWidth * 2;

                _channelRectangle = _clickedBarRectangle;

                _thumbRectangle = new Rectangle(ClientRectangle.X + _arrowWidth + 1, ClientRectangle.Y + 2, _thumbWidth, _thumbHeight - 1);
                _topArrowRectangle = new Rectangle(ClientRectangle.X + 1, ClientRectangle.Y + 2, _arrowWidth, _arrowHeight - 1);
                _bottomArrowRectangle = new Rectangle(ClientRectangle.Right - _arrowWidth - 2, ClientRectangle.Y + 2, _arrowWidth, _arrowHeight - 1);

                // Set the default starting thumb position.
                _thumbPosition = _thumbRectangle.Width / 2;

                // Set the bottom limit of the thumb's bottom border.
                _thumbBottomLimitBottom = ClientRectangle.Right - _arrowWidth - 2;

                // Set the bottom limit of the thumb's top border.
                _thumbBottomLimitTop = _thumbBottomLimitBottom - _thumbRectangle.Width;

                // Set the top limit of the thumb's top border.
                _thumbTopLimit = ClientRectangle.X + _arrowWidth + 1;
            }

            ChangeThumbPosition(GetThumbPosition());
            Refresh();
        }

        /// <summary>Draws the clicked bar rectangle.</summary>
        /// <param name="e">The paint event args.</param>
        private void DrawClickedBar(PaintEventArgs e)
        {
            if (_topBarClicked)
            {
                if (_orientation == Orientation.Vertical)
                {
                    _clickedBarRectangle.Y = _thumbTopLimit;
                    _clickedBarRectangle.Height = _thumbRectangle.Y - _thumbTopLimit;
                }
                else
                {
                    _clickedBarRectangle.X = _thumbTopLimit;
                    _clickedBarRectangle.Width = _thumbRectangle.X - _thumbTopLimit;
                }

                e.Graphics.FillRectangle(new SolidBrush(_trackPressed), _clickedBarRectangle);
            }
            else if (_bottomBarClicked)
            {
                if (_orientation == Orientation.Vertical)
                {
                    _clickedBarRectangle.Y = _thumbRectangle.Bottom + 1;
                    _clickedBarRectangle.Height = (_thumbBottomLimitBottom - _clickedBarRectangle.Y) + 1;
                }
                else
                {
                    _clickedBarRectangle.X = _thumbRectangle.Right + 1;
                    _clickedBarRectangle.Width = (_thumbBottomLimitBottom - _clickedBarRectangle.X) + 1;
                }

                e.Graphics.FillRectangle(new SolidBrush(_trackPressed), _clickedBarRectangle);
            }
        }

        /// <summary>Enables the timer.</summary>
        private void EnableTimer()
        {
            if (!_progressTimer.Enabled)
            {
                _progressTimer.Interval = 600;
                _progressTimer.Start();
            }
            else
            {
                _progressTimer.Interval = 10;
            }
        }

        /// <summary>Calculates the new thumb position.</summary>
        /// <returns>The new thumb position.</returns>
        private int GetThumbPosition()
        {
            int _pixelRange;
            int _arrowSize;

            if (_orientation == Orientation.Vertical)
            {
                _pixelRange = Height - (2 * _arrowHeight) - _thumbHeight;
                _arrowSize = _arrowHeight;
            }
            else
            {
                _pixelRange = Width - (2 * _arrowWidth) - _thumbWidth;
                _arrowSize = _arrowWidth;
            }

            int _realRange = _maximum - _minimum;
            var _percent = 0F;

            if (_realRange != 0)
            {
                _percent = (_value - (float)_minimum) / _realRange;
            }

            return Math.Max(_thumbTopLimit, Math.Min(_thumbBottomLimitTop, Convert.ToInt32((_percent * _pixelRange) + _arrowSize)));
        }

        /// <summary>Calculates the height of the thumb.</summary>
        /// <returns>The height of the thumb.</returns>
        private int GetThumbSize()
        {
            int _trackSize = _orientation == Orientation.Vertical ? Height - (2 * _arrowHeight) : Width - (2 * _arrowWidth);

            if ((_maximum == 0) || (_largeChange == 0))
            {
                return _trackSize;
            }

            float _newThumbSize = (_largeChange * (float)_trackSize) / _maximum;

            return Convert.ToInt32(Math.Min(_trackSize, Math.Max(_newThumbSize, 10f)));
        }

        /// <summary>Calculates the new value of the scrollbar.</summary>
        /// <param name="smallIncrement">true for a small change, false otherwise.</param>
        /// <param name="up">true for up movement, false otherwise.</param>
        /// <returns>The new scrollbar value.</returns>
        private int GetValue(bool smallIncrement, bool up)
        {
            int newValue;

            // calculate the new value of the scrollbar
            // with checking if new value is in bounds (min/max)
            if (up)
            {
                newValue = _value - (smallIncrement ? _smallChange : _largeChange);

                if (newValue < _minimum)
                {
                    newValue = _minimum;
                }
            }
            else
            {
                newValue = _value + (smallIncrement ? _smallChange : _largeChange);

                if (newValue > _maximum)
                {
                    newValue = _maximum;
                }
            }

            return newValue;
        }

        /// <summary>Initializes the context menu.</summary>
        private void InitializeComponent()
        {
            _components = new Container();
            _contextMenu = new ContextMenuStrip(_components);
            _tsmiScrollHere = new ToolStripMenuItem();
            _toolStripSeparator1 = new ToolStripSeparator();
            _tsmiTop = new ToolStripMenuItem();
            _tsmiBottom = new ToolStripMenuItem();
            _toolStripSeparator2 = new ToolStripSeparator();
            _tsmiLargeUp = new ToolStripMenuItem();
            _tsmiLargeDown = new ToolStripMenuItem();
            _toolStripSeparator3 = new ToolStripSeparator();
            _tsmiSmallUp = new ToolStripMenuItem();
            _tsmiSmallDown = new ToolStripMenuItem();
            _contextMenu.SuspendLayout();
            SuspendLayout();

            // contextMenu
            _contextMenu.Items.AddRange(new ToolStripItem[] { _tsmiScrollHere, _toolStripSeparator1, _tsmiTop, _tsmiBottom, _toolStripSeparator2, _tsmiLargeUp, _tsmiLargeDown, _toolStripSeparator3, _tsmiSmallUp, _tsmiSmallDown });
            _contextMenu.Name = "_contextMenu";
            _contextMenu.Size = new Size(151, 176);

            // tsmiScrollHere
            _tsmiScrollHere.DisplayStyle = ToolStripItemDisplayStyle.Text;
            _tsmiScrollHere.Name = "_tsmiScrollHere";
            _tsmiScrollHere.Size = new Size(150, 22);
            _tsmiScrollHere.Text = @"Scroll here";
            _tsmiScrollHere.Click += ScrollHereClick;

            // toolStripSeparator1
            _toolStripSeparator1.Name = "_toolStripSeparator1";
            _toolStripSeparator1.Size = new Size(147, 6);

            // tsmiTop
            _tsmiTop.DisplayStyle = ToolStripItemDisplayStyle.Text;
            _tsmiTop.Name = "_tsmiTop";
            _tsmiTop.Size = new Size(150, 22);
            _tsmiTop.Text = @"Top";
            _tsmiTop.Click += TopClick;

            // tsmiBottom
            _tsmiBottom.DisplayStyle = ToolStripItemDisplayStyle.Text;
            _tsmiBottom.Name = "_tsmiBottom";
            _tsmiBottom.Size = new Size(150, 22);
            _tsmiBottom.Text = @"Bottom";
            _tsmiBottom.Click += BottomClick;

            // toolStripSeparator2
            _toolStripSeparator2.Name = "_toolStripSeparator2";
            _toolStripSeparator2.Size = new Size(147, 6);

            // tsmiLargeUp
            _tsmiLargeUp.DisplayStyle = ToolStripItemDisplayStyle.Text;
            _tsmiLargeUp.Name = "_tsmiLargeUp";
            _tsmiLargeUp.Size = new Size(150, 22);
            _tsmiLargeUp.Text = @"Page up";
            _tsmiLargeUp.Click += LargeUpClick;

            // tsmiLargeDown
            _tsmiLargeDown.DisplayStyle = ToolStripItemDisplayStyle.Text;
            _tsmiLargeDown.Name = "_tsmiLargeDown";
            _tsmiLargeDown.Size = new Size(150, 22);
            _tsmiLargeDown.Text = @"Page down";
            _tsmiLargeDown.Click += LargeDownClick;

            // toolStripSeparator3
            _toolStripSeparator3.Name = "_toolStripSeparator3";
            _toolStripSeparator3.Size = new Size(147, 6);

            // tsmiSmallUp
            _tsmiSmallUp.DisplayStyle = ToolStripItemDisplayStyle.Text;
            _tsmiSmallUp.Name = "_tsmiSmallUp";
            _tsmiSmallUp.Size = new Size(150, 22);
            _tsmiSmallUp.Text = @"Scroll up";
            _tsmiSmallUp.Click += SmallUpClick;

            // tsmiSmallDown
            _tsmiSmallDown.DisplayStyle = ToolStripItemDisplayStyle.Text;
            _tsmiSmallDown.Name = "_tsmiSmallDown";
            _tsmiSmallDown.Size = new Size(150, 22);
            _tsmiSmallDown.Text = @"Scroll down";
            _tsmiSmallDown.Click += SmallDownClick;
            _contextMenu.ResumeLayout(false);
            ResumeLayout(false);
        }

        /// <summary>Context menu handler.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments.</param>
        private void LargeDownClick(object sender, EventArgs e)
        {
            Value = GetValue(false, false);
        }

        /// <summary>Context menu handler.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments.</param>
        private void LargeUpClick(object sender, EventArgs e)
        {
            Value = GetValue(false, true);
        }

        /// <summary>Controls the movement of the thumb.</summary>
        /// <param name="enableTimer">true for enabling the timer, false otherwise.</param>
        private void ProgressThumb(bool enableTimer)
        {
            int scrollOldValue = _value;
            ScrollEventType type = ScrollEventType.First;
            int thumbSize, thumbPos;

            if (_orientation == Orientation.Vertical)
            {
                thumbPos = _thumbRectangle.Y;
                thumbSize = _thumbRectangle.Height;
            }
            else
            {
                thumbPos = _thumbRectangle.X;
                thumbSize = _thumbRectangle.Width;
            }

            // arrow down or shaft down clicked
            if (_bottomArrowClicked || (_bottomBarClicked && (thumbPos + thumbSize < _trackPosition)))
            {
                type = _bottomArrowClicked ? ScrollEventType.SmallIncrement : ScrollEventType.LargeIncrement;

                _value = GetValue(_bottomArrowClicked, false);

                if (_value == _maximum)
                {
                    ChangeThumbPosition(_thumbBottomLimitTop);

                    type = ScrollEventType.Last;
                }
                else
                {
                    ChangeThumbPosition(Math.Min(_thumbBottomLimitTop, GetThumbPosition()));
                }
            }
            else if (_topArrowClicked || (_topBarClicked && (thumbPos > _trackPosition)))
            {
                type = _topArrowClicked ? ScrollEventType.SmallDecrement : ScrollEventType.LargeDecrement;

                // arrow up or shaft up clicked
                _value = GetValue(_topArrowClicked, true);

                if (_value == _minimum)
                {
                    ChangeThumbPosition(_thumbTopLimit);

                    type = ScrollEventType.First;
                }
                else
                {
                    ChangeThumbPosition(Math.Max(_thumbTopLimit, GetThumbPosition()));
                }
            }
            else if (!((_topArrowClicked && (thumbPos == _thumbTopLimit)) || (_bottomArrowClicked && (thumbPos == _thumbBottomLimitTop))))
            {
                ResetScrollStatus();

                return;
            }

            if (scrollOldValue != _value)
            {
                OnScroll(new ScrollEventArgs(type, scrollOldValue, _value, _scrollOrientation));

                Invalidate(_channelRectangle);

                if (enableTimer)
                {
                    EnableTimer();
                }
            }
            else
            {
                if (_topArrowClicked)
                {
                    type = ScrollEventType.SmallDecrement;
                }
                else if (_bottomArrowClicked)
                {
                    type = ScrollEventType.SmallIncrement;
                }

                OnScroll(new ScrollEventArgs(type, _value));
            }
        }

        /// <summary>Handles the updating of the thumb.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">An object that contains the event data.</param>
        private void ProgressTimerTick(object sender, EventArgs e)
        {
            ProgressThumb(true);
        }

        /// <summary>Resets the scroll status of the scrollbar.</summary>
        private void ResetScrollStatus()
        {
            Point _location = PointToClient(Cursor.Position);

            if (ClientRectangle.Contains(_location))
            {
                _buttonTopState = MouseStates.Hover;
                _buttonBottomState = MouseStates.Hover;
            }
            else
            {
                _buttonTopState = MouseStates.Normal;
                _buttonBottomState = MouseStates.Normal;
            }

            _thumbState = _thumbRectangle.Contains(_location) ? MouseStates.Hover : MouseStates.Normal;
            _bottomArrowClicked = _bottomBarClicked = _topArrowClicked = _topBarClicked = false;

            StopTimer();
            Refresh();
        }

        /// <summary>Context menu handler.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments.</param>
        private void ScrollHereClick(object sender, EventArgs e)
        {
            int _thumbSize;
            int _thumbLocation;
            int _arrowSize;
            int _size;

            if (_orientation == Orientation.Vertical)
            {
                _thumbSize = _thumbHeight;
                _arrowSize = _arrowHeight;
                _size = Height;

                ChangeThumbPosition(Math.Max(_thumbTopLimit, Math.Min(_thumbBottomLimitTop, _trackPosition - (_thumbRectangle.Height / 2))));

                _thumbLocation = _thumbRectangle.Y;
            }
            else
            {
                _thumbSize = _thumbWidth;
                _arrowSize = _arrowWidth;
                _size = Width;

                ChangeThumbPosition(Math.Max(_thumbTopLimit, Math.Min(_thumbBottomLimitTop, _trackPosition - (_thumbRectangle.Width / 2))));

                _thumbLocation = _thumbRectangle.X;
            }

            int _pixelRange = _size - (2 * _arrowSize) - _thumbSize;
            var _percentage = 0F;

            if (_pixelRange != 0)
            {
                _percentage = (_thumbLocation - _arrowSize) / (float)_pixelRange;
            }

            int _oldValue = _value;

            _value = Convert.ToInt32((_percentage * (_maximum - _minimum)) + _minimum);

            OnScroll(new ScrollEventArgs(ScrollEventType.ThumbPosition, _oldValue, _value, _scrollOrientation));
            Refresh();
        }

        /// <summary>Context menu handler.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments.</param>
        private void SmallDownClick(object sender, EventArgs e)
        {
            Value = GetValue(true, false);
        }

        /// <summary>Context menu handler.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments.</param>
        private void SmallUpClick(object sender, EventArgs e)
        {
            Value = GetValue(true, true);
        }

        /// <summary>Stops the progress timer.</summary>
        private void StopTimer()
        {
            _progressTimer.Stop();
        }

        /// <summary>Context menu handler.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments.</param>
        private void TopClick(object sender, EventArgs e)
        {
            Value = _minimum;
        }

        #endregion
    }
}