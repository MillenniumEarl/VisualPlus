#region License

// -----------------------------------------------------------------------------------------------------------
//
// Name: ArgumentMessages.cs
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
using System.Diagnostics;
using System.Reflection;
using System.Text;

using VisualPlus.Models;

#endregion Namespace

namespace VisualPlus.Localization
{
    /// <summary>The <see cref="ArgumentMessages" />.</summary>
    [DebuggerStepThrough]
    public static class ArgumentMessages
    {
        #region Public Methods and Operators

        /// <summary>Returns the <see cref="string" /> when the file not found text.</summary>
        /// <param name="path">The package path.</param>
        /// <returns>The <see cref="string" />.</returns>
        public static string FileNotFound(string path)
        {
            StringBuilder fileNotFound = new StringBuilder();
            fileNotFound.AppendLine("Unable to locate the file using the provided path.");
            fileNotFound.AppendLine("Path: " + path);
            return fileNotFound.ToString();
        }

        /// <summary>Returns the <see cref="string" /> when the operation is invalid.</summary>
        /// <returns>The <see cref="string" />.</returns>
        public static string IsInvalidOperation()
        {
            return "The condition resulted in an invalid operation.";
        }

        /// <summary>
        ///     Returns the <see cref="string" /> when the <see cref="object" /> is <see langword="null" />.
        /// </summary>
        /// <param name="value">>The object of the parameter that caused the exception.</param>
        /// <returns>The <see cref="string" />.</returns>
        public static string IsNull(object value)
        {
            StringBuilder nullObjectOutput = new StringBuilder();
            nullObjectOutput.AppendLine("The object must not be null." + Environment.NewLine);
            nullObjectOutput.AppendLine("Object: " + nameof(value));
            nullObjectOutput.AppendLine("Type: " + value.GetType());
            return nullObjectOutput.ToString();
        }

        /// <summary>
        ///     Returns the <see cref="string" /> when the <see cref="object" /> is <see langword="null" /> or
        ///     <see cref="Empty" />.
        /// </summary>
        /// <typeparam name="T">The object type.</typeparam>
        /// <param name="source">The object source.</param>
        /// <returns>The <see cref="string" />.</returns>
        public static string IsNull<T>(object source)
        {
            // Create object reference message
            StringBuilder isNullString = new StringBuilder();
            isNullString.AppendLine($"The {nameof(source)} object is null.");

            MethodBase methodBase = new StackTrace().GetFrame(1).GetMethod();
            Type memberInfo = new StackTrace().GetFrame(1).GetMethod().DeclaringType;

            if (memberInfo != null)
            {
                string declaringType = memberInfo.ToString();
                isNullString.AppendLine($"Declaring Type: {declaringType}");
            }

            string fileName = new StackTrace().GetFrame(1).GetFileName();

            if (string.IsNullOrEmpty(fileName))
            {
                fileName = "null";
            }

            isNullString.AppendLine($"Method: {methodBase}");
            isNullString.AppendLine($"File Name: {fileName}");

            isNullString.AppendLine();
            isNullString.AppendLine("Object Information:");
            isNullString.AppendLine($"Name: {typeof(T).Name}");
            isNullString.AppendLine($"Namespace: {typeof(T).Namespace}");

            return isNullString.ToString();
        }

        /// <summary>Returns the <see cref="string" /> is <see langword="null" /> or <see cref="Empty" /> text.</summary>
        /// <returns>The <see cref="string" />.</returns>
        public static string IsNullOrEmpty()
        {
            return "The value must not be null or an empty.";
        }

        /// <summary>
        ///     Returns the specified text due to being <see langword="null" />, empty, or consists of only white-space
        ///     characters.
        /// </summary>
        /// <returns>The <see cref="string" />.</returns>
        public static string IsNullOrWhitespace()
        {
            return "The value must not be null or contain any white-space characters.";
        }

        /// <summary>Returns the <see cref="string" /> when the <see cref="ArgumentOutOfRangeException" /> is thrown.</summary>
        /// <typeparam name="T">Type of the argument to check, it must be an<see cref="IComparable" /> type.</typeparam>
        /// <param name="value">The value to check.</param>
        /// <param name="minimum">The minimum.</param>
        /// <param name="maximum">The maximum.</param>
        /// <returns>The <see cref="string" />.</returns>
        public static string IsOutOfRangeException<T>(T value, T minimum, T maximum) where T : struct, IComparable<T>
        {
            return $@"The value ({value}) must be in range of ({minimum}) to ({maximum}).";
        }

        /// <summary>Returns the <see cref="string" /> when the <see cref="ArgumentOutOfRangeException" /> is thrown.</summary>
        /// <typeparam name="T">Type of the argument to check, it must be an<see cref="IComparable" /> type.</typeparam>
        /// <param name="range">The range.</param>
        /// <returns>The <see cref="string" />.</returns>
        public static string IsOutOfRangeException<T>(Range<T> range) where T : struct, IComparable<T>
        {
            return IsOutOfRangeException(range.Value, range.Minimum, range.Maximum);
        }

        /// <summary>Returns the <see cref="string" /> when the argument is valid.</summary>
        /// <returns>The <see cref="string" />.</returns>
        public static string IsValidOperation()
        {
            return "The condition resulted in an valid operation.";
        }

        #endregion Public Methods and Operators
    }
}