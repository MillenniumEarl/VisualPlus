#region License

// -----------------------------------------------------------------------------------------------------------
// 
// Name: VisualAttribute.cs
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
using System.Diagnostics;
using System.Runtime.InteropServices;

using VisualPlus.Constants;
using VisualPlus.Localization;
using VisualPlus.Utilities.Debugging;

#endregion

namespace VisualPlus.Attributes
{
    /// <summary>Represents the <see cref="VisualAttribute" /> class for custom attributes.</summary>
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    [ClassInterface(ClassInterfaceType.None)]
    [ComVisible(true)]
    [DebuggerDisplay(DefaultConstants.DefaultDebuggerDisplay)]
    [Serializable]
    public abstract class VisualAttribute : Attribute
    {
        #region Fields

        private string description;
        private Type targetType;

        #endregion

        #region Constructors and Destructors

        /// <summary>Initializes a new instance of the <see cref="VisualAttribute" /> class with a description.</summary>
        /// <param name="target">The target <see cref="Type" />.</param>
        /// <param name="description">The description text.</param>
        protected VisualAttribute(Type target, string description) : this()
        {
            if (target != null)
            {
                targetType = target;
            }

            this.description = description;
        }

        /// <summary>Initializes a new instance of the <see cref="VisualAttribute" /> class with a description.</summary>
        /// <param name="description">The description text.</param>
        protected VisualAttribute(string description) : this(null, description)
        {
        }

        /// <summary>Initializes a new instance of the <see cref="VisualAttribute" /> class.</summary>
        protected VisualAttribute()
        {
            description = string.Empty;
            targetType = null;
        }

        /// <summary>Initializes a new instance of the <see cref="VisualAttribute" /> class with a target object.</summary>
        /// <param name="target">The target <see cref="object" />.</param>
        protected VisualAttribute(object target) : this(target.GetType())
        {
        }

        /// <summary>Initializes a new instance of the <see cref="VisualAttribute" /> class with a target type.</summary>
        /// <param name="target">The target <see cref="Type" />.</param>
        protected VisualAttribute(Type target) : this(target, string.Empty)
        {
        }

        #endregion

        #region Public Properties

        /// <summary>Gets the description value stored in this attribute.</summary>
        /// <returns>The <see cref="string" />.</returns>
        public virtual string DescriptionValue
        {
            get
            {
                return GetType().Name;
            }
        }

        #endregion

        #region Properties

        /// <summary>Gets or sets the <see cref="string" /> stored as the description.</summary>
        /// <returns>The <see cref="string" />.</returns>
        protected string Description
        {
            get
            {
                return description;
            }

            set
            {
                description = value;
            }
        }

        /// <summary>Gets the <see cref="Type" /> of the attribute's target.</summary>
        /// <returns>The attribute's target <see cref="Type" />.</returns>
        protected Type Target
        {
            get
            {
                return targetType;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value), ArgumentMessages.IsNull(Target));
                }

                targetType = value;
            }
        }

        /// <summary>Gets the target array of custom attributes applied to this member and applied to this <see cref="Type" />.</summary>
        /// <returns>The <see cref="object{T}" />.</returns>
        protected object[] TargetCustomAttributes
        {
            get
            {
                if (targetType != null)
                {
                    return targetType.GetCustomAttributes(typeof(VisualAttribute), true);
                }

                return new object[] { };
            }
        }

        /// <summary>Gets the target name stored in this attribute.</summary>
        /// <returns>The <see cref="string" />.</returns>
        protected string TargetName
        {
            get
            {
                string targetName;

                if (targetType != null)
                {
                    targetName = targetType.Name;
                }
                else
                {
                    targetName = string.Empty;
                }

                return targetName;
            }
        }

        #endregion

        #region Public Methods and Operators

        public override bool Equals(object obj)
        {
            if (obj == this)
            {
                return true;
            }

            switch (obj)
            {
                case VisualAttribute visualAttribute:
                    {
                        bool equal;

                        // Validate the property's
                        if ((visualAttribute.DescriptionValue == DescriptionValue) &&
                            (visualAttribute.Description == Description) &&
                            (visualAttribute.Target == Target))
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
            return false;
        }

        public override string ToString()
        {
            if (Debugger.IsAttached)
            {
                return this.ToDebug("DescriptionValue");
            }
            else
            {
                return base.ToString();
            }
        }

        #endregion
    }
}