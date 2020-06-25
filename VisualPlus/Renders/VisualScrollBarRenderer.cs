#region License

// -----------------------------------------------------------------------------------------------------------
//
// Name: VisualScrollBarRenderer.cs
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

#endregion License

#region Namespace

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;

using VisualPlus.Enumerators;
using VisualPlus.Models;
using VisualPlus.Properties;

#endregion Namespace

namespace VisualPlus.Renders
{
    public sealed class VisualScrollBarRenderer
    {
        #region Public Methods and Operators

        /// <summary>Draws an arrow button.</summary>
        /// <param name="graphics">The <see cref="Graphics" /> used to paint.</param>
        /// <param name="arrowUp">true for an up arrow, false otherwise.</param>
        /// <param name="border">The border.</param>
        /// <param name="color">The color.</param>
        /// <param name="enabled">The enabled.</param>
        /// <param name="orientation">The <see cref="Orientation" />.</param>
        /// <param name="rectangle">The rectangle in which to paint.</param>
        /// <param name="state">The <see cref="MouseStates" /> of the arrow button.</param>
        public static void DrawArrowButton(Graphics graphics, bool arrowUp, Border border, ControlColorState color, bool enabled, Orientation orientation, Rectangle rectangle, MouseStates state)
        {
            if (graphics == null)
            {
                throw new ArgumentNullException("graphics");
            }

            if (rectangle.IsEmpty || graphics.IsVisibleClipEmpty || !graphics.VisibleClipBounds.IntersectsWith(rectangle))
            {
                return;
            }

            Color _thumbBackColor = ControlColorState.BackColorState(color, enabled, state);
            VisualBackgroundRenderer.DrawBackground(graphics, _thumbBackColor, state, rectangle, border);

            GraphicsPath _buttonGraphicsPath = VisualBorderRenderer.CreateBorderTypePath(rectangle, border);
            VisualBorderRenderer.DrawBorderStyle(graphics, border, _buttonGraphicsPath, state);

            Image _arrowImage = RetrieveButtonArrowImage(enabled);
            _arrowImage = RotateImageByOrientation(_arrowImage, orientation, arrowUp);
            graphics.DrawImage(_arrowImage, rectangle);
        }

        /// <summary>Draws the grip of the thumb.</summary>
        /// <param name="graphics">The <see cref="Graphics" /> used to paint.</param>
        /// <param name="rectangle">The rectangle in which to paint.</param>
        /// <param name="orientation">The <see cref="Orientation" />.</param>
        public static void DrawThumbGrip(Graphics graphics, Rectangle rectangle, Orientation orientation)
        {
            if (graphics == null)
            {
                throw new ArgumentNullException("graphics");
            }

            if (rectangle.IsEmpty || graphics.IsVisibleClipEmpty || !graphics.VisibleClipBounds.IntersectsWith(rectangle))
            {
                return;
            }

            using (Image _gripImage = Resources.GripNormal)
            {
                Rectangle _rectangle = AdjustThumbGrip(rectangle, orientation, _gripImage);

                // adjust alpha channel of grip image
                using (ImageAttributes _imageAttributes = new ImageAttributes())
                {
                    _imageAttributes.SetColorMatrix(
                        new ColorMatrix(new[] { new[] { 1F, 0, 0, 0, 0 }, new[] { 0, 1F, 0, 0, 0 }, new[] { 0, 0, 1F, 0, 0 }, new[] { 0, 0, 0, .8F, 0 }, new[] { 0, 0, 0, 0, 1F } }),
                        ColorMatrixFlag.Default,
                        ColorAdjustType.Bitmap);

                    graphics.DrawImage(_gripImage, _rectangle, 0, 0, _rectangle.Width, _rectangle.Height, GraphicsUnit.Pixel, _imageAttributes);
                }
            }
        }

        /// <summary>Retrieves the arrow image.</summary>
        /// <param name="enabled">The enabled.</param>
        /// <returns>The <see cref="Image" />.</returns>
        public static Image RetrieveButtonArrowImage(bool enabled)
        {
            Bitmap _bitmap = new Bitmap(15, 17, PixelFormat.Format32bppArgb);
            _bitmap.SetResolution(72f, 72f);

            using (Graphics _graphics = Graphics.FromImage(_bitmap))
            {
                _graphics.SmoothingMode = SmoothingMode.None;
                _graphics.InterpolationMode = InterpolationMode.Low;

                using (Image _arrowImage = Resources.ScrollBarArrowDown)
                {
                    if (enabled)
                    {
                        _graphics.DrawImage(_arrowImage, 3, 6);
                    }
                    else
                    {
                        ControlPaint.DrawImageDisabled(_graphics, _arrowImage, 3, 6, Color.Transparent);
                    }
                }
            }

            return _bitmap;
        }

        /// <summary>Rotates the arrow buttons by orientation.</summary>
        /// <param name="image">The image.</param>
        /// <param name="orientation">The orientation.</param>
        /// <param name="arrowUp">The arrow up.</param>
        /// <returns>The <see cref="Image" />.</returns>
        public static Image RotateImageByOrientation(Image image, Orientation orientation, bool arrowUp)
        {
            Image _image = image;

            if (orientation == Orientation.Vertical)
            {
                if (arrowUp)
                {
                    _image.RotateFlip(RotateFlipType.Rotate180FlipNone);
                }
            }
            else
            {
                if (arrowUp)
                {
                    _image.RotateFlip(RotateFlipType.Rotate90FlipNone);
                }
                else
                {
                    _image.RotateFlip(RotateFlipType.Rotate270FlipNone);
                }
            }

            return _image;
        }

        #endregion Public Methods and Operators

        #region Methods

        /// <summary>Adjusts the thumb grip according to the specified <see cref="Orientation" />.</summary>
        /// <param name="rectangle">The rectangle to adjust.</param>
        /// <param name="orientation">The scrollbar orientation.</param>
        /// <param name="image">The grip image.</param>
        /// <returns>The adjusted rectangle.</returns>
        /// <remarks>Also rotates the grip image if necessary.</remarks>
        private static Rectangle AdjustThumbGrip(Rectangle rectangle, Orientation orientation, Image image)
        {
            Rectangle _rectangle = rectangle;

            _rectangle.Inflate(-1, -1);

            if (orientation == Orientation.Vertical)
            {
                _rectangle.X += 3;
                _rectangle.Y += (_rectangle.Height / 2) - (image.Height / 2);
            }
            else
            {
                image.RotateFlip(RotateFlipType.Rotate90FlipNone);

                _rectangle.X += (_rectangle.Width / 2) - (image.Width / 2);
                _rectangle.Y += 3;
            }

            _rectangle.Width = image.Width;
            _rectangle.Height = image.Height;

            return _rectangle;
        }

        #endregion Methods
    }
}