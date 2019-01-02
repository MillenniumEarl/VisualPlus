#region License

// -----------------------------------------------------------------------------------------------------------
// 
// Name: ImageExtensions.cs
// VisualPlus - The VisualPlus Framework (VPF) for WinForms .NET development.
// 
// Created: 10/12/2018 - 11:45 PM
// Last Modified: 01/01/2019 - 11:41 PM
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
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;

#endregion

namespace VisualPlus.Extensibility
{
    public static class ImageExtensions
    {
        #region Public Methods and Operators

        /// <summary>Creates a Base64 string value from the image.</summary>
        /// <param name="image">The image.</param>
        /// <returns>The <see cref="string" />.</returns>
        public static string ToBase64(this Image image)
        {
            using (MemoryStream _base64 = new MemoryStream())
            {
                image.Save(_base64, image.RawFormat);
                image.Dispose();
                return Convert.ToBase64String(_base64.ToArray());
            }
        }

        /// <summary>Creates a pen from the image.</summary>
        /// <param name="image">The image.</param>
        /// <param name="width">The width.</param>
        /// <param name="startCap">The start cap.</param>
        /// <param name="endCap">The end cap.</param>
        /// <returns>The <see cref="Pen" />.</returns>
        public static Pen ToPen(this Image image, float width = 1, LineCap startCap = LineCap.Custom, LineCap endCap = LineCap.Custom)
        {
            using (TextureBrush _textureBrush = new TextureBrush(image))
            {
                return new Pen(_textureBrush, width) { StartCap = startCap, EndCap = endCap };
            }
        }

        /// <summary>Creates a texture brush from the image.</summary>
        /// <param name="image">The image.</param>
        /// <returns>The <see cref="TextureBrush" />.</returns>
        public static TextureBrush ToTextureBrush(this Image image)
        {
            return new TextureBrush(image);
        }

        /// <summary>Creates a texture brush from the image.</summary>
        /// <param name="image">The image.</param>
        /// <param name="rectangle">The rectangle boundaries.</param>
        /// <returns>The <see cref="TextureBrush" />.</returns>
        public static TextureBrush ToTextureBrush(this Image image, Rectangle rectangle)
        {
            return new TextureBrush(image, rectangle);
        }

        #endregion
    }
}