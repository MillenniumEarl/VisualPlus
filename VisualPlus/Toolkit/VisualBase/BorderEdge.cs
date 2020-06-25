#region License

// -----------------------------------------------------------------------------------------------------------
//
// Name: BorderEdge.cs
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

using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

using VisualPlus.Localization;

#endregion Namespace

namespace VisualPlus.Toolkit.VisualBase
{
    [ToolboxItem(false)]
    [DefaultEvent("Paint")]
    [DefaultProperty("Orientation")]
    [DesignerCategory("code")]
    [Description("Displays a vertical or horizontal border edge.")]
    [ClassInterface(ClassInterfaceType.AutoDispatch)]
    [ComVisible(true)]
    public class BorderEdge : VisualStyleBase
    {
        #region Fields

        private Orientation _orientation;

        #endregion Fields

        #region Constructors and Destructors

        /// <summary>Initializes a new instance of the <see cref="BorderEdge" /> class.</summary>
        public BorderEdge()
        {
            SetStyle(ControlStyles.Selectable, false);
            Cursor = Cursors.Default;
            BackColor = Color.Black;
            Size = new Size(50, 50);
            _orientation = Orientation.Horizontal;
        }

        #endregion Constructors and Destructors

        #region Public Properties

        [Category(PropertyCategory.Behavior)]
        [Description(PropertyDescription.Orientation)]
        public Orientation Orientation
        {
            get
            {
                return _orientation;
            }

            set
            {
                _orientation = value;

                if (_orientation == Orientation.Horizontal)
                {
                    if (Width < Height)
                    {
                        int _temp = Width;
                        Width = Height;
                        Height = _temp;
                    }
                }
                else
                {
                    if (Width > Height)
                    {
                        int _temp = Width;
                        Width = Height;
                        Height = _temp;
                    }
                }

                Invalidate();
            }
        }

        #endregion Public Properties

        #region Methods

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.DrawLine(new Pen(BackColor), Location.X, Location.Y, Width, Height);
        }

        #endregion Methods
    }
}