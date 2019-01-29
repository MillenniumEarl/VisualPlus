#region License

// -----------------------------------------------------------------------------------------------------------
// 
// Name: Theme.cs
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
using System.Data;
using System.IO;
using System.Runtime.InteropServices;
using System.Xml.Linq;

using VisualPlus.Enumerators;
using VisualPlus.Interfaces;
using VisualPlus.Localization;
using VisualPlus.TypeConverters;

#endregion

namespace VisualPlus.Structure
{
    [ClassInterface(ClassInterfaceType.AutoDispatch)]
    [ComVisible(true)]
    [Description("The theme structure.")]
    [DesignerCategory("code")]
    [ToolboxItem(false)]
    [TypeConverter(typeof(ThemeTypeConverter))]
    public class Theme
    {
        #region Fields

        private ColorPalette _colorPalette;
        private ThemeInformation _information;
        private string _rawTheme;

        #endregion

        #region Constructors and Destructors

        /// <summary>Initializes a new instance of the <see cref="Theme" /> class.</summary>
        /// <param name="themeInformation">The theme information.</param>
        /// <param name="colorPalette">The color Palette.</param>
        public Theme(ThemeInformation themeInformation, ColorPalette colorPalette)
        {
            UpdateTheme(themeInformation, colorPalette);
        }

        /// <summary>Initializes a new instance of the <see cref="Theme" /> class.</summary>
        /// <param name="theme">The theme.</param>
        public Theme(Theme theme)
        {
            UpdateTheme(theme.Information, theme.ColorPalette);
        }

        /// <summary>Initializes a new instance of the <see cref="Theme" /> class.</summary>
        /// <param name="themes">The internal themes.</param>
        public Theme(Themes themes)
        {
            LoadThemeFromResources(themes);
        }

        /// <summary>Initializes a new instance of the <see cref="Theme" /> class.</summary>
        /// <param name="filePath">The file.</param>
        public Theme(string filePath) : this()
        {
            if (string.IsNullOrEmpty(filePath))
            {
                throw new NoNullAllowedException(ArgumentMessages.IsNullOrEmpty());
            }

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException(ArgumentMessages.FileNotFound(filePath));
            }

            Load(filePath);
        }

        /// <summary>Initializes a new instance of the <see cref="Theme" /> class.</summary>
        public Theme()
        {
            _rawTheme = string.Empty;
            _information = new ThemeInformation();
            _colorPalette = new ColorPalette();
        }

        #endregion

        #region Public Properties

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [TypeConverter(typeof(VisualSettingsTypeConverter))]
        public ColorPalette ColorPalette
        {
            get
            {
                return _colorPalette;
            }

            set
            {
                if (_colorPalette != value)
                {
                    _colorPalette = value;
                    UpdateTheme(_information, _colorPalette);
                }
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [TypeConverter(typeof(VisualSettingsTypeConverter))]
        public ThemeInformation Information
        {
            get
            {
                return _information;
            }

            set
            {
                if (_information != value)
                {
                    _information = value;
                    UpdateTheme(_information, _colorPalette);
                }
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public string RawTheme
        {
            get
            {
                return _rawTheme;
            }

            private set
            {
                if (_rawTheme != value)
                {
                    _rawTheme = value;

                    // TODO: Verify raw theme is compatible theme then update contents.
                }
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>Invoke the theme update to the supported component.</summary>
        /// <param name="component">The component.</param>
        /// <param name="type">The type.</param>
        /// <param name="theme">The theme.</param>
        public static void InvokeThemeUpdate(IDisposable component, Type type, Theme theme)
        {
            if (component == null)
            {
                throw new ArgumentNullException(nameof(component));
            }

            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (theme == null)
            {
                throw new ArgumentNullException(nameof(theme));
            }

            if (component is IThemeSupport controlThemeSupported)
            {
                controlThemeSupported.UpdateTheme(theme);
            }
        }

        /// <summary>Loads the <see cref="Theme" /> from the file path.</summary>
        /// <param name="filePath">The file path.</param>
        public void Load(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                throw new NoNullAllowedException(ArgumentMessages.IsNullOrEmpty());
            }

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException(ArgumentMessages.FileNotFound(filePath));
            }

            try
            {
                if (File.Exists(filePath))
                {
                    Theme theme = ThemeSerialization.Deserialize(filePath);
                    UpdateTheme(theme.Information, theme.ColorPalette);
                }
                else
                {
                    ConsoleEx.WriteDebug(new FileNotFoundException(ArgumentMessages.FileNotFound(filePath)));
                }
            }
            catch (Exception e)
            {
                ConsoleEx.WriteDebug(e);
            }
        }

        /// <summary>Saves the theme to a file.</summary>
        /// <param name="filePath">The file path.</param>
        public void Save(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                throw new NoNullAllowedException(ArgumentMessages.IsNullOrEmpty());
            }

            _rawTheme = ThemeSerialization.Serialize(this);

            if (string.IsNullOrEmpty(_rawTheme))
            {
                throw new ArgumentNullException(nameof(_rawTheme));
            }

            XDocument _theme = XDocument.Parse(_rawTheme);
            _theme.Save(filePath);
        }

        public override string ToString()
        {
            return _rawTheme;
        }

        /// <summary>Update the theme contents.</summary>
        /// <param name="themeInformation">The theme Information.</param>
        /// <param name="colorPalette">The color Palette.</param>
        public void UpdateTheme(ThemeInformation themeInformation, ColorPalette colorPalette)
        {
            _information = themeInformation;
            _colorPalette = colorPalette;

            _rawTheme = ThemeSerialization.Serialize(this);
        }

        #endregion

        #region Methods

        /// <summary>Loads a <see cref="Theme" /> from resources.</summary>
        /// <param name="themes">The theme.</param>
        private void LoadThemeFromResources(Themes themes)
        {
            try
            {
                Theme theme = ThemeSerialization.Deserialize(themes);
                UpdateTheme(theme.Information, theme.ColorPalette);
            }
            catch (Exception e)
            {
                ConsoleEx.WriteDebug(e);
            }
        }

        #endregion
    }
}