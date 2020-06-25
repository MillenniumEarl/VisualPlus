#region License

// -----------------------------------------------------------------------------------------------------------
//
// Name: ControlManager.cs
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
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using VisualPlus.Toolkit.Controls.DataManagement;
using VisualPlus.Toolkit.Controls.DataVisualization;
using VisualPlus.Toolkit.Controls.Editors;
using VisualPlus.Toolkit.Controls.Interactivity;
using VisualPlus.Toolkit.Controls.Layout;
using VisualPlus.Toolkit.Dialogs;

#endregion Namespace

namespace VisualPlus.Utilities
{
    [Description("The control manager.")]
    public sealed class ControlManager
    {
        #region Public Methods and Operators

        /// <summary>Centers the control inside the parent control.</summary>
        /// <param name="control">The control to center.</param>
        /// <param name="parent">The parent control.</param>
        /// <param name="centerX">Center X coordinate.</param>
        /// <param name="centerY">Center Y coordinate.</param>
        public static void CenterControl(Control control, Control parent, bool centerX, bool centerY)
        {
            Point _controlLocation = control.Location;

            if (centerX)
            {
                _controlLocation.X = (parent.Width - control.Width) / 2;
            }

            if (centerY)
            {
                _controlLocation.Y = (parent.Height - control.Height) / 2;
            }

            control.Location = _controlLocation;
        }

        /// <summary>Retrieves the registered controls.</summary>
        /// <returns>The <see cref="Type" /> list.</returns>
        public static List<Type> ControlsSupported()
        {
            var control = new List<Type>
                {
                    typeof(VisualButton),
                    typeof(VisualCheckBox),
                    typeof(VisualComboBox),
                    typeof(VisualControlBox),
                    typeof(VisualDateTimePicker),
                    typeof(VisualGauge),
                    typeof(VisualGroupBox),
                    typeof(VisualLabel),
                    typeof(VisualListBox),
                    typeof(VisualListView),
                    typeof(VisualNumericUpDown),
                    typeof(VisualPanel),
                    typeof(VisualProgressBar),
                    typeof(VisualRadialProgress),
                    typeof(VisualRadioButton),
                    typeof(VisualRating),
                    typeof(VisualRichTextBox),
                    typeof(VisualScrollBar),
                    typeof(VisualSeparator),
                    typeof(VisualTextBox),
                    typeof(VisualTile),
                    typeof(VisualToggle),
                    typeof(VisualTrackBar)
                };

            return control;
        }

        /// <summary>Retrieves the registered dialog forms.</summary>
        /// <returns>The <see cref="Type" /> list.</returns>
        public static List<Type> DialogsSupported()
        {
            var control = new List<Type> { typeof(VisualForm) };

            return control;
        }

        /// <summary>Determines if the <see cref="Component" /> type is a Control.</summary>
        /// <param name="componentType">The component type</param>
        /// <returns>The <see cref="bool" />.</returns>
        public static bool IsControl(Type componentType)
        {
            var control = false;

            foreach (Type controlType in ControlsSupported())
            {
                if (componentType == controlType)
                {
                    control = true;
                }
            }

            return control;
        }

        /// <summary>Determines if the <see cref="Component" /> type is a Dialog.</summary>
        /// <param name="componentType">The component type</param>
        /// <returns>The <see cref="bool" />.</returns>
        public static bool IsDialog(Type componentType)
        {
            var dialog = false;

            foreach (Type dialogType in DialogsSupported())
            {
                if (componentType == dialogType)
                {
                    dialog = true;
                }
            }

            return dialog;
        }

        /// <summary>Retrieves the registered theme supported types.</summary>
        /// <returns>The <see cref="Type" /> list.</returns>
        public static List<Type> ThemeSupportedTypes()
        {
            var components = new List<Type>();

            components.AddRange(ControlsSupported());
            components.AddRange(DialogsSupported());

            return components;
        }

        #endregion Public Methods and Operators
    }
}