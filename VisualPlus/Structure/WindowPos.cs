#region License

// -----------------------------------------------------------------------------------------------------------
// 
// Name: WINDOWPOS.cs
// 
// Copyright (c) 2019 - 2019 VisualPlus <https://darkbyte7.github.io/VisualPlus/>
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
using System.Runtime.InteropServices;

using VisualPlus.Enumerators;

#endregion

namespace VisualPlus.Structure
{
    /// <summary>The structure contains information about the size and position of a window.</summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct WINDOWPOS
    {
        #region Fields

        /// <summary>The window width, in pixels.</summary>
        public int cx;

        /// <summary>The window height, in pixels.</summary>
        public int cy;

        /// <summary>The window position. This member can be one or more of the values specified in SetWindowPosFlags enumerator.</summary>
        public SetWindowPosFlags flags;

        /// <summary>A handle to the window.</summary>
        public IntPtr hwnd;

        /// <summary>
        ///     The position of the window in Z order (front-to-back position). This member can be a handle to the window
        ///     behind which this window is placed, or can be one of the special values listed with the SetWindowPos function.
        /// </summary>
        public IntPtr hWndInsertAfter;

        /// <summary>The position of the left edge of the window.</summary>
        public int x;

        /// <summary>The position of the top edge of the window.</summary>
        public int y;

        #endregion
    }
}