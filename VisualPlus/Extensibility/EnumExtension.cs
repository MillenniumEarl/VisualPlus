#region License

// -----------------------------------------------------------------------------------------------------------
// 
// Name: EnumExtension.cs
// VisualPlus - The VisualPlus Framework (VPF) for WinForms .NET development.
// 
// Created: 10/12/2018 - 11:45 PM
// Last Modified: 02/01/2019 - 1:23 AM
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
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

#endregion

namespace VisualPlus.Extensibility
{
    public static class EnumExtension
    {
        #region Public Methods and Operators

        /// <summary>Returns the count length.</summary>
        /// <param name="enumerator">The enumerator.</param>
        /// <returns>The <see cref="int" />.</returns>
        public static int Count(this Enum enumerator)
        {
            return Enum.GetNames(enumerator.GetType()).Length;
        }

        /// <summary>Returns the <see cref="Enum" /> attribute description.</summary>
        /// <param name="enumerator">The enumerator.</param>
        /// <returns>The <see cref="string" />.</returns>
        public static string GetDescription(this Enum enumerator)
        {
            FieldInfo _fieldInfo = enumerator.GetType().GetField(enumerator.ToString());

            if (_fieldInfo == null)
            {
                return enumerator.ToString();
            }

            var _attributes = (DescriptionAttribute[])_fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
            return _attributes.Length > 0 ? _attributes[0].Description : enumerator.ToString();
        }

        /// <summary>Returns the index value of the <see cref="Enum" />.</summary>
        /// <param name="enumerator">The enumerator.</param>
        /// <returns>The <see cref="int" />.</returns>
        public static int GetIndex(this Enum enumerator)
        {
            Array _values = Enum.GetValues(enumerator.GetType());
            return Array.IndexOf(_values, enumerator);
        }

        /// <summary>Gets the enumerator index from the value.</summary>
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

        /// <summary>Gets the enumerator value from the index.</summary>
        /// <typeparam name="T">Type parameter.</typeparam>
        /// <param name="enumerator">The enumerator.</param>
        /// <param name="index">The index to search.</param>
        /// <returns>The <see cref="string" />.</returns>
        public static string GetValueByIndex<T>(this Enum enumerator, int index)
            where T : struct
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

        /// <summary>Returns the string as an enumerator.</summary>
        /// <typeparam name="T">Type parameter.</typeparam>
        /// <param name="enumeratorString">The string.</param>
        /// <returns>The <see cref="Enum" />.</returns>
        public static Enum ToEnum<T>(this string enumeratorString)
            where T : struct
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

        /// <summary>Converts enumerator to a list type.</summary>
        /// <typeparam name="T">Type parameter.</typeparam>
        /// <param name="enumerator">The enumerator.</param>
        /// <returns>The <see cref="List{T}" />.</returns>
        public static List<T> ToList<T>(this Enum enumerator)
            where T : struct
        {
            Type type = typeof(T);
            return !type.IsEnum ? null : Enum.GetValues(type).Cast<T>().ToList();
        }

        #endregion
    }
}