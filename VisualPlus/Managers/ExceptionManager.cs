#region License

// -----------------------------------------------------------------------------------------------------------
// 
// Name: ExceptionManager.cs
// VisualPlus - The VisualPlus Framework (VPF) for WinForms .NET development.
// 
// Created: 10/12/2018 - 11:45 PM
// Last Modified: 01/01/2019 - 11:49 PM
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
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using System.Text;

#endregion

namespace VisualPlus.Managers
{
    [Description("The exception manager.")]
    public sealed class ExceptionManager
    {
        #region Public Methods and Operators

        /// <summary>Returns a bool indicating whether the value is in range.</summary>
        /// <param name="value">The main value.</param>
        /// <param name="minimum">Minimum value.</param>
        /// <param name="maximum">Maximum value.</param>
        /// <param name="round">Round to nearest value when out of range.</param>
        /// <returns>The <see cref="int" />.</returns>
        public static int ArgumentOutOfRangeException(int value, int minimum, int maximum, bool round)
        {
            if ((value >= minimum) && (value <= maximum))
            {
                return value;
            }
            else
            {
                if (round)
                {
                    return MathManager.FindClosestValue(value, new[] { minimum, maximum });
                }
                else
                {
                    throw new ArgumentOutOfRangeException(nameof(value), $@"The value ({value}) must be in range of ({minimum}) to ({maximum}).");
                }
            }
        }

        /// <summary>Returns a bool indicating whether the value is in range.</summary>
        /// <param name="value">The main value.</param>
        /// <param name="minimum">Minimum value.</param>
        /// <param name="maximum">Maximum value.</param>
        /// <returns>The <see cref="bool" />.</returns>
        public static bool ArgumentOutOfRangeException(float value, float minimum, float maximum)
        {
            if ((value >= minimum) && (value <= maximum))
            {
                return true;
            }
            else
            {
                throw new ArgumentOutOfRangeException(nameof(value), $@"The value ({value}) must be in range of ({minimum}) to ({maximum}).");
            }
        }

        /// <summary>Runs an object reference not set to an instance of an object error check.</summary>
        /// <typeparam name="T">The object type.</typeparam>
        /// <param name="source">The object source.</param>
        public static void ObjectReferenceNotSetToAnInstanceOfAnObject<T>(object source)
        {
            if (source != null)
            {
                return;
            }

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

            throw new ArgumentNullException(emptyObject.ToString());
        }

        #endregion
    }
}