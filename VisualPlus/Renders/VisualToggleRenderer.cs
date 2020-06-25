#region License

// -----------------------------------------------------------------------------------------------------------
//
// Name: VisualToggleRenderer.cs
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

using VisualPlus.Enumerators;
using VisualPlus.Extensibility;
using VisualPlus.Models;
using VisualPlus.Utilities;

#endregion Namespace

namespace VisualPlus.Renders
{
    public sealed class VisualToggleRenderer
    {
        #region Public Methods and Operators

        /// <summary>Draws a check box control in the specified state and with the specified text.</summary>
        /// <param name="graphics">The graphics to draw on.</param>
        /// <param name="border">The border type.</param>
        /// <param name="checkStyle">The check mark type.</param>
        /// <param name="rectangle">The rectangle that represents the dimensions of the check box.</param>
        /// <param name="state">The toggle state of the check mark.</param>
        /// <param name="enabled">The state to draw the check mark in.</param>
        /// <param name="color">The brush used to fill the background.</param>
        /// <param name="backgroundImage">The background Image.</param>
        /// <param name="mouseState">The state of the mouse on the control.</param>
        /// <param name="text">The text.</param>
        /// <param name="font">The font.</param>
        /// <param name="foreColor">The fore Color.</param>
        /// <param name="textPoint">The text Point.</param>
        public static void DrawCheckBox(Graphics graphics, Border border, CheckStyle checkStyle, Rectangle rectangle, bool state, bool enabled, Color color, Image backgroundImage, MouseStates mouseState, string text, Font font, Color foreColor, Point textPoint)
        {
            DrawCheckBox(graphics, border, checkStyle, rectangle, state, enabled, color, backgroundImage, mouseState);
            graphics.DrawString(text, font, new SolidBrush(foreColor), textPoint);
        }

        /// <summary>Draws a check box control in the specified state and location.</summary>
        /// <param name="graphics">The graphics to draw on.</param>
        /// <param name="border">The border type.</param>
        /// <param name="checkStyle">The check mark type.</param>
        /// <param name="rectangle">The rectangle that represents the dimensions of the check box.</param>
        /// <param name="checkState">The check State.</param>
        /// <param name="enabled">The state to draw the check mark in.</param>
        /// <param name="color">The background color.</param>
        /// <param name="backgroundImage">The background Image.</param>
        /// <param name="mouseStates">The mouse States.</param>
        public static void DrawCheckBox(Graphics graphics, Border border, CheckStyle checkStyle, Rectangle rectangle, bool checkState, bool enabled, Color color, Image backgroundImage, MouseStates mouseStates)
        {
            GraphicsPath _boxGraphicsPath = VisualBorderRenderer.CreateBorderTypePath(rectangle, border);
            graphics.SetClip(_boxGraphicsPath);
            VisualBackgroundRenderer.DrawBackground(graphics, color, backgroundImage, mouseStates, rectangle, border);

            if (checkState)
            {
                DrawCheckMark(graphics, checkStyle, rectangle, enabled);
            }

            VisualBorderRenderer.DrawBorderStyle(graphics, border, _boxGraphicsPath, mouseStates);
            graphics.ResetClip();
        }

        /// <summary>Draws the checkmark.</summary>
        /// <param name="graphics">The specified graphics to draw on.</param>
        /// <param name="color">The color.</param>
        /// <param name="rectangle">The rectangle.</param>
        /// <param name="thickness">The thickness.</param>
        public static void DrawCheckmark(Graphics graphics, Color color, Rectangle rectangle, float thickness = 2)
        {
            Point[] _locations = { new Point((rectangle.Width / 4) - 1, rectangle.Y + 4 + (rectangle.Height / 3)), new Point((rectangle.Width / 4) + 3, rectangle.Y + 7 + (rectangle.Height / 3)), new Point((rectangle.Width / 4) + 9, rectangle.Y + (rectangle.Height / 3)) };

            graphics.DrawLines(new Pen(color, thickness), _locations);
        }

        /// <summary>
        ///     Draws a check mark control in the specified state, on the specified graphics surface, and within the specified
        ///     bounds.
        /// </summary>
        /// <param name="graphics">The graphics to draw on.</param>
        /// <param name="checkStyle">The check mark type.</param>
        /// <param name="rectangle">The rectangle that represents the dimensions of the check box.</param>
        /// <param name="enabled">The state to draw the check mark in.</param>
        public static void DrawCheckMark(Graphics graphics, CheckStyle checkStyle, Rectangle rectangle, bool enabled)
        {
            Size _characterSize = StringUtil.MeasureText(checkStyle.Character.ToString(), checkStyle.Font, graphics);

            int _styleCount = checkStyle.Style.Count();
            var _defaultLocations = new Point[_styleCount];
            _defaultLocations[0] = new Point((rectangle.X + (rectangle.Width / 2)) - (_characterSize.Width / 2), (rectangle.Y + (rectangle.Height / 2)) - (_characterSize.Height / 2));
            _defaultLocations[1] = new Point((rectangle.X + (rectangle.Width / 2)) - (checkStyle.Bounds.Width / 2), (rectangle.Y + (rectangle.Height / 2)) - (checkStyle.Bounds.Height / 2));
            _defaultLocations[2] = new Point((rectangle.X + (rectangle.Width / 2)) - (checkStyle.Bounds.Width / 2), (rectangle.Y + (rectangle.Height / 2)) - (checkStyle.Bounds.Height / 2));
            _defaultLocations[3] = new Point((rectangle.X + (rectangle.Width / 2)) - (checkStyle.Bounds.Width / 2), (rectangle.Y + (rectangle.Height / 2)) - (checkStyle.Bounds.Height / 2));

            Point _tempLocation;
            if (checkStyle.AutoSize)
            {
                int styleIndex;

                switch (checkStyle.Style)
                {
                    case CheckStyle.CheckType.Character:
                        {
                            styleIndex = 0;
                            break;
                        }

                    case CheckStyle.CheckType.Image:
                        {
                            styleIndex = 1;
                            break;
                        }

                    case CheckStyle.CheckType.Shape:
                        {
                            styleIndex = 2;
                            break;
                        }

                    case CheckStyle.CheckType.Checkmark:
                        {
                            styleIndex = 3;
                            break;
                        }

                    default:
                        {
                            throw new ArgumentOutOfRangeException();
                        }
                }

                _tempLocation = _defaultLocations[styleIndex];
            }
            else
            {
                _tempLocation = checkStyle.Bounds.Location;
            }

            switch (checkStyle.Style)
            {
                case CheckStyle.CheckType.Character:
                    {
                        graphics.DrawString(checkStyle.Character.ToString(), checkStyle.Font, new SolidBrush(checkStyle.CheckColor), _tempLocation);
                        break;
                    }

                case CheckStyle.CheckType.Image:
                    {
                        Rectangle _imageRectangle = new Rectangle(_tempLocation, checkStyle.Bounds.Size);
                        graphics.DrawImage(checkStyle.Image, _imageRectangle);
                        break;
                    }

                case CheckStyle.CheckType.Shape:
                    {
                        Rectangle shapeRectangle = new Rectangle(_tempLocation, checkStyle.Bounds.Size);
                        GraphicsPath shapePath = VisualBorderRenderer.CreateBorderTypePath(shapeRectangle, checkStyle.ShapeRounding, checkStyle.ShapeRounding, checkStyle.ShapeType);
                        graphics.FillPath(new SolidBrush(checkStyle.CheckColor), shapePath);
                        break;
                    }

                case CheckStyle.CheckType.Checkmark:
                    {
                        DrawCheckmark(graphics, checkStyle.CheckColor, rectangle, checkStyle.Thickness);
                    }

                    break;

                default:
                    {
                        throw new ArgumentOutOfRangeException();
                    }
            }
        }

        public static string GetBase64CheckImage()
        {
            return
                "iVBORw0KGgoAAAANSUhEUgAAABMAAAAQCAYAAAD0xERiAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAADsMAAA7DAcdvqGQAAAEySURBVDhPY/hPRUBdw/79+/efVHz77bf/X37+wRAn2bDff/7+91l+83/YmtsYBpJs2ITjz/8rTbrwP2Dlrf9XXn5FkSPJsD13P/y3nHsVbNjyy28w5Ik27NWXX//TNt8DG1S19zFWNRiGvfzy8//ccy9RxEB4wvFnYIMMZl7+//brLwx5EEYx7MP33/9dF18Ha1py8RVcHBR7mlMvgsVXX8X0Hgwz/P379z8yLtz5AKxJdcpFcBj9+v3nf/CqW2Cx5E13UdSiYwzDvv36/d9/BUSzzvRL/0t2PQSzQd57+vEHilp0jGEYCJ9+8hnuGhiee+4Vhjp0jNUwEN566/1/m/mQZJC/48H/zz9+YVWHjHEaBsKgwAZ59eH771jl0TFew0D48osvWMWxYYKGEY///gcAqiuA6kEmfEMAAAAASUVORK5CYII=";
        }

        #endregion Public Methods and Operators
    }
}