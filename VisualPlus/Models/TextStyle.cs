#region License

// -----------------------------------------------------------------------------------------------------------
// 
// Name: TextStyle.cs
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
using System.Drawing.Text;

using VisualPlus.Constants;
using VisualPlus.Enumerators;
using VisualPlus.Interfaces;
using VisualPlus.Localization;
using VisualPlus.TypeConverters;

#endregion

namespace VisualPlus.Models
{
    [TypeConverter(typeof(VisualSettingsTypeConverter))]
    public class TextStyle : ITextColor
    {
        #region Fields

        private StringAlignment textAlignment;
        private ControlColorState textColorState;
        private StringAlignment textLineAlignment;
        private TextRenderingHint textRenderingHint;

        #endregion

        #region Constructors and Destructors

        /// <summary>Initializes a new instance of the <see cref="TextStyle" /> class.</summary>
        /// <param name="colorState">The color State.</param>
        public TextStyle(ControlColorState colorState) : this()
        {
            if (colorState.IsEmpty)
            {
                throw new ArgumentNullException(nameof(colorState));
            }

            textColorState = colorState;
        }

        /// <summary>Initializes a new instance of the <see cref="TextStyle" /> class.</summary>
        public TextStyle()
        {
            textColorState = new ControlColorState();
            textRenderingHint = DefaultConstants.TextRenderingHint;
            textAlignment = StringAlignment.Center;
            textLineAlignment = StringAlignment.Center;
        }

        #endregion

        #region Public Properties

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ControlColorState ColorState
        {
            get
            {
                return textColorState;
            }
        }

        [Category(PropertyCategory.Appearance)]
        [Description(PropertyDescription.Color)]
        public Color Disabled
        {
            get
            {
                return textColorState.Disabled;
            }

            set
            {
                textColorState.Disabled = value;
            }
        }

        [Category(PropertyCategory.Appearance)]
        [Description(PropertyDescription.Color)]
        public Color Enabled
        {
            get
            {
                return textColorState.Enabled;
            }

            set
            {
                textColorState.Enabled = value;
            }
        }

        [Category(PropertyCategory.Appearance)]
        [Description(PropertyDescription.Color)]
        public Color Hover
        {
            get
            {
                return textColorState.Hover;
            }

            set
            {
                textColorState.Hover = value;
            }
        }

        [Category(PropertyCategory.Appearance)]
        [Description(PropertyDescription.Color)]
        public Color Pressed
        {
            get
            {
                return textColorState.Pressed;
            }

            set
            {
                textColorState.Pressed = value;
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public StringFormat StringFormat
        {
            get
            {
                StringFormat stringFormat = new StringFormat { Alignment = textAlignment, LineAlignment = textLineAlignment };

                return stringFormat;
            }
        }

        [Category(PropertyCategory.Behavior)]
        [Description(PropertyDescription.Alignment)]
        public StringAlignment TextAlignment
        {
            get
            {
                return textAlignment;
            }

            set
            {
                textAlignment = value;
            }
        }

        [Category(PropertyCategory.Behavior)]
        [Description(PropertyDescription.Alignment)]
        public StringAlignment TextLineAlignment
        {
            get
            {
                return textLineAlignment;
            }

            set
            {
                textLineAlignment = value;
            }
        }

        [Category(PropertyCategory.Behavior)]
        [Description(PropertyDescription.Type)]
        public TextRenderingHint TextRenderingHint
        {
            get
            {
                return textRenderingHint;
            }

            set
            {
                textRenderingHint = value;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>Retrieves the color state.</summary>
        /// <param name="enabled">The enabled state.</param>
        /// <param name="mouseState">The mouse state.</param>
        /// <param name="textStyle">The text style.></param>
        /// <returns>The <see cref="Color" />.</returns>
        public static Color GetColorState(bool enabled, MouseStates mouseState, ITextColor textStyle)
        {
            Color _textColor;

            switch (mouseState)
            {
                case MouseStates.Normal:
                    {
                        _textColor = enabled ? textStyle.Enabled : textStyle.Disabled;
                        break;
                    }

                case MouseStates.Hover:
                    {
                        _textColor = enabled ? textStyle.Hover : textStyle.Disabled;
                        break;
                    }

                case MouseStates.Pressed:
                    {
                        _textColor = enabled ? textStyle.Pressed : textStyle.Disabled;
                        break;
                    }

                default:
                    {
                        throw new ArgumentOutOfRangeException(nameof(mouseState), mouseState, null);
                    }
            }

            return _textColor;
        }

        #endregion
    }
}