#region License

// -----------------------------------------------------------------------------------------------------------
// 
// Name: dwmapi.cs
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

using System.Runtime.InteropServices;
using System.Security;

#endregion

namespace VisualPlus.Native
{
    [SuppressUnmanagedCodeSecurity]
    public static class dwmapi
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Obtains a value that indicates whether Desktop Window Manager (DWM) composition is enabled. Applications on
        ///     machines running Windows 7 or earlier can listen for composition state changes by handling the
        ///     WM_DWMCOMPOSITIONCHANGED notification.
        /// </summary>
        /// <param name="enabled">
        ///     A pointer to a value that, when this function returns successfully, receives TRUE if DWM
        ///     composition is enabled; otherwise, FALSE.
        /// </param>
        [DllImport("dwmapi.dll", CharSet = CharSet.Auto)]
        public static extern void DwmIsCompositionEnabled(out bool enabled);

        #endregion
    }
}