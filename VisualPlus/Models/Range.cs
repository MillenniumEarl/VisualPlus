#region License

// -----------------------------------------------------------------------------------------------------------
// 
// Name: Range.cs
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
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;

using VisualPlus.Constants;

#endregion

namespace VisualPlus.Models
{
    /// <summary>The <see cref="Range{T}" /> struct.</summary>
    /// <typeparam name="T">Type of the argument to check, it must be an<see cref="IComparable" /> type.</typeparam>
    [ComVisible(true)]
    [DebuggerDisplay(DefaultConstants.DefaultDebuggerDisplay)]
    [Serializable]
    public struct Range<T> : IComparable<Range<T>>, IEquatable<Range<T>> where T : struct, IComparable<T>
    {
        #region Constructors and Destructors

        /// <summary>Initializes a new instance of the <see cref="Range{T}" /> struct.</summary>
        /// <param name="value">The value.</param>
        /// <param name="minimum">The minimum.</param>
        /// <param name="max">The maximum.</param>
        public Range(T value, T minimum, T max) : this(value, minimum, max, RangeBoundaryType.Inclusive, RangeBoundaryType.Inclusive)
        {
        }

        /// <summary>Initializes a new instance of the <see cref="Range{T}" /> struct.</summary>
        /// <param name="min">The minimum.</param>
        /// <param name="max">The maximum.</param>
        public Range(T min, T max) : this(min, min, max, RangeBoundaryType.Inclusive, RangeBoundaryType.Inclusive)
        {
        }

        /// <summary>Initializes a new instance of the <see cref="Range{T}" /> struct.</summary>
        /// <param name="value">The value.</param>
        /// <param name="minimum">The minimum.</param>
        /// <param name="maximum">The maximum.</param>
        /// <param name="minimumBoundary">The minimum boundary.</param>
        /// <param name="maximumBoundary">The maximum boundary.</param>
        public Range(T value, T minimum, T maximum, RangeBoundaryType minimumBoundary, RangeBoundaryType maximumBoundary)
        {
            Value = value;
            Minimum = minimum;
            Maximum = maximum;
            MinimumBoundary = minimumBoundary;
            MaximumBoundary = maximumBoundary;
        }

        #endregion

        #region Enums

        /// <summary>The <see cref="RangeBoundaryType" />.</summary>
        public enum RangeBoundaryType
        {
            /// <summary>The inclusive range.</summary>
            Inclusive = 0,

            /// <summary>The exclusive range.</summary>
            Exclusive = 1
        }

        #endregion

        #region Public Properties

        /// <summary>Represents a <see cref="Range{T}" /> structure with its properties left uninitialized.</summary>
        [Browsable(false)]
        public static Range<T> Empty { get; }

        /// <summary>Gets a value that indicates whether the <see cref="Range{T}" /> is the <see cref="Empty" />.</summary>
        [Browsable(false)]
        public bool IsEmpty
        {
            get
            {
                bool isEmpty;

                // Determines if value is equal to default
                if (Equals(Value, default(T)) && Equals(Minimum, default(T)) && Equals(Maximum, default(T)))
                {
                    isEmpty = true;
                }
                else
                {
                    isEmpty = false;
                }

                return isEmpty;
            }
        }

        /// <summary>Gets or sets the maximum value.</summary>
        public T Maximum { get; set; }

        /// <summary>Gets the maximum boundary.</summary>
        public RangeBoundaryType MaximumBoundary { get; set; }

        /// <summary>Gets or sets the Minimum value.</summary>
        public T Minimum { get; set; }

        /// <summary>Gets the minimum boundary.</summary>
        public RangeBoundaryType MinimumBoundary { get; set; }

        /// <summary>Creates an array from all <see cref="IEnumerable{T}" /> range values.</summary>
        [Browsable(false)]
        public T[] ToArray
        {
            get
            {
                var toArrayList = new T[3];
                toArrayList[0] = Value;
                toArrayList[1] = Minimum;
                toArrayList[2] = Maximum;
                return toArrayList;
            }
        }

        /// <summary>Creates an array from a <see cref="IEnumerable{T}" /> ranges minimum and maximum values only.</summary>
        [Browsable(false)]
        public T[] ToRange
        {
            get
            {
                var toRange = new T[2];
                toRange[0] = Minimum;
                toRange[1] = Maximum;
                return toRange;
            }
        }

        /// <summary>Gets or sets the current <see cref="Range{T}" /> value.</summary>
        public T Value { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     Replaces this <see cref="Range{T}" /> with the intersection of itself and the specified
        ///     <see cref="Range{T}" />.
        /// </summary>
        /// <param name="sourceRange">The source Range.</param>
        /// <param name="endRange">The end Range.</param>
        /// <returns>The <see cref="bool" />.</returns>
        public static bool Intersect(Range<T> sourceRange, Range<T> endRange)
        {
            return !((endRange.Minimum.CompareTo(sourceRange.Maximum) > 0) || (sourceRange.Minimum.CompareTo(endRange.Maximum) > 0));
        }

        public static bool operator ==(Range<T> source, Range<T> comparison)
        {
            return source.Equals(comparison);
        }

        public static bool operator !=(Range<T> source, Range<T> comparison)
        {
            return !source.Equals(comparison);
        }

        public int CompareTo(Range<T> range)
        {
            if (Value.CompareTo(range.Value) != 0)
            {
                return Value.CompareTo(range.Value);
            }

            if (Minimum.CompareTo(range.Minimum) != 0)
            {
                return Minimum.CompareTo(range.Minimum);
            }

            if (Maximum.CompareTo(range.Maximum) != 0)
            {
                return Maximum.CompareTo(range.Maximum);
            }

            if (MinimumBoundary != range.MinimumBoundary)
            {
                return MinimumBoundary.CompareTo(range.Minimum);
            }

            if (MaximumBoundary != range.MaximumBoundary)
            {
                return MaximumBoundary.CompareTo(range.MaximumBoundary);
            }

            return 0;
        }

        /// <summary>Determines if another <see cref="Range{T}" /> is inside the bounds of this <see cref="Range{T}" />.</summary>
        /// <param name="range">The range.</param>
        /// <returns>The <see cref="bool" />.</returns>
        public bool ContainsRange(Range<T> range)
        {
            return IsValid() && range.IsValid() && ContainsValue(range.Minimum) && ContainsValue(range.Maximum);
        }

        /// <summary>Determines if the provided value is inside the <see cref="Range{T}" />.</summary>
        /// <param name="value">The value to test.</param>
        /// <returns>The <see cref="bool" />.</returns>
        public bool ContainsValue(T value)
        {
            return (Minimum.CompareTo(value) <= 0) && (value.CompareTo(Maximum) <= 0);
        }

        public bool Equals(Range<T> range)
        {
            return (Value.CompareTo(range.Value) == 0) &&
                   (Minimum.CompareTo(range.Minimum) == 0) &&
                   (Maximum.CompareTo(range.Maximum) == 0) &&
                   (MinimumBoundary == range.MinimumBoundary) &&
                   (MaximumBoundary == range.MaximumBoundary);
        }

        public override bool Equals(object obj)
        {
            return obj is Range<T> range && Equals(range);
        }

        public override int GetHashCode()
        {
            if (IsEmpty)
            {
                return 0;
            }
            else
            {
                // Perform field-by-field XOR of HashCodes
                return ToArray[0].GetHashCode() ^ ToArray[1].GetHashCode() ^ ToArray[2].GetHashCode();
            }
        }

        /// <summary>
        ///     Replaces this <see cref="Range{T}" /> with the intersection of itself and the specified
        ///     <see cref="Range{T}" />.
        /// </summary>
        /// <param name="range">The <see cref="Range{T}" /> with which to intersect. </param>
        /// <returns>The <see cref="bool" />.</returns>
        public bool Intersect(Range<T> range)
        {
            return Intersect(this, range);
        }

        /// <summary>Determines if this <see cref="Range{T}" /> is inside the bounds of another <see cref="Range{T}" />.</summary>
        /// <param name="range">The range.</param>
        /// <returns>The <see cref="bool" />.</returns>
        public bool IsInsideRange(Range<T> range)
        {
            return IsValid() && range.IsValid() && range.ContainsValue(Minimum) && range.ContainsValue(Maximum);
        }

        /// <summary>Determines if the <see cref="Range{T}" /> is valid.</summary>
        /// <returns>The <see cref="bool" />.</returns>
        public bool IsValid()
        {
            return Minimum.CompareTo(Maximum) <= 0;
        }

        public override string ToString()
        {
            if (Debugger.IsAttached)
            {
                return $"[Value = {Value}, Minimum = {Minimum}, Maximum = {Maximum}]";

                // return this.ToDebug("TargetName");
            }
            else
            {
                return base.ToString();
            }
        }

        #endregion
    }
}