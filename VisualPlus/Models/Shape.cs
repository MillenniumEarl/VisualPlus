﻿#region License

// -----------------------------------------------------------------------------------------------------------
//
// Name: Shape.cs
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

#endregion License

#region Namespace

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;

using VisualPlus.Attributes;
using VisualPlus.Constants;
using VisualPlus.Delegates;
using VisualPlus.Enumerators;
using VisualPlus.Events;
using VisualPlus.Localization;
using VisualPlus.TypeConverters;
using VisualPlus.Utilities;

#endregion Namespace

namespace VisualPlus.Models
{
    /// <summary>The <see cref="Shape" /> class.</summary>
    [TypeConverter(typeof(VisualSettingsTypeConverter))]
    [ToolboxItem(false)]
    [DesignerCategory("code")]
    [ClassInterface(ClassInterfaceType.AutoDispatch)]
    [ComVisible(true)]
    [DebuggerDisplay(DefaultConstants.DefaultDebuggerDisplay)]
    [Serializable]
    [Test("Move class to a structure.", Labels = new[] { Labels.Planned, Labels.Refactoring })]
    public class Shape
    {
        #region Fields

        private Color _color;
        private int _rounding;
        private ShapeTypes _shapeType;
        private int _thickness;
        private bool _visible;

        #endregion Fields

        #region Constructors and Destructors

        /// <summary>Initializes a new instance of the <see cref="Shape" /> class.</summary>
        public Shape()
        {
            Theme theme = new Theme(DefaultConstants.DefaultStyle);
            Color color = theme.ColorPalette.BorderNormal;
            ConstructShape(ShapeTypes.Rounded, color, DefaultConstants.Rounding.Default, DefaultConstants.BorderThickness, true);
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

        #endregion Constructors and Destructors

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

        #endregion Public Events

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

                var range = new Range<int>(value, DefaultConstants.MinimumRounding, DefaultConstants.MaximumRounding);

                // TODO: Improve handling of value rounding.
                if (false)
                {
                    _rounding = MathUtil.RoundToNearestValue(range.Value, range.Minimum, range.Maximum);
                }
                else
                {
                    _rounding = range.Value;
                }

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

                var range = new Range<int>(value, DefaultConstants.MinimumBorderSize, DefaultConstants.MaximumBorderSize);

                // TODO: Improve handling of value rounding.
                if (false)
                {
                    _thickness = MathUtil.RoundToNearestValue(range.Value, range.Minimum, range.Maximum);
                }
                else
                {
                    _thickness = range.Value;
                }

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

        #endregion Public Properties

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

        #endregion Methods
    }
}