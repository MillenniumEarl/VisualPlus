#region License

// -----------------------------------------------------------------------------------------------------------
//
// Name: VisualBitmap.cs
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

using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;

using VisualPlus.Localization;
using VisualPlus.Renders;
using VisualPlus.TypeConverters;

#endregion Namespace

namespace VisualPlus.Models
{
    [Description("The VisualBitmap")]
    [TypeConverter(typeof(VisualSettingsTypeConverter))]
    public class VisualBitmap
    {
        #region Fields

        private Border border;
        private Bitmap image;
        private Point imagePoint;
        private Size imageSize;
        private bool visible;

        #endregion Fields

        #region Constructors and Destructors

        /// <summary>Initializes a new instance of the <see cref="VisualBitmap" /> class.</summary>
        /// <param name="bitmap">The image bitmap.</param>
        /// <param name="bitmapSize">The size of the bitmap.</param>
        public VisualBitmap(Bitmap bitmap, Size bitmapSize)
        {
            border = new Border { Visible = false, HoverVisible = false };

            imagePoint = new Point();
            visible = false;

            image = bitmap;
            imageSize = bitmapSize;
        }

        #endregion Constructors and Destructors

        #region Public Properties

        [NotifyParentProperty(true)]
        [RefreshProperties(RefreshProperties.Repaint)]
        [TypeConverter(typeof(VisualSettingsTypeConverter))]
        [Category(PropertyCategory.Appearance)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public Border Border
        {
            get
            {
                return border;
            }

            set
            {
                border = value;
            }
        }

        [RefreshProperties(RefreshProperties.Repaint)]
        [Description(PropertyDescription.Image)]
        public Bitmap Image
        {
            get
            {
                return image;
            }

            set
            {
                image = value;
            }
        }

        [RefreshProperties(RefreshProperties.Repaint)]
        [Description(PropertyDescription.Point)]
        public Point Point
        {
            get
            {
                return imagePoint;
            }

            set
            {
                imagePoint = value;
            }
        }

        [RefreshProperties(RefreshProperties.Repaint)]
        [Description(PropertyDescription.Size)]
        public Size Size
        {
            get
            {
                return imageSize;
            }

            set
            {
                imageSize = value;
            }
        }

        [RefreshProperties(RefreshProperties.Repaint)]
        [Description(PropertyDescription.Visible)]
        public bool Visible
        {
            get
            {
                return visible;
            }

            set
            {
                visible = value;
            }
        }

        #endregion Public Properties

        #region Public Methods and Operators

        /// <summary>Draws the bitmap image.</summary>
        /// <param name="graphics">Graphics controller.</param>
        /// <param name="_border">The border.</param>
        /// <param name="_imagePoint">The location.</param>
        /// <param name="_image">The image.</param>
        /// <param name="_imageSize">The size.</param>
        /// <param name="_visible">The visibility.</param>
        public static void DrawImage(Graphics graphics, Border _border, Point _imagePoint, Bitmap _image, Size _imageSize, bool _visible)
        {
            if (_image != null)
            {
                using (GraphicsPath imagePath = new GraphicsPath())
                {
                    imagePath.AddRectangle(new Rectangle(_imagePoint, _imageSize));

                    if (_border.Visible)
                    {
                        VisualBorderRenderer.DrawBorder(graphics, imagePath, _border.Color, thickness: _border.Thickness);
                    }
                }

                if (_visible)
                {
                    graphics.DrawImage(_image, new Rectangle(_imagePoint, _imageSize));
                }
            }
        }

        /// <summary>Returns the size of the image.</summary>
        /// <param name="_image">The image.</param>
        /// <returns>The <see cref="Size" />.</returns>
        public Size GetOriginalSize(Image _image)
        {
            return _image.Size;
        }

        #endregion Public Methods and Operators
    }
}