#region License

// -----------------------------------------------------------------------------------------------------------
//
// Name: StringExtension.cs
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
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

#endregion Namespace

namespace VisualPlus.Extensibility
{
    /// <summary>The collection of the <see cref="StringExtension" /> class.</summary>
    public static class StringExtension
    {
        #region Public Methods and Operators

        /// <summary>The text between the <see cref="string" />/s.</summary>
        /// <param name="text">The text.</param>
        /// <param name="first">The first.</param>
        /// <param name="last">The last.</param>
        /// <param name="isFirstMatchForEnd">Is first match for end.</param>
        /// <param name="includeFirstAndLast">Include first and last.</param>
        /// <returns>The <see cref="string" />.</returns>
        public static string Between(this string text, string first, string last, bool isFirstMatchForEnd = false, bool includeFirstAndLast = false)
        {
            int _start = text.IndexOf(first, StringComparison.Ordinal);
            if (_start < 0)
            {
                return null;
            }

            if (!includeFirstAndLast)
            {
                _start += first.Length;
            }

            text = text.Substring(_start);

            int _end = isFirstMatchForEnd ? text.IndexOf(last, StringComparison.Ordinal) : text.LastIndexOf(last, StringComparison.Ordinal);
            if (_end < 0)
            {
                return null;
            }

            if (includeFirstAndLast)
            {
                _end += last.Length;
            }

            return text.Remove(_end);
        }

        /// <summary>Determines whether the <see cref="string" /> contains the value with the specified comparison.</summary>
        /// <param name="text">The text.</param>
        /// <param name="value">The value to compare.</param>
        /// <param name="comparison">The comparison.</param>
        /// <returns>The <see cref="bool" />.</returns>
        public static bool Contains(this string text, string value, StringComparison comparison)
        {
            return text.IndexOf(value, comparison) >= 0;
        }

        /// <summary>Converts the <see cref="string" /> HTML to a <see cref="Color" />.</summary>
        /// <param name="withoutHash">The HTML color. (Don't include hash '#')</param>
        /// <returns>The <see cref="Color" />.</returns>
        public static Color FromHtml(this string withoutHash)
        {
            return ColorTranslator.FromHtml("#" + withoutHash);
        }

        /// <summary>Converts the hex to bytes.</summary>
        /// <param name="hex">The hex.</param>
        /// <returns>The <see cref="byte" />.</returns>
        public static byte[] HexToBytes(this string hex)
        {
            var bytes = new byte[hex.Length / 2];
            for (var i = 0; i < bytes.Length; i++)
            {
                bytes[i] = Convert.ToByte(hex.Substring(i * 2, 2), 16);
            }

            return bytes;
        }

        /// <summary>Determines whether the <see cref="string" /> is a number.</summary>
        /// <param name="instance">The <see cref="string" /> instance.</param>
        /// <returns>The <see cref="string" />.</returns>
        public static bool IsNumber(this string instance)
        {
            int _number;
            return int.TryParse(instance, out _number);
        }

        /// <summary>Moves the <see cref="string" /> left.</summary>
        /// <param name="text">The text.</param>
        /// <param name="length">The length.</param>
        /// <returns>The <see cref="string" />.</returns>
        public static string Left(this string text, int length)
        {
            if (length < 1)
            {
                return string.Empty;
            }

            return length < text.Length ? text.Substring(0, length) : text;
        }

        /// <summary>The amount of lines in the <see cref="string" />.</summary>
        /// <param name="text">The text.</param>
        /// <returns>The <see cref="int" />.</returns>
        public static int LineCount(this string text)
        {
            // int numLinesMethod = text.Length - text.Replace(Environment.NewLine, string.Empty).Length;
            int numLines = text.Split('\n').Length;
            return numLines;
        }

        /// <summary>Returns the text as a <see cref="string" /><see cref="Array" />.</summary>
        /// <param name="text">The text.</param>
        /// <returns>The <see cref="string" />.</returns>
        public static string[] Lines(this string text)
        {
            return text.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);
        }

        /// <summary>Provides the <see cref="Size" />, in pixels, of the specified text when drawn with the specified font.</summary>
        /// <param name="text">The text to measure.</param>
        /// <param name="font">The font to apply to the measured text.</param>
        /// <returns>The <see cref="Size" />.</returns>
        public static Size MeasureText(this string text, Font font)
        {
            return TextRenderer.MeasureText(text, font);
        }

        /// <summary>Parses the quote <see cref="string" />.</summary>
        /// <param name="text">The text.</param>
        /// <returns>The <see cref="string" />.</returns>
        public static string ParseQuoteString(this string text)
        {
            text = text.Trim();

            int firstQuote = text.IndexOf('"');

            if (firstQuote < 0)
            {
                return text;
            }

            text = text.Substring(firstQuote + 1);

            int secondQuote = text.IndexOf('"');

            if (secondQuote >= 0)
            {
                text = text.Remove(secondQuote);
            }

            return text;
        }

        /// <summary>Removes the left of the <see cref="string" />.</summary>
        /// <param name="text">The text.</param>
        /// <param name="length">The length.</param>
        /// <returns>The <see cref="string" />.</returns>
        public static string RemoveLeft(this string text, int length)
        {
            if (length < 1)
            {
                return string.Empty;
            }

            return length < text.Length ? text.Remove(0, length) : text;
        }

        /// <summary>Removes the right of the <see cref="string" />.</summary>
        /// <param name="text">The text.</param>
        /// <param name="length">The length.</param>
        /// <returns>The <see cref="string" />.</returns>
        public static string RemoveRight(this string text, int length)
        {
            if (length < 1)
            {
                return string.Empty;
            }

            return length < text.Length ? text.Remove(text.Length - length) : text;
        }

        /// <summary>Removes the white spaces.</summary>
        /// <param name="text">The text.</param>
        /// <returns>The <see cref="string" />.</returns>
        public static string RemoveWhiteSpaces(this string text)
        {
            return new string(text.Where(_char => !char.IsWhiteSpace(_char)).ToArray());
        }

        /// <summary>Repeat the text certain amount of times.</summary>
        /// <param name="text">The text.</param>
        /// <param name="count">The amount.</param>
        /// <returns>The <see cref="string" />.</returns>
        public static string Repeat(this string text, int count)
        {
            if (string.IsNullOrEmpty(text) || (count <= 0))
            {
                return null;
            }

            StringBuilder _stringRepeatBuilder = new StringBuilder(text.Length * count);

            for (var i = 0; i < count; i++)
            {
                _stringRepeatBuilder.Append(text);
            }

            return _stringRepeatBuilder.ToString();
        }

        /// <summary>Replaces the old value with the new.</summary>
        /// <param name="text">The text.</param>
        /// <param name="oldValue">The old value.</param>
        /// <param name="newValue">The new value.</param>
        /// <param name="comparison">The comparison.</param>
        /// <returns>The <see cref="string" />.</returns>
        public static string Replace(this string text, string oldValue, string newValue, StringComparison comparison)
        {
            if (string.IsNullOrEmpty(oldValue))
            {
                return text;
            }

            var _previousIndex = 0;
            int _index = text.IndexOf(oldValue, comparison);
            StringBuilder _stringReplaceBuilder = new StringBuilder();

            while (_index != -1)
            {
                _stringReplaceBuilder.Append(text.Substring(_previousIndex, _index - _previousIndex));
                _stringReplaceBuilder.Append(newValue);
                _index += oldValue.Length;

                _previousIndex = _index;
                _index = text.IndexOf(oldValue, _index, comparison);
            }

            _stringReplaceBuilder.Append(text.Substring(_previousIndex));

            return _stringReplaceBuilder.ToString();
        }

        /// <summary>Replace all.</summary>
        /// <param name="text">The text.</param>
        /// <param name="search">The search.</param>
        /// <param name="replace">The replace.</param>
        /// <returns>The <see cref="string" />.</returns>
        public static string ReplaceAll(this string text, string search, Func<string> replace)
        {
            while (true)
            {
                int _location = text.IndexOf(search, StringComparison.Ordinal);

                if (_location < 0)
                {
                    break;
                }

                text = text.Remove(_location, search.Length).Insert(_location, replace());
            }

            return text;
        }

        /// <summary>Replace first.</summary>
        /// <param name="text">The text.</param>
        /// <param name="search">The search.</param>
        /// <param name="replace">The replace.</param>
        /// <param name="result">The result.</param>
        /// <returns>The <see cref="string" />.</returns>
        public static bool ReplaceFirst(this string text, string search, string replace, out string result)
        {
            int _location = text.IndexOf(search, StringComparison.Ordinal);

            if (_location < 0)
            {
                result = text;
                return false;
            }

            result = text.Remove(_location, search.Length).Insert(_location, replace);
            return true;
        }

        /// <summary>Replace with.</summary>
        /// <param name="text">The str.</param>
        /// <param name="search">The search.</param>
        /// <param name="replace">The replace.</param>
        /// <param name="occurrence">The occurrence.</param>
        /// <param name="comparison">The comparison.</param>
        /// <returns>The <see cref="string" />.</returns>
        public static string ReplaceWith(this string text, string search, string replace, int occurrence = 0, StringComparison comparison = StringComparison.InvariantCultureIgnoreCase)
        {
            if (string.IsNullOrEmpty(search))
            {
                return text;
            }

            var _count = 0;

            while ((occurrence == 0) || (occurrence > _count))
            {
                int _location = text.IndexOf(search, comparison);
                if (_location < 0)
                {
                    break;
                }

                _count++;
                text = text.Remove(_location, search.Length).Insert(_location, replace);
            }

            return text;
        }

        /// <summary>Reverse the <see cref="string" />.</summary>
        /// <param name="text">The text.</param>
        /// <returns>The <see cref="string" />.</returns>
        public static string Reverse(this string text)
        {
            var _chars = text.ToCharArray();
            Array.Reverse(_chars);
            return new string(_chars);
        }

        /// <summary>Moves the <see cref="string" /> right.</summary>
        /// <param name="text">The text.</param>
        /// <param name="length">The length.</param>
        /// <returns>The <see cref="string" />.</returns>
        public static string Right(this string text, int length)
        {
            if (length < 1)
            {
                return string.Empty;
            }

            return length < text.Length ? text.Substring(text.Length - length) : text;
        }

        /// <summary>Converts the html <see cref="string" /> by the alpha value.</summary>
        /// <param name="htmlColor">The html color.</param>
        /// <param name="alpha">The alpha value.</param>
        /// <returns>The <see cref="Color" />.</returns>
        public static Color ToColor(this string htmlColor, int alpha = 255)
        {
            if (htmlColor == "#00FFFFFF")
            {
                return Color.Transparent;
            }
            else
            {
                return Color.FromArgb(alpha > 255 ? 255 : alpha, ColorTranslator.FromHtml(htmlColor));
            }
        }

        /// <summary>Converts the <see cref="string" /> to a <see cref="Font" />.</summary>
        /// <param name="fontName">The font name.</param>
        /// <param name="size">The size.</param>
        /// <param name="fontStyle">The font style.</param>
        /// <param name="unit">The unit.</param>
        /// <returns>The <see cref="Font" />.</returns>
        public static Font ToFont(this string fontName, float size, FontStyle fontStyle = FontStyle.Regular, GraphicsUnit unit = GraphicsUnit.Pixel)
        {
            return new Font(fontName, size, fontStyle, unit);
        }

        /// <summary>Converts the Base64 <see cref="string" /> to an <see cref="Image" />.</summary>
        /// <param name="base64Text">The Base64 text value.</param>
        /// <returns>The <see cref="Image" />.</returns>
        public static Image ToImage(this string base64Text)
        {
            using (MemoryStream _image = new MemoryStream(Convert.FromBase64String(base64Text)))
            {
                return Image.FromStream(_image);
            }
        }

        /// <summary>Converts the html <see cref="string" /> by the alpha value.</summary>
        /// <param name="htmlColor">The html color.</param>
        /// <param name="alpha">The alpha value.</param>
        /// <param name="size">The size.</param>
        /// <param name="startCap">The start cap.</param>
        /// <param name="endCap">The end cap.</param>
        /// <returns>The <see cref="SolidBrush" />.</returns>
        public static Pen ToPen(this string htmlColor, int alpha = 255, float size = 1, LineCap startCap = LineCap.Custom, LineCap endCap = LineCap.Custom)
        {
            return new Pen(Color.FromArgb(alpha > 255 ? 255 : alpha, ColorTranslator.FromHtml(htmlColor)), size) { StartCap = startCap, EndCap = endCap };
        }

        /// <summary>Converts the html <see cref="string" /> by the alpha value.</summary>
        /// <param name="htmlColor">The html color.</param>
        /// <param name="alpha">The alpha value.</param>
        /// <returns>The <see cref="SolidBrush" />.</returns>
        public static SolidBrush ToSolidBrush(this string htmlColor, int alpha = 255)
        {
            return new SolidBrush(Color.FromArgb(alpha > 255 ? 255 : alpha, ColorTranslator.FromHtml(htmlColor)));
        }

        /// <summary>Truncate the <see cref="string" />.</summary>
        /// <param name="text">The text.</param>
        /// <param name="maxLength">The maximum length.</param>
        /// <returns>The <see cref="string" />.</returns>
        public static string Truncate(this string text, int maxLength)
        {
            if (!string.IsNullOrEmpty(text) && (text.Length > maxLength))
            {
                return text.Substring(0, maxLength);
            }

            return text;
        }

        /// <summary>Truncate the <see cref="string" />.</summary>
        /// <param name="text">The text.</param>
        /// <param name="maxLength">The maximum length.</param>
        /// <param name="endings">The endings.</param>
        /// <param name="truncateFromRight">Truncate from right toggle.</param>
        /// <returns>The <see cref="string" />.</returns>
        public static string Truncate(this string text, int maxLength, string endings, bool truncateFromRight = true)
        {
            if (string.IsNullOrEmpty(text) || (text.Length <= maxLength))
            {
                return text;
            }

            int _length = maxLength - endings.Length;

            if (_length <= 0)
            {
                return text;
            }

            if (truncateFromRight)
            {
                text = text.Left(_length) + endings;
            }
            else
            {
                text = endings + text.Right(_length);
            }

            return text;
        }

        #endregion Public Methods and Operators
    }
}