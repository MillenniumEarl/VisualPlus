#region License

// -----------------------------------------------------------------------------------------------------------
// 
// Name: ProgressBase.cs
// VisualPlus - The VisualPlus Framework (VPF) for WinForms .NET development.
// 
// Created: 10/12/2018 - 11:45 PM
// Last Modified: 02/01/2019 - 1:27 AM
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

using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

using VisualPlus.Enumerators;
using VisualPlus.Localization;

#endregion

namespace VisualPlus.Toolkit.VisualBase
{
    [DesignerCategory("code")]
    [ClassInterface(ClassInterfaceType.AutoDispatch)]
    [ComVisible(true)]
    [ToolboxItem(false)]
    public abstract class ProgressBase : VisualStyleBase
    {
        #region Fields

        private int _largeChange;
        private int _maximum;
        private int _minimum;
        private int _smallChange;
        private int _value;

        #endregion

        #region Constructors and Destructors

        /// <summary>Initializes a new instance of the <see cref="ProgressBase" /> class.</summary>
        protected ProgressBase()
        {
            _value = 0;
            _minimum = 0;
            _maximum = 10;
            _smallChange = 1;
            _largeChange = 5;
        }

        #endregion

        #region Public Events

        [Category(EventCategory.Action)]
        [Description("Occurs when the value of the Value property changes.")]
        public event EventHandler ValueChanged;

        #endregion

        #region Public Properties

        [Bindable(true)]
        [Category(PropertyCategory.Behavior)]
        [Description("Gets or sets a value to be added to or subtracted from the Value property when the scroll box is moved a large distance.")]
        public int LargeChange
        {
            get
            {
                return _largeChange;
            }

            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(LargeChange.ToString(), @"LargeChange cannot be less than zero.");
                }

                _largeChange = value;
            }
        }

        [Bindable(true)]
        [Category(PropertyCategory.Behavior)]
        [Description("The upper bound of the range this ProgressBar is working on.")]
        public int Maximum
        {
            get
            {
                return _maximum;
            }

            set
            {
                if (value != _maximum)
                {
                    if (value < _minimum)
                    {
                        _minimum = value;
                    }

                    SetRange(Minimum, value);
                }
            }
        }

        [Bindable(true)]
        [Category(PropertyCategory.Behavior)]
        [Description("The lower bound of the range this ProgressBar is working on.")]
        public int Minimum
        {
            get
            {
                return _minimum;
            }

            set
            {
                if (value != _minimum)
                {
                    if (value > _maximum)
                    {
                        _maximum = value;
                    }

                    SetRange(value, Maximum);
                }
            }
        }

        [Bindable(true)]
        [Category(PropertyCategory.Behavior)]
        [Description("Gets or sets the value added to or subtracted from the Value property when the scroll box is moved a small distance.")]
        public int SmallChange
        {
            get
            {
                return _smallChange;
            }

            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(SmallChange.ToString(), @"SmallChange cannot be less than zero.");
                }

                _smallChange = value;
            }
        }

        [Bindable(true)]
        [Category(PropertyCategory.Behavior)]
        [Description("The current value for the ProgressBar, in the range specified by the minimum and maximum properties.")]
        public int Value
        {
            get
            {
                return _value;
            }

            set
            {
                if (value != _value)
                {
                    if ((value < Minimum) || (value > Maximum))
                    {
                        throw new ArgumentOutOfRangeException(nameof(Value), @"Provided value is out of the Minimum to Maximum range of values.");
                    }

                    _value = value;
                    OnValueChanged(EventArgs.Empty);
                }
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>Decrement from the value.</summary>
        /// <param name="value">Amount of value to decrement.</param>
        public void Decrement(int value)
        {
            if (Value > Minimum)
            {
                Value -= value;
                if (Value < Minimum)
                {
                    Value = Minimum;
                }
            }
            else
            {
                Value = Minimum;
            }

            Invalidate();
        }

        /// <summary>Increment to the value.</summary>
        /// <param name="value">Amount of value to increment.</param>
        public void Increment(int value)
        {
            if (Value < Maximum)
            {
                Value += value;
                if (Value > Maximum)
                {
                    Value = Maximum;
                }
            }
            else
            {
                Value = Maximum;
            }

            Invalidate();
        }

        /// <summary>Set the value range.</summary>
        /// <param name="minimumValue">The minimum.</param>
        /// <param name="maximumValue">The maximum.</param>
        public void SetRange(int minimumValue, int maximumValue)
        {
            if ((Minimum != minimumValue) || (Maximum != maximumValue))
            {
                if (minimumValue > maximumValue)
                {
                    minimumValue = maximumValue;
                }

                _minimum = minimumValue;
                _maximum = maximumValue;

                int beforeValue = _value;
                if (_value < _minimum)
                {
                    _value = _minimum;
                }

                if (_value > _maximum)
                {
                    _value = _maximum;
                }

                if (beforeValue != _value)
                {
                    OnValueChanged(EventArgs.Empty);
                }
            }
        }

        #endregion

        #region Methods

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            MouseState = MouseStates.Hover;
            Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            MouseState = MouseStates.Normal;
            Invalidate();
        }

        protected virtual void OnValueChanged(EventArgs e)
        {
            ValueChanged?.Invoke(this, e);
        }

        #endregion
    }
}