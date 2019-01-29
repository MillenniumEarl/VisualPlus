#region License

// -----------------------------------------------------------------------------------------------------------
// 
// Name: VisualStyleBase.cs
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
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Forms;

using VisualPlus.Constants;
using VisualPlus.Delegates;
using VisualPlus.Enumerators;
using VisualPlus.Events;
using VisualPlus.Interfaces;
using VisualPlus.Localization;
using VisualPlus.Models;
using VisualPlus.Toolkit.Components;
using VisualPlus.TypeConverters;

#endregion

namespace VisualPlus.Toolkit.VisualBase
{
    [ToolboxItem(false)]
    [DesignerCategory("code")]
    [ClassInterface(ClassInterfaceType.AutoDispatch)]
    [ComVisible(true)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public class VisualStyleBase : VisualControlBase, IThemeManager
    {
        #region Fields

        private MouseStates _mouseState;
        private TextStyle _textStyle;
        private StyleManager _themeManager;

        #endregion

        #region Constructors and Destructors

        /// <summary>Initializes a new instance of the <see cref="VisualStyleBase" /> class.</summary>
        public VisualStyleBase()
        {
            // Allow transparent BackColor.
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);

            // Double buffering to reduce drawing flicker.
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);

            // Repaint entire control whenever resizing.
            SetStyle(ControlStyles.ResizeRedraw, true);

            UpdateStyles();
            Initialize();
        }

        #endregion

        #region Public Events

        [Category(EventCategory.Mouse)]
        [Description("Occours when the MouseState of the control has changed.")]
        public event MouseStateChangedEventHandler MouseStateChanged;

        [Category(EventCategory.PropertyChanged)]
        [Description("Occours when the text style of the control has changed.")]
        public event EventHandler TextStyleChanged;

        [Category(EventCategory.PropertyChanged)]
        [Description("Occours when the theme of the control has changed.")]
        public event ThemeChangedEventHandler ThemeChanged;

        #endregion

        #region Public Properties

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
                if (_mouseState == value)
                {
                    return;
                }

                _mouseState = value;
                OnMouseStateChanged(new MouseStateEventArgs(_mouseState));
            }
        }

        /// <summary>Gets or sets the <see cref="TextStyle" />.</summary>
        [Browsable(false)]
        [Category(PropertyCategory.Appearance)]
        [Description(PropertyDescription.TextStyle)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [TypeConverter(typeof(VisualSettingsTypeConverter))]
        public TextStyle TextStyle
        {
            get
            {
                return _textStyle;
            }

            set
            {
                if (_textStyle == null)
                {
                    return;
                }

                _textStyle = value;
                OnTextStyleChanged(new EventArgs());
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
                if ((_themeManager == null) || (_themeManager == value))
                {
                    return;
                }

                _themeManager = value;
            }
        }

        #endregion

        #region Properties

        /// <summary>Gets or sets the <see cref="GraphicsPath" />.</summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        internal GraphicsPath ControlGraphicsPath { get; set; }

        #endregion

        #region Methods

        /// <summary>Invokes the mouse state changed event.</summary>
        /// <param name="e">The event args.</param>
        protected virtual void OnMouseStateChanged(MouseStateEventArgs e)
        {
            Invalidate();
            MouseStateChanged?.Invoke(e);
        }

        /// <summary>Invokes the text style changed event.</summary>
        /// <param name="e">The event args.</param>
        protected virtual void OnTextStyleChanged(EventArgs e)
        {
            Invalidate();
            TextStyleChanged?.Invoke(this, e);
        }

        /// <summary>Invokes the theme changed event.</summary>
        /// <param name="e">The event args.</param>
        protected virtual void OnThemeChanged(ThemeEventArgs e)
        {
            Invalidate();
            ThemeChanged?.Invoke(e);
        }

        /// <summary>Initialize the base.</summary>
        private void Initialize()
        {
            DoubleBuffered = true;
            ResizeRedraw = true;

            _mouseState = MouseStates.Normal;
            _themeManager = new StyleManager(DefaultConstants.DefaultStyle);

            Theme theme = new Theme(DefaultConstants.DefaultStyle);

            ControlColorState controlState = new ControlColorState { Disabled = theme.ColorPalette.TextDisabled, Enabled = theme.ColorPalette.TextEnabled, Hover = theme.ColorPalette.TextHover, Pressed = theme.ColorPalette.TextPressed };

            _textStyle = new TextStyle(controlState);
        }

        #endregion
    }
}