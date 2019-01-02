#region License

// -----------------------------------------------------------------------------------------------------------
// 
// Name: ManagedHScrollBar.cs
// VisualPlus - The VisualPlus Framework (VPF) for WinForms .NET development.
// 
// Created: 10/12/2018 - 11:45 PM
// Last Modified: 02/01/2019 - 12:55 AM
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
using System.Diagnostics;
using System.Windows.Forms;

#endregion

namespace VisualPlus.Toolkit.VisualBase
{
    [ToolboxItem(false)]
    public class ManagedHScrollBar : HScrollBar
    {
        #region Constructors and Destructors

        public ManagedHScrollBar()
        {
            InitializeComponent();
        }

        #endregion

        #region Public Properties

        public int MHeight
        {
            get
            {
                if (Visible != true)
                {
                    return 0;
                }
                else
                {
                    return Height;
                }
            }

            set
            {
                if (Height != value)
                {
                    Height = value;
                }
            }
        }

        public int MLargeChange
        {
            set
            {
                if (LargeChange != value)
                {
                    LargeChange = value;
                }
            }
        }

        public int MLeft
        {
            set
            {
                if (Left != value)
                {
                    Left = value;
                }
            }
        }

        public int MMaximum
        {
            set
            {
                if (Maximum != value)
                {
                    Maximum = value;
                }
            }
        }

        public int MSmallChange
        {
            set
            {
                if (SmallChange != value)
                {
                    SmallChange = value;
                }
            }
        }

        public int MTop
        {
            set
            {
                if (Top != value)
                {
                    Top = value;
                }
            }
        }

        public bool MVisible
        {
            set
            {
                if (Visible != value)
                {
                    Visible = value;
                }
            }
        }

        public int MWidth
        {
            get
            {
                if (Visible != true)
                {
                    return 0;
                }
                else
                {
                    return Width;
                }
            }

            set
            {
                if (Width != value)
                {
                    Width = value;
                }
            }
        }

        #endregion

        #region Public Methods and Operators

        public void ReflectFocus(object source, EventArgs e)
        {
            Debug.WriteLine("ManagedHScrollbar::Focus called");
            Parent.Focus();
        }

        #endregion

        #region Methods

        private void InitializeComponent()
        {
            TabStop = false;
            GotFocus += ReflectFocus;
        }

        #endregion
    }
}