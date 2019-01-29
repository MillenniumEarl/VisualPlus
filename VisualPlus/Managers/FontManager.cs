#region License

// -----------------------------------------------------------------------------------------------------------
// 
// Name: FontManager.cs
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
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;

using VisualPlus.Localization;

#endregion

namespace VisualPlus.Managers
{
    public sealed class FontManager
    {
        #region Public Methods and Operators

        /// <summary>Construct a font from a bytes array.</summary>
        /// <param name="bytes">The bytes array.</param>
        /// <param name="size">The size.</param>
        /// <param name="fontStyle">The font style.</param>
        /// <returns>The <see cref="Font" />.</returns>
        public static Font ConstructFont(byte[] bytes, float size, FontStyle fontStyle = FontStyle.Regular)
        {
            var _font = bytes;
            IntPtr _buffer = Marshal.AllocCoTaskMem(_font.Length);
            Marshal.Copy(_font, 0, _buffer, _font.Length);

            using (PrivateFontCollection _privateFontCollection = new PrivateFontCollection())
            {
                _privateFontCollection.AddMemoryFont(_buffer, _font.Length);
                return new Font(_privateFontCollection.Families[0].Name, size, fontStyle);
            }
        }

        /// <summary>Construct a font from a font file.</summary>
        /// <param name="fontPath">The font path.</param>
        /// <param name="size">The size.</param>
        /// <param name="fontStyle">The font style.</param>
        /// <returns>The <see cref="Font" />.</returns>
        public static Font ConstructFont(string fontPath, float size, FontStyle fontStyle = FontStyle.Regular)
        {
            var _font = File.ReadAllBytes(fontPath);
            IntPtr _buffer = Marshal.AllocCoTaskMem(_font.Length);
            Marshal.Copy(_font, 0, _buffer, _font.Length);

            using (PrivateFontCollection _privateFontCollection = new PrivateFontCollection())
            {
                _privateFontCollection.AddMemoryFont(_buffer, _font.Length);
                return new Font(_privateFontCollection.Families[0].Name, size, fontStyle);
            }
        }

        /// <summary>Determines whether the font is installed on the system.</summary>
        /// <param name="fontName">The font name.</param>
        /// <returns>The <see cref="bool" />.</returns>
        public static bool FontInstalled(string fontName)
        {
            if (string.IsNullOrEmpty(fontName))
            {
                throw new ArgumentNullException(ArgumentMessages.IsNullOrEmpty());
            }

            return InstalledFonts().Any(_fontFamily => _fontFamily.Name == fontName);
        }

        /// <summary>Retrieves the installed font families.</summary>
        /// <returns>The <see cref="FontFamily" />.</returns>
        public static List<FontFamily> InstalledFonts()
        {
            return new InstalledFontCollection().Families.ToList();
        }

        /// <summary>Resolves to default family if font can't be resolved.</summary>
        /// <param name="fontName">The font name.</param>
        /// <param name="size">The font size.</param>
        /// <param name="defaultFontName">The default font name.</param>
        /// <returns>The <see cref="Font" />.</returns>
        public static Font ResolveFontFamily(string fontName, float size = 8.25F, string defaultFontName = "Arial")
        {
            if (string.IsNullOrEmpty(fontName))
            {
                throw new NoNullAllowedException(nameof(fontName));
            }

            // The font range is between 1 - 1638.
            if ((size <= 0) && (size > 1637))
            {
                throw new ArgumentOutOfRangeException(nameof(size));
            }

            if (string.IsNullOrEmpty(defaultFontName))
            {
                throw new NoNullAllowedException(nameof(fontName));
            }

            if (FontInstalled(fontName))
            {
                return new Font(new FontFamily(fontName), size);
            }
            else
            {
                return FontInstalled(defaultFontName) ? new Font(new FontFamily(defaultFontName), size) : new Font(SystemFonts.DefaultFont.FontFamily, size);
            }
        }

        #endregion
    }
}