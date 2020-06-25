#region License

// -----------------------------------------------------------------------------------------------------------
//
// Name: VisualListViewActionList.cs
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
using System.Windows.Forms;

using VisualPlus.Collections.CollectionsBase;
using VisualPlus.Toolkit.Controls.DataManagement;

#endregion Namespace

namespace VisualPlus.ActionList
{
    internal class VisualListViewActionList : DesignerActionList
    {
        #region Fields

        private VisualListView _control;
        private DesignerActionUIService _designerService;
        private bool _dockState;
        private string _dockText;

        #endregion Fields

        #region Constructors and Destructors

        public VisualListViewActionList(IComponent component) : base(component)
        {
            _control = (VisualListView)component;
            _designerService = (DesignerActionUIService)GetService(typeof(DesignerActionUIService));
            _dockState = false;
            _dockText = ContainerText.Dock;
        }

        #endregion Constructors and Destructors

        #region Public Properties

        // FIX: Editor is causing drop-down error. Removing it prevents the columns from being filled with default data.
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]

        // [Editor(typeof(VisualListViewColumnCollectionEditor), typeof(UITypeEditor))]
        [Localizable(true)]
        [MergableProperty(false)]
        public VisualListViewColumnCollection Columns
        {
            get
            {
                return _control.Columns;
            }
        }

        // FIX: Editor is causing drop-down error. Removing it prevents the columns from being filled with default data.
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]

        // [Editor(typeof(VisualListViewItemCollectionEditor), typeof(UITypeEditor))]
        [Localizable(true)]
        [MergableProperty(false)]
        public VisualListViewItemCollection Items
        {
            get
            {
                return _control.Items;
            }
        }

        #endregion Public Properties

        #region Public Methods and Operators

        /// <summary>Dock the container.</summary>
        public void DockContainer()
        {
            if (!_dockState)
            {
                _control.Dock = DockStyle.None;
                _dockText = ContainerText.Dock;
                _dockState = true;
            }
            else
            {
                _control.Dock = DockStyle.Fill;
                _dockText = ContainerText.Undock;
                _dockState = false;
            }

            _designerService.Refresh(_control);
        }

        public override DesignerActionItemCollection GetSortedActionItems()
        {
            DesignerActionItemCollection items = new DesignerActionItemCollection
                {
                    new DesignerActionPropertyItem("Columns", "Edit Columns..."),
                    new DesignerActionPropertyItem("Items", "Edit Items..."),

                    // new DesignerActionPropertyItem("Groups", "Edit Groups..."),
                    new DesignerActionMethodItem(this, "DockContainer", _dockText)
                };

            return items;
        }

        #endregion Public Methods and Operators

        private struct ContainerText
        {
            #region Constants

            public const string Dock = "Dock in Parent Container.";
            public const string Undock = "Undock in Parent Container.";

            #endregion Constants
        }
    }
}