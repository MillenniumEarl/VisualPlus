﻿#region License

// -----------------------------------------------------------------------------------------------------------
//
// Name: VisualInputField.cs
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

using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

using VisualPlus.Constants;
using VisualPlus.Delegates;
using VisualPlus.Designer;
using VisualPlus.Events;
using VisualPlus.Localization;

#endregion Namespace

namespace VisualPlus.Toolkit.VisualBase
{
    /// <summary>The <see cref="VisualInputField" /> control.</summary>
    [ClassInterface(ClassInterfaceType.AutoDispatch)]
    [ComVisible(true)]
    [DefaultEvent("TextChanged")]
    [DefaultProperty("Text")]
    [Description("The Visual Input Field")]
    [Designer(typeof(VisualInputFieldDesigner))]
    [ToolboxBitmap(typeof(VisualInputField), "VisualInputField.bmp")]
    [ToolboxItem(false)]
    public class VisualInputField : TextBox
    {
        #region Fields

        private bool alphaNumericToggle;

        #endregion Fields

        #region Constructors and Destructors

        /// <summary>Initializes a new instance of the <see cref="VisualInputField" /> class.</summary>
        public VisualInputField()
        {
            alphaNumericToggle = false;
        }

        #endregion Constructors and Destructors

        #region Public Events

        public event ClipboardEventHandler ClipboardCopy;

        public event ClipboardEventHandler ClipboardCut;

        public event ClipboardEventHandler ClipboardPaste;

        #endregion Public Events

        #region Public Properties

        /// <summary>Gets or sets the alpha numeric toggle, specifying whether to only accept numbers input.</summary>
        [Description(PropertyDescription.Toggle)]
        [Category(PropertyCategory.Behavior)]
        public bool AlphaNumeric
        {
            get
            {
                return alphaNumericToggle;
            }

            set
            {
                alphaNumericToggle = value;
            }
        }

        #endregion Public Properties

        #region Methods

        /// <summary>Occurs when the clipboard copy event fires.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event args.</param>
        protected virtual void OnClipboardCopy(object sender, ClipboardEventArgs e)
        {
            ClipboardCopy?.Invoke(sender, e);
        }

        /// <summary>Occurs when the clipboard cut event fires.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event args.</param>
        protected virtual void OnClipboardCut(object sender, ClipboardEventArgs e)
        {
            ClipboardCut?.Invoke(sender, e);
        }

        /// <summary>Occurs when the clipboard paste event fires.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event args.</param>
        protected virtual void OnClipboardPaste(object sender, ClipboardEventArgs e)
        {
            ClipboardPaste?.Invoke(sender, e);
        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            // Determine if numeric only input
            if (alphaNumericToggle)
            {
                const char DECIMAL_POINT = '.';

                // const char NEGATIVE_VALUE = '-'; // TODO: Check for '-' to allow negative values

                // Check key char input
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != DECIMAL_POINT))
                {
                    e.Handled = true;
                }

                // Allow one decimal point only - allow more in future commits
                if ((e.KeyChar == DECIMAL_POINT) && (Text.IndexOf(DECIMAL_POINT) > -1))
                {
                    e.Handled = true;
                }
            }

            base.OnKeyPress(e);
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == ClipboardConstants.WM_CUT)
            {
                OnClipboardCut(this, new ClipboardEventArgs(Clipboard.GetText()));
            }
            else if (m.Msg == ClipboardConstants.WM_COPY)
            {
                OnClipboardCopy(this, new ClipboardEventArgs(Clipboard.GetText()));
            }
            else if (m.Msg == ClipboardConstants.WM_PASTE)
            {
                OnClipboardPaste(this, new ClipboardEventArgs(Clipboard.GetText()));
            }

            base.WndProc(ref m);
        }

        #endregion Methods
    }
}