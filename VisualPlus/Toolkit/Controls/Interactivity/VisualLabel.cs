#region Namespace

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Runtime.InteropServices;
using System.Windows.Forms;

using VisualPlus.Constants;
using VisualPlus.Designer;
using VisualPlus.Events;
using VisualPlus.Interfaces;
using VisualPlus.Localization;
using VisualPlus.Managers;
using VisualPlus.Renders;
using VisualPlus.Structure;
using VisualPlus.Toolkit.VisualBase;

#endregion

namespace VisualPlus.Toolkit.Controls.Interactivity
{
    [ClassInterface(ClassInterfaceType.AutoDispatch)]
    [ComVisible(true)]
    [DefaultEvent("Click")]
    [DefaultProperty("Text")]
    [Description("The Visual Label")]
    [Designer(typeof(VisualLabelDesigner))]
    [ToolboxBitmap(typeof(VisualLabel), "VisualLabel.bmp")]
    [ToolboxItem(true)]
    public class VisualLabel : VisualStyleBase, IThemeSupport
    {
        #region Variables

        private StringAlignment _alignment;
        private StringAlignment _lineAlignment;
        private Orientation _orientation;
        private bool _outline;
        private Color _outlineColor;
        private Point _outlineLocation;
        private bool _reflection;
        private Color _reflectionColor;
        private int _reflectionSpacing;
        private bool _shadow;
        private Color _shadowColor;
        private int _shadowDepth;
        private int _shadowDirection;
        private Point _shadowLocation;
        private float _shadowSmooth;
        private int shadowOpacity;
        private Rectangle textBoxRectangle;

        #endregion

        #region Constructors

        /// <summary>Initializes a new instance of the <see cref="VisualLabel" /> class.</summary>
        public VisualLabel()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer | ControlStyles.SupportsTransparentBackColor, true);

            UpdateStyles();
            _alignment = StringAlignment.Near;
            _lineAlignment = StringAlignment.Center;
            _orientation = Orientation.Horizontal;
            _outlineColor = Color.Red;
            _outlineLocation = new Point(0, 0);
            _reflectionColor = Color.FromArgb(120, 0, 0, 0);
            _shadowColor = Color.Black;
            _shadowDepth = 4;
            _shadowDirection = 315;
            _shadowLocation = new Point(0, 0);
            shadowOpacity = 100;
            _shadowSmooth = 1.5f;

            Size = new Size(75, 23);
            UpdateTheme(ThemeManager.Theme);
        }

        #endregion

        #region Properties

        [Category(PropertyCategory.Appearance)]
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
                Size = GraphicsManager.FlipOrientationSize(_orientation, Size);
                Invalidate();
            }
        }

        [Category(PropertyCategory.Appearance)]
        [Description(PropertyDescription.Outline)]
        public bool Outline
        {
            get
            {
                return _outline;
            }

            set
            {
                _outline = value;
                Invalidate();
            }
        }

        [Category(PropertyCategory.Appearance)]
        [Description(PropertyDescription.Color)]
        public Color OutlineColor
        {
            get
            {
                return _outlineColor;
            }

            set
            {
                _outlineColor = value;
                Invalidate();
            }
        }

        [Category(PropertyCategory.Layout)]
        [Description(PropertyDescription.Point)]
        public Point OutlineLocation
        {
            get
            {
                return _outlineLocation;
            }

            set
            {
                _outlineLocation = value;
                Invalidate();
            }
        }

        [DefaultValue(false)]
        [Category(PropertyCategory.Behavior)]
        [Description(PropertyDescription.Toggle)]
        public bool Reflection
        {
            get
            {
                return _reflection;
            }

            set
            {
                _reflection = value;
                Invalidate();
            }
        }

        [Category(PropertyCategory.Appearance)]
        [Description(PropertyDescription.Color)]
        public Color ReflectionColor
        {
            get
            {
                return _reflectionColor;
            }

            set
            {
                _reflectionColor = value;
                Invalidate();
            }
        }

        [Category(PropertyCategory.Layout)]
        [Description(PropertyDescription.Spacing)]
        public int ReflectionSpacing
        {
            get
            {
                return _reflectionSpacing;
            }

            set
            {
                _reflectionSpacing = value;
                Invalidate();
            }
        }

        [DefaultValue(false)]
        [Category(PropertyCategory.Appearance)]
        [Description(PropertyDescription.Toggle)]
        public bool Shadow
        {
            get
            {
                return _shadow;
            }

            set
            {
                _shadow = value;
                Invalidate();
            }
        }

        [Category(PropertyCategory.Appearance)]
        [Description(PropertyDescription.Color)]
        public Color ShadowColor
        {
            get
            {
                return _shadowColor;
            }

            set
            {
                _shadowColor = value;
                Invalidate();
            }
        }

        [Category(PropertyCategory.Appearance)]
        [Description(PropertyDescription.Direction)]
        public int ShadowDirection
        {
            get
            {
                return _shadowDirection;
            }

            set
            {
                _shadowDirection = value;
                Invalidate();
            }
        }

        [Category(PropertyCategory.Layout)]
        [Description(PropertyDescription.Point)]
        public Point ShadowLocation
        {
            get
            {
                return _shadowLocation;
            }

            set
            {
                _shadowLocation = value;
                Invalidate();
            }
        }

        [Category(PropertyCategory.Appearance)]
        [Description(PropertyDescription.Opacity)]
        public int ShadowOpacity
        {
            get
            {
                return shadowOpacity;
            }

            set
            {
                if (shadowOpacity == value)
                {
                    return;
                }

                shadowOpacity = ExceptionManager.ArgumentOutOfRangeException(value, SettingConstants.MinimumAlpha, SettingConstants.MaximumAlpha, true);
                Invalidate();
            }
        }

        [Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        public override string Text
        {
            get
            {
                return base.Text;
            }

            set
            {
                base.Text = value;
                Invalidate();
            }
        }

        [Category(PropertyCategory.Appearance)]
        [Description(PropertyDescription.TextAlign)]
        public StringAlignment TextAlignment
        {
            get
            {
                return _alignment;
            }

            set
            {
                _alignment = value;
                Invalidate();
            }
        }

        [Category(PropertyCategory.Appearance)]
        [Description(PropertyDescription.TextAlign)]
        public StringAlignment TextLineAlignment
        {
            get
            {
                return _lineAlignment;
            }

            set
            {
                _lineAlignment = value;
                Invalidate();
            }
        }

        #endregion

        #region Overrides

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics graphics = e.Graphics;

            Color _foreColor = Enabled ? ForeColor : TextStyle.Disabled;

            if (_reflection && (_orientation == Orientation.Vertical))
            {
                textBoxRectangle = new Rectangle(TextManager.MeasureText(Text, Font, graphics).Height, 0, ClientRectangle.Width, ClientRectangle.Height);
            }
            else
            {
                textBoxRectangle = new Rectangle(0, 0, ClientRectangle.Width, ClientRectangle.Height);
            }

            // Draw the text outline
            if (_outline)
            {
                VisualTextRenderer.RenderTextOutline(graphics, _orientation, Text, Font, _outlineColor, _outlineLocation);
            }

            // Draw the shadow
            if (_shadow)
            {
                VisualTextRenderer.RenderTextShadow(graphics, _orientation, Text, Font, _shadowColor, ClientRectangle, _shadowLocation, _shadowSmooth, _shadowDepth, _shadowDirection, shadowOpacity);
            }

            // Draw the reflection text.
            if (_reflection)
            {
                VisualTextRenderer.RenderTextReflection(graphics, _orientation, Text, Font, _reflectionColor, ClientRectangle, _reflectionSpacing, textBoxRectangle.Location, _alignment, _lineAlignment);
            }

            graphics.DrawString(Text, Font, new SolidBrush(_foreColor), textBoxRectangle, VisualTextRenderer.GetOrientedStringFormat(_orientation, _alignment, _lineAlignment));
        }

        #endregion

        #region Methods

        public void UpdateTheme(Theme theme)
        {
            try
            {
                ForeColor = theme.ColorPalette.TextEnabled;
                TextStyle.Enabled = theme.ColorPalette.TextEnabled;
                TextStyle.Disabled = theme.ColorPalette.TextDisabled;

                // Font = theme.ColorPalette.Font;
            }
            catch (Exception e)
            {
                ConsoleEx.WriteDebug(e);
            }

            Invalidate();
            OnThemeChanged(new ThemeEventArgs(theme));
        }

        #endregion
    }
}