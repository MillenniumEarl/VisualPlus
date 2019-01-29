#region License

// -----------------------------------------------------------------------------------------------------------
// 
// Name: VisualRadialProgress.cs
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
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Forms;

using VisualPlus.Constants;
using VisualPlus.Designer;
using VisualPlus.Events;
using VisualPlus.Interfaces;
using VisualPlus.Localization;
using VisualPlus.Managers;
using VisualPlus.Structure;
using VisualPlus.Toolkit.VisualBase;

#endregion

namespace VisualPlus.Toolkit.Controls.DataVisualization
{
    [ClassInterface(ClassInterfaceType.AutoDispatch)]
    [ComVisible(true)]
    [DefaultEvent("Click")]
    [DefaultProperty("Value")]
    [Description("The Visual Radial Progress")]
    [Designer(typeof(VisualRadialProgressDesigner))]
    [ToolboxBitmap(typeof(VisualRadialProgress), "VisualRadialProgress.bmp")]
    [ToolboxItem(true)]
    public class VisualRadialProgress : ProgressBase, IThemeSupport
    {
        #region Fields

        private Color _backCircleColor;
        private bool _backCircleVisible;
        private Color _foreCircleColor;
        private bool _foreCircleVisible;
        private Image _image;
        private Point _imageLocation;
        private Size _imageSize;
        private LineCap _lineCap;
        private Color _progressColor;
        private float _progressSize;
        private Color _subscriptColor;
        private Font _subscriptFont;
        private Point _subscriptLocation;
        private string _subscriptText;
        private Color _superscriptColor;
        private Font _superscriptFont;
        private Point _superscriptLocation;
        private string _superscriptText;
        private bool _textVisible;

        #endregion

        #region Constructors and Destructors

        /// <summary>Initializes a new instance of the <see cref="VisualRadialProgress" /> class.</summary>
        public VisualRadialProgress()
        {
            _backCircleVisible = true;
            _foreCircleVisible = true;
            _imageSize = new Size(16, 16);
            _lineCap = LineCap.Round;
            _progressSize = 5F;

            _superscriptLocation = new Point(70, 35);
            _subscriptLocation = new Point(70, 50);
            _superscriptText = "°C";
            _subscriptText = ".25";

            Size = new Size(100, 100);
            MinimumSize = Size;
            Maximum = 100;

            UpdateTheme(ThemeManager.Theme);
        }

        #endregion

        #region Public Properties

        [Category(PropertyCategory.Appearance)]
        [Description(PropertyDescription.Color)]
        public Color BackCircleColor
        {
            get
            {
                return _backCircleColor;
            }

            set
            {
                _backCircleColor = value;
                Invalidate();
            }
        }

        [DefaultValue(true)]
        [Category(PropertyCategory.Appearance)]
        [Description(PropertyDescription.Visible)]
        public bool BackCircleVisible
        {
            get
            {
                return _backCircleVisible;
            }

            set
            {
                _backCircleVisible = value;
                Invalidate();
            }
        }

        [Category(PropertyCategory.Appearance)]
        [Description(PropertyDescription.Color)]
        public Color ForeCircle
        {
            get
            {
                return _foreCircleColor;
            }

            set
            {
                _foreCircleColor = value;
                Invalidate();
            }
        }

        [DefaultValue(true)]
        [Category(PropertyCategory.Appearance)]
        [Description(PropertyDescription.Visible)]
        public bool ForeCircleVisible
        {
            get
            {
                return _foreCircleVisible;
            }

            set
            {
                _foreCircleVisible = value;
                Invalidate();
            }
        }

        [Category(PropertyCategory.Appearance)]
        [Description(PropertyDescription.Image)]
        public Image Image
        {
            get
            {
                return _image;
            }

            set
            {
                _image = value;
                Invalidate();
            }
        }

        [Category(PropertyCategory.Appearance)]
        [Description(PropertyDescription.Image)]
        public Point ImageLocation
        {
            get
            {
                return _imageLocation;
            }

            set
            {
                _imageLocation = value;
                Invalidate();
            }
        }

        [Category(PropertyCategory.Layout)]
        [Description(PropertyDescription.Size)]
        public Size ImageSize
        {
            get
            {
                return _imageSize;
            }

            set
            {
                _imageSize = value;
                Invalidate();
            }
        }

        [Category(PropertyCategory.Appearance)]
        [Description(PropertyDescription.Type)]
        public LineCap LineCap
        {
            get
            {
                return _lineCap;
            }

            set
            {
                _lineCap = value;
                Invalidate();
            }
        }

        [DefaultValue(typeof(Color), "Green")]
        [Category(PropertyCategory.Appearance)]
        [Description(PropertyDescription.Color)]
        public Color ProgressColor
        {
            get
            {
                return _progressColor;
            }

            set
            {
                _progressColor = value;
                Invalidate();
            }
        }

        [DefaultValue(DefaultConstants.ProgressSize)]
        [Category(PropertyCategory.Layout)]
        [Description(PropertyDescription.Size)]
        public float ProgressSize
        {
            get
            {
                return _progressSize;
            }

            set
            {
                _progressSize = value;
                Invalidate();
            }
        }

        [DefaultValue(typeof(Color), "Black")]
        [Category(PropertyCategory.Appearance)]
        [Description(PropertyDescription.Color)]
        public Color SubscriptColor
        {
            get
            {
                return _subscriptColor;
            }

            set
            {
                _subscriptColor = value;
                Invalidate();
            }
        }

        [Category(PropertyCategory.Appearance)]
        [Description(PropertyDescription.Font)]
        public Font SubscriptFont
        {
            get
            {
                return _subscriptFont;
            }

            set
            {
                _subscriptFont = value;
                Invalidate();
            }
        }

        [Category(PropertyCategory.Layout)]
        [Description(PropertyDescription.Point)]
        public Point SubscriptLocation
        {
            get
            {
                return _subscriptLocation;
            }

            set
            {
                _subscriptLocation = value;
                Invalidate();
            }
        }

        [Category(PropertyCategory.Appearance)]
        [Description(PropertyDescription.Text)]
        public string SubscriptText
        {
            get
            {
                return _subscriptText;
            }

            set
            {
                _subscriptText = value;
                Invalidate();
            }
        }

        [DefaultValue(typeof(Color), "Black")]
        [Category(PropertyCategory.Appearance)]
        [Description(PropertyDescription.Color)]
        public Color SuperscriptColor
        {
            get
            {
                return _superscriptColor;
            }

            set
            {
                _superscriptColor = value;
                Invalidate();
            }
        }

        [Category(PropertyCategory.Appearance)]
        [Description(PropertyDescription.Font)]
        public Font SuperscriptFont
        {
            get
            {
                return _superscriptFont;
            }

            set
            {
                _superscriptFont = value;
                Invalidate();
            }
        }

        [Category(PropertyCategory.Layout)]
        [Description(PropertyDescription.Point)]
        public Point SuperscriptLocation
        {
            get
            {
                return _superscriptLocation;
            }

            set
            {
                _superscriptLocation = value;
                Invalidate();
            }
        }

        [Category(PropertyCategory.Appearance)]
        [Description(PropertyDescription.Text)]
        public string SuperscriptText
        {
            get
            {
                return _superscriptText;
            }

            set
            {
                _superscriptText = value;
                Invalidate();
            }
        }

        [Category(PropertyCategory.Appearance)]
        [Description(PropertyDescription.Visible)]
        public bool TextVisible
        {
            get
            {
                return _textVisible;
            }

            set
            {
                _textVisible = value;
                Invalidate();
            }
        }

        #endregion

        #region Public Methods and Operators

        public void UpdateTheme(Theme theme)
        {
            try
            {
                ForeColor = theme.ColorPalette.TextEnabled;
                TextStyle.Enabled = theme.ColorPalette.TextEnabled;
                TextStyle.Disabled = theme.ColorPalette.TextDisabled;

                // Font = theme.ColorPalette.Font; // TODO: 16F - Bold
                _subscriptFont = SystemFonts.DefaultFont; // TODO: - Bold
                _superscriptFont = SystemFonts.DefaultFont; // TODO: - Bold

                _superscriptColor = theme.ColorPalette.SuperscriptColor;
                _subscriptColor = theme.ColorPalette.SubscriptColor;

                _backCircleColor = theme.ColorPalette.BackCircle;
                _foreCircleColor = theme.ColorPalette.ForeCircle;
                _progressColor = theme.ColorPalette.Progress;
            }
            catch (Exception e)
            {
                ConsoleEx.WriteDebug(e);
            }

            Invalidate();
            OnThemeChanged(new ThemeEventArgs(theme));
        }

        #endregion

        #region Methods

        protected void DrawCircles(Graphics graphics)
        {
            if (_backCircleVisible)
            {
                graphics.FillEllipse(new SolidBrush(_backCircleColor), _progressSize, _progressSize, Width - _progressSize - 1, Height - _progressSize - 1);
            }

            using (Pen progressPen = new Pen(_progressColor, _progressSize))
            {
                progressPen.StartCap = _lineCap;
                progressPen.EndCap = _lineCap;
                graphics.DrawArc(progressPen, _progressSize + 2, _progressSize + 2, Width - (_progressSize * 2), Height - (_progressSize * 2), -90, (int)Math.Round((360.0 / Maximum) * Value));
            }

            if (_foreCircleVisible)
            {
                graphics.FillEllipse(new SolidBrush(_foreCircleColor), _progressSize + 4, _progressSize + 4, Width - _progressSize - 10, Height - _progressSize - 10);
            }
        }

        protected void DrawImage(Graphics graphics)
        {
            if (Image == null)
            {
                return;
            }

            Rectangle _imageRectangle;

            if (AutoSize)
            {
                _imageLocation = new Point((Width / 2) - (_imageSize.Width / 2), (Height / 2) - (_imageSize.Height / 2));
                _imageRectangle = new Rectangle(_imageLocation, _imageSize);
            }
            else
            {
                _imageRectangle = new Rectangle(_imageLocation, _imageSize);
            }

            graphics.DrawImage(Image, _imageRectangle);
        }

        protected void DrawText(Graphics graphics)
        {
            string _value = _textVisible ? Text : Value.ToString(string.Empty);

            Size _textSize = TextManager.MeasureText(_textVisible ? Text : Value.ToString("0"), Font, graphics);
            Point _textPoint = new Point((Width / 2) - (_textSize.Width / 2), (Height / 2) - (_textSize.Height / 2));
            StringFormat _stringFormat = new StringFormat(RightToLeft == RightToLeft.Yes ? StringFormatFlags.DirectionRightToLeft : 0) { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Near };

            if ((_subscriptText != string.Empty) || (_superscriptText != string.Empty))
            {
                if (_superscriptText != string.Empty)
                {
                    graphics.DrawString(_superscriptText, _superscriptFont, new SolidBrush(_superscriptColor), SuperscriptLocation, _stringFormat);
                }

                if (_subscriptText != string.Empty)
                {
                    graphics.DrawString(_subscriptText, _subscriptFont, new SolidBrush(_subscriptColor), SubscriptLocation, _stringFormat);
                }
            }

            graphics.DrawString(_value, Font, new SolidBrush(ForeColor), _textPoint);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics graphics = e.Graphics;
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            DrawCircles(graphics);
            DrawImage(graphics);
            DrawText(graphics);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            UpdateSize();
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            UpdateSize();
        }

        private void UpdateSize()
        {
            Size = new Size(Math.Max(Width, Height), Math.Max(Width, Height));
        }

        #endregion
    }
}