#region License

// -----------------------------------------------------------------------------------------------------------
// 
// Name: Test.cs
// VisualPlus - The VisualPlus Framework (VPF) for WinForms .NET development.
// 
// Created: 10/12/2018 - 11:45 PM
// Last Modified: 02/01/2019 - 1:22 AM
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

#endregion

namespace VisualPlus.Attributes
{
    /// <summary>Marks the program elements that need to be tested further.</summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Constructor | AttributeTargets.Delegate | AttributeTargets.Enum | AttributeTargets.Event | AttributeTargets.Field | AttributeTargets.Interface | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Struct, AllowMultiple = true)]
    [Serializable]
    public class Test : Attribute
    {
        #region Fields

        private string text;

        #endregion

        #region Constructors and Destructors

        /// <summary>Initializes a new instance of the <see cref="Test" /> class.</summary>
        /// <param name="text">The text.</param>
        public Test(string text)
        {
            Text = text;
        }

        /// <summary>Initializes a new instance of the <see cref="Test" /> class.</summary>
        public Test()
        {
            text = string.Empty;
        }

        #endregion

        #region Public Properties

        /// <summary>The text of the attribute.</summary>
        public string Text
        {
            get
            {
                return text;
            }

            set
            {
                text = value;
            }
        }

        #endregion
    }
}