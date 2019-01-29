#region License

// -----------------------------------------------------------------------------------------------------------
// 
// Name: VisualDialog.cs
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

using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using VisualPlus.Enumerators;
using VisualPlus.Toolkit.Dialogs;

#endregion

namespace VisualPlus.Toolkit.VisualBase
{
    public class VisualDialog : VisualForm
    {
        #region Constructors and Destructors

        /// <summary>Initializes a new instance of the <see cref="VisualDialog" /> class.</summary>
        /// <param name="text">The caption text.</param>
        public VisualDialog(string text) : this()
        {
            Text = text;
        }

        /// <summary>Initializes a new instance of the <see cref="VisualDialog" /> class.</summary>
        public VisualDialog()
        {
            BackColor = Color.White;
            ButtonSize = new Size(75, 23);
            HelpButton = false;
            MinimizeBox = false;
            MaximizeBox = false;
            Sizable = false;
            ShowIcon = false;
            ShowInTaskbar = false;
            TitleAlignment = Alignment.TextAlignment.Left;
            StartPosition = FormStartPosition.CenterParent;
        }

        #endregion

        #region Public Properties

        /// <summary>Gets or sets the button size.</summary>
        [Browsable(false)]
        public Size ButtonSize { get; set; }

        #endregion
    }
}