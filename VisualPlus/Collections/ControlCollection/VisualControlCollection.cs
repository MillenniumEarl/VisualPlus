#region License

// -----------------------------------------------------------------------------------------------------------
//
// Name: VisualControlCollection.cs
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
using System.Windows.Forms;

#endregion Namespace

namespace VisualPlus.Collections.ControlCollection
{
    /// <summary>The base class for specific control collections.</summary>
    public class VisualControlCollection : Control.ControlCollection
    {
        #region Constructors and Destructors

        /// <summary>Initializes a new instance of the <see cref="VisualControlCollection" /> class.</summary>
        /// <param name="owner">The owner.</param>
        public VisualControlCollection(Control owner) : base(owner)
        {
        }

        #endregion Constructors and Destructors

        #region Public Methods and Operators

        /// <summary>Add a control to the collection.</summary>
        /// <param name="control">The control.</param>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void AddInternal(Control control)
        {
            Add(control);
        }

        /// <summary>Clear the collection.</summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void ClearInternal()
        {
            for (int i = Count - 1; i >= 0; i--)
            {
                RemoveInternal(this[i]);
            }
        }

        /// <summary>Remove a control from the collection.</summary>
        /// <param name="control">The control.</param>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void RemoveInternal(Control control)
        {
            Remove(control);
        }

        #endregion Public Methods and Operators
    }
}