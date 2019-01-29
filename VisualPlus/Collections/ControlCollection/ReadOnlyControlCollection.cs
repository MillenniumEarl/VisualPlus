#region License

// -----------------------------------------------------------------------------------------------------------
// 
// Name: ReadOnlyControlCollection.cs
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
using System.Windows.Forms;

#endregion

namespace VisualPlus.Collections.ControlCollection
{
    public class ReadOnlyControlCollection : VisualControlCollection
    {
        #region Fields

        private bool _allowRemove;

        #endregion

        #region Constructors and Destructors

        /// <summary>Initializes a new instance of the <see cref="ReadOnlyControlCollection" /> class.</summary>
        /// <param name="owner">The owner.</param>
        public ReadOnlyControlCollection(Control owner) : base(owner)
        {
            _allowRemove = false;
        }

        #endregion

        #region Public Properties

        /// <summary>Clear out all the entries in the collection.</summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool AllowRemoveInternal
        {
            get
            {
                return _allowRemove;
            }

            set
            {
                _allowRemove = value;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>Adds the specified control to the control collection.</summary>
        /// <param name="control">The control.</param>
        public override void Add(Control control)
        {
            if (AllowRemoveInternal)
            {
                base.Add(control);
            }
            else
            {
                throw new NotSupportedException("ReadOnly controls collection");
            }
        }

        /// <summary>Adds an array of control objects to the collection.</summary>
        /// <param name="controls">An array of Control objects to add to the collection.</param>
        public override void AddRange(Control[] controls)
        {
            if (AllowRemoveInternal)
            {
                base.AddRange(controls);
            }
            else
            {
                throw new NotSupportedException("ReadOnly controls collection");
            }
        }

        /// <summary>Removes all controls from the collection.</summary>
        public override void Clear()
        {
            if (AllowRemoveInternal)
            {
                base.Clear();
            }
            else
            {
                if (Count > 0)
                {
                    throw new NotSupportedException("ReadOnly controls collection");
                }
            }
        }

        /// <summary>Removes the specified control from the control collection.</summary>
        /// <param name="control">The control.</param>
        public override void Remove(Control control)
        {
            if (AllowRemoveInternal)
            {
                base.Remove(control);
            }
            else
            {
                if (Contains(control))
                {
                    throw new NotSupportedException("ReadOnly controls collection");
                }
            }
        }

        /// <summary>Removes the child control with the specified key.</summary>
        /// <param name="key">The name of the child control to remove.</param>
        public override void RemoveByKey(string key)
        {
            if (AllowRemoveInternal)
            {
                base.RemoveByKey(key);
            }
            else
            {
                if (ContainsKey(key))
                {
                    throw new NotSupportedException("ReadOnly controls collection");
                }
            }
        }

        #endregion
    }
}