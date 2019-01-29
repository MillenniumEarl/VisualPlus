#region License

// -----------------------------------------------------------------------------------------------------------
// 
// Name: RadioButtonBase.cs
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
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows.Forms;

using VisualPlus.Events;
using VisualPlus.Toolkit.Controls.Interactivity;

#endregion

namespace VisualPlus.Toolkit.VisualBase
{
    [ToolboxItem(false)]
    [DesignerCategory("code")]
    [ClassInterface(ClassInterfaceType.AutoDispatch)]
    [ComVisible(true)]
    public abstract class RadioButtonBase : ToggleCheckmarkBase
    {
        #region Methods

        protected override void OnClick(EventArgs e)
        {
            if (!Checked)
            {
                Checked = true;
            }

            base.OnClick(e);
        }

        protected override void OnToggleChanged(ToggleEventArgs e)
        {
            base.OnToggleChanged(e);
            AutoUpdateOthers();
            Invalidate();
        }

        private void AutoUpdateOthers()
        {
            // Only un-check others if they are checked
            if (Checked)
            {
                Control parent = Parent;
                if (parent != null)
                {
                    // Search all sibling controls
                    foreach (Control control in parent.Controls)
                    {
                        // If another radio button found, that is not us
                        if ((control != this) && control is VisualRadioButton)
                        {
                            // Cast to correct type
                            VisualRadioButton radioButton = (VisualRadioButton)control;

                            // If target allows auto check changed and is currently checked
                            if (radioButton.Checked)
                            {
                                // Set back to not checked
                                radioButton.Checked = false;
                            }
                        }
                    }
                }
            }
        }

        #endregion
    }
}