#region License

// -----------------------------------------------------------------------------------------------------------
// 
// Name: ExceptionsInvoker.cs
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

using VisualPlus.Localization;
using VisualPlus.Structure;

#endregion

namespace VisualPlus.Managers
{
    /// <summary>The <see cref="Exception" /> invoker.</summary>
    public sealed class ExceptionsInvoker
    {
        #region Public Methods and Operators

        /// <summary>Returns a bool indicating whether the value is in range.</summary>
        /// <param name="value">The value.</param>
        /// <param name="round">Round to nearest value when out of range.</param>
        /// <returns>The <see cref="int" />.</returns>
        public static long ArgumentOutOfRangeException(ValuePairRange value, bool round)
        {
            // Determine if value inside range
            if ((value.Value >= value.Minimum) && (value.Value <= value.Maximum))
            {
                return value.Value;
            }
            else
            {
                // Determine if value needs to be rounded
                if (round)
                {
                    return MathManager.FindClosestValue(value.Value, new[] { value.Minimum, value.Maximum });
                }
                else
                {
                    throw new ArgumentOutOfRangeException(nameof(value), ExceptionsMessages.ArgumentOutOfRangeException(value));
                }
            }
        }

        /// <summary>Returns a bool indicating whether the value is in range.</summary>
        /// <param name="value">The value.</param>
        /// <param name="round">Round to nearest value when out of range.</param>
        /// <returns>The <see cref="int" />.</returns>
        public static double ArgumentOutOfRangeException(ValuePairRangeF value, bool round)
        {
            // Determine if value inside range
            if ((value.Value >= value.Minimum) && (value.Value <= value.Maximum))
            {
                return value.Value;
            }
            else
            {
                // Determine if value needs to be rounded
                if (round)
                {
                    return MathManager.FindClosestValue(value.Value, new[] { value.Minimum, value.Maximum });
                }
                else
                {
                    throw new ArgumentOutOfRangeException(nameof(value), ExceptionsMessages.ArgumentOutOfRangeException(value));
                }
            }
        }

        /// <summary>Returns a bool indicating whether the value is in range.</summary>
        /// <param name="value">The value.</param>
        public static void ArgumentOutOfRangeException(ValuePairRangeF value)
        {
            // Determine if value inside range
            if ((value.Value < value.Minimum) || (value.Value > value.Maximum))
            {
                throw new ArgumentOutOfRangeException(nameof(value), ExceptionsMessages.ArgumentOutOfRangeException(value));
            }
        }

        /// <summary>Returns a bool indicating whether the value is in range.</summary>
        /// <param name="value">The value.</param>
        public static void ArgumentOutOfRangeException(ValuePairRange value)
        {
            // Determine if value inside range
            if ((value.Value < value.Minimum) || (value.Value > value.Maximum))
            {
                throw new ArgumentOutOfRangeException(nameof(value), ExceptionsMessages.ArgumentOutOfRangeException(value));
            }
        }

        /// <summary>Indicates whether the specified object is <see langword="null" />.</summary>
        /// <param name="source">The source to test.</param>
        public static void IsNull(object source)
        {
            if (ExceptionsHandler.IsNull(source))
            {
                throw new ArgumentOutOfRangeException(nameof(source), ExceptionsMessages.IsNull(source));
            }
        }

        /// <summary>Indicates whether the specified string is <see langword="null" /> or an <see cref="Empty" /> string.</summary>
        /// <param name="value">The string to test.</param>
        public static void IsNullOrEmpty(string value)
        {
            if (ExceptionsHandler.IsNullOrEmpty(value))
            {
                throw new ArgumentOutOfRangeException(nameof(value), ExceptionsMessages.IsNullOrEmpty(value));
            }
        }

        /// <summary>Runs an object reference not set to an instance of an object error check.</summary>
        /// <typeparam name="T">The object type.</typeparam>
        /// <param name="source">The object source.</param>
        public static void ObjectReferenceNotSetToAnInstanceOfAnObject<T>(object source)
        {
            if (ExceptionsHandler.ObjectReferenceNotSetToAnInstanceOfAnObject<T>(source))
            {
                throw new ArgumentNullException(ExceptionsMessages.ObjectReferenceNotSetToAnInstanceOfAnObject<T>(source));
            }
        }

        #endregion
    }
}