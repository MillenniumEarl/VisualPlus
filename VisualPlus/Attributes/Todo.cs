#region License

// -----------------------------------------------------------------------------------------------------------
//
// Name: Todo.cs
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

using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

using VisualPlus.Constants;
using VisualPlus.Utilities.Debugging;

#endregion Namespace

namespace VisualPlus.Attributes
{
    /// <summary>Represents the <see cref="Todo" /> class for attributes.</summary>
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    [ClassInterface(ClassInterfaceType.None)]
    [ComVisible(true)]
    [DebuggerDisplay(DefaultConstants.DefaultDebuggerDisplay)]
    [Serializable]
    public class Todo : VisualAttribute
    {
        #region Static Fields

        /// <summary>
        ///     Specifies the default value for the <see cref="Todo" />, that represents the current object. This
        ///     <see langword="static" /> field is read-only.
        /// </summary>
        public static readonly Todo Default = new Todo();

        #endregion Static Fields

        #region Constructors and Destructors

        /// <summary>Initializes a new instance of the <see cref="Todo" /> class.</summary>
        /// <param name="description">The description text.</param>
        public Todo(string description) : base(description)
        {
        }

        /// <summary>Initializes a new instance of the <see cref="Todo" /> class.</summary>
        public Todo()
        {
        }

        #endregion Constructors and Destructors

        #region Public Methods and Operators

        public override bool Equals(object obj)
        {
            if (obj == this)
            {
                return true;
            }

            switch (obj)
            {
                case Todo testAttribute:
                    {
                        bool equal;

                        // Validate the property's
                        if (testAttribute.Description.Equals(Description))
                        {
                            equal = true;
                        }
                        else
                        {
                            equal = false;
                        }

                        return equal;
                    }

                default:
                    {
                        return false;
                    }
            }
        }

        public override int GetHashCode()
        {
            return DescriptionValue.GetHashCode();
        }

        public override bool IsDefaultAttribute()
        {
            return Equals(Default);
        }

        public override string ToString()
        {
            if (Debugger.IsAttached)
            {
                return this.ToDebug("Description");
            }
            else
            {
                return base.ToString();
            }
        }

        #endregion Public Methods and Operators
    }
}