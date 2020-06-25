#region License

// -----------------------------------------------------------------------------------------------------------
//
// Name: VisualContextMenu.cs
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
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;

using VisualPlus.Constants;
using VisualPlus.Enumerators;
using VisualPlus.Localization;
using VisualPlus.Models;
using VisualPlus.TypeConverters;

#endregion Namespace

namespace VisualPlus.Toolkit.Components
{
    [Obsolete]
    [ClassInterface(ClassInterfaceType.AutoDispatch)]
    [ComVisible(true)]
    [DefaultEvent("Opening")]
    [DefaultProperty("Items")]
    [Description("The Visual Context Menu Strip")]
    [ToolboxBitmap(typeof(VisualContextMenu), "VisualContextMenu.bmp")]
    [ToolboxItem(false)]
    public class VisualContextMenu : ContextMenuStrip
    {
        #region Static Fields

        private static bool _arrowVisible = DefaultConstants.TextVisible;
        private static Color _backgroundColor;
        private static Color _selectedItemBackColor;
        private static Color _textHoverColor;
        private static Color arrowColor;
        private static Color arrowDisabledColor;
        private static Border border;
        private static Font contextMenuFont;
        private static Color foreColor;
        private static Color textDisabledColor;

        #endregion Static Fields

        #region Fields

        private ToolStripItemClickedEventArgs _toolStripItemClickedEventArgs;

        private StyleManager styleManager;

        #endregion Fields

        #region Constructors and Destructors

        /// <summary>Initializes a new instance of the<see cref="VisualContextMenu" /> class.</summary>
        public VisualContextMenu()
        {
            styleManager = new StyleManager(DefaultConstants.DefaultStyle);

            Renderer = new VisualToolStripRender();
            ConfigureStyleManager();
        }

        #endregion Constructors and Destructors

        #region Delegates

        public delegate void ClickedEventHandler(object sender);

        #endregion Delegates

        #region Public Events

        public event ClickedEventHandler Clicked;

        #endregion Public Events

        #region Public Properties

        [Category(PropertyCategory.Appearance)]
        [Description(PropertyDescription.Color)]
        public Color ArrowColor
        {
            get
            {
                return arrowColor;
            }

            set
            {
                arrowColor = value;
                Invalidate();
            }
        }

        [Category(PropertyCategory.Appearance)]
        [Description(PropertyDescription.Color)]
        public Color ArrowDisabledColor
        {
            get
            {
                return arrowDisabledColor;
            }

            set
            {
                arrowDisabledColor = value;
                Invalidate();
            }
        }

        [DefaultValue(DefaultConstants.BorderVisible)]
        [Category(PropertyCategory.Behavior)]
        [Description(PropertyDescription.Visible)]
        public bool ArrowVisible
        {
            get
            {
                return _arrowVisible;
            }

            set
            {
                _arrowVisible = value;
                Invalidate();
            }
        }

        [Category(PropertyCategory.Appearance)]
        [Description(PropertyDescription.Color)]
        public Color Background
        {
            get
            {
                return _backgroundColor;
            }

            set
            {
                _backgroundColor = value;
                Invalidate();
            }
        }

        [TypeConverter(typeof(VisualSettingsTypeConverter))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Category(PropertyCategory.Appearance)]
        public Border Border
        {
            get
            {
                return border;
            }

            set
            {
                border = value;
                Invalidate();
            }
        }

        [Browsable(true)]
        [Category(PropertyCategory.Appearance)]
        [Description(PropertyDescription.Color)]
        public new Color ForeColor
        {
            get
            {
                return foreColor;
            }

            set
            {
                base.ForeColor = value;
                foreColor = value;
                Invalidate();
            }
        }

        [Category(PropertyCategory.Appearance)]
        [Description(PropertyDescription.Font)]
        public Font MenuFont
        {
            get
            {
                return contextMenuFont;
            }

            set
            {
                contextMenuFont = value;
                Invalidate();
            }
        }

        [Category(PropertyCategory.Appearance)]
        [Description(PropertyDescription.Color)]
        public Color SelectedItemBackColor
        {
            get
            {
                return _selectedItemBackColor;
            }

            set
            {
                _selectedItemBackColor = value;
            }
        }

        [Category(PropertyCategory.Appearance)]
        [Description(PropertyDescription.Color)]
        public Color TextDisabledColor
        {
            get
            {
                return textDisabledColor;
            }

            set
            {
                textDisabledColor = value;
                Invalidate();
            }
        }

        [Category(PropertyCategory.Appearance)]
        [Description(PropertyDescription.Color)]
        public Color TextHoverColor
        {
            get
            {
                return _textHoverColor;
            }

            set
            {
                _textHoverColor = value;
            }
        }

        #endregion Public Properties

        #region Methods

        protected override void OnItemClicked(ToolStripItemClickedEventArgs e)
        {
            if ((e.ClickedItem != null) && !(e.ClickedItem is ToolStripSeparator))
            {
                if (ReferenceEquals(e, _toolStripItemClickedEventArgs))
                {
                    OnItemClicked(e);
                }
                else
                {
                    _toolStripItemClickedEventArgs = e;
                    Clicked?.Invoke(this);
                }
            }

            base.OnItemClicked(e);
        }

        protected override void OnMouseHover(EventArgs e)
        {
            base.OnMouseHover(e);
            Invalidate();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            Invalidate();
        }

        private void ConfigureStyleManager()
        {
            border = new Border { HoverVisible = false, Type = ShapeTypes.Rectangle };

            // Font = styleManager.Theme.ColorPalette.Font;
            foreColor = styleManager.Theme.ColorPalette.TextEnabled;
            textDisabledColor = styleManager.Theme.ColorPalette.TextDisabled;

            BackColor = _backgroundColor;
            arrowColor = styleManager.Theme.ColorPalette.ElementEnabled;
            arrowDisabledColor = styleManager.Theme.ColorPalette.ElementDisabled;
            contextMenuFont = Font;

            _backgroundColor = styleManager.Theme.ColorPalette.ControlEnabled;
            _selectedItemBackColor = styleManager.Theme.ColorPalette.ItemHover;
            _textHoverColor = styleManager.Theme.ColorPalette.TextHover;
        }

        #endregion Methods

        public sealed class VisualToolStripRender : ToolStripProfessionalRenderer
        {
            #region Methods

            /// <summary>Renders the arrow.</summary>
            /// <param name="e">The tool strip render event args.</param>
            protected override void OnRenderArrow(ToolStripArrowRenderEventArgs e)
            {
                if (!_arrowVisible)
                {
                    return;
                }

                int _xArrow = e.Item.ContentRectangle.X + e.Item.ContentRectangle.Width;
                int _yArrow = (e.ArrowRectangle.Y + e.ArrowRectangle.Height) / 2;

                Point[] _arrowLocation = { new Point(_xArrow - 5, _yArrow - 5), new Point(_xArrow, _yArrow), new Point(_xArrow - 5, _yArrow + 5) };

                Color _arrowStateColor = e.Item.Enabled ? arrowColor : arrowDisabledColor;
                e.Graphics.FillPolygon(new SolidBrush(_arrowStateColor), _arrowLocation);
            }

            /// <summary>Renders the image margin.</summary>
            /// <param name="e">The tool strip render event args.</param>
            protected override void OnRenderImageMargin(ToolStripRenderEventArgs e)
            {
                // Allow to add images to ToolStrips
                // MyBase.OnRenderImageMargin(e)
            }

            /// <summary>Renders the item text.</summary>
            /// <param name="e">The tool strip render event args.</param>
            protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
            {
                e.Graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
                Rectangle _itemTextRectangle = new Rectangle(25, e.Item.ContentRectangle.Y, e.Item.ContentRectangle.Width - (24 + 16), e.Item.ContentRectangle.Height - 4);

                Color _itemTextColor = e.Item.Enabled ? e.Item.Selected ? _textHoverColor : foreColor : textDisabledColor;

                StringFormat _stringFormat = new StringFormat
                {
                    // Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center
                };

                e.Graphics.DrawString(e.Text, contextMenuFont, new SolidBrush(_itemTextColor), _itemTextRectangle, _stringFormat);
            }

            /// <summary>Renders the menu item background.</summary>
            /// <param name="e">The tool strip render event args.</param>
            protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
            {
                e.Graphics.InterpolationMode = InterpolationMode.High;
                e.Graphics.Clear(_backgroundColor);

                Rectangle _menuItemBackgroundRectangle = new Rectangle(0, e.Item.ContentRectangle.Y - 2, e.Item.ContentRectangle.Width + 4, e.Item.ContentRectangle.Height + 3);

                e.Graphics.FillRectangle(e.Item.Selected && e.Item.Enabled ? new SolidBrush(_selectedItemBackColor) : new SolidBrush(_backgroundColor), _menuItemBackgroundRectangle);
            }

            /// <summary>Renders the separator.</summary>
            /// <param name="e">The tool strip render event args.</param>
            protected override void OnRenderSeparator(ToolStripSeparatorRenderEventArgs e)
            {
                Point _pt1 = new Point(e.Item.Bounds.Left, e.Item.Bounds.Height / 2);
                Point _pt2 = new Point(e.Item.Bounds.Right - 5, e.Item.Bounds.Height / 2);

                e.Graphics.DrawLine(new Pen(Color.FromArgb(200, border.Color), border.Thickness), _pt1, _pt2);
            }

            /// <summary>Renders the tool strip background.</summary>
            /// <param name="e">The tool strip render event args.</param>
            protected override void OnRenderToolStripBackground(ToolStripRenderEventArgs e)
            {
                base.OnRenderToolStripBackground(e);
                e.Graphics.Clear(_backgroundColor);
            }

            /// <summary>Renders the tool strip border.</summary>
            /// <param name="e">The tool strip render event args.</param>
            protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e)
            {
                if (border.Visible)
                {
                    e.Graphics.InterpolationMode = InterpolationMode.High;
                    e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

                    Rectangle borderRectangle = new Rectangle(e.AffectedBounds.X, e.AffectedBounds.Y, e.AffectedBounds.Width - border.Thickness - 1, e.AffectedBounds.Height - border.Thickness - 1);
                    GraphicsPath borderPath = new GraphicsPath();
                    borderPath.AddRectangle(borderRectangle);
                    borderPath.CloseAllFigures();

                    e.Graphics.SetClip(borderPath);
                    e.Graphics.DrawPath(new Pen(border.Color), borderPath);
                    e.Graphics.ResetClip();
                }
            }

            #endregion Methods
        }
    }
}