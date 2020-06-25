#region License

// -----------------------------------------------------------------------------------------------------------
//
// Name: DebuggerDisplayFormat.cs
//
// Copyright (c) 2019 - 2019 VisualPlus <https://darkbyte7.github.io/VisualPlus/>
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
using VisualPlus.Localization;

#endregion Namespace

namespace VisualPlus.Utilities.Debugging
{
    /// <summary>Represents the <see cref="LogFormat" /> class.</summary>
    [ComVisible(true)]
    [DebuggerDisplay(DefaultConstants.DefaultDebuggerDisplay)]
    [Serializable]
    public class LogFormat : IComparable
    {
        #region Fields

        private string groupSpacingSeparator;
        private string objectValueSeparator;
        private string prefix;
        private string suffix;

        #endregion Fields

        #region Constructors and Destructors

        /// <summary>Initializes a new instance of the <see cref="LogFormat" /> class.</summary>
        public LogFormat()
        {
            groupSpacingSeparator = DebuggerText.GroupSpacingSeparatorFormat;
            objectValueSeparator = DebuggerText.ObjectValueSeparatorFormat;
            prefix = DebuggerText.PrefixFormat;
            suffix = DebuggerText.SuffixFormat;
        }

        /// <summary>Initializes a new instance of the <see cref="LogFormat" /> class.</summary>
        /// <param name="prefix">The prefix.</param>
        /// <param name="suffix">The suffix.</param>
        public LogFormat(string prefix, string suffix) : this(DebuggerText.ObjectValueSeparatorFormat, DebuggerText.GroupSpacingSeparatorFormat, prefix, suffix)
        {
        }

        /// <summary>Initializes a new instance of the <see cref="LogFormat" /> class.</summary>
        /// <param name="objectValueSeparator">The name Value Separator.</param>
        /// <param name="groupSpacingSeparator">The group Spacing Separator.</param>
        /// <param name="prefix">The prefix.</param>
        /// <param name="suffix">The suffix.</param>
        public LogFormat(string objectValueSeparator, string groupSpacingSeparator, string prefix, string suffix) : this()
        {
            this.objectValueSeparator = objectValueSeparator;
            this.groupSpacingSeparator = groupSpacingSeparator;
            this.prefix = prefix;
            this.suffix = suffix;
        }

        #endregion Constructors and Destructors

        #region Public Properties

        /// <summary>
        ///     Specifies the default value for the <see cref="LogFormat" />, that represents the current object. This
        ///     <see langword="static" /> field is read-only.
        /// </summary>
        public static LogFormat Default
        {
            get
            {
                return new LogFormat();
            }
        }

        /// <summary>
        ///     Gets the empty value for the <see cref="LogFormat" />, that represents the current object. This
        ///     <see langword="static" /> field is read-only.
        /// </summary>
        public static LogFormat Empty
        {
            get
            {
                return new LogFormat(string.Empty, string.Empty, string.Empty, string.Empty);
            }
        }

        /// <summary>Gets or sets the group spacing formatting.</summary>
        /// <returns>The <see cref="string" />.</returns>
        public virtual string GroupSpacingSeparator
        {
            get
            {
                return groupSpacingSeparator;
            }

            set
            {
                groupSpacingSeparator = value;
            }
        }

        /// <summary>Gets or sets the object value formatting.</summary>
        /// <returns>The <see cref="string" />.</returns>
        public string ObjectValueSeparator
        {
            get
            {
                return objectValueSeparator;
            }

            set
            {
                objectValueSeparator = value;
            }
        }

        /// <summary>Gets or sets the prefix formatting.</summary>
        /// <returns>The <see cref="string" />.</returns>
        public string Prefix
        {
            get
            {
                return prefix;
            }

            set
            {
                prefix = value;
            }
        }

        /// <summary>Gets or sets the suffix formatting.</summary>
        /// <returns>The <see cref="string" />.</returns>
        public string Suffix
        {
            get
            {
                return suffix;
            }

            set
            {
                suffix = value;
            }
        }

        #endregion Public Properties

        #region Public Methods and Operators

        public int CompareTo(object obj)
        {
            if (obj == null)
            {
                return -1;
            }

            if (Equals(obj))
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == this)
            {
                return true;
            }

            switch (obj)
            {
                case LogFormat debuggerDisplayFormat:
                    {
                        bool equal;

                        // Validate the property's
                        if ((debuggerDisplayFormat.GroupSpacingSeparator == GroupSpacingSeparator) &&
                            (debuggerDisplayFormat.ObjectValueSeparator == ObjectValueSeparator) &&
                            (debuggerDisplayFormat.Prefix == Prefix) &&
                            (debuggerDisplayFormat.Suffix == Suffix))
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
            return GetType().GetHashCode();
        }

        #endregion Public Methods and Operators
    }
}