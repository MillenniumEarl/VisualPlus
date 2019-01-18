#region License

// -----------------------------------------------------------------------------------------------------------
// 
// Name: VisualSeparatorDesigner.cs
// VisualPlus - The VisualPlus Framework (VPF) for WinForms .NET development.
// 
// Created: 17/01/2019 - 7:15 PM
// Last Modified: 17/01/2019 - 7:15 PM
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

using System.Reflection;

#endregion

namespace VisualPlus
{
    /// <summary>A collection of the <see cref="VisualPlus" /> API information.</summary>
    public class Library
    {
        #region Static Fields

        public static string Name = "VisualPlus";
        public static string ProjectUrl = "https://github.com/DarkByte7/VisualPlus";

        #endregion

        #region Public Properties

        /// <summary>Returns the <c>AssemblyVersion</c> of this Library.</summary>
        public static string Version
        {
            get
            {
                try
                {
                    Assembly assembly = Assembly.GetExecutingAssembly();
                    AssemblyName assemblyName = assembly.GetName();

                    return assemblyName.Version.ToString();
                }
                catch
                {
                    return "0.0.0.0";
                }
            }
        }

        #endregion
    }
}