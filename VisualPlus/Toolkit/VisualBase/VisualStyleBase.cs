#region Namespace

using System;
using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Forms;

using VisualPlus.Delegates;
using VisualPlus.Enumerators;
using VisualPlus.Events;
using VisualPlus.Interfaces;
using VisualPlus.Localization;
using VisualPlus.Structure;
using VisualPlus.Toolkit.Components;
using VisualPlus.TypeConverters;

#endregion

namespace VisualPlus.Toolkit.VisualBase
{
    [ToolboxItem(false)]
    [DesignerCategory("code")]
    [ClassInterface(ClassInterfaceType.AutoDispatch)]
    [ComVisible(true)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public class VisualStyleBase : VisualControlBase, IThemeManager
    {
        #region Variables

        private MouseStates _mouseState;
        private TextStyle _textStyle;
        private StyleManager _themeManager;

        #endregion

        #region Constructors

        /// <summary>Initializes a new instance of the <see cref="VisualStyleBase" /> class.</summary>
        public VisualStyleBase()
        {
            // Allow transparent BackColor.
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);

            // Double buffering to reduce drawing flicker.
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);

            // Repaint entire control whenever resizing.
            SetStyle(ControlStyles.ResizeRedraw, true);

            UpdateStyles();
            Initialize();
        }

        #endregion

        #region Events

        [Category(EventCategory.Mouse)]
        [Description("Occours when the MouseState of the control has changed.")]
        public event MouseStateChangedEventHandler MouseStateChanged;

        [Category(EventCategory.PropertyChanged)]
        [Description("Occours when the text style of the control has changed.")]
        public event EventHandler TextStyleChanged;

        [Category(EventCategory.PropertyChanged)]
        [Description("Occours when the theme of the control has changed.")]
        public event ThemeChangedEventHandler ThemeChanged;

        #endregion

        #region Properties

        /// <summary>Gets or sets the <see cref="MouseState" />.</summary>
        [Category(PropertyCategory.Appearance)]
        [Description(PropertyDescription.MouseState)]
        public MouseStates MouseState
        {
            get
            {
                return _mouseState;
            }

            set
            {
                if (_mouseState == value)
                {
                    return;
                }

                _mouseState = value;
                OnMouseStateChanged(new MouseStateEventArgs(_mouseState));
            }
        }

        /// <summary>Gets or sets the <see cref="TextStyle" />.</summary>
        [Browsable(false)]
        [Category(PropertyCategory.Appearance)]
        [Description(PropertyDescription.TextStyle)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [TypeConverter(typeof(VisualSettingsTypeConverter))]
        public TextStyle TextStyle
        {
            get
            {
                return _textStyle;
            }

            set
            {
                if (_textStyle == null)
                {
                    return;
                }

                _textStyle = value;
                OnTextStyleChanged(new EventArgs());
            }
        }

        /// <summary>Gets or sets the <see cref="StyleManager" />.</summary>
        [Browsable(false)]
        [Category(PropertyCategory.Appearance)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public StyleManager ThemeManager
        {
            get
            {
                return _themeManager;
            }

            set
            {
                if ((_themeManager == null) || (_themeManager == value))
                {
                    return;
                }

                _themeManager = value;
            }
        }

        /// <summary>Gets or sets the <see cref="GraphicsPath" />.</summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        internal GraphicsPath ControlGraphicsPath { get; set; }

        #endregion

        #region Overrides

        /// <summary>Invokes the mouse state changed event.</summary>
        /// <param name="e">The event args.</param>
        protected virtual void OnMouseStateChanged(MouseStateEventArgs e)
        {
            Invalidate();
            MouseStateChanged?.Invoke(e);
        }

        /// <summary>Invokes the text style changed event.</summary>
        /// <param name="e">The event args.</param>
        protected virtual void OnTextStyleChanged(EventArgs e)
        {
            Invalidate();
            TextStyleChanged?.Invoke(this, e);
        }

        /// <summary>Invokes the theme changed event.</summary>
        /// <param name="e">The event args.</param>
        protected virtual void OnThemeChanged(ThemeEventArgs e)
        {
            Invalidate();
            ThemeChanged?.Invoke(e);
        }

        #endregion

        #region Methods

        /// <summary>Initialize the base.</summary>
        private void Initialize()
        {
            DoubleBuffered = true;
            ResizeRedraw = true;

            _mouseState = MouseStates.Normal;
            _themeManager = new StyleManager(Settings.DefaultValue.DefaultStyle);

            Theme theme = new Theme(Settings.DefaultValue.DefaultStyle);

            ControlColorState controlState = new ControlColorState
                {
                    Disabled = theme.ColorPalette.TextDisabled,
                    Enabled = theme.ColorPalette.TextEnabled,
                    Hover = theme.ColorPalette.TextHover,
                    Pressed = theme.ColorPalette.TextPressed
                };

            _textStyle = new TextStyle(controlState);
        }

        #endregion
    }
}