#region License

// -----------------------------------------------------------------------------------------------------------
//
// Name: VisualListViewDesigner.cs
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

using System.Collections;
using System.ComponentModel.Design;
using System.Windows.Forms.Design;

using VisualPlus.ActionList;

#endregion Namespace

namespace VisualPlus.Designer
{
    internal class VisualListViewDesigner : ControlDesigner
    {
        #region Fields

        private DesignerActionListCollection _actionListCollection;
        private IDesignerHost _designerHost;

        #endregion Fields

        #region Public Properties

        /// <summary>Gets the design-time action lists supported by the component associated with the designer.</summary>
        public override DesignerActionListCollection ActionLists
        {
            get
            {
                if (_actionListCollection == null)
                {
                    _actionListCollection = new DesignerActionListCollection { new VisualListViewActionList(Component) };
                }

                return _actionListCollection;
            }
        }

        /// <summary>Provides an interface for managing designer transactions and components.</summary>
        public IDesignerHost DesignerHost
        {
            get
            {
                if (_designerHost == null)
                {
                    _designerHost = (IDesignerHost)GetService(typeof(IDesignerHost));
                }

                return _designerHost;
            }
        }

        #endregion Public Properties

        #region Methods

        protected override void PreFilterProperties(IDictionary properties)
        {
            properties.Remove("ImeMode");
            properties.Remove("Padding");
            properties.Remove("FlatAppearance");
            properties.Remove("FlatStyle");
            properties.Remove("AutoEllipsis");
            properties.Remove("UseCompatibleTextRendering");
            properties.Remove("Image");
            properties.Remove("ImageAlign");
            properties.Remove("ImageIndex");
            properties.Remove("ImageKey");
            properties.Remove("ImageList");
            properties.Remove("TextImageRelation");
            properties.Remove("BackgroundImageLayout");
            properties.Remove("UseVisualStyleBackColor");
            properties.Remove("RightToLeft");
            properties.Remove("View");

            base.PreFilterProperties(properties);
        }

        #endregion Methods
    }
}