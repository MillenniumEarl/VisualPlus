#region License

// -----------------------------------------------------------------------------------------------------------
// 
// Name: ExceptionMessenger.cs
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
using System.Diagnostics;
using System.Reflection;
using System.Text;

using VisualPlus.Structure;

#endregion

namespace VisualPlus.Localization
{
    /// <summary>The collection of <see cref="Exception" />/s messages.</summary>
    public class ExceptionsMessages
    {
        #region Public Methods and Operators

        /// <summary>Returns the <see cref="string" /> when the <see langword="ArgumentOutOfRangeException" /> is thrown.</summary>
        /// <param name="valuePairRange">The value Pair Range.</param>
        /// <returns>The <see cref="string" />.</returns>
        public static string ArgumentOutOfRangeException(ValuePairRangeF valuePairRange)
        {
            return $@"The value ({valuePairRange.Value}) must be in range of ({valuePairRange.Minimum}) to ({valuePairRange.Maximum}).";
        }

        /// <summary>Returns the <see cref="string" /> when the <see langword="ArgumentOutOfRangeException" /> is thrown.</summary>
        /// <param name="valuePairRange">The value Pair Range.</param>
        /// <returns>The <see cref="string" />.</returns>
        public static string ArgumentOutOfRangeException(ValuePairRange valuePairRange)
        {
            return $@"The value ({valuePairRange.Value}) must be in range of ({valuePairRange.Minimum}) to ({valuePairRange.Maximum}).";
        }

        /// <summary>Returns the <see cref="string" /> when the file not found string.</summary>
        /// <param name="path">The package path.</param>
        /// <returns>The <see cref="string" />.</returns>
        public static string FileNotFound(string path)
        {
            StringBuilder fileNotFound = new StringBuilder();
            fileNotFound.AppendLine("Unable to locate the file using the provided path.");
            fileNotFound.AppendLine("Path: " + path);
            return fileNotFound.ToString();
        }

        /// <summary>
        ///     Returns the <see cref="string" /> when the <see cref="object" /> is <see langword="null" /> or
        ///     <see cref="Empty" /> string.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The <see cref="string" />.</returns>
        public static string IsNull(object value)
        {
            StringBuilder nullOrEmpty = new StringBuilder();
            nullOrEmpty.AppendLine("The object is null." + Environment.NewLine);
            nullOrEmpty.AppendLine("Object: " + nameof(value));
            nullOrEmpty.AppendLine("Type: " + value.GetType());
            return nullOrEmpty.ToString();
        }

        /// <summary>
        ///     Returns the <see cref="string" /> when the <see cref="string" /> is <see langword="null" /> or
        ///     <see cref="Empty" /> string.
        /// </summary>
        /// <param name="value">The text.</param>
        /// <returns>The <see cref="string" />.</returns>
        public static string IsNullOrEmpty(string value)
        {
            StringBuilder isNullOrEmpty = new StringBuilder();
            isNullOrEmpty.AppendLine("The string is null or empty. " + nameof(value));
            return isNullOrEmpty.ToString();
        }

        /// <summary>Returns the object reference not set to an instance of an object error check.</summary>
        /// <typeparam name="T">The object type.</typeparam>
        /// <param name="source">The object source.</param>
        /// <returns>The <see cref="string" />.</returns>
        public static string ObjectReferenceNotSetToAnInstanceOfAnObject<T>(object source)
        {
            // Create object reference message
            StringBuilder emptyObject = new StringBuilder();
            emptyObject.AppendLine($"The {nameof(source)} object is null.");

            MethodBase methodBase = new StackTrace().GetFrame(1).GetMethod();
            Type memberInfo = new StackTrace().GetFrame(1).GetMethod().DeclaringType;

            if (memberInfo != null)
            {
                string declaringType = memberInfo.ToString();
                emptyObject.AppendLine($"Declaring Type: {declaringType}");
            }

            string fileName = new StackTrace().GetFrame(1).GetFileName();

            if (string.IsNullOrEmpty(fileName))
            {
                fileName = "null";
            }

            emptyObject.AppendLine($"Method: {methodBase}");
            emptyObject.AppendLine($"File Name: {fileName}");

            emptyObject.AppendLine();
            emptyObject.AppendLine("Object Information:");
            emptyObject.AppendLine($"Name: {typeof(T).Name}");
            emptyObject.AppendLine($"Namespace: {typeof(T).Namespace}");

            return emptyObject.ToString();
        }

        #endregion
    }
}