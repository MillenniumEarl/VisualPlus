#region License

// -----------------------------------------------------------------------------------------------------------
// 
// Name: ImageManager.cs
// VisualPlus - The VisualPlus Framework (VPF) for WinForms .NET development.
// 
// Created: 10/12/2018 - 11:45 PM
// Last Modified: 01/01/2019 - 11:49 PM
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
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Windows.Forms;

#endregion

namespace VisualPlus.Managers
{
    [Description("The image manager.")]
    public sealed class ImageManager
    {
        #region Public Methods and Operators

        /// <summary>Creates a bitmap with the color and size.</summary>
        /// <param name="fill">The fill color.</param>
        /// <param name="size">The bitmap size.</param>
        /// <returns>The <see cref="Bitmap" />.</returns>
        public static Bitmap CreateBitmap(Color fill, Size size)
        {
            Bitmap bitmap = new Bitmap(size.Width, size.Height);
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                graphics.FillRectangle(new SolidBrush(fill), 0, 0, size.Width, size.Height);
            }

            return bitmap;
        }

        /// <summary>Creates a gradient bitmap.</summary>
        /// <param name="size">The size of the gradient.</param>
        /// <param name="topLeft">The color for top-left.</param>
        /// <param name="topRight">The color for top-right.</param>
        /// <param name="bottomLeft">The color for bottom-left.</param>
        /// <param name="bottomRight">The color for bottom-right.</param>
        /// <returns>The <see cref="Bitmap" />.</returns>
        public static Image CreateGradientBitmap(Size size, Color topLeft, Color topRight, Color bottomLeft, Color bottomRight)
        {
            Bitmap _bitmap = new Bitmap(size.Width, size.Height);

            for (var i = 0; i < _bitmap.Width; i++)
            {
                Color _xColor = ColorManager.TransitionColor(int.Parse(Math.Round((i / (double)_bitmap.Width) * 100.0, 0).ToString(CultureInfo.CurrentCulture)), topLeft, topRight);
                for (var j = 0; j < _bitmap.Height; j++)
                {
                    Color _yColor = ColorManager.TransitionColor(int.Parse(Math.Round((j / (double)_bitmap.Height) * 100.0, 0).ToString(CultureInfo.CurrentCulture)), bottomLeft, bottomRight);
                    _bitmap.SetPixel(i, j, ColorManager.InsertColor(_xColor, _yColor));
                }
            }

            return _bitmap;
        }

        /// <summary>Create the image from a Base64 value.</summary>
        /// <param name="value">The Base64 value.</param>
        /// <returns>The <see cref="Image" />.</returns>
        public static Image DrawImageFromBase64(string value)
        {
            Image _image;
            using (MemoryStream _memoryStream = new MemoryStream(Convert.FromBase64String(value)))
            {
                _image = Image.FromStream(_memoryStream);
            }

            return _image;
        }

        /// <summary>Draws the image with a custom color overlay.</summary>
        /// <param name="graphics">The graphics to draw on.</param>
        /// <param name="rectangle">The rectangle.</param>
        /// <param name="image">The image.</param>
        /// <param name="color">The color.</param>
        public static void DrawImageWithColorOverlay(Graphics graphics, Rectangle rectangle, Image image, Color color)
        {
            float[][] _colorMatrix = { new[] { Convert.ToSingle(color.R / 255.0), 0f, 0f, 0f, 0f }, new[] { 0f, Convert.ToSingle(color.G / 255.0), 0f, 0f, 0f }, new[] { 0f, 0f, Convert.ToSingle(color.B / 255.0), 0f, 0f }, new[] { 0f, 0f, 0f, Convert.ToSingle(color.A / 255.0), 0f }, new[] { Convert.ToSingle(color.R / 255.0), Convert.ToSingle(color.G / 255.0), Convert.ToSingle(color.B / 255.0), 0f, Convert.ToSingle(color.A / 255.0) } };

            ImageAttributes _imageAttributes = new ImageAttributes();
            _imageAttributes.SetColorMatrix(new ColorMatrix(_colorMatrix), ColorMatrixFlag.Default, ColorAdjustType.Default);
            graphics.DrawImage(image, rectangle, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, _imageAttributes);
            image.Dispose();
        }

        /// <summary>Draws the image with a custom transparency.</summary>
        /// <param name="graphics">The graphics to draw on.</param>
        /// <param name="alpha">The alpha.</param>
        /// <param name="rectangle">The rectangle.</param>
        /// <param name="image">The image.</param>
        /// <param name="color">The color.</param>
        public static void DrawTransparentImage(Graphics graphics, float alpha, Rectangle rectangle, Image image, Color color)
        {
            ColorMatrix _colorMatrix = new ColorMatrix { Matrix33 = alpha };
            ImageAttributes _imageAttributes = new ImageAttributes();
            _imageAttributes.SetColorMatrix(_colorMatrix);
            graphics.DrawImage(image, new Rectangle(rectangle.X, rectangle.Y, image.Width, image.Height), rectangle.X, rectangle.Y, image.Width, image.Height, GraphicsUnit.Pixel, _imageAttributes);
            _imageAttributes.Dispose();
        }

        /// <summary>Sets the image opacity.</summary>
        /// <param name="image">The image.</param>
        /// <param name="opacity">The opacity value.</param>
        /// <returns>The <see cref="Image" />.</returns>
        public static Image SetOpacity(Image image, float opacity)
        {
            try
            {
                // create a Bitmap the size of the image provided  
                Bitmap bmp = new Bitmap(image.Width, image.Height);

                // create a graphics object from the image  
                using (Graphics gfx = Graphics.FromImage(bmp))
                {
                    // create a color matrix object  
                    ColorMatrix matrix = new ColorMatrix();

                    // set the opacity  
                    matrix.Matrix33 = opacity;

                    // create image attributes  
                    ImageAttributes attributes = new ImageAttributes();

                    // set the color(opacity) of the image  
                    attributes.SetColorMatrix(matrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

                    // now draw the image  
                    gfx.DrawImage(image, new Rectangle(0, 0, bmp.Width, bmp.Height), 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, attributes);
                }

                return bmp;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        #endregion
    }
}