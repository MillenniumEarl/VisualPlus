#region Namespace

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

using VisualPlus.Enumerators;
using VisualPlus.Events;
using VisualPlus.Extensibility;
using VisualPlus.Interfaces;
using VisualPlus.Localization;
using VisualPlus.Managers;
using VisualPlus.Renders;
using VisualPlus.Structure;
using VisualPlus.TypeConverters;

#endregion

namespace VisualPlus.Toolkit.VisualBase
{
    [ToolboxItem(false)]
    [DesignerCategory("code")]
    [ClassInterface(ClassInterfaceType.AutoDispatch)]
    [ComVisible(true)]
    public abstract class ToggleCheckmarkBase : ToggleBase, IAnimationSupport
    {
        #region Variables

        private bool _animation;
        private Border _border;
        private Rectangle _box;
        private int _boxSpacing;
        private CheckStyle _checkStyle;
        private ControlColorState _colorState;
        private Point _mouseLocation;
        private VFXManager _rippleEffectsManager;
        private Size _textSize;

        #endregion

        #region Constructors

        /// <summary>Initializes a new instance of the <see cref="ToggleCheckmarkBase" /> class.</summary>
        protected ToggleCheckmarkBase()
        {
            _boxSpacing = 2;
            _box = new Rectangle(0, 0, 14, 14);
            _animation = Settings.DefaultValue.Animation;
            _checkStyle = new CheckStyle(ClientRectangle);
            _border = new Border();
            _colorState = new ControlColorState();
            ConfigureAnimation(new[] { 0.05, 0.10, 0.08 }, new[] { EffectType.EaseInOut, EffectType.Linear });
        }

        #endregion

        #region Properties

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

                // Make AutoSize directly set the bounds.
                AutoSize = AutoSize;

                if (value)
                {
                    Margin = new Padding(0);
                }

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

        [Description(PropertyDescription.Size)]
        [Category(PropertyCategory.Layout)]
        public Size Box
        {
            get
            {
                return _box.Size;
            }

            set
            {
                _box.Size = value;
                if (AutoSize)
                {
                    AutoFit(Text.MeasureText(Font));
                }

                Invalidate();
            }
        }

        [TypeConverter(typeof(VisualSettingsTypeConverter))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public ControlColorState BoxColorState
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

        [Category(PropertyCategory.Layout)]
        [Description(PropertyDescription.Spacing)]
        public int BoxSpacing
        {
            get
            {
                return _boxSpacing;
            }

            set
            {
                _boxSpacing = value;
                Invalidate();
            }
        }

        [DefaultValue(false)]
        [Category(PropertyCategory.Behavior)]
        [Description(PropertyDescription.Checked)]
        public bool Checked
        {
            get
            {
                return Toggle;
            }

            set
            {
                if (Toggle != value)
                {
                    // Store new values
                    Toggle = value;

                    // Generate events
                    OnToggleChanged(new ToggleEventArgs(Toggle));

                    // Repaint
                    Invalidate();
                }
            }
        }

        [TypeConverter(typeof(VisualSettingsTypeConverter))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Category(PropertyCategory.Appearance)]
        public CheckStyle CheckStyle
        {
            get
            {
                return _checkStyle;
            }

            set
            {
                _checkStyle = value;
                Invalidate();
            }
        }

        /// <summary>Gets the <see cref="GlyphSize" /> of the control.</summary>
        [Browsable(false)]
        [Description(PropertyDescription.Size)]
        public Size GlyphSize
        {
            get
            {
                return _box.Size;
            }
        }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool IsBoxLarger { get; set; }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public Size TextSize
        {
            get
            {
                return _textSize;
            }

            set
            {
                _textSize = value;
            }
        }

        #endregion

        #region Overrides

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
                };
            MouseLeave += (sender, args) =>
                {
                    _mouseLocation = new Point(-1, -1);
                    MouseState = MouseStates.Normal;
                };
            MouseDown += (sender, args) =>
                {
                    MouseState = MouseStates.Pressed;

                    if (_animation && (args.Button == MouseButtons.Left) && MouseManager.IsMouseInBounds(_mouseLocation, _box))
                    {
                        _rippleEffectsManager.SecondaryIncrement = 0;
                        _rippleEffectsManager.StartNewAnimation(AnimationDirection.InOutIn, new object[] { Toggle });
                    }
                };
            MouseUp += (sender, args) =>
                {
                    MouseState = MouseStates.Hover;
                    _rippleEffectsManager.SecondaryIncrement = 0.08;
                };
            MouseMove += (sender, args) =>
                {
                    _mouseLocation = args.Location;
                    Cursor = MouseManager.IsMouseInBounds(_mouseLocation, _box) ? Cursors.Hand : Cursors.Default;
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

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            MouseState = MouseStates.Normal;
            Invalidate();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            MouseState = MouseStates.Hover;
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (AutoSize)
            {
                _box = new Rectangle(new Point(Padding.Left, (ClientRectangle.Height / 2) - (_box.Height / 2)), _box.Size);
                AutoFit(Text.MeasureText(Font));
            }
            else
            {
                _box = new Rectangle(new Point(Padding.Left, (ClientRectangle.Height / 2) - (_box.Height / 2)), _box.Size);
            }

            Color _backColor = ControlColorState.BackColorState(BoxColorState, Enabled, MouseState);

            Graphics _graphics = e.Graphics;
            _graphics.SmoothingMode = SmoothingMode.HighQuality;
            _graphics.TextRenderingHint = TextStyle.TextRenderingHint;

            Rectangle _clientRectangle = new Rectangle(ClientRectangle.X - 1, ClientRectangle.Y - 1, ClientRectangle.Width + 2, ClientRectangle.Height + 2);
            Shape _clientShape = new Shape(ShapeTypes.Rectangle, _backColor, 0);

            GraphicsPath _clientPath = VisualBorderRenderer.CreateBorderTypePath(_clientRectangle, _clientShape);
            ControlGraphicsPath = VisualBorderRenderer.CreateBorderTypePath(_clientRectangle, _border);

            e.Graphics.SetClip(_clientPath);

            _textSize = TextManager.MeasureText(Text, Font, _graphics);
            Point _textLocation = new Point(_box.Right + _boxSpacing, (ClientRectangle.Height / 2) - (_textSize.Height / 2));
            Color _textColor = Enabled ? ForeColor : TextStyle.Disabled;

            VisualToggleRenderer.DrawCheckBox(_graphics, Border, _checkStyle, _box, Checked, Enabled, _backColor, BackgroundImage, MouseState, Text, Font, _textColor, _textLocation);
            DrawAnimation(_graphics);
            e.Graphics.ResetClip();
        }

        #endregion

        #region Methods

        /// <summary>Gets the first or default control.</summary>
        /// <param name="container">The container control.</param>
        /// <returns>The <see cref="ToggleCheckmarkBase" />.</returns>
        public static ToggleCheckmarkBase GetSelectedControl(Control container)
        {
            if (container == null)
            {
                throw new ArgumentNullException(nameof(container));
            }

            if (container.Controls.Count == 0)
            {
                return null;
            }

            return container.Controls.OfType<ToggleCheckmarkBase>().FirstOrDefault(r => r.Toggle);
        }

        /// <summary>Gets the toggled controls from the container.</summary>
        /// <param name="container">The container control.</param>
        /// <returns>The <see cref="ToggleCheckmarkBase" /> list.</returns>
        public static List<ToggleCheckmarkBase> GetToggled(Control container)
        {
            if (container == null)
            {
                throw new ArgumentNullException(nameof(container));
            }

            if (container.Controls.Count == 0)
            {
                return null;
            }

            var toggledControlsList = new List<ToggleCheckmarkBase>();
            foreach (object control in container.Controls)
            {
                if (control is ToggleCheckmarkBase toggledControl && toggledControl.Toggle)
                {
                    toggledControlsList.Add(toggledControl);
                }
            }

            return toggledControlsList;
        }

        public void ConfigureAnimation(double[] effectIncrement, EffectType[] effectType)
        {
            VFXManager effectsManager = new VFXManager
                {
                    Increment = effectIncrement[0],
                    EffectType = effectType[0]
                };

            _rippleEffectsManager = new VFXManager(false)
                {
                    Increment = effectIncrement[1],
                    SecondaryIncrement = effectIncrement[2],
                    EffectType = effectType[1]
                };

            effectsManager.OnAnimationProgress += sender => Invalidate();
            _rippleEffectsManager.OnAnimationProgress += sender => Invalidate();
            effectsManager.StartNewAnimation(Toggle ? AnimationDirection.In : AnimationDirection.Out);
        }

        public void DrawAnimation(Graphics graphics)
        {
            if (_animation && _rippleEffectsManager.IsAnimating())
            {
                for (var i = 0; i < _rippleEffectsManager.GetAnimationCount(); i++)
                {
                    double animationValue = _rippleEffectsManager.GetProgress(i);

                    Point animationSource = new Point(_box.X + (_box.Width / 2), _box.Y + (_box.Height / 2));
                    SolidBrush animationBrush = new SolidBrush(Color.FromArgb((int)(animationValue * 40), (bool)_rippleEffectsManager.GetData(i)[0] ? Color.Black : _checkStyle.CheckColor));

                    int height = _box.Height;
                    int size = _rippleEffectsManager.GetDirection(i) == AnimationDirection.InOutIn ? (int)(height * (0.8d + (0.2d * animationValue))) : height;

                    Rectangle _animationBox = new Rectangle(animationSource.X - (size / 2), animationSource.Y - (size / 2), size, size);
                    GraphicsPath _path = VisualBorderRenderer.CreateBorderTypePath(_animationBox, _border);

                    graphics.FillPath(animationBrush, _path);
                    animationBrush.Dispose();
                }
            }
        }

        /// <summary>Auto fit to the text size.</summary>
        /// <param name="textSize">The text size.</param>
        private void AutoFit(Size textSize)
        {
            if (GraphicsManager.TextLargerThanRectangle(textSize, _box))
            {
                IsBoxLarger = false;
                Size = new Size(_box.X + _box.Width + _boxSpacing + textSize.Width, textSize.Height);
            }
            else
            {
                IsBoxLarger = true;
                Size = new Size(_box.X + _box.Width + _boxSpacing + textSize.Width, _box.Height);
            }
        }

        #endregion
    }
}