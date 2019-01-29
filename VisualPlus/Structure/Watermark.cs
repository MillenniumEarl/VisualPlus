#region License

// -----------------------------------------------------------------------------------------------------------
// 
// Name: Watermark.cs
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

using VisualPlus.Constants;
using VisualPlus.Delegates;
using VisualPlus.Localization;
using VisualPlus.TypeConverters;

#endregion

namespace VisualPlus.Structure
{
    [Description("The watermark")]
    [TypeConverter(typeof(VisualSettingsTypeConverter))]
    public class Watermark
    {
        #region Fields

        private Color active;
        private SolidBrush brush;
        private Font font;
        private Color inactive;
        private string text;
        private bool visible;

        #endregion

        #region Constructors and Destructors

        /// <summary>Initializes a new instance of the <see cref="Watermark" /> class.</summary>
        public Watermark()
        {
            Theme theme = new Theme(DefaultConstants.DefaultStyle);

            active = theme.ColorPalette.WatermarkActive;
            inactive = theme.ColorPalette.WatermarkInactive;

            font = SystemFonts.DefaultFont;

            text = DefaultConstants.WatermarkText;
            visible = DefaultConstants.WatermarkVisible;

            brush = new SolidBrush(inactive);
        }

        #endregion

        #region Public Events

        [Category(EventCategory.PropertyChanged)]
        [Description("Occours when the active color property has changed.")]
        public event WatermarkActiveColorChangedEventHandler ActiveColorChanged;

        [Category(EventCategory.PropertyChanged)]
        [Description("Occours when the font property has changed.")]
        public event WatermarkFontChangedEventHandler FontChanged;

        [Category(EventCategory.PropertyChanged)]
        [Description("Occours when the inactive property has changed.")]
        public event WatermarkInactiveColorChangedEventHandler InactiveColorChanged;

        [Category(EventCategory.PropertyChanged)]
        [Description("Occours when the text property has changed.")]
        public event WatermarkTextChangedEventHandler TextChanged;

        [Category(EventCategory.PropertyChanged)]
        [Description("Occours when the visible property has changed.")]
        public event WatermarkVisibleChangedEventHandler VisibleChanged;

        #endregion

        #region Public Properties

        [Category(PropertyCategory.Appearance)]
        [Description(PropertyDescription.Color)]
        [NotifyParentProperty(true)]
        [RefreshProperties(RefreshProperties.Repaint)]
        public Color Active
        {
            get
            {
                return active;
            }

            set
            {
                if (active != value)
                {
                    active = value;
                    ActiveColorChanged?.Invoke();
                }
            }
        }

        /// <summary>The <see cref="Watermark" /> brush.</summary>
        [Browsable(false)]
        [NotifyParentProperty(true)]
        [RefreshProperties(RefreshProperties.Repaint)]
        public SolidBrush Brush
        {
            get
            {
                return brush;
            }

            set
            {
                brush = value;
            }
        }

        [Category(PropertyCategory.Appearance)]
        [Description(PropertyDescription.Font)]
        [NotifyParentProperty(true)]
        [RefreshProperties(RefreshProperties.Repaint)]
        public Font Font
        {
            get
            {
                return font;
            }

            set
            {
                if (!font.Equals(value))
                {
                    font = value;
                    FontChanged?.Invoke();
                }
            }
        }

        [Category(PropertyCategory.Appearance)]
        [Description(PropertyDescription.Color)]
        [NotifyParentProperty(true)]
        [RefreshProperties(RefreshProperties.Repaint)]
        public Color Inactive
        {
            get
            {
                return inactive;
            }

            set
            {
                if (inactive != value)
                {
                    inactive = value;
                    InactiveColorChanged?.Invoke();
                }
            }
        }

        [Category(PropertyCategory.Data)]
        [Description(PropertyDescription.Text)]
        [NotifyParentProperty(true)]
        [RefreshProperties(RefreshProperties.Repaint)]
        public string Text
        {
            get
            {
                return text;
            }

            set
            {
                if (text != value)
                {
                    text = value;
                    TextChanged?.Invoke();
                }
            }
        }

        [Category(PropertyCategory.Appearance)]
        [Description(PropertyDescription.Visible)]
        [NotifyParentProperty(true)]
        [RefreshProperties(RefreshProperties.Repaint)]
        public bool Visible
        {
            get
            {
                return visible;
            }

            set
            {
                if (visible != value)
                {
                    visible = value;
                    VisibleChanged?.Invoke();
                }
            }
        }

        #endregion
    }
}