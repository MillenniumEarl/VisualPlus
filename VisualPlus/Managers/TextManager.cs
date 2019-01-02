#region License

// -----------------------------------------------------------------------------------------------------------
// 
// Name: TextManager.cs
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
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Windows.Forms;

#endregion

namespace VisualPlus.Managers
{
    public sealed class TextManager
    {
        #region Public Methods and Operators

        /// <summary>Convert the content alignment to string alignment.</summary>
        /// <param name="alignment">The content alignment.</param>
        /// <param name="orientation">The string alignment to convert to.</param>
        /// <returns>The <see cref="StringAlignment" />.</returns>
        public static StringAlignment ConvertContentAlignmentToStringAlignment(ContentAlignment alignment, Orientation orientation)
        {
            StringAlignment _stringAlignment;

            switch (orientation)
            {
                case Orientation.Horizontal:
                    {
                        switch (alignment)
                        {
                            case ContentAlignment.TopLeft:
                            case ContentAlignment.MiddleLeft:
                            case ContentAlignment.BottomLeft:
                                {
                                    _stringAlignment = StringAlignment.Near;
                                    break;
                                }

                            case ContentAlignment.TopCenter:
                            case ContentAlignment.MiddleCenter:
                            case ContentAlignment.BottomCenter:
                                {
                                    _stringAlignment = StringAlignment.Center;
                                    break;
                                }

                            case ContentAlignment.TopRight:
                            case ContentAlignment.MiddleRight:
                            case ContentAlignment.BottomRight:
                                {
                                    _stringAlignment = StringAlignment.Far;
                                    break;
                                }

                            default:
                                throw new ArgumentOutOfRangeException(nameof(alignment), alignment, null);
                        }

                        break;
                    }

                case Orientation.Vertical:
                    {
                        switch (alignment)
                        {
                            case ContentAlignment.TopLeft:
                            case ContentAlignment.TopCenter:
                            case ContentAlignment.TopRight:
                                {
                                    _stringAlignment = StringAlignment.Near;
                                    break;
                                }

                            case ContentAlignment.MiddleLeft:
                            case ContentAlignment.MiddleCenter:
                            case ContentAlignment.MiddleRight:
                                {
                                    _stringAlignment = StringAlignment.Center;
                                    break;
                                }

                            case ContentAlignment.BottomLeft:
                            case ContentAlignment.BottomCenter:
                            case ContentAlignment.BottomRight:
                                {
                                    _stringAlignment = StringAlignment.Far;
                                    break;
                                }

                            default:
                                throw new ArgumentOutOfRangeException(nameof(alignment), alignment, null);
                        }

                        break;
                    }

                default:
                    {
                        throw new ArgumentOutOfRangeException(nameof(orientation), orientation, null);
                    }
            }

            return _stringAlignment;
        }

        /// <summary>Measures the specified multi-line string when draw with the specified font.</summary>
        /// <param name="text">The text to measure.</param>
        /// <param name="font">The font to apply to the measured text.</param>
        /// <param name="graphics">The specified graphics input.</param>
        /// <returns>The <see cref="SizeF" />.</returns>
        public static Size MeasureMultiLineText(string text, Font font, Graphics graphics = null)
        {
            Graphics _graphics = graphics ?? new Control().CreateGraphics();
            StringReader _stringReader = new StringReader(text);
            Size _textSize = new Size(0, 0);

            string _line;
            while ((_line = _stringReader.ReadLine()) != null)
            {
                int _width = Convert.ToInt32(_graphics.MeasureString(_line, font).Width);
                int _height = Convert.ToInt32(_graphics.MeasureString(_line, font).Height);

                Size _stringSize = new Size(_width, _height);
                _textSize.Height += _stringSize.Height;

                if (_textSize.Width < _stringSize.Width)
                {
                    _textSize.Width = _stringSize.Width;
                }
            }

            return _textSize;
        }

        /// <summary>Measures the specified string when draw with the specified font.</summary>
        /// <param name="text">The text to measure.</param>
        /// <param name="font">The font to apply to the measured text.</param>
        /// <param name="graphics">The specified graphics input.</param>
        /// <returns>The <see cref="Size" />.</returns>
        public static Size MeasureText(string text, Font font, Graphics graphics = null)
        {
            Graphics _graphics = graphics ?? new Control().CreateGraphics();
            int _width = Convert.ToInt32(_graphics.MeasureString(text, font).Width);
            int _height = Convert.ToInt32(_graphics.MeasureString(text, font).Height);
            return new Size(_width, _height);
        }

        /// <summary>Measures the specified string when draw with the specified font.</summary>
        /// <param name="text">The text to measure.</param>
        /// <param name="font">The font to apply to the measured text.</param>
        /// <returns>The <see cref="Size" />.</returns>
        public static Size MeasureTextRenderer(string text, Font font)
        {
            return TextRenderer.MeasureText(text, font);
        }

        /// <summary>Removes the line breaks and new lines from the text.</summary>
        /// <param name="text">The text.</param>
        /// <returns>The <see cref="string" />.</returns>
        public static string RemoveLineBreaks(string text)
        {
            return text.Replace(Environment.NewLine, " ");
        }

        /// <summary>Compares the strings using invariant culture for Turkish-I support.</summary>
        /// <param name="string1">The first string.</param>
        /// <param name="string2">The second string.</param>
        /// <param name="ignoreCase">Whether to ignore the casing on the strings.</param>
        /// <returns>The <see cref="bool" />.</returns>
        public static bool SafeCompareStrings(string string1, string string2, bool ignoreCase)
        {
            if ((string1 == null) || (string2 == null))
            {
                return false;
            }

            if (string1.Length != string2.Length)
            {
                return false;
            }

            return string.Compare(string1, string2, ignoreCase, CultureInfo.InvariantCulture) == 0;
        }

        /// <summary>Set the string format alignments.</summary>
        /// <param name="horizontalAlignment">The horizontal alignment.</param>
        /// <param name="verticalAlignment">The vertical alignment.</param>
        /// <returns>The <see cref="StringFormat" />.</returns>
        public static StringFormat SetStringFormat(StringAlignment horizontalAlignment = StringAlignment.Center, StringAlignment verticalAlignment = StringAlignment.Center)
        {
            return new StringFormat { Alignment = horizontalAlignment, LineAlignment = verticalAlignment };
        }

        /// <summary>Truncate a string, including multi-line strings.</summary>
        /// <param name="text">The text to measure.</param>
        /// <param name="font">The font to apply to the measured text.</param>
        /// <param name="width">The width.</param>
        /// <param name="graphics">The specified graphics input.</param>
        /// <returns>The <see cref="string" />.</returns>
        public static string TruncateString(string text, Font font, int width, Graphics graphics = null)
        {
            Graphics _graphics = graphics ?? new Control().CreateGraphics();
            string _stringTruncated = string.Empty;

            SizeF _sizeString = MeasureMultiLineText(text, font, _graphics);
            if (_sizeString.Width < width)
            {
                // No work required.
                return text;
            }

            var _stringSize = (int)_graphics.MeasureString("...", font).Width;
            if (_stringSize > width)
            {
                // Unable to fit triple dots.
                return string.Empty;
            }

            StringReader _stringReader = new StringReader(text);
            string _line;
            while ((_line = _stringReader.ReadLine()) != null)
            {
                if (_graphics.MeasureString(_line, font).Width < width)
                {
                    // Original sub-line is fine, doesn't require truncation.
                    _stringTruncated += _line + "\n";
                }
                else
                {
                    // Truncate sub-line.
                    for (int index = _line.Length; index != 0; index--)
                    {
                        string _tempString = _line.Substring(0, index) + "...";
                        if (_graphics.MeasureString(_tempString, font).Width < width)
                        {
                            _stringTruncated += _tempString + "\n";
                            break; // Stop loop to test more strings.
                        }
                    }
                }
            }

            // Remove the trailing line feed for the last line in a sequence.
            if (_stringTruncated.Length > 1)
            {
                string _tempString = _stringTruncated.Remove(_stringTruncated.Length - 1, 1);
                _stringTruncated = _tempString;
            }

            return _stringTruncated;
        }

        #endregion
    }
}