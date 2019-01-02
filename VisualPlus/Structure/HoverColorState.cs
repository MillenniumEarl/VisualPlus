#region License

// -----------------------------------------------------------------------------------------------------------
// 
// Name: HoverColorState.cs
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
    [Description("The hover color state of a component.")]
    [Category(PropertyCategory.Appearance)]
    public class HoverColorState : ColorState
    {
        #region Fields

        private Color _hover;

        #endregion

        #region Constructors and Destructors

        /// <summary>Initializes a new instance of the <see cref="HoverColorState" /> class.</summary>
        /// <param name="disabled">The disabled color</param>
        /// <param name="enabled">The enabled color.</param>
        /// <param name="hover">The hover color.</param>
        public HoverColorState(Color disabled, Color enabled, Color hover)
        {
            Disabled = disabled;
            Enabled = enabled;
            _hover = hover;
        }

        /// <summary>Initializes a new instance of the <see cref="HoverColorState" /> class.</summary>
        public HoverColorState()
        {
            Disabled = Color.Empty;
            Enabled = Color.Empty;
            _hover = Color.Empty;
        }

        #endregion

        #region Public Events

        [Category(EventCategory.PropertyChanged)]
        [Description(EventDescription.PropertyEventChanged)]
        public event BackColorStateChangedEventHandler HoverColorChanged;

        #endregion

        #region Public Properties

        [NotifyParentProperty(true)]
        [RefreshProperties(RefreshProperties.Repaint)]
        [Description(PropertyDescription.Color)]
        public Color Hover
        {
            get
            {
                return _hover;
            }

            set
            {
                _hover = value;
                OnDisabledColorChanged(new ColorEventArgs(_hover));
            }
        }

        /// <summary>Gets a value indicating whether this <see cref="ControlColorState" /> is empty.</summary>
        [Browsable(false)]
        public new bool IsEmpty
        {
            get
            {
                return _hover.IsEmpty && Disabled.IsEmpty && Enabled.IsEmpty;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>Get the control back color state.</summary>
        /// <param name="hoverColorState">The hover Color State.</param>
        /// <param name="enabled">The enabled toggle.</param>
        /// <param name="mouseState">The mouse state.</param>
        /// <returns>
        ///     <see cref="Color" />
        /// </returns>
        public static Color BackColorState(HoverColorState hoverColorState, bool enabled, MouseStates mouseState)
        {
            Color _color;

            if (enabled)
            {
                switch (mouseState)
                {
                    case MouseStates.Normal:
                        {
                            _color = hoverColorState.Enabled;
                            break;
                        }

                    case MouseStates.Hover:
                        {
                            _color = hoverColorState.Hover;
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
                _color = hoverColorState.Disabled;
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
                _stringBuilder.Append("Hover=");
                _stringBuilder.Append(Hover);
                _stringBuilder.Append("Normal=");
                _stringBuilder.Append(Enabled);
            }

            _stringBuilder.Append("]");

            return _stringBuilder.ToString();
        }

        #endregion

        #region Methods

        protected virtual void OnHoverColorChanged(ColorEventArgs e)
        {
            HoverColorChanged?.Invoke(e);
        }

        #endregion
    }
}