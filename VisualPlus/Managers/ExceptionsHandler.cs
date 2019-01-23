#region License

// -----------------------------------------------------------------------------------------------------------
// 
// Name: ExceptionsHandler.cs
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

using System;

using VisualPlus.Structure;

#endregion

namespace VisualPlus.Managers
{
    /// <summary>The <see cref="Exception" />/s handler.</summary>
    public class ExceptionsHandler
    {
        #region Public Methods and Operators

        /// <summary>Indicates whether the value is in range.</summary>
        /// <param name="value">The value.</param>
        /// <returns>The <see cref="bool" />.</returns>
        public static bool ArgumentOutOfRangeException(ValuePairRangeF value)
        {
            // Determine if value inside range
            if ((value.Value >= value.Minimum) && (value.Value <= value.Maximum))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>Indicates whether the value is in range.</summary>
        /// <param name="value">The value.</param>
        /// <returns>The <see cref="bool" />.</returns>
        public static bool ArgumentOutOfRangeException(ValuePairRange value)
        {
            // Determine if value inside range
            if ((value.Value >= value.Minimum) && (value.Value <= value.Maximum))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>Indicates whether the specified object is <see langword="null" />.</summary>
        /// <param name="source">The object to test.</param>
        /// <returns>The <see cref="bool" />.</returns>
        public static bool IsNull(object source)
        {
            // Variables
            bool isNullOrEmpty;

            // Determine if the contents or not Null
            if (source != null)
            {
                isNullOrEmpty = false;
            }
            else
            {
                isNullOrEmpty = true;
            }

            return isNullOrEmpty;
        }

        /// <summary>Indicates whether the specified string is <see langword="null" /> or an <see cref="Empty" /> string.</summary>
        /// <param name="value">The string to test.</param>
        /// <returns>The <see cref="bool" />.</returns>
        public static bool IsNullOrEmpty(string value)
        {
            // Variables
            bool isNullOrEmpty;

            // Determine if the contents or not Null or Empty
            if (string.IsNullOrEmpty(value))
            {
                isNullOrEmpty = false;
            }
            else
            {
                isNullOrEmpty = true;
            }

            return isNullOrEmpty;
        }

        /// <summary>Indicates whether an object reference not set to an instance of an object error check.</summary>
        /// <typeparam name="T">The object type.</typeparam>
        /// <param name="source">The object source.</param>
        /// <returns>The <see cref="bool" />.</returns>
        public static bool ObjectReferenceNotSetToAnInstanceOfAnObject<T>(object source)
        {
            // Variables
            bool initialized;

            // Determine if source type is initialized
            if ((T)source != null)
            {
                initialized = true;
            }
            else
            {
                initialized = false;
            }

            return initialized;
        }

        #endregion
    }
}