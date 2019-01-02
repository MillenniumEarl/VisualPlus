#region License

// -----------------------------------------------------------------------------------------------------------
// 
// Name: VisualTextRenderer.cs
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
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

using VisualPlus.Enumerators;
using VisualPlus.Managers;
using VisualPlus.Structure;

#endregion

namespace VisualPlus.Renders
{
    public sealed class VisualTextRenderer
    {
        #region Public Methods and Operators

        /// <summary>Retrieves the appropriate string format.</summary>
        /// <param name="orientation">The orientation.</param>
        /// <param name="alignment">The alignment.</param>
        /// <param name="lineAlignment">The line Alignment.</param>
        /// <returns>The <see cref="StringFormat" />.</returns>
        public static StringFormat GetOrientedStringFormat(Orientation orientation, StringAlignment alignment, StringAlignment lineAlignment)
        {
            StringFormat orientedStringFormat;

            switch (orientation)
            {
                case Orientation.Horizontal:
                    {
                        orientedStringFormat = new StringFormat { Alignment = alignment, LineAlignment = lineAlignment };
                        break;
                    }

                case Orientation.Vertical:
                    {
                        orientedStringFormat = new StringFormat(StringFormatFlags.DirectionVertical);
                        break;
                    }

                default:
                    throw new ArgumentOutOfRangeException();
            }

            return orientedStringFormat;
        }

        /// <summary>Render the text in the specified location.</summary>
        /// <param name="graphics">The specified graphics to draw on.</param>
        /// <param name="clientRectangle">The client rectangle.</param>
        /// <param name="text">The text to draw.</param>
        /// <param name="font">The font to  draw.</param>
        /// <param name="color">The fore color.</param>
        /// <param name="location">The location.</param>
        public static void RenderText(Graphics graphics, Rectangle clientRectangle, string text, Font font, Color color, Point location)
        {
            graphics.DrawString(text, font, new SolidBrush(color), new Rectangle(location, clientRectangle.Size));
        }

        /// <summary>Render the text using the string format.</summary>
        /// <param name="graphics">The specified graphics to draw on.</param>
        /// <param name="clientRectangle">The client rectangle.</param>
        /// <param name="text">The text to draw.</param>
        /// <param name="font">The font to  draw.</param>
        /// <param name="color">The fore color.</param>
        /// <param name="stringFormat">The string Format.</param>
        public static void RenderText(Graphics graphics, Rectangle clientRectangle, string text, Font font, Color color, StringFormat stringFormat)
        {
            graphics.DrawString(text, font, new SolidBrush(color), clientRectangle, stringFormat);
        }

        /// <summary>Render the text using the text style.</summary>
        /// <param name="graphics">The specified graphics to draw on.</param>
        /// <param name="clientRectangle">The client rectangle.</param>
        /// <param name="text">The text to draw.</param>
        /// <param name="font">The font to  draw.</param>
        /// <param name="enabled">The enabled.</param>
        /// <param name="textStyle">The text Style.</param>
        public static void RenderText(Graphics graphics, Rectangle clientRectangle, string text, Font font, bool enabled, TextStyle textStyle)
        {
            Color _textColor = enabled ? textStyle.Enabled : textStyle.Disabled;
            RenderText(graphics, clientRectangle, text, font, _textColor, textStyle.StringFormat);
        }

        /// <summary>Render the text using the text style.</summary>
        /// <param name="graphics">The specified graphics to draw on.</param>
        /// <param name="clientRectangle">The client rectangle.</param>
        /// <param name="text">The text to draw.</param>
        /// <param name="font">The font to  draw.</param>
        /// <param name="enabled">The enabled.</param>
        /// <param name="mouseState">The mouse State.</param>
        /// <param name="textStyle">The text Style.</param>
        public static void RenderText(Graphics graphics, Rectangle clientRectangle, string text, Font font, bool enabled, MouseStates mouseState, TextStyle textStyle)
        {
            Color _textColor = TextStyle.GetColorState(enabled, mouseState, textStyle);
            RenderText(graphics, clientRectangle, text, font, _textColor, textStyle.StringFormat);
        }

        /// <summary>Render the text in the center of the client rectangle.</summary>
        /// <param name="graphics">The specified graphics to draw on.</param>
        /// <param name="clientRectangle">The client rectangle.</param>
        /// <param name="text">The text to draw.</param>
        /// <param name="font">The font to  draw.</param>
        /// <param name="color">The fore color.</param>
        /// <param name="offset">The location offset.</param>
        public static void RenderTextCentered(Graphics graphics, Rectangle clientRectangle, string text, Font font, Color color, Point offset = new Point())
        {
            Size _stringSize = TextManager.MeasureText(text, font, graphics);
            Point _location = new Point(((clientRectangle.Width / 2) - (_stringSize.Width / 2)) + offset.X, ((clientRectangle.Height / 2) - (_stringSize.Height / 2)) + offset.Y);

            graphics.DrawString(text, font, new SolidBrush(color), _location);
        }

        /// <summary>Renders the text outline.</summary>
        /// <param name="graphics">The specified graphics to draw on.</param>
        /// <param name="orientation">The orientation.</param>
        /// <param name="text">The text to draw.</param>
        /// <param name="font">The font to  draw.</param>
        /// <param name="color">The fore color.</param>
        /// <param name="offset">The location offset.</param>
        public static void RenderTextOutline(Graphics graphics, Orientation orientation, string text, Font font, Color color, Point offset)
        {
            GraphicsPath outlinePath = new GraphicsPath();

            switch (orientation)
            {
                case Orientation.Horizontal:
                    {
                        outlinePath.AddString(
                            text,
                            font.FontFamily,
                            (int)font.Style,
                            (graphics.DpiY * font.SizeInPoints) / 72,
                            offset,
                            new StringFormat());

                        break;
                    }

                case Orientation.Vertical:
                    {
                        outlinePath.AddString(
                            text,
                            font.FontFamily,
                            (int)font.Style,
                            (graphics.DpiY * font.SizeInPoints) / 72,
                            offset,
                            new StringFormat(StringFormatFlags.DirectionVertical));

                        break;
                    }

                default:
                    {
                        throw new ArgumentOutOfRangeException(nameof(orientation));
                    }
            }

            graphics.DrawPath(new Pen(color), outlinePath);
        }

        /// <summary>Renders the text reflection.</summary>
        /// <param name="graphics">The specified graphics to draw on.</param>
        /// <param name="orientation">The orientation.</param>
        /// <param name="text">The text to draw.</param>
        /// <param name="font">The font to  draw.</param>
        /// <param name="color">The fore color.</param>
        /// <param name="clientRectangle">The client rectangle.</param>
        /// <param name="spacing">The reflection spacing.</param>
        /// <param name="offset">The location offset.</param>
        /// <param name="alignment">The alignment.</param>
        /// <param name="lineAlignment">The line Alignment.</param>
        public static void RenderTextReflection(Graphics graphics, Orientation orientation, string text, Font font, Color color, Rectangle clientRectangle, int spacing, Point offset, StringAlignment alignment, StringAlignment lineAlignment)
        {
            Point reflectionLocation;
            Bitmap reflectionBitmap = new Bitmap(clientRectangle.Width, clientRectangle.Height);
            Graphics imageGraphics = Graphics.FromImage(reflectionBitmap);

            // Setup text render
            imageGraphics.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;

            // Rotate reflection
            switch (orientation)
            {
                case Orientation.Horizontal:
                    {
                        imageGraphics.TranslateTransform(0, TextManager.MeasureText(text, font, graphics).Height);
                        imageGraphics.ScaleTransform(1, -1);

                        reflectionLocation = new Point(0, offset.Y - (TextManager.MeasureText(text, font, graphics).Height / 2) - spacing);
                        break;
                    }

                case Orientation.Vertical:
                    {
                        imageGraphics.ScaleTransform(-1, 1);
                        reflectionLocation = new Point((offset.X - (TextManager.MeasureText(text, font, graphics).Width / 2)) + spacing, 0);
                        break;
                    }

                default:
                    {
                        throw new ArgumentOutOfRangeException(nameof(orientation));
                    }
            }

            // Draw reflected string
            imageGraphics.DrawString(text, font, new SolidBrush(color), reflectionLocation, GetOrientedStringFormat(orientation, alignment, lineAlignment));

            // Draw the reflection image
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics.DrawImage(reflectionBitmap, clientRectangle, 0, 0, reflectionBitmap.Width, reflectionBitmap.Height, GraphicsUnit.Pixel);
            graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
        }

        /// <summary>Render the text shadow.</summary>
        /// <param name="graphics">The specified graphics to draw on.</param>
        /// <param name="orientation">The orientation.</param>
        /// <param name="text">The text to draw.</param>
        /// <param name="font">The font to  draw.</param>
        /// <param name="color">The fore color.</param>
        /// <param name="clientRectangle">The client rectangle.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="smoothness">The smoothness.</param>
        /// <param name="depth">The depth.</param>
        /// <param name="direction">The direction.</param>
        /// <param name="shadowOpacity">The shadow Opacity.</param>
        public static void RenderTextShadow(Graphics graphics, Orientation orientation, string text, Font font, Color color, Rectangle clientRectangle, Point offset, float smoothness, int depth, int direction, int shadowOpacity)
        {
            // Create shadow into a bitmap
            Bitmap shadowBitmap = new Bitmap(Math.Max((int)(clientRectangle.Width / smoothness), 1), Math.Max((int)(clientRectangle.Height / smoothness), 1));
            Graphics imageGraphics = Graphics.FromImage(shadowBitmap);

            // Setup text render
            imageGraphics.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;

            // Create transformation matrix
            Matrix transformMatrix = new Matrix();
            transformMatrix.Scale(1 / smoothness, 1 / smoothness);
            transformMatrix.Translate((float)(depth * Math.Cos(direction)), (float)(depth * Math.Sin(direction)));
            imageGraphics.Transform = transformMatrix;

            switch (orientation)
            {
                case Orientation.Horizontal:
                    {
                        imageGraphics.DrawString(text, font, new SolidBrush(Color.FromArgb(shadowOpacity, color)), offset);
                        break;
                    }

                case Orientation.Vertical:
                    {
                        imageGraphics.DrawString(text, font, new SolidBrush(Color.FromArgb(shadowOpacity, color)), offset, new StringFormat(StringFormatFlags.DirectionVertical));
                        break;
                    }

                default:
                    {
                        throw new ArgumentOutOfRangeException(nameof(orientation));
                    }
            }

            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics.DrawImage(shadowBitmap, clientRectangle, 0, 0, shadowBitmap.Width, shadowBitmap.Height, GraphicsUnit.Pixel);
            graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
        }

        #endregion
    }
}