#region License

// -----------------------------------------------------------------------------------------------------------
// 
// File: KeysExtensions.cs
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
using System.Windows.Forms;

#endregion

namespace VisualPlus.Extensibility
{
    /// <summary>The collection of the <see cref="KeysExtensions" /> class.</summary>
    public static class KeysExtensions
    {
        #region Public Methods and Operators

        /// <summary>Converts the specified <see cref="Keys" /> codes and modifiers type to a <see cref="int" />.</summary>
        /// <param name="key">Specified key codes and modifiers.</param>
        /// <returns>The <see cref="int" />.</returns>
        public static int ToKeyCode(this Keys key)
        {
            return Convert.ToInt32(key);
        }

        /// <summary>Converts the specified <see cref="int" /> key code to a <see cref="Keys" />.</summary>
        /// <param name="keyCode">The value to convert to an enumeration member.</param>
        /// <returns>The <see cref="Keys" />.</returns>
        public static Keys ToKeys(this int keyCode)
        {
            return (Keys)Enum.ToObject(typeof(Keys), keyCode);
        }

        #endregion
    }
}