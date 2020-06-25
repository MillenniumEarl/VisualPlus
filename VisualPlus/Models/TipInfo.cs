#region License

// -----------------------------------------------------------------------------------------------------------
//
// Name: TipInfo.cs
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

using System.Drawing;
using System.Windows.Forms;

using VisualPlus.Toolkit.Components;

#endregion Namespace

namespace VisualPlus.Models
{
    public class TipInfo
    {
        #region Constructors and Destructors

        /// <summary>Initializes a new instance of the <see cref="TipInfo" /> class.</summary>
        public TipInfo()
        {
            Caption = string.Empty;
            Control = null;
            Icon = ToolTipIcon.None;
            Image = null;
            Position = new Point();
            Text = string.Empty;
            Type = ToolTipType.Default;
        }

        #endregion Constructors and Destructors

        #region Enums

        public enum ToolTipType
        {
            /// <summary>The default.</summary>
            Default = 0,

            /// <summary>The image.</summary>
            Image = 1,

            /// <summary>The text.</summary>
            Text = 2
        }

        #endregion Enums

        #region Public Properties

        /// <summary>Gets or sets the <see cref="VisualToolTip" /> title to display when the pointer is on the control.</summary>
        public string Caption { get; set; }

        /// <summary>Gets or sets the <see cref="VisualToolTip" /> control.</summary>
        public Control Control { get; set; }

        /// <summary>
        ///     Gets or sets a value that defines the type of icon to be displayed along side <see cref="VisualToolTip" />
        ///     Text.
        /// </summary>
        public ToolTipIcon Icon { get; set; }

        /// <summary>
        ///     Gets or sets a value that defines the type of image to be displayed along side <see cref="VisualToolTip" />
        ///     Text.
        /// </summary>
        public Image Image { get; set; }

        /// <summary>Gets or sets the <see cref="VisualToolTip" /> position.</summary>
        public Point Position { get; set; }

        /// <summary>Gets or sets the <see cref="VisualToolTip" /> size.</summary>
        public Size Size { get; set; }

        /// <summary>Gets or sets the <see cref="VisualToolTip" /> text content to display when the pointer is on the control.</summary>
        public string Text { get; set; }

        /// <summary>Gets or sets a value that defines the type to be displayed.</summary>
        public ToolTipType Type { get; set; }

        #endregion Public Properties
    }
}