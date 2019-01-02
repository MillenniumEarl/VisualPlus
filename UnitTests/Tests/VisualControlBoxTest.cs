#region License

// -----------------------------------------------------------------------------------------------------------
// 
// Name: VisualControlBoxTest.cs
// UnitTests - The VisualPlus Framework (VPF) for WinForms .NET development.
// 
// Created: 10/12/2018 - 11:45 PM
// Last Modified: 02/01/2019 - 1:28 AM
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

using VisualPlus.Events;
using VisualPlus.Toolkit.Dialogs;

#endregion

namespace UnitTests.Tests
{
    /// <summary>The form test.</summary>
    public partial class VisualControlBoxTest : VisualForm
    {
        #region Constructors and Destructors

        public VisualControlBoxTest()
        {
            InitializeComponent();
        }

        #endregion

        #region Methods

        private void TClose_ToggleChanged(ToggleEventArgs e)
        {
            // CloseBox = e.State;
            ControlBox.CloseButton.Visible = e.State;
        }

        private void TControlBox_ToggleChanged(ToggleEventArgs e)
        {
            ControlBox.Visible = e.State;
        }

        private void THelp_ToggleChanged(ToggleEventArgs e)
        {
            HelpButton = e.State;
        }

        private void TMaximize_ToggleChanged_1(ToggleEventArgs e)
        {
            MaximizeBox = e.State;
        }

        private void TMinimize_ToggleChanged(ToggleEventArgs e)
        {
            MinimizeBox = e.State;
        }

        private void VisualControlBoxTest_Load(object sender, EventArgs e)
        {
        }

        #endregion
    }
}