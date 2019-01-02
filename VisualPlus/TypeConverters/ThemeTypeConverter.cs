#region License

// -----------------------------------------------------------------------------------------------------------
// 
// Name: ThemeTypeConverter.cs
// VisualPlus - The VisualPlus Framework (VPF) for WinForms .NET development.
// 
// Created: 10/12/2018 - 11:45 PM
// Last Modified: 02/01/2019 - 1:27 AM
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
using System.Globalization;

using VisualPlus.Structure;

#endregion

namespace VisualPlus.TypeConverters
{
    public class ThemeTypeConverter : ExpandableObjectConverter
    {
        #region Public Methods and Operators

        /// <summary>Can convert context from source type.</summary>
        /// <param name="context">The context.</param>
        /// <param name="sourceType">The source type.</param>
        /// <returns>The <see cref="bool" />.</returns>
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            // Sets the property to read only.
            return false;
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            object result = null;

            if (value is Theme theme && (destinationType == typeof(string)))
            {
                string text = $@"{theme.Information.Name} by {theme.Information.Author}";

                result = text;
            }

            return result ?? base.ConvertTo(context, culture, value, destinationType);
        }

        #endregion
    }
}