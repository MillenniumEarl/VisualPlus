#region License

// -----------------------------------------------------------------------------------------------------------
// 
// Name: VisualBorderRenderer.cs
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

using System;
using System.Drawing;
using System.Drawing.Drawing2D;

using VisualPlus.Enumerators;
using VisualPlus.Managers;
using VisualPlus.Structure;

#endregion

namespace VisualPlus.Renders
{
    public sealed class VisualBorderRenderer
    {
        #region Public Methods and Operators

        /// <summary>Creates a border type path.</summary>
        /// <param name="rectangle">The rectangle.</param>
        /// <param name="shape">The shape.</param>
        /// <returns>The <see cref="GraphicsPath" />.</returns>
        public static GraphicsPath CreateBorderTypePath(Rectangle rectangle, Shape shape)
        {
            return CreateBorderTypePath(rectangle, shape.Rounding, shape.Thickness, shape.Type);
        }

        /// <summary>Creates a border type path.</summary>
        /// <param name="rectangle">The rectangle.</param>
        /// <param name="rounding">The rounding.</param>
        /// <param name="thickness">The thickness.</param>
        /// <param name="type">The shape.</param>
        /// <returns>The <see cref="GraphicsPath" />.</returns>
        public static GraphicsPath CreateBorderTypePath(Rectangle rectangle, int rounding, int thickness, ShapeTypes type)
        {
            Rectangle _borderRectangle = new Rectangle(rectangle.X, rectangle.Y, rectangle.Width - thickness, rectangle.Height - thickness);
            GraphicsPath _borderShape = new GraphicsPath();

            switch (type)
            {
                case ShapeTypes.Rectangle:
                    {
                        _borderShape.AddRectangle(_borderRectangle);
                        break;
                    }

                case ShapeTypes.Rounded:
                    {
                        _borderShape.AddArc(rectangle.X, rectangle.Y, rounding, rounding, 180.0F, 90.0F);
                        _borderShape.AddArc(rectangle.Right - rounding, rectangle.Y, rounding, rounding, 270.0F, 90.0F);
                        _borderShape.AddArc(rectangle.Right - rounding, rectangle.Bottom - rounding, rounding, rounding, 0.0F, 90.0F);
                        _borderShape.AddArc(rectangle.X, rectangle.Bottom - rounding, rounding, rounding, 90.0F, 90.0F);
                        break;
                    }

                default:
                    {
                        throw new ArgumentOutOfRangeException(nameof(type), type, null);
                    }
            }

            _borderShape.CloseAllFigures();
            return _borderShape;
        }

        /// <summary>Draws a border around the rectangle, with the specified thickness.</summary>
        /// <param name="graphics">The graphics to draw on.</param>
        /// <param name="rectangle">The rectangle.</param>
        /// <param name="color">The color.</param>
        /// <param name="thickness">The thickness.</param>
        public static void DrawBorder(Graphics graphics, Rectangle rectangle, Color color, float thickness)
        {
            GraphicsPath _borderGraphicsPath = new GraphicsPath();
            _borderGraphicsPath.AddRectangle(rectangle);
            Pen _borderPen = new Pen(color, thickness);
            graphics.DrawPath(_borderPen, _borderGraphicsPath);
        }

        /// <summary>Draws a border around the custom graphics path, with the specified thickness.</summary>
        /// <param name="graphics">The graphics to draw on.</param>
        /// <param name="customPath">The custom Path.</param>
        /// <param name="color">The color.</param>
        /// <param name="thickness">The thickness.</param>
        public static void DrawBorder(Graphics graphics, GraphicsPath customPath, Color color, float thickness)
        {
            Pen _borderPen = new Pen(color, thickness);
            graphics.DrawPath(_borderPen, customPath);
        }

        /// <summary>Draws a border around the rounded rectangle, with the specified rounding and thickness.</summary>
        /// <param name="graphics">The graphics to draw on.</param>
        /// <param name="rectangle">The rectangle.</param>
        /// <param name="color">The color.</param>
        /// <param name="rounding">The amount of rounding.</param>
        /// <param name="thickness">The thickness.</param>
        public static void DrawBorder(Graphics graphics, Rectangle rectangle, Color color, int rounding, float thickness)
        {
            GraphicsPath _borderGraphicsPath = GraphicsManager.DrawRoundedRectangle(rectangle, rounding);
            Pen _borderPen = new Pen(color, thickness);
            graphics.DrawPath(_borderPen, _borderGraphicsPath);
        }

        /// <summary>Draws a border with the specified shape settings.</summary>
        /// <param name="graphics">The graphics to draw on.</param>
        /// <param name="rectangle">The rectangle.</param>
        /// <param name="color">The color.</param>
        /// <param name="rounding">The rounding.</param>
        /// <param name="thickness">The thickness.</param>
        /// <param name="shape">The shape.</param>
        public static void DrawBorder(Graphics graphics, Rectangle rectangle, Color color, int rounding, float thickness, ShapeTypes shape)
        {
            switch (shape)
            {
                case ShapeTypes.Rectangle:
                    {
                        DrawBorder(graphics, rectangle, color, thickness);
                        break;
                    }

                case ShapeTypes.Rounded:
                    {
                        DrawBorder(graphics, rectangle, color, rounding, thickness);
                        break;
                    }

                default:
                    {
                        throw new ArgumentOutOfRangeException(nameof(shape), shape, null);
                    }
            }
        }

        /// <summary>Draws a border with the specified shape.</summary>
        /// <param name="graphics">The graphics to draw on.</param>
        /// <param name="rectangle">The rectangle.</param>
        /// <param name="shape">The shape.</param>
        public static void DrawBorder(Graphics graphics, Rectangle rectangle, Shape shape)
        {
            DrawBorder(graphics, rectangle, shape.Color, shape.Rounding, shape.Thickness, shape.Type);
        }

        /// <summary>Draws a border around the rectangle, with the specified mouse state.</summary>
        /// <param name="graphics">Graphics controller.</param>
        /// <param name="border">The border type.</param>
        /// <param name="mouseState">The mouse state.</param>
        /// <param name="rectangle">The rectangle.</param>
        public static void DrawBorder(Graphics graphics, Border border, MouseStates mouseState, Rectangle rectangle)
        {
            if (!border.Visible)
            {
                return;
            }

            switch (mouseState)
            {
                case MouseStates.Normal:
                    {
                        DrawBorder(graphics, rectangle, border.Color, border.Rounding, border.Thickness, border.Type);
                        break;
                    }

                case MouseStates.Hover:
                    {
                        DrawBorder(graphics, rectangle, border.HoverVisible ? border.HoverColor : border.Color, border.Rounding, border.Thickness, border.Type);
                        break;
                    }

                case MouseStates.Pressed:
                    {
                        DrawBorder(graphics, rectangle, border.HoverVisible ? border.HoverColor : border.Color, border.Rounding, border.Thickness, border.Type);
                        break;
                    }

                default:
                    {
                        throw new ArgumentOutOfRangeException(nameof(mouseState), mouseState, null);
                    }
            }
        }

        /// <summary>Draws a border around the custom path, with the specified mouse state.</summary>
        /// <param name="graphics">Graphics controller.</param>
        /// <param name="border">The border type.</param>
        /// <param name="customPath">The custom Path.</param>
        /// <param name="mouseState">The mouse state.</param>
        public static void DrawBorderStyle(Graphics graphics, Border border, GraphicsPath customPath, MouseStates mouseState)
        {
            if (!border.Visible)
            {
                return;
            }

            switch (mouseState)
            {
                case MouseStates.Normal:
                    {
                        DrawBorder(graphics, customPath, border.Color, border.Thickness);
                        break;
                    }

                case MouseStates.Hover:
                    {
                        DrawBorder(graphics, customPath, border.HoverVisible ? border.HoverColor : border.Color, border.Thickness);
                        break;
                    }

                case MouseStates.Pressed:
                    {
                        DrawBorder(graphics, customPath, border.HoverVisible ? border.HoverColor : border.Color, border.Thickness);
                        break;
                    }

                default:
                    {
                        throw new ArgumentOutOfRangeException(nameof(mouseState), mouseState, null);
                    }
            }
        }

        #endregion
    }
}