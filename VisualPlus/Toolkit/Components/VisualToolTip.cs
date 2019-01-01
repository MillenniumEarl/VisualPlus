#region Namespace

using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;

using VisualPlus.Designer;
using VisualPlus.Interfaces;
using VisualPlus.Localization;
using VisualPlus.Structure;
using VisualPlus.TypeConverters;

#endregion

namespace VisualPlus.Toolkit.Components
{
    [ClassInterface(ClassInterfaceType.AutoDispatch)]
    [ComVisible(true)]
    [DefaultEvent("Popup")]
    [DefaultProperty("Text")]
    [Description("The Visual Tool Tip")]
    [Designer(typeof(VisualToolTipDesigner))]
    [ToolboxBitmap(typeof(VisualToolTip), "VisualToolTip.bmp")]
    [ToolboxItem(true)]
    [ToolboxItemFilter("System.Windows.Forms")]
    public class VisualToolTip : ToolTip, IToolTip
    {
        #region Variables

        private bool _autoSize;
        private Color _background;
        private Border _border;
        private Font _font;
        private bool _iconBorder;
        private GraphicsPath _iconGraphicsPath;
        private Point _iconPoint;
        private Rectangle _iconRectangle;
        private Size _iconSize;
        private Color _lineColor;
        private Padding _padding;
        private Rectangle _separator;
        private int _separatorThickness;
        private int _spacing;
        private Point _textPoint;
        private TextRenderingHint _textRendererHint;
        private bool _textShadow;
        private TipInfo _tipInfo;
        private Point _tipSize;
        private Color _titleColor;
        private Font _titleFont;
        private Point _titlePoint;
        private Size _toolTipSize;
        private Hashtable _toolTipTexts;

        #endregion

        #region Constructors

        /// <summary>Initializes a new instance of the <see cref="VisualToolTip" /> class.</summary>
        public VisualToolTip()
        {
            try
            {
                StyleManager styleManager = new StyleManager(Settings.DefaultValue.DefaultStyle);

                _toolTipTexts = new Hashtable();
                _iconPoint = new Point(0, 0);
                _iconSize = new Size(16, 16);
                _padding = new Padding(4, 4, 4, 4);
                _separatorThickness = 1;
                _titleColor = Color.Gray;
                _toolTipSize = new Size(100, 40);
                _tipSize = new Point();

                _tipInfo = new TipInfo { Caption = "Title", Text = "Enter your custom text here." };

                _spacing = 2;
                _textRendererHint = Settings.DefaultValue.TextRenderingHint;
                _background = styleManager.Theme.ColorPalette.Enabled;
                _font = SystemFonts.DefaultFont;
                _autoSize = true;
                ForeColor = styleManager.Theme.ColorPalette.TextEnabled;
                _lineColor = styleManager.Theme.ColorPalette.BorderNormal;
                _titleFont = SystemFonts.DefaultFont;

                _border = new Border();

                ShowAlways = true;
                IsBalloon = false;
                OwnerDraw = true;
                Popup += VisualToolTip_Popup;
                Draw += VisualToolTip_Draw;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        /// <summary>Finalizes an instance of the <see cref="VisualToolTip" /> class.</summary>
        ~VisualToolTip()
        {
            Dispose(false);
        }

        #endregion

        #region Properties

        [Category(PropertyCategory.Behavior)]
        [Description(PropertyDescription.AutoSize)]
        public bool AutoSize
        {
            get
            {
                return _autoSize;
            }

            set
            {
                _autoSize = value;
            }
        }

        [Category(PropertyCategory.Appearance)]
        [Description(PropertyDescription.Color)]
        public Color Background
        {
            get
            {
                return _background;
            }

            set
            {
                _background = value;
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
            }
        }

        [Category(PropertyCategory.Appearance)]
        [Description(PropertyDescription.Font)]
        public Font Font
        {
            get
            {
                return _font;
            }

            set
            {
                _font = value;
            }
        }

        [Category(PropertyCategory.Appearance)]
        [Description(PropertyDescription.Visible)]
        public bool IconBorder
        {
            get
            {
                return _iconBorder;
            }

            set
            {
                _iconBorder = value;
            }
        }

        [Category(PropertyCategory.Layout)]
        [Description(PropertyDescription.Size)]
        public Size IconSize
        {
            get
            {
                return _iconSize;
            }

            set
            {
                _iconSize = value;
            }
        }

        [Category(PropertyCategory.Appearance)]
        [Description(PropertyDescription.Image)]
        public Image Image
        {
            get
            {
                return _tipInfo.Image;
            }

            set
            {
                _tipInfo.Image = value;
            }
        }

        [Category(PropertyCategory.Appearance)]
        [Description(PropertyDescription.Color)]
        public Color LineColor
        {
            get
            {
                return _lineColor;
            }

            set
            {
                _lineColor = value;
            }
        }

        [Category(PropertyCategory.Appearance)]
        [Description(PropertyDescription.Padding)]
        public Padding Padding
        {
            get
            {
                return _padding;
            }

            set
            {
                _padding = value;
            }
        }

        [Category(PropertyCategory.Appearance)]
        [Description(PropertyDescription.Size)]
        public int SeparatorThickness
        {
            get
            {
                return _separatorThickness;
            }

            set
            {
                _separatorThickness = value;
            }
        }

        [Category(PropertyCategory.Appearance)]
        [Description(PropertyDescription.Spacing)]
        public int Spacing
        {
            get
            {
                return _spacing;
            }

            set
            {
                _spacing = value;
            }
        }

        [Category(PropertyCategory.Appearance)]
        [Description(PropertyDescription.Text)]
        public string Text
        {
            get
            {
                return _tipInfo.Text;
            }

            set
            {
                _tipInfo.Text = value;
            }
        }

        [Category(PropertyCategory.Appearance)]
        [Description(PropertyDescription.TextRenderingHint)]
        public TextRenderingHint TextRendering
        {
            get
            {
                return _textRendererHint;
            }

            set
            {
                _textRendererHint = value;
            }
        }

        [Category(PropertyCategory.Appearance)]
        [Description(PropertyDescription.Visible)]
        public bool TextShadow
        {
            get
            {
                return _textShadow;
            }

            set
            {
                _textShadow = value;
            }
        }

        [Browsable(false)]
        public TipInfo TipInfo
        {
            get
            {
                return _tipInfo;
            }

            set
            {
                _tipInfo = value;
            }
        }

        [Category(PropertyCategory.Appearance)]
        [Description(PropertyDescription.Type)]
        public TipInfo.ToolTipType TipType
        {
            get
            {
                return _tipInfo.Type;
            }

            set
            {
                _tipInfo.Type = value;
            }
        }

        [Category(PropertyCategory.Appearance)]
        [Description(PropertyDescription.Color)]
        public Color TitleColor
        {
            get
            {
                return _titleColor;
            }

            set
            {
                _titleColor = value;
            }
        }

        [Category(PropertyCategory.Appearance)]
        [Description(PropertyDescription.Font)]
        public Font TitleFont
        {
            get
            {
                return _titleFont;
            }

            set
            {
                _titleFont = value;
            }
        }

        [Browsable(false)]
        public Control ToolTipControl
        {
            get
            {
                return _tipInfo.Control;
            }

            set
            {
                _tipInfo.Control = value;
            }
        }

        [Category(PropertyCategory.Appearance)]
        [Description(PropertyDescription.Size)]
        public Size ToolTipSize
        {
            get
            {
                return _toolTipSize;
            }

            set
            {
                _toolTipSize = value;
            }
        }

        [Category(PropertyCategory.Appearance)]
        [Description(PropertyDescription.Text)]
        public new string ToolTipTitle
        {
            get
            {
                return _tipInfo.Caption;
            }

            set
            {
                _tipInfo.Caption = value;
            }
        }

        #endregion

        #region Overrides

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _toolTipTexts.Clear();
                _toolTipTexts = null;
            }

            base.Dispose(disposing);
        }

        #endregion

        #region Methods

        public new string GetToolTip(Control control)
        {
            // Safety checks
            if (control == null)
            {
                return string.Empty;
            }

            return base.GetToolTip(control);
        }

        public new void SetToolTip(Control control, string caption)
        {
            // Safety checks
            if (control == null)
            {
                return;
            }

            if (caption == null)
            {
                caption = string.Empty;
            }

            // Checks whether the tool text has been cleared, remove the control from the list.
            if (string.IsNullOrEmpty(caption))
            {
                _toolTipTexts.Remove(control);
                _tipInfo.Control = control;
            }
            else
            {
                _tipInfo.Control = control;
                _tipInfo.Position = control.ClientRectangle.Location;
                _tipInfo.Size = control.ClientRectangle.Size;
                _tipInfo.Caption = caption;

                if (_toolTipTexts.Contains(control))
                {
                    _toolTipTexts[control] = caption;
                }
                else
                {
                    _toolTipTexts.Add(control, caption);
                    base.SetToolTip(control, caption);
                }
            }
        }

        public new void Show(string text, IWin32Window window)
        {
            if (window == null)
            {
                throw new ArgumentException(nameof(window));
            }

            _tipInfo.Control = window as Control;
            SetToolTip(_tipInfo.Control, text);
        }

        public new void Show(string text, IWin32Window window, int duration)
        {
            AutoPopDelay = duration;
            Show(text, window);
        }

        /// <summary>Input the text height to compare it to the icon height.</summary>
        /// <param name="textHeight">The text height.</param>
        /// <returns>The <see cref="int" />.</returns>
        private int GetTipHeight(int textHeight)
        {
            int tipHeight = textHeight > _iconSize.Height ? textHeight : _iconSize.Height;
            return tipHeight;
        }

        /// <summary>Input the title and text width to retrieve total width.</summary>
        /// <param name="titleWidth">The title width.</param>
        /// <param name="textWidth">The text width.</param>
        /// <returns>The <see cref="int" />.</returns>
        private int GetTipWidth(int titleWidth, int textWidth)
        {
            int tipWidth = titleWidth > _iconSize.Width + textWidth ? titleWidth : _iconSize.Width + textWidth;
            return tipWidth;
        }

        private void VisualToolTip_Draw(object sender, DrawToolTipEventArgs e)
        {
            Graphics graphics = e.Graphics;
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            graphics.TextRenderingHint = _textRendererHint;

            graphics.FillRectangle(new SolidBrush(_background), new Rectangle(_tipInfo.Position, _tipInfo.Size));

            if (_border.Visible)
            {
                Rectangle boxRectangle = new Rectangle(_tipInfo.Position.X, _tipInfo.Position.Y, _tipInfo.Size.Width - 1, _tipInfo.Size.Height - 1);
                GraphicsPath borderPath = new GraphicsPath();
                borderPath.AddRectangle(boxRectangle);
                graphics.DrawPath(new Pen(_border.Color, _border.Thickness), borderPath);
            }

            if ((_textShadow && (_tipInfo.Type == TipInfo.ToolTipType.Text)) || (_textShadow && (_tipInfo.Type == TipInfo.ToolTipType.Default)))
            {
                // Draw shadow text
                graphics.DrawString(_tipInfo.Text, new Font(Font, FontStyle.Regular), Brushes.Silver, new PointF(_textPoint.X + 1, _textPoint.Y + 1));
            }

            switch (_tipInfo.Type)
            {
                case TipInfo.ToolTipType.Default:
                    {
                        // Draw the title
                        graphics.DrawString(_tipInfo.Caption, _titleFont, new SolidBrush(_titleColor), new PointF(_titlePoint.X, _titlePoint.Y));

                        // Draw the separator
                        graphics.DrawLine(new Pen(_lineColor), _separator.X, _separator.Y, _separator.Width, _separator.Y);

                        // Draw the text
                        graphics.DrawString(_tipInfo.Text, Font, new SolidBrush(ForeColor), new PointF(_textPoint.X, _textPoint.Y));

                        if (_tipInfo.Image != null)
                        {
                            // Update point
                            _iconRectangle.Location = _iconPoint;

                            // Draw icon border
                            if (_iconBorder)
                            {
                                graphics.DrawPath(new Pen(_border.Color), _iconGraphicsPath);
                            }

                            // Draw icon
                            graphics.DrawImage(Image, _iconRectangle);
                        }

                        break;
                    }

                case TipInfo.ToolTipType.Image:
                    {
                        if (_tipInfo.Image != null)
                        {
                            // Update point
                            _iconRectangle.Location = _iconPoint;

                            // Draw icon border
                            if (_iconBorder)
                            {
                                graphics.DrawPath(new Pen(_border.Color), _iconGraphicsPath);
                            }

                            // Draw icon
                            graphics.DrawImage(Image, _iconRectangle);
                        }

                        break;
                    }

                case TipInfo.ToolTipType.Text:
                    {
                        // Draw the text
                        graphics.DrawString(_tipInfo.Text, Font, new SolidBrush(ForeColor), new PointF(_textPoint.X, _textPoint.Y));
                        break;
                    }

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void VisualToolTip_Popup(object sender, PopupEventArgs e)
        {
            switch (_tipInfo.Type)
            {
                case TipInfo.ToolTipType.Default:
                    {
                        if (!_autoSize)
                        {
                            _tipSize.X = _toolTipSize.Width;
                            _tipSize.Y = _toolTipSize.Height;
                        }
                        else
                        {
                            _tipSize.X = GetTipWidth(TextRenderer.MeasureText(_tipInfo.Caption, Font).Width, TextRenderer.MeasureText(_tipInfo.Text, Font).Width);
                            _tipSize.Y = TextRenderer.MeasureText(_tipInfo.Caption, Font).Height + SeparatorThickness + GetTipHeight(TextRenderer.MeasureText(_tipInfo.Text, Font).Height);
                        }

                        _titlePoint.X = _padding.Left;
                        _titlePoint.Y = _padding.Top;

                        Point separatorPoint = new Point(_padding.Left + Spacing, TextRenderer.MeasureText(_tipInfo.Caption, Font).Height + 5);
                        Size separatorSize = new Size(_tipSize.X, SeparatorThickness);
                        _separator = new Rectangle(separatorPoint, separatorSize);

                        _textPoint.X = _padding.Left + _iconSize.Width + Spacing;
                        _textPoint.Y = _separator.Y + Spacing;

                        _iconPoint = new Point(_padding.Left, _textPoint.Y);
                        break;
                    }

                case TipInfo.ToolTipType.Image:
                    {
                        _iconPoint = new Point(_padding.Left, _padding.Top);
                        _tipSize.X = _iconSize.Width + 1;
                        _tipSize.Y = _iconSize.Height + 1;
                        break;
                    }

                case TipInfo.ToolTipType.Text:
                    {
                        _textPoint = new Point(_padding.Left, _padding.Top);
                        _tipSize.X = TextRenderer.MeasureText(_tipInfo.Text, Font).Width;
                        _tipSize.Y = TextRenderer.MeasureText(_tipInfo.Text, Font).Height;
                        break;
                    }

                default:
                    throw new ArgumentOutOfRangeException();
            }

            // Create icon rectangle
            _iconRectangle = new Rectangle(_iconPoint, _iconSize);

            // Create icon path
            _iconGraphicsPath = new GraphicsPath();
            _iconGraphicsPath.AddRectangle(_iconRectangle);
            _iconGraphicsPath.CloseAllFigures();

            // Initialize new size
            e.ToolTipSize = new Size(_padding.Left + _tipSize.X + _padding.Right, _padding.Top + _tipSize.Y + _padding.Bottom);
        }

        #endregion
    }
}