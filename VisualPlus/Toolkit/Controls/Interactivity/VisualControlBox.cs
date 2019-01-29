#region License

// -----------------------------------------------------------------------------------------------------------
// 
// Name: VisualControlBox.cs
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
using System.Runtime.InteropServices;
using System.Windows.Forms;

using VisualPlus.Constants;
using VisualPlus.Delegates;
using VisualPlus.Designer;
using VisualPlus.Events;
using VisualPlus.Interfaces;
using VisualPlus.Localization;
using VisualPlus.Models;
using VisualPlus.Toolkit.Dialogs;
using VisualPlus.Toolkit.VisualBase;
using VisualPlus.TypeConverters;
using VisualPlus.Utilities.Debugging;

#endregion

namespace VisualPlus.Toolkit.Controls.Interactivity
{
    [ClassInterface(ClassInterfaceType.AutoDispatch)]
    [ComVisible(true)]
    [DefaultEvent("Click")]
    [DefaultProperty("MaximizeButton")]
    [Description("The Visual ControlBox")]
    [Designer(typeof(VisualControlBoxDesigner))]
    [ToolboxBitmap(typeof(VisualControlBox), "VisualControlBox.bmp")]
    [ToolboxItem(true)]
    [TypeConverter(typeof(VisualSettingsTypeConverter))]
    public class VisualControlBox : VisualStyleBase, IThemeSupport
    {
        #region Fields

        protected ControlBoxButton _closeButton;
        protected ControlBoxButton _helpButton;
        protected ControlBoxButton _maximizeButton;
        protected ControlBoxButton _minimizeButton;

        private Size _buttonSize;
        private bool _initialized;

        #endregion

        #region Constructors and Destructors

        /// <summary>Initializes a new instance of the <see cref="VisualControlBox" /> class.</summary>
        public VisualControlBox()
        {
            SetStyle(
                ControlStyles.ResizeRedraw |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.SupportsTransparentBackColor,
                true);

            InitializeControlBox();

            UpdateTheme(ThemeManager.Theme);
        }

        #endregion

        #region Public Events

        [Category(EventCategory.PropertyChanged)]
        [Description(EventDescription.PropertyEventChanged)]
        public event ControlBoxEventHandler CloseClick;

        [Category(EventCategory.PropertyChanged)]
        [Description(EventDescription.PropertyEventChanged)]
        public event ControlBoxEventHandler HelpClick;

        [Category(EventCategory.PropertyChanged)]
        [Description(EventDescription.PropertyEventChanged)]
        public event ControlBoxEventHandler MaximizeClick;

        [Category(EventCategory.PropertyChanged)]
        [Description(EventDescription.PropertyEventChanged)]
        public event ControlBoxEventHandler MaximizedClick;

        [Category(EventCategory.PropertyChanged)]
        [Description(EventDescription.PropertyEventChanged)]
        public event ControlBoxEventHandler MinimizeClick;

        [Category(EventCategory.PropertyChanged)]
        [Description(EventDescription.PropertyEventChanged)]
        public event ControlBoxEventHandler RestoredFormWindow;

        #endregion

        #region Public Properties

        [TypeConverter(typeof(VisualSettingsTypeConverter))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public ControlBoxButton CloseButton
        {
            get
            {
                return _closeButton;
            }

            set
            {
                _closeButton = value;
            }
        }

        [TypeConverter(typeof(VisualSettingsTypeConverter))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public ControlBoxButton HelpButton
        {
            get
            {
                return _helpButton;
            }

            set
            {
                _helpButton = value;
            }
        }

        [TypeConverter(typeof(VisualSettingsTypeConverter))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public ControlBoxButton MaximizeButton
        {
            get
            {
                return _maximizeButton;
            }

            set
            {
                _maximizeButton = value;
            }
        }

        [TypeConverter(typeof(VisualSettingsTypeConverter))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public ControlBoxButton MinimizeButton
        {
            get
            {
                return _minimizeButton;
            }

            set
            {
                _minimizeButton = value;
            }
        }

        /// <summary>Gets the parent form.</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public Form ParentForm
        {
            get
            {
                return Parent.FindForm();
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>Toggles the form window state.</summary>
        /// <param name="windowState">The form window state.</param>
        /// <returns>The <see cref="FormWindowState" />.</returns>
        public static FormWindowState ToggleFormWindowState(FormWindowState windowState)
        {
            return windowState == FormWindowState.Normal ? FormWindowState.Maximized : FormWindowState.Normal;
        }

        /// <summary>Automatically places the <see cref="VisualControlBox" /> on the <see cref="Form" /> corner location.</summary>
        /// <param name="spacing">The spacing.</param>
        public void AutoPlaceOnForm(int spacing)
        {
            Location = new Point(((ParentForm.Location.X + ParentForm.Width) - Width) + spacing, 0);
        }

        /// <summary>Closes the form.</summary>
        /// <param name="form">The form.</param>
        public void CloseForm(Form form)
        {
            if (form == null)
            {
                form = ParentForm;
            }

            CloseClick?.Invoke(new ControlBoxEventArgs(form));
            form.Close();
        }

        /// <summary>Maximizes the form.</summary>
        /// <param name="form">The form.</param>
        public void MaximizeForm(Form form)
        {
            if (form == null)
            {
                form = ParentForm;
            }

            if (form.WindowState == FormWindowState.Normal)
            {
                if (form is VisualForm visualForm)
                {
                    if (!visualForm.MaximizeBox)
                    {
                        // Disabled maximizing.
                        return;
                    }
                }
                else
                {
                    if (!form.MaximizeBox)
                    {
                        // Disabled maximizing.
                        return;
                    }
                }

                if (_maximizeButton.BoxType == ControlBoxButton.ControlBoxType.Default)
                {
                    _maximizeButton.Text = ControlBoxConstants.RestoreText;
                }

                form.WindowState = ToggleFormWindowState(form.WindowState);
                MaximizeButton.Invalidate();
                OnMaximizedClick(new ControlBoxEventArgs(form));
            }
        }

        /// <summary>Minimizes the form.</summary>
        /// <param name="form">The form.</param>
        public void MinimizeForm(Form form)
        {
            if (form == null)
            {
                form = ParentForm;
            }

            form.WindowState = FormWindowState.Minimized;
            MinimizeClick?.Invoke(new ControlBoxEventArgs(form));
        }

        /// <summary>Restores the form window.</summary>
        /// <param name="form">The form.</param>
        public void RestoreFormWindow(Form form)
        {
            if (form == null)
            {
                form = ParentForm;
            }

            if (form.WindowState == FormWindowState.Maximized)
            {
                if (form is VisualForm visualForm)
                {
                    if (!visualForm.MaximizeBox)
                    {
                        // Disabled restoring form.
                        return;
                    }
                }
                else
                {
                    if (!form.MaximizeBox)
                    {
                        // Disabled restoring form.
                        return;
                    }
                }

                if (_maximizeButton.BoxType == ControlBoxButton.ControlBoxType.Default)
                {
                    _maximizeButton.Text = ControlBoxConstants.MaximizeText;
                }

                form.WindowState = ToggleFormWindowState(form.WindowState);
                Invalidate();
                RestoredFormWindow?.Invoke(new ControlBoxEventArgs(form));
            }
        }

        /// <summary>Toggle the window state between maximized and restored form.</summary>
        /// <param name="form">The form.</param>
        public void ToggleWindowState(Form form)
        {
            if (form == null)
            {
                form = ParentForm;
            }

            if (form is VisualForm visualForm)
            {
                if (!visualForm.WindowTitleBarVisible)
                {
                    return;
                }
            }

            if (form.WindowState == FormWindowState.Normal)
            {
                MaximizeForm(form);
            }
            else
            {
                RestoreFormWindow(form);
            }
        }

        public void UpdateTheme(Theme theme)
        {
            try
            {
                _closeButton.BackColorState = new ControlColorState { Disabled = theme.ColorPalette.CloseButtonBackDisabled, Enabled = theme.ColorPalette.CloseButtonBackEnabled, Hover = theme.ColorPalette.CloseButtonBackHover, Pressed = theme.ColorPalette.CloseButtonBackPressed };

                _closeButton.ForeColorState = new ControlColorState { Disabled = theme.ColorPalette.CloseButtonForeDisabled, Enabled = theme.ColorPalette.CloseButtonForeEnabled, Hover = theme.ColorPalette.CloseButtonForeHover, Pressed = theme.ColorPalette.CloseButtonForePressed };

                _maximizeButton.BackColorState = new ControlColorState { Disabled = theme.ColorPalette.MaximizeButtonBackDisabled, Enabled = theme.ColorPalette.MaximizeButtonBackEnabled, Hover = theme.ColorPalette.MaximizeButtonBackHover, Pressed = theme.ColorPalette.MaximizeButtonBackPressed };

                _maximizeButton.ForeColorState = new ControlColorState { Disabled = theme.ColorPalette.MaximizeButtonForeDisabled, Enabled = theme.ColorPalette.MaximizeButtonForeEnabled, Hover = theme.ColorPalette.MaximizeButtonForeHover, Pressed = theme.ColorPalette.MaximizeButtonForePressed };

                _minimizeButton.BackColorState = new ControlColorState { Disabled = theme.ColorPalette.MinimizeButtonBackDisabled, Enabled = theme.ColorPalette.MinimizeButtonBackEnabled, Hover = theme.ColorPalette.MinimizeButtonBackHover, Pressed = theme.ColorPalette.MinimizeButtonBackPressed };

                _minimizeButton.ForeColorState = new ControlColorState { Disabled = theme.ColorPalette.MinimizeButtonForeDisabled, Enabled = theme.ColorPalette.MinimizeButtonForeEnabled, Hover = theme.ColorPalette.MinimizeButtonForeHover, Pressed = theme.ColorPalette.MinimizeButtonForePressed };

                _helpButton.BackColorState = new ControlColorState { Disabled = theme.ColorPalette.HelpButtonBackDisabled, Enabled = theme.ColorPalette.HelpButtonBackEnabled, Hover = theme.ColorPalette.HelpButtonBackHover, Pressed = theme.ColorPalette.HelpButtonBackPressed };

                _helpButton.ForeColorState = new ControlColorState { Disabled = theme.ColorPalette.HelpButtonForeDisabled, Enabled = theme.ColorPalette.HelpButtonForeEnabled, Hover = theme.ColorPalette.HelpButtonForeHover, Pressed = theme.ColorPalette.HelpButtonForePressed };
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

        /// <summary>The close button click.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event args.</param>
        protected virtual void OnCloseClick(object sender, EventArgs e)
        {
            CloseForm(ParentForm);
        }

        /// <summary>The help button click.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event args.</param>
        protected virtual void OnHelpClick(object sender, EventArgs e)
        {
            HelpClick?.Invoke(new ControlBoxEventArgs(ParentForm));
        }

        /// <summary>The maximize button click.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event args.</param>
        protected virtual void OnMaximizeClick(object sender, EventArgs e)
        {
            MaximizeClick?.Invoke(new ControlBoxEventArgs(ParentForm));
            ToggleWindowState(null);
        }

        /// <summary>Occurs when the form is maximized.</summary>
        /// <param name="e">The event args.</param>
        protected virtual void OnMaximizedClick(ControlBoxEventArgs e)
        {
            MaximizedClick?.Invoke(e);
        }

        /// <summary>The minimize button click.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event args.</param>
        protected virtual void OnMinimizeClick(object sender, EventArgs e)
        {
            MinimizeForm(ParentForm);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            if (_initialized)
            {
                Size = GetAdjustedSize();
            }
            else
            {
                Size = new Size(100, 25);
            }
        }

        private void Button_VisibleChanged(object sender, EventArgs e)
        {
            if (_helpButton.Visible)
            {
                _minimizeButton.Location = new Point(_buttonSize.Width, 0);
            }
            else
            {
                _minimizeButton.Location = new Point(0, 0);
                _maximizeButton.Location = new Point(_minimizeButton.Right, 0);
                _closeButton.Location = new Point(_maximizeButton.Right, 0);
            }

            if (_minimizeButton.Visible)
            {
                _maximizeButton.Location = new Point(_minimizeButton.Right, 0);
            }
            else
            {
                if (_helpButton.Visible)
                {
                    _maximizeButton.Location = new Point(_helpButton.Right, 0);
                }
                else
                {
                    _maximizeButton.Location = new Point(0, 0);
                }
            }

            if (_maximizeButton.Visible)
            {
                _closeButton.Location = new Point(_maximizeButton.Right, 0);
            }
            else
            {
                if (_minimizeButton.Visible)
                {
                    _closeButton.Location = new Point(_minimizeButton.Right, 0);
                }
                else
                {
                    if (_helpButton.Visible)
                    {
                        _closeButton.Location = new Point(_helpButton.Right, 0);
                    }
                    else
                    {
                        _closeButton.Location = new Point(0, 0);
                    }
                }
            }

            OnResize(new EventArgs());
        }

        /// <summary>Retrieves the adjusted <see cref="Control" />-<see cref="Size" />.</summary>
        /// <param name="height">The height.</param>
        /// <returns>The <see cref="Size" />.</returns>
        private Size GetAdjustedSize(int height = 25)
        {
            try
            {
                var _x = 0;

                if (_helpButton.Visible)
                {
                    _x += _helpButton.Width;
                }

                if (_minimizeButton.Visible)
                {
                    _x += _minimizeButton.Width;
                }

                if (_maximizeButton.Visible)
                {
                    _x += _maximizeButton.Width;
                }

                if (_closeButton.Visible)
                {
                    _x += _closeButton.Width;
                }

                return new Size(_x, height);
            }
            catch (Exception e)
            {
                ConsoleEx.WriteDebug(e);
                return new Size(25, 25);
            }
        }

        /// <summary>Initializes the <see cref="VisualControlBox" />.</summary>
        private void InitializeControlBox()
        {
            DoubleBuffered = true;
            UpdateStyles();

            Anchor = AnchorStyles.Top | AnchorStyles.Right;
            BackColor = Color.Transparent;

            Size = new Size(100, 25);

            Padding = new Padding(0);

            _buttonSize = new Size(24, Height);

            _helpButton = new ControlBoxButton { Location = new Point(0, 0), Size = _buttonSize, Text = ControlBoxConstants.HelpText, OffsetLocation = new Point(0, 1) };

            _helpButton.Click += OnHelpClick;
            _helpButton.VisibleChanged += Button_VisibleChanged;

            _minimizeButton = new ControlBoxButton { Location = new Point(_buttonSize.Width, 0), Size = _buttonSize, Text = ControlBoxConstants.MinimizeText, OffsetLocation = new Point(2, 0) };

            _minimizeButton.Click += OnMinimizeClick;
            _minimizeButton.VisibleChanged += Button_VisibleChanged;

            _maximizeButton = new ControlBoxButton { Location = new Point(_buttonSize.Width * 2, 0), Size = _buttonSize, Text = ControlBoxConstants.MaximizeText, OffsetLocation = new Point(1, 1) };

            _maximizeButton.Click += OnMaximizeClick;
            _maximizeButton.VisibleChanged += Button_VisibleChanged;

            _closeButton = new ControlBoxButton { Location = new Point(_buttonSize.Width * 3, 0), Size = _buttonSize, Text = ControlBoxConstants.CloseText, OffsetLocation = new Point(1, 2) };

            _closeButton.Click += OnCloseClick;
            _closeButton.VisibleChanged += Button_VisibleChanged;

            _initialized = true;

            Controls.Add(_helpButton);
            Controls.Add(_minimizeButton);
            Controls.Add(_maximizeButton);
            Controls.Add(_closeButton);
        }

        #endregion
    }
}