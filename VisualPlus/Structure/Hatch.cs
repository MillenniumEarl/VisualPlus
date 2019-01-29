#region License

// -----------------------------------------------------------------------------------------------------------
// 
// Name: Hatch.cs
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

using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;

using VisualPlus.Constants;
using VisualPlus.Localization;
using VisualPlus.TypeConverters;

#endregion

namespace VisualPlus.Structure
{
    [TypeConverter(typeof(VisualSettingsTypeConverter))]
    [ToolboxItem(false)]
    [DesignerCategory("code")]
    [ClassInterface(ClassInterfaceType.AutoDispatch)]
    [ComVisible(true)]
    [Description("The Hatch structure.")]
    public class Hatch
    {
        #region Fields

        private Color _backColor;
        private Color _foreColor;
        private Size _size;
        private HatchStyle _style;
        private bool _visible;

        #endregion

        #region Constructors and Destructors

        /// <summary>Initializes a new instance of the <see cref="Hatch" /> class.</summary>
        public Hatch()
        {
            Theme theme = new Theme(DefaultConstants.DefaultStyle);

            _visible = DefaultConstants.HatchVisible;
            _size = DefaultConstants.HatchSize;
            _style = DefaultConstants.HatchStyle;
            _backColor = theme.ColorPalette.HatchBackColor;
            _foreColor = Color.FromArgb(40, theme.ColorPalette.HatchForeColor);
        }

        /// <summary>Initializes a new instance of the <see cref="Hatch" /> class.</summary>
        /// <param name="visible">The visibility of the hatch.</param>
        /// <param name="size">The size of the hatch.</param>
        /// <param name="style">The style of the hatch.</param>
        /// <param name="backColor">The back Color.</param>
        /// <param name="foreColor">The fore Color.</param>
        public Hatch(bool visible, Size size, HatchStyle style, Color backColor, Color foreColor)
        {
            _visible = visible;
            _size = size;
            _style = style;
            _backColor = backColor;
            _foreColor = foreColor;
        }

        #endregion

        #region Public Properties

        [NotifyParentProperty(true)]
        [RefreshProperties(RefreshProperties.Repaint)]
        [Description(PropertyDescription.Color)]
        public Color BackColor
        {
            get
            {
                return _backColor;
            }

            set
            {
                _backColor = value;
            }
        }

        [NotifyParentProperty(true)]
        [RefreshProperties(RefreshProperties.Repaint)]
        [Description(PropertyDescription.Color)]
        public Color ForeColor
        {
            get
            {
                return _foreColor;
            }

            set
            {
                _foreColor = value;
            }
        }

        [NotifyParentProperty(true)]
        [RefreshProperties(RefreshProperties.Repaint)]
        [Description(PropertyDescription.Size)]
        public Size Size
        {
            get
            {
                return _size;
            }

            set
            {
                _size = value;
            }
        }

        [NotifyParentProperty(true)]
        [RefreshProperties(RefreshProperties.Repaint)]
        [Description(PropertyDescription.HatchStyle)]
        public HatchStyle Style
        {
            get
            {
                return _style;
            }

            set
            {
                _style = value;
            }
        }

        [NotifyParentProperty(true)]
        [RefreshProperties(RefreshProperties.Repaint)]
        [Description(PropertyDescription.Visible)]
        public bool Visible
        {
            get
            {
                return _visible;
            }

            set
            {
                _visible = value;
            }
        }

        #endregion
    }
}