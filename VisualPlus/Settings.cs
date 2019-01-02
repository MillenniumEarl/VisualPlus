#region License

// -----------------------------------------------------------------------------------------------------------
// 
// Name: Settings.cs
// VisualPlus - The VisualPlus Framework (VPF) for WinForms .NET development.
// 
// Created: 10/12/2018 - 11:45 PM
// Last Modified: 01/01/2019 - 11:33 PM
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

using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;

using VisualPlus.Enumerators;

#endregion

namespace VisualPlus
{
    public class Settings
    {
        #region Static Fields

        public static readonly string DebugLogFile = "VisualPlus-Debug.log";

        #endregion

        public struct DefaultValue
        {
            #region Constants

            public const bool Animation = true;
            public const int BorderThickness = 1;
            public const ShapeTypes BorderType = ShapeTypes.Rounded;
            public const bool BorderVisible = true;
            public const int ColumnWidth = 60;
            public const Themes DefaultStyle = Themes.Visual;
            public const HatchStyle HatchStyle = System.Drawing.Drawing2D.HatchStyle.DarkDownwardDiagonal;
            public const bool HatchVisible = true;
            public const float ProgressSize = 5F;
            public const bool TextVisible = true;
            public const bool WatermarkVisible = false;

            #endregion

            #region Static Fields

            public static readonly Size HatchSize = new Size(2, 2);
            public static readonly string WatermarkText = "Watermark text";
            public static TextRenderingHint TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

            #endregion

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
}