#region License

// -----------------------------------------------------------------------------------------------------------
// 
// Name: VisualKnob.cs
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
using System.Drawing.Text;
using System.Windows.Forms;

using VisualPlus.Constants;
using VisualPlus.Enumerators;
using VisualPlus.Localization;
using VisualPlus.Models;
using VisualPlus.Renders;
using VisualPlus.Toolkit.Components;
using VisualPlus.TypeConverters;
using VisualPlus.Utilities;

#endregion

namespace VisualPlus.Toolkit.Controls.Interactivity
{
    [ToolboxItem(true)]
    [ToolboxBitmap(typeof(VisualKnob), "VisualKnob.bmp")]
    [DefaultEvent("Click")]
    [DefaultProperty("Value")]
    [Description("The Visual Knob")]
    public class VisualKnob : UserControl
    {
        #region Fields

        private int _buttonDivisions;
        private Container _components;
        private float _deltaAngle;
        private bool _drawDivInside;
        private float _drawRatio;
        private float _endAngle;
        private bool _focused;
        private Gradient _knob;
        private Border _knobBorder;
        private int _knobDistance;
        private Font _knobFont;
        private Point _knobPoint;
        private Rectangle _knobRectangle;
        private Size _knobSize;
        private Size _knobTickSize;
        private Gradient _knobTop;
        private Border _knobTopBorder;
        private Size _knobTopSize;
        private int _largeChange;
        private Size _lineSize;
        private int _maximum;
        private int _minimum;
        private MouseStates _mouseState;
        private int _mouseWheelBarPartitions;
        private Graphics _offGraphics;
        private Image _offScreenImage;
        private Color _pointerColor;
        private PointerStyle _pointerStyle;
        private bool _rotating;
        private Gradient _scale;
        private int _scaleDivisions;
        private int _scaleSubDivisions;
        private bool _showLargeScale;
        private bool _showSmallScale;
        private int _smallChange;
        private float _startAngle;
        private Color _tickColor;
        private int _value;
        private bool _valueVisible;

        #endregion

        #region Constructors and Destructors

        /// <summary>Initializes a new instance of the <see cref="VisualKnob" /> class.</summary>
        public VisualKnob()
        {
            SetStyle(
                ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer | ControlStyles.SupportsTransparentBackColor,
                true);

            StyleManager styleManager = new StyleManager(DefaultConstants.DefaultStyle);

            _pointerColor = styleManager.Theme.ColorPalette.Progress;

            UpdateStyles();
            _knobFont = Font;
            ForeColor = Color.DimGray;

            _buttonDivisions = 30;
            _components = null;
            _endAngle = 405;
            _knobDistance = 35;
            _knobSize = new Size(90, 90);
            _knobTickSize = new Size(86, 86);
            _knobTopSize = new Size(75, 75);
            _largeChange = 5;
            _lineSize = new Size(1, 1);
            _maximum = 100;
            _mouseWheelBarPartitions = 10;
            _pointerStyle = PointerStyle.Circle;
            _scaleDivisions = 11;
            _scaleSubDivisions = 4;
            _showLargeScale = true;
            _showSmallScale = true;
            _smallChange = 1;
            _startAngle = 135;
            _tickColor = Color.DimGray;
            _valueVisible = true;

            _knobBorder = new Border();
            _knobTopBorder = new Border { HoverVisible = false };

            InitializeGradient();
            InitializeComponent();

            // "start angle" and "end angle" possible values:

            // 90 = bottom (minimum value for "start angle")
            // 180 = left
            // 270 = top
            // 360 = right
            // 450 = bottom again (maximum value for "end angle")

            // So the couple (90, 450) will give an entire circle and the couple (180, 360) will give half a circle.
            _deltaAngle = _endAngle - _startAngle;
            ConfigureDimensions();
        }

        #endregion

        #region Delegates

        public delegate void ValueChangedEventHandler(object Sender);

        #endregion

        #region Public Events

        public event ValueChangedEventHandler ValueChanged;

        #endregion

        #region Enums

        public enum PointerStyle
        {
            /// <summary>The circle.</summary>
            Circle,

            /// <summary>The line.</summary>
            Line
        }

        #endregion

        #region Public Properties

        // [Description("Set the number of intervals between minimum and maximum")]
        // [Category(Localize.Category.Behavior)]
        // public int ButtonDivisions
        // {
        // get
        // {
        // return buttonDivisions;
        // }

        // set
        // {
        // buttonDivisions = value;
        // Invalidate();
        // }
        // }
        [Category(PropertyCategory.Behavior)]
        [Description("Draw graduation strings inside or outside the knob circle")]
        [DefaultValue(false)]
        public bool DrawDivInside
        {
            get
            {
                return _drawDivInside;
            }

            set
            {
                _drawDivInside = value;
                Invalidate();
            }
        }

        [Description("Set the end angle to display graduations (max 450)")]
        [Category(PropertyCategory.Behavior)]
        [DefaultValue(405)]
        public float EndAngle
        {
            get
            {
                return _endAngle;
            }

            set
            {
                if ((value <= 450) && (value > _startAngle))
                {
                    _endAngle = value;
                    _deltaAngle = _endAngle - _startAngle;
                    Invalidate();
                }
            }
        }

        [TypeConverter(typeof(VisualSettingsTypeConverter))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Category(PropertyCategory.Appearance)]
        public Gradient Knob
        {
            get
            {
                return _knob;
            }

            set
            {
                _knob = value;
                Invalidate();
            }
        }

        [TypeConverter(typeof(VisualSettingsTypeConverter))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Category(PropertyCategory.Appearance)]
        public Border KnobBorder
        {
            get
            {
                return _knobBorder;
            }

            set
            {
                _knobBorder = value;
                Invalidate();
            }
        }

        [Description(PropertyDescription.Size)]
        [Category(PropertyCategory.Layout)]
        public int KnobDistance
        {
            get
            {
                return _knobDistance;
            }

            set
            {
                _knobDistance = value;
                Invalidate();
            }
        }

        [Description(PropertyDescription.Size)]
        [Category(PropertyCategory.Layout)]
        public Size KnobSize
        {
            get
            {
                return _knobSize;
            }

            set
            {
                _knobSize = value;
                Invalidate();
            }
        }

        [Description("Set the style of the knob pointer: a circle or a line")]
        [Category(PropertyCategory.Appearance)]
        public PointerStyle KnobStyle
        {
            get
            {
                return _pointerStyle;
            }

            set
            {
                _pointerStyle = value;
                Invalidate();
            }
        }

        [Description(PropertyDescription.Size)]
        [Category(PropertyCategory.Layout)]
        public Size KnobTickSize
        {
            get
            {
                return _knobTickSize;
            }

            set
            {
                _knobTickSize = value;
                Invalidate();
            }
        }

        [TypeConverter(typeof(VisualSettingsTypeConverter))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Category(PropertyCategory.Appearance)]
        public Gradient KnobTop
        {
            get
            {
                return _knobTop;
            }

            set
            {
                _knobTop = value;
                Invalidate();
            }
        }

        [TypeConverter(typeof(VisualSettingsTypeConverter))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Category(PropertyCategory.Appearance)]
        public Border KnobTopBorder
        {
            get
            {
                return _knobTopBorder;
            }

            set
            {
                _knobTopBorder = value;
                Invalidate();
            }
        }

        [Description(PropertyDescription.Size)]
        [Category(PropertyCategory.Layout)]
        public Size KnobTopSize
        {
            get
            {
                return _knobTopSize;
            }

            set
            {
                _knobTopSize = value;
                Invalidate();
            }
        }

        [Description("set the value for the large changes")]
        [Category(PropertyCategory.Behavior)]
        public int LargeChange
        {
            get
            {
                return _largeChange;
            }

            set
            {
                _largeChange = value;
                Invalidate();
            }
        }

        [Description("set the maximum value for the knob control")]
        [Category(PropertyCategory.Behavior)]
        public int Maximum
        {
            get
            {
                return _maximum;
            }

            set
            {
                if (value > _minimum)
                {
                    _maximum = value;

                    if ((_scaleSubDivisions > 0) && (_scaleDivisions > 0) && ((_maximum - _minimum) / (_scaleSubDivisions * _scaleDivisions) <= 0))
                    {
                        _showSmallScale = false;
                    }

                    ConfigureDimensions();
                    Invalidate();
                }
            }
        }

        [Description("set the minimum value for the knob control")]
        [Category(PropertyCategory.Behavior)]
        public int Minimum
        {
            get
            {
                return _minimum;
            }

            set
            {
                _minimum = value;
                Invalidate();
            }
        }

        [Description("Set to how many parts is bar divided when using mouse wheel")]
        [Category(PropertyCategory.Behavior)]
        [DefaultValue(10)]
        public int MouseWheelBarPartitions
        {
            get
            {
                return _mouseWheelBarPartitions;
            }

            set
            {
                if (value > 0)
                {
                    _mouseWheelBarPartitions = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("MouseWheelBarPartitions has to be greather than zero");
                }
            }
        }

        [Description(PropertyDescription.Color)]
        [Category(PropertyCategory.Appearance)]
        public Color PointerColor
        {
            get
            {
                return _pointerColor;
            }

            set
            {
                _pointerColor = value;
                Invalidate();
            }
        }

        [Description("Set the number of intervals between minimum and maximum")]
        [Category(PropertyCategory.Behavior)]
        public int ScaleDivisions
        {
            get
            {
                return _scaleDivisions;
            }

            set
            {
                _scaleDivisions = value;
                Invalidate();
            }
        }

        [TypeConverter(typeof(VisualSettingsTypeConverter))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Category(PropertyCategory.Appearance)]
        public Gradient ScaleGradient
        {
            get
            {
                return _scale;
            }

            set
            {
                _scale = value;
                Invalidate();
            }
        }

        [Description("Set the number of subdivisions between main divisions of graduation.")]
        [Category(PropertyCategory.Behavior)]
        public int ScaleSubDivisions
        {
            get
            {
                return _scaleSubDivisions;
            }

            set
            {
                if ((value > 0) && (_scaleDivisions > 0) && ((_maximum - _minimum) / (value * _scaleDivisions) > 0))
                {
                    _scaleSubDivisions = value;
                    Invalidate();
                }
            }
        }

        [Description("Show or hide graduations")]
        [Category(PropertyCategory.Behavior)]
        public bool ShowLargeScale
        {
            get
            {
                return _showLargeScale;
            }

            set
            {
                _showLargeScale = value;

                // need to redraw
                ConfigureDimensions();

                Invalidate();
            }
        }

        [Description("Show or hide subdivisions of graduations")]
        [Category(PropertyCategory.Behavior)]
        public bool ShowSmallScale
        {
            get
            {
                return _showSmallScale;
            }

            set
            {
                if (value)
                {
                    if ((_scaleDivisions > 0) && (_scaleSubDivisions > 0) && ((_maximum - _minimum) / (_scaleSubDivisions * _scaleDivisions) > 0))
                    {
                        _showSmallScale = value;
                        Invalidate();
                    }
                }
                else
                {
                    _showSmallScale = value;

                    // need to redraw 
                    Invalidate();
                }
            }
        }

        [Description("set the minimum value for the small changes")]
        [Category(PropertyCategory.Behavior)]
        public int SmallChange
        {
            get
            {
                return _smallChange;
            }

            set
            {
                _smallChange = value;
                Invalidate();
            }
        }

        [Description("Set the start angle to display graduations (min 90)")]
        [Category(PropertyCategory.Behavior)]
        [DefaultValue(135)]
        public float StartAngle
        {
            get
            {
                return _startAngle;
            }

            set
            {
                if ((value >= 90) && (value < _endAngle))
                {
                    _startAngle = value;
                    _deltaAngle = _endAngle - StartAngle;
                    Invalidate();
                }
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

        [Description(PropertyDescription.Color)]
        [Category(PropertyCategory.Behavior)]
        public Color TickColor
        {
            get
            {
                return _tickColor;
            }

            set
            {
                _tickColor = value;
                Invalidate();
            }
        }

        [Description("set the current value of the knob control")]
        [Category(PropertyCategory.Behavior)]
        public int Value
        {
            get
            {
                return _value;
            }

            set
            {
                _value = value;

                // need to redraw 
                Invalidate();

                // call delegate  
                OnValueChanged(this);
            }
        }

        [Description("Displays the value text")]
        [Category(PropertyCategory.Behavior)]
        public bool ValueVisible
        {
            get
            {
                return _valueVisible;
            }

            set
            {
                _valueVisible = value;
                Invalidate();
            }
        }

        #endregion

        #region Methods

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

        protected override bool IsInputKey(Keys key)
        {
            switch (key)
            {
                case Keys.Up:
                case Keys.Down:
                case Keys.Right:
                case Keys.Left:
                    return true;
            }

            return base.IsInputKey(key);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (_focused)
            {
                // --------------------------------------------------------
                // Handles knob rotation with up,down,left and right keys 
                // --------------------------------------------------------
                if ((e.KeyCode == Keys.Up) || (e.KeyCode == Keys.Right))
                {
                    if (_value < _maximum)
                    {
                        Value = _value + 1;
                    }

                    Refresh();
                }
                else if ((e.KeyCode == Keys.Down) || (e.KeyCode == Keys.Left))
                {
                    if (_value > _minimum)
                    {
                        Value = _value - 1;
                    }

                    Refresh();
                }
            }
        }

        protected override void OnLeave(EventArgs e)
        {
            // unselect the control (remove dotted border)
            _focused = false;
            _rotating = false;
            Invalidate();

            base.OnLeave(new EventArgs());
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (MouseManager.IsMouseInBounds(e.Location, _knobRectangle))
            {
                if (_focused)
                {
                    // was already selected
                    // Start Rotation of knob only if it was selected before        
                    _rotating = true;
                }
                else
                {
                    // Was not selected before => select it
                    Focus();
                    _focused = true;
                    _rotating = false; // disallow rotation, must click again

                    // draw dotted border to show that it is selected
                    Invalidate();
                }
            }
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            State = MouseStates.Hover;
            Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            State = MouseStates.Normal;
            Invalidate();
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            // --------------------------------------
            // Following Handles Knob Rotating     
            // --------------------------------------
            if ((e.Button == MouseButtons.Left) && _rotating)
            {
                Cursor = Cursors.Hand;
                Point p = new Point(e.X, e.Y);
                int posVal = GetValueFromPosition(p);
                Value = posVal;
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (MouseManager.IsMouseInBounds(e.Location, _knobRectangle))
            {
                if (_focused && _rotating)
                {
                    // change value is allowed only only after 2nd click                   
                    Value = GetValueFromPosition(new Point(e.X, e.Y));
                }
                else
                {
                    // 1st click = only focus
                    _focused = true;
                    _rotating = true;
                }
            }

            Cursor = Cursors.Default;
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);

            if (_focused && _rotating && MouseManager.IsMouseInBounds(e.Location, _knobRectangle))
            {
                // the Delta value is always 120, as explained in MSDN
                int v = ((e.Delta / 120) * (_maximum - _minimum)) / _mouseWheelBarPartitions;
                SetValue(Value + v);

                // Avoid to send MouseWheel event to the parent container
                ((HandledMouseEventArgs)e).Handled = true;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            graphics.CompositingMode = CompositingMode.SourceOver;
            graphics.CompositingQuality = CompositingQuality.Default;
            graphics.InterpolationMode = InterpolationMode.Default;
            graphics.PixelOffsetMode = PixelOffsetMode.Default;
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            graphics.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;

            _offGraphics = graphics;
            _offGraphics.Clear(BackColor);

            DrawScale();
            DrawKnob();
            DrawKnobTop();
            DrawPointer(_offGraphics);
            DrawDivisions(_offGraphics, _knobRectangle);

            graphics.DrawImage(_offScreenImage, 0, 0);

            if (_valueVisible)
            {
                string value = _value.ToString("0");
                Size textAreaSize = TextManager.MeasureText(value, Font, e.Graphics);
                graphics.DrawString(value, Font, new SolidBrush(ForeColor), (Width / 2) - (textAreaSize.Width / 2), (Height / 2) - (textAreaSize.Height / 2));
            }
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            // Empty To avoid flickering due do background Drawing.
        }

        private void ConfigureDimensions()
        {
            int size = Width;
            Height = size;

            // Rectangle
            float x, y, w, h;
            x = 0;
            y = 0;
            w = Size.Width;
            h = Size.Height;

            // Calculate ratio
            _drawRatio = Math.Min(w, h) / 200;
            if (_drawRatio == 0.0)
            {
                _drawRatio = 1;
            }

            if (_showLargeScale)
            {
                float fSize = 6F * _drawRatio;
                if (fSize < 6)
                {
                    fSize = 6;
                }

                _knobFont = new Font(Font.FontFamily, fSize);
                double val = _maximum;
                string str = string.Format("{0,0:D}", (int)val);

                Graphics Gr = CreateGraphics();
                SizeF strsize = Gr.MeasureString(str, _knobFont);
                int strw = (int)strsize.Width + 4;
                var strh = (int)strsize.Height;

                // allow 10% gap on all side to determine size of knob    
                // this.rKnob = new Rectangle((int)(size * 0.10), (int)(size * 0.15), (int)(size * 0.80), (int)(size * 0.80));
                x = strw;

                // y = x;
                y = 2 * strh;
                w = size - (2 * strw);
                if (w <= 0)
                {
                    w = 1;
                }

                h = w;
                _knobRectangle = new Rectangle((int)x, (int)y, (int)w, (int)h);
                Gr.Dispose();
            }
            else
            {
                // rKnob = new Rectangle(0, 0, Width, Height);
            }

            // Center of knob
            _knobPoint = new Point(_knobRectangle.X + (_knobRectangle.Width / 2), _knobRectangle.Y + (_knobRectangle.Height / 2));

            _offScreenImage = new Bitmap(Width, Height);
            _offGraphics = Graphics.FromImage(_offScreenImage);
        }

        private bool DrawDivisions(Graphics graphics, RectangleF rectangleF)
        {
            if (this == null)
            {
                return false;
            }

            float cx = _knobPoint.X;
            float cy = _knobPoint.Y;

            float w = rectangleF.Width;
            float h = rectangleF.Height;

            float tx;
            float ty;

            float incr = MathManager.DegreeToRadians((_endAngle - _startAngle) / ((_scaleDivisions - 1) * (_scaleSubDivisions + 1)));
            float currentAngle = MathManager.DegreeToRadians(_startAngle);

            float radius = _knobRectangle.Width / 2;
            float rulerValue = _minimum;

            Pen penL = new Pen(TickColor, 2 * _drawRatio);
            Pen penS = new Pen(TickColor, 1 * _drawRatio);

            SolidBrush br = new SolidBrush(ForeColor);

            PointF ptStart = new PointF(0, 0);
            PointF ptEnd = new PointF(0, 0);
            var n = 0;

            if (_showLargeScale)
            {
                for (; n < _scaleDivisions; n++)
                {
                    // draw divisions
                    ptStart.X = (float)(cx + (radius * Math.Cos(currentAngle)));
                    ptStart.Y = (float)(cy + (radius * Math.Sin(currentAngle)));

                    ptEnd.X = (float)(cx + ((radius + (w / 50)) * Math.Cos(currentAngle)));
                    ptEnd.Y = (float)(cy + ((radius + (w / 50)) * Math.Sin(currentAngle)));

                    graphics.DrawLine(penL, ptStart, ptEnd);

                    // Draw graduations Strings                    
                    float fSize = 6F * _drawRatio;
                    if (fSize < 6)
                    {
                        fSize = 6;
                    }

                    Font font = new Font(Font.FontFamily, fSize);

                    double val = Math.Round(rulerValue);
                    string str = string.Format("{0,0:D}", (int)val);
                    SizeF size = graphics.MeasureString(str, font);

                    if (_drawDivInside)
                    {
                        // graduations strings inside the knob
                        tx = (float)(cx + ((radius - (11 * _drawRatio)) * Math.Cos(currentAngle)));
                        ty = (float)(cy + ((radius - (11 * _drawRatio)) * Math.Sin(currentAngle)));
                    }
                    else
                    {
                        // graduation strings outside the knob
                        tx = (float)(cx + ((radius + (11 * _drawRatio)) * Math.Cos(currentAngle)));
                        ty = (float)(cy + ((radius + (11 * _drawRatio)) * Math.Sin(currentAngle)));
                    }

                    graphics.DrawString(str,
                        font,
                        br,
                        tx - (float)(size.Width * 0.5),
                        ty - (float)(size.Height * 0.5));

                    rulerValue += (_maximum - _minimum) / (_scaleDivisions - 1);

                    if (n == _scaleDivisions - 1)
                    {
                        font.Dispose();
                        break;
                    }

                    // Subdivisions
                    if (_scaleDivisions <= 0)
                    {
                        currentAngle += incr;
                    }
                    else
                    {
                        for (var j = 0; j <= _scaleSubDivisions; j++)
                        {
                            currentAngle += incr;

                            // if user want to display small graduations
                            if (_showSmallScale)
                            {
                                ptStart.X = (float)(cx + (radius * Math.Cos(currentAngle)));
                                ptStart.Y = (float)(cy + (radius * Math.Sin(currentAngle)));
                                ptEnd.X = (float)(cx + ((radius + (w / 50)) * Math.Cos(currentAngle)));
                                ptEnd.Y = (float)(cy + ((radius + (w / 50)) * Math.Sin(currentAngle)));

                                graphics.DrawLine(penS, ptStart, ptEnd);
                            }
                        }
                    }

                    font.Dispose();
                }
            }

            return true;
        }

        private void DrawKnob()
        {
            Point knobPoint = new Point((_knobRectangle.X + (_knobRectangle.Width / 2)) - (KnobSize.Width / 2), (_knobRectangle.Y + (_knobRectangle.Height / 2)) - (KnobSize.Height / 2));
            Rectangle knobRectangle = new Rectangle(knobPoint, KnobSize);

            _offGraphics.FillEllipse(_knob.Brush, knobRectangle);

            GraphicsPath borderPath = new GraphicsPath();
            borderPath.AddEllipse(knobRectangle);
            VisualBorderRenderer.DrawBorderStyle(_offGraphics, _knobBorder, borderPath, State);
        }

        private void DrawKnobTop()
        {
            Point knobTopPoint = new Point((_knobRectangle.X + (_knobRectangle.Width / 2)) - (KnobTopSize.Width / 2), (_knobRectangle.Y + (_knobRectangle.Height / 2)) - (KnobTopSize.Height / 2));
            Rectangle knobTopRectangle = new Rectangle(knobTopPoint, KnobTopSize);

            _offGraphics.FillEllipse(_knobTop.Brush, knobTopRectangle);

            GraphicsPath borderPath = new GraphicsPath();
            borderPath.AddEllipse(knobTopRectangle);
            VisualBorderRenderer.DrawBorderStyle(_offGraphics, _knobTopBorder, borderPath, State);

            float cx = _knobPoint.X;
            float cy = _knobPoint.Y;

            float w = KnobTopSize.Width;

            // TODO: Adjust
            float incr = MathManager.DegreeToRadians((_startAngle - _endAngle) / ((_buttonDivisions - 1) * (_scaleSubDivisions + 1)));
            float currentAngle = MathManager.DegreeToRadians(0);

            float radius = KnobTickSize.Width / 2;

            Pen penL = new Pen(TickColor, 2 * _drawRatio);

            PointF ptStart = new PointF(0, 0);
            PointF ptEnd = new PointF(0, 0);
            var n = 0;

            for (; n < _buttonDivisions; n++)
            {
                // draw divisions
                ptStart.X = (float)(cx + (radius * Math.Cos(currentAngle)));
                ptStart.Y = (float)(cy + (radius * Math.Sin(currentAngle)));

                ptEnd.X = (float)(cx + ((radius + (w / 50)) * Math.Cos(currentAngle)));
                ptEnd.Y = (float)(cy + ((radius + (w / 50)) * Math.Sin(currentAngle)));

                // TODO: draw lines along button border
                // gOffScreen.DrawLine(penL, ptStart, ptEnd);

                // Draw graduations Strings                    
                float fSize = 6F * _drawRatio;
                if (fSize < 6)
                {
                    fSize = 6;
                }

                Font font = new Font(Font.FontFamily, fSize);

                if (n == _buttonDivisions - 1)
                {
                    font.Dispose();
                    break;
                }

                // Subdivisions
                if (_buttonDivisions <= 0)
                {
                    currentAngle += incr;
                }
                else
                {
                    for (var j = 0; j <= _scaleSubDivisions; j++)
                    {
                        currentAngle += incr;
                    }
                }

                font.Dispose();
            }
        }

        private void DrawPointer(Graphics graphics)
        {
            try
            {
                if (_pointerStyle == PointerStyle.Line)
                {
                    float radius = _knobRectangle.Width / 2;

                    int l = ((int)radius / 2) + _lineSize.Height;
                    int w = (l / 4) + _lineSize.Width;
                    var pt = GetKnobLine(l);

                    graphics.DrawLine(new Pen(_pointerColor, w), pt[0], pt[1]);
                }
                else
                {
                    int w;
                    int h;

                    w = _knobRectangle.Width / 10;
                    if (w < 7)
                    {
                        w = 7;
                    }

                    h = w;

                    Point Arrow = GetKnobPosition(w);
                    Rectangle rPointer = new Rectangle(Arrow.X - (w / 2), Arrow.Y - (w / 2), w, h);
                    graphics.FillEllipse(new SolidBrush(_pointerColor), rPointer);
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
        }

        private void DrawScale()
        {
            Size scaleSize = new Size(_knobRectangle.Width, _knobRectangle.Height);
            Point scalePoint = new Point(_knobRectangle.X, _knobRectangle.Y);
            Rectangle scaleRectangle = new Rectangle(scalePoint, scaleSize);

            _offGraphics.FillEllipse(_scale.Brush, scaleRectangle);
        }

        private Point[] GetKnobLine(int l)
        {
            var pret = new Point[2];

            float cx = _knobPoint.X;
            float cy = _knobPoint.Y;

            float radius = _knobRectangle.Width / 2;

            float degree = (_deltaAngle * Value) / (_maximum - _minimum);
            degree = MathManager.DegreeToRadians(degree + _startAngle);

            Point Pos = new Point(0, 0);

            Pos.X = (int)(cx + ((radius - (_drawRatio * 10)) * Math.Cos(degree)));
            Pos.Y = (int)(cy + ((radius - (_drawRatio * 10)) * Math.Sin(degree)));

            pret[0] = new Point(Pos.X, Pos.Y);

            Pos.X = (int)(cx + ((radius - (_drawRatio * 10) - l) * Math.Cos(degree)));
            Pos.Y = (int)(cy + ((radius - (_drawRatio * 10) - l) * Math.Sin(degree)));

            pret[1] = new Point(Pos.X, Pos.Y);

            return pret;
        }

        private Point GetKnobPosition(int l)
        {
            float cx = _knobPoint.X;
            float cy = _knobPoint.Y;

            float radius = _knobRectangle.Width / 2;

            float degree = (_deltaAngle * Value) / (_maximum - _minimum);
            degree = MathManager.DegreeToRadians(degree + _startAngle);

            Point Pos = new Point(0, 0) { X = (int)(cx + ((radius - (KnobDistance * _drawRatio)) * Math.Cos(degree))), Y = (int)(cy + ((radius - (KnobDistance * _drawRatio)) * Math.Sin(degree))) };

            return Pos;
        }

        private int GetValueFromPosition(Point point)
        {
            float degree = 0;
            var v = 0;

            if (point.X <= _knobPoint.X)
            {
                degree = (_knobPoint.Y - point.Y) / (float)(_knobPoint.X - point.X);
                degree = (float)Math.Atan(degree);

                degree = (degree * (float)(180 / Math.PI)) + (180 - _startAngle);
            }
            else if (point.X > _knobPoint.X)
            {
                degree = (point.Y - _knobPoint.Y) / (float)(point.X - _knobPoint.X);
                degree = (float)Math.Atan(degree);

                degree = ((degree * (float)(180 / Math.PI)) + 360) - _startAngle;
            }

            // round to the nearest value (when you click just before or after a graduation!)
            v = (int)Math.Round((degree * (_maximum - _minimum)) / _deltaAngle);

            if (v > _maximum)
            {
                v = _maximum;
            }

            if (v < _minimum)
            {
                v = _minimum;
            }

            return v;
        }

        private void InitializeComponent()
        {
            ImeMode = ImeMode.On;
            Name = "VisualKnob";
            Resize += KnobControl_Resize;
        }

        private void InitializeGradient()
        {
            float[] gradientPosition = { 0, 1 };

            Color[] knobColor = { Color.LightGray, Color.White };

            Color[] knobTopColor = { Color.White, Color.LightGray };

            Color[] scaleColor = { Color.LightGray, Color.White };

            _knob = new Gradient(180, knobColor, gradientPosition, ClientRectangle);
            _knobTop = new Gradient(knobTopColor, gradientPosition, ClientRectangle);
            _scale = new Gradient(scaleColor, gradientPosition, ClientRectangle);
        }

        private void KnobControl_Resize(object sender, EventArgs e)
        {
            ConfigureDimensions();

            // Refresh();
            Invalidate();
        }

        private void OnValueChanged(object sender)
        {
            if (ValueChanged != null)
            {
                ValueChanged(sender);
            }
        }

        private void SetValue(int value)
        {
            if (value < _minimum)
            {
                Value = _minimum;
            }
            else if (value > _maximum)
            {
                Value = _maximum;
            }
            else
            {
                Value = value;
            }
        }

        #endregion
    }
}