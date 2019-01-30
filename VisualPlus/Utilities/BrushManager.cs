#region License

// -----------------------------------------------------------------------------------------------------------
// 
// Name: BrushManager.cs
// 
// Copyright (c) 2019 - 2019 VisualPlus <https://darkbyte7.github.io/VisualPlus/>
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

using System.Drawing;
using System.Drawing.Drawing2D;

#endregion

namespace VisualPlus.Utilities
{
    public sealed class BrushManager
    {
        #region Public Methods and Operators

        /// <summary>Creates a glow brush from the specified graphics path.</summary>
        /// <param name="center">The center color of path gradient.</param>
        /// <param name="surrounding">The array of colors correspond to the points in the path.</param>
        /// <param name="point">The focus point for the gradient offset.</param>
        /// <param name="graphicsPath">The graphics path.</param>
        /// <param name="wrapMode">The wrap mode.</param>
        /// <returns>The <see cref="Brush" />.</returns>
        public static Brush GlowBrush(Color center, Color[] surrounding, PointF point, GraphicsPath graphicsPath, WrapMode wrapMode = WrapMode.Clamp)
        {
            return new PathGradientBrush(graphicsPath) { CenterColor = center, SurroundColors = surrounding, FocusScales = point, WrapMode = wrapMode };
        }

        /// <summary>Creates a glow brush from the specified the specified points.</summary>
        /// <param name="center">The center color of path gradient.</param>
        /// <param name="surrounding">The array of colors correspond to the points in the path.</param>
        /// <param name="point">The focus point for the gradient offset.</param>
        /// <param name="wrapMode">The wrap mode.</param>
        /// <returns>The <see cref="Brush" />.</returns>
        public static Brush GlowBrush(Color center, Color[] surrounding, PointF[] point, WrapMode wrapMode = WrapMode.Clamp)
        {
            return new PathGradientBrush(point) { CenterColor = center, SurroundColors = surrounding, WrapMode = wrapMode };
        }

        /// <summary>Draws the hatch brush as an image and then converts it to a texture brush for scaling.</summary>
        /// <param name="brush">Hatch brush pattern.</param>
        /// <returns>The <see cref="TextureBrush" />.</returns>
        public static TextureBrush HatchTextureBrush(HatchBrush brush)
        {
            using (Bitmap _bitmap = new Bitmap(8, 8))
            using (Graphics graphics = Graphics.FromImage(_bitmap))
            {
                graphics.FillRectangle(brush, 0, 0, 8, 8);
                return new TextureBrush(_bitmap);
            }
        }

        #endregion
    }
}