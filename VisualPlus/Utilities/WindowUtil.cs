#region License

// -----------------------------------------------------------------------------------------------------------
// 
// File: WindowUtil.cs
// 
// Copyright (c) 2019 - 2019 VisualPlus <https://darkbyte7.github.io/VisualPlus/>
// All Rights Reserved.
//  
// -------------------------------------------------------------------------------------------------------------
// 
// GNU General Public License v3.0 (GPL-3.0)
//  
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND,
// EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED WARRANTIES OF
// MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
//  
// This program is free software: you can redistribute it and/or modify it under the terms of the GNU
// General Public License as published by the Free Software Foundation, either version 3 of the
// License, or (at your option) any later version.
//  
// This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; 
// without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
// See the GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License along with this program. 
// If not, see <http://www.gnu.org/licenses/>.
//  
// This file is subject to the terms and conditions defined in the file 
// 'LICENSE.md', which should be in the root directory of the source code package.
// 
// -------------------------------------------------------------------------------------------------------------

#endregion

#region Namespace

using System;
using System.Text;
using System.Windows.Forms;

using VisualPlus.Native;

#endregion

namespace VisualPlus.Utilities
{
    /// <summary>Represents the <see cref="WindowUtil" /> class.</summary>
    /// <remarks>Assists with the management of <see cref="Form" />/s and windows.</remarks>
    public sealed class WindowUtil
    {
        #region Public Methods and Operators

        /// <summary>Returns the class name using the specified window handle.</summary>
        /// <param name="hWnd">A handle to the window and, indirectly, the class to which the window belongs.</param>
        /// <param name="defaultName">The default class name to use when <see langword="null" />.</param>
        /// <returns>The <see cref="string" />.</returns>
        public static string GetClassName(IntPtr hWnd, string defaultName = "")
        {
            // Variable
            StringBuilder className = new StringBuilder(100);

            // Retrieves class name
            if (User32.GetClassName(hWnd, className, className.Capacity) > 0)
            {
                return className.ToString();
            }
            else
            {
                return defaultName;
            }
        }

        #endregion
    }
}