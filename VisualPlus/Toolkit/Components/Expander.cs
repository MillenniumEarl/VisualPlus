#region License

// -----------------------------------------------------------------------------------------------------------
// 
// Name: Expander.cs
// VisualPlus - The VisualPlus Framework (VPF) for WinForms .NET development.
// 
// Created: 10/12/2018 - 11:45 PM
// Last Modified: 02/01/2019 - 12:39 AM
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
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using VisualPlus.Enumerators;
using VisualPlus.Localization;
using VisualPlus.Renders;

#endregion

namespace VisualPlus.Toolkit.Components
{
    [ToolboxItem(false)]
    [Description("The VisualPlus expander component enables controls to be expandable.")]
    public class Expander : Label
    {
        #region Static Fields

        private static List<Control> controlsList = new List<Control>();

        #endregion

        #region Public Properties

        [DefaultValue(typeof(Color), "Black")]
        [Category(PropertyCategory.Appearance)]
        public Color Color { get; set; }

        [DefaultValue(typeof(int), "0")]
        [Category(PropertyCategory.Appearance)]
        public int ContractedHeight { get; set; }

        [DefaultValue(typeof(Size), "0, 0")]
        [Category(PropertyCategory.Appearance)]
        public Size Original { get; set; }

        [DefaultValue(typeof(bool), "true")]
        [Category(PropertyCategory.Appearance)]
        public bool State { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>Add a expander control to the control, with the specified settings.</summary>
        /// <param name="control">The control to draw the expander on.</param>
        /// <param name="rectangle">The rectangle.</param>
        /// <param name="contractedHeight">The contracted Height.</param>
        /// <param name="state">The expanded state.</param>
        /// <returns>The <see cref="bool" />.</returns>
        public static bool Add(Control control, Rectangle rectangle, int contractedHeight, bool state)
        {
            if (controlsList.Contains(control))
            {
                return false;
            }

            ConstructExpander(control, Color.Black, rectangle, contractedHeight, Cursors.Hand, state);
            return true;
        }

        /// <summary>Add a expander control to the control, with the specified settings.</summary>
        /// <param name="control">The control to draw the expander on.</param>
        /// <param name="color">The color.</param>
        /// <param name="rectangle">The rectangle.</param>
        /// <param name="contractedHeight">The contracted Height.</param>
        /// <param name="state">The expanded state.</param>
        /// <returns>The <see cref="bool" />.</returns>
        public static bool Add(Control control, Color color, Rectangle rectangle, int contractedHeight, bool state)
        {
            if (controlsList.Contains(control))
            {
                return false;
            }

            ConstructExpander(control, color, rectangle, contractedHeight, Cursors.Hand, state);
            return true;
        }

        /// <summary>Add a expander control to the control, with the specified settings.</summary>
        /// <param name="control">The control to draw the expander on.</param>
        /// <param name="color">The color.</param>
        /// <param name="rectangle">The rectangle.</param>
        /// <param name="contractedHeight">The contracted Height.</param>
        /// <param name="cursor">The cursor.</param>
        /// <param name="state">The expanded state.</param>
        /// <returns>The <see cref="bool" />.</returns>
        public static bool Add(Control control, Color color, Rectangle rectangle, int contractedHeight, Cursor cursor, bool state)
        {
            if (controlsList.Contains(control))
            {
                return false;
            }

            ConstructExpander(control, color, rectangle, contractedHeight, cursor, state);
            return true;
        }

        /// <summary>Draws the expander button.</summary>
        /// <param name="graphics">The graphics to draw on.</param>
        /// <param name="rectangle">The button rectangle.</param>
        /// <param name="color">The button color.</param>
        /// <param name="state">The expanded toggle.</param>
        public static void Draw(Graphics graphics, Rectangle rectangle, Color color, bool state)
        {
            VisualElementRenderer.RenderTriangle(graphics, rectangle, color, state ? Alignment.Vertical.Up : Alignment.Vertical.Down);
        }

        /// <summary>Gets the back color from the expander.</summary>
        /// <param name="control">The control.</param>
        /// <returns>The <see cref="Color" />.</returns>
        public static Color GetColor(Control control)
        {
            Expander _expander = GetExpander(control);
            return _expander?.Color ?? DefaultBackColor;
        }

        /// <summary>Gets the contracted height from the expander.</summary>
        /// <param name="control">The control.</param>
        /// <returns>The <see cref="int" />.</returns>
        public static int GetContractedHeight(Control control)
        {
            Expander _expander = GetExpander(control);
            return _expander?.ContractedHeight ?? 0;
        }

        /// <summary>Gets the cursor from the expander.</summary>
        /// <param name="control">The control.</param>
        /// <returns>The <see cref="Cursor" />.</returns>
        public static Cursor GetCursor(Control control)
        {
            Expander _expander = GetExpander(control);
            return _expander?.Cursor ?? Cursors.Hand;
        }

        /// <summary>Gets the expander from the control.</summary>
        /// <param name="control">The control.</param>
        /// <returns>The <see cref="Expander" />.</returns>
        public static Expander GetExpander(Control control)
        {
            for (var i = 0; i < control.Controls.Count; i++)
            {
                if (control.Controls[i] is Expander)
                {
                    return control.Controls[i] as Expander;
                }
            }

            return null;
        }

        /// <summary>Gets the original size from the expander.</summary>
        /// <param name="control">The control.</param>
        /// <returns>The <see cref="Size" />.</returns>
        public static Size GetOriginal(Control control)
        {
            Expander _expander = GetExpander(control);
            return _expander?.Original ?? new Size(0, 0);
        }

        /// <summary>Gets the position for the expander.</summary>
        /// <param name="control">The control.</param>
        /// <returns>The <see cref="Point" />.</returns>
        public static Point GetPosition(Control control)
        {
            Expander _expander = GetExpander(control);
            return _expander?.Location ?? new Point(0, 0);
        }

        /// <summary>Gets the size for the expander.</summary>
        /// <param name="control">The control.</param>
        /// <returns>The <see cref="Size" />.</returns>
        public static Size GetSize(Control control)
        {
            Expander _expander = GetExpander(control);
            return _expander?.Size ?? new Size(0, 0);
        }

        /// <summary>Gets the expanded state from the expander.</summary>
        /// <param name="control">The control.</param>
        /// <returns>The <see cref="bool" />.</returns>
        public static bool GetState(Control control)
        {
            Expander _expander = GetExpander(control);
            return _expander?.State ?? false;
        }

        /// <summary>Remove the expander from the control.</summary>
        /// <param name="control">The control to remove the expander from.</param>
        /// <returns>The <see cref="bool" />.</returns>
        public static bool Remove(Control control)
        {
            Expander _expander = GetExpander(control);
            if (_expander != null)
            {
                control.Controls.Remove(_expander);
                controlsList.Remove(control);
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>Sets the back color for the expander.</summary>
        /// <param name="control">The control.</param>
        /// <param name="color">The color.</param>
        public static void SetColor(Control control, Color color)
        {
            Expander _expander = GetExpander(control);
            if (_expander != null)
            {
                _expander.Color = color;
                _expander.Invalidate();
            }
        }

        /// <summary>Sets the contracted height for the expander.</summary>
        /// <param name="control">The control.</param>
        /// <param name="contractedHeight">The contracted Height.</param>
        public static void SetContractedHeight(Control control, int contractedHeight)
        {
            Expander _expander = GetExpander(control);
            if (_expander != null)
            {
                _expander.ContractedHeight = contractedHeight;
            }
        }

        /// <summary>Sets the cursor for the expander.</summary>
        /// <param name="control">The control.</param>
        /// <param name="cursor">The cursor.</param>
        public static void SetCursor(Control control, Cursor cursor)
        {
            Expander _expander = GetExpander(control);
            if (_expander != null)
            {
                _expander.Cursor = cursor;
            }
        }

        /// <summary>Sets the original size for the expander.</summary>
        /// <param name="control">The control.</param>
        /// <param name="original">The original.</param>
        public static void SetOriginal(Control control, Size original)
        {
            Expander _expander = GetExpander(control);
            if (_expander != null)
            {
                _expander.Original = original;
            }
        }

        /// <summary>Sets the position for the expander.</summary>
        /// <param name="control">The control.</param>
        /// <param name="position">The position.</param>
        public static void SetPosition(Control control, Point position)
        {
            Expander expander = GetExpander(control);
            if (expander != null)
            {
                expander.Location = position;
            }
        }

        /// <summary>Sets the size for the expander.</summary>
        /// <param name="control">The control.</param>
        /// <param name="size">The size.</param>
        public static void SetSize(Control control, Size size)
        {
            Expander _expander = GetExpander(control);
            if (_expander != null)
            {
                _expander.Size = size;
            }
        }

        /// <summary>Sets the expanded state for the expander.</summary>
        /// <param name="control">The control.</param>
        /// <param name="expanded">The expanded.</param>
        public static void SetState(Control control, bool expanded)
        {
            Expander _expander = GetExpander(control);
            if (_expander != null)
            {
                _expander.State = expanded;
                ConfigureState(control, _expander, expanded);
            }
        }

        #endregion

        #region Methods

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);

            ConfigureState(Parent, this, State);
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Draw(e.Graphics, new Rectangle(0, 0, Width, Height), Color, State);
        }

        /// <summary>Attach the expander control.</summary>
        /// <param name="control">The control.</param>
        /// <param name="expander">The expander.</param>
        private static void Attach(Control control, Expander expander)
        {
            controlsList.Add(control);
            control.Controls.Add(expander);

            // control.Resize += ControlReSizeChanged;
            // control.SizeChanged += ControlReSizeChanged;
        }

        /// <summary>Configures the state of the control.</summary>
        /// <param name="control">The control.</param>
        /// <param name="expander">The expander.</param>
        /// <param name="state">The state.</param>
        private static void ConfigureState(Control control, Expander expander, bool state)
        {
            int _height;

            if (state)
            {
                _height = expander.ContractedHeight;
                expander.State = false;
            }
            else
            {
                _height = expander.Original.Height;
                expander.State = true;
            }

            control.Size = new Size(GetOriginal(control).Width, _height);
            control.Invalidate();
        }

        /// <summary>Construct a expander control for the control, with the specified settings.</summary>
        /// <param name="control">The control to draw the expander on.</param>
        /// <param name="color">The color of the expander.</param>
        /// <param name="rectangle">The expander rectangle.</param>
        /// <param name="contractedHeight">The contracted height.</param>
        /// <param name="cursor">The cursor for the expander.</param>
        /// <param name="state">The state toggle.</param>
        private static void ConstructExpander(Control control, Color color, Rectangle rectangle, int contractedHeight, Cursor cursor, bool state)
        {
            Expander _expander = new Expander
                {
                    AutoSize = false,
                    BackColor = Color.Transparent,
                    Color = color,
                    Location = rectangle.Location,
                    Size = rectangle.Size,
                    Original = control.Size,
                    ContractedHeight = contractedHeight,
                    Cursor = cursor,
                    State = state
                };

            Attach(control, _expander);
            SetPosition(control, rectangle.Location);
        }

        #endregion
    }
}