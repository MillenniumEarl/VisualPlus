#region License

// -----------------------------------------------------------------------------------------------------------
// 
// Name: BitmapExtension.cs
// VisualPlus - The VisualPlus Framework (VPF) for WinForms .NET development.
// 
// Created: 10/12/2018 - 11:45 PM
// Last Modified: 22/01/2019 - 11:55 PM
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
    /// <summary>The <see cref="Bitmap" /> extensions collection.</summary>
    public static class BitmapExtension
    {
        #region Public Methods and Operators

        /// <summary>Filters the <see cref="Bitmap" /> using GrayScale.</summary>
        /// <param name="bitmap">The bitmap image.</param>
        /// <returns>The <see cref="Bitmap" />.</returns>
        public static Bitmap FilterGrayScale(this Bitmap bitmap)
        {
            // Constants
            const double RED_THRESHOLD = 0.3;
            const double GREEN_THRESHOLD = 0.59;
            const double BLUE_THRESHOLD = 0.11;

            // Create new gray-scaled bitmap image to work with using the original pixel size
            using (Bitmap filteredGrayScaleImage = new Bitmap(bitmap.Width, bitmap.Height))
            {
                // Loop thru the Y coordinates
                for (var y = 0; y < filteredGrayScaleImage.Height; y++)
                {
                    // Loop thru the X coordinates
                    for (var x = 0; x < filteredGrayScaleImage.Width; x++)
                    {
                        // Retrieve the color from the input bitmap pixels
                        Color pixelColor = bitmap.GetPixel(x, y);

                        // Calculate gray-scale value of the selected pixel
                        var pixelColorGrayScaleValue = (int)((pixelColor.R * RED_THRESHOLD) + (pixelColor.G * GREEN_THRESHOLD) + (pixelColor.B * BLUE_THRESHOLD));

                        // Update the color of the specified pixel in the bitmap
                        filteredGrayScaleImage.SetPixel(x, y, Color.FromArgb(pixelColorGrayScaleValue, pixelColorGrayScaleValue, pixelColorGrayScaleValue));
                    }
                }

                return filteredGrayScaleImage;
            }
        }

        #endregion
    }
}