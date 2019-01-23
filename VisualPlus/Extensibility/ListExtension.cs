#region License

// -----------------------------------------------------------------------------------------------------------
// 
// Name: ListExtension.cs
// VisualPlus - The VisualPlus Framework (VPF) for WinForms .NET development.
// 
// Created: 10/12/2018 - 11:45 PM
// Last Modified: 22/01/2019 - 11:55 PM
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

using System.Collections;

#endregion

namespace VisualPlus.Extensibility
{
    public static class ListExtension
    {
        #region Public Methods and Operators

        /// <summary>Retrieve the next ID from the <see cref="IList" />.</summary>
        /// <param name="list">The list.</param>
        /// <returns>The <see cref="int" />.</returns>
        public static int GetNextID(this IList list)
        {
            return list.Count + 1;
        }

        /// <summary>Determines if the list is <see cref="Empty"/>.</summary>
        /// <param name="list">The list.</param>
        /// <returns>The <see cref="bool" />.</returns>
        public static bool IsEmpty(this IList list)
        {
            if (list == null)
            {
                return true;
            }

            return list.Count <= 0;
        }

        /// <summary>Determines if the index is valid for the collection.</summary>
        /// <param name="list">The list.</param>
        /// <param name="index">The zero-based index of the item.</param>
        /// <returns>The <see cref="bool" />.</returns>
        public static bool IsValidIndex(this IList list, int index)
        {
            return (index >= 0) && (index < list.Count);
        }

        #endregion
    }
}