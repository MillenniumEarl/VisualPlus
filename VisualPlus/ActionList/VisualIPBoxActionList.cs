#region License

// -----------------------------------------------------------------------------------------------------------
// 
// Name: VisualIPBoxActionList.cs
// VisualPlus - The VisualPlus Framework (VPF) for WinForms .NET development.
// 
// Created: 12/12/2018 - 8:03 PM
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

using System.ComponentModel;
using System.ComponentModel.Design;

using VisualPlus.Toolkit.Controls.Editors;

#endregion

namespace VisualPlus.ActionList
{
    internal class VisualIPBoxActionList : DesignerActionList
    {
        #region Fields

        private VisualIPBox _control;
        private DesignerActionUIService _designerService;

        #endregion

        #region Constructors and Destructors

        public VisualIPBoxActionList(IComponent component) : base(component)
        {
            _control = (VisualIPBox)component;
            _designerService = (DesignerActionUIService)GetService(typeof(DesignerActionUIService));
        }

        #endregion

        // [Category("Behaviour")]
        // [Description("Gets or sets a value indicating whether this is a multiline TextBox control.")]
        // [DefaultValue(false)]
        // public virtual bool MultiLine
        // {
        // get
        // {
        // return _control.MultiLine;
        // }

        // set
        // {
        // _control.MultiLine = value;
        // _control.Invalidate();
        // }
        // }

        // [Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        // [Localizable(false)]
        // public virtual string Text
        // {
        // get
        // {
        // return _control.Text;
        // }

        // set
        // {
        // _control.Text = value;
        // }
        // }

        // public override DesignerActionItemCollection GetSortedActionItems()
        // {
        // DesignerActionItemCollection items = new DesignerActionItemCollection
        // {
        // new DesignerActionPropertyItem("MultiLine", "MultiLine"),
        // new DesignerActionPropertyItem("Text", "Edit Text:")
        // };

        // return items;
        // }
    }
}