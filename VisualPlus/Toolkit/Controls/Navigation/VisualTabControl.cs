#region License

// -----------------------------------------------------------------------------------------------------------
// 
// Name: VisualTabControl.cs
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
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using System.Windows.Forms;

using VisualPlus.Collections.CollectionsEditor;
using VisualPlus.Constants;
using VisualPlus.Designer;
using VisualPlus.Enumerators;
using VisualPlus.Localization;
using VisualPlus.Managers;
using VisualPlus.Renders;
using VisualPlus.Structure;
using VisualPlus.Toolkit.Child;
using VisualPlus.Toolkit.Components;
using VisualPlus.Utilities.Debugging;

#endregion

namespace VisualPlus.Toolkit.Controls.Navigation
{
    [ToolboxItem(true)]
    [ToolboxBitmap(typeof(VisualTabControl), "VisualTabControl.bmp")]
    [DefaultEvent("SelectedIndexChanged")]
    [DefaultProperty("TabPages")]
    [Description("The Visual TabControl")]
    [Designer(typeof(VisualTabControlDesigner))]
    public class VisualTabControl : TabControl
    {
        #region Fields

        private TabAlignment _alignment;
        private Border _border;
        private Size _itemSize;
        private Point _mouseLocation;
        private MouseStates _mouseState;
        private TabAlignment _selectorAlignment;
        private int _selectorSpacing;
        private int _selectorThickness;
        private SelectorTypes _selectorTypes;
        private bool _selectorVisible;
        private Color _separator;
        private int _separatorSpacing;
        private float _separatorThickness;
        private bool _separatorVisible;
        private StyleManager _styleManager;
        private Color _tabMenu;
        private Color _tabSelector;
        private TextRenderingHint _textRendererHint;

        #endregion

        #region Constructors and Destructors

        /// <summary>Initializes a new instance of the <see cref="VisualTabControl" /> class.</summary>
        public VisualTabControl()
        {
            SetStyle(
                ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint |
                ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.SupportsTransparentBackColor,
                true);

            UpdateStyles();

            _styleManager = new StyleManager(DefaultConstants.DefaultStyle);

            _border = new Border { Type = ShapeTypes.Rectangle };

            _alignment = TabAlignment.Top;
            _selectorVisible = true;
            _selectorSpacing = 10;
            _selectorThickness = 5;
            _selectorAlignment = TabAlignment.Bottom;

            _itemSize = new Size(100, 25);
            _tabMenu = Color.FromArgb(55, 61, 73);
            _tabSelector = _styleManager.Theme.ColorPalette.ControlEnabled;
            _textRendererHint = DefaultConstants.TextRenderingHint;

            _separatorSpacing = 2;
            _separatorThickness = 2F;
            _separator = _styleManager.Theme.ColorPalette.BorderNormal;
            _selectorTypes = SelectorTypes.Arrow;

            Size = new Size(320, 160);
            DrawMode = TabDrawMode.OwnerDrawFixed;
            ItemSize = _itemSize;
        }

        #endregion

        #region Enums

        public enum SelectorTypes
        {
            /// <summary>The arrow.</summary>
            Arrow,

            /// <summary>The line.</summary>
            Line
        }

        #endregion

        #region Public Properties

        [Category(PropertyCategory.Appearance)]
        [Description(PropertyDescription.Alignment)]
        public new TabAlignment Alignment
        {
            get
            {
                return _alignment;
            }

            set
            {
                _alignment = value;
                base.Alignment = _alignment;

                // Resize tabs
                switch (_alignment)
                {
                    case TabAlignment.Top:
                    case TabAlignment.Bottom:
                        {
                            if (_itemSize.Width < _itemSize.Height)
                            {
                                ItemSize = new Size(_itemSize.Height, _itemSize.Width);
                            }

                            break;
                        }

                    case TabAlignment.Left:
                    case TabAlignment.Right:
                        {
                            if (_itemSize.Width > _itemSize.Height)
                            {
                                ItemSize = new Size(_itemSize.Height, _itemSize.Width);
                            }

                            break;
                        }

                    default:
                        {
                            throw new ArgumentOutOfRangeException();
                        }
                }

                UpdateArrowLocation();
                Invalidate();
            }
        }

        [Category(PropertyCategory.Appearance)]
        [Description(PropertyDescription.Size)]
        public new Size ItemSize
        {
            get
            {
                return _itemSize;
            }

            set
            {
                _itemSize = value;
                base.ItemSize = _itemSize;
                Invalidate();
            }
        }

        [Category(PropertyCategory.Appearance)]
        [Description(PropertyDescription.Alignment)]
        public TabAlignment SelectorAlignment
        {
            get
            {
                return _selectorAlignment;
            }

            set
            {
                _selectorAlignment = value;
                Invalidate();
            }
        }

        [Category(PropertyCategory.Layout)]
        [Description(PropertyDescription.Spacing)]
        public int SelectorSpacing
        {
            get
            {
                return _selectorSpacing;
            }

            set
            {
                _selectorSpacing = value;
                Invalidate();
            }
        }

        [Category(PropertyCategory.Layout)]
        [Description(PropertyDescription.Size)]
        public int SelectorThickness
        {
            get
            {
                return _selectorThickness;
            }

            set
            {
                _selectorThickness = value;
                Invalidate();
            }
        }

        [Category(PropertyCategory.Appearance)]
        [Description(PropertyDescription.Type)]
        public SelectorTypes SelectorType
        {
            get
            {
                return _selectorTypes;
            }

            set
            {
                _selectorTypes = value;
                Invalidate();
            }
        }

        [Category(PropertyCategory.Appearance)]
        [Description(PropertyDescription.Visible)]
        public bool SelectorVisible
        {
            get
            {
                return _selectorVisible;
            }

            set
            {
                _selectorVisible = value;
                Invalidate();
            }
        }

        [Category(PropertyCategory.Appearance)]
        [Description(PropertyDescription.Color)]
        public Color Separator
        {
            get
            {
                return _separator;
            }

            set
            {
                _separator = value;
                Invalidate();
            }
        }

        [Category(PropertyCategory.Layout)]
        [Description(PropertyDescription.Spacing)]
        public int SeparatorSpacing
        {
            get
            {
                return _separatorSpacing;
            }

            set
            {
                _separatorSpacing = value;
                Invalidate();
            }
        }

        [Category(PropertyCategory.Layout)]
        [Description(PropertyDescription.Size)]
        public float SeparatorThickness
        {
            get
            {
                return _separatorThickness;
            }

            set
            {
                _separatorThickness = value;
                Invalidate();
            }
        }

        [DefaultValue(false)]
        [Category(PropertyCategory.Behavior)]
        [Description(PropertyDescription.Visible)]
        public bool SeparatorVisible
        {
            get
            {
                return _separatorVisible;
            }

            set
            {
                _separatorVisible = value;
                Invalidate();
            }
        }

        [Category(PropertyCategory.Appearance)]
        [Description(PropertyDescription.MouseState)]
        public MouseStates State
        {
            get
            {
                return _mouseState;
            }

            set
            {
                _mouseState = value;
                Invalidate();
            }
        }

        [Category(PropertyCategory.Appearance)]
        [Description(PropertyDescription.Color)]
        public Color TabMenu
        {
            get
            {
                return _tabMenu;
            }

            set
            {
                _tabMenu = value;
                Invalidate();
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Editor(typeof(VisualTabPageCollectionEditor), typeof(UITypeEditor))]
        [MergableProperty(false)]
        public new TabPageCollection TabPages
        {
            get
            {
                return base.TabPages;
            }
        }

        [Category(PropertyCategory.Appearance)]
        [Description(PropertyDescription.Color)]
        public Color TabSelector
        {
            get
            {
                return _tabSelector;
            }

            set
            {
                _tabSelector = value;
                Invalidate();
            }
        }

        [Category(PropertyCategory.Appearance)]
        [Description(PropertyDescription.TextRenderingHint)]
        public TextRenderingHint TextRendering
        {
            get
            {
                return _textRendererHint;
            }

            set
            {
                _textRendererHint = value;
                Invalidate();
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>Swaps the tab pages.</summary>
        /// <param name="tabPage1">The tab page.</param>
        /// <param name="tabPage2">The other tab page.</param>
        public void SwapTabPages(TabPage tabPage1, TabPage tabPage2)
        {
            if (!TabPages.Contains(tabPage1) || !TabPages.Contains(tabPage2))
            {
                throw new ArgumentException(@"TabPages must be in the TabControls TabPageCollection.");
            }

            int _index1 = TabPages.IndexOf(tabPage1);
            int _index2 = TabPages.IndexOf(tabPage2);

            TabPages[_index1] = tabPage2;
            TabPages[_index2] = tabPage1;
        }

        #endregion

        #region Methods

        protected override void CreateHandle()
        {
            base.CreateHandle();

            Appearance = TabAppearance.Normal;
            DoubleBuffered = true;
            Font = SystemFonts.DefaultFont;
            MinimumSize = new Size(144, 85);
            SizeMode = TabSizeMode.Fixed;
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            State = MouseStates.Hover;
            Invalidate();
        }

        protected override void OnMouseHover(EventArgs e)
        {
            base.OnMouseHover(e);
            _mouseState = MouseStates.Hover;
            Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            State = MouseStates.Normal;
            if (TabPages.Cast<VisualTabPage>().Any(Tab => Tab.DisplayRectangle.Contains(_mouseLocation)))
            {
                Invalidate();
            }

            base.OnMouseLeave(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            _mouseLocation = e.Location;
            if (TabPages.Cast<VisualTabPage>().Any(Tab => Tab.DisplayRectangle.Contains(e.Location)))
            {
                Invalidate();
            }

            Invalidate();
            base.OnMouseMove(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics _graphics = e.Graphics;
            _graphics.SmoothingMode = SmoothingMode.HighQuality;
            _graphics.CompositingMode = CompositingMode.SourceOver;
            _graphics.CompositingQuality = CompositingQuality.Default;
            _graphics.InterpolationMode = InterpolationMode.Default;
            _graphics.PixelOffsetMode = PixelOffsetMode.Default;
            _graphics.SmoothingMode = SmoothingMode.HighQuality;
            _graphics.TextRenderingHint = _textRendererHint;

            VisualBackgroundRenderer.DrawBackground(_graphics, _tabMenu, BackgroundImage, _mouseState, ClientRectangle, _border);

            DrawTabPages(_graphics);
            DrawSeparator(_graphics);
            DrawSelector(_graphics, _selectorTypes);
        }

        /// <summary>Draws the header background.</summary>
        /// <param name="graphics">The specified graphics to draw on.</param>
        /// <param name="tabPage">The tab page.</param>
        /// <param name="selected">The selected toggle.</param>
        private static void DrawBackgroundHeader(Graphics graphics, VisualTabPage tabPage, bool selected)
        {
            Color _tabBackColor = selected ? tabPage.TabSelected : tabPage.TabNormal;

            graphics.FillRectangle(new SolidBrush(_tabBackColor), tabPage.Rectangle);

            if (tabPage.HeaderImage != null)
            {
                graphics.DrawImage(tabPage.HeaderImage, tabPage.Rectangle);
            }
        }

        /// <summary>Draws the headers border.</summary>
        /// <param name="graphics">The specified graphics to draw on.</param>
        /// <param name="tabPage">The tab page to draw.</param>
        private static void DrawBorderHeader(Graphics graphics, VisualTabPage tabPage)
        {
            if (!tabPage.Border.Visible)
            {
                return;
            }

            GraphicsPath _borderGraphicsPath = new GraphicsPath();
            _borderGraphicsPath.AddRectangle(tabPage.Rectangle);
            Pen _borderPen = new Pen(tabPage.Border.Color, tabPage.Border.Thickness);
            graphics.DrawPath(_borderPen, _borderGraphicsPath);
        }

        /// <summary>Draws the tab header content.</summary>
        /// <param name="graphics">The specified graphics to draw on.</param>
        /// <param name="tabPage">The tab page.</param>
        /// <param name="selected">The selected toggle.</param>
        private static void DrawContentHeader(Graphics graphics, VisualTabPage tabPage, bool selected)
        {
            Rectangle _imageRectangle = new Rectangle(tabPage.Rectangle.X + tabPage.Border.Thickness + 1, (tabPage.Rectangle.Y + (tabPage.Rectangle.Height / 2)) - (tabPage.ImageSize.Height / 2), tabPage.ImageSize.Width, tabPage.ImageSize.Height);
            Rectangle _imageCenterRectangle = new Rectangle((tabPage.Rectangle.X + (tabPage.Rectangle.Width / 2)) - (tabPage.ImageSize.Width / 2), (tabPage.Rectangle.Y + (tabPage.Rectangle.Height / 2)) - (tabPage.ImageSize.Height / 2), tabPage.ImageSize.Width, tabPage.ImageSize.Height);

            StringFormat _tabStringFormat = new StringFormat { Alignment = tabPage.TextAlignment, LineAlignment = tabPage.TextLineAlignment };

            Size _textSize = TextManager.MeasureText(tabPage.Text, tabPage.Font, graphics);
            Color _foreColor = selected ? tabPage.TextSelected : tabPage.ForeColor;

            switch (tabPage.TextImageRelation)
            {
                case VisualTabPage.TextImageRelations.ImageBeforeText:
                    {
                        if (tabPage.Image != null)
                        {
                            graphics.DrawImage(tabPage.Image, _imageRectangle);
                        }

                        graphics.DrawString(tabPage.Text, tabPage.Font, new SolidBrush(_foreColor), new Rectangle(_imageRectangle.Right + 1, (tabPage.Rectangle.Y + (tabPage.Rectangle.Height / 2)) - (_textSize.Height / 2), tabPage.Rectangle.Width, tabPage.Rectangle.Height));

                        break;
                    }

                case VisualTabPage.TextImageRelations.Image:
                    {
                        if (tabPage.Image != null)
                        {
                            graphics.DrawImage(tabPage.Image, _imageCenterRectangle);
                        }

                        break;
                    }

                case VisualTabPage.TextImageRelations.Text:
                    {
                        graphics.DrawString(tabPage.Text, tabPage.Font, new SolidBrush(_foreColor), tabPage.Rectangle, _tabStringFormat);
                        break;
                    }

                default:
                    {
                        throw new ArgumentOutOfRangeException();
                    }
            }
        }

        /// <summary>Draws the selector.</summary>
        /// <param name="graphics">The specified graphics to draw on.</param>
        /// <param name="selectorType">The selector Type.</param>
        private void DrawSelector(Graphics graphics, SelectorTypes selectorType)
        {
            if (_selectorVisible)
            {
                try
                {
                    for (var tabIndex = 0; tabIndex <= TabCount - 1; tabIndex++)
                    {
                        VisualTabPage _tabPage = (VisualTabPage)TabPages[tabIndex];
                        _tabPage.Rectangle = GetStyledTabRectangle(tabIndex);

                        if (tabIndex == SelectedIndex)
                        {
                            switch (selectorType)
                            {
                                case SelectorTypes.Arrow:
                                    {
                                        DrawSelectorArrow(graphics, _tabPage.Rectangle);
                                        break;
                                    }

                                case SelectorTypes.Line:
                                    {
                                        DrawSelectorLine(graphics, _tabPage.Rectangle);
                                        break;
                                    }

                                default:
                                    {
                                        throw new ArgumentOutOfRangeException(nameof(selectorType), selectorType, null);
                                    }
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    ConsoleEx.WriteDebug(e);
                }
            }
        }

        /// <summary>Draws the selection arrow.</summary>
        /// <param name="graphics">The specified graphics to draw on.</param>
        /// <param name="rectangle">The rectangle.</param>
        private void DrawSelectorArrow(Graphics graphics, Rectangle rectangle)
        {
            var _arrow = new Point[3];

            switch (Alignment)
            {
                case TabAlignment.Left:
                    {
                        _arrow[0].X = rectangle.Right - SelectorThickness;
                        _arrow[0].Y = rectangle.Y + (rectangle.Height / 2);

                        _arrow[1].X = rectangle.Right + SelectorSpacing;
                        _arrow[1].Y = rectangle.Top + SelectorSpacing;

                        _arrow[2].X = rectangle.Right + SelectorSpacing;
                        _arrow[2].Y = rectangle.Bottom - SelectorSpacing;
                        break;
                    }

                case TabAlignment.Top:
                    {
                        _arrow[0].X = rectangle.X + (rectangle.Width / 2);
                        _arrow[0].Y = rectangle.Bottom - SelectorThickness;

                        _arrow[1].X = rectangle.Left + SelectorSpacing;
                        _arrow[1].Y = rectangle.Bottom + SelectorSpacing;

                        _arrow[2].X = rectangle.Right - SelectorSpacing;
                        _arrow[2].Y = rectangle.Bottom + SelectorSpacing;
                        break;
                    }

                case TabAlignment.Bottom:
                    {
                        _arrow[0].X = rectangle.X + (rectangle.Width / 2);
                        _arrow[0].Y = rectangle.Top + SelectorThickness;

                        _arrow[1].X = rectangle.Left + SelectorSpacing;
                        _arrow[1].Y = rectangle.Top - SelectorSpacing;

                        _arrow[2].X = rectangle.Right - SelectorSpacing;
                        _arrow[2].Y = rectangle.Top - SelectorSpacing;
                        break;
                    }

                case TabAlignment.Right:
                    {
                        _arrow[0].X = rectangle.Left + SelectorThickness;
                        _arrow[0].Y = rectangle.Y + (rectangle.Height / 2);

                        _arrow[1].X = rectangle.Left - SelectorSpacing;
                        _arrow[1].Y = rectangle.Top + SelectorSpacing;

                        _arrow[2].X = rectangle.Left - SelectorSpacing;
                        _arrow[2].Y = rectangle.Bottom - SelectorSpacing;
                        break;
                    }

                default:
                    {
                        throw new ArgumentOutOfRangeException();
                    }
            }

            graphics.FillPolygon(new SolidBrush(_tabSelector), _arrow);
        }

        /// <summary>Draws the selector line.</summary>
        /// <param name="graphics">The specified graphics to draw on.</param>
        /// <param name="rectangle">The rectangle.</param>
        private void DrawSelectorLine(Graphics graphics, Rectangle rectangle)
        {
            Rectangle selectorRectangle = GraphicsManager.ApplyAnchor(_selectorAlignment, rectangle, _selectorThickness);
            graphics.FillRectangle(new SolidBrush(_tabSelector), selectorRectangle);
        }

        /// <summary>Draws the separator.</summary>
        /// <param name="graphics">The specified graphics to draw on.</param>
        private void DrawSeparator(Graphics graphics)
        {
            if (!_separatorVisible)
            {
                return;
            }

            // Draw divider that separates the panels.
            switch (Alignment)
            {
                case TabAlignment.Top:
                    {
                        graphics.DrawLine(new Pen(_separator, _separatorThickness), 0, ItemSize.Height + _separatorSpacing, Width, ItemSize.Height + _separatorSpacing);
                        break;
                    }

                case TabAlignment.Bottom:
                    {
                        graphics.DrawLine(new Pen(_separator, _separatorThickness), 0, Height - ItemSize.Height - _separatorSpacing, Width, Height - ItemSize.Height - _separatorSpacing);
                        break;
                    }

                case TabAlignment.Left:
                    {
                        graphics.DrawLine(new Pen(_separator, _separatorThickness), ItemSize.Height + _separatorSpacing, 0, ItemSize.Height + _separatorSpacing, Height);
                        break;
                    }

                case TabAlignment.Right:
                    {
                        graphics.DrawLine(new Pen(_separator, _separatorThickness), Width - ItemSize.Height - _separatorSpacing, 0, Width - ItemSize.Height - _separatorSpacing, Height);
                        break;
                    }

                default:
                    {
                        throw new ArgumentOutOfRangeException();
                    }
            }
        }

        /// <summary>Draws the tab pages.</summary>
        /// <param name="graphics">The specified graphics to draw on.</param>
        private void DrawTabPages(Graphics graphics)
        {
            try
            {
                for (var tabIndex = 0; tabIndex <= TabCount - 1; tabIndex++)
                {
                    VisualTabPage _tabPage = (VisualTabPage)TabPages[tabIndex];
                    _tabPage.Rectangle = GetStyledTabRectangle(tabIndex);

                    if (tabIndex == SelectedIndex)
                    {
                        // Selected tab header
                        DrawBackgroundHeader(graphics, _tabPage, true);
                        DrawBorderHeader(graphics, _tabPage);
                        DrawContentHeader(graphics, _tabPage, true);
                    }
                    else
                    {
                        // Unselected tab header
                        DrawBackgroundHeader(graphics, _tabPage, false);

                        if ((_mouseState == MouseStates.Hover) && _tabPage.Rectangle.Contains(_mouseLocation))
                        {
                            graphics.FillRectangle(new SolidBrush(_tabPage.TabHover), _tabPage.Rectangle);
                        }

                        DrawBorderHeader(graphics, _tabPage);
                        DrawContentHeader(graphics, _tabPage, false);
                    }
                }
            }
            catch (Exception e)
            {
                ConsoleEx.WriteDebug(e);
            }
        }

        /// <summary>Retrieves the styled tab rectangle using the index.</summary>
        /// <param name="index">The index.</param>
        /// <returns>The <see cref="Rectangle" />.</returns>
        private Rectangle GetStyledTabRectangle(int index)
        {
            Rectangle _rectangle;

            if ((Alignment == TabAlignment.Top) && (Alignment == TabAlignment.Bottom))
            {
                // Top - Bottom
                _rectangle = new Rectangle(new Point(GetTabRect(index).Location.X, GetTabRect(index).Location.Y), new Size(GetTabRect(index).Width, GetTabRect(index).Height));
            }
            else
            {
                // Left - Right
                _rectangle = new Rectangle(new Point(GetTabRect(index).Location.X, GetTabRect(index).Location.Y), new Size(GetTabRect(index).Width, GetTabRect(index).Height));
            }

            return _rectangle;
        }

        /// <summary>Update the arrow location.</summary>
        private void UpdateArrowLocation()
        {
            switch (_alignment)
            {
                case TabAlignment.Top:
                case TabAlignment.Bottom:
                    {
                        _selectorThickness = 5;
                        _selectorSpacing = 10;
                        break;
                    }

                case TabAlignment.Left:
                case TabAlignment.Right:
                    {
                        _selectorThickness = 10;
                        _selectorSpacing = 3;
                        break;
                    }

                default:
                    {
                        throw new ArgumentOutOfRangeException();
                    }
            }
        }

        #endregion
    }
}