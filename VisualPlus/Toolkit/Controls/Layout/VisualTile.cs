#region License

// -----------------------------------------------------------------------------------------------------------
// 
// Name: VisualTile.cs
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

using VisualPlus.Designer;
using VisualPlus.Enumerators;
using VisualPlus.Events;
using VisualPlus.Interfaces;
using VisualPlus.Localization;
using VisualPlus.Properties;
using VisualPlus.Renders;
using VisualPlus.Structure;
using VisualPlus.Toolkit.VisualBase;
using VisualPlus.TypeConverters;

#endregion

namespace VisualPlus.Toolkit.Controls.Layout
{
    /// <summary>The <see cref="VisualTile" /> control.</summary>
    [ClassInterface(ClassInterfaceType.AutoDispatch)]
    [ComVisible(true)]
    [DefaultEvent("Click")]
    [DefaultProperty("Text")]
    [Description("The Visual Tile")]
    [Designer(typeof(VisualTileDesigner))]
    [DesignerCategory("code")]
    [ToolboxBitmap(typeof(VisualTile), "VisualTile.bmp")]
    [ToolboxItem(true)]
    public class VisualTile : VisualStyleBase, IThemeSupport
    {
        #region Fields

        private ControlColorState backColorState;
        private BackgroundLayout backgroundLayout;
        private Image image;
        private Point offset;
        private TileType type;

        #endregion

        #region Constructors and Destructors

        /// <summary>Initializes a new instance of the <see cref="VisualTile" /> class.</summary>
        /// <param name="text">The text to display.</param>
        public VisualTile(string text) : this()
        {
            Text = text;
        }

        /// <summary>Initializes a new instance of the <see cref="VisualTile" /> class.</summary>
        public VisualTile()
        {
            Size = new Size(110, 80);

            backgroundLayout = BackgroundLayout.Stretch;
            backColorState = new ControlColorState();
            type = TileType.Text;
            image = Resources.VisualPlus;
            MouseState = MouseStates.Normal;
            offset = new Point(0, 0);

            UpdateTheme(ThemeManager.Theme);
        }

        #endregion

        #region Enums

        public enum TileType
        {
            /// <summary>The image.</summary>
            Image,

            /// <summary>The text.</summary>
            Text
        }

        #endregion

        #region Public Properties

        public new Color BackColor
        {
            get
            {
                base.BackColor = BackColorState.Enabled;
                return base.BackColor;
            }

            set
            {
                BackColorState.Enabled = value;
                base.BackColor = BackColorState.Enabled;
            }
        }

        [TypeConverter(typeof(VisualSettingsTypeConverter))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public ControlColorState BackColorState
        {
            get
            {
                return backColorState;
            }

            set
            {
                if (value == backColorState)
                {
                    return;
                }

                backColorState = value;
                Invalidate();
            }
        }

        [Category(PropertyCategory.Appearance)]
        [Description(PropertyDescription.Type)]
        public new BackgroundLayout BackgroundImageLayout
        {
            get
            {
                return backgroundLayout;
            }

            set
            {
                if (backgroundLayout == value)
                {
                    return;
                }

                backgroundLayout = value;
                Invalidate();
            }
        }

        public new Color ForeColor
        {
            get
            {
                base.ForeColor = TextStyle.Enabled;
                return base.ForeColor;
            }

            set
            {
                TextStyle.Enabled = value;
                base.ForeColor = TextStyle.Enabled;
            }
        }

        [Category(PropertyCategory.Appearance)]
        [Description(PropertyDescription.Image)]
        public Image Image
        {
            get
            {
                return image;
            }

            set
            {
                image = value;
            }
        }

        [Category(PropertyCategory.Layout)]
        [Description(PropertyDescription.Point)]
        public Point Offset
        {
            get
            {
                return offset;
            }

            set
            {
                offset = value;
            }
        }

        [Browsable(true)]
        [Category(PropertyCategory.Appearance)]
        [Description(PropertyDescription.TextStyle)]
        public new TextStyle TextStyle
        {
            get
            {
                return base.TextStyle;
            }

            set
            {
                base.TextStyle = value;
            }
        }

        [Category(PropertyCategory.Appearance)]
        [Description(PropertyDescription.Type)]
        public TileType Type
        {
            get
            {
                return type;
            }

            set
            {
                if (type != value)
                {
                    type = value;
                    Invalidate();
                }
            }
        }

        #endregion

        #region Public Methods and Operators

        public void UpdateTheme(Theme theme)
        {
            try
            {
                backColorState = new ControlColorState { Disabled = theme.ColorPalette.ControlDisabled, Enabled = theme.ColorPalette.ControlEnabled, Hover = theme.ColorPalette.ItemHover, Pressed = theme.ColorPalette.Pressed };

                TextStyle.Enabled = theme.ColorPalette.TextEnabled;
                TextStyle.Disabled = theme.ColorPalette.TextDisabled;
                TextStyle.Hover = theme.ColorPalette.TextHover;
                TextStyle.Pressed = theme.ColorPalette.TextPressed;
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

        protected override void OnMouseDown(MouseEventArgs e)
        {
            MouseState = MouseStates.Pressed;
            Focus();
            base.OnMouseDown(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            Cursor = Cursors.Default;
            MouseState = MouseStates.Normal;
            Invalidate();
            base.OnMouseLeave(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (Enabled)
            {
                Cursor = Cursors.Hand;
            }
            else
            {
                Cursor = Cursors.Default;
            }

            MouseState = MouseStates.Hover;
            base.OnMouseMove(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (Enabled)
            {
                Cursor = Cursors.Hand;
            }
            else
            {
                Cursor = Cursors.Default;
            }

            base.OnMouseUp(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            try
            {
                Color backColor = ControlColorState.BackColorState(backColorState, Enabled, MouseState);

                VisualBackgroundRenderer.RenderBackground(e.Graphics, backColor, BackgroundImage, backgroundLayout, ClientRectangle);
                VisualTileRenderer.RenderTile(e.Graphics, type, ClientRectangle, Image, Text, Font, Enabled, MouseState, TextStyle, offset);
            }
            catch (Exception exception)
            {
                ConsoleEx.WriteDebug(exception);
            }

            base.OnPaint(e);
        }

        #endregion
    }
}