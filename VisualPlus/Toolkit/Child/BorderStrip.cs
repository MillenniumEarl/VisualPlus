#region License

// -----------------------------------------------------------------------------------------------------------
// 
// Name: BorderStrip.cs
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
using System.Drawing;
using System.Windows.Forms;

#endregion

namespace VisualPlus.Toolkit.Child
{
    [ToolboxItem(false)]
    public class BorderStrip : Control
    {
        #region Fields

        private BorderTypes _borderType;
        private Container _components;

        #endregion

        #region Constructors and Destructors

        /// <summary>Initializes a new instance of the <see cref="BorderStrip" /> class.</summary>
        public BorderStrip()
        {
            InitializeComponent();
        }

        #endregion

        #region Enums

        public enum BorderTypes
        {
            /// <summary>The left.</summary>
            Left = 0,

            /// <summary>The right.</summary>
            Right = 1,

            /// <summary>The top.</summary>
            Top = 2,

            /// <summary>The bottom.</summary>
            Bottom = 3,

            /// <summary>The square.</summary>
            Square = 4
        }

        #endregion

        #region Public Properties

        /// <summary>The look of the control outside.</summary>
        public BorderTypes BorderType
        {
            get
            {
                return _borderType;
            }

            set
            {
                _borderType = value;
            }
        }

        #endregion

        #region Methods

        /// <summary>Cleanup any resources being used.</summary>
        /// <param name="disposing">Indicates whether the method call comes from a <see cref="Dispose" /> method or a finalizer.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_components != null)
                {
                    _components.Dispose();
                }
            }

            base.Dispose(disposing);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            switch (_borderType)
            {
                case BorderTypes.Left:
                    {
                        // To make a fake rectangle is because we are specifically looking for only a part of the rectangle which in this case is the left 2 however, we have to make it bigger for it to draw an entire left side with 2 pixels, I made it 8 just for safety.
                        Rectangle _rectangle = new Rectangle(0, 0, 8, ClientRectangle.Height);
                        ControlPaint.DrawBorder3D(e.Graphics, _rectangle, Border3DStyle.Sunken);
                        break;
                    }

                case BorderTypes.Right:
                    {
                        // This should put only the right 2 pixels of the border on the visible strip.
                        Rectangle _rectangle = new Rectangle(-6, 0, 8, ClientRectangle.Height);
                        ControlPaint.DrawBorder3D(e.Graphics, _rectangle, Border3DStyle.Sunken);
                        break;
                    }

                case BorderTypes.Top:
                    {
                        Rectangle _rectangle = new Rectangle(0, 0, ClientRectangle.Width, 8);
                        ControlPaint.DrawBorder3D(e.Graphics, _rectangle, Border3DStyle.Sunken);
                        break;
                    }

                case BorderTypes.Bottom:
                    {
                        Rectangle _rectangle = new Rectangle(0, -6, ClientRectangle.Width, 8);
                        ControlPaint.DrawBorder3D(e.Graphics, _rectangle, Border3DStyle.Sunken);
                        break;
                    }

                case BorderTypes.Square:
                    {
                        ControlPaint.DrawBorder3D(e.Graphics, ClientRectangle, Border3DStyle.SunkenInner);

                        // e.Graphics.FillRectangle(SystemBrushes.Control, ClientRectangle);
                        break;
                    }

                default:
                    {
                        throw new ArgumentOutOfRangeException();
                    }
            }

            base.OnPaint(e);
        }

        /// <summary>Required method for Designer support. Do not modify contents of this method with the code editor.</summary>
        private void InitializeComponent()
        {
            _components = new Container();
        }

        #endregion
    }
}