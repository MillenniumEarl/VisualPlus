#region License

// -----------------------------------------------------------------------------------------------------------
// 
// Name: RectConverter.cs
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
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Reflection;

using VisualPlus.Structure;

#endregion

namespace VisualPlus.TypeConverters
{
    /// <summary>Converts <see cref="RECT" /> from one data type to another.</summary>
    public class RectConverter : TypeConverter
    {
        #region Public Methods and Operators

        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            // Check if source is of type string
            if (sourceType == typeof(string))
            {
                return true;
            }

            return base.CanConvertFrom(context, sourceType);
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            // Indicates whether can convert to the destination instance
            if (destinationType == typeof(InstanceDescriptor))
            {
                return true;
            }

            return base.CanConvertTo(context, destinationType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            // Variables
            var textData = (string)value;

            // Determine if data is not null
            if (textData != null)
            {
                // Trim whitespace contents
                string tokenData = textData.Trim();

                // Check if data length = 0
                if (tokenData.Length == 0)
                {
                    return null;
                }
                else
                {
                    // Validate current culture if null
                    if (culture == null)
                    {
                        culture = CultureInfo.CurrentCulture;
                    }

                    // Parse the 4 integer values
                    char characterListSeparator = culture.TextInfo.ListSeparator[0];

                    // Split them at the separator
                    var tokens = tokenData.Split(characterListSeparator);

                    // Initialize new dimension array
                    var dimensionsArray = new int[tokens.Length];

                    // Initialize 'int' typeof conversion
                    TypeConverter typeConverter = TypeDescriptor.GetConverter(typeof(int));

                    // Loop thru each dimension value
                    for (var i = 0; i < dimensionsArray.Length; i++)
                    {
                        // Note: ConvertFromString will raise exception if value cannot be converted
                        dimensionsArray[i] = (int)typeConverter.ConvertFromString(context, culture, tokens[i]);
                    }

                    // Validate there is only 4 value dimensions
                    if (dimensionsArray.Length == 4)
                    {
                        // Return new RECT type
                        return new RECT(dimensionsArray[0], dimensionsArray[1], dimensionsArray[2], dimensionsArray[3]);
                    }
                    else
                    {
                        throw new ArgumentException("The " + textData + " failed to parse. " + tokenData);
                    }
                }
            }

            return base.ConvertFrom(context, culture, value);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            // Determines if destination type is not null
            if (destinationType == null)
            {
                throw new ArgumentNullException($"The {nameof(destinationType)} was null.");
            }

            // Indicates whether the value is of a RECT type
            if (value is RECT rectOrigin)
            {
                // Validate destination is also typeof string
                if (destinationType == typeof(string))
                {
                    // Validate current culture
                    if (culture == null)
                    {
                        culture = CultureInfo.CurrentCulture;
                    }

                    // Get list separator
                    string separator = culture.TextInfo.ListSeparator + " ";

                    // Initialize 'int' typeof conversion
                    TypeConverter typeConverter = TypeDescriptor.GetConverter(typeof(int));

                    // Initialize variables
                    var dimensionsArray = new string[4];
                    var dimensionIndex = 0;

                    // Note: ConvertToString will raise exception if value cannot be converted
                    // Construct dimension types
                    dimensionsArray[dimensionIndex++] = typeConverter.ConvertToString(context, culture, rectOrigin.X);
                    dimensionsArray[dimensionIndex++] = typeConverter.ConvertToString(context, culture, rectOrigin.Y);
                    dimensionsArray[dimensionIndex++] = typeConverter.ConvertToString(context, culture, rectOrigin.Width);
                    dimensionsArray[dimensionIndex++] = typeConverter.ConvertToString(context, culture, rectOrigin.Height);

                    // Return the joined string
                    return string.Join(separator, dimensionsArray);
                }

                // Determines if destination type is of InstanceDescriptor
                if (destinationType == typeof(InstanceDescriptor))
                {
                    // Initialize RECT object
                    RECT rect = rectOrigin;

                    // Retrieve constructor info
                    ConstructorInfo constructorInfo = typeof(RECT).GetConstructor(new[] { typeof(int), typeof(int), typeof(int), typeof(int) });

                    // Check if not null
                    if (constructorInfo != null)
                    {
                        return new InstanceDescriptor(constructorInfo, new object[] { rect.X, rect.Y, rect.Width, rect.Height });
                    }
                }
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }

        public override object CreateInstance(ITypeDescriptorContext context, IDictionary propertyValues)
        {
            // Safety check
            if (propertyValues == null)
            {
                throw new ArgumentNullException($"The {nameof(propertyValues)} is null.");
            }

            // Retrieve the property values by name
            object x = propertyValues["X"];
            object y = propertyValues["Y"];
            object width = propertyValues["Width"];
            object height = propertyValues["Height"];

            // Checks if all the values are initialized
            if ((x == null) || (y == null) || (width == null) || (height == null) || !(x is int) || !(y is int) || !(width is int) || !(height is int))
            {
                throw new ArgumentException($"The {nameof(propertyValues)} is data value is invalid.");
            }

            // Return object type
            return new RECT((int)x, (int)y, (int)width, (int)height);
        }

        public override bool GetCreateInstanceSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
        {
            PropertyDescriptorCollection propertyDescriptorCollection = TypeDescriptor.GetProperties(typeof(RECT), attributes);
            return propertyDescriptorCollection.Sort(new[] { "X", "Y", "Width", "Height" });
        }

        public override bool GetPropertiesSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        #endregion
    }
}