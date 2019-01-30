#region License

// -----------------------------------------------------------------------------------------------------------
// 
// Name: VisualProgressBar.cs
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
using VisualPlus.Localization;
using VisualPlus.Models;
using VisualPlus.Renders;
using VisualPlus.Toolkit.VisualBase;
using VisualPlus.TypeConverters;
using VisualPlus.Utilities.Debugging;

#endregion

namespace VisualPlus.Toolkit.Controls.DataVisualization
{
    [ClassInterface(ClassInterfaceType.AutoDispatch)]
    [ComVisible(true)]
    [DefaultEvent("Click")]
    [DefaultProperty("Value")]
    [Description("The Visual ProgressBar")]
    [Designer(typeof(VisualProgressBarDesigner))]
    [ToolboxBitmap(typeof(VisualProgressBar), "VisualProgressBar.bmp")]
    [ToolboxItem(true)]
    public class VisualProgressBar : ProgressBase
    {
        #region Fields

        private Border _border;
        private ColorState _colorState;
        private Hatch _hatch;
        private Timer _marqueeTimer;
        private bool _marqueeTimerEnabled;
        private int _marqueeWidth;
        private int _marqueeX;
        private int _marqueeY;
        private Orientation _orientation;
        private bool _percentageVisible;
        private ProgressBarStyle _progressBarStyle;
        private Color _progressColor;
        private Image _progressImage;
        private StringAlignment _valueAlignment;

        #endregion

        #region Constructors and Destructors

        /// <summary>Initializes a new instance of the <see cref="VisualProgressBar" /> class.</summary>
        public VisualProgressBar()
        {
            Maximum = 100;
            _hatch = new Hatch();
            _orientation = Orientation.Horizontal;
            _marqueeWidth = 20;
            Size = new Size(100, 20);
            _percentageVisible = true;
            _border = new Border();
            _progressBarStyle = ProgressBarStyle.Blocks;
            _valueAlignment = StringAlignment.Center;

            UpdateTheme(ThemeManager.Theme);
        }

        #endregion

        #region Public Properties

        [TypeConverter(typeof(VisualSettingsTypeConverter))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Category(PropertyCategory.Appearance)]
        public ColorState BackColorState
        {
            get
            {
                return _colorState;
            }

            set
            {
                if (value == _colorState)
                {
                    return;
                }

                _colorState = value;
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
                return _border;
            }

            set
            {
                _border = value;
                Invalidate();
            }
        }

        [TypeConverter(typeof(VisualSettingsTypeConverter))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Category(PropertyCategory.Behavior)]
        public Hatch Hatch
        {
            get
            {
                return _hatch;
            }

            set
            {
                _hatch = value;
                Invalidate();
            }
        }

        [Category(PropertyCategory.Layout)]
        [Description(PropertyDescription.Size)]
        public int MarqueeWidth
        {
            get
            {
                return _marqueeWidth;
            }

            set
            {
                _marqueeWidth = value;
                Invalidate();
            }
        }

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
                        int temp = Width;
                        Width = Height;
                        Height = temp;
                    }
                }
                else
                {
                    // Vertical
                    if (Width > Height)
                    {
                        int temp = Width;
                        Width = Height;
                        Height = temp;
                    }
                }

                // Resize check
                OnResize(EventArgs.Empty);

                Invalidate();
            }
        }

        [DefaultValue(DefaultConstants.TextVisible)]
        [Category(PropertyCategory.Appearance)]
        [Description(PropertyDescription.Visible)]
        public bool PercentageVisible
        {
            get
            {
                return _percentageVisible;
            }

            set
            {
                _percentageVisible = value;
                Invalidate();
            }
        }

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

        [Category(PropertyCategory.Appearance)]
        [Description(PropertyDescription.Image)]
        public Image ProgressImage
        {
            get
            {
                return _progressImage;
            }

            set
            {
                _progressImage = value;
                Invalidate();
            }
        }

        [DefaultValue(typeof(ProgressBarStyle))]
        [Category(PropertyCategory.Behavior)]
        [Description(PropertyDescription.ProgressBarStyle)]
        public ProgressBarStyle Style
        {
            get
            {
                return _progressBarStyle;
            }

            set
            {
                _progressBarStyle = value;
                Invalidate();
            }
        }

        [Category(PropertyCategory.Layout)]
        [Description(PropertyDescription.Alignment)]
        public StringAlignment ValueAlignment
        {
            get
            {
                return _valueAlignment;
            }

            set
            {
                _valueAlignment = value;
                Invalidate();
            }
        }

        #endregion

        #region Public Methods and Operators

        public void UpdateTheme(Theme theme)
        {
            try
            {
                _colorState = new ColorState { Enabled = theme.ColorPalette.ProgressBackground, Disabled = theme.ColorPalette.ProgressDisabled };

                _hatch.BackColor = Color.FromArgb(0, theme.ColorPalette.HatchBackColor);
                _hatch.ForeColor = Color.FromArgb(20, theme.ColorPalette.HatchForeColor);

                _progressColor = theme.ColorPalette.Progress;

                _border.Color = theme.ColorPalette.BorderNormal;
                _border.HoverColor = theme.ColorPalette.BorderHover;

                ForeColor = theme.ColorPalette.TextEnabled;
                TextStyle.Enabled = theme.ColorPalette.TextEnabled;
                TextStyle.Disabled = theme.ColorPalette.TextDisabled;

                // Font = theme.ColorPalette.Font;
                BackColorState.Enabled = theme.ColorPalette.Enabled;
                BackColorState.Disabled = theme.ColorPalette.Disabled;
            }
            catch (Exception e)
            {
                ConsoleEx.WriteDebug(e);
            }

            Invalidate();
            OnThemeChanged(this, new ThemeEventArgs(theme));
        }

        #endregion

        #region Methods

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics _graphics = e.Graphics;
            _graphics.SmoothingMode = SmoothingMode.HighQuality;
            _graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            _graphics.TextRenderingHint = TextStyle.TextRenderingHint;
            Rectangle _clientRectangle = new Rectangle(ClientRectangle.X, ClientRectangle.Y, ClientRectangle.Width - 1, ClientRectangle.Height - 1);

            ControlGraphicsPath = VisualBorderRenderer.CreateBorderTypePath(_clientRectangle, _border);

            _graphics.SetClip(ControlGraphicsPath);

            Color _backColor = Enabled ? BackColorState.Enabled : BackColorState.Disabled;
            VisualBackgroundRenderer.DrawBackground(_graphics, _backColor, BackgroundImage, MouseState, _clientRectangle, _border);

            DrawProgress(_orientation, _graphics);
            VisualBorderRenderer.DrawBorderStyle(e.Graphics, _border, ControlGraphicsPath, MouseState);
            e.Graphics.ResetClip();
        }

        private void DrawProgress(Orientation orientation, Graphics graphics)
        {
            if (_progressBarStyle == ProgressBarStyle.Marquee)
            {
                if (!DesignMode && Enabled)
                {
                    StartTimer();
                }

                if (!Enabled)
                {
                    StopTimer();
                }

                if (Value == Maximum)
                {
                    StopTimer();
                    DrawProgressContinuous(graphics);
                }
                else
                {
                    DrawProgressMarquee(graphics);
                }
            }
            else
            {
                int _indexValue;

                GraphicsPath _progressPath;
                Rectangle _progressRectangle;

                switch (orientation)
                {
                    case Orientation.Horizontal:
                        {
                            _indexValue = (int)Math.Round(((Value - Minimum) / (double)(Maximum - Minimum)) * (Width - 2));
                            _progressRectangle = new Rectangle(0, 0, _indexValue + _border.Thickness, Height);
                            _progressPath = VisualBorderRenderer.CreateBorderTypePath(_progressRectangle, _border);
                        }

                        break;
                    case Orientation.Vertical:
                        {
                            _indexValue = (int)Math.Round(((Value - Minimum) / (double)(Maximum - Minimum)) * (Height - 2));
                            _progressRectangle = new Rectangle(0, Height - _indexValue - _border.Thickness - 1, Width, _indexValue);
                            _progressPath = VisualBorderRenderer.CreateBorderTypePath(_progressRectangle, _border);
                        }

                        break;
                    default:
                        {
                            throw new ArgumentOutOfRangeException(nameof(orientation), orientation, null);
                        }
                }

                if (_indexValue > 1)
                {
                    graphics.SetClip(ControlGraphicsPath);
                    graphics.FillRectangle(new SolidBrush(_progressColor), _progressRectangle);
                    VisualProgressRenderer.RenderHatch(graphics, _hatch, _progressPath);
                    graphics.ResetClip();
                }
            }

            DrawText(graphics);
        }

        private void DrawProgressContinuous(Graphics graphics)
        {
            GraphicsPath _progressPath = new GraphicsPath();
            _progressPath.AddRectangle(new Rectangle(0, 0, (Value / Maximum) * ClientRectangle.Width, ClientRectangle.Height));
            graphics.FillPath(new SolidBrush(_progressColor), _progressPath);
        }

        private void DrawProgressMarquee(Graphics graphics)
        {
            Rectangle _progressRectangle;

            switch (_orientation)
            {
                case Orientation.Horizontal:
                    {
                        _progressRectangle = new Rectangle(_marqueeX, 0, _marqueeWidth, ClientRectangle.Height);
                        break;
                    }

                case Orientation.Vertical:
                    {
                        _progressRectangle = new Rectangle(0, _marqueeY, ClientRectangle.Width, ClientRectangle.Height);
                        break;
                    }

                default:
                    {
                        throw new ArgumentOutOfRangeException();
                    }
            }

            GraphicsPath _progressPath = new GraphicsPath();
            _progressPath.AddRectangle(_progressRectangle);
            graphics.SetClip(ControlGraphicsPath);
            graphics.FillPath(new SolidBrush(_progressColor), _progressPath);
            VisualProgressRenderer.RenderHatch(graphics, _hatch, _progressPath);
            graphics.ResetClip();
        }

        private void DrawText(Graphics graphics)
        {
            // Draw value as a string
            string percentValue = Convert.ToString(Convert.ToInt32(Value)) + "%";

            // Toggle percentage
            if (_percentageVisible)
            {
                StringFormat stringFormat = new StringFormat { Alignment = _valueAlignment, LineAlignment = StringAlignment.Center };

                graphics.DrawString(
                    percentValue,
                    Font,
                    new SolidBrush(ForeColor),
                    new Rectangle(0, 0, Width, Height + 2),
                    stringFormat);
            }
        }

        private void MarqueeTimer_Tick(object sender, EventArgs e)
        {
            switch (_orientation)
            {
                case Orientation.Horizontal:
                    {
                        _marqueeX++;
                        if (_marqueeX > ClientRectangle.Width)
                        {
                            _marqueeX = -_marqueeWidth;
                        }

                        break;
                    }

                case Orientation.Vertical:
                    {
                        _marqueeY++;
                        if (_marqueeY > ClientRectangle.Height)
                        {
                            _marqueeY = -_marqueeWidth;
                        }

                        break;
                    }

                default:
                    {
                        throw new ArgumentOutOfRangeException();
                    }
            }

            Invalidate();
        }

        private void StartTimer()
        {
            if (_marqueeTimerEnabled)
            {
                return;
            }

            if (_marqueeTimer == null)
            {
                _marqueeTimer = new Timer { Interval = 10 };
                _marqueeTimer.Tick += MarqueeTimer_Tick;
            }

            switch (_orientation)
            {
                case Orientation.Horizontal:
                    {
                        _marqueeX = -_marqueeWidth;
                        break;
                    }

                case Orientation.Vertical:
                    {
                        _marqueeY = -_marqueeWidth;
                        break;
                    }

                default:
                    {
                        throw new ArgumentOutOfRangeException();
                    }
            }

            _marqueeTimer.Stop();
            _marqueeTimer.Start();

            _marqueeTimerEnabled = true;

            Invalidate();
        }

        private void StopTimer()
        {
            if (_marqueeTimer == null)
            {
                return;
            }

            _marqueeTimer.Stop();

            Invalidate();
        }

        #endregion
    }
}