#region License

// -----------------------------------------------------------------------------------------------------------
// 
// Name: Guard.cs
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

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;

using VisualPlus.Structure;

#endregion

namespace VisualPlus.Utilities
{
    /// <summary>Represents the <see cref="Guard" /> class.</summary>
    /// <remarks>Assists with validating data before an <see cref="Exception" />.</remarks>
    [ComVisible(true)]
    [DebuggerStepThrough]
    [Serializable]
    public static class Guard
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Indicates whether the specified <see cref="IEnumerable{T}" /> is <see langword="null" /> or an
        ///     <see cref="Empty " /><see cref="IEnumerable{T}" />.
        /// </summary>
        /// <typeparam name="T">The type parameter.</typeparam>
        /// <param name="source">The source.</param>
        /// <returns>The <see cref="bool" />.</returns>
        public static bool IsAnyOrNotNull<T>(IEnumerable<T> source)
        {
            return (source != null) && source.Any();
        }

        /// <summary>Indicates whether the specified object is <see langword="null" />.</summary>
        /// <param name="source">The object to test.</param>
        /// <returns>The <see cref="bool" />.</returns>
        public static bool IsNull(object source)
        {
            bool isNull;

            // Indicates whether the source is equal to null
            if (source == null)
            {
                isNull = true;
            }
            else
            {
                isNull = false;
            }

            return isNull;
        }

        /// <summary>Indicates whether the specified object is <see langword="null" />.</summary>
        /// <typeparam name="T">The source type.</typeparam>
        /// <param name="source">The object to test.</param>
        /// <returns>The <see cref="bool" />.</returns>
        public static bool IsNull<T>(object source)
        {
            bool isNull;

            // Determine if source type is null
            if ((T)source != null)
            {
                isNull = true;
            }
            else
            {
                isNull = false;
            }

            return isNull;
        }

        /// <summary>Indicates whether the specified string is <see langword="null" /> or an <see cref="Empty" /> string.</summary>
        /// <param name="value">The string to test.</param>
        /// <returns>The <see cref="bool" />.</returns>
        public static bool IsNullOrEmpty(string value)
        {
            bool isNullOrEmpty;

            // Indicates whether the value is null or Empty
            if (string.IsNullOrEmpty(value))
            {
                isNullOrEmpty = true;
            }
            else
            {
                isNullOrEmpty = false;
            }

            return isNullOrEmpty;
        }

        /// <summary>
        ///     Indicates whether the specified <see cref="Array" /> is <see langword="null" /> or an <see cref="Empty " />
        ///     <see cref="Array" />.
        /// </summary>
        /// <param name="array">The array.</param>
        /// <returns>The <see cref="bool" />.</returns>
        public static bool IsNullOrEmpty(Array array)
        {
            return (array == null) || (array.Length == 0);
        }

        /// <summary>
        ///     Indicates whether the specified string is <see langword="null" />, empty, or consists of only white-space
        ///     characters.
        /// </summary>
        /// <param name="value">The string to test.</param>
        /// <returns>The <see cref="bool" />.</returns>
        public static bool IsNullOrWhitespace(string value)
        {
            bool isNullOrWhitespace;

            // Indicates whether the value is null, empty or white-space
            if (string.IsNullOrWhiteSpace(value))
            {
                isNullOrWhitespace = true;
            }
            else
            {
                isNullOrWhitespace = false;
            }

            return isNullOrWhitespace;
        }

        /// <summary>Indicates whether the value is out of range.</summary>
        /// <typeparam name="T">Type of the argument to check, it must be an<see cref="IComparable" /> type.</typeparam>
        /// <param name="value">The value to check.</param>
        /// <param name="minimum">The minimum.</param>
        /// <param name="maximum">The maximum.</param>
        /// <returns>The <see cref="bool" />.</returns>
        public static bool IsOutOfRange<T>(T value, T minimum, T maximum) where T : struct, IComparable<T>
        {
            bool isOutOfRange;

            if ((value.CompareTo(minimum) <= 0) || (value.CompareTo(maximum) >= 0))
            {
                isOutOfRange = true;
            }
            else
            {
                isOutOfRange = false;
            }

            return isOutOfRange;
        }

        /// <summary>Indicates whether the value is out of range.</summary>
        /// <typeparam name="T">Type of the argument to check, it must be an<see cref="IComparable" /> type.</typeparam>
        /// <param name="range">The range.</param>
        /// <returns>The <see cref="bool" />.</returns>
        public static bool IsOutOfRange<T>(Range<T> range) where T : struct, IComparable<T>
        {
            return IsOutOfRange(range.Value, range.Minimum, range.Maximum);
        }

        #endregion
    }
}