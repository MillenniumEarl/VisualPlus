#region License

// -----------------------------------------------------------------------------------------------------------
// 
// Name: VisualDebuggerDisplay.cs
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
using System.Collections.Generic;

using VisualPlus.Attributes;

#endregion

namespace VisualPlus.Utilities.Debugging
{
    /// <summary>Represents the <see cref="VisualDebuggerDisplay" /> class.</summary>
    /// <remarks>Serializes <see cref="object" /> for the <see cref="ToDebug" /> attribute.</remarks>
    [Test("Add support for collection of properties not just limited to basic 3.")]
    public static class VisualDebuggerDisplay
    {
        #region Public Methods and Operators

        /// <summary>Serializes the target to a more read able string for debugging.</summary>
        /// <param name="target">The target instance.</param>
        /// <param name="propertyName">The property name.</param>
        /// <returns>The <see cref="string" />.</returns>
        public static string Serialize(object target, string propertyName)
        {
            return Serialize(target, propertyName, DebuggerDisplayFormat.Default);
        }

        /// <summary>Serializes the target to a more read able string for debugging.</summary>
        /// <param name="target">The target instance.</param>
        /// <param name="propertyName">The property name.</param>
        /// <param name="format">The formatting display.</param>
        /// <returns>The <see cref="string" />.</returns>
        public static string Serialize(object target, string propertyName, DebuggerDisplayFormat format)
        {
            // Load property value
            object valueResult = target.GetType().GetProperty(propertyName)?.GetValue(target, null);

            // Output data is null
            string valueData = Convert.ToString(valueResult);
            if (string.IsNullOrEmpty(valueData))
            {
                valueData = "null";
            }

            // Initialize defaults
            var targetID = new KeyValuePair<string, string>("Type", target.GetType().Name);
            var propertyNamePair = new KeyValuePair<string, string>("Name", propertyName);
            var propertyValuePair = new KeyValuePair<string, string>("Value", valueData);

            // Format types to string
            string objectFormat = GenerateID(targetID, format.ObjectValueSeparator);
            string propertyFormat = GenerateID(propertyNamePair, format.ObjectValueSeparator);
            string propertyValueFormat = GenerateID(propertyValuePair, format.ObjectValueSeparator);

            return string.Format("{0}{3}{2}{4}{2}{5}{1}", format.Prefix, format.Suffix, format.GroupSpacingSeparator, objectFormat, propertyFormat, propertyValueFormat);
        }

        /// <summary>Returns a string that represents the current object for debug details.</summary>
        /// <param name="target">The target instance.</param>
        /// <param name="propertyName">The property name.</param>
        /// <returns>The <see cref="string" />.</returns>
        public static string ToDebug(this object target, string propertyName)
        {
            return Serialize(target, propertyName);
        }

        #endregion

        #region Methods

        /// <summary>Generate a single identification output with its value pair.</summary>
        /// <param name="keyValuePair">The key value pair.</param>
        /// <param name="separatorFormat">The separator format.</param>
        /// <returns>The <see cref="string" />.</returns>
        private static string GenerateID(KeyValuePair<string, string> keyValuePair, string separatorFormat)
        {
            return string.Format("{1}{0}{2}", separatorFormat, keyValuePair.Key, keyValuePair.Value);
        }

        #endregion
    }
}