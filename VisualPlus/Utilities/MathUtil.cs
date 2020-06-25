#region License

// -----------------------------------------------------------------------------------------------------------
//
// Name: MathUtil.cs
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
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

#endregion Namespace

namespace VisualPlus.Utilities
{
    /// <summary>Represents the <see cref="MathUtil" /> class.</summary>
    /// <remarks>Assists with calculating mathematical equations and other less common operations.</remarks>
    public static class MathUtil
    {
        #region Public Methods and Operators

        /// <summary>Tests if 2 numbers are within a specified error tolerance of each other.</summary>
        /// <param name="x">First number</param>
        /// <param name="y">Second number</param>
        /// <param name="error">
        ///     Error tolerance or the allowable difference, expressed as a fraction
        ///     of the magnitude of the largest of the numbers
        /// </param>
        /// <returns>True iff x and y are within the error tolerance of each other</returns>
        public static bool AreApproxEqual(double x, double y, double error)
        {
            double difference = Math.Abs(x - y);
            double magnitude = Math.Max(Math.Abs(x), Math.Abs(y));
            return difference <= error * magnitude;

            // same as difference / magnitude < error, but safe
            // <= is important so it works when both x and y are 0 or exactly the same and error is 0
        }

        /// <summary>
        ///     Text whether all components of 2 vectors (expressed as float arrays) are within an error tolerance of each
        ///     other.
        /// </summary>
        /// <param name="v1">First vector (float array)</param>
        /// <param name="v2">Second vector (float array)</param>
        /// <param name="error">
        ///     Error tolerance or allowable difference, expressed as a fraction
        ///     of the magnitude of the largest of each component of the vectors
        /// </param>
        /// <returns>True iff all components of the vectors are within the error tolerance of each other</returns>
        /// <remarks>The vector arrays must have the same length</remarks>
        public static bool AreApproxEqual(float[] v1, float[] v2, double error)
        {
            if (v1.Length != v2.Length)
            {
                throw new ArgumentException("Incompatible arrays");
            }

            for (int i = 0; i < v1.Length; i++)
            {
                if (!AreApproxEqual(v1[i], v2[i], error))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        ///     Tests if all components of 2 vectors (expressed as double arrays) are within an error tolerance of each
        ///     other.
        /// </summary>
        /// <param name="v1">First vector (double array)</param>
        /// <param name="v2">Second vector (double array)</param>
        /// <param name="error">
        ///     Error tolerance or allowable difference, expressed as a fraction
        ///     of the magnitude of the largest of each component of the vectors
        /// </param>
        /// <returns>True iff all components of the vectors are within the error tolerance of each other</returns>
        public static bool AreApproxEqual(double[] v1, double[] v2, double error)
        {
            if (v1.Length != v2.Length)
            {
                throw new ArgumentException("Incompatible arrays");
            }

            for (int i = 0; i < v1.Length; i++)
            {
                if (!AreApproxEqual(v1[i], v2[i], error))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        ///     Determine if two numbers are close in value
        /// </summary>
        /// <param name="left">First number</param>
        /// <param name="right">Second number</param>
        /// <returns>True iff the first number is close in value to the second</returns>
        public static bool AreClose(double left, double right)
        {
            if (left == right)
            {
                return true;
            }

            double a = (Math.Abs(left) + Math.Abs(right) + 10.0) * 2.2204460492503131E-16;
            double b = left - right;
            return (-a < b) && (a > b);
        }

        /// <summary>Clamps arbitrary IComparable value to range [min, max].</summary>
        /// <typeparam name="T">Type of value or object being clamped</typeparam>
        /// <param name="value">Number to clamp</param>
        /// <param name="min">Minimum value</param>
        /// <param name="max">Maximum value</param>
        /// <returns>Clamped number</returns>
        public static T Clamp<T>(T value, T min, T max) where T : IComparable<T>
        {
            if (Comparer<T>.Default.Compare(value, min) < 0)
            {
                return min;
            }

            if (Comparer<T>.Default.Compare(value, max) > 0)
            {
                return max;
            }

            return value;
        }

        /// <summary>Determines which of two values is the closest to another value.</summary>
        /// <param name="value">Value to find closest number to</param>
        /// <param name="cmp1">First number</param>
        /// <param name="cmp2">Second number</param>
        /// <returns>The closest of the two numbers (or first number, if they are equally close)</returns>
        public static float Closest(float value, float cmp1, float cmp2)
        {
            if (Math.Abs(cmp1 - value) <= Math.Abs(cmp2 - value))
            {
                return cmp1;
            }
            else
            {
                return cmp2;
            }
        }

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
        /// <returns>The <see cref="double" />.</returns>
        public static double FindClosestValue(double value, double[] valueCollection)
        {
            return valueCollection.Aggregate((x, y) => Math.Abs(x - value) < Math.Abs(y - value) ? x : y);
        }

        /// <summary>Retrieves the number closest from the value collection.</summary>
        /// <param name="value">The initial value to compare with.</param>
        /// <param name="valueCollection">The value collection to search.</param>
        /// <returns>The <see cref="int" />.</returns>
        public static int FindClosestValue(int value, int[] valueCollection)
        {
            return valueCollection.Aggregate((x, y) => Math.Abs(x - value) < Math.Abs(y - value) ? x : y);
        }

        /// <summary>Retrieves the number closest from the value collection.</summary>
        /// <param name="value">The initial value to compare with.</param>
        /// <param name="valueCollection">The value collection to search.</param>
        /// <returns>The <see cref="long" />.</returns>
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

        /// <summary>Linearly interpolates from min to max, using a value in the range of 0 to 1.</summary>
        /// <param name="value">A value in the range of 0 to 1</param>
        /// <param name="min">The low end of the range, corresponding to a value of 0</param>
        /// <param name="max">The high end of the range, corresponding to a value of 1</param>
        /// <returns>min + value * (max - min)</returns>
        /// <remarks>A better name for this method is LinearInterp or Lerp.</remarks>
        public static float Interp(float value, float min, float max)
        {
            return min + (value * (max - min));
        }

        /// <summary>Determine if one number is greater than another.</summary>
        /// <param name="left">First number</param>
        /// <param name="right">Second number</param>
        /// <returns>True if the first number is greater than the second, false otherwise</returns>
        public static bool IsGreaterThan(double left, double right)
        {
            return (left > right) && !AreClose(left, right);
        }

        /// <summary>Determine if one number is less than or close to another.</summary>
        /// <param name="left">First number</param>
        /// <param name="right">Second number</param>
        /// <returns>True if the first number is less than or close to the second, false otherwise</returns>
        public static bool IsLessThanOrClose(double left, double right)
        {
            return (left < right) || AreClose(left, right);
        }

        /// <summary>Return maximum of two values.</summary>
        /// <typeparam name="T">Type of compared values</typeparam>
        /// <param name="x">One of two values to compare</param>
        /// <param name="y">Second of two values to compare</param>
        /// <returns>Maximum value</returns>
        public static T Max<T>(T x, T y) where T : IComparable<T>
        {
            return Comparer<T>.Default.Compare(x, y) > 0 ? x : y;
        }

        /// <summary>Return minimum of two values.</summary>
        /// <typeparam name="T">Type of compared values</typeparam>
        /// <param name="x">One of two values to compare</param>
        /// <param name="y">Second of two values to compare</param>
        /// <returns>Minimum value</returns>
        public static T Min<T>(T x, T y) where T : IComparable<T>
        {
            return Comparer<T>.Default.Compare(x, y) < 0 ? x : y;
        }

        /// <summary>Converts a radian angle to a degree.</summary>
        /// <param name="angle">The angle.</param>
        /// <returns>The <see cref="float" />.</returns>
        public static float RadianToDegree(float angle)
        {
            return (float)(angle * (180.0 / Math.PI));
        }

        /// <summary>
        ///     Returns the remainder of dividing x by y such that the remainder's sign matches the divisor's.
        ///     This is different behavior from Math.IEEERemainder(x, y), which can return a negative number
        ///     even if both x and y are positive.
        /// </summary>
        /// <param name="x">Dividend x</param>
        /// <param name="y">Divisor y, must be non-zero</param>
        /// <returns>Remainder, r, such that x = q*y + r where q is an integer and abs(r) is less than abs(y)</returns>
        /// <remarks>
        ///     IEEERemainder takes the quotient (x / y) and rounds it to the nearest even integer
        ///     before computing the remainder. This can produce an unexpected negative number. For example,
        ///     IEEERemainder(3.0, 2.0) returns -1 because 3/2 = 1.5 and the 1.5 is rounded up to 2 so the
        ///     remainder has to be -1.
        /// </remarks>
        public static double Remainder(double x, double y)
        {
            return x - (y * Math.Floor(x / y));
        }

        /// <summary>Remaps value from one range to another.</summary>
        /// <param name="value">Value to be remapped</param>
        /// <param name="min">Old minimum value</param>
        /// <param name="max">Old maximum value</param>
        /// <param name="newMin">New minimum value</param>
        /// <param name="newMax">New maximum value</param>
        /// <returns>Result in [newMin, newMax] equivalent to value's position in [min, max]</returns>
        public static float Remap(float value, float min, float max, float newMin, float newMax)
        {
            return Interp(ReverseInterp(value, min, max), newMin, newMax);
        }

        /// <summary>
        ///     Takes a value in the range of [min,max] and returns a value in the range of [0,1],
        ///     interpolated linearly. Is the inverse of Interp.
        /// </summary>
        /// <param name="value">A value in the range [min,max]</param>
        /// <param name="min">The minimum possible value. Must be less than 'max'.</param>
        /// <param name="max">The maximum possible value. Must be greater than 'min'.</param>
        /// <returns>(value - min) / (max - min)</returns>
        public static float ReverseInterp(float value, float min, float max)
        {
            return (value - min) / (max - min);
        }

        /// <summary>Rounds the value data type to be nearest inside it's range values.</summary>
        /// <param name="value">The value data.</param>
        /// <param name="minimum">The minimum.</param>
        /// <param name="maximum">The maximum.</param>
        /// <returns>The <see cref="long" />.</returns>
        public static long RoundToNearestValue(long value, long minimum, long maximum)
        {
            // Create the range array
            var range = new long[2];
            range[0] = minimum;
            range[1] = maximum;

            return FindClosestValue(value, range);
        }

        /// <summary>Rounds the value data type to be nearest inside it's range values.</summary>
        /// <param name="value">The value data.</param>
        /// <param name="minimum">The minimum.</param>
        /// <param name="maximum">The maximum.</param>
        /// <returns>The <see cref="long" />.</returns>
        public static double RoundToNearestValue(double value, double minimum, double maximum)
        {
            // Create the range array
            var range = new double[2];
            range[0] = minimum;
            range[1] = maximum;

            return FindClosestValue(value, range);
        }

        /// <summary>Rounds the value data type to be nearest inside it's range values.</summary>
        /// <param name="value">The value data.</param>
        /// <param name="minimum">The minimum.</param>
        /// <param name="maximum">The maximum.</param>
        /// <returns>The <see cref="int" />.</returns>
        public static int RoundToNearestValue(int value, int minimum, int maximum)
        {
            // Create the range array
            var range = new int[2];
            range[0] = minimum;
            range[1] = maximum;

            return FindClosestValue(value, range);
        }

        /// <summary>Snaps the given number x to the closest multiple of a step.</summary>
        /// <param name="x">Number to snap</param>
        /// <param name="step">Step to snap to. Must not be negative. Zero is OK.</param>
        /// <returns>Closest multiple of step to number</returns>
        public static double Snap(double x, double step)
        {
            if (step < 0)
            {
                throw new ArgumentException("step can't be negative");
            }

            double result = x;

            const double MinFraction = 1.0 / (double.MaxValue / 2);
            if (Math.Abs(result) * MinFraction < step)
            {
                // safe to divide?
                result = result / step;
                result = Math.Floor(result + 0.5);
                result = result * step;
            }

            return result;
        }

        /// <summary>Snaps the given number x to the closest multiple of a step.</summary>
        /// <param name="x">Number to snap.</param>
        /// <param name="step">Step to snap to. Must not be negative. Zero is OK.</param>
        /// <returns>Closest multiple of step to number</returns>
        public static int Snap(int x, int step)
        {
            if (step < 0)
            {
                throw new ArgumentException("step can't be negative");
            }

            if (step == 0)
            {
                return x; // to keep same semantics as the 'double' version
            }

            int result;
            if (x >= 0)
            {
                result = x + (step / 2);
            }
            else
            {
                result = x - (step / 2);
            }

            result = result - (result % step);

            return result;
        }

        /// <summary>Returns value confined to range [min, max), "wrapping around" at either end.</summary>
        /// <param name="value">Number to wrap</param>
        /// <param name="min">Minimum value</param>
        /// <param name="max">Maximum value</param>
        /// <returns>Wrapped number</returns>
        public static int Wrap(int value, int min, int max)
        {
            int n;
            if (value >= min)
            {
                n = (value - min) / (max - min);
            }
            else
            {
                n = (((value - min) + 1) / (max - min)) - 1;
            }

            value -= n * (max - min);
            return value;
        }

        #endregion Public Methods and Operators
    }
}