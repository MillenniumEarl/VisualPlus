#region License

// -----------------------------------------------------------------------------------------------------------
// 
// Name: VisualBackgroundRenderer.cs
// VisualPlus - The VisualPlus Framework (VPF) for WinForms .NET development.
// 
// Created: 10/12/2018 - 11:45 PM
// Last Modified: 02/01/2019 - 1:24 AM
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

using VisualPlus.Enumerators;
using VisualPlus.Managers;
using VisualPlus.Structure;

#endregion

namespace VisualPlus.Renders
{
    public sealed class VisualBackgroundRenderer
    {
        #region Public Methods and Operators

        /// <summary>Draws a background with a color filled rectangle and the specified background image.</summary>
        /// <param name="graphics">The graphics to draw on.</param>
        /// <param name="backColor">The back Color.</param>
        /// <param name="backgroundImage">The background Image.</param>
        /// <param name="rectangle">The coordinates of the rectangle to draw.</param>
        /// <param name="rounding">The rounding.</param>
        public static void DrawBackground(Graphics graphics, Color backColor, Image backgroundImage, Rectangle rectangle, int rounding)
        {
            GraphicsPath _controlGraphicsPath = FillBackgroundPath(graphics, backColor, rectangle, rounding);

            if (backgroundImage == null)
            {
                return;
            }

            Point _location = new Point(rectangle.Width - backgroundImage.Width, rectangle.Height - backgroundImage.Height);
            Size _size = new Size(backgroundImage.Width, backgroundImage.Height);
            graphics.SetClip(_controlGraphicsPath);
            graphics.DrawImage(backgroundImage, new Rectangle(_location, _size));
            graphics.ResetClip();
        }

        /// <summary>Draws a background with a color filled rectangle.</summary>
        /// <param name="graphics">The graphics to draw on.</param>
        /// <param name="backColor">The back Color.</param>
        /// <param name="rectangle">The coordinates of the rectangle to draw.</param>
        public static void DrawBackground(Graphics graphics, Color backColor, Rectangle rectangle)
        {
            GraphicsPath _graphicsPath = new GraphicsPath();
            _graphicsPath.AddRectangle(rectangle);
            graphics.FillPath(new SolidBrush(backColor), _graphicsPath);
        }

        /// <summary>Draws the control background, with a BackColor and the specified BackgroundImage.</summary>
        /// <param name="graphics">The graphics to draw on.</param>
        /// <param name="backColor">The color to use for the background.</param>
        /// <param name="backgroundImage">The background image to use for the background.</param>
        /// <param name="mouseState">The mouse state.</param>
        /// <param name="rectangle">The coordinates of the rectangle to draw.</param>
        /// <param name="border">The shape settings.</param>
        public static void DrawBackground(Graphics graphics, Color backColor, Image backgroundImage, MouseStates mouseState, Rectangle rectangle, Border border)
        {
            GraphicsPath _controlGraphicsPath = FillBackgroundPath(graphics, backColor, rectangle, border);

            if (backgroundImage != null)
            {
                Point _location = new Point(rectangle.Width - backgroundImage.Width, rectangle.Height - backgroundImage.Height);
                Size _size = new Size(backgroundImage.Width, backgroundImage.Height);
                graphics.SetClip(_controlGraphicsPath);
                graphics.DrawImage(backgroundImage, new Rectangle(_location, _size));
                graphics.ResetClip();
            }
        }

        /// <summary>Draws the control background, with a BackColor and the specified BackgroundImage.</summary>
        /// <param name="graphics">The graphics to draw on.</param>
        /// <param name="backColor">The color to use for the background.</param>
        /// <param name="mouseState">The mouse state.</param>
        /// <param name="rectangle">The coordinates of the rectangle to draw.</param>
        /// <param name="shape">The shape settings.</param>
        public static void DrawBackground(Graphics graphics, Color backColor, MouseStates mouseState, Rectangle rectangle, Border shape)
        {
            GraphicsPath _controlGraphicsPath = FillBackgroundPath(graphics, backColor, rectangle, shape);
            VisualBorderRenderer.DrawBorderStyle(graphics, shape, _controlGraphicsPath, mouseState);
        }

        /// <summary>Draws a background with a border style.</summary>
        /// <param name="graphics">The graphics to draw on.</param>
        /// <param name="background">The background color.</param>
        /// <param name="border">The border type.</param>
        /// <param name="mouseState">The control mouse state.</param>
        /// <param name="rectangle">The coordinates of the rectangle to draw.</param>
        public static void DrawBackground(Graphics graphics, Color background, Border border, MouseStates mouseState, Rectangle rectangle)
        {
            GraphicsPath backgroundPath = FillBackgroundPath(graphics, background, rectangle, border);
            VisualBorderRenderer.DrawBorderStyle(graphics, border, backgroundPath, mouseState);
        }

        /// <summary>Draws a background with a still border style.</summary>
        /// <param name="graphics">The graphics to draw on.</param>
        /// <param name="border">The border type.</param>
        /// <param name="background">The background color.</param>
        /// <param name="rectangle">The coordinates of the rectangle to draw.</param>
        public static void DrawBackground(Graphics graphics, Border border, Color background, Rectangle rectangle)
        {
            GraphicsPath backgroundPath = FillBackgroundPath(graphics, background, rectangle, border);
            VisualBorderRenderer.DrawBorder(graphics, backgroundPath, border.Color, thickness: border.Thickness);
        }

        /// <summary>Draws a background with a still border style.</summary>
        /// <param name="graphics">The graphics to draw on.</param>
        /// <param name="background">The background color.</param>
        /// <param name="rectangle">The coordinates of the rectangle to draw.</param>
        /// <param name="shape">The shape.</param>
        public static void DrawBackground(Graphics graphics, Color background, Rectangle rectangle, Shape shape)
        {
            GraphicsPath backgroundPath = VisualBorderRenderer.CreateBorderTypePath(rectangle, shape);
            graphics.SetClip(backgroundPath);
            graphics.FillRectangle(new SolidBrush(background), rectangle);
            graphics.ResetClip();
            VisualBorderRenderer.DrawBorder(graphics, backgroundPath, shape.Color, thickness: shape.Thickness);
        }

        /// <summary>Draws a background with a linear gradient still border style.</summary>
        /// <param name="graphics">The graphics to draw on.</param>
        /// <param name="background">The background linear gradient.</param>
        /// <param name="border">The border type.</param>
        /// <param name="mouseState">The control mouse state.</param>
        /// <param name="rectangle">The coordinates of the rectangle to draw.</param>
        public static void DrawBackground(Graphics graphics, LinearGradientBrush background, Border border, MouseStates mouseState, Rectangle rectangle)
        {
            GraphicsPath backgroundPath = VisualBorderRenderer.CreateBorderTypePath(rectangle, border);
            FillBackground(graphics, backgroundPath, background);
            VisualBorderRenderer.DrawBorderStyle(graphics, border, backgroundPath, mouseState);
        }

        /// <summary>Draws a background with a linear gradient still border style.</summary>
        /// <param name="graphics">The graphics to draw on.</param>
        /// <param name="background">The background linear gradient.</param>
        /// <param name="border">The border type.</param>
        /// <param name="rectangle">The coordinates of the rectangle to draw.</param>
        public static void DrawBackground(Graphics graphics, LinearGradientBrush background, Border border, Rectangle rectangle)
        {
            GraphicsPath backgroundPath = VisualBorderRenderer.CreateBorderTypePath(rectangle, border);
            FillBackground(graphics, backgroundPath, background);
            VisualBorderRenderer.DrawBorder(graphics, backgroundPath, border.Color, border.Thickness);
        }

        /// <summary>Fills the background.</summary>
        /// <param name="graphics">Graphics controller.</param>
        /// <param name="graphicsPath">The graphics path.</param>
        /// <param name="brush">The gradient brush.</param>
        public static void FillBackground(Graphics graphics, GraphicsPath graphicsPath, Brush brush)
        {
            graphics.FillPath(brush, graphicsPath);
        }

        /// <summary>Renders the background.</summary>
        /// <param name="graphics">The specified graphics to draw on.</param>
        /// <param name="backColor">The back color.</param>
        /// <param name="backgroundImage">The background image.</param>
        /// <param name="backgroundLayout">The background Layout.</param>
        /// <param name="clientRectangle">The client rectangle.</param>
        public static void RenderBackground(Graphics graphics, Color backColor, Image backgroundImage, BackgroundLayout backgroundLayout, Rectangle clientRectangle)
        {
            if (graphics == null)
            {
                throw new ArgumentNullException(nameof(graphics));
            }

            if (backColor == Color.Empty)
            {
                throw new ArgumentNullException(nameof(backColor));
            }

            if (clientRectangle == Rectangle.Empty)
            {
                throw new ArgumentNullException(nameof(clientRectangle));
            }

            graphics.FillRectangle(new SolidBrush(backColor), clientRectangle);
            RenderBackgroundImage(graphics, backgroundImage, backgroundLayout, clientRectangle);
        }

        /// <summary>Render the background image.</summary>
        /// <param name="graphics">The specified graphics to draw on.</param>
        /// <param name="backgroundImage">The background image.</param>
        /// <param name="backgroundLayout">The background layout.</param>
        /// <param name="clientRectangle">The client rectangle.</param>
        public static void RenderBackgroundImage(Graphics graphics, Image backgroundImage, BackgroundLayout backgroundLayout, Rectangle clientRectangle)
        {
            if (graphics == null)
            {
                throw new ArgumentNullException(nameof(graphics));
            }

            if (backgroundImage == null)
            {
                return;
            }

            if (clientRectangle == Rectangle.Empty)
            {
                throw new ArgumentNullException(nameof(clientRectangle));
            }

            switch (backgroundLayout)
            {
                case BackgroundLayout.None:
                    {
                        VisualImageRenderer.RenderImage(graphics, backgroundImage);
                        break;
                    }

                case BackgroundLayout.Center:
                    {
                        VisualImageRenderer.RenderImageCenteredFit(graphics, clientRectangle, backgroundImage);
                        break;
                    }

                case BackgroundLayout.Stretch:
                    {
                        VisualImageRenderer.RenderImageFilled(graphics, clientRectangle, backgroundImage);
                        break;
                    }

                default:
                    {
                        throw new ArgumentOutOfRangeException(nameof(backgroundLayout), backgroundLayout, null);
                    }
            }
        }

        #endregion

        #region Methods

        /// <summary>Fills the background graphics path.</summary>
        /// <param name="graphics">The graphics to draw on.</param>
        /// <param name="background">The background color.</param>
        /// <param name="rectangle">The coordinates of the rectangle to draw.</param>
        /// <param name="border">The border type.</param>
        /// <returns>The <see cref="GraphicsPath" />.</returns>
        private static GraphicsPath FillBackgroundPath(Graphics graphics, Color background, Rectangle rectangle, Shape border)
        {
            GraphicsPath backgroundPath = VisualBorderRenderer.CreateBorderTypePath(rectangle, border);
            graphics.SetClip(backgroundPath);
            graphics.FillRectangle(new SolidBrush(background), rectangle);
            graphics.ResetClip();
            return backgroundPath;
        }

        /// <summary>Fills the background graphics path.</summary>
        /// <param name="graphics">The graphics to draw on.</param>
        /// <param name="background">The background color.</param>
        /// <param name="rectangle">The coordinates of the rectangle to draw.</param>
        /// <param name="rounding">The amount of rounding.</param>
        /// <returns>The <see cref="GraphicsPath" />.</returns>
        private static GraphicsPath FillBackgroundPath(Graphics graphics, Color background, Rectangle rectangle, int rounding)
        {
            GraphicsPath backgroundPath = GraphicsManager.DrawRoundedRectangle(rectangle, rounding);
            graphics.SetClip(backgroundPath);
            graphics.FillRectangle(new SolidBrush(background), rectangle);
            graphics.ResetClip();
            return backgroundPath;
        }

        #endregion
    }
}