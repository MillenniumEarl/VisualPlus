#region License

// -----------------------------------------------------------------------------------------------------------
// 
// Name: Shape.cs
// VisualPlus - The VisualPlus Framework (VPF) for WinForms .NET development.
// 
// Created: 10/12/2018 - 11:45 PM
// Last Modified: 02/01/2019 - 12:09 AM
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

using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;

using VisualPlus.Constants;
using VisualPlus.Delegates;
using VisualPlus.Enumerators;
using VisualPlus.Events;
using VisualPlus.Localization;
using VisualPlus.Managers;
using VisualPlus.TypeConverters;

#endregion

namespace VisualPlus.Structure
{
    [TypeConverter(typeof(VisualSettingsTypeConverter))]
    [ToolboxItem(false)]
    [DesignerCategory("code")]
    [ClassInterface(ClassInterfaceType.AutoDispatch)]
    [ComVisible(true)]
    [Description("The shape.")]
    public class Shape
    {
        #region Fields

        private Color _color;
        private int _rounding;
        private ShapeTypes _shapeType;
        private int _thickness;
        private bool _visible;

        #endregion

        #region Constructors and Destructors

        /// <summary>Initializes a new instance of the <see cref="Shape" /> class.</summary>
        public Shape()
        {
            Theme theme = new Theme(Settings.DefaultValue.DefaultStyle);
            Color color = theme.ColorPalette.BorderNormal;
            ConstructShape(ShapeTypes.Rounded, color, Settings.DefaultValue.Rounding.Default, Settings.DefaultValue.BorderThickness, true);
        }

        /// <summary>Initializes a new instance of the <see cref="Shape" /> class.</summary>
        /// <param name="shapeType">The shape type.</param>
        /// <param name="color">The color.</param>
        /// <param name="rounding">The rounding.</param>
        public Shape(ShapeTypes shapeType, Color color, int rounding) : this()
        {
            ConstructShape(shapeType, color, rounding, _thickness, _visible);
        }

        /// <summary>Initializes a new instance of the <see cref="Shape" /> class.</summary>
        /// <param name="shapeType">The shape type.</param>
        /// <param name="color">The color.</param>
        /// <param name="rounding">The rounding.</param>
        /// <param name="thickness">The thickness.</param>
        public Shape(ShapeTypes shapeType, Color color, int rounding, int thickness) : this()
        {
            ConstructShape(shapeType, color, rounding, thickness, _visible);
        }

        /// <summary>Initializes a new instance of the <see cref="Shape" /> class.</summary>
        /// <param name="shapeType">The shape type.</param>
        /// <param name="color">The color.</param>
        /// <param name="rounding">The rounding.</param>
        /// <param name="thickness">The thickness.</param>
        /// <param name="visible">The visibility.</param>
        public Shape(ShapeTypes shapeType, Color color, int rounding, int thickness, bool visible)
        {
            ConstructShape(shapeType, color, rounding, thickness, visible);
        }

        #endregion

        #region Public Events

        [Category(EventCategory.PropertyChanged)]
        [Description(EventDescription.PropertyEventChanged)]
        public event BorderColorChangedEventHandler ColorChanged;

        [Category(EventCategory.PropertyChanged)]
        [Description(EventDescription.PropertyEventChanged)]
        public event BorderRoundingChangedEventHandler RoundingChanged;

        [Category(EventCategory.PropertyChanged)]
        [Description(EventDescription.PropertyEventChanged)]
        public event BorderThicknessChangedEventHandler ThicknessChanged;

        [Category(EventCategory.PropertyChanged)]
        [Description(EventDescription.PropertyEventChanged)]
        public event BorderTypeChangedEventHandler TypeChanged;

        [Category(EventCategory.PropertyChanged)]
        [Description(EventDescription.PropertyEventChanged)]
        public event BorderVisibleChangedEventHandler VisibleChanged;

        #endregion

        #region Public Properties

        /// <summary>Gets the distance from the rounded border.</summary>
        [Browsable(false)]
        public int BorderCurve
        {
            get
            {
                return (_rounding / 2) + _thickness + 1;
            }
        }

        [NotifyParentProperty(true)]
        [RefreshProperties(RefreshProperties.Repaint)]
        [Description(PropertyDescription.Color)]
        public Color Color
        {
            get
            {
                return _color;
            }

            set
            {
                _color = value;
                ColorChanged?.Invoke(new ColorEventArgs(_color));
            }
        }

        /// <summary>Gets the <see cref="Shape" /> display distance based on thickness and visibility.</summary>
        [Browsable(false)]
        public int Distance
        {
            get
            {
                return _visible ? _thickness : 0;
            }
        }

        [NotifyParentProperty(true)]
        [RefreshProperties(RefreshProperties.Repaint)]
        [Description(PropertyDescription.Rounding)]
        public int Rounding
        {
            get
            {
                return _rounding;
            }

            set
            {
                if (_rounding == value)
                {
                    return;
                }

                _rounding = ExceptionManager.ArgumentOutOfRangeException(value, SettingConstants.MinimumRounding, SettingConstants.MaximumRounding, true);
                RoundingChanged?.Invoke();
            }
        }

        [NotifyParentProperty(true)]
        [RefreshProperties(RefreshProperties.Repaint)]
        [Description(PropertyDescription.Thickness)]
        public int Thickness
        {
            get
            {
                return _thickness;
            }

            set
            {
                if (_thickness == value)
                {
                    return;
                }

                _thickness = ExceptionManager.ArgumentOutOfRangeException(value, SettingConstants.MinimumBorderSize, SettingConstants.MaximumBorderSize, true);
                ThicknessChanged?.Invoke();
            }
        }

        [NotifyParentProperty(true)]
        [RefreshProperties(RefreshProperties.Repaint)]
        [Description(PropertyDescription.Shape)]
        public ShapeTypes Type
        {
            get
            {
                return _shapeType;
            }

            set
            {
                _shapeType = value;
                TypeChanged?.Invoke();
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
                VisibleChanged?.Invoke();
            }
        }

        #endregion

        #region Methods

        /// <summary>Constructs the shape.</summary>
        /// <param name="shapeType">The shape type.</param>
        /// <param name="color">The color.</param>
        /// <param name="rounding">The rounding.</param>
        /// <param name="thickness">The thickness.</param>
        /// <param name="visible">The visibility.</param>
        private void ConstructShape(ShapeTypes shapeType, Color color, int rounding, int thickness, bool visible)
        {
            _color = color;
            _rounding = rounding;
            _thickness = thickness;
            _shapeType = shapeType;
            _visible = visible;
        }

        #endregion
    }
}