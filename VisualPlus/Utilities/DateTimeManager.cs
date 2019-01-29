#region License

// -----------------------------------------------------------------------------------------------------------
// 
// Name: DateTimeManager.cs
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

using VisualPlus.Enumerators;

#endregion

namespace VisualPlus.Utilities
{
    public class DateTimeManager
    {
        #region Public Methods and Operators

        /// <summary>Compares the dates and determines the comparison type.</summary>
        /// <param name="source">The source.</param>
        /// <param name="comparison">The comparison.</param>
        /// <returns>The <see cref="DateTimeComparer" />.</returns>
        public static DateTimeComparer CompareDates(DateTime source, DateTime comparison)
        {
            int result = DateTime.Compare(source, comparison);

            DateTimeComparer dateTimeComparer;

            if (result < 0)
            {
                dateTimeComparer = DateTimeComparer.Earlier;
            }
            else if (result == 0)
            {
                dateTimeComparer = DateTimeComparer.Same;
            }
            else
            {
                dateTimeComparer = DateTimeComparer.Later;
            }

            return dateTimeComparer;
        }

        /// <summary>Determines if the date is currently expired.</summary>
        /// <param name="expiryDateTime">The expired date time.</param>
        /// <returns>The <see cref="bool" />.</returns>
        public static bool DateExpired(DateTime expiryDateTime)
        {
            bool dateExpired;

            if (DateTime.Now > expiryDateTime)
            {
                dateExpired = true;
            }
            else
            {
                dateExpired = false;
            }

            return dateExpired;
        }

        /// <summary>Determines if the source date is older.</summary>
        /// <param name="source">Thee source.</param>
        /// <param name="comparison">The comparison.</param>
        /// <returns>The <see cref="bool" />.</returns>
        public static bool DateOlder(DateTime source, DateTime comparison)
        {
            bool sourceOlder;

            switch (CompareDates(source, comparison))
            {
                case DateTimeComparer.Earlier:
                    {
                        sourceOlder = false;
                        break;
                    }

                case DateTimeComparer.Same:
                    {
                        sourceOlder = false;
                        break;
                    }

                case DateTimeComparer.Later:
                    {
                        sourceOlder = true;
                        break;
                    }

                default:
                    {
                        throw new ArgumentOutOfRangeException();
                    }
            }

            return sourceOlder;
        }

        #endregion
    }
}