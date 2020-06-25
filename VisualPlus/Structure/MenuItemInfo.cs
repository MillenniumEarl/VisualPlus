#region License

// -----------------------------------------------------------------------------------------------------------
//
// Name: MENUITEMINFO.cs
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

#endregion License

#region Namespace

using System;
using System.Runtime.InteropServices;

#endregion Namespace

namespace VisualPlus.Structure
{
    /// <summary>Contains information about a menu item..</summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public struct MENUITEMINFO
    {
        #region Fields

        /// <summary>The size of the structure, in bytes. The caller must set this member to sizeof(MENUITEMINFO).</summary>
        public uint cbSize;

        /// <summary>
        ///     The length of the menu item text, in characters, when information is received about a menu item of the
        ///     MFT_STRING type.
        /// </summary>
        public uint cch;

        /// <summary>An application-defined value associated with the menu item.</summary>
        public IntPtr dwItemData;

        /// <summary>The contents of the menu item.</summary>
        public string dwTypeData;

        /// <summary> Indicates the members to be retrieved or set. This member can be one or more of the following values.</summary>
        public uint fMask;

        /// <summary>The menu item state. This member can be one or more of these values.</summary>
        public uint fState;

        /// <summary>The menu item type. This member can be one or more of the following values.</summary>
        public uint fType;

        /// <summary>
        ///     A handle to the bitmap to display next to the item if it is selected. If this member is NULL, a default bitmap
        ///     is used.
        /// </summary>
        public IntPtr hbmpChecked;

        /// <summary>A handle to the bitmap to be displayed, or it can be one of the values in the following table.</summary>
        public IntPtr hbmpItem;

        /// <summary>A handle to the bitmap to display next to the item if it is not selected.</summary>
        public IntPtr hbmpUnchecked;

        /// <summary>
        ///     A handle to the drop-down menu or submenu associated with the menu item. If the menu item is not an item that
        ///     opens a drop-down menu or submenu, this member is NULL.
        /// </summary>
        public IntPtr hSubMenu;

        /// <summary>An application-defined value that identifies the menu item.</summary>
        public uint wID;

        #endregion Fields

        #region Public Properties

        // Return the size of the structure
        public static uint sizeOf
        {
            get
            {
                return (uint)Marshal.SizeOf(typeof(MENUITEMINFO));
            }
        }

        #endregion Public Properties
    }
}