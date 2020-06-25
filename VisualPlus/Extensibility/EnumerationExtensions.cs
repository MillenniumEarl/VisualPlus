#region License

// -----------------------------------------------------------------------------------------------------------
//
// Name: EnumerationExtensions.cs
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
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

#endregion Namespace

namespace VisualPlus.Extensibility
{
    /// <summary>The collection of the <see cref="EnumerationExtensions" /> class.</summary>
    public static class EnumerationExtensions
    {
        #region Public Methods and Operators

        /// <summary>One conversion type to another collection type of arrays.</summary>
        /// <typeparam name="T">Type parameter.</typeparam>
        /// <typeparam name="TResult">The output type.</typeparam>
        /// <param name="collection">The collection of items.</param>
        /// <param name="converter">The conversion type.</param>
        /// <returns>the <see cref="TResult{T}" />.</returns>
        public static TResult[] ConvertAll<T, TResult>(this T[] collection, Func<T, TResult> converter)
        {
            int count = collection.Length;
            var results = new TResult[count];

            for (var i = 0; i < count; i++)
            {
                results[i] = converter(collection[i]);
            }

            return results;
        }

        /// <summary>Gets the total number of elements in all the dimensions of the <see cref="Array" />.</summary>
        /// <param name="enumerator">The enumeration type.</param>
        /// <returns>The <see cref="int" />.</returns>
        public static int Count(this Enum enumerator)
        {
            return Enum.GetNames(enumerator.GetType()).Length;
        }

        /// <summary>For each iteration of the enumeration that invokes an action.</summary>
        /// <typeparam name="T">Type parameter.</typeparam>
        /// <param name="collection">The collection of items.</param>
        /// <param name="action">The action to invoke each time.</param>
        public static void ForEach<T>(this IEnumerable<T> collection, Action<T> action)
        {
            foreach (T item in collection)
            {
                action(item);
            }
        }

        /// <summary>Returns the <see cref="Enum" /> attribute description.</summary>
        /// <param name="enumerator">The enumerator.</param>
        /// <returns>The <see cref="string" />.</returns>
        public static string GetDescription(this Enum enumerator)
        {
            // Variables
            FieldInfo fieldInfo = enumerator.GetType().GetField(enumerator.ToString());

            // Safety check
            if (fieldInfo == null)
            {
                return enumerator.ToString();
            }

            var descriptionAttributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);

            // Check if contains any description
            if (descriptionAttributes.Length > 0)
            {
                return descriptionAttributes[0].Description;
            }
            else
            {
                return enumerator.ToString();
            }
        }

        /// <summary>Retrieves an array of the values of the constants in a specified enumeration.</summary>
        /// <param name="instance">The instance.</param>
        /// <typeparam name="T">The object type.</typeparam>
        /// <returns>The <see cref="object" />.</returns>
        public static object GetEnumeratedObject<T>(this T instance) where T : class
        {
            if (!instance.IsEnum())
            {
                throw new ArgumentException($@"{nameof(T)} is not an enumerator type.");
            }

            return Enum.GetValues(typeof(T)).Cast<T>();
        }

        /// <summary>Returns the index value of the <see cref="Enum" />.</summary>
        /// <param name="enumerator">The enumerator.</param>
        /// <returns>The <see cref="int" />.</returns>
        public static int GetIndex(this Enum enumerator)
        {
            Array enumeratedArray = Enum.GetValues(enumerator.GetType());
            return Array.IndexOf(enumeratedArray, enumerator);
        }

        /// <summary>Gets the <see cref="Enum" /> index by the value.</summary>
        /// <param name="enumerator">The enumerator.</param>
        /// <param name="value">Value to search.</param>
        /// <returns>The <see cref="int" />.</returns>
        public static int GetIndexByValue(this Enum enumerator, string value)
        {
            try
            {
                var indexCount = (int)Enum.Parse(enumerator.GetType(), value);
                return indexCount;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return 0;
            }
        }

        /// <summary>Gets the <see cref="Enum" /> value by the index.</summary>
        /// <typeparam name="T">Type parameter.</typeparam>
        /// <param name="enumerator">The enumerator.</param>
        /// <param name="index">The index to search.</param>
        /// <returns>The <see cref="string" />.</returns>
        public static string GetValueByIndex<T>(this Enum enumerator, int index) where T : struct
        {
            Type type = typeof(T);
            if (type.IsEnum && Enum.IsDefined(enumerator.GetType(), index))
            {
                return Enum.GetName(enumerator.GetType(), index);
            }
            else
            {
                return null;
            }
        }

        /// <summary>Gets a value indicating whether the current <see cref="Type" /> represents an enumeration.</summary>
        /// <param name="instance">The instance.</param>
        /// <typeparam name="T">The object type.</typeparam>
        /// <returns>The <see cref="bool" />.</returns>
        public static bool IsEnum<T>(this T instance)
        {
            return typeof(T).IsEnum;
        }

        /// <summary>Returns the <see cref="string" /> as an <see cref="Enum" />.</summary>
        /// <typeparam name="T">Type parameter.</typeparam>
        /// <param name="enumeratorString">The string.</param>
        /// <returns>The <see cref="Enum" />.</returns>
        public static Enum ToEnum<T>(this string enumeratorString) where T : struct
        {
            Type type = typeof(T);

            try
            {
                return (Enum)Enum.Parse(type, enumeratorString);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        /// <summary>Converts the <see cref="Enum" /> to a <see cref="List{T}" /> type.</summary>
        /// <typeparam name="T">Type parameter.</typeparam>
        /// <param name="enumerator">The enumerator.</param>
        /// <returns>The <see cref="List{T}" />.</returns>
        public static List<T> ToList<T>(this Enum enumerator) where T : List<T>
        {
            Type type = typeof(T);
            return !type.IsEnum ? null : Enum.GetValues(type).Cast<T>().ToList();
        }

        #endregion Public Methods and Operators
    }
}