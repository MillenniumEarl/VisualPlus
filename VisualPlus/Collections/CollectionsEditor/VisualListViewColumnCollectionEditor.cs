#region License

// -----------------------------------------------------------------------------------------------------------
// 
// Name: VisualListViewColumnCollectionEditor.cs
// VisualPlus - The VisualPlus Framework (VPF) for WinForms .NET development.
// 
// Created: 10/12/2018 - 11:45 PM
// Last Modified: 02/01/2019 - 1:23 AM
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
using System.ComponentModel.Design;

using VisualPlus.Toolkit.Child;
using VisualPlus.Toolkit.Controls.DataManagement;

#endregion

namespace VisualPlus.Collections.CollectionsEditor
{
    internal class VisualListViewColumnCollectionEditor : CollectionEditor
    {
        #region Fields

        private int _uniqueID = 1;

        #endregion

        #region Constructors and Destructors

        /// <summary>Initializes a new instance of the <see cref="VisualListViewColumnCollectionEditor" /> class.</summary>
        /// <param name="type">The type.</param>
        public VisualListViewColumnCollectionEditor(Type type) : base(type)
        {
        }

        #endregion

        #region Public Methods and Operators

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider isp, object value)
        {
            VisualListView originalControl = (VisualListView)context.Instance;

            object returnObject = base.EditValue(context, isp, value);

            originalControl.Refresh();
            return returnObject;
        }

        #endregion

        #region Methods

        protected override Type CreateCollectionItemType()
        {
            return typeof(VisualListViewColumn);
        }

        protected override object CreateInstance(Type itemType)
        {
            object[] _columns;
            string _columnName;

            do
            {
                _columnName = itemType.Name + _uniqueID;
                _columns = GetItems(_columnName);
                _uniqueID++;
            }
            while (_columns.Length != 0);

            object _column = base.CreateInstance(itemType);

            ((VisualListViewColumn)_column).Name = _columnName;
            ((VisualListViewColumn)_column).Text = _columnName;

            return _column;
        }

        protected override Type[] CreateNewItemTypes()
        {
            return new[] { typeof(VisualListViewColumn) };
        }

        #endregion
    }
}