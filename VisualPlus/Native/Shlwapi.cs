#region License

// -----------------------------------------------------------------------------------------------------------
//
// Name: Shlwapi.cs
//
// Copyright (c) 2018 - 2019 VisualPlus <https://darkbyte7.github.io/VisualPlus/>
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
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

#endregion Namespace

namespace VisualPlus.Native
{
    /// <summary>Represents the <see cref="Shlwapi" /> class.</summary>
    /// <remarks>For the assistance of accessing unmanaged method calls.</remarks>
    [SuppressUnmanagedCodeSecurity]
    public static class Shlwapi
    {
        #region Constants

        /// <summary>
        ///     The name of the DLL that contains the unmanaged method. This can include an assembly display name, if the DLL is
        ///     included in an assembly.
        /// </summary>
        private const string DllName = "shlwapi.dll";

        #endregion Constants

        #region Public Methods and Operators

        /// <summary>Truncates a path to fit within a certain number of characters by replacing path components with ellipses.</summary>
        /// <param name="hDc">A handle to the device context used for font metrics. This value can be NULL.</param>
        /// <param name="pszPath">
        ///     A pointer to a null-terminated string of length MAX_PATH that contains the path to be modified.
        ///     On return, this buffer will contain the modified string.
        /// </param>
        /// <param name="dx">The width, in pixels, in which the string must fit.</param>
        /// <returns>
        ///     Returns TRUE if the path was successfully compacted to the specified width. Returns FALSE on failure, or if
        ///     the base portion of the path would not fit the specified width.
        /// </returns>
        [DllImport(DllName, CharSet = CharSet.Auto)]
        public static extern bool PathCompactPath(IntPtr hDc, [In][Out] StringBuilder pszPath, int dx);

        #endregion Public Methods and Operators
    }
}