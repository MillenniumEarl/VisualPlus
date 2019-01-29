#region License

// -----------------------------------------------------------------------------------------------------------
// 
// Name: ColorExtension.cs
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
using System.Drawing;

using VisualPlus.Utilities;

#endregion

namespace VisualPlus.Extensibility
{
    /// <summary>The collection of the <see cref="ColorExtension" /> class.</summary>
    public static class ColorExtension
    {
        #region Public Methods and Operators

        /// <summary>Converts the string from an HTML code to a <see cref="Color" />.</summary>
        /// <param name="color">The color.</param>
        /// <param name="withoutHash">The HTML color. (Don't include hash '#')</param>
        /// <returns>The <see cref="Color" />.</returns>
        public static Color FromHTML(this Color color, string withoutHash)
        {
            return ColorTranslator.FromHtml("#" + withoutHash);
        }

        /// <summary>Converts the <see cref="Color" /> mixture to a new <see cref="Color" />.</summary>
        /// <param name="colors">The colors.</param>
        /// <returns>The <see cref="Color" />.</returns>
        public static Color MixColors(this Color[] colors)
        {
            // Variables
            int red = 0;
            int green = 0;
            int blue = 0;

            // Loop thru each color
            foreach (Color color in colors)
            {
                red += color.R;
                green += color.B;
                blue += color.B;
            }

            return Color.FromArgb(red / colors.Length, green / colors.Length, blue / colors.Length);
        }

        /// <summary>Applies the alpha transparency value to the <see cref="Color" />.</summary>
        /// <param name="color">The color.</param>
        /// <param name="alpha">The alpha value.</param>
        /// <returns>The <see cref="Color" />.</returns>
        public static Color SetTransparency(this Color color, int alpha = 255)
        {
            // Safety Check
            if (Guard.IsOutOfRange(alpha, byte.MinValue, byte.MaxValue))
            {
                throw new ArgumentOutOfRangeException("The " + nameof(alpha) + " value must be inside the range of (0-255).");
            }

            return Color.FromArgb(alpha, color.R, color.G, color.B);
        }

        /// <summary>Converts the <see cref="Color" /> to an ARGB <see cref="string" />.</summary>
        /// <param name="color">The color.</param>
        /// <returns>The <see cref="string" />.</returns>
        public static string ToARGB(this Color color)
        {
            return $"ARGB:({color.A}, {color.R}, {color.G}, {color.B})";
        }

        /// <summary>Converts the <see cref="Color" /> to an HTML string.</summary>
        /// <param name="color">The color to convert to HTML code.</param>
        /// <returns>The <see cref="string" />.</returns>
        public static string ToHTML(this Color color)
        {
            // Check if color is transparent
            if (color == Color.Transparent)
            {
                return "#00FFFFFF";
            }
            else
            {
                return ColorTranslator.ToHtml(color);
            }
        }

        /// <summary>Converts the <see cref="Color" /> to a <see cref="Pen" />.</summary>
        /// <param name="color">The color.</param>
        /// <param name="size">The size.</param>
        /// <returns>The <see cref="Pen" />.</returns>
        public static Pen ToPen(this Color color, float size = 1)
        {
            return new Pen(ToSolidBrush(color), size);
        }

        /// <summary>Converts the <see cref="Color" /> to an RGB <see cref="string" />.</summary>
        /// <param name="color">The color.</param>
        /// <returns>The <see cref="string" />.</returns>
        public static string ToRGB(this Color color)
        {
            return $"RGB:({color.R}, {color.G}, {color.B})";
        }

        /// <summary>Converts the <see cref="Color" /> to an RGBA <see cref="string" />.</summary>
        /// <param name="color">The color.</param>
        /// <returns>The <see cref="string" />.</returns>
        public static string ToRGBA(this Color color)
        {
            return $"RGBA:({color.R}, {color.G}, {color.B}, {color.A})";
        }

        /// <summary>Converts the <see cref="Color" /> to a <see cref="SolidBrush" />.</summary>
        /// <param name="color">The color.</param>
        /// <returns>The <see cref="SolidBrush" />.</returns>
        public static SolidBrush ToSolidBrush(this Color color)
        {
            return new SolidBrush(color);
        }

        #endregion
    }
}