﻿#region License

// -----------------------------------------------------------------------------------------------------------
//
// Name: LVComboBox.cs
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
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms;

using VisualPlus.Interfaces;
using VisualPlus.Toolkit.Child;
using VisualPlus.Toolkit.Controls.DataManagement;

#endregion Namespace

namespace VisualPlus.Toolkit.EmbeddedControls
{
    [ToolboxItem(false)]
    public class LVComboBox : ComboBox, ILVEmbeddedControl
    {
        #region Fields

        private Container _components;
        private VisualListViewItem _item;
        private VisualListView _owner;
        private VisualListViewSubItem _subItem;

        #endregion Fields

        #region Constructors and Destructors

        /// <summary>Initializes a new instance of the <see cref="LVComboBox" /> class.</summary>
        public LVComboBox()
        {
            InitializeComponent();
        }

        #endregion Constructors and Destructors

        #region Public Properties

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

        public VisualListView ListView
        {
            get
            {
                return _owner;
            }

            set
            {
                _owner = value;
            }
        }

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

        #region Public Methods and Operators

        public bool LVEmbeddedControlLoad(VisualListViewItem item, VisualListViewSubItem subItem, VisualListView listView)
        {
            _item = item;
            _subItem = subItem;
            _owner = listView;

            Text = _subItem.Text;

            Items.Add("Item1");
            Items.Add("Item2");
            Items.Add("Item3");

            return true;
        }

        public string LVEmbeddedControlReturnText()
        {
            return Text;
        }

        public void LVEmbeddedControlUnload()
        {
            // take information from control and return it to the item
            _subItem.Text = Text;
        }

        #endregion Public Methods and Operators

        #region Methods

        /// <summary>Cleanup any resources being used.</summary>
        /// <param name="disposing">Indicates whether the method call comes from a <see cref="Dispose" /> method or a finalizer.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _components?.Dispose();
            }

            base.Dispose(disposing);
        }

        protected override void OnGotFocus(EventArgs e)
        {
            Debug.WriteLine("LVTextBox::Got Focus");
            base.OnGotFocus(e);
        }

        protected override void OnLostFocus(EventArgs e)
        {
            Debug.WriteLine("LVTextBox::Lost Focus");
            base.OnLostFocus(e);
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            // TODO: Add custom paint code here

            // Calling the base class OnPaint
            base.OnPaint(pe);
        }

        /// <summary>Required method for Designer support - do not modify the contents of this method with the code editor.</summary>
        private void InitializeComponent()
        {
            _components = new Container();
        }

        #endregion Methods
    }
}