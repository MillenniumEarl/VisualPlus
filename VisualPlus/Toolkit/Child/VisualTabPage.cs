#region License

// -----------------------------------------------------------------------------------------------------------
//
// Name: VisualTabPage.cs
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

#endregion License

#region Namespace

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;

using VisualPlus.Constants;
using VisualPlus.Designer;
using VisualPlus.Enumerators;
using VisualPlus.Localization;
using VisualPlus.Models;
using VisualPlus.TypeConverters;

#endregion Namespace

namespace VisualPlus.Toolkit.Child
{
    [Designer(typeof(VisualTabPageDesigner))]
    [ToolboxItem(false)]
    public class VisualTabPage : TabPage
    {
        #region Fields

        private Shape _border;
        private Image _headerImage;
        private Image _image;
        private Size _imageSize;
        private Color _tabHover;
        private Color _tabNormal;
        private Color _tabSelected;
        private StringAlignment _textAlignment;
        private TextImageRelations _textImageRelation;
        private StringAlignment _textLineAlignment;
        private Color _textSelected;

        /// <summary>Required designer variable.</summary>
        private Container components;

        #endregion Fields

        #region Constructors and Destructors

        /// <summary>Initializes a new instance of the <see cref="VisualTabPage" /> class.</summary>
        /// <param name="text">The text of the page.</param>
        public VisualTabPage(string text) : this()
        {
            Text = text;
        }

        /// <summary>Initializes a new instance of the <see cref="VisualTabPage" /> class.</summary>
        public VisualTabPage()
        {
            InitializeComponent();
            UpdateStyles();

            _textLineAlignment = StringAlignment.Center;
            _textAlignment = StringAlignment.Center;

            Theme theme = new Theme(DefaultConstants.DefaultStyle);

            BackColor = theme.ColorPalette.ControlEnabled;
            ForeColor = Color.FromArgb(174, 181, 187);
            _textSelected = Color.FromArgb(217, 220, 227);

            _border = new Shape { Visible = false, Type = ShapeTypes.Rectangle };

            _textImageRelation = TextImageRelations.Text;

            _image = null;
            _imageSize = new Size(16, 16);

            _headerImage = null;

            _tabNormal = theme.ColorPalette.TabPageEnabled;
            _tabSelected = theme.ColorPalette.TabPageSelected;
            _tabHover = theme.ColorPalette.TabPageHover;
        }

        #endregion Constructors and Destructors

        #region Enums

        public enum TextImageRelations
        {
            /// <summary>The image before text.</summary>
            ImageBeforeText,

            /// <summary>The image.</summary>
            Image,

            /// <summary>The text.</summary>
            Text
        }

        #endregion Enums

        #region Public Properties

        [TypeConverter(typeof(VisualSettingsTypeConverter))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Category(PropertyCategory.Appearance)]
        public Shape Border
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

        [Browsable(true)]
        [Category(PropertyCategory.Behavior)]
        [Description(PropertyDescription.Toggle)]
        public new bool Enabled
        {
            get
            {
                return base.Enabled;
            }

            set
            {
                base.Enabled = value;
            }
        }

        [Category(PropertyCategory.Appearance)]
        [Description(PropertyDescription.Image)]
        public Image HeaderImage
        {
            get
            {
                return _headerImage;
            }

            set
            {
                _headerImage = value;
                Invalidate();
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

        [Browsable(false)]
        public Rectangle Rectangle { get; set; }

        [Category(PropertyCategory.Appearance)]
        [Description(PropertyDescription.Color)]
        public Color TabHover
        {
            get
            {
                return _tabHover;
            }

            set
            {
                _tabHover = value;
                Invalidate();
            }
        }

        [Category(PropertyCategory.Appearance)]
        [Description(PropertyDescription.Color)]
        public Color TabNormal
        {
            get
            {
                return _tabNormal;
            }

            set
            {
                _tabNormal = value;
                Invalidate();
            }
        }

        [Category(PropertyCategory.Appearance)]
        [Description(PropertyDescription.Color)]
        public Color TabSelected
        {
            get
            {
                return _tabSelected;
            }

            set
            {
                _tabSelected = value;
                Invalidate();
            }
        }

        [Category(PropertyCategory.Appearance)]
        [Description(PropertyDescription.VerticalAlignment)]
        public StringAlignment TextAlignment
        {
            get
            {
                return _textAlignment;
            }

            set
            {
                _textAlignment = value;
                Invalidate();
            }
        }

        [Category(PropertyCategory.Behavior)]
        [Description(PropertyDescription.TextImageRelation)]
        public TextImageRelations TextImageRelation
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

        [Category(PropertyCategory.Appearance)]
        [Description(PropertyDescription.Alignment)]
        public StringAlignment TextLineAlignment
        {
            get
            {
                return _textLineAlignment;
            }

            set
            {
                _textLineAlignment = value;
                Invalidate();
            }
        }

        [Category(PropertyCategory.Appearance)]
        [Description(PropertyDescription.Color)]
        public Color TextSelected
        {
            get
            {
                return _textSelected;
            }

            set
            {
                _textSelected = value;
                Invalidate();
            }
        }

        #endregion Public Properties

        #region Public Methods and Operators

        /// <summary>Updates the properties after an Invalidate.</summary>
        public void UpdateProperties()
        {
            try
            {
                Invalidate();
            }
            catch (Exception e)
            {
                throw new Exception(e.StackTrace);
            }
        }

        #endregion Public Methods and Operators

        #region Methods

        protected override ControlCollection CreateControlsInstance()
        {
            SetStyle(
                ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.ResizeRedraw |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.SupportsTransparentBackColor,
                true);

            DoubleBuffered = true;

            return base.CreateControlsInstance();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                components?.Dispose();
            }

            base.Dispose(disposing);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics _graphics = e.Graphics;
            _graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

            if (BackgroundImage != null)
            {
                _graphics.DrawImage(BackgroundImage, new Rectangle(new Point(0, 0), Size));
            }
        }

        /// <summary>Required method for Designer support - do not modify the contents of this method with the code editor.</summary>
        private void InitializeComponent()
        {
            components = new Container();
            Disposed += VisualTabPage_Disposed;
        }

        private void VisualTabPage_Disposed(object sender, EventArgs e)
        {
        }

        #endregion Methods
    }
}