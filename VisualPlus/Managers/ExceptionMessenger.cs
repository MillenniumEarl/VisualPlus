#region License

// -----------------------------------------------------------------------------------------------------------
// 
// Name: ExceptionMessenger.cs
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
using System.Text;

#endregion

namespace VisualPlus.Managers
{
    public class ExceptionMessenger
    {
        #region Public Methods and Operators

        /// <summary>Create file not found string.</summary>
        /// <param name="path">The package path.</param>
        /// <returns>
        ///     <see cref="string" />
        /// </returns>
        public static string FileNotFound(string path)
        {
            StringBuilder _fileNotFound = new StringBuilder();
            _fileNotFound.AppendLine("Unable to locate the file using the following path. " + path);
            return _fileNotFound.ToString();
        }

        /// <summary>Create is null or empty string.</summary>
        /// <param name="value">The value.</param>
        /// <returns>
        ///     <see cref="string" />
        /// </returns>
        public static string IsNull(object value)
        {
            StringBuilder _isNullOrEmpty = new StringBuilder();
            _isNullOrEmpty.AppendLine("The object is null.");
            _isNullOrEmpty.Append(Environment.NewLine);
            _isNullOrEmpty.AppendLine("Object: " + nameof(value));
            _isNullOrEmpty.AppendLine("Type: " + value.GetType());
            return _isNullOrEmpty.ToString();
        }

        /// <summary>Create is null or empty string.</summary>
        /// <param name="text">The text.</param>
        /// <returns>
        ///     <see cref="string" />
        /// </returns>
        public static string IsNullOrEmpty(string text)
        {
            StringBuilder _isNullOrEmpty = new StringBuilder();
            _isNullOrEmpty.AppendLine("The string is null or empty. " + nameof(text));
            return _isNullOrEmpty.ToString();
        }

        #endregion
    }
}