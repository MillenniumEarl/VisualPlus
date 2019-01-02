#region License

// -----------------------------------------------------------------------------------------------------------
// 
// Name: VisualTabPageCollectionEditor.cs
// VisualPlus - The VisualPlus Framework (VPF) for WinForms .NET development.
// 
// Created: 10/12/2018 - 11:45 PM
// Last Modified: 01/01/2019 - 11:17 PM
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
using System.ComponentModel.Design;

using VisualPlus.Toolkit.Child;

#endregion

namespace VisualPlus.Collections.CollectionsEditor
{
    internal class VisualTabPageCollectionEditor : CollectionEditor
    {
        #region Constructors and Destructors

        /// <summary>Initializes a new instance of the <see cref="VisualTabPageCollectionEditor" /> class.</summary>
        /// <param name="type">The type.</param>
        public VisualTabPageCollectionEditor(Type type) : base(type)
        {
        }

        #endregion

        #region Methods

        protected override Type CreateCollectionItemType()
        {
            return typeof(VisualTabPage);
        }

        protected override Type[] CreateNewItemTypes()
        {
            // return new[] { typeof(TabPage), typeof(VisualTabPage) };
            return new[] { typeof(VisualTabPage) };
        }

        #endregion
    }
}