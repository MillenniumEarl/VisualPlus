#region License

// -----------------------------------------------------------------------------------------------------------
// 
// Name: VisualProgressRenderer.cs
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

using System.Drawing;
using System.Drawing.Drawing2D;

using VisualPlus.Managers;
using VisualPlus.Structure;

#endregion

namespace VisualPlus.Renders
{
    public sealed class VisualProgressRenderer
    {
        #region Public Methods and Operators

        /// <summary>Draws a hatch component on the specified path.</summary>
        /// <param name="graphics">The specified graphics to draw on.</param>
        /// <param name="hatch">The hatch type.</param>
        /// <param name="hatchPath">The hatch path to fill.</param>
        public static void RenderHatch(Graphics graphics, Hatch hatch, GraphicsPath hatchPath)
        {
            if (!hatch.Visible)
            {
                return;
            }

            HatchBrush hatchBrush = new HatchBrush(hatch.Style, hatch.ForeColor, hatch.BackColor);
            using (TextureBrush textureBrush = BrushManager.HatchTextureBrush(hatchBrush))
            {
                textureBrush.ScaleTransform(hatch.Size.Width, hatch.Size.Height);
                graphics.FillPath(textureBrush, hatchPath);
            }
        }

        #endregion
    }
}