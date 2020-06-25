#region License

// -----------------------------------------------------------------------------------------------------------
//
// Name: VisualComboBoxActionList.cs
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

using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing.Design;
using System.Windows.Forms;

using VisualPlus.Toolkit.Controls.Interactivity;

#endregion Namespace

namespace VisualPlus.ActionList
{
    internal class VisualComboBoxActionList : DesignerActionList
    {
        #region Fields

        private VisualComboBox _control;
        private DesignerActionUIService _designerService;

        #endregion Fields

        #region Constructors and Destructors

        public VisualComboBoxActionList(IComponent component) : base(component)
        {
            _control = (VisualComboBox)component;
            _designerService = (DesignerActionUIService)GetService(typeof(DesignerActionUIService));
        }

        #endregion Constructors and Destructors

        #region Public Properties

        [Category("Data")]
        [Description("The items in the VisualComboBox.")]
        [Editor("System.Windows.Forms.Design.ListControlStringCollectionEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [MergableProperty(false)]
        [Localizable(true)]
        public virtual ComboBox.ObjectCollection Items
        {
            get
            {
                return _control.Items;
            }
        }

        #endregion Public Properties

        #region Public Methods and Operators

        public override DesignerActionItemCollection GetSortedActionItems()
        {
            DesignerActionItemCollection items = new DesignerActionItemCollection { new DesignerActionHeaderItem("Unbound Mode"), new DesignerActionPropertyItem("Items", "Edit Items...", "Unbound Mode") };

            return items;
        }

        #endregion Public Methods and Operators
    }
}