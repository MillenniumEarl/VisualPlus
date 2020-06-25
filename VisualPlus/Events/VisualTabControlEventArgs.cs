#region License

// -----------------------------------------------------------------------------------------------------------
//
// Name: VisualTabControlEventArgs.cs
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
using System.Windows.Forms;

#endregion Namespace

namespace VisualPlus.Events
{
    public class VisualTabControlEventArgs : EventArgs
    {
        #region Fields

        public bool Cancel = false;

        private TabPage _tabPage;
        private int _tagPageIndex;

        #endregion Fields

        #region Constructors and Destructors

        public VisualTabControlEventArgs(TabPage tabPage, int tabPageIndex) : this()
        {
            _tabPage = tabPage;
            _tagPageIndex = tabPageIndex;
        }

        public VisualTabControlEventArgs()
        {
            _tagPageIndex = -1;
        }

        #endregion Constructors and Destructors

        #region Public Properties

        public TabPage TabPage
        {
            get
            {
                return _tabPage;
            }
        }

        public int TabPageIndex
        {
            get
            {
                return _tagPageIndex;
            }
        }

        #endregion Public Properties
    }
}