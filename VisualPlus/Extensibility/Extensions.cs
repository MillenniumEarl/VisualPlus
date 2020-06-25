#region License

// -----------------------------------------------------------------------------------------------------------
//
// Name: Extensions.cs
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

#endregion License

#region Namespace

using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;

#endregion Namespace

namespace VisualPlus.Extensibility
{
    /// <summary>Represents the <see cref="Extensions" /> class.</summary>
    public static class Extensions
    {
        #region Public Methods and Operators

        /// <summary>Tries the <see cref="object" /> cast to the <see cref="T" />.</summary>
        /// <typeparam name="T">The type of source.</typeparam>
        /// <param name="source">The object source data.</param>
        /// <returns>The <see cref="bool" />.</returns>
        public static T Cast<T>(this object source) where T : class
        {
            return source as T;
        }

        ///// <summary>Returns the closest <see cref="T" /> from the <see cref="IEnumerable{T}" /> using the specified target.</summary>
        ///// <typeparam name="T">The type.</typeparam>
        ///// <typeparam name="TKey">The key type.</typeparam>
        ///// <param name="collection">The source of the collection.</param>
        ///// <param name="keySelection">The key selector.</param>
        ///// <param name="target">The target key.</param>
        ///// <returns>The <see cref="T" />.</returns>
        // [Obsolete("Broke it.")]
        // public static T Closest<T, TKey>(this IEnumerable<T> collection, Func<T, TKey> keySelection, TKey target) where TKey : IComparable<TKey>
        // {
        // try
        // {
        // if (Guard.IsNull<T>(collection))
        // {
        // return default(T);
        // }
        // else
        // {
        // return collection.Where(x => target.CompareTo(keySelection(x)) <= 0).OrderBy(keySelection).FirstOrDefault();
        // }
        // }
        // catch (Exception e)
        // {
        // Console.WriteLine(e);
        // throw;
        // }

        // }

        ///// <summary>Retrieves the number closest from the value collection.</summary>
        ///// <typeparam name="T">The target type.</typeparam>
        ///// <param name="target">The target value to compare with.</param>
        ///// <param name="collection">The collection to search.</param>
        ///// <returns>The <see cref="int" />.</returns>
        // public static T FindClosestValue<T>(this T target, IEnumerable<T> collection) where T : IComparable<T>
        // {
        // if (Guard.IsNull<T>(target))
        // {
        // return target;
        // }
        // else
        // {
        // return collection.Closest(keyIndexer => keyIndexer, target);
        // }
        // }

        /// <summary>
        ///     Returns an <see cref="Array" /> of the custom <see cref="Attribute" />/s from the <see cref="MemberInfo" /> by
        ///     <see cref="Type" />.
        /// </summary>
        /// <typeparam name="TAttribute">The attribute type.</typeparam>
        /// <param name="instance">The member info.</param>
        /// <returns>The <see cref="bool" />.</returns>
        public static TAttribute[] GetAttributes<TAttribute>(this MemberInfo instance) where TAttribute : Attribute
        {
            return (TAttribute[])instance.GetCustomAttributes(typeof(TAttribute), true);
        }

        /// <summary>
        ///     Returns an <see cref="Array" /> of custom <see cref="Attribute" />/s defined on this member, identified by
        ///     type, or an empty array if there are no custom attributes of that type.
        /// </summary>
        /// <typeparam name="T">The type parameter.</typeparam>
        /// <param name="attributeProvider">The attribute Provider.</param>
        /// <param name="inherit">
        ///     Indicates whether one or more instance of <see cref="Attribute" /> type is defined on this
        ///     member..
        /// </param>
        /// <returns>The <see cref="bool" />.</returns>
        public static T[] GetAttributes<T>(this ICustomAttributeProvider attributeProvider, bool inherit) where T : class
        {
            return (T[])attributeProvider.GetCustomAttributes(typeof(T), inherit);
        }

        /// <summary>Indicates whether the <see cref="Assembly" /> contains the specified <see cref="Attribute" /> type.</summary>
        /// <typeparam name="T">The type parameter.</typeparam>
        /// <param name="assembly">The assembly.</param>
        /// <returns>The <see cref="bool" />.</returns>
        public static T[] GetAttributes<T>(this Assembly assembly) where T : class
        {
            return GetAttributes<T>(assembly, false);
        }

        /// <summary>Indicates whether the <see cref="Type" /> contains the specified <see cref="Attribute" /> type.</summary>
        /// <typeparam name="T">The type parameter.</typeparam>
        /// <param name="type">The type.</param>
        /// <param name="inherit">
        ///     Indicates whether one or more instance of <see cref="Attribute" /> type is defined on this
        ///     member.
        /// </param>
        /// <returns>The <see cref="bool" />.</returns>
        public static T[] GetAttributes<T>(this Type type, bool inherit) where T : class
        {
            return GetAttributes<T>((ICustomAttributeProvider)type, inherit);
        }

        /// <summary>Returns the get value using the string.</summary>
        /// <typeparam name="T">The value type.</typeparam>
        /// <param name="value">The data value.</param>
        /// <returns>The <see cref="string" />.</returns>
        public static T GetValue<T>(string value) where T : struct
        {
            T result;
            Enum.TryParse(value, true, out result);
            return result;
        }

        /// <summary>Indicates whether the <see cref="Type" /> is a <see cref="DateTime" />.</summary>
        /// <param name="type">The type to represent.</param>
        /// <returns>The <see cref="bool" />.</returns>
        public static bool IsDateTime(this Type type)
        {
            return (type != null) && (type == typeof(DateTime));
        }

        /// <summary>Indicates whether the <see cref="Type" /> is a <see cref="decimal" />.</summary>
        /// <param name="type">The type to represent.</param>
        /// <returns>The <see cref="bool" />.</returns>
        public static bool IsDecimal(this Type type)
        {
            return (type != null) && (type == typeof(decimal));
        }

        /// <summary>Indicates whether the <see cref="Type" /> is a floating-point.</summary>
        /// <param name="type">The type to represent.</param>
        /// <returns>The <see cref="bool" />.</returns>
        public static bool IsFloatingPoint(this Type type)
        {
            return (type != null) && ((type == typeof(double)) || (type == typeof(float)));
        }

        /// <summary>Indicates whether the <see cref="Type" /> is an integer.</summary>
        /// <param name="type">The type to represent.</param>
        /// <returns>The <see cref="bool" />.</returns>
        public static bool IsInteger(this Type type)
        {
            return (type != null) &&
                   (IsUnsignedInteger(type) ||
                    (type == typeof(byte)) ||
                    (type == typeof(sbyte)) ||
                    (type == typeof(int)) ||
                    (type == typeof(short)) ||
                    (type == typeof(long)));
        }

        /// <summary>Indicates whether the <see cref="Type" /> is intrinsic.</summary>
        /// <param name="type">The type to represent.</param>
        /// <returns>The <see cref="bool" />.</returns>
        public static bool IsIntrinsic(this Type type)
        {
            return IsInteger(type) ||
                   IsDecimal(type) ||
                   IsFloatingPoint(type) ||
                   IsString(type) ||
                   IsDateTime(type);
        }

        /// <summary>Indicates whether the <see cref="Type" /> is a <see cref="string" />.</summary>
        /// <param name="type">The type to represent.</param>
        /// <returns>The <see cref="bool" />.</returns>
        public static bool IsString(this Type type)
        {
            return (type != null) && (type == typeof(string));
        }

        /// <summary>Indicates whether the <see cref="Type" /> is an unsigned integer.</summary>
        /// <param name="type">The type to represent.</param>
        /// <returns>The <see cref="bool" />.</returns>
        public static bool IsUnsignedInteger(this Type type)
        {
            return (type != null) &&
                   ((type == typeof(uint)) ||
                    (type == typeof(ushort)) ||
                    (type == typeof(ulong)));
        }

        /// <summary>Limits the number exclusively to its range.</summary>
        /// <param name="value">The value.</param>
        /// <param name="minimum">The minimum.</param>
        /// <param name="maximum">The maximum.</param>
        /// <returns>The <see cref="int" />.</returns>
        public static int LimitToRange(this int value, int minimum, int maximum)
        {
            if (value < minimum)
            {
                return minimum;
            }

            if (value > maximum)
            {
                return maximum;
            }

            return value;
        }

        ///// <summary>Rounds the value data type to be nearest inside it's range values.</summary>
        ///// <typeparam name="T">The value type.</typeparam>
        ///// <param name="value">The value data.</param>
        ///// <param name="minimum">The minimum.</param>
        ///// <param name="maximum">The maximum.</param>
        ///// <returns>The <see cref="T" />.</returns>
        // [Obsolete("Broke it.")]
        // public static T RoundToNearestValue<T>(this T value, T minimum, T maximum) where T : struct, IComparable<T>
        // {
        // // Create the range array
        // var range = new T[2];
        // range[0] = minimum;
        // range[1] = maximum;

        // var r = MathManager.FindClosestValue(value, range);

        // return value.FindClosestValue(range);

        // }

        ///// <summary>Rounds the value data type to be nearest inside it's range values.</summary>
        ///// <typeparam name="T">The value type.</typeparam>
        ///// <param name="range">The value data.</param>
        ///// <returns>The <see cref="T" />.</returns>
        // public static T RoundToNearestValue<T>(this Range<T> range) where T : struct, IComparable<T>
        // {
        // return range.Value.FindClosestValue(range.ToRange);
        // }

        /// <summary>Scroll down the <see cref="Panel" />.</summary>
        /// <param name="panel">The panel.</param>
        /// <param name="position">The position.</param>
        public static void ScrollDown(this Panel panel, int position)
        {
            // position passed in should be positive
            using (Control c = new Control { Parent = panel, Height = 1, Top = panel.ClientSize.Height + position })
            {
                panel.ScrollControlIntoView(c);
            }
        }

        /// <summary>Scroll to the bottom of the <see cref="Panel" />.</summary>
        /// <param name="panel">The panel.</param>
        public static void ScrollToBottom(this Panel panel)
        {
            using (Control c = new Control { Parent = panel, Dock = DockStyle.Bottom })
            {
                panel.ScrollControlIntoView(c);
                c.Parent = null;
            }
        }

        /// <summary>Scroll up the <see cref="Panel" />.</summary>
        /// <param name="panel">The panel.</param>
        /// <param name="position">The position.</param>
        public static void ScrollUp(this Panel panel, int position)
        {
            // position passed in should be negative
            using (Control c = new Control { Parent = panel, Height = 1, Top = position })
            {
                panel.ScrollControlIntoView(c);
            }
        }

        /// <summary>Returns the size of an unmanaged <see cref="struct" /> in bytes.</summary>
        /// <typeparam name="T">The type.</typeparam>
        /// <param name="value">The value.</param>
        /// <returns>The <see cref="uint" />.</returns>
        public static uint SizeOf<T>(this T value) where T : struct
        {
            return (uint)Marshal.SizeOf(typeof(T));
        }

        #endregion Public Methods and Operators
    }
}