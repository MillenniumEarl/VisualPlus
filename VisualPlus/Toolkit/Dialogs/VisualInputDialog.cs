#region License

// -----------------------------------------------------------------------------------------------------------
// 
// Name: VisualInputDialog.cs
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

using VisualPlus.Toolkit.VisualBase;

#endregion

namespace VisualPlus.Toolkit.Dialogs
{
    /// <summary>The <see cref="VisualInputDialog" />.</summary>
    [ClassInterface(ClassInterfaceType.AutoDispatch)]
    [ComVisible(true)]
    [DefaultEvent("Load")]
    [DefaultProperty("Text")]
    [Description("The Visual Input Dialog")]
    [DesignerCategory("Form")]
    [DesignTimeVisible(false)]
    [ToolboxItem(false)]
    public partial class VisualInputDialog : VisualDialog
    {
        #region Constructors and Destructors

        /// <summary>Initializes a new instance of the <see cref="VisualInputDialog" /> class.</summary>
        /// <param name="caption">The caption.</param>
        public VisualInputDialog(string caption) : this()
        {
            Text = caption;
        }

        /// <summary>Initializes a new instance of the <see cref="VisualInputDialog" /> class.</summary>
        /// <param name="caption">The caption.</param>
        /// <param name="watermark">The watermark.</param>
        public VisualInputDialog(string caption, string watermark) : this()
        {
            Text = caption;
            tbInput.Watermark.Text = watermark;
        }

        /// <summary>Initializes a new instance of the <see cref="VisualInputDialog" /> class.</summary>
        /// <param name="text">The caption text.</param>
        /// <param name="caption">The caption.</param>
        /// <param name="watermark">The watermark.</param>
        public VisualInputDialog(string text, string caption, string watermark) : this()
        {
            Text = caption;
            tbInput.Text = text;
            tbInput.Watermark.Text = watermark;
        }

        /// <summary>Initializes a new instance of the <see cref="VisualInputDialog" /> class.</summary>
        public VisualInputDialog()
        {
            InitializeComponent();
        }

        #endregion

        #region Public Properties

        /// <summary>Contains the input result.</summary>
        [Browsable(false)]
        public string InputResult
        {
            get
            {
                return tbInput.Text;
            }
        }

        #endregion

        #region Methods

        /// <summary>The input text changed.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event args.</param>
        private void Input_TextChanged(object sender, EventArgs e)
        {
            btnOK.Enabled = tbInput.TextLength > 0;
        }

        #endregion
    }
}