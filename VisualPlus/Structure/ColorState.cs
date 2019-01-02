#region License

// -----------------------------------------------------------------------------------------------------------
// 
// Name: ColorState.cs
// VisualPlus - The VisualPlus Framework (VPF) for WinForms .NET development.
// 
// Created: 10/12/2018 - 11:45 PM
// Last Modified: 02/01/2019 - 1:24 AM
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
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;

using VisualPlus.Delegates;
using VisualPlus.Enumerators;
using VisualPlus.Events;
using VisualPlus.Localization;
using VisualPlus.TypeConverters;

#endregion

namespace VisualPlus.Structure
{
    [TypeConverter(typeof(VisualSettingsTypeConverter))]
    [ToolboxItem(false)]
    [DesignerCategory("code")]
    [ClassInterface(ClassInterfaceType.AutoDispatch)]
    [ComVisible(true)]
    [Description("The color states of a component.")]
    [Category(PropertyCategory.Appearance)]
    public class ColorState
    {
        #region Fields

        private Color _disabled;
        private Color _enabled;

        #endregion

        #region Constructors and Destructors

        /// <summary>Initializes a new instance of the <see cref="ColorState" /> class.</summary>
        /// <param name="disabled">The disabled color.</param>
        /// <param name="enabled">The normal color.</param>
        public ColorState(Color disabled, Color enabled)
        {
            _disabled = disabled;
            _enabled = enabled;
        }

        /// <summary>Initializes a new instance of the <see cref="ColorState" /> class.</summary>
        public ColorState()
        {
            _disabled = Color.Empty;
            _enabled = Color.Empty;
        }

        #endregion

        #region Public Events

        [Category(EventCategory.PropertyChanged)]
        [Description(EventDescription.PropertyEventChanged)]
        public event BackColorStateChangedEventHandler DisabledColorChanged;

        [Category(EventCategory.PropertyChanged)]
        [Description(EventDescription.PropertyEventChanged)]
        public event BackColorStateChangedEventHandler NormalColorChanged;

        #endregion

        #region Public Properties

        [NotifyParentProperty(true)]
        [RefreshProperties(RefreshProperties.Repaint)]
        [Description(PropertyDescription.Color)]
        public Color Disabled
        {
            get
            {
                return _disabled;
            }

            set
            {
                _disabled = value;
                OnDisabledColorChanged(new ColorEventArgs(_disabled));
            }
        }

        [NotifyParentProperty(true)]
        [RefreshProperties(RefreshProperties.Repaint)]
        [Description(PropertyDescription.Color)]
        public Color Enabled
        {
            get
            {
                return _enabled;
            }

            set
            {
                _enabled = value;
                OnDisabledColorChanged(new ColorEventArgs(_enabled));
            }
        }

        /// <summary>Gets a value indicating whether this <see cref="ColorState" /> is empty.</summary>
        [Browsable(false)]
        public bool IsEmpty
        {
            get
            {
                return _disabled.IsEmpty && _enabled.IsEmpty;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>Get the control back color state.</summary>
        /// <param name="colorState">The color State.</param>
        /// <param name="enabled">The enabled toggle.</param>
        /// <param name="mouseState">The mouse state.</param>
        /// <returns>
        ///     <see cref="Color" />
        /// </returns>
        public static Color BackColorState(ColorState colorState, bool enabled, MouseStates mouseState)
        {
            Color _color;

            if (enabled)
            {
                switch (mouseState)
                {
                    case MouseStates.Normal:
                        {
                            _color = colorState.Enabled;
                            break;
                        }

                    case MouseStates.Hover:
                        {
                            _color = colorState.Enabled;
                            break;
                        }

                    case MouseStates.Pressed:
                        {
                            _color = colorState.Enabled;
                            break;
                        }

                    default:
                        {
                            throw new ArgumentOutOfRangeException(nameof(mouseState), mouseState, null);
                        }
                }
            }
            else
            {
                _color = colorState.Disabled;
            }

            return _color;
        }

        public override string ToString()
        {
            StringBuilder _stringBuilder = new StringBuilder();
            _stringBuilder.Append(GetType().Name);
            _stringBuilder.Append(" [");

            if (IsEmpty)
            {
                _stringBuilder.Append("IsEmpty");
            }
            else
            {
                _stringBuilder.Append("Disabled=");
                _stringBuilder.Append(Disabled);
                _stringBuilder.Append("Normal=");
                _stringBuilder.Append(Enabled);
            }

            _stringBuilder.Append("]");

            return _stringBuilder.ToString();
        }

        #endregion

        #region Methods

        protected virtual void OnDisabledColorChanged(ColorEventArgs e)
        {
            DisabledColorChanged?.Invoke(e);
        }

        protected virtual void OnNormalColorChanged(ColorEventArgs e)
        {
            NormalColorChanged?.Invoke(e);
        }

        #endregion
    }
}