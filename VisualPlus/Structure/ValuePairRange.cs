#region License

// -----------------------------------------------------------------------------------------------------------
// 
// Name: ValuePairRange.cs
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

using VisualPlus.Interfaces;
using VisualPlus.Managers;

#endregion

namespace VisualPlus.Structure
{
    /// <summary>
    ///     The <see cref="ValuePairRange" /> structure for use with <see cref="decimal" />, <see cref="int" /> and
    ///     <see cref="long" /> types.
    /// </summary>
    public struct ValuePairRange : IValuePairRange
    {
        #region Fields

        private long inputValue;
        private long maximumValue;
        private long minimumValue;

        #endregion

        #region Constructors and Destructors

        /// <summary>Initializes a new instance of the <see cref="ValuePairRange" /> struct.</summary>
        /// <param name="value">The value.</param>
        /// <param name="minimum">The minimum.</param>
        /// <param name="maximum">The maximum.</param>
        public ValuePairRange(long value, long minimum, long maximum)
        {
            inputValue = value;
            minimumValue = minimum;
            maximumValue = maximum;
        }

        /// <summary>Initializes a new instance of the <see cref="ValuePairRange" /> struct.</summary>
        /// <param name="value">The value.</param>
        /// <param name="minimum">The minimum.</param>
        /// <param name="maximum">The maximum.</param>
        public ValuePairRange(decimal value, decimal minimum, decimal maximum)
        {
            inputValue = (long)value;
            minimumValue = (long)minimum;
            maximumValue = (long)maximum;
        }

        /// <summary>Initializes a new instance of the <see cref="ValuePairRange" /> struct.</summary>
        /// <param name="value">The value.</param>
        /// <param name="minimum">The minimum.</param>
        /// <param name="maximum">The maximum.</param>
        public ValuePairRange(int value, int minimum, int maximum)
        {
            inputValue = value;
            minimumValue = minimum;
            maximumValue = maximum;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Determines whether the <see cref="Value" /> is inside the <see cref="Minimum" /> and <see cref="Maximum" />
        ///     range.
        /// </summary>
        public bool IsInsideRange
        {
            get
            {
                // Determines if value inside the range
                if (ExceptionsHandler.ArgumentOutOfRangeException(this))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        /// <summary>Gets or sets the maximum value.</summary>
        public long Maximum
        {
            get
            {
                return maximumValue;
            }

            set
            {
                maximumValue = value;
            }
        }

        /// <summary>Gets or sets the minimum value.</summary>
        public long Minimum
        {
            get
            {
                return minimumValue;
            }

            set
            {
                minimumValue = value;
            }
        }

        /// <summary>Gets or sets the value.</summary>
        public long Value
        {
            get
            {
                return inputValue;
            }

            set
            {
                inputValue = value;
            }
        }

        #endregion

        #region Public Methods and Operators

        public override string ToString()
        {
            return $"Value: {inputValue}, Minimum: {minimumValue}, Maximum: {maximumValue}";
        }

        #endregion
    }
}