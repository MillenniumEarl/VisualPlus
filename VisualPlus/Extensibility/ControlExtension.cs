#region License

// -----------------------------------------------------------------------------------------------------------
//
// Name: ControlExtension.cs
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

using System.Windows.Forms;

using VisualPlus.Utilities;

#endregion Namespace

namespace VisualPlus.Extensibility
{
    /// <summary>The collection of the <see cref="ControlExtension" /> class.</summary>
    public static class ControlExtension
    {
        #region Public Methods and Operators

        /// <summary>Centers the <see cref="Control" /> inside the parent <see cref="Control" />.</summary>
        /// <param name="control">The control to center.</param>
        /// <param name="centerX">Center X coordinate.</param>
        /// <param name="centerY">Center Y coordinate.</param>
        /// <returns>The <see cref="Control" />.</returns>
        public static Control ToCenter(this Control control, bool centerX, bool centerY)
        {
            ControlManager.CenterControl(control, control.Parent, centerX, centerY);
            return control;
        }

        /// <summary>Centers the <see cref="Control" /> inside the parent <see cref="Control" />.</summary>
        /// <param name="control">The control to center.</param>
        /// <returns>The <see cref="Control" />.</returns>
        public static Control ToCenter(this Control control)
        {
            ControlManager.CenterControl(control, control.Parent, true, true);
            return control;
        }

        #endregion Public Methods and Operators
    }
}