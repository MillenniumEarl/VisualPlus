#region License

// -----------------------------------------------------------------------------------------------------------
// 
// Name: VisualButton.cs
// VisualPlus - The VisualPlus Framework (VPF) for WinForms .NET development.
// 
// Created: 10/12/2018 - 11:45 PM
// Last Modified: 02/01/2019 - 1:26 AM
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
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Forms;

using VisualPlus.Designer;
using VisualPlus.Enumerators;
using VisualPlus.Events;
using VisualPlus.Interfaces;
using VisualPlus.Localization;
using VisualPlus.Managers;
using VisualPlus.Renders;
using VisualPlus.Structure;
using VisualPlus.Toolkit.VisualBase;
using VisualPlus.TypeConverters;

#endregion

namespace VisualPlus.Toolkit.Controls.Interactivity
{
    [ClassInterface(ClassInterfaceType.AutoDispatch)]
    [ComVisible(true)]
    [DefaultEvent("Click")]
    [DefaultProperty("Text")]
    [Description("The Visual Button")]
    [Designer(typeof(VisualButtonDesigner))]
    [ToolboxBitmap(typeof(VisualButton), "VisualButton.bmp")]
    [ToolboxItem(true)]
    public class VisualButton : VisualStyleBase, IAnimationSupport, IButtonControl, IThemeSupport
    {
        #region Fields

        private bool _animation;
        private ControlColorState _backColorState;
        private Border _border;
        private VFXManager _effectsManager;
        private VFXManager _hoverEffectsManager;
        private Image _image;
        private TextImageRelation _textImageRelation;
        private DialogResult dialogResult;

        #endregion

        #region Constructors and Destructors

        /// <summary>Initializes a new instance of the <see cref="VisualButton" /> class.</summary>
        public VisualButton()
        {
            Size = new Size(140, 45);
            _animation = Settings.DefaultValue.Animation;
            _border = new Border();
            _backColorState = new ControlColorState();
            dialogResult = DialogResult.None;
            _textImageRelation = TextImageRelation.Overlay;
            ConfigureAnimation(new[] { 0.03, 0.07 }, new[] { EffectType.EaseOut, EffectType.EaseInOut });

            UpdateTheme(ThemeManager.Theme);
        }

        /// <summary>Finalizes an instance of the <see cref="VisualButton" /> class.</summary>
        ~VisualButton()
        {
            Dispose(false);
        }

        #endregion

        #region Public Properties

        [DefaultValue(Settings.DefaultValue.Animation)]
        [Category(PropertyCategory.Behavior)]
        [Description(PropertyDescription.Animation)]
        public bool Animation
        {
            get
            {
                return _animation;
            }

            set
            {
                _animation = value;
                AutoSize = AutoSize; // Make AutoSize directly set the bounds.

                if (value)
                {
                    Margin = new Padding(0);
                }

                Invalidate();
            }
        }

        [TypeConverter(typeof(VisualSettingsTypeConverter))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public ControlColorState BackColorState
        {
            get
            {
                return _backColorState;
            }

            set
            {
                if (value == _backColorState)
                {
                    return;
                }

                _backColorState = value;
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

        /// <summary>Gets or sets the value returned from the parent form when the button is clicked.</summary>
        [Browsable(false)]
        public DialogResult DialogResult
        {
            get
            {
                return dialogResult;
            }

            set
            {
                if (Enum.IsDefined(typeof(DialogResult), value))
                {
                    dialogResult = value;
                }
            }
        }

        public new Color ForeColor
        {
            get
            {
                base.ForeColor = TextStyle.Enabled;
                return base.ForeColor;
            }

            set
            {
                TextStyle.Enabled = value;
                base.ForeColor = TextStyle.Enabled;
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

        [Category(PropertyCategory.Behavior)]
        [Description(PropertyDescription.TextImageRelation)]
        public TextImageRelation TextImageRelation
        {
            get
            {
                return _textImageRelation;
            }

            set
            {
                _textImageRelation = value;
                Invalidate();
            }
        }

        [Browsable(true)]
        [Category(PropertyCategory.Appearance)]
        [Description(PropertyDescription.TextStyle)]
        public new TextStyle TextStyle
        {
            get
            {
                return base.TextStyle;
            }

            set
            {
                base.TextStyle = value;
            }
        }

        #endregion

        #region Properties

        private bool IsDefault { get; set; }

        #endregion

        #region Public Methods and Operators

        public void ConfigureAnimation(double[] effectIncrement, EffectType[] effectType)
        {
            _effectsManager = new VFXManager(false) { Increment = effectIncrement[0], EffectType = effectType[0] };

            _hoverEffectsManager = new VFXManager { Increment = effectIncrement[1], EffectType = effectType[1] };

            _hoverEffectsManager.OnAnimationProgress += sender => Invalidate();
            _effectsManager.OnAnimationProgress += sender => Invalidate();
        }

        public void DrawAnimation(Graphics graphics)
        {
            if (!_effectsManager.IsAnimating() || !_animation)
            {
                return;
            }

            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            for (var i = 0; i < _effectsManager.GetAnimationCount(); i++)
            {
                double _value = _effectsManager.GetProgress(i);
                Point _source = _effectsManager.GetSource(i);

                using (Brush _rippleBrush = new SolidBrush(Color.FromArgb((int)(101 - (_value * 100)), Color.Black)))
                {
                    var _rippleSize = (int)(_value * Width * 2);
                    graphics.SetClip(ControlGraphicsPath);
                    graphics.FillEllipse(_rippleBrush, new Rectangle(_source.X - (_rippleSize / 2), _source.Y - (_rippleSize / 2), _rippleSize, _rippleSize));
                }
            }

            graphics.SmoothingMode = SmoothingMode.None;
        }

        /// <summary>
        ///     Notify s a control that this is the default button so that its appearance and behavior is adjusted
        ///     accordingly.
        /// </summary>
        /// <param name="value">If the control should behave as a default button.</param>
        public void NotifyDefault(bool value)
        {
            if (IsDefault != value)
            {
                IsDefault = value;
            }
        }

        /// <summary>Generates a click event for the control.</summary>
        public void PerformClick()
        {
            if (CanSelect)
            {
                OnClick(EventArgs.Empty);
            }
        }

        public void UpdateTheme(Theme theme)
        {
            try
            {
                _border.Color = theme.ColorPalette.BorderNormal;
                _border.HoverColor = theme.ColorPalette.BorderHover;

                TextStyle.Enabled = theme.ColorPalette.TextEnabled;
                TextStyle.Disabled = theme.ColorPalette.TextDisabled;
                TextStyle.Hover = theme.ColorPalette.TextHover;
                TextStyle.Pressed = theme.ColorPalette.TextPressed;

                _backColorState.Enabled = theme.ColorPalette.Enabled;
                _backColorState.Disabled = theme.ColorPalette.Disabled;
                _backColorState.Hover = theme.ColorPalette.Hover;
                _backColorState.Pressed = theme.ColorPalette.Pressed;
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

        protected override void OnClick(EventArgs e)
        {
            Form form = FindForm();

            if (form != null)
            {
                form.DialogResult = dialogResult;
            }

            AccessibilityNotifyClients(AccessibleEvents.StateChange, -1);
            AccessibilityNotifyClients(AccessibleEvents.NameChange, -1);

            base.OnClick(e);
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            if (DesignMode)
            {
                return;
            }

            MouseState = MouseStates.Normal;
            MouseEnter += (sender, args) =>
                {
                    MouseState = MouseStates.Hover;
                    _hoverEffectsManager.StartNewAnimation(AnimationDirection.In);
                    Invalidate();
                };
            MouseLeave += (sender, args) =>
                {
                    MouseState = MouseStates.Normal;
                    _hoverEffectsManager.StartNewAnimation(AnimationDirection.Out);
                    Invalidate();
                };
            MouseDown += (sender, args) =>
                {
                    if (args.Button == MouseButtons.Left)
                    {
                        MouseState = MouseStates.Pressed;
                        _effectsManager.StartNewAnimation(AnimationDirection.In, args.Location);
                        Invalidate();
                    }
                };
            MouseUp += (sender, args) =>
                {
                    MouseState = MouseStates.Hover;
                    Invalidate();
                };
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            MouseState = MouseStates.Pressed;
            Invalidate();
        }

        protected override void OnMouseHover(EventArgs e)
        {
            base.OnMouseHover(e);
            MouseState = MouseStates.Hover;
            Invalidate();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            MouseState = MouseState = MouseStates.Hover;
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            try
            {
                Graphics _graphics = e.Graphics;

                _graphics.SmoothingMode = SmoothingMode.HighQuality;
                _graphics.TextRenderingHint = TextStyle.TextRenderingHint;
                Rectangle _clientRectangle = new Rectangle(ClientRectangle.X, ClientRectangle.Y, ClientRectangle.Width - 1, ClientRectangle.Height - 1);
                ControlGraphicsPath = VisualBorderRenderer.CreateBorderTypePath(_clientRectangle, _border);

                Color _backColor = ControlColorState.BackColorState(BackColorState, Enabled, MouseState);

                e.Graphics.SetClip(ControlGraphicsPath);
                VisualBackgroundRenderer.DrawBackground(e.Graphics, _backColor, BackgroundImage, MouseState, _clientRectangle, _border);

                if (_image != null)
                {
                    Color _textColor = Enabled ? ForeColor : TextStyle.Disabled;
                    VisualControlRenderer.DrawContent(e.Graphics, ClientRectangle, Text, Font, _textColor, _image, _image.Size, _textImageRelation);
                }
                else
                {
                    VisualTextRenderer.RenderText(e.Graphics, ClientRectangle, Text, Font, Enabled, MouseState, TextStyle);
                }

                VisualBorderRenderer.DrawBorderStyle(e.Graphics, _border, ControlGraphicsPath, MouseState);
                DrawAnimation(e.Graphics);
                e.Graphics.ResetClip();
            }
            catch (Exception exception)
            {
                ConsoleEx.WriteDebug(exception);
            }
        }

        #endregion
    }
}