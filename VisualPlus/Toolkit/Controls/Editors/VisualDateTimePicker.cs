#region License

// -----------------------------------------------------------------------------------------------------------
// 
// Name: VisualDateTimePicker.cs
// VisualPlus - The VisualPlus Framework (VPF) for WinForms .NET development.
// 
// Created: 10/12/2018 - 11:45 PM
// Last Modified: 02/01/2019 - 1:26 AM
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
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

using VisualPlus.Delegates;
using VisualPlus.Designer;
using VisualPlus.Enumerators;
using VisualPlus.Events;
using VisualPlus.Interfaces;
using VisualPlus.Localization;
using VisualPlus.Managers;
using VisualPlus.Renders;
using VisualPlus.Structure;
using VisualPlus.Toolkit.Components;
using VisualPlus.TypeConverters;

#endregion

namespace VisualPlus.Toolkit.Controls.Editors
{
    [ClassInterface(ClassInterfaceType.AutoDispatch)]
    [ComVisible(true)]
    [DefaultEvent("ValueChanged")]
    [DefaultProperty("Value")]
    [Description("The Visual DateTime Picker")]
    [ToolboxBitmap(typeof(VisualDateTimePicker), "VisualDateTimePicker.bmp")]
    [Designer(typeof(VisualDateTimePickerDesigner))]
    [ToolboxItem(true)]
    public class VisualDateTimePicker : DateTimePicker, IThemeSupport
    {
        #region Fields

        private Color _arrowColor;
        private Color _arrowDisabledColor;
        private Size _arrowSize;
        private ColorState _backColor;
        private Border _border;
        private Image _dropDownImage;
        private bool _focused;
        private Image _image;
        private Size _imageSize;
        private MouseStates _mouseState;
        private bool _showFocus;
        private StyleManager _themeManager;

        #endregion

        #region Constructors and Destructors

        /// <summary>Initializes a new instance of the <see cref="VisualDateTimePicker" /> class.</summary>
        public VisualDateTimePicker()
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.UserPaint, true);

            _arrowSize = new Size(10, 5);
            _imageSize = new Size(16, 16);

            _border = new Border();
            _mouseState = MouseStates.Normal;
            _themeManager = new StyleManager(Settings.DefaultValue.DefaultStyle);

            _image = null;
            _dropDownImage = null;

            _backColor = new ColorState();

            UpdateTheme(ThemeManager.Theme);
        }

        #endregion

        #region Public Events

        [Category(EventCategory.Mouse)]
        [Description("Occours when the MouseState of the control has changed.")]
        public event MouseStateChangedEventHandler MouseStateChanged;

        [Category(EventCategory.PropertyChanged)]
        [Description("Occours when the theme of the control has changed.")]
        public event ThemeChangedEventHandler ThemeChanged;

        #endregion

        #region Public Properties

        [Category(PropertyCategory.Appearance)]
        [Description(PropertyDescription.Color)]
        public Color ArrowColor
        {
            get
            {
                return _arrowColor;
            }

            set
            {
                _arrowColor = value;
                Invalidate();
            }
        }

        [Category(PropertyCategory.Appearance)]
        [Description(PropertyDescription.Color)]
        public Color ArrowDisabledColor
        {
            get
            {
                return _arrowDisabledColor;
            }

            set
            {
                _arrowDisabledColor = value;
                Invalidate();
            }
        }

        [Category(PropertyCategory.Appearance)]
        [Description(PropertyDescription.Size)]
        public Size ArrowSize
        {
            get
            {
                return _arrowSize;
            }

            set
            {
                _arrowSize = value;
            }
        }

        [Browsable(true)]
        public new Color BackColor
        {
            get
            {
                return base.BackColor;
            }

            set
            {
                base.BackColor = value;
            }
        }

        [TypeConverter(typeof(VisualSettingsTypeConverter))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public ColorState BackColorState
        {
            get
            {
                return _backColor;
            }

            set
            {
                if (value == _backColor)
                {
                    return;
                }

                _backColor = value;
                Invalidate();
            }
        }

        [Browsable(true)]
        [Category(PropertyCategory.Appearance)]
        [Description(PropertyDescription.Image)]
        public new Image BackgroundImage
        {
            get
            {
                return base.BackgroundImage;
            }

            set
            {
                base.BackgroundImage = value;
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

        [Category(PropertyCategory.Appearance)]
        [Description(PropertyDescription.Image)]
        public Image DropDownImage
        {
            get
            {
                return _dropDownImage;
            }

            set
            {
                _dropDownImage = value;
                Invalidate();
            }
        }

        [Browsable(true)]
        public new Color ForeColor
        {
            get
            {
                return base.ForeColor;
            }

            set
            {
                base.ForeColor = value;
            }
        }

        [Category(PropertyCategory.Appearance)]
        [Description(PropertyDescription.Image)]
        public Image Image
        {
            get
            {
                return _image;
            }

            set
            {
                _image = value;
                Invalidate();
            }
        }

        [Category(PropertyCategory.Appearance)]
        [Description(PropertyDescription.Size)]
        public Size ImageSize
        {
            get
            {
                return _imageSize;
            }

            set
            {
                _imageSize = value;
                Invalidate();
            }
        }

        [Browsable(false)]
        public bool IsPressed
        {
            get
            {
                return _mouseState == MouseStates.Pressed;
            }
        }

        /// <summary>Gets or sets the <see cref="MouseState" />.</summary>
        [Category(PropertyCategory.Appearance)]
        [Description(PropertyDescription.MouseState)]
        public MouseStates MouseState
        {
            get
            {
                return _mouseState;
            }

            set
            {
                if (value == _mouseState)
                {
                    return;
                }

                _mouseState = value;
                OnMouseStateChanged(new MouseStateEventArgs(_mouseState));
            }
        }

        [DefaultValue(false)]
        [Category(PropertyCategory.Appearance)]
        [Description(PropertyDescription.Toggle)]
        public bool ShowFocus
        {
            get
            {
                return _showFocus;
            }

            set
            {
                _showFocus = value;
            }
        }

        [Browsable(false)]
        public new bool ShowUpDown
        {
            get
            {
                return base.ShowUpDown;
            }

            set
            {
                base.ShowUpDown = value;
            }
        }

        /// <summary>Gets or sets the <see cref="StyleManager" />.</summary>
        [Browsable(false)]
        [Category(PropertyCategory.Appearance)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public StyleManager ThemeManager
        {
            get
            {
                return _themeManager;
            }

            set
            {
                if ((_themeManager == null) || (value == _themeManager))
                {
                    return;
                }

                _themeManager = value;
            }
        }

        [Browsable(false)]
        [Category(PropertyCategory.Behavior)]
        [DefaultValue(true)]
        public bool UseSelectable
        {
            get
            {
                return GetStyle(ControlStyles.Selectable);
            }

            set
            {
                SetStyle(ControlStyles.Selectable, value);
            }
        }

        #endregion

        #region Public Methods and Operators

        public override Size GetPreferredSize(Size proposedSize)
        {
            Size preferredSize;
            base.GetPreferredSize(proposedSize);

            using (Graphics graphics = CreateGraphics())
            {
                string measureText = Text.Length > 0 ? Text : "MeasureText";
                proposedSize = new Size(int.MaxValue, int.MaxValue);
                preferredSize = TextRenderer.MeasureText(graphics, measureText, Font, proposedSize, TextFormatFlags.Left | TextFormatFlags.LeftAndRightPadding | TextFormatFlags.VerticalCenter);
                preferredSize.Height += 10;
            }

            return preferredSize;
        }

        public void UpdateTheme(Theme theme)
        {
            try
            {
                _border.Color = theme.ColorPalette.BorderNormal;
                _border.HoverColor = theme.ColorPalette.BorderHover;

                _backColor.Enabled = theme.ColorPalette.Enabled;
                _backColor.Disabled = theme.ColorPalette.Disabled;

                ForeColor = theme.ColorPalette.TextEnabled;

                // Font = theme.ColorPalette.Font;
                _arrowColor = theme.ColorPalette.ElementEnabled;
                _arrowDisabledColor = theme.ColorPalette.ElementDisabled;
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

        protected override void OnEnter(EventArgs e)
        {
            _focused = true;
            _mouseState = MouseStates.Hover;
            Invalidate();

            base.OnEnter(e);
        }

        protected override void OnGotFocus(EventArgs e)
        {
            _focused = true;
            _mouseState = MouseStates.Hover;
            Invalidate();
            base.OnGotFocus(e);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                _mouseState = MouseStates.Hover;
                Invalidate();
            }

            base.OnKeyDown(e);
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            _mouseState = MouseStates.Normal;
            Invalidate();
            base.OnKeyUp(e);
        }

        protected override void OnLeave(EventArgs e)
        {
            _focused = false;
            _mouseState = MouseStates.Normal;
            Invalidate();
            base.OnLeave(e);
        }

        protected override void OnLostFocus(EventArgs e)
        {
            _focused = false;
            _mouseState = MouseStates.Normal;
            Invalidate();
            base.OnLostFocus(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _mouseState = MouseStates.Pressed;
                Invalidate();
            }

            base.OnMouseDown(e);
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            _mouseState = MouseStates.Hover;
            Invalidate();

            base.OnMouseEnter(e);
        }

        protected override void OnMouseHover(EventArgs e)
        {
            base.OnMouseHover(e);
            _mouseState = MouseStates.Hover;
            Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            _mouseState = MouseStates.Normal;
            Invalidate();

            base.OnMouseLeave(e);
        }

        /// <summary>Invokes the mouse state changed event.</summary>
        /// <param name="e">The event args.</param>
        protected virtual void OnMouseStateChanged(MouseStateEventArgs e)
        {
            Invalidate();
            MouseStateChanged?.Invoke(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            _mouseState = MouseStates.Hover;
            Invalidate();

            base.OnMouseUp(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            try
            {
                if (GetStyle(ControlStyles.AllPaintingInWmPaint))
                {
                    OnPaintBackground(e);
                }

                MinimumSize = new Size(0, GetPreferredSize(Size.Empty).Height);

                Rectangle _clientRectangle = new Rectangle(ClientRectangle.X, ClientRectangle.Y, ClientRectangle.Width - 1, ClientRectangle.Height - 1);
                GraphicsPath controlGraphicsPath = VisualBorderRenderer.CreateBorderTypePath(_clientRectangle, _border);
                Color backColorState = ColorState.BackColorState(_backColor, Enabled, MouseState);

                e.Graphics.SetClip(controlGraphicsPath);
                VisualBackgroundRenderer.DrawBackground(e.Graphics, backColorState, BackgroundImage, MouseState, _clientRectangle, _border);
                VisualBorderRenderer.DrawBorderStyle(e.Graphics, _border, controlGraphicsPath, _mouseState);
                Rectangle arrowRectangle = new Rectangle(Width - _arrowSize.Width - 5, (Height / 2) - (_arrowSize.Height / 2), _arrowSize.Width, _arrowSize.Height);

                if (_image != null)
                {
                    e.Graphics.DrawImage(_image, new Rectangle(arrowRectangle.Left - _image.Width - 2, (Height / 2) - (_image.Height / 2), _imageSize.Width, _imageSize.Height));
                }

                VisualElementRenderer.RenderTriangle(e.Graphics, _arrowColor, _arrowDisabledColor, Enabled, _dropDownImage, arrowRectangle, Alignment.Vertical.Down);

                var _check = 0;
                Rectangle checkBoxRectangle = new Rectangle(3, (Height / 2) - 6, 12, 12);

                if (ShowCheckBox)
                {
                    _check = 15;

                    if (Checked)
                    {
                        CheckBoxRenderer.DrawCheckBox(e.Graphics, checkBoxRectangle.Location, CheckBoxState.CheckedNormal);
                    }
                    else
                    {
                        CheckBoxRenderer.DrawCheckBox(e.Graphics, checkBoxRectangle.Location, CheckBoxState.UncheckedNormal);
                    }
                }

                Size textSize = TextManager.MeasureText(Text, Font);

                Rectangle textBoxRectangle = new Rectangle(2 + _check, (Height / 2) - (textSize.Height / 2), textSize.Width, textSize.Height);
                TextRenderer.DrawText(e.Graphics, Text, Font, textBoxRectangle, ForeColor, TextFormatFlags.Left | TextFormatFlags.VerticalCenter);

                if (_showFocus && _focused)
                {
                    ControlPaint.DrawFocusRectangle(e.Graphics, ClientRectangle);
                }

                e.Graphics.ResetClip();
            }
            catch
            {
                Invalidate();
            }
        }

        /// <summary>Invokes the theme changed event.</summary>
        /// <param name="e">The event args.</param>
        protected virtual void OnThemeChanged(ThemeEventArgs e)
        {
            ThemeChanged?.Invoke(e);
            Invalidate();
        }

        protected override void OnValueChanged(EventArgs e)
        {
            base.OnValueChanged(e);
            Invalidate();
        }

        #endregion
    }
}