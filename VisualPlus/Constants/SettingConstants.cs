#region License

// -----------------------------------------------------------------------------------------------------------
// 
// Name: SettingConstants.cs
// VisualPlus - The VisualPlus Framework (VPF) for WinForms .NET development.
// 
// Created: 10/12/2018 - 11:45 PM
// Last Modified: 02/01/2019 - 1:23 AM
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
using System.Diagnostics;
using System.Reflection;

#endregion

namespace VisualPlus.Constants
{
    public class SettingConstants
    {
        #region Constants

        public const string DefaultCategoryText = "VisualExtension";
        public const float EndPoint = 1.0F;
        public const float StartPoint = 0.0F;

        #endregion

        #region Static Fields

        public static readonly string ComponentUpdateMethodName = "UpdateTheme";
        public static readonly int MaximumAlpha = 255;
        public static readonly int MaximumBorderSize = 24;
        public static readonly int MaximumCheckBoxBorderRounding = 12;
        public static readonly int MaximumCheckBoxSize = 11;
        public static readonly int MaximumRounding = 30;
        public static readonly int MinimumAlpha = 1;
        public static readonly int MinimumBorderSize = 1;
        public static readonly int MinimumCheckBoxBorderRounding = 1;
        public static readonly int MinimumCheckBoxSize = 3;
        public static readonly int MinimumRounding = 1;
        public static readonly string ProductName = Assembly.GetExecutingAssembly().GetName().Name;
        public static readonly string ProjectURL = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).LegalTrademarks;
        public static readonly string TemplatesFolder = Environment.GetFolderPath(Environment.SpecialFolder.Templates) + @"\VisualPlus Themes\";
        public static readonly string TemplatesFilePath = TemplatesFolder + @"DefaultTheme.xml";

        public static readonly string ThemeAuthor = "Unknown";
        public static readonly string ThemeExtensionSupportedFileFilter = "Theme|*.xml";
        public static readonly string ThemeName = "Unnamed";
        public static readonly string ThemeResourceLocation = "VisualPlus.Resources.Themes.";

        #endregion
    }
}