#region License

// -----------------------------------------------------------------------------------------------------------
// 
// Name: VisualPanel.cs
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
using System.Drawing.Drawing2D;
using System.Windows.Forms;

using VisualPlus.Events;
using VisualPlus.Interfaces;
using VisualPlus.Localization;
using VisualPlus.Models;
using VisualPlus.Renders;
using VisualPlus.Toolkit.VisualBase;
using VisualPlus.TypeConverters;
using VisualPlus.Utilities.Debugging;

#endregion

namespace VisualPlus.Toolkit.Controls.Layout
{
    [ToolboxItem(true)]
    [ToolboxBitmap(typeof(VisualPanel), "VisualPanel.bmp")]
    [DefaultEvent("Paint")]
    [DefaultProperty("Enabled")]
    [Description("The Visual Panel")]
    public class VisualPanel : NestedControlsBase, IThemeSupport
    {
        #region Fields

        private Border _border;

        #endregion

        #region Constructors and Destructors

        /// <summary>Initializes a new instance of the <see cref="VisualPanel" /> class.</summary>
        public VisualPanel()
        {
            Size = new Size(187, 117);
            Padding = new Padding(5, 5, 5, 5);
            _border = new Border();

            UpdateTheme(ThemeManager.Theme);
        }

        #endregion

        #region Public Properties

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

        #endregion

        #region Public Methods and Operators

        public void UpdateTheme(Theme theme)
        {
            try
            {
                _border.Color = theme.ColorPalette.BorderNormal;
                _border.HoverColor = theme.ColorPalette.BorderHover;

                ForeColor = theme.ColorPalette.TextEnabled;
                TextStyle.Enabled = theme.ColorPalette.TextEnabled;
                TextStyle.Disabled = theme.ColorPalette.TextDisabled;

                BackColorState.Enabled = theme.ColorPalette.Enabled;
                BackColorState.Disabled = theme.ColorPalette.Disabled;
            }
            catch (Exception e)
            {
                ConsoleEx.WriteDebug(e);
            }

            Invalidate();
            OnThemeChanged(this, new ThemeEventArgs(theme));
        }

        #endregion

        #region Methods

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics graphics = e.Graphics;
            graphics.SmoothingMode = SmoothingMode.HighQuality;

            Rectangle _clientRectangle = new Rectangle(ClientRectangle.X, ClientRectangle.Y, ClientRectangle.Width - 1, ClientRectangle.Height - 1);
            ControlGraphicsPath = VisualBorderRenderer.CreateBorderTypePath(_clientRectangle, _border);

            Color _backColor = Enabled ? BackColorState.Enabled : BackColorState.Disabled;
            VisualBackgroundRenderer.DrawBackground(e.Graphics, _backColor, BackgroundImage, MouseState, _clientRectangle, Border);
            VisualBorderRenderer.DrawBorderStyle(e.Graphics, _border, ControlGraphicsPath, MouseState);
        }

        #endregion
    }
}