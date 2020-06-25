#region License

// -----------------------------------------------------------------------------------------------------------
//
// Name: Test.cs
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
using VisualPlus.Enumerators;
using VisualPlus.Utilities.Debugging;

#endregion Namespace

namespace VisualPlus.Attributes
{
    /// <summary>Represents the <see cref="Test" /> class for attributes.</summary>
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    [ClassInterface(ClassInterfaceType.None)]
    [ComVisible(true)]
    [DebuggerDisplay(DefaultConstants.DefaultDebuggerDisplay)]
    [Serializable]
    public class Test : VisualAttribute
    {
        #region Static Fields

        /// <summary>
        ///     Specifies the default value for the <see cref="Test" />, that represents the current object. This
        ///     <see langword="static" /> field is read-only.
        /// </summary>
        public static readonly Test Default = new Test();

        #endregion Static Fields

        #region Constructors and Destructors

        /// <summary>Initializes a new instance of the <see cref="Test" /> class.</summary>
        /// <param name="description">The description text.</param>
        /// <param name="author">The author.</param>
        public Test(string description, string author = "") : this(null, description, author, 0)
        {
        }

        /// <summary>Initializes a new instance of the <see cref="Test" /> class.</summary>
        public Test() : this(null, string.Empty, string.Empty, 0, false, null, new Labels[0])
        {
        }

        /// <summary>Initializes a new instance of the <see cref="Test" /> class with a description.</summary>
        /// <param name="target">The target <see cref="Type" />.</param>
        /// <param name="expectedResult">The expected result.</param>
        public Test(Type target, object expectedResult) : this(target, string.Empty, string.Empty, 0, false, expectedResult)
        {
        }

        /// <summary>Initializes a new instance of the <see cref="Test" /> class with a description.</summary>
        /// <param name="target">The target <see cref="Type" />.</param>
        /// <param name="description">The description text.</param>
        /// <param name="author">The author.</param>
        public Test(Type target, string description, string author) : this(target, description, author, 0)
        {
        }

        /// <summary>Initializes a new instance of the <see cref="Test" /> class with a description.</summary>
        /// <param name="target">The target <see cref="Type" />.</param>
        /// <param name="description">The description text.</param>
        public Test(Type target, string description) : this(target, description, string.Empty)
        {
        }

        /// <summary>Initializes a new instance of the <see cref="Test" /> class.</summary>
        /// <param name="target">The target <see cref="Type" />.</param>
        /// <param name="description">The description text.</param>
        /// <param name="author">The test author.</param>
        /// <param name="errorCode">The error Code.</param>
        /// <param name="explicitRun">The explicit Run.</param>
        /// <param name="expectedResult">The expected Result.</param>
        /// <param name="collection">The collection.</param>
        private Test(Type target = null, string description = "", string author = "", int errorCode = 0, bool explicitRun = false, object expectedResult = null, Labels[] collection = null) : base(target, description)
        {
            Author = author;
            ErrorCode = errorCode;
            Explicit = explicitRun;
            ExpectedResult = expectedResult;
            Labels = collection;
        }

        #endregion Constructors and Destructors

        #region Public Properties

        /// <summary>Gets or sets the author of the <see cref="Test" />.</summary>
        /// <returns>The <see cref="string" />.</returns>
        public virtual string Author { get; set; }

        /// <summary>Gets or sets the error code value of the <see cref="Test" />.</summary>
        /// <returns>The <see cref="int" />.</returns>
        public virtual int ErrorCode { get; set; }

        /// <summary>Gets or sets the author of the <see cref="Test" />.</summary>
        /// <returns>The <see cref="string" />.</returns>
        public virtual object ExpectedResult { get; set; }

        /// <summary>Gets or sets the whether the <see cref="Test" /> can be run explicitly.</summary>
        /// <returns>The <see cref="string" />.</returns>
        public virtual bool Explicit { get; set; }

        /// <summary>Gets or sets the labels <see cref="Array" /> of the <see cref="Test" />.</summary>
        /// <returns>The <see cref="Labels" />.</returns>
        public virtual Labels[] Labels { get; set; }

        #endregion Public Properties

        #region Public Methods and Operators

        public override bool Equals(object obj)
        {
            if (obj == this)
            {
                return true;
            }

            switch (obj)
            {
                case Test testAttribute:
                    {
                        bool equal;

                        // Validate the property's
                        if ((testAttribute.DescriptionValue == DescriptionValue) &&
                            (testAttribute.Description == Description) &&
                            (testAttribute.Target == Target) &&
                            (testAttribute.ErrorCode == ErrorCode) &&
                            (testAttribute.Author == Author) &&
                            (testAttribute.Explicit == Explicit) &&
                            (testAttribute.ExpectedResult == ExpectedResult) &&
                            (testAttribute.Labels == Labels))
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
                return this.ToDebug("TargetName");
            }
            else
            {
                return base.ToString();
            }
        }

        #endregion Public Methods and Operators
    }
}