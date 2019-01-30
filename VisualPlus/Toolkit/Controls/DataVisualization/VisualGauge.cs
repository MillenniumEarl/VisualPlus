#region License

// -----------------------------------------------------------------------------------------------------------
// 
// Name: VisualGauge.cs
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
using System.Windows.Forms;

using VisualPlus.Events;
using VisualPlus.Interfaces;
using VisualPlus.Localization;
using VisualPlus.Models;
using VisualPlus.Toolkit.VisualBase;
using VisualPlus.TypeConverters;
using VisualPlus.Utilities;
using VisualPlus.Utilities.Debugging;

#endregion

namespace VisualPlus.Toolkit.Controls.DataVisualization
{
    [ToolboxItem(true)]
    [ToolboxBitmap(typeof(VisualGauge), "VisualGauge.bmp")]
    [DefaultEvent("Click")]
    [DefaultProperty("Value")]
    [Description("The Visual Gauge")]
    public class VisualGauge : ProgressBase, IThemeSupport
    {
        #region Fields

        private ColorState _colorState;
        private Label _labelMaximum;
        private Label _labelMinimum;
        private Label _labelProgress;
        private Color _progress;
        private Size _progressTextSize;
        private int _thickness;

        #endregion

        #region Constructors and Destructors

        /// <summary>Initializes a new instance of the <see cref="VisualGauge" /> class.</summary>
        public VisualGauge()
        {
            _thickness = 25;
            Maximum = 100;

            ConstructDisplay();
            Controls.Add(_labelMaximum);
            Controls.Add(_labelMinimum);
            Controls.Add(_labelProgress);

            Margin = new Padding(6);
            Size = new Size(174, 117);

            Font = SystemFonts.DefaultFont;
            UpdateTheme(ThemeManager.Theme);
        }

        #endregion

        #region Public Properties

        [TypeConverter(typeof(VisualSettingsTypeConverter))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public ColorState BackColorState
        {
            get
            {
                return _colorState;
            }

            set
            {
                _colorState = value;
                Invalidate();
            }
        }

        [Category(PropertyCategory.Appearance)]
        [Description(PropertyDescription.Visible)]
        public bool MaximumVisible
        {
            get
            {
                return _labelMaximum.Visible;
            }

            set
            {
                _labelMaximum.Visible = value;
            }
        }

        [Category(PropertyCategory.Appearance)]
        [Description(PropertyDescription.Visible)]
        public bool MinimumVisible
        {
            get
            {
                return _labelMinimum.Visible;
            }

            set
            {
                _labelMinimum.Visible = value;
            }
        }

        [DefaultValue(typeof(Color), "Green")]
        [Category(PropertyCategory.Appearance)]
        [Description(PropertyDescription.Color)]
        public Color Progress
        {
            get
            {
                return _progress;
            }

            set
            {
                _progress = value;
                Invalidate();
            }
        }

        [Category(PropertyCategory.Appearance)]
        [Description(PropertyDescription.Visible)]
        public bool ProgressVisible
        {
            get
            {
                return _labelProgress.Visible;
            }

            set
            {
                _labelProgress.Visible = value;
            }
        }

        [DefaultValue(30)]
        [Category(PropertyCategory.Layout)]
        [Description(PropertyDescription.Thickness)]
        public int Thickness
        {
            get
            {
                return _thickness;
            }

            set
            {
                _thickness = value;
                Invalidate();
            }
        }

        [DefaultValue(0)]
        [Category(PropertyCategory.Behavior)]
        public new int Value
        {
            get
            {
                return base.Value;
            }

            set
            {
                base.Value = value;
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

                _colorState = new ColorState { Enabled = theme.ColorPalette.ControlEnabled, Disabled = theme.ColorPalette.ControlDisabled };

                _progress = theme.ColorPalette.Progress;
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

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            _progressTextSize = TextManager.MeasureText(_labelProgress.Text + @"%", Font, e.Graphics);
            _labelProgress.Location = new Point((Width / 2) - (_progressTextSize.Width / 2), Height - _progressTextSize.Height - 30);

            Graphics _graphics = e.Graphics;
            _graphics.SmoothingMode = SmoothingMode.HighQuality;

            Color _backColor = Enabled ? BackColorState.Enabled : BackColorState.Disabled;

            Pen _penBackground = new Pen(_backColor, _thickness);
            int _width = Size.Width - (_thickness * 2);
            Rectangle _rectangle = new Rectangle(_thickness, Size.Height / 4, _width, _width);
            Pen _penProgress = new Pen(_progress, _thickness);

            _graphics.DrawArc(_penBackground, _rectangle, 180F, 180F);
            _graphics.DrawArc(_penProgress, _rectangle, 180F, MathUtil.GetHalfRadianAngle(Value));
            
            _labelProgress.Text = Value + @"%";
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            _labelMinimum.Top = _labelMaximum.Top = Height - _labelMaximum.Height - 10;
            _labelMinimum.Left = 20;
            _labelMaximum.Left = Size.Width - _labelMaximum.Width - 20;
        }

        private void ConstructDisplay()
        {
            _labelProgress = new Label
                {
                    AutoSize = true,
                    Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0),
                    Location = new Point(83, 34),
                    Margin = new Padding(6, 0, 6, 0),
                    Name = "visualLabelProgress",
                    Size = new Size(22, 24),
                    TabIndex = 1,
                    Text = @"0",
                    BackColor = Color.Transparent
                };

            _labelMinimum = new Label
                {
                    AutoSize = true,
                    Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0),
                    Location = new Point(26, 86),
                    Margin = new Padding(6, 0, 6, 0),
                    Name = "visualLabelMinimum",
                    Size = new Size(15, 17),
                    TabIndex = 2,
                    Text = @"0",
                    BackColor = Color.Transparent
                };

            _labelMaximum = new Label
                {
                    AutoSize = true,
                    Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0),
                    Location = new Point(145, 86),
                    Margin = new Padding(6, 0, 6, 0),
                    Name = "visualLabelMaximum",
                    Size = new Size(29, 17),
                    TabIndex = 3,
                    Text = @"100",
                    BackColor = Color.Transparent
                };
        }

        #endregion
    }
}