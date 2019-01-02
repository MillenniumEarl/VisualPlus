#region License

// -----------------------------------------------------------------------------------------------------------
// 
// Name: ObjectManagement.cs
// VisualPlus - The VisualPlus Framework (VPF) for WinForms .NET development.
// 
// Created: 10/12/2018 - 11:45 PM
// Last Modified: 02/01/2019 - 1:24 AM
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
using System.Linq;

#endregion

namespace VisualPlus.Managers
{
    public sealed class ObjectManagement
    {
        #region Public Methods and Operators

        /// <summary>Retrieves the namespace of the type.</summary>
        /// <param name="type">The type.</param>
        /// <returns>The <see cref="type" />.</returns>
        public static string GetNamespace(Type type)
        {
            return type.Namespace;
        }

        /// <summary>Gets the enumerated object.</summary>
        /// <typeparam name="T">The object type.</typeparam>
        /// <returns>The <see cref="object" />.</returns>
        public static object GetObjectEnumerated<T>()
        {
            if (!IsEnum<T>())
            {
                throw new ArgumentException($@"{nameof(T)} is not an enumerator type.");
            }

            return Enum.GetValues(typeof(T)).Cast<T>();
        }

        /// <summary>Determines whether the object has the method.</summary>
        /// <param name="source">The object source.</param>
        /// <param name="methodName">The name of the method.</param>
        /// <returns>The <see cref="bool" />.</returns>
        public static bool HasMethod(object source, string methodName)
        {
            return source.GetType().GetMethod(methodName) != null;
        }

        /// <summary>Determines whether the object has the method.</summary>
        /// <typeparam name="T">The type.</typeparam>
        /// <param name="methodName">The name of the method.</param>
        /// <returns>The <see cref="bool" />.</returns>
        public static bool HasMethod<T>(string methodName)
        {
            return typeof(T).GetMethod(methodName) != null;
        }

        /// <summary>Determines whether the object is an enum.</summary>
        /// <typeparam name="T">The object type.</typeparam>
        /// <returns>The <see cref="bool" />.</returns>
        public static bool IsEnum<T>()
        {
            return typeof(T).IsEnum;
        }

        #endregion
    }
}