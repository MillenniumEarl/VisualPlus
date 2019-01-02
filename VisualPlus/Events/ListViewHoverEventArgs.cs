#region License

// -----------------------------------------------------------------------------------------------------------
// 
// Name: ListViewHoverEventArgs.cs
// VisualPlus - The VisualPlus Framework (VPF) for WinForms .NET development.
// 
// Created: 10/12/2018 - 11:45 PM
// Last Modified: 01/01/2019 - 11:38 PM
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

using VisualPlus.Enumerators;

#endregion

namespace VisualPlus.Events
{
    public class ListViewHoverEventArgs : EventArgs
    {
        #region Fields

        private int _columnIndex;
        private ListViewHoverTypes _hoverType;
        private int _itemIndex;
        private ListViewRegion _region;

        #endregion

        #region Constructors and Destructors

        /// <summary>Initializes a new instance of the <see cref="ListViewHoverEventArgs" /> class.</summary>
        /// <param name="hoverType">The hover Type.</param>
        /// <param name="itemIndex">The item Index.</param>
        /// <param name="columnIndex">The column Index.</param>
        /// <param name="region">The region.</param>
        public ListViewHoverEventArgs(ListViewHoverTypes hoverType, int itemIndex, int columnIndex, ListViewRegion region)
        {
            _hoverType = hoverType;
            _region = region;
            _itemIndex = itemIndex;
            _columnIndex = columnIndex;
        }

        #endregion

        #region Public Properties

        /// <summary>The column index.</summary>
        public int ColumnIndex
        {
            get
            {
                return _columnIndex;
            }
        }

        /// <summary>The hover type.</summary>
        public ListViewHoverTypes HoverType
        {
            get
            {
                return _hoverType;
            }
        }

        /// <summary>The item index.</summary>
        public int ItemIndex
        {
            get
            {
                return _itemIndex;
            }
        }

        /// <summary>The region being hovered.</summary>
        public ListViewRegion Region
        {
            get
            {
                return _region;
            }
        }

        #endregion
    }
}