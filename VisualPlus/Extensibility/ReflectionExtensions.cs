#region License

// -----------------------------------------------------------------------------------------------------------
//
// Name: ReflectionExtensions.cs
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
using System.Linq.Expressions;
using System.Reflection;

#endregion Namespace

namespace VisualPlus.Extensibility
{
    /// <summary>The collection of the <see cref="ReflectionExtensions" /> class.</summary>
    public static class ReflectionExtensions
    {
        #region Public Methods and Operators

        /// <summary>Gets the <see cref="MethodInfo" /> for the method to be called.</summary>
        /// <typeparam name="T">The type parameter.</typeparam>
        /// <param name="instance">The instance.</param>
        /// <param name="methodSelector">The method selector.</param>
        /// <returns>The <see cref="MethodInfo" />.</returns>
        public static MethodInfo GetMethod<T>(this T instance, Expression<Func<T, object>> methodSelector)
        {
            return ((MethodCallExpression)methodSelector.Body).Method;
        }

        /// <summary>Gets the <see cref="MethodInfo" /> for the method to be called.</summary>
        /// <typeparam name="T">The type parameter.</typeparam>
        /// <param name="instance">The instance.</param>
        /// <param name="methodSelector">The method selector.</param>
        /// <returns>The <see cref="MethodInfo" />.</returns>
        public static MethodInfo GetMethod<T>(this T instance, Expression<Action<T>> methodSelector)
        {
            return ((MethodCallExpression)methodSelector.Body).Method;
        }

        /// <summary>Gets the name of the <see cref="Type" />.</summary>
        /// <typeparam name="T">The type parameter.</typeparam>
        /// <param name="instance">The <see cref="object" /> instance.</param>
        /// <returns>The <see cref="string" />.</returns>
        public static string GetName<T>(this T instance) where T : class
        {
            if (instance == null)
            {
                return string.Empty;
            }

            return typeof(T).GetProperties()[0].Name;
        }

        /// <summary>Gets the namespace of the <see cref="Type" />.</summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <param name="instance">The object instance.</param>
        /// <returns>The <see cref="type" />.</returns>
        public static string GetNamespace<T>(this T instance)
        {
            return instance.GetType().Namespace;
        }

        /// <summary>Indicates whether the <see cref="MemberInfo" /> contains the specified <see cref="Attribute" /> type.</summary>
        /// <typeparam name="TAttribute">The attribute type.</typeparam>
        /// <param name="member">The member info.</param>
        /// <returns>The <see cref="bool" />.</returns>
        public static bool HasAttribute<TAttribute>(this MemberInfo member) where TAttribute : Attribute
        {
            return member.GetAttributes<TAttribute>().Length > 0;
        }

        /// <summary>
        ///     Indicates whether the <see cref="ICustomAttributeProvider" /> contains the specified <see cref="Attribute" />
        ///     type.
        /// </summary>
        /// <typeparam name="T">The type parameter.</typeparam>
        /// <param name="attributeProvider">The attribute Provider.</param>
        /// <param name="inherit">
        ///     Indicates whether one or more instance of <see cref="Attribute" /> type is defined on this
        ///     member..
        /// </param>
        /// <returns>The <see cref="bool" />.</returns>
        public static bool HasAttribute<T>(this ICustomAttributeProvider attributeProvider, bool inherit)
        {
            return attributeProvider.IsDefined(typeof(T), inherit);
        }

        /// <summary>
        ///     Indicates whether the <see cref="ICustomAttributeProvider" /> contains the specified <see cref="Attribute" />
        ///     type.
        /// </summary>
        /// <typeparam name="T">The type parameter.</typeparam>
        /// <param name="type">The type.</param>
        /// <param name="inherit">
        ///     Indicates whether one or more instance of <see cref="Attribute" /> type is defined on this
        ///     member.
        /// </param>
        /// <returns>The <see cref="bool" />.</returns>
        public static bool HasAttribute<T>(this Type type, bool inherit)
        {
            return HasAttribute<T>((ICustomAttributeProvider)type, inherit);
        }

        /// <summary>Determines if <see cref="ParameterInfo" /> has a default value.</summary>
        /// <param name="instance">The instance.</param>
        /// <returns>The <see cref="bool" />.</returns>
        public static bool HasDefaultValue(this ParameterInfo instance)
        {
            return (instance.Attributes & ParameterAttributes.HasDefault) != 0;
        }

        /// <summary>Indicates whether the <see cref="Type" /> contains the specified method name.</summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <param name="instance">The <see cref="object" /> instance.</param>
        /// <param name="name">The string containing the name of the public method to get.</param>
        /// <returns>The <see cref="bool" />.</returns>
        public static bool HasMethod<T>(this T instance, string name)
        {
            return instance.GetType().GetMethod(name) != null;
        }

        /// <summary>Indicates whether the <see cref="Type" /> contains the specified property name.</summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <param name="instance">The <see cref="object" /> instance.</param>
        /// <param name="name">The string containing the name of the public property to get.</param>
        /// <returns>The <see cref="bool" />.</returns>
        public static bool HasProperty<T>(this T instance, string name)
        {
            return instance.GetType().GetProperty(name) != null;
        }

        /// <summary>Determines whether an instance type can be assigned.</summary>
        /// <typeparam name="TType">The instance type.</typeparam>
        /// <param name="type">The type.</param>
        /// <returns>The <see cref="bool" />.</returns>
        public static bool Is<TType>(this Type type)
        {
            return typeof(TType).IsAssignableFrom(type);
        }

        /// <summary>Indicates whether the source type is <see lang="T" />.</summary>
        /// <typeparam name="T">The type of source.</typeparam>
        /// <param name="source">The object source data.</param>
        /// <returns>The <see cref="bool" />.</returns>
        public static bool IsNotNull<T>(this T source) where T : class
        {
            return !IsNull(source);
        }

        /// <summary>Indicates whether the source type is null.</summary>
        /// <typeparam name="T">The type of source.</typeparam>
        /// <param name="source">The object source data.</param>
        /// <returns>The <see cref="bool" />.</returns>
        public static bool IsNull<T>(this T source) where T : class
        {
            return source == null;
        }

        /// <summary>Indicates whether the <see cref="Type" /> is a numeric value.</summary>
        /// <param name="type">The source type.</param>
        /// <returns>The <see cref="bool" />.</returns>
        public static bool IsNumeric(this Type type)
        {
            // Check if numeric value
            if (!(type == typeof(double)) && !(type == typeof(float)) && !(type == typeof(decimal)) && !(type == typeof(long)) && !(type == typeof(int)) && !(type == typeof(short)) && !(type == typeof(ulong)) && !(type == typeof(uint)) && !(type == typeof(ushort)) && !(type == typeof(byte)))
            {
                return type == typeof(sbyte);
            }

            return true;
        }

        /// <summary>Gets a value indicating whether the <see cref="Type" /> is neither abstract or declared sealed.</summary>
        /// <typeparam name="T">The type parameter.</typeparam>
        /// <param name="source">The source.</param>
        /// <returns>The <see cref="bool" />.</returns>
        public static bool IsStatic<T>(this T source)
        {
            if (source.GetType().IsAbstract)
            {
                return source.GetType().IsSealed;
            }

            return false;
        }

        #endregion Public Methods and Operators
    }
}