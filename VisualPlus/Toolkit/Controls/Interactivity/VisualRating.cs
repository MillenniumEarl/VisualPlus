#region License

// -----------------------------------------------------------------------------------------------------------
//
// Name: VisualRating.cs
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
using System.Runtime.InteropServices;
using System.Windows.Forms;

using VisualPlus.Designer;
using VisualPlus.Events;
using VisualPlus.Interfaces;
using VisualPlus.Localization;
using VisualPlus.Models;
using VisualPlus.Toolkit.VisualBase;
using VisualPlus.Utilities;
using VisualPlus.Utilities.Debugging;

#endregion Namespace

namespace VisualPlus.Toolkit.Controls.Interactivity
{
    [ClassInterface(ClassInterfaceType.AutoDispatch)]
    [ComVisible(true)]
    [DefaultEvent("RatingChanged")]
    [DefaultProperty("Value")]
    [Description("The Visual Rating")]
    [Designer(typeof(VisualRatingDesigner))]
    [ToolboxBitmap(typeof(VisualRating), "VisualRating.bmp")]
    [ToolboxItem(true)]
    public class VisualRating : VisualStyleBase, IThemeSupport
    {
        #region Fields

        private readonly BufferedGraphicsContext _bufferedContext;
        private BufferedGraphics _bufferedGraphics;
        private int _maximum;
        private float _mouseOverIndex;
        private StarType _ratingType;
        private bool _settingRating;
        private int _starSpacing;
        private int _starWidth;
        private bool _toggleHalfStar;
        private float _value;
        private float borderWidth;
        private float dullStroke;
        private Color starBorderColor;
        private Color starColor;
        private Color starDull;
        private Color starDullBorderColor;

        #endregion Fields

        #region Constructors and Destructors

        /// <summary>Initializes a new instance of the <see cref="VisualRating" /> class.</summary>
        public VisualRating()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer, true);
            _bufferedContext = BufferedGraphicsManager.Current;
            _toggleHalfStar = true;
            _maximum = 5;
            _mouseOverIndex = -1;
            _ratingType = StarType.Thick;
            _starSpacing = 1;

            borderWidth = 3f;
            dullStroke = 3f;

            _starWidth = 25;

            Size = new Size(200, 100);

            UpdateGraphicsBuffer();
            UpdateTheme(ThemeManager.Theme);
        }

        #endregion Constructors and Destructors

        #region Public Events

        [Description("Occurs when the star rating of the strip has changed (Typically by a click operation)")]
        public event EventHandler RatingChanged;

        [Description("Occurs when a different number of stars are illuminated (does not include mouseleave un-ilum)")]
        public event EventHandler StarsPanned;

        #endregion Public Events

        #region Enums

        public enum StarType
        {
            /// <summary>Default star.</summary>
            Default,

            /// <summary>Detailed star.</summary>
            Detailed,

            /// <summary>Thick star.</summary>
            Thick
        }

        #endregion Enums

        #region Public Properties

        [Description("The dull stroke width")]
        [Category(PropertyCategory.Appearance)]
        [DefaultValue(3)]
        public float DullStrokeWidth
        {
            get
            {
                return dullStroke;
            }

            set
            {
                if (dullStroke != value)
                {
                    dullStroke = value;
                    Invalidate();
                }
            }
        }

        [Description("The number of stars to display")]
        [Category(PropertyCategory.Appearance)]
        [DefaultValue(5)]
        public int Maximum
        {
            get
            {
                return _maximum;
            }

            set
            {
                bool changed = _maximum != value;
                _maximum = value;

                if (changed)
                {
                    UpdateSize();
                    Invalidate();
                }
            }
        }

        [Browsable(false)]
        public float MouseOverStarIndex
        {
            get
            {
                return _mouseOverIndex;
            }
        }

        /// <summary>
        ///     Gets or sets the preset appearance of the star
        /// </summary>
        [Description("The star style to use")]
        [Category(PropertyCategory.Appearance)]
        [DefaultValue(StarType.Thick)]
        public StarType RatingType
        {
            get
            {
                return _ratingType;
            }

            set
            {
                _ratingType = value;
                Invalidate();
            }
        }

        [Description("The color to use for the star borders when they are illuminated")]
        [Category(PropertyCategory.Appearance)]
        [DefaultValue(typeof(Color), "Gold")]
        public Color StarBorderColor
        {
            get
            {
                return starBorderColor;
            }

            set
            {
                starBorderColor = value;
                Invalidate();
            }
        }

        /// <summary>
        ///     Gets or sets the width of the border around the star (including the dull version)
        /// </summary>
        [Description("The width of the star border")]
        [Category(PropertyCategory.Appearance)]
        [DefaultValue(3f)]
        public float StarBorderWidth
        {
            get
            {
                return borderWidth;
            }

            set
            {
                borderWidth = value;
                dullStroke = value;
                UpdateSize();
                Invalidate();
            }
        }

        [Description("The color to use for the star when they are illuminated")]
        [Category(PropertyCategory.Appearance)]
        [DefaultValue(typeof(Color), "Yellow")]
        public Color StarColor
        {
            get
            {
                return starColor;
            }

            set
            {
                starColor = value;
                Invalidate();
            }
        }

        [Description("The color to use for the star borders when they are not illuminated")]
        [Category(PropertyCategory.Appearance)]
        [DefaultValue(typeof(Color), "Gray")]
        public Color StarDullBorderColor
        {
            get
            {
                return starDullBorderColor;
            }

            set
            {
                starDullBorderColor = value;
                Invalidate();
            }
        }

        [Description("The color to use for the stars when they are not illuminated")]
        [Category(PropertyCategory.Appearance)]
        [DefaultValue(typeof(Color), "Silver")]
        public Color StarDullColor
        {
            get
            {
                return starDull;
            }

            set
            {
                starDull = value;
                Invalidate();
            }
        }

        [Description("The amount of space between each star")]
        [Category(PropertyCategory.Layout)]
        [DefaultValue(1)]
        public int StarSpacing
        {
            get
            {
                return _starSpacing;
            }

            set
            {
                _starSpacing = _starSpacing < 0 ? 0 : value;
                UpdateSize();
                Invalidate();
            }
        }

        [Description("The width and height of the star in pixels (not including the border)")]
        [Category(PropertyCategory.Layout)]
        [DefaultValue(25)]
        public int StarWidth
        {
            get
            {
                return _starWidth;
            }

            set
            {
                _starWidth = _starWidth < 1 ? 1 : value;
                UpdateSize();
                Invalidate();
            }
        }

        [Description("Determines whether the user can rate with a half a star of specificity")]
        [Category(PropertyCategory.Behavior)]
        [DefaultValue(false)]
        public bool ToggleHalfStar
        {
            get
            {
                return _toggleHalfStar;
            }

            set
            {
                bool disabled = !value && _toggleHalfStar;
                _toggleHalfStar = value;

                if (disabled)
                {
                    // Only set rating if half star was enabled and now disabled
                    Value = (int)(Value + 0.5);
                }
            }
        }

        [Description("The number of stars selected (Note: 0 is considered un-rated")]
        [Category(PropertyCategory.Appearance)]
        [DefaultValue(0f)]
        public float Value
        {
            get
            {
                return _value;
            }

            set
            {
                if (value > _maximum)
                {
                    value = _maximum;
                }
                else if (value < 0)
                {
                    value = 0;
                }
                else
                {
                    if (_toggleHalfStar)
                    {
                        value = RoundToNearestHalf(value);
                    }
                    else
                    {
                        value = (int)(value + 0.5f);
                    }
                }

                bool changed = value != _value;
                _value = value;

                if (changed)
                {
                    if (!_settingRating)
                    {
                        _mouseOverIndex = _value;
                        if (!_toggleHalfStar)
                        {
                            _mouseOverIndex -= 1f;
                        }
                    }

                    OnRatingChanged();
                    Invalidate();
                }
            }
        }

        #endregion Public Properties

        #region Properties

        /// <summary>Gets all of the spacing between the stars.</summary>
        private int TotalSpacing
        {
            get
            {
                return (_maximum - 1) * _starSpacing;
            }
        }

        /// <summary>Gets the sum of all star widths.</summary>
        private int TotalStarWidth
        {
            get
            {
                return _maximum * _starWidth;
            }
        }

        /// <summary>Gets the sum of the width of the stroke for each star.</summary>
        private float TotalStrokeWidth
        {
            get
            {
                return _maximum * borderWidth;
            }
        }

        #endregion Properties

        #region Public Methods and Operators

        public void UpdateTheme(Theme theme)
        {
            try
            {
                // Star border color.
                starBorderColor = theme.ColorPalette.StarBorder;

                // Star color.
                starColor = theme.ColorPalette.Star;

                // Star dull border color
                starDullBorderColor = theme.ColorPalette.StarDullBorder;

                // Star dull color
                starDull = theme.ColorPalette.StarDull;
            }
            catch (Exception e)
            {
                Logger.WriteDebug(e);
            }

            Invalidate();
            OnThemeChanged(this, new ThemeEventArgs(theme));
        }

        #endregion Public Methods and Operators

        #region Methods

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);

            if (_value == 0f)
            {
                _settingRating = true;
                Value = _toggleHalfStar ? _mouseOverIndex : _mouseOverIndex + 1f;
                _settingRating = false;
                Invalidate();
            }
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            if (_value > 0)
            {
                return;
            }

            _mouseOverIndex = -1; // No stars will be highlighted
            Invalidate();
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (_value > 0)
            {
                return;
            }

            float index = GetHoveredStarIndex(e.Location);

            if (index != _mouseOverIndex)
            {
                _mouseOverIndex = index;
                OnStarsPanned();
                Invalidate();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            _bufferedGraphics.Graphics.Clear(BackColor);
            DrawDullStars();
            DrawIlluminatedStars();
            _bufferedGraphics.Render(e.Graphics);
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            UpdateSize();
            UpdateGraphicsBuffer();
        }

        /// <summary>Rounds precise numbers to a number no more precise than .5.</summary>
        /// <param name="f">The value.</param>
        /// <returns>Star shape.</returns>
        private static float RoundToNearestHalf(float f)
        {
            return (float)Math.Round(f / 5.0, 1) * 5f;
        }

        /// <summary>Draws the dull stars.</summary>
        private void DrawDullStars()
        {
            float height = Height - borderWidth;
            float lastX = borderWidth / 2f; // Start off at stroke size and increment
            float width = (Width - TotalSpacing - TotalStrokeWidth) / _maximum;

            Pen starDullStroke = new Pen(starDullBorderColor, dullStroke) { LineJoin = LineJoin.Round, Alignment = PenAlignment.Outset };

            // Draw stars
            for (var i = 0; i < _maximum; i++)
            {
                RectangleF rect = new RectangleF(lastX, borderWidth / 2f, width, height);
                var polygon = GetStarPolygon(rect);
                _bufferedGraphics.Graphics.FillPolygon(new SolidBrush(starDull), polygon);
                _bufferedGraphics.Graphics.DrawPolygon(starDullStroke, polygon);
                lastX += _starWidth + _starSpacing + borderWidth;
                _bufferedGraphics.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                _bufferedGraphics.Graphics.FillPolygon(new SolidBrush(starDull), polygon);
                _bufferedGraphics.Graphics.DrawPolygon(starDullStroke, polygon);
                _bufferedGraphics.Graphics.PixelOffsetMode = PixelOffsetMode.Default;
            }
        }

        /// <summary>Draws the illuminated stars.</summary>
        private void DrawIlluminatedStars()
        {
            float height = Height - borderWidth;
            float lastX = borderWidth / 2f; // Start off at stroke size and increment
            float width = (Width - TotalSpacing - TotalStrokeWidth) / _maximum;

            Pen _starStroke = new Pen(starBorderColor, borderWidth) { LineJoin = LineJoin.Round, Alignment = PenAlignment.Outset };

            if (_toggleHalfStar)
            {
                // Draw stars
                for (var i = 0; i < _maximum; i++)
                {
                    RectangleF rect = new RectangleF(lastX, borderWidth / 2f, width, height);

                    if (i < _mouseOverIndex - 0.5f)
                    {
                        var polygon = GetStarPolygon(rect);
                        _bufferedGraphics.Graphics.FillPolygon(new SolidBrush(starColor), polygon);
                        _bufferedGraphics.Graphics.DrawPolygon(_starStroke, polygon);
                    }
                    else if (i == _mouseOverIndex - 0.5f)
                    {
                        var polygon = GetSemiStarPolygon(rect);
                        _bufferedGraphics.Graphics.FillPolygon(new SolidBrush(starColor), polygon);
                        _bufferedGraphics.Graphics.DrawPolygon(_starStroke, polygon);
                    }
                    else
                    {
                        break;
                    }

                    lastX += _starWidth + _starSpacing + borderWidth;
                }
            }
            else
            {
                // Draw stars
                for (var i = 0; i < _maximum; i++)
                {
                    RectangleF rect = new RectangleF(lastX, borderWidth / 2f, width, height);
                    var polygon = GetStarPolygon(rect);

                    if (i <= _mouseOverIndex)
                    {
                        _bufferedGraphics.Graphics.FillPolygon(new SolidBrush(starColor), polygon);
                        _bufferedGraphics.Graphics.DrawPolygon(_starStroke, polygon);
                    }
                    else
                    {
                        break;
                    }

                    lastX += _starWidth + _starSpacing + borderWidth;
                }
            }
        }

        private float GetHoveredStarIndex(Point pos)
        {
            if (_toggleHalfStar)
            {
                float widthSection = Width / (float)_maximum / 2f;

                for (var i = 0f; i < _maximum; i += 0.5f)
                {
                    float starX = i * widthSection * 2f;

                    // If cursor is within the x region of the iterated star
                    if ((pos.X >= starX) && (pos.X <= starX + widthSection))
                    {
                        return i + 0.5f;
                    }
                }

                return -1;
            }
            else
            {
                var widthSection = (int)((Width / (double)_maximum) + 0.5);

                for (var i = 0; i < _maximum; i++)
                {
                    float starX = i * widthSection;

                    // If cursor is within the x region of the iterated star
                    if ((pos.X >= starX) && (pos.X <= starX + widthSection))
                    {
                        return i;
                    }
                }
            }

            return -1;
        }

        private PointF[] GetSemiStarPolygon(RectangleF rect)
        {
            switch (_ratingType)
            {
                case StarType.Default:
                    {
                        return ElementsManager.GenerateNormalSemiStar(rect);
                    }

                case StarType.Thick:
                    {
                        return ElementsManager.GenerateFatSemiStar(rect);
                    }

                case StarType.Detailed:
                    {
                        return ElementsManager.GenerateDetailedSemiStar(rect);
                    }

                default:
                    {
                        return null;
                    }
            }
        }

        private PointF[] GetStarPolygon(RectangleF rect)
        {
            switch (_ratingType)
            {
                case StarType.Default:
                    {
                        return ElementsManager.GenerateNormalStar(rect);
                    }

                case StarType.Thick:
                    {
                        return ElementsManager.GenerateFatStar(rect);
                    }

                case StarType.Detailed:
                    {
                        return ElementsManager.GenerateDetailedStar(rect);
                    }

                default:
                    {
                        return null;
                    }
            }
        }

        private void OnRatingChanged()
        {
            RatingChanged?.Invoke(this, EventArgs.Empty);
        }

        private void OnStarsPanned()
        {
            StarsPanned?.Invoke(this, EventArgs.Empty);
        }

        private void UpdateGraphicsBuffer()
        {
            if ((Width > 0) && (Height > 0))
            {
                _bufferedContext.MaximumBuffer = new Size(Width + 1, Height + 1);
                _bufferedGraphics = _bufferedContext.Allocate(CreateGraphics(), ClientRectangle);
                _bufferedGraphics.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            }
        }

        private void UpdateSize()
        {
            var height = (int)(_starWidth + borderWidth + 0.5);
            var width = (int)(TotalStarWidth + TotalSpacing + TotalStrokeWidth + 0.5);
            Size = new Size(width, height);
        }

        #endregion Methods
    }
}