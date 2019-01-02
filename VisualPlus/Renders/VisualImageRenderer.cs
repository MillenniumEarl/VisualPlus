#region License

// -----------------------------------------------------------------------------------------------------------
// 
// Name: VisualImageRenderer.cs
// VisualPlus - The VisualPlus Framework (VPF) for WinForms .NET development.
// 
// Created: 10/12/2018 - 11:45 PM
// Last Modified: 01/01/2019 - 11:59 PM
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

#endregion

namespace VisualPlus.Renders
{
    public sealed class VisualImageRenderer
    {
        #region Public Methods and Operators

        /// <summary>Renders the full image.</summary>
        /// <param name="graphics">The specified graphics to draw on.</param>
        /// <param name="image">The image to draw.</param>
        public static void RenderImage(Graphics graphics, Image image)
        {
            graphics.DrawImage(image, new Point(0, 0));
        }

        /// <summary>Render the image in the center of the client rectangle using the image size.</summary>
        /// <param name="graphics">The specified graphics to draw on.</param>
        /// <param name="clientRectangle">The client rectangle.</param>
        /// <param name="image">The image to draw.</param>
        /// <param name="offset">The location offset.</param>
        public static void RenderImageCentered(Graphics graphics, Rectangle clientRectangle, Image image, Point offset = new Point())
        {
            Point _location = new Point(((clientRectangle.Width / 2) - (image.Width / 2)) + offset.X, ((clientRectangle.Height / 2) - (image.Height / 2)) + offset.Y);
            graphics.DrawImage(image, _location);
        }

        /// <summary>
        ///     Render the image in the center of the client rectangle using the client size to make the image fit if it's too
        ///     large.
        /// </summary>
        /// <param name="graphics">The specified graphics to draw on.</param>
        /// <param name="clientRectangle">The client rectangle.</param>
        /// <param name="image">The image to draw.</param>
        public static void RenderImageCenteredFit(Graphics graphics, Rectangle clientRectangle, Image image)
        {
            Rectangle centerRectangle = new Rectangle { Location = new Point(clientRectangle.Width / 4, clientRectangle.Height / 4), Size = new Size(clientRectangle.Width / 2, clientRectangle.Height / 2) };

            graphics.DrawImage(image, centerRectangle);
        }

        /// <summary>Render the image.</summary>
        /// <param name="graphics">The specified graphics to draw on.</param>
        /// <param name="clientRectangle">The client rectangle.</param>
        /// <param name="image">The image to draw.</param>
        public static void RenderImageFilled(Graphics graphics, Rectangle clientRectangle, Image image)
        {
            if (graphics == null)
            {
                throw new ArgumentNullException(nameof(graphics));
            }

            if (image == null)
            {
                throw new ArgumentNullException(nameof(image));
            }

            graphics.DrawImage(image, clientRectangle);
        }

        #endregion
    }
}