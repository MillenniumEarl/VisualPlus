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
    [DefaultEvent("ToggleChanged")]
    [DefaultProperty("Toggled")]
    [Description("The Visual Toggle")]
    [Designer(typeof(VisualToggleDesigner))]
    [ToolboxBitmap(typeof(VisualToggle), "VisualToggle.bmp")]
    [ToolboxItem(true)]
    public class VisualToggle : ToggleBase, IThemeSupport
    {
        #region Variables

        private readonly Timer _animationTimer;
        private Border _border;
        private Border _buttonBorder;
        private ControlColorState _buttonColorState;
        private Rectangle _buttonRectangle;
        private Size _buttonSize;
        private ColorState _controlColorState;
        private string _falseTextToggle;
        private Image _progressImage;
        private string _textProcessor;
        private int _toggleLocation;
        private ToggleTypes _toggleType;
        private string _trueTextToggle;

        #endregion

        #region Constructors

        /// <summary>Initializes a new instance of the <see cref="VisualToggle" /> class.</summary>
        public VisualToggle()
        {
            Size = new Size(50, 25);

            _animationTimer = new Timer
                    {
                       Interval = 1 
                    };

            _animationTimer.Tick += AnimationTimerTick;
            _toggleType = ToggleTypes.YesNo;
            _buttonSize = new Size(20, 20);
            _trueTextToggle = "Yes";
            _falseTextToggle = "No";

            _border = new Border
                    {
                       Rounding = Settings.DefaultValue.Rounding.ToggleBorder 
                    };

            _buttonBorder = new Border
                    {
                       Rounding = Settings.DefaultValue.Rounding.ToggleButton 
                    };

            UpdateTheme(ThemeManager.Theme);
        }

        #endregion

        #region Enumerators

        public enum ToggleTypes
        {
            /// <summary>Yes / No toggle.</summary>
            YesNo,

            /// <summary>On / Off toggle.</summary>
            OnOff,

            /// <summary>I / O toggle.</summary>
            IO,

            /// <summary>The custom toggle.</summary>
            Custom
        }

        #endregion

        #region Properties

        [TypeConverter(typeof(VisualSettingsTypeConverter))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Category(PropertyCategory.Appearance)]
        public ColorState BackColorState
        {
            get
            {
                return _controlColorState;
            }

            set
            {
                if (value == _controlColorState)
                {
                    return;
                }

                _controlColorState = value;
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
        [Category(PropertyCategory.Appearance)]
        public Border ButtonBorder
        {
            get
            {
                return _buttonBorder;
            }

            set
            {
                _buttonBorder = value;
                Invalidate();
            }
        }

        [TypeConverter(typeof(VisualSettingsTypeConverter))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Category(PropertyCategory.Appearance)]
        public ControlColorState ButtonColorState
        {
            get
            {
                return _buttonColorState;
            }

            set
            {
                if (value == _buttonColorState)
                {
                    return;
                }

                _buttonColorState = value;
                Invalidate();
            }
        }

        [Category(PropertyCategory.Layout)]
        [Description(PropertyDescription.Size)]
        public Size ButtonSize
        {
            get
            {
                return _buttonSize;
            }

            set
            {
                _buttonSize = value;
                Invalidate();
            }
        }

        [Category(PropertyCategory.Appearance)]
        [Description(PropertyDescription.Text)]
        public string FalseTextToggle
        {
            get
            {
                return _falseTextToggle;
            }

            set
            {
                _falseTextToggle = value;
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

        [DefaultValue(false)]
        [Category(PropertyCategory.Behavior)]
        [Description(PropertyDescription.Toggle)]
        public bool Toggled
        {
            get
            {
                return Toggle;
            }

            set
            {
                Toggle = value;
                Invalidate();
                OnToggleChanged(new ToggleEventArgs(Toggle));
            }
        }

        [Category(PropertyCategory.Appearance)]
        [Description(PropertyDescription.Text)]
        public string TrueTextToggle
        {
            get
            {
                return _trueTextToggle;
            }

            set
            {
                _trueTextToggle = value;
                Invalidate();
            }
        }

        [Category(PropertyCategory.Appearance)]
        [Description(PropertyDescription.Type)]
        public ToggleTypes Type
        {
            get
            {
                return _toggleType;
            }

            set
            {
                _toggleType = value;
                Invalidate();
            }
        }

        #endregion

        #region Overrides

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            _animationTimer.Start();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            MouseState = MouseStates.Pressed;
            Invalidate();
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
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
            MouseState = MouseState = MouseStates.Hover;
            Toggled = !Toggled;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics _graphics = e.Graphics;
            _graphics.SmoothingMode = SmoothingMode.HighQuality;
            _graphics.TextRenderingHint = TextStyle.TextRenderingHint;
            Rectangle _clientRectangle = new Rectangle(ClientRectangle.X, ClientRectangle.Y, ClientRectangle.Width - 1, ClientRectangle.Height - 1);
            ControlGraphicsPath = VisualBorderRenderer.CreateBorderTypePath(_clientRectangle, _border);
            Color _backColor = Enabled ? _controlColorState.Enabled : _controlColorState.Disabled;

            _graphics.SetClip(ControlGraphicsPath);

            VisualBackgroundRenderer.DrawBackground(e.Graphics, _backColor, BackgroundImage, MouseState, _clientRectangle, Border);

            // Determines button/toggle state
            Point _startPoint = new Point(0 + 2, (_clientRectangle.Height / 2) - (_buttonSize.Height / 2));
            Point _endPoint = new Point(_clientRectangle.Width - _buttonSize.Width - 2, (_clientRectangle.Height / 2) - (_buttonSize.Height / 2));
            Point _buttonLocation = Toggle ? _endPoint : _startPoint;
            _buttonRectangle = new Rectangle(_buttonLocation, _buttonSize);
            DrawToggleText(_graphics);

            Color _buttonColor = ControlColorState.BackColorState(ButtonColorState, Enabled, MouseState);
            VisualBackgroundRenderer.DrawBackground(e.Graphics, _buttonColor, _buttonRectangle, _buttonBorder);

            VisualBorderRenderer.DrawBorderStyle(e.Graphics, _border, ControlGraphicsPath, MouseState);
            _graphics.ResetClip();
        }

        #endregion

        #region Methods

        public void UpdateTheme(Theme theme)
        {
            try
            {
                _border.Color = theme.ColorPalette.BorderNormal;
                _border.HoverColor = theme.ColorPalette.BorderHover;

                _buttonBorder.Color = theme.ColorPalette.BorderNormal;
                _buttonBorder.HoverColor = theme.ColorPalette.BorderHover;

                ForeColor = theme.ColorPalette.TextEnabled;
                TextStyle.Enabled = theme.ColorPalette.TextEnabled;
                TextStyle.Disabled = theme.ColorPalette.TextDisabled;

                // Font = theme.ColorPalette.Font;
                _controlColorState = new ColorState
                    {
                        Enabled = theme.ColorPalette.ControlEnabled,
                        Disabled = theme.ColorPalette.ControlDisabled
                    };

                _buttonColorState = new ControlColorState
                    {
                        Enabled = theme.ColorPalette.Enabled,
                        Disabled = theme.ColorPalette.Disabled,
                        Hover = theme.ColorPalette.Hover,
                        Pressed = theme.ColorPalette.Pressed
                    };
            }
            catch (Exception e)
            {
                ConsoleEx.WriteDebug(e);
            }

            Invalidate();
            OnThemeChanged(new ThemeEventArgs(theme));
        }

        /// <summary>Create a slide animation when toggled.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event args.</param>
        private void AnimationTimerTick(object sender, EventArgs e)
        {
            if (Toggle)
            {
                if (_toggleLocation >= 100)
                {
                    return;
                }

                _toggleLocation += 10;
                Invalidate(false);
            }
            else if (_toggleLocation > 0)
            {
                _toggleLocation -= 10;
                Invalidate(false);
            }
        }

        private void DrawToggleText(Graphics graphics)
        {
            // Determines the type of toggle to draw.
            switch (_toggleType)
            {
                case ToggleTypes.YesNo:
                    {
                        _textProcessor = Toggled ? "No" : "Yes";
                        break;
                    }

                case ToggleTypes.OnOff:
                    {
                        _textProcessor = Toggled ? "Off" : "On";
                        break;
                    }

                case ToggleTypes.IO:
                    {
                        _textProcessor = Toggled ? "O" : "I";
                        break;
                    }

                case ToggleTypes.Custom:
                    {
                        _textProcessor = Toggled ? _falseTextToggle : _trueTextToggle;
                        break;
                    }

                default:
                    throw new ArgumentOutOfRangeException();
            }

            // Draw string
            Rectangle textBoxRectangle;

            const int XOff = 5;
            const int XOn = 7;

            if (Toggle)
            {
                textBoxRectangle = new Rectangle(XOff, 0, Width - 1, Height - 1);
            }
            else
            {
                Size textSize = TextManager.MeasureText(_textProcessor, Font, graphics);
                textBoxRectangle = new Rectangle(Width - (textSize.Width / 2) - (XOn * 2), 0, Width - 1, Height - 1);
            }

            StringFormat stringFormat = new StringFormat
                {
                    // Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center
                };

            Color _foreColor = Enabled ? ForeColor : TextStyle.Disabled;

            graphics.DrawString(
                _textProcessor,
                new Font(Font.FontFamily, 7f, Font.Style),
                new SolidBrush(_foreColor),
                textBoxRectangle,
                stringFormat);
        }

        #endregion
    }
}