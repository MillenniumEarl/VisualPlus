#region License

// -----------------------------------------------------------------------------------------------------------
// 
// Name: DefaultConstants.cs
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
using System.Drawing.Text;
using System.Globalization;
using System.Threading;

using VisualPlus.Enumerators;

#endregion

namespace VisualPlus.Constants
{
    /// <summary>The <see cref="DefaultConstants" />.</summary>
    public static class DefaultConstants
    {
        #region Constants

        public const bool Animation = true;
        public const int BorderThickness = 1;
        public const ShapeTypes BorderType = ShapeTypes.Rounded;
        public const bool BorderVisible = true;
        public const int ColumnWidth = 60;
        public const string ComponentUpdateMethodName = "UpdateTheme";
        public const string DebugLogFile = "VisualPlus-Debug.log";
        public const string DefaultCategoryText = "VisualExtension";

        /// <summary>Determines how a class or field is displayed in the debugger variable windows.</summary>
        public const string DefaultDebuggerDisplay = "{ToString(),nq}";

        public const Themes DefaultStyle = Themes.Visual;
        public const float EndPoint = 1.0F;
        public const HatchStyle HatchStyle = System.Drawing.Drawing2D.HatchStyle.DarkDownwardDiagonal;
        public const bool HatchVisible = true;
        public const int MaximumAlpha = 255;
        public const int MaximumBorderSize = 24;
        public const int MaximumCheckBoxBorderRounding = 12;
        public const int MaximumCheckBoxSize = 11;
        public const int MaximumRounding = 30;
        public const int MinimumAlpha = 1;
        public const int MinimumBorderSize = 1;
        public const int MinimumCheckBoxBorderRounding = 1;
        public const int MinimumCheckBoxSize = 3;
        public const int MinimumRounding = 1;
        public const float ProgressSize = 5F;
        public const float StartPoint = 0.0F;
        public const bool TextVisible = true;
        public const string ThemeAuthor = "Unknown";
        public const string ThemeExtensionSupportedFileFilter = "Theme|*.xml";
        public const string ThemeName = "Unnamed";
        public const string ThemeResourceLocation = "VisualPlus.Resources.Themes.";
        public const string WatermarkText = "Watermark text";
        public const bool WatermarkVisible = false;

        #endregion

        #region Static Fields

        /// <summary>Gets the current <see cref="CultureInfo" />.</summary>
        public static readonly CultureInfo DefaultCultureInfo = Thread.CurrentThread.CurrentCulture;

        /// <summary>
        ///     Gets the <see cref="NumberFormatInfo" /> that defines the culturally appropriate format of displaying numbers,
        ///     currency, and percentage.
        /// </summary>
        public static readonly NumberFormatInfo DefaultNumberFormatInfo = DefaultCultureInfo.NumberFormat;

        public static readonly Size HatchSize = new Size(2, 2);
        public static readonly string TemplatesFolder = Environment.GetFolderPath(Environment.SpecialFolder.Templates) + @"\VisualPlus Themes\";
        public static readonly string TemplatesFilePath = TemplatesFolder + @"DefaultTheme.xml";
        public static TextRenderingHint TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

        #endregion

        /// <summary>The default rounding values.</summary>
        public struct Rounding
        {
            #region Constants

            public const int BoxRounding = 3;
            public const int Default = 6;
            public const int RoundedRectangle = 12;
            public const int ToggleBorder = 20;
            public const int ToggleButton = 18;

            #endregion
        }
    }
}