#region License

// -----------------------------------------------------------------------------------------------------------
// 
// Name: IToolTip.cs
// VisualPlus - The VisualPlus Framework (VPF) for WinForms .NET development.
// 
// Created: 31/12/2018 - 10:04 PM
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

using System.Drawing;
using System.Windows.Forms;

using VisualPlus.Structure;
using VisualPlus.Toolkit.Components;

#endregion

namespace VisualPlus.Interfaces
{
    public interface IToolTip
    {
        #region Public Properties

        /// <summary>Gets or sets the <see cref="VisualToolTip" /> image.</summary>
        Image Image { get; set; }

        /// <summary>Gets or sets the <see cref="VisualToolTip" /> text.</summary>
        string Text { get; set; }

        /// <summary>Gets or sets the <see cref="VisualToolTip" /> info.</summary>
        TipInfo TipInfo { get; set; }

        /// <summary>Gets or sets the <see cref="VisualToolTip" /> type.</summary>
        TipInfo.ToolTipType TipType { get; set; }

        /// <summary>Gets or sets the <see cref="VisualToolTip" /> control.</summary>
        Control ToolTipControl { get; set; }

        /// <summary>Gets or sets the <see cref="VisualToolTip" /> title.</summary>
        string ToolTipTitle { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>Retrieves the <see cref="VisualToolTip" /> text associated with the specified control.</summary>
        /// <param name="control">The control for which to retrieve the <see cref="VisualToolTip" /> title.</param>
        /// <returns>The <see cref="string" />.</returns>
        string GetToolTip(Control control);

        /// <summary>Associates <see cref="VisualToolTip" /> text with the control.</summary>
        /// <param name="control">The control to associate <see cref="VisualToolTip" /> text with.</param>
        /// <param name="caption">The <see cref="VisualToolTip" /> text to display when the pointer is on the control.</param>
        void SetToolTip(Control control, string caption);

        /// <summary>
        ///     Sets the <see cref="VisualToolTip" /> text associated with the specified control, and displays the
        ///     <see cref="VisualToolTip" /> modally.
        /// </summary>
        /// <param name="text">A <see cref="string" /> containing the new <see cref="VisualToolTip" /> text.</param>
        /// <param name="window">The <see cref="Control" /> to display the <see cref="VisualToolTip" /> for.</param>
        void Show(string text, IWin32Window window);

        /// <summary>
        ///     Sets the <see cref="VisualToolTip" /> text associated with the specified control, and displays the
        ///     <see cref="VisualToolTip" /> modally.
        /// </summary>
        /// <param name="text">A <see cref="string" /> containing the new <see cref="VisualToolTip" /> text.</param>
        /// <param name="window">The <see cref="Control" /> to display the <see cref="VisualToolTip" /> for.</param>
        /// <param name="duration">
        ///     An <see cref="int" /> containing the duration, in milliseconds, to display the
        ///     <see cref="VisualToolTip" />.
        /// </param>
        void Show(string text, IWin32Window window, int duration);

        /// <summary>
        ///     Sets the <see cref="VisualToolTip" /> text associated with the specified control, and displays the
        ///     <see cref="VisualToolTip" /> modally.
        /// </summary>
        /// <param name="text">A <see cref="string" /> containing the new <see cref="VisualToolTip" /> text.</param>
        /// <param name="window">The <see cref="Control" /> to display the <see cref="VisualToolTip" /> for.</param>
        /// <param name="point">
        ///     A <see cref="Point" /> containing the offset, in pixels, relative to the upper-left corner of the
        ///     associated control window, to display the <see cref="VisualToolTip" />.
        /// </param>
        void Show(string text, IWin32Window window, Point point);

        /// <summary>
        ///     Sets the <see cref="VisualToolTip" /> text associated with the specified control, and displays the
        ///     <see cref="VisualToolTip" /> modally.
        /// </summary>
        /// <param name="text">A <see cref="string" /> containing the new <see cref="VisualToolTip" /> text.</param>
        /// <param name="window">The <see cref="Control" /> to display the <see cref="VisualToolTip" /> for.</param>
        /// <param name="point">
        ///     A <see cref="Point" /> containing the offset, in pixels, relative to the upper-left corner of the
        ///     associated control window, to display the <see cref="VisualToolTip" />.
        /// </param>
        /// <param name="duration">
        ///     An <see cref="int" /> containing the duration, in milliseconds, to display the
        ///     <see cref="VisualToolTip" />.
        /// </param>
        void Show(string text, IWin32Window window, Point point, int duration);

        /// <summary>
        ///     Sets the <see cref="VisualToolTip" /> text associated with the specified control, and displays the
        ///     <see cref="VisualToolTip" /> modally.
        /// </summary>
        /// <param name="text">A <see cref="string" /> containing the new <see cref="VisualToolTip" /> text.</param>
        /// <param name="window">The <see cref="Control" /> to display the <see cref="VisualToolTip" /> for.</param>
        /// <param name="x">
        ///     The horizontal offset, in pixels, relative to the upper-left corner of the associated control window,
        ///     to display the <see cref="VisualToolTip" />.
        /// </param>
        /// <param name="y">
        ///     The vertical offset, in pixels, relative to the upper-left corner of the associated control window, to
        ///     display the <see cref="VisualToolTip" />.
        /// </param>
        void Show(string text, IWin32Window window, int x, int y);

        /// <summary>
        ///     Sets the <see cref="VisualToolTip" /> text associated with the specified control, and displays the
        ///     <see cref="VisualToolTip" /> modally.
        /// </summary>
        /// <param name="text">A <see cref="string" /> containing the new <see cref="VisualToolTip" /> text.</param>
        /// <param name="window">The <see cref="Control" /> to display the <see cref="VisualToolTip" /> for.</param>
        /// <param name="x">
        ///     The horizontal offset, in pixels, relative to the upper-left corner of the associated control window,
        ///     to display the <see cref="VisualToolTip" />.
        /// </param>
        /// <param name="y">
        ///     The vertical offset, in pixels, relative to the upper-left corner of the associated control window, to
        ///     display the <see cref="VisualToolTip" />.
        /// </param>
        /// <param name="duration">
        ///     An <see cref="int" /> containing the duration, in milliseconds, to display the
        ///     <see cref="VisualToolTip" />.
        /// </param>
        void Show(string text, IWin32Window window, int x, int y, int duration);

        #endregion
    }
}