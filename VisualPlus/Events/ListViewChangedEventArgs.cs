#region License

// -----------------------------------------------------------------------------------------------------------
//
// Name: ListViewChangedEventArgs.cs
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

using VisualPlus.Enumerators;
using VisualPlus.Toolkit.Child;

#endregion Namespace

namespace VisualPlus.Events
{
    public class ListViewChangedEventArgs : EventArgs
    {
        #region Fields

        private VisualListViewColumn _column;
        private VisualListViewItem _item;
        private ListViewChangedTypes _listViewChangedType;
        private VisualListViewSubItem _subItem;

        #endregion Fields

        #region Constructors and Destructors

        /// <summary>Initializes a new instance of the <see cref="ListViewChangedEventArgs" /> class.</summary>
        /// <param name="listViewChangedType">The list View Changed Type.</param>
        /// <param name="column">The column.</param>
        /// <param name="item">The item.</param>
        /// <param name="subItem">The sub Item.</param>
        public ListViewChangedEventArgs(ListViewChangedTypes listViewChangedType, VisualListViewColumn column, VisualListViewItem item, VisualListViewSubItem subItem)
        {
            _listViewChangedType = listViewChangedType;
            _column = column;
            _item = item;
            _subItem = subItem;
        }

        #endregion Constructors and Destructors

        #region Public Properties

        /// <summary>The type of change.</summary>
        public ListViewChangedTypes ChangedType
        {
            get
            {
                return _listViewChangedType;
            }
        }

        /// <summary>The column name.</summary>
        public VisualListViewColumn Column
        {
            get
            {
                return _column;
            }

            set
            {
                _column = value;
            }
        }

        /// <summary>The item name.</summary>
        public VisualListViewItem Item
        {
            get
            {
                return _item;
            }

            set
            {
                _item = value;
            }
        }

        /// <summary>The sub item name.</summary>
        public VisualListViewSubItem SubItem
        {
            get
            {
                return _subItem;
            }

            set
            {
                _subItem = value;
            }
        }

        #endregion Public Properties
    }
}