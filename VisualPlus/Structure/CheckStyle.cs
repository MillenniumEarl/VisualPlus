#region License

// -----------------------------------------------------------------------------------------------------------
// 
// Name: CheckStyle.cs
// VisualPlus - The VisualPlus Framework (VPF) for WinForms .NET development.
// 
// Created: 10/12/2018 - 11:45 PM
// Last Modified: 02/01/2019 - 1:24 AM
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
using System.IO;

using VisualPlus.Enumerators;
using VisualPlus.Localization;
using VisualPlus.Renders;
using VisualPlus.TypeConverters;

#endregion

namespace VisualPlus.Structure
{
    [Description("The check style structure.")]
    [TypeConverter(typeof(VisualSettingsTypeConverter))]
    public class CheckStyle
    {
        #region Fields

        private bool _autoSize;
        private Rectangle _bounds;
        private char _character;
        private Font _characterFont;
        private CheckType _checkType;
        private Color _color;
        private Image _image;
        private int _shapeRounding;
        private ShapeTypes _shapeType;
        private float _thickness;

        #endregion

        #region Constructors and Destructors

        /// <summary>Initializes a new instance of the <see cref="CheckStyle" /> class.</summary>
        /// <param name="boundary">The boundary.</param>
        public CheckStyle(Rectangle boundary)
        {
            Theme theme = new Theme(Settings.DefaultValue.DefaultStyle);

            _color = theme.ColorPalette.Progress;

            _autoSize = true;
            _character = '✔';

            // _characterFont = styleManager.Theme.ColorPalette.Font;
            _characterFont = SystemFonts.DefaultFont;
            _checkType = CheckType.Character;

            _shapeRounding = Settings.DefaultValue.Rounding.BoxRounding;
            _shapeType = Settings.DefaultValue.BorderType;
            _thickness = 2.0F;

            Bitmap _bitmap = new Bitmap(Image.FromStream(new MemoryStream(Convert.FromBase64String(VisualToggleRenderer.GetBase64CheckImage()))));
            _image = _bitmap;
            _bounds = boundary;
        }

        #endregion

        #region Enums

        public enum CheckType
        {
            /// <summary>The character.</summary>
            Character,

            /// <summary>The checkmark.</summary>
            Checkmark,

            /// <summary>The image.</summary>
            Image,

            /// <summary>The shape.</summary>
            Shape
        }

        #endregion

        #region Public Properties

        [NotifyParentProperty(true)]
        [RefreshProperties(RefreshProperties.Repaint)]
        [Description(PropertyDescription.AutoSize)]
        public bool AutoSize
        {
            get
            {
                return _autoSize;
            }

            set
            {
                _autoSize = value;
            }
        }

        [NotifyParentProperty(true)]
        [RefreshProperties(RefreshProperties.Repaint)]
        [Description(PropertyDescription.Size)]
        public Rectangle Bounds
        {
            get
            {
                return _bounds;
            }

            set
            {
                _bounds = value;
            }
        }

        [NotifyParentProperty(true)]
        [RefreshProperties(RefreshProperties.Repaint)]
        [Description(PropertyDescription.Character)]
        public char Character
        {
            get
            {
                return _character;
            }

            set
            {
                _character = value;
            }
        }

        [NotifyParentProperty(true)]
        [RefreshProperties(RefreshProperties.Repaint)]
        [Description(PropertyDescription.Color)]
        public Color CheckColor
        {
            get
            {
                return _color;
            }

            set
            {
                _color = value;
            }
        }

        [NotifyParentProperty(true)]
        [RefreshProperties(RefreshProperties.Repaint)]
        [Description(PropertyDescription.Font)]
        public Font Font
        {
            get
            {
                return _characterFont;
            }

            set
            {
                _characterFont = value;
            }
        }

        [NotifyParentProperty(true)]
        [RefreshProperties(RefreshProperties.Repaint)]
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
            }
        }

        [NotifyParentProperty(true)]
        [RefreshProperties(RefreshProperties.Repaint)]
        [Description(PropertyDescription.Rounding)]
        public int ShapeRounding
        {
            get
            {
                return _shapeRounding;
            }

            set
            {
                _shapeRounding = value;
            }
        }

        [NotifyParentProperty(true)]
        [RefreshProperties(RefreshProperties.Repaint)]
        [Description(PropertyDescription.Type)]
        public ShapeTypes ShapeType
        {
            get
            {
                return _shapeType;
            }

            set
            {
                _shapeType = value;
            }
        }

        [NotifyParentProperty(true)]
        [RefreshProperties(RefreshProperties.Repaint)]
        [Description(PropertyDescription.CheckType)]
        public CheckType Style
        {
            get
            {
                return _checkType;
            }

            set
            {
                _checkType = value;
            }
        }

        [NotifyParentProperty(true)]
        [RefreshProperties(RefreshProperties.Repaint)]
        [Description(PropertyDescription.Thickness)]
        public float Thickness
        {
            get
            {
                return _thickness;
            }

            set
            {
                _thickness = value;
            }
        }

        #endregion
    }
}