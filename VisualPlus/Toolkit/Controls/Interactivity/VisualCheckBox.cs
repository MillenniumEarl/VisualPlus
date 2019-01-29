#region License

// -----------------------------------------------------------------------------------------------------------
// 
// Name: VisualCheckBox.cs
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

using VisualPlus.Constants;
using VisualPlus.Designer;
using VisualPlus.Events;
using VisualPlus.Interfaces;
using VisualPlus.Structure;
using VisualPlus.Toolkit.VisualBase;
using VisualPlus.Utilities.Debugging;

#endregion

namespace VisualPlus.Toolkit.Controls.Interactivity
{
    [ClassInterface(ClassInterfaceType.AutoDispatch)]
    [ComVisible(true)]
    [DefaultEvent("ToggleChanged")]
    [DefaultProperty("Checked")]
    [Description("The Visual CheckBox")]
    [Designer(typeof(VisualCheckBoxDesigner))]
    [ToolboxBitmap(typeof(VisualCheckBox), "VisualCheckBox.bmp")]
    [ToolboxItem(true)]
    public class VisualCheckBox : CheckBoxBase, IThemeSupport
    {
        #region Constructors and Destructors

        /// <summary>Initializes a new instance of the <see cref="VisualCheckBox" /> class.</summary>
        public VisualCheckBox()
        {
            Size = new Size(125, 23);

            Border = new Border { Rounding = DefaultConstants.Rounding.BoxRounding };

            CheckStyle = new CheckStyle(ClientRectangle) { Style = CheckStyle.CheckType.Checkmark };

            UpdateTheme(ThemeManager.Theme);
        }

        #endregion

        #region Public Methods and Operators

        public void UpdateTheme(Theme theme)
        {
            try
            {
                Border.Color = theme.ColorPalette.BorderNormal;
                Border.HoverColor = theme.ColorPalette.BorderHover;

                CheckStyle.CheckColor = theme.ColorPalette.Progress;

                ForeColor = theme.ColorPalette.TextEnabled;
                TextStyle.Enabled = theme.ColorPalette.TextEnabled;
                TextStyle.Disabled = theme.ColorPalette.TextDisabled;

                // Font = theme.ColorPalette.Font;
                BoxColorState.Enabled = theme.ColorPalette.Enabled;
                BoxColorState.Disabled = theme.ColorPalette.Disabled;
                BoxColorState.Hover = theme.ColorPalette.Hover;
                BoxColorState.Pressed = theme.ColorPalette.Pressed;
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
            Cursor = Cursors.Hand;
        }

        #endregion
    }
}