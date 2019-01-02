#region License

// -----------------------------------------------------------------------------------------------------------
// 
// Name: RectangleExtension.cs
// VisualPlus - The VisualPlus Framework (VPF) for WinForms .NET development.
// 
// Created: 10/12/2018 - 11:45 PM
// Last Modified: 02/01/2019 - 1:23 AM
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

using System.Drawing;

#endregion

namespace VisualPlus.Extensibility
{
    public static class RectangleExtension
    {
        #region Public Methods and Operators

        /// <summary>Aligns the rectangle to the bottom.</summary>
        /// <param name="rectangle">The rectangle.</param>
        /// <param name="outerBounds">The outside rectangle.</param>
        /// <param name="spacing">The spacing.</param>
        /// <returns>The <see cref="Rectangle" />.</returns>
        public static Rectangle AlignBottom(this Rectangle rectangle, Rectangle outerBounds, int spacing)
        {
            return new Rectangle(rectangle.X, outerBounds.Height - spacing - rectangle.Height, rectangle.Width, rectangle.Height);
        }

        /// <summary>Aligns the rectangle to the center.</summary>
        /// <param name="rectangle">The rectangle.</param>
        /// <param name="outerBounds">The outside rectangle.</param>
        /// <returns>The <see cref="Rectangle" />.</returns>
        public static Rectangle AlignCenterX(this Rectangle rectangle, Rectangle outerBounds)
        {
            return new Rectangle((outerBounds.Width / 2) - (rectangle.Width / 2), rectangle.Y, rectangle.Width, rectangle.Height);
        }

        /// <summary>Aligns the rectangle to the center height.</summary>
        /// <param name="rectangle">The rectangle.</param>
        /// <param name="outerBounds">The outside rectangle.</param>
        /// <returns>The <see cref="Rectangle" />.</returns>
        public static Rectangle AlignCenterY(this Rectangle rectangle, Rectangle outerBounds)
        {
            return new Rectangle(rectangle.X, (outerBounds.Height / 2) - (rectangle.Height / 2), rectangle.Width, rectangle.Height);
        }

        /// <summary>Aligns the rectangle to the left.</summary>
        /// <param name="rectangle">The rectangle.</param>
        /// <param name="outerBounds">The outside rectangle.</param>
        /// <param name="spacing">The spacing.</param>
        /// <returns>The <see cref="Rectangle" />.</returns>
        public static Rectangle AlignLeft(this Rectangle rectangle, Rectangle outerBounds, int spacing)
        {
            return new Rectangle(outerBounds.X + spacing, rectangle.Y, rectangle.Width, rectangle.Height);
        }

        /// <summary>Aligns the rectangle to the right.</summary>
        /// <param name="rectangle">The rectangle.</param>
        /// <param name="outerBounds">The outside rectangle.</param>
        /// <param name="spacing">The spacing.</param>
        /// <returns>The <see cref="Rectangle" />.</returns>
        public static Rectangle AlignRight(this Rectangle rectangle, Rectangle outerBounds, int spacing)
        {
            return new Rectangle(outerBounds.Width - spacing - rectangle.Width, rectangle.Y, rectangle.Width, rectangle.Height);
        }

        /// <summary>Aligns the rectangle to the top.</summary>
        /// <param name="rectangle">The rectangle.</param>
        /// <param name="outerBounds">The outside rectangle.</param>
        /// <param name="spacing">The spacing.</param>
        /// <returns>The <see cref="Rectangle" />.</returns>
        public static Rectangle AlignTop(this Rectangle rectangle, Rectangle outerBounds, int spacing)
        {
            return new Rectangle(rectangle.X, outerBounds.Y + spacing, rectangle.Width, rectangle.Height);
        }

        /// <summary>Rounds a RectangleF to a Rectangle.</summary>
        /// <param name="rectangleF">The rectangleF.</param>
        /// <returns>The <see cref="Rectangle" />.</returns>
        public static Rectangle ToRectangle(this RectangleF rectangleF)
        {
            return Rectangle.Round(rectangleF);
        }

        #endregion
    }
}