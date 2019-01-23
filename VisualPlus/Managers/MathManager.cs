#region License

// -----------------------------------------------------------------------------------------------------------
// 
// Name: MathManager.cs
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
using System.Globalization;
using System.Linq;

#endregion

namespace VisualPlus.Managers
{
    /// <summary>The class for managing mathematical equations.</summary>
    public sealed class MathManager
    {
        #region Public Methods and Operators

        /// <summary>Converts a degree to a radian.</summary>
        /// <param name="angle">The angle.</param>
        /// <returns>The <see cref="float" />.</returns>
        public static float DegreeToRadians(float angle)
        {
            return (float)((angle * Math.PI) / 180);
        }

        /// <summary>Retrieves the number closest from the value collection.</summary>
        /// <param name="value">The initial value to compare with.</param>
        /// <param name="valueCollection">The value collection to search.</param>
        /// <returns>The <see cref="int" />.</returns>
        public static double FindClosestValue(double value, double[] valueCollection)
        {
            return valueCollection.Aggregate((x, y) => Math.Abs(x - value) < Math.Abs(y - value) ? x : y);
        }

        /// <summary>Retrieves the number closest from the value collection.</summary>
        /// <param name="value">The initial value to compare with.</param>
        /// <param name="valueCollection">The value collection to search.</param>
        /// <returns>The <see cref="int" />.</returns>
        public static long FindClosestValue(long value, long[] valueCollection)
        {
            return valueCollection.Aggregate((x, y) => Math.Abs(x - value) < Math.Abs(y - value) ? x : y);
        }

        /// <summary>Gets the fraction.</summary>
        /// <param name="value">Current value.</param>
        /// <param name="total">Total value.</param>
        /// <param name="digits">The number of fractional digits in the return number.</param>
        /// <returns>The <see cref="float" />.</returns>
        public static float GetFraction(double value, double total, int digits)
        {
            // Convert to double value
            double factor = value / 100;

            // Multiply by self
            factor = total * factor;

            // Round to digits
            factor = Math.Round(factor, digits);

            return (float)factor;
        }

        /// <summary>Gets the fraction.</summary>
        /// <param name="value">Current value.</param>
        /// <param name="total">Total value.</param>
        /// <returns>The <see cref="int" />.</returns>
        public static int GetFraction(double value, double total)
        {
            // Convert to decimal value
            double factor = value / 100;

            // Multiply by amount of bars
            factor = total * factor;

            // Round to fraction
            factor = Math.Round(factor, 0);

            return Convert.ToInt32(factor);
        }

        /// <summary>Gets half a radian angle.</summary>
        /// <param name="value">The progress value.</param>
        /// <returns>The <see cref="int" />.</returns>
        public static int GetHalfRadianAngle(int value)
        {
            return int.Parse(Math.Round((value * 180.0) / 100.0, 0).ToString(CultureInfo.CurrentCulture));
        }

        /// <summary>Converts a radian angle to a degree.</summary>
        /// <param name="angle">The angle.</param>
        /// <returns>The <see cref="float" />.</returns>
        public static float RadianToDegree(float angle)
        {
            return (float)(angle * (180.0 / Math.PI));
        }

        #endregion
    }
}