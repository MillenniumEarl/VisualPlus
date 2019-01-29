#region License

// -----------------------------------------------------------------------------------------------------------
// 
// Name: ExceptionInvoker.cs
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
using System.Collections;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Runtime.InteropServices;

using VisualPlus.Attributes;
using VisualPlus.Localization;
using VisualPlus.Models;
using VisualPlus.Utilities.Debugging;

#endregion

namespace VisualPlus.Managers
{
    /// <summary>The <see cref="ExceptionInvoker" />.</summary>
    [ComVisible(true)]
    [DebuggerStepThrough]
    [DebuggerNonUserCode]
    public static class ExceptionInvoker
    {
        #region Public Methods and Operators

        [DebuggerHidden]
        [Test]
        public static void CanBeAssigned(Type typeToAssign, Type targetType, string paramName)
        {
            if (!targetType.IsAssignableFrom(typeToAssign))
            {
                if (targetType.IsInterface)
                {
                    throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, "The type is not an implemented interface. {0}-{1}", typeToAssign, targetType), paramName);
                }

                throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, "The type is not an inherited type. {0}-{1}", typeToAssign, targetType), paramName);
            }
        }

        [DebuggerHidden]
        [Test]
        public static void CanRead(PropertyInfo property)
        {
            if (!property.CanRead)
            {
                throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, "Property get was not found. {0}-{1}", property.DeclaringType.Name, property.Name));
            }
        }

        [DebuggerHidden]
        [Test]
        public static void CanWrite(PropertyInfo property)
        {
            if (!property.CanWrite)
            {
                throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, "Property set was not found. {0}-{1}", property.DeclaringType.Name, property.Name));
            }
        }

        /// <summary>Throws an instance of the <see cref="InvalidOperationException" /> due the specified condition being invalid.</summary>
        /// <param name="condition">The condition to test.</param>
        /// <param name="message">A message that describes the error.</param>
        [DebuggerHidden]
        public static void IsInvalidOperation(bool condition, string message = "")
        {
            if (condition)
            {
                if (Guard.IsNullOrEmpty(message))
                {
                    throw new InvalidOperationException(ArgumentMessages.IsInvalidOperation());
                }
                else
                {
                    throw new InvalidOperationException(message);
                }
            }
        }

        /// <summary>
        ///     Throws an instance of the <see cref="ArgumentNullException" /> due the specified object being
        ///     <see langword="null" />.
        /// </summary>
        /// <param name="source">The object of the parameter that caused the exception.</param>
        /// <param name="message">A message that describes the error.</param>
        [DebuggerHidden]
        public static void IsNull(object source, string message = "")
        {
            if (Guard.IsNull(source))
            {
                if (Guard.IsNullOrEmpty(message))
                {
                    throw new ArgumentNullException(nameof(source), ArgumentMessages.IsNull(source));
                }
                else
                {
                    throw new ArgumentNullException(nameof(source), message);
                }
            }
        }

        /// <summary>Throws an instance of the <see cref="ArgumentNullException" /> when the specified source type is null.</summary>
        /// <typeparam name="T">The object type.</typeparam>
        /// <param name="source">The object source.</param>
        /// <param name="message">A message that describes the error.</param>
        [DebuggerHidden]
        public static void IsNull<T>(object source, string message = "")
        {
            if (Guard.IsNull<T>(source))
            {
                if (Guard.IsNullOrEmpty(message))
                {
                    throw new ArgumentNullException(nameof(source), ArgumentMessages.IsNull(source));
                }
                else
                {
                    throw new ArgumentNullException(nameof(source), message);
                }
            }
        }

        /// <summary>Throws an instance of the <see cref="ArgumentNullException" /> when the specified source type is null.</summary>
        /// <typeparam name="T">The object type.</typeparam>
        /// <param name="source">The object source.</param>
        /// <param name="message">A message that describes the error.</param>
        [DebuggerHidden]
        public static void IsNull<T>(T source, string message = "") where T : class
        {
            if (Guard.IsNull(source))
            {
                if (Guard.IsNullOrEmpty(message))
                {
                    throw new ArgumentNullException(nameof(source), ArgumentMessages.IsNull(source));
                }
                else
                {
                    throw new ArgumentNullException(nameof(source), message);
                }
            }
        }

        /// <summary>
        ///     Throws an instance of the <see cref="ArgumentNullException" /> due the specified string being
        ///     <see langword="null" /> or an <see cref="Empty" /> string.
        /// </summary>
        /// <param name="value">The string to test.</param>
        /// <param name="message">A message that describes the error.</param>
        [DebuggerHidden]
        public static void IsNullOrEmpty(string value, string message = "")
        {
            if (Guard.IsNullOrEmpty(value))
            {
                if (Guard.IsNullOrEmpty(message))
                {
                    throw new ArgumentNullException(nameof(value), ArgumentMessages.IsNullOrEmpty());
                }
                else
                {
                    throw new ArgumentNullException(nameof(value), message);
                }
            }
        }

        /// <summary>
        ///     Throws an instance of the <see cref="ArgumentException" /> due the specified text being
        ///     <see langword="null" />, empty, or consists of only white-space characters.
        /// </summary>
        /// <param name="value">The string to test.</param>
        /// <param name="message">A message that describes the error.</param>
        [DebuggerHidden]
        public static void IsNullOrWhiteSpace(string value, string message = "")
        {
            if (Guard.IsNullOrWhitespace(value))
            {
                if (Guard.IsNullOrEmpty(message))
                {
                    throw new ArgumentException(ArgumentMessages.IsNullOrWhitespace());
                }
                else
                {
                    throw new ArgumentException(message);
                }
            }
        }

        /// <summary>Throws an <see cref="ArgumentOutOfRangeException" /> if valid.</summary>
        /// <typeparam name="T">Type of the argument to check, it must be an<see cref="IComparable" /> type.</typeparam>
        /// <param name="value">The value to check.</param>
        /// <param name="minimum">The minimum.</param>
        /// <param name="maximum">The maximum.</param>
        /// <param name="message">A message that describes the error.</param>
        /// <param name="paramName">The name of the parameter that caused the current exception.</param>
        [DebuggerHidden]
        public static void IsOutOfRange<T>(T value, T minimum, T maximum, string message = "", string paramName = "") where T : struct, IComparable<T>
        {
            if (Guard.IsOutOfRange(value, minimum, maximum))
            {
                if (Guard.IsNullOrEmpty(paramName))
                {
                    if (Guard.IsNullOrEmpty(message))
                    {
                        throw new ArgumentOutOfRangeException(nameof(value), ArgumentMessages.IsOutOfRangeException(value, minimum, maximum));
                    }
                    else
                    {
                        throw new ArgumentOutOfRangeException(nameof(value), message);
                    }
                }
                else
                {
                    throw new ArgumentOutOfRangeException(paramName, message);
                }
            }
        }

        /// <summary>Throws an <see cref="ArgumentOutOfRangeException" /> if valid.</summary>
        /// <typeparam name="T">Type of the argument to check, it must be an<see cref="IComparable" /> type.</typeparam>
        /// <param name="range">The data range.</param>
        /// <param name="message">A message that describes the error.</param>
        /// <param name="paramName">The name of the parameter that caused the current exception.</param>
        [DebuggerHidden]
        public static void IsOutOfRange<T>(Range<T> range, string message = "", string paramName = "") where T : struct, IComparable<T>
        {
            IsOutOfRange(range.Value, range.Minimum, range.Maximum, message, paramName);
        }

        /// <summary>Throws an instance of the <see cref="ArgumentException" /> due the specified argument is valid.</summary>
        /// <param name="condition">The condition to test.</param>
        /// <param name="message">A message that describes the error.</param>
        /// <param name="paramName">The name of the parameter that caused the current exception.</param>
        [DebuggerHidden]
        public static void IsValid(bool condition, string message = "", string paramName = "")
        {
            if (condition)
            {
                if (Guard.IsNullOrEmpty(message))
                {
                    if (Guard.IsNullOrEmpty(paramName))
                    {
                        throw new ArgumentException(ArgumentMessages.IsValidOperation());
                    }
                    else
                    {
                        throw new ArgumentException(ArgumentMessages.IsValidOperation(), paramName);
                    }
                }
                else
                {
                    throw new ArgumentException(message, paramName);
                }
            }
        }

        [DebuggerHidden]
        [Test]
        public static T NotBeEmpty<T>(T arg, string name) where T : class, IEnumerable
        {
            if (NotBeNull(arg, name).GetEnumerator().MoveNext() == false)
            {
                throw new ArgumentException(name);
            }

            return arg;
        }

        [DebuggerHidden]
        [Test]
        public static T NotBeNull<T>(T arg, string name) where T : class
        {
            if (arg == null)
            {
                throw new ArgumentNullException(name);
            }

            return arg;
        }

        /// <summary>
        ///     Checks an argument to ensure it is in the specified range excluding the edges.
        /// </summary>
        /// <typeparam name="T">
        ///     Type of the argument to check, it must be an <see cref="IComparable" /> type.
        /// </typeparam>
        /// <param name="value">The argument value to check.</param>
        /// <param name="from">The minimum allowed value for the argument.</param>
        /// <param name="to">The maximum allowed value for the argument.</param>
        /// <param name="paramName">The name of the parameter.</param>
        [DebuggerHidden]
        [Test]
        public static void NotOutOfRangeExclusive<T>(T value, T from, T to, string paramName) where T : IComparable
        {
            if ((value != null) && ((value.CompareTo(from) <= 0) || (value.CompareTo(to) >= 0)))
            {
                throw new ArgumentOutOfRangeException(paramName);
            }
        }

        /// <summary>
        ///     Checks an argument to ensure it is in the specified range including the edges.
        /// </summary>
        /// <typeparam name="T">
        ///     Type of the argument to check, it must be an <see cref="IComparable" /> type.
        /// </typeparam>
        /// <param name="value">The argument value to check.</param>
        /// <param name="from">The minimum allowed value for the argument.</param>
        /// <param name="to">The maximum allowed value for the argument.</param>
        /// <param name="paramName">The name of the parameter.</param>
        [DebuggerHidden]
        [Test]
        public static void NotOutOfRangeInclusive<T>(T value, T from, T to, string paramName) where T : IComparable
        {
            if ((value != null) && ((value.CompareTo(from) < 0) || (value.CompareTo(to) > 0)))
            {
                throw new ArgumentOutOfRangeException(paramName);
            }
        }

        #endregion
    }
}