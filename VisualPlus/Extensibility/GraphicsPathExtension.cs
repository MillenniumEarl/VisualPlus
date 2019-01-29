#region License

// -----------------------------------------------------------------------------------------------------------
// 
// Name: GraphicsPathExtension.cs
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

using VisualPlus.Models;
using VisualPlus.Renders;

#endregion

namespace VisualPlus.Extensibility
{
    /// <summary>The collection of the <see cref="GraphicsPathExtension" /> class.</summary>
    public static class GraphicsPathExtension
    {
        #region Public Methods and Operators

        /// <summary>Converts the <see cref="GraphicsPath" /> to a border path.</summary>
        /// <param name="borderPath">The border path.</param>
        /// <param name="border">The border.</param>
        /// <returns>The <see cref="GraphicsPath" />.</returns>
        public static GraphicsPath ToBorderPath(this GraphicsPath borderPath, Border border)
        {
            return VisualBorderRenderer.CreateBorderTypePath(borderPath.GetBounds().ToRectangle(), border);
        }

        /// <summary>Converts the <see cref="Rectangle" /> to a <see cref="GraphicsPath" />.</summary>
        /// <param name="rectangle">The rectangle.</param>
        /// <returns>The <see cref="GraphicsPath" />.</returns>
        public static GraphicsPath ToGraphicsPath(this Rectangle rectangle)
        {
            GraphicsPath convertedPath = new GraphicsPath();
            convertedPath.AddRectangle(rectangle);
            return convertedPath;
        }

        #endregion
    }
}