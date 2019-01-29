#region License

// -----------------------------------------------------------------------------------------------------------
// 
// Name: InterfaceTypes.cs
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

using System.Drawing;

using VisualPlus.Enumerators;
using VisualPlus.Models;
using VisualPlus.Toolkit.Child;
using VisualPlus.Toolkit.Components;
using VisualPlus.Toolkit.Controls.DataManagement;

#endregion

namespace VisualPlus.Interfaces
{
    /// <summary>The IListViewEmbeddedControl interface.</summary>
    public interface ILVEmbeddedControl
    {
        #region Public Properties

        VisualListViewItem Item { get; set; }

        VisualListView ListView { get; set; }

        VisualListViewSubItem SubItem { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>Populate the control with settings.</summary>
        /// <param name="item">The item.</param>
        /// <param name="subItem">The sub item.</param>
        /// <param name="listView">The list view.</param>
        /// <returns>The <see cref="bool" />.</returns>
        bool LVEmbeddedControlLoad(VisualListViewItem item, VisualListViewSubItem subItem, VisualListView listView);

        /// <summary>The return text string.</summary>
        /// <returns>The <see cref="string" />.</returns>
        string LVEmbeddedControlReturnText();

        /// <summary>Unload the control.</summary>
        void LVEmbeddedControlUnload();

        #endregion
    }

    /// <summary>The IThemeManager.</summary>
    public interface IThemeManager
    {
        #region Public Properties

        /// <summary>The style manager.</summary>
        StyleManager ThemeManager { get; set; }

        #endregion
    }

    /// <summary>The ITheme supported control.</summary>
    public interface IThemeSupport
    {
        #region Public Methods and Operators

        /// <summary>Update the control theme.</summary>
        /// <param name="theme">The theme to update with.</param>
        void UpdateTheme(Theme theme);

        #endregion
    }

    public interface IInputMethods
    {
        #region Public Methods and Operators

        void AppendText(string text);

        void Clear();

        void ClearUndo();

        void Copy();

        void Cut();

        void DeselectAll();

        int GetCharFromPosition(Point pt);

        int GetCharIndexFromPosition(Point pt);

        int GetFirstCharIndexFromLine(int lineNumber);

        int GetLineFromCharIndex(int index);

        Point GetPositionFromCharIndex(int index);

        void Paste();

        void ScrollToCaret();

        void Select(int start, int length);

        void SelectAll();

        void Undo();

        #endregion
    }

    public interface IAnimationSupport
    {
        #region Public Properties

        /// <summary>Gets or sets the animation state.</summary>
        bool Animation { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>Configures the animation settings.</summary>
        /// <param name="effectIncrement">The effect Increment.</param>
        /// <param name="effectType">The effect Type.</param>
        void ConfigureAnimation(double[] effectIncrement, EffectType[] effectType);

        /// <summary>Draws the animation on the graphics.</summary>
        /// <param name="graphics">The specified graphics to draw on.</param>
        void DrawAnimation(Graphics graphics);

        #endregion
    }

    /// <summary>The ITextColor.</summary>
    public interface ITextColor
    {
        #region Public Properties

        /// <summary>Gets or sets the Disabled state <see cref="Color" />.</summary>
        Color Disabled { get; set; }

        /// <summary>Gets or sets the Enabled state <see cref="Color" />.</summary>
        Color Enabled { get; set; }

        /// <summary>Gets or sets the Hover state <see cref="Color" />.</summary>
        Color Hover { get; set; }

        /// <summary>Gets or sets the Pressed state <see cref="Color" />.</summary>
        Color Pressed { get; set; }

        #endregion
    }
}