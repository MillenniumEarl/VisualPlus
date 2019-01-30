#region License

// -----------------------------------------------------------------------------------------------------------
// 
// Name: VisualTextBox.cs
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
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Forms;

using VisualPlus.Constants;
using VisualPlus.Delegates;
using VisualPlus.Designer;
using VisualPlus.Enumerators;
using VisualPlus.Events;
using VisualPlus.Interfaces;
using VisualPlus.Localization;
using VisualPlus.Models;
using VisualPlus.Renders;
using VisualPlus.Toolkit.Components;
using VisualPlus.Toolkit.VisualBase;
using VisualPlus.TypeConverters;
using VisualPlus.Utilities;
using VisualPlus.Utilities.Debugging;

#endregion

namespace VisualPlus.Toolkit.Controls.Editors
{
    [ClassInterface(ClassInterfaceType.AutoDispatch)]
    [ComVisible(true)]
    [DefaultEvent("TextChanged")]
    [DefaultProperty("Text")]
    [Description("The Visual TextBox")]
    [Designer(typeof(VisualTextBoxDesigner))]
    [ToolboxBitmap(typeof(TextBox), "VisualTextBox.bmp")]
    [ToolboxItem(true)]
    public class VisualTextBox : ContainedControlBase, IInputMethods, IThemeSupport
    {
        #region Fields

        private ColorState _backColorState;
        private Border _border;
        private BorderEdge _borderButton;
        private BorderEdge _borderImage;
        private Border _buttonBorder;
        private ControlColorState _buttonColorState;
        private Font _buttonFont;
        private int _buttonIndent;
        private Rectangle _buttonRectangle;
        private string _buttontext;
        private bool _buttonVisible;
        private Image _image;
        private Rectangle _imageRectangle;
        private Size _imageSize;
        private bool _imageVisible;
        private int _imageWidth;
        private TextBoxExtended _textBox;
        private int _textWidth;
        private Watermark _watermark;
        private Panel _waterMarkContainer;
        private int _xValue;
        private int _yValue;

        #endregion

        #region Constructors and Destructors

        /// <summary>Initializes a new instance of the <see cref="VisualTextBox" /> class.</summary>
        public VisualTextBox()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.SupportsTransparentBackColor, true);

            // Contains another control
            SetStyle(ControlStyles.ContainerControl, true);

            // Cannot select this control, only the child and does not generate a click event
            SetStyle(ControlStyles.Selectable | ControlStyles.StandardClick, false);

            _borderButton = new BorderEdge { Visible = false };

            _borderImage = new BorderEdge { Visible = false };

            _textWidth = GetTextBoxWidth();
            _border = new Border();

            ThemeManager = new StyleManager(DefaultConstants.DefaultStyle);

            _backColorState = new ColorState { Enabled = ThemeManager.Theme.ColorPalette.ControlEnabled };

            _textBox = new TextBoxExtended
                {
                    Size = new Size(125, 25),
                    Location = new Point(_border.BorderCurve, _border.BorderCurve),
                    Text = string.Empty,
                    BorderStyle = BorderStyle.None,
                    TextAlign = HorizontalAlignment.Left,
                    Font = Font,
                    ForeColor = ForeColor,
                    BackColor = _backColorState.Enabled,
                    Multiline = false
                };

            _imageWidth = 35;
            _buttonFont = Font;
            _buttonIndent = 3;
            _buttontext = "visualButton";

            _image = null;
            _imageSize = new Size(16, 16);

            _watermark = new Watermark();
            _buttonBorder = new Border();

            _textBox.KeyDown += TextBox_KeyDown;
            _textBox.Leave += OnLeave;
            _textBox.Enter += OnEnter;
            _textBox.GotFocus += OnEnter;
            _textBox.LostFocus += OnLeave;
            _textBox.MouseLeave += OnLeave;
            _textBox.TextChanged += TextBox_TextChanged;
            _textBox.SizeChanged += TextBox_SizeChanged;

            Controls.Add(_textBox);
            Controls.Add(_borderButton);
            Controls.Add(_borderImage);

            _waterMarkContainer = null;

            Size = new Size(135, 25);

            if (_watermark.Visible)
            {
                DrawWaterMark();
            }

            UpdateTheme(ThemeManager.Theme);
        }

        #endregion

        #region Delegates

        public delegate void ButtonClickedEventHandler();

        #endregion

        #region Public Events

        public event ButtonClickedEventHandler ButtonClicked;

        [Browsable(true)]
        [Category(EventCategory.PropertyChanged)]
        [Description("Occurs when the clipboard text is copied.")]
        public event ClipboardEventHandler ClipboardCopy
        {
            add
            {
                _textBox.ClipboardCopy += value;
            }

            remove
            {
                _textBox.ClipboardCopy -= value;
            }
        }

        [Browsable(true)]
        [Category(EventCategory.PropertyChanged)]
        [Description("Occurs when the clipboard text is cut.")]
        public event ClipboardEventHandler ClipboardCut
        {
            add
            {
                _textBox.ClipboardCut += value;
            }

            remove
            {
                _textBox.ClipboardCut -= value;
            }
        }

        [Browsable(true)]
        [Category(EventCategory.PropertyChanged)]
        [Description("Occurs when the clipboard text is pasted.")]
        public event ClipboardEventHandler ClipboardPaste
        {
            add
            {
                _textBox.ClipboardPaste += value;
            }

            remove
            {
                _textBox.ClipboardPaste -= value;
            }
        }

        [Browsable(true)]
        [Description(EventDescription.PropertyEventChanged)]
        [Category(EventCategory.PropertyChanged)]
        public new event KeyEventHandler KeyDown
        {
            add
            {
                _textBox.KeyDown += value;
            }

            remove
            {
                _textBox.KeyDown -= value;
            }
        }

        [Browsable(true)]
        [Description(EventDescription.PropertyEventChanged)]
        [Category(EventCategory.PropertyChanged)]
        public new event KeyPressEventHandler KeyPress
        {
            add
            {
                _textBox.KeyPress += value;
            }

            remove
            {
                _textBox.KeyPress -= value;
            }
        }

        [Browsable(true)]
        [Description(EventDescription.PropertyEventChanged)]
        [Category(EventCategory.PropertyChanged)]
        public new event KeyEventHandler KeyUp
        {
            add
            {
                _textBox.KeyUp += value;
            }

            remove
            {
                _textBox.KeyUp -= value;
            }
        }

        [Browsable(true)]
        [Description(EventDescription.PropertyEventChanged)]
        [Category(EventCategory.PropertyChanged)]
        public new event PreviewKeyDownEventHandler PreviewKeyDown
        {
            add
            {
                _textBox.PreviewKeyDown += value;
            }

            remove
            {
                _textBox.PreviewKeyDown -= value;
            }
        }

        [Browsable(true)]
        [Description(EventDescription.TextChanged)]
        [Category(EventCategory.PropertyChanged)]
        public new event EventHandler TextChanged
        {
            add
            {
                _textBox.TextChanged += value;
            }

            remove
            {
                _textBox.TextChanged -= value;
            }
        }

        #endregion

        #region Public Properties

        /// <summary>Gets or sets the alpha numeric toggle, specifying whether to only accept numbers input.</summary>
        [Description(PropertyDescription.Toggle)]
        [Category(PropertyCategory.Behavior)]
        public bool AlphaNumeric
        {
            get
            {
                return _textBox.AlphaNumeric;
            }

            set
            {
                _textBox.AlphaNumeric = value;
            }
        }

        [Editor("System.Windows.Forms.Design.ListControlStringCollectionEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Localizable(true)]
        [Browsable(true)]
        [Category(PropertyCategory.Behavior)]
        [Description(PropertyDescription.AutoCompleteCustomSource)]
        public AutoCompleteStringCollection AutoCompleteCustomSource
        {
            get
            {
                return _textBox.AutoCompleteCustomSource;
            }

            set
            {
                _textBox.AutoCompleteCustomSource = value;
            }
        }

        [DefaultValue(typeof(AutoCompleteMode), "None")]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Browsable(true)]
        [Category(PropertyCategory.Behavior)]
        [Description(PropertyDescription.AutoCompleteMode)]
        public AutoCompleteMode AutoCompleteMode
        {
            get
            {
                return _textBox.AutoCompleteMode;
            }

            set
            {
                _textBox.AutoCompleteMode = value;
            }
        }

        [DefaultValue(typeof(AutoCompleteSource), "None")]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Browsable(true)]
        [Category(PropertyCategory.Appearance)]
        [Description(PropertyDescription.AutoCompleteSource)]
        public AutoCompleteSource AutoCompleteSource
        {
            get
            {
                return _textBox.AutoCompleteSource;
            }

            set
            {
                _textBox.AutoCompleteSource = value;
            }
        }

        [TypeConverter(typeof(VisualSettingsTypeConverter))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Category(PropertyCategory.Appearance)]
        public ColorState BackColorState
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
        public ControlColorState ButtonColor
        {
            get
            {
                return _buttonColorState;
            }

            set
            {
                _buttonColorState = value;
                Invalidate();
            }
        }

        [Description(PropertyDescription.Font)]
        [Category(PropertyCategory.Appearance)]
        public Font ButtonFont
        {
            get
            {
                return _buttonFont;
            }

            set
            {
                _buttonFont = value;
                Invalidate();
            }
        }

        [Description(PropertyDescription.Size)]
        [Category(PropertyCategory.Layout)]
        public int ButtonIndent
        {
            get
            {
                return _buttonIndent;
            }

            set
            {
                _buttonIndent = value;
                Invalidate();
            }
        }

        [Description(PropertyDescription.Text)]
        [Category(PropertyCategory.Appearance)]
        public string ButtonText
        {
            get
            {
                return _buttontext;
            }

            set
            {
                _buttontext = value;
                Invalidate();
            }
        }

        [Category(PropertyCategory.Appearance)]
        [Description(PropertyDescription.Visible)]
        public bool ButtonVisible
        {
            get
            {
                return _buttonVisible;
            }

            set
            {
                _buttonVisible = value;

                if (_buttonVisible)
                {
                    _borderButton.Visible = true;
                }
                else
                {
                    _borderButton.Visible = false;
                }

                Invalidate();
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Browsable(false)]
        [Description("Gets access to the contained control.")]
        public TextBoxExtended ContainedControl
        {
            get
            {
                return _textBox;
            }

            set
            {
                _textBox = value;
            }
        }

        public new Font Font
        {
            get
            {
                return base.Font;
            }

            set
            {
                _textBox.Font = value;
                base.Font = value;
            }
        }

        public new Color ForeColor
        {
            get
            {
                return base.ForeColor;
            }

            set
            {
                _textBox.ForeColor = value;
                base.ForeColor = value;
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
        [Description(PropertyDescription.Image)]
        public bool ImageVisible
        {
            get
            {
                return _imageVisible;
            }

            set
            {
                _imageVisible = value;

                if (_imageVisible)
                {
                    _borderImage.Visible = true;
                }
                else
                {
                    _borderImage.Visible = false;
                }

                Invalidate();
            }
        }

        [Category(PropertyCategory.Layout)]
        [Description(PropertyDescription.Size)]
        public int ImageWidth
        {
            get
            {
                return _imageWidth;
            }

            set
            {
                _imageWidth = value;
                Invalidate();
            }
        }

        [DefaultValue(32767)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Browsable(true)]
        [Category(PropertyCategory.Behavior)]
        [Description(PropertyDescription.MaxLength)]
        public int MaxLength
        {
            get
            {
                return _textBox.MaxLength;
            }

            set
            {
                _textBox.MaxLength = value;
            }
        }

        [EditorBrowsable(EditorBrowsableState.Always)]
        [Browsable(false)]
        [Description("Gets whether the mouse is on the button.")]
        public bool MouseOnButton { get; private set; }

        [Category(PropertyCategory.Behavior)]
        [Description(PropertyDescription.MultiLine)]
        [DefaultValue(false)]
        public virtual bool MultiLine
        {
            get
            {
                return _textBox.Multiline;
            }

            set
            {
                _textBox.Multiline = value;
                Invalidate();
            }
        }

        [DefaultValue('*')]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Browsable(true)]
        [Category(PropertyCategory.Behavior)]
        [Description(PropertyDescription.PasswordChar)]
        public char PasswordChar
        {
            get
            {
                return _textBox.PasswordChar;
            }

            set
            {
                _textBox.PasswordChar = value;
            }
        }

        [Category(PropertyCategory.Behavior)]
        [Description(PropertyDescription.ReadOnly)]
        public bool ReadOnly
        {
            get
            {
                return _textBox.ReadOnly;
            }

            set
            {
                _textBox.ReadOnly = value;
            }
        }

        [Category(PropertyCategory.Behavior)]
        [Description(PropertyDescription.ScrollBars)]
        public ScrollBars ScrollBars
        {
            get
            {
                return _textBox.ScrollBars;
            }

            set
            {
                _textBox.ScrollBars = value;
            }
        }

        [Bindable(true)]
        [Browsable(true)]
        [Category(PropertyCategory.Appearance)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Description(PropertyDescription.Text)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        [Localizable(false)]
        public new string Text
        {
            get
            {
                return _textBox.Text;
            }

            set
            {
                if (_textBox.Text != value)
                {
                    if (!MultiLine)
                    {
                        string text = StringUtil.RemoveLineBreaks(value);
                        value = text;
                    }

                    _textBox.Text = value;
                }

                if (_watermark.Visible)
                {
                    // If the text of the text box is not empty.
                    if (_textBox.TextLength > 0)
                    {
                        // Remove the watermark
                        RemoveWaterMark();
                    }
                    else
                    {
                        // But if the text is empty, draw the watermark again.
                        DrawWaterMark();
                    }
                }
            }
        }

        [DefaultValue(typeof(HorizontalAlignment), "Left")]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Browsable(true)]
        [Category(PropertyCategory.Behavior)]
        [Description(PropertyDescription.TextAlign)]
        public HorizontalAlignment TextAlign
        {
            get
            {
                return _textBox.TextAlign;
            }

            set
            {
                _textBox.TextAlign = value;
            }
        }

        [Category(PropertyCategory.Layout)]
        [Description(PropertyDescription.Size)]
        public int TextBoxWidth
        {
            get
            {
                return _textWidth;
            }

            set
            {
                _textBox.Width = value;
                _textWidth = value;
                Invalidate();
            }
        }

        /// <summary>Gets the length of text in the control.</summary>
        /// <returns>The number of characters contained in the text of the control.</returns>
        [Browsable(false)]
        public int TextLength
        {
            get
            {
                return _textBox.TextLength;
            }
        }

        [DefaultValue(false)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Browsable(true)]
        [Category(PropertyCategory.Behavior)]
        [Description(PropertyDescription.UseSystemPasswordChar)]
        public bool UseSystemPasswordChar
        {
            get
            {
                return _textBox.UseSystemPasswordChar;
            }

            set
            {
                _textBox.UseSystemPasswordChar = value;
            }
        }

        [TypeConverter(typeof(VisualSettingsTypeConverter))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Category(PropertyCategory.Behavior)]
        public Watermark Watermark
        {
            get
            {
                return _watermark;
            }

            set
            {
                _watermark = value;
                Invalidate();
            }
        }

        [Category(PropertyCategory.Behavior)]
        [Description(PropertyDescription.WordWrap)]
        public bool WordWrap
        {
            get
            {
                return _textBox.WordWrap;
            }

            set
            {
                _textBox.WordWrap = value;
                Invalidate();
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>Appends text to the current text of a rich text box.</summary>
        /// <param name="text">The text to append to the current contents of the text box.</param>
        public void AppendText(string text)
        {
            _textBox.AppendText(text);
        }

        /// <summary>Clears all text from the text box control.</summary>
        public void Clear()
        {
            _textBox.Clear();
        }

        /// <summary>Clears information about the most recent operation from the undo buffer of the rich text box.</summary>
        public void ClearUndo()
        {
            _textBox.ClearUndo();
        }

        /// <summary>Copies the current selection in the text box to the Clipboard.</summary>
        public void Copy()
        {
            _textBox.Copy();
        }

        /// <summary>Moves the current selection in the text box to the Clipboard.</summary>
        public void Cut()
        {
            _textBox.Cut();
        }

        /// <summary>
        ///     Specifies that the value of the SelectionLength property is zero so that no characters are selected in the
        ///     control.
        /// </summary>
        public void DeselectAll()
        {
            _textBox.DeselectAll();
        }

        /// <summary>Retrieves the character that is closest to the specified location within the control.</summary>
        /// <param name="pt">The location from which to seek the nearest character.</param>
        /// <returns>The character at the specified location.</returns>
        public int GetCharFromPosition(Point pt)
        {
            return _textBox.GetCharFromPosition(pt);
        }

        /// <summary>Retrieves the index of the character nearest to the specified location.</summary>
        /// <param name="pt">The location to search.</param>
        /// <returns>The zero-based character index at the specified location.</returns>
        public int GetCharIndexFromPosition(Point pt)
        {
            return _textBox.GetCharIndexFromPosition(pt);
        }

        /// <summary>Retrieves the index of the first character of a given line.</summary>
        /// <param name="lineNumber">The line for which to get the index of its first character.</param>
        /// <returns>The zero-based character index in the specified line.</returns>
        public int GetFirstCharIndexFromLine(int lineNumber)
        {
            return _textBox.GetFirstCharIndexFromLine(lineNumber);
        }

        /// <summary>Retrieves the index of the first character of the current line.</summary>
        /// <returns>The zero-based character index in the current line.</returns>
        public int GetFirstCharIndexOfCurrentLine()
        {
            return _textBox.GetFirstCharIndexOfCurrentLine();
        }

        /// <summary>Retrieves the line number from the specified character position within the text of the RichTextBox control.</summary>
        /// <param name="index">The character index position to search.</param>
        /// <returns>The zero-based line number in which the character index is located.</returns>
        public int GetLineFromCharIndex(int index)
        {
            return _textBox.GetLineFromCharIndex(index);
        }

        /// <summary>Retrieves the location within the control at the specified character index.</summary>
        /// <param name="index">The index of the character for which to retrieve the location.</param>
        /// <returns>The location of the specified character.</returns>
        public Point GetPositionFromCharIndex(int index)
        {
            return _textBox.GetPositionFromCharIndex(index);
        }

        /// <summary>Returns the internal text box width.</summary>
        /// <returns>The <see cref="int" />.</returns>
        public int GetTextBoxWidth()
        {
            // 5px for each side from the VisualTextBox control border.
            int i = Size.Width - 10;

            // Remove extra pixels if image is visible.
            if (_imageVisible)
            {
                i = i - _imageWidth - (_border.Distance * 2);
            }

            return i;
        }

        /// <summary>Replaces the current selection in the text box with the contents of the Clipboard.</summary>
        public void Paste()
        {
            _textBox.Paste();
        }

        /// <summary>Scrolls the contents of the control to the current caret position.</summary>
        public void ScrollToCaret()
        {
            _textBox.ScrollToCaret();
        }

        /// <summary>Selects a range of text in the control.</summary>
        /// <param name="start">The position of the first character in the current text selection within the text box.</param>
        /// <param name="length">The number of characters to select.</param>
        public void Select(int start, int length)
        {
            _textBox.Select(start, length);
        }

        /// <summary>Selects all text in the control.</summary>
        public void SelectAll()
        {
            _textBox.SelectAll();
        }

        /// <summary>Undoes the last edit operation in the text box.</summary>
        public void Undo()
        {
            _textBox.Undo();
        }

        public void UpdateTheme(Theme theme)
        {
            try
            {
                _border.Color = theme.ColorPalette.BorderNormal;
                _border.HoverColor = theme.ColorPalette.BorderHover;

                ForeColor = theme.ColorPalette.TextEnabled;
                TextStyle.Enabled = theme.ColorPalette.TextEnabled;
                TextStyle.Disabled = theme.ColorPalette.TextDisabled;

                _buttonColorState = new ControlColorState { Disabled = theme.ColorPalette.Disabled, Enabled = theme.ColorPalette.Enabled, Hover = theme.ColorPalette.Hover, Pressed = theme.ColorPalette.Pressed };

                _backColorState = new ColorState { Enabled = theme.ColorPalette.ControlEnabled, Disabled = theme.ColorPalette.ControlDisabled };

                _borderButton.BackColor = theme.ColorPalette.BorderNormal;
                _borderImage.BackColor = theme.ColorPalette.BorderNormal;
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

        protected override void CreateHandle()
        {
            base.CreateHandle();
            AutoSize = true;
        }

        protected override void OnEnter(EventArgs e)
        {
            base.OnEnter(e);

            if (_watermark.Visible)
            {
                // If focused use focus color
                _watermark.Brush = new SolidBrush(_watermark.Active);

                // Don't draw watermark if contains text.
                if (_textBox.TextLength <= 0)
                {
                    RemoveWaterMark();
                    DrawWaterMark();
                }
            }
        }

        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
            _textBox.Focus();
        }

        protected override void OnInvalidated(InvalidateEventArgs e)
        {
            base.OnInvalidated(e);

            // Check if there is a watermark
            if (_waterMarkContainer != null)
            {
                // if there is a watermark it should also be invalidated();
                _waterMarkContainer.Invalidate();
            }
        }

        protected override void OnLeave(EventArgs e)
        {
            base.OnLeave(e);

            MouseState = MouseStates.Normal;

            if (_watermark.Visible)
            {
                // If the user has written something and left the control
                if (_textBox.TextLength > 0)
                {
                    // Remove the watermark
                    RemoveWaterMark();
                }
                else
                {
                    // But if the user didn't write anything, then redraw the control.
                    Invalidate();
                }
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            OnMouseClick(e);

            if (MouseOnButton)
            {
                MouseState = MouseStates.Pressed;
            }
            else
            {
                MouseState = MouseStates.Hover;
            }

            if (_borderButton.Visible)
            {
                // Check if mouse in X position.
                if ((_xValue > _buttonRectangle.X) && (_xValue < Width))
                {
                    // Determine the button middle separator by checking for the Y position.
                    if ((_yValue > _buttonRectangle.Y) && (_yValue < Height))
                    {
                        ButtonClicked?.Invoke();
                    }
                }
            }

            Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);

            if (!_textBox.Focused)
            {
                MouseState = MouseStates.Normal;
                Invalidate();
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            _xValue = e.Location.X;
            _yValue = e.Location.Y;
            MouseOnButton = MouseManager.IsMouseInBounds(e.Location, _buttonRectangle);

            Invalidate();

            // IBeam cursor toggle
            if ((e.X > _textBox.Location.X) && (e.X < _textBox.Right))
            {
                Cursor = Cursors.IBeam;
            }
            else
            {
                Cursor = Cursors.Default;
            }
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
            Graphics graphics = e.Graphics;
            Rectangle _clientRectangle = new Rectangle(ClientRectangle.X, ClientRectangle.Y, ClientRectangle.Width - 1, ClientRectangle.Height - 1);
            ControlGraphicsPath = VisualBorderRenderer.CreateBorderTypePath(_clientRectangle, _border);

            Color _backColor = Enabled ? _backColorState.Enabled : _backColorState.Disabled;

            if (_textBox.BackColor != _backColor)
            {
                _textBox.BackColor = _backColor;
            }

            graphics.SetClip(ControlGraphicsPath);

            VisualBackgroundRenderer.DrawBackground(graphics, _backColor, BackgroundImage, MouseState, _clientRectangle, _border);

            _buttonRectangle = new Rectangle(_textBox.Right, _border.Thickness, Width - _textBox.Right - _border.Thickness, Height);
            _imageRectangle = new Rectangle(0, 0, _imageWidth, Height);

            if (!_textBox.Multiline)
            {
                if (_borderImage.Visible)
                {
                    _textBox.Location = new Point(_border.BorderCurve + _imageRectangle.Width, _textBox.Location.Y);

                    DrawImage(graphics);

                    if (_borderButton.Visible)
                    {
                        DrawButton(graphics);
                    }
                }
                else
                {
                    _textBox.Location = new Point(_border.BorderCurve, _textBox.Location.Y);

                    if (_borderButton.Visible)
                    {
                        DrawButton(graphics);
                    }
                }
            }

            graphics.ResetClip();

            if (_watermark.Visible)
            {
                DrawWaterMark();
            }

            VisualBorderRenderer.DrawBorderStyle(e.Graphics, _border, ControlGraphicsPath, MouseState);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            if (!_textBox.Multiline)
            {
                if (_borderImage.Visible)
                {
                    _textBox.Location = new Point(_border.BorderCurve + _imageWidth, _textBox.Location.Y);
                }
                else
                {
                    _textBox.Location = new Point(_border.BorderCurve, _textBox.Location.Y);
                }

                if ((!_borderImage.Visible & !_borderButton.Visible) && AutoSize)
                {
                    _textBox.Width = GetInternalControlSize(Size, _border).Width;
                }

                _textBox.Height = GetTextBoxHeight();
                Size = new Size(Width, _border.BorderCurve + _textBox.Height + _border.BorderCurve);
            }
            else
            {
                _textBox.Location = GetInternalControlLocation(_border);
                _textBox.Size = GetInternalControlSize(Size, _border);
            }

            Invalidate();
        }

        /// <summary>Draws the button.</summary>
        /// <param name="graphics">The specified graphics to draw on.</param>
        private void DrawButton(Graphics graphics)
        {
            Color _buttonColor;

            if (Enabled)
            {
                switch (MouseState)
                {
                    case MouseStates.Normal:
                        {
                            _buttonColor = _buttonColorState.Enabled;
                            break;
                        }

                    case MouseStates.Hover:
                        {
                            _buttonColor = _buttonColorState.Hover;
                            break;
                        }

                    case MouseStates.Pressed:
                        {
                            _buttonColor = _buttonColorState.Pressed;
                            break;
                        }

                    default:
                        {
                            throw new ArgumentOutOfRangeException();
                        }
                }
            }
            else
            {
                _buttonColor = Enabled ? _buttonColorState.Enabled : _buttonColorState.Disabled;
            }

            GraphicsPath buttonPath = new GraphicsPath();
            buttonPath.AddRectangle(_buttonRectangle);

            graphics.SetClip(ControlGraphicsPath);
            graphics.FillPath(new SolidBrush(_buttonColor), buttonPath);

            _borderButton.Location = new Point(_buttonRectangle.X, _border.Thickness);
            _borderButton.Size = new Size(1, Height - _border.Thickness - 1);

            Size textSize = StringUtil.MeasureText(_buttontext, _buttonFont, graphics);
            graphics.SetClip(buttonPath);
            graphics.DrawString(_buttontext, Font, new SolidBrush(ForeColor), new PointF(_buttonRectangle.X + _buttonIndent, (Height / 2) - (textSize.Height / 2)));
            graphics.ResetClip();
        }

        /// <summary>Draws the image.</summary>
        /// <param name="graphics">The specified graphics to draw on.</param>
        private void DrawImage(Graphics graphics)
        {
            if (!_borderImage.Visible)
            {
                return;
            }

            _borderImage.Location = new Point(_imageRectangle.Right, _border.Thickness);
            _borderImage.Size = new Size(1, Height - _border.Thickness - 1);

            GraphicsPath _imagePath = new GraphicsPath();
            _imagePath.AddRectangle(_imageRectangle);
            graphics.SetClip(_imagePath);

            if (_image != null)
            {
                int _xLocation = (_imageRectangle.X + (_imageRectangle.Width / 2)) - (_imageSize.Width / 2);
                int _yLocation = (_imageRectangle.Y + (_imageRectangle.Height / 2)) - (_imageSize.Height / 2);

                Rectangle _imageFinalRectangle = new Rectangle(_xLocation, _yLocation, _imageSize.Width, _imageSize.Height);

                graphics.DrawImage(_image, _imageFinalRectangle);
            }

            graphics.ResetClip();
        }

        /// <summary>Draws the watermark.</summary>
        private void DrawWaterMark()
        {
            if ((_waterMarkContainer == null) && (_textBox.TextLength <= 0))
            {
                _waterMarkContainer = new Panel(); // Creates the new panel instance
                _waterMarkContainer.Paint += WaterMarkContainer_Paint;
                _waterMarkContainer.Invalidate();
                _waterMarkContainer.Click += WaterMarkContainer_Click;
                _textBox.Controls.Add(_waterMarkContainer); // adds the control
                _waterMarkContainer.BringToFront();
            }
        }

        private int GetTextBoxHeight()
        {
            if (_textBox.TextLength > 0)
            {
                return StringUtil.MeasureTextRenderer(Text, Font).Height;
            }
            else
            {
                return StringUtil.MeasureTextRenderer("Hello World.", Font).Height;
            }
        }

        private void OnEnter(object sender, EventArgs e)
        {
            MouseState = MouseStates.Hover;
        }

        private void OnLeave(object sender, EventArgs e)
        {
            if (!_textBox.Focused)
            {
                MouseState = MouseStates.Normal;
            }
        }

        /// <summary>Removes the watermark.</summary>
        private void RemoveWaterMark()
        {
            if (_waterMarkContainer != null)
            {
                _textBox.Controls.Remove(_waterMarkContainer);
                _waterMarkContainer = null;
            }
        }

        private void TextBox_KeyDown(object obj, KeyEventArgs e)
        {
            // Select all
            if (e.Control && (e.KeyCode == Keys.A))
            {
                _textBox.SelectAll();
                e.SuppressKeyPress = true;
            }

            // Copy
            if (e.Control && (e.KeyCode == Keys.C))
            {
                _textBox.Copy();
                e.SuppressKeyPress = true;
            }
        }

        private void TextBox_SizeChanged(object sender, EventArgs e)
        {
            _textWidth = _textBox.Width;
        }

        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            if (_watermark.Visible)
            {
                // If the text of the text box is not empty.
                if (_textBox.TextLength > 0)
                {
                    // Remove the watermark
                    RemoveWaterMark();
                }
                else
                {
                    // But if the text is empty, draw the watermark again.
                    DrawWaterMark();
                }
            }
        }

        private void WaterMarkContainer_Click(object sender, EventArgs e)
        {
            _textBox.Focus();
        }

        private void WaterMarkContainer_Paint(object sender, PaintEventArgs e)
        {
            // Configure the watermark
            _waterMarkContainer.Location = new Point(0, 0);
            _waterMarkContainer.Height = _textBox.Height;
            _waterMarkContainer.Width = _textBox.Width;

            // Forces it to resize with the parent control
            _waterMarkContainer.Anchor = AnchorStyles.Left | AnchorStyles.Right;

            // Set color
            _watermark.Brush = ContainsFocus ? new SolidBrush(_watermark.Active) : new SolidBrush(_watermark.Inactive);

            // Draws the string on the panel
            e.Graphics.DrawString(_watermark.Text, _watermark.Font, _watermark.Brush, new PointF(0F, 0F));
        }

        #endregion
    }
}