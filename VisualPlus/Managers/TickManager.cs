#region License

// -----------------------------------------------------------------------------------------------------------
// 
// Name: TickManager.cs
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
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;

#endregion

namespace VisualPlus.Managers
{
    [Description("The tick manager.")]
    public sealed class TickManager
    {
        #region Public Methods and Operators

        /// <summary>Draws the tick line.</summary>
        /// <param name="graphics">Graphics controller.</param>
        /// <param name="color">The tick Color.</param>
        /// <param name="rectangle">The rectangle</param>
        /// <param name="frequency">Tick frequency.</param>
        /// <param name="minimum">The minimum.</param>
        /// <param name="maximum">The maximum.</param>
        /// <param name="orientation">The orientation.</param>
        public static void DrawTickLine(Graphics graphics, Color color, RectangleF rectangle, int frequency, int minimum, int maximum, Orientation orientation)
        {
            if (maximum == minimum)
            {
                return;
            }

            Pen _pen = new Pen(color, 1);
            float _frequencySize = GetFrequencyLength(orientation, rectangle, frequency, minimum, maximum);
            int _count = (maximum - minimum) / frequency;

            if ((maximum - minimum) % frequency == 0)
            {
                _count -= 1;
            }

            switch (orientation)
            {
                case Orientation.Horizontal:
                    {
                        for (var i = 0; i <= _count; i++)
                        {
                            graphics.DrawLine(_pen, rectangle.Left + (_frequencySize * i), rectangle.Top, rectangle.Left + (_frequencySize * i), rectangle.Bottom);
                        }

                        graphics.DrawLine(_pen, rectangle.Right, rectangle.Top, rectangle.Right, rectangle.Bottom);
                        break;
                    }

                case Orientation.Vertical:
                    {
                        // Draw each tick
                        for (var i = 0; i <= _count; i++)
                        {
                            graphics.DrawLine(_pen, rectangle.Left, rectangle.Bottom - (_frequencySize * i), rectangle.Right, rectangle.Bottom - (_frequencySize * i));
                        }

                        // Draw last tick at Maximum
                        graphics.DrawLine(_pen, rectangle.Left, rectangle.Top, rectangle.Right, rectangle.Top);
                        break;
                    }

                default:
                    {
                        throw new ArgumentOutOfRangeException(nameof(orientation), orientation, null);
                    }
            }
        }

        /// <summary>Draws the tick text.</summary>
        /// <param name="graphics">Graphics controller.</param>
        /// <param name="color">Fore color.</param>
        /// <param name="rectangle">The rectangle</param>
        /// <param name="frequency">Tick frequency.</param>
        /// <param name="minimum">The minimum.</param>
        /// <param name="maximum">The maximum.</param>
        /// <param name="orientation">The orientation.</param>
        /// <param name="font">The font.</param>
        public static void DrawTickTextLine(Graphics graphics, Color color, RectangleF rectangle, int frequency, int minimum, int maximum, Orientation orientation, Font font)
        {
            if (maximum == minimum)
            {
                return;
            }

            int _count = (maximum - minimum) / frequency;
            if ((maximum - minimum) % frequency == 0)
            {
                _count -= 1;
            }

            StringFormat _stringFormat = new StringFormat
                {
                    FormatFlags = StringFormatFlags.NoWrap,
                    LineAlignment = StringAlignment.Center,
                    Alignment = StringAlignment.Center,
                    Trimming = StringTrimming.EllipsisCharacter,
                    HotkeyPrefix = HotkeyPrefix.Show
                };

            Brush _brush = new SolidBrush(color);
            string _text;
            float _frequencySize = GetFrequencyLength(orientation, rectangle, frequency, minimum, maximum);

            switch (orientation)
            {
                case Orientation.Horizontal:
                    {
                        for (var i = 0; i <= _count; i++)
                        {
                            _text = Convert.ToString(minimum + (frequency * i), 10);
                            graphics.DrawString(_text, font, _brush, rectangle.Left + (_frequencySize * i), rectangle.Top + (rectangle.Height / 2), _stringFormat);
                        }

                        // Draw last tick text at Maximum
                        _text = Convert.ToString(maximum, 10);
                        graphics.DrawString(_text, font, _brush, rectangle.Right, rectangle.Top + (rectangle.Height / 2), _stringFormat);
                        break;
                    }

                case Orientation.Vertical:
                    {
                        // Draw each tick text
                        for (var i = 0; i <= _count; i++)
                        {
                            _text = Convert.ToString(minimum + (frequency * i), 10);
                            graphics.DrawString(_text, font, _brush, rectangle.Left + (rectangle.Width / 2), rectangle.Bottom - (_frequencySize * i), _stringFormat);
                        }

                        // Draw last tick text at Maximum
                        _text = Convert.ToString(maximum, 10);
                        graphics.DrawString(_text, font, _brush, rectangle.Left + (rectangle.Width / 2), rectangle.Top, _stringFormat);
                        break;
                    }

                default:
                    {
                        throw new ArgumentOutOfRangeException(nameof(orientation), orientation, null);
                    }
            }
        }

        #endregion

        #region Methods

        /// <summary>Calculate the frequency length.</summary>
        /// <param name="orientation">The orientation.</param>
        /// <param name="rectangle">The rectangle.</param>
        /// <param name="frequency">The frequency.</param>
        /// <param name="minimum">The minimum.</param>
        /// <param name="maximum">The maximum.</param>
        /// <returns>The <see cref="float" />.</returns>
        private static float GetFrequencyLength(Orientation orientation, RectangleF rectangle, int frequency, int minimum, int maximum)
        {
            float _length;

            switch (orientation)
            {
                case Orientation.Horizontal:
                    {
                        _length = (rectangle.Width * frequency) / (maximum - minimum);
                        break;
                    }

                case Orientation.Vertical:
                    {
                        _length = (rectangle.Height * frequency) / (maximum - minimum);
                        break;
                    }

                default:
                    {
                        throw new ArgumentOutOfRangeException(nameof(orientation), orientation, null);
                    }
            }

            return _length;
        }

        #endregion
    }
}