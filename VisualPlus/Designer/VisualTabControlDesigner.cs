#region License

// -----------------------------------------------------------------------------------------------------------
// 
// Name: VisualTabControlDesigner.cs
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

#endregion

#region Namespace

using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Windows.Forms.Design;

using VisualPlus.Events;
using VisualPlus.Toolkit.Child;
using VisualPlus.Toolkit.Controls.Navigation;

#endregion

namespace VisualPlus.Designer
{
    internal class VisualTabControlDesigner : ParentControlDesigner
    {
        #region Fields

        private IDesignerHost _designerHost;
        private DesignerVerbCollection _designerVerbCollection;
        private ISelectionService _selectionService;

        #endregion

        #region Constructors and Destructors

        public VisualTabControlDesigner()
        {
            DesignerVerb verb1 = new DesignerVerb("Add Tab", OnAddPage);
            DesignerVerb verb2 = new DesignerVerb("Insert Tab", OnInsertPage);
            DesignerVerb verb3 = new DesignerVerb("Remove Tab", OnRemovePage);

            _designerVerbCollection = new DesignerVerbCollection();
            _designerVerbCollection.AddRange(new[] { verb1, verb2, verb3 });
        }

        #endregion

        #region Delegates

        public delegate void VisualTabControlEventHandler(object sender, VisualTabControlEventArgs e);

        #endregion

        #region Enums

        private enum TabControlHitTest
        {
            TCHT_NOWHERE = 1,
            TCHT_ONITEMICON = 2,
            TCHT_ONITEMLABEL = 4,
            TCHT_ONITEM = TCHT_ONITEMICON | TCHT_ONITEMLABEL
        }

        #endregion

        #region Public Properties

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

        /// <summary>Fix: the AllSizable selection rule on DockStyle.Fill</summary>
        public override SelectionRules SelectionRules
        {
            get
            {
                if (Control.Dock == DockStyle.Fill)
                {
                    return SelectionRules.Visible;
                }

                return base.SelectionRules;
            }
        }

        public ISelectionService SelectionService
        {
            get
            {
                if (_selectionService == null)
                {
                    _selectionService = (ISelectionService)GetService(typeof(ISelectionService));
                }

                return _selectionService;
            }
        }

        public override DesignerVerbCollection Verbs
        {
            get
            {
                if (_designerVerbCollection.Count != 3)
                {
                    return _designerVerbCollection;
                }

                VisualTabControl _tabControl = (VisualTabControl)Control;
                if (_tabControl.TabCount > 0)
                {
                    _designerVerbCollection[1].Enabled = true;
                    _designerVerbCollection[2].Enabled = true;
                }
                else
                {
                    _designerVerbCollection[1].Enabled = false;
                    _designerVerbCollection[2].Enabled = false;
                }

                return _designerVerbCollection;
            }
        }

        #endregion

        #region Public Methods and Operators

        public override void InitializeNewComponent(IDictionary defaultValues)
        {
            base.InitializeNewComponent(defaultValues);

            VisualTabPage _tabPage1 = (VisualTabPage)DesignerHost.CreateComponent(typeof(VisualTabPage));
            VisualTabPage _tabPage2 = (VisualTabPage)DesignerHost.CreateComponent(typeof(VisualTabPage));

            _tabPage1.Text = _tabPage1.Name;
            _tabPage2.Text = _tabPage2.Name;

            Control.Controls.Add(_tabPage1);
            Control.Controls.Add(_tabPage2);
        }

        #endregion

        #region Methods

        protected override bool GetHitTest(Point point)
        {
            if (SelectionService.PrimarySelection == Control)
            {
                TCHITTESTINFO _tabControlHitTestInfo = new TCHITTESTINFO { Point = Control.PointToClient(point), Flags = 0 };

                Message _message = new Message { HWnd = Control.Handle, Msg = 0x130D };

                IntPtr _longIntParameter = Marshal.AllocHGlobal(Marshal.SizeOf(_tabControlHitTestInfo));
                Marshal.StructureToPtr(_tabControlHitTestInfo, _longIntParameter, false);
                _message.LParam = _longIntParameter;

                base.WndProc(ref _message);
                Marshal.FreeHGlobal(_longIntParameter);

                if (_message.Result.ToInt32() != -1)
                {
                    return _tabControlHitTestInfo.Flags != TabControlHitTest.TCHT_NOWHERE;
                }
            }

            return false;
        }

        protected override void OnPaintAdornments(PaintEventArgs paintEventArgs)
        {
            // Don't want DrawGrid dots.
        }

        protected override void PreFilterProperties(IDictionary properties)
        {
            properties.Remove("AutoEllipsis");
            properties.Remove("FlatAppearance");
            properties.Remove("FlatStyle");
            properties.Remove("ImeMode");
            properties.Remove("Padding");
            properties.Remove("UseCompatibleTextRendering");

            base.PreFilterProperties(properties);
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            if (m.Msg == 0x84)
            {
                // select tab control when Tab control clicked outside of TabItem.
                if (m.Result.ToInt32() == -1)
                {
                    m.Result = (IntPtr)1;
                }
            }
        }

        private void OnAddPage(object sender, EventArgs e)
        {
            VisualTabControl _parentalControl = (VisualTabControl)Control;
            Control.ControlCollection _controlCollection = _parentalControl.Controls;

            RaiseComponentChanging(TypeDescriptor.GetProperties(_parentalControl)["TabPages"]);

            VisualTabPage _tabPage = (VisualTabPage)DesignerHost.CreateComponent(typeof(VisualTabPage));
            _tabPage.Text = _tabPage.Name;
            _parentalControl.TabPages.Add(_tabPage);

            RaiseComponentChanged(TypeDescriptor.GetProperties(_parentalControl)["TabPages"], _controlCollection, _parentalControl.TabPages);
            _parentalControl.SelectedTab = _tabPage;

            SetVerbs();
        }

        private void OnInsertPage(object sender, EventArgs e)
        {
            VisualTabControl _parentalControl = (VisualTabControl)Control;
            Control.ControlCollection _controlCollection = _parentalControl.Controls;
            int _index = _parentalControl.SelectedIndex;

            RaiseComponentChanging(TypeDescriptor.GetProperties(_parentalControl)["TabPages"]);

            VisualTabPage _tabPage = (VisualTabPage)DesignerHost.CreateComponent(typeof(VisualTabPage));
            _tabPage.Text = _tabPage.Name;

            var _tabPageCollection = new VisualTabPage[_parentalControl.TabCount];

            // Starting at our Insert Position, store and remove all the tab pages.
            for (int i = _index; i <= _tabPageCollection.Length - 1; i++)
            {
                _tabPageCollection[i] = (VisualTabPage)_parentalControl.TabPages[_index];
                _parentalControl.TabPages.Remove(_parentalControl.TabPages[_index]);
            }

            // add the tab page to be inserted.
            _parentalControl.TabPages.Add(_tabPage);

            // then re-add the original tab pages.
            for (int i = _index; i <= _tabPageCollection.Length - 1; i++)
            {
                _parentalControl.TabPages.Add(_tabPageCollection[i]);
            }

            RaiseComponentChanged(TypeDescriptor.GetProperties(_parentalControl)["TabPages"], _controlCollection, _parentalControl.TabPages);
            _parentalControl.SelectedTab = _tabPage;

            SetVerbs();
        }

        private void OnRemovePage(object sender, EventArgs e)
        {
            VisualTabControl _parentalControl = (VisualTabControl)Control;
            Control.ControlCollection _controlCollection = _parentalControl.Controls;

            if (_parentalControl.SelectedIndex < 0)
            {
                return;
            }

            RaiseComponentChanging(TypeDescriptor.GetProperties(_parentalControl)["TabPages"]);

            DesignerHost.DestroyComponent(_parentalControl.TabPages[_parentalControl.SelectedIndex]);

            RaiseComponentChanged(TypeDescriptor.GetProperties(_parentalControl)["TabPages"], _controlCollection, _parentalControl.TabPages);

            SelectionService.SetSelectedComponents(new IComponent[] { _parentalControl }, SelectionTypes.Auto);

            SetVerbs();
        }

        private void SetVerbs()
        {
            VisualTabControl _parentalControl = (VisualTabControl)Control;

            switch (_parentalControl.TabPages.Count)
            {
                case 0:
                    {
                        Verbs[1].Enabled = false;
                        Verbs[2].Enabled = false;
                        break;
                    }

                case 1:
                    {
                        Verbs[1].Enabled = false;
                        Verbs[2].Enabled = true;
                        break;
                    }

                default:
                    {
                        Verbs[1].Enabled = true;
                        Verbs[2].Enabled = true;
                        break;
                    }
            }
        }

        #endregion

        private struct TCHITTESTINFO
        {
            #region Fields

            public TabControlHitTest Flags;
            public Point Point;

            #endregion
        }
    }
}