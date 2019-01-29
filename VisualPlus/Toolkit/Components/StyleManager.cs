#region License

// -----------------------------------------------------------------------------------------------------------
// 
// Name: StyleManager.cs
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
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Design;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

using VisualPlus.Constants;
using VisualPlus.Delegates;
using VisualPlus.Enumerators;
using VisualPlus.Events;
using VisualPlus.Extensibility;
using VisualPlus.Interfaces;
using VisualPlus.Managers;
using VisualPlus.Models;
using VisualPlus.TypeConverters;
using VisualPlus.UITypeEditors;
using VisualPlus.Utilities.Debugging;

#endregion

namespace VisualPlus.Toolkit.Components
{
    [ClassInterface(ClassInterfaceType.AutoDispatch)]
    [ComVisible(true)]
    [DefaultEvent("ThemeChanged")]
    [DefaultProperty("Theme")]
    [Description("The style manager component enables you to manage the control themes.")]
    [ToolboxItem(true)]
    [ToolboxBitmap(typeof(StyleManager), "StyleManager.bmp")]
    public class StyleManager : Component, ICloneable
    {
        #region Fields

        private string _customThemePath;
        private List<Form> _formCollection;
        private Theme _theme;
        private Themes _themeType;

        #endregion

        #region Constructors and Destructors

        /// <summary>Initializes a new instance of the <see cref="StyleManager" /> class.</summary>
        /// <param name="container">The container.</param>
        public StyleManager(IContainer container) : this()
        {
            container.Add(this);
        }

        /// <summary>Initializes a new instance of the <see cref="StyleManager" /> class.</summary>
        /// <param name="filePath">The custom theme.</param>
        public StyleManager(string filePath) : this()
        {
            _theme = new Theme(filePath);
        }

        /// <summary>Initializes a new instance of the <see cref="StyleManager" /> class.</summary>
        /// <param name="theme">The custom theme.</param>
        public StyleManager(Theme theme) : this()
        {
            _theme = new Theme(theme);
        }

        /// <summary>Initializes a new instance of the <see cref="StyleManager" /> class.</summary>
        /// <param name="form">The form.</param>
        public StyleManager(Form form) : this()
        {
            AddFormToManage(form);
        }

        /// <summary>Initializes a new instance of the <see cref="StyleManager" /> class.</summary>
        /// <param name="themes">The internal themes.</param>
        public StyleManager(Themes themes) : this()
        {
            _theme = new Theme(themes);
        }

        /// <summary>Initializes a new instance of the <see cref="StyleManager" /> class.</summary>
        /// <param name="form">The form.</param>
        /// <param name="filePath">The custom theme.</param>
        public StyleManager(Form form, string filePath) : this()
        {
            _theme = new Theme(filePath);
            AddFormToManage(form);
        }

        /// <summary>Initializes a new instance of the <see cref="StyleManager" /> class.</summary>
        /// <param name="form">The form.</param>
        /// <param name="theme">The style.</param>
        public StyleManager(Form form, Themes theme) : this()
        {
            _theme = new Theme(theme);
            AddFormToManage(form);
        }

        /// <summary>Prevents a default instance of the <see cref="StyleManager" /> class from being created.</summary>
        private StyleManager()
        {
            try
            {
                GenerateDefaultThemeFile();

                if (_customThemePath == null)
                {
                    string _themePath = DefaultConstants.TemplatesFilePath;
                    _customThemePath = _themePath;
                }

                _formCollection = new List<Form>();

                _themeType = DefaultConstants.DefaultStyle;
                _theme = new Theme(_themeType);
            }
            catch (Exception e)
            {
                ConsoleEx.WriteDebug(e);
            }
        }

        #endregion

        #region Public Events

        public event ThemeChangedEventHandler ThemeChanged;

        #endregion

        #region Public Properties

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public List<Control> Controls
        {
            get
            {
                var _controlsList = new List<Control>();

                foreach (Form _forms in _formCollection)
                {
                    _controlsList.AddRange(_forms.Controls.Cast<Control>());
                }

                return _controlsList;
            }
        }

        [Editor(typeof(ThemesEditor), typeof(UITypeEditor))]
        public string CustomThemePath
        {
            get
            {
                return _customThemePath;
            }

            set
            {
                if (value == _customThemePath)
                {
                    return;
                }

                _customThemePath = value;
                ReadTheme(_customThemePath);
            }
        }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public List<Form> Forms
        {
            get
            {
                return _formCollection;
            }
        }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public List<Control> SupportedControls
        {
            get
            {
                var _controlsList = new List<Control>();

                foreach (Form _forms in _formCollection)
                {
                    _controlsList.AddRange(_forms.Controls.Cast<Control>().Where(_control => _control.HasMethod(DefaultConstants.ComponentUpdateMethodName)));
                }

                return _controlsList;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [TypeConverter(typeof(ThemeTypeConverter))]
        public Theme Theme
        {
            get
            {
                return _theme;
            }

            set
            {
                if (value == _theme)
                {
                    return;
                }

                _theme = value;

                // _theme = new Theme(_themeType);
                Update();
            }
        }

        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public Themes ThemeType
        {
            get
            {
                return _themeType;
            }

            set
            {
                if (value == _themeType)
                {
                    return;
                }

                _themeType = value;
                _theme = new Theme(_themeType);
                Update();
            }
        }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public List<Control> UnsupportedControls
        {
            get
            {
                var _controlsList = new List<Control>();

                foreach (Form _forms in _formCollection)
                {
                    _controlsList.AddRange(_forms.Controls.Cast<Control>().Where(_control => !_control.HasMethod(DefaultConstants.ComponentUpdateMethodName)));
                }

                return _controlsList;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>Creates a default theme file in the templates folder.</summary>
        /// <param name="force">Forcefully overwrite any default theme with a new one.</param>
        public static void GenerateDefaultThemeFile(bool force = false)
        {
            string _defaultThemePath = DefaultConstants.TemplatesFilePath;

            if (force)
            {
                CreateDefaultThemeFile();
                return;
            }

            if (File.Exists(_defaultThemePath))
            {
                FileInfo fileInfo = new FileInfo(_defaultThemePath);
                DateTime lastModified = fileInfo.LastWriteTime;

                // 24 Hours interval.
                DateTime expiryDate = lastModified.AddDays(1);

                if (DateTimeManager.DateExpired(expiryDate))
                {
                    CreateDefaultThemeFile();
                }
                else
                {
                    // Next update check within 24 hours.
                }
            }
            else
            {
                // Create default theme file since none found.
                CreateDefaultThemeFile();
            }
        }

        /// <summary>Opens the templates directory in the windows explorer.</summary>
        public static void OpenTemplatesDirectory()
        {
            Process.Start(DefaultConstants.TemplatesFolder);
        }

        /// <summary>Adds a form to the collection to manage.</summary>
        /// <param name="form">The form.</param>
        public void AddFormToManage(Form form)
        {
            if (!_formCollection.Contains(form))
            {
                _formCollection.Add(form);
            }

            Update();
        }

        /// <summary>Creates a copy of the current object.</summary>
        /// <returns>The <see cref="object" />.</returns>
        public object Clone()
        {
            return MemberwiseClone();
        }

        /// <summary>Open the ThemesEditor dialog to pick a custom theme file.</summary>
        public void OpenCustomTheme()
        {
            using (OpenFileDialog _openFileDialog = new OpenFileDialog())
            {
                _openFileDialog.Filter = @"Theme File|*.xml";

                if (_openFileDialog.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                _customThemePath = _openFileDialog.FileName;
                ReadTheme(_customThemePath);
            }
        }

        /// <summary>Reads the theme from the custom file path.</summary>
        /// <param name="path">The path.</param>
        public void ReadTheme(string path)
        {
            _theme = new Theme(path);
            OnThemeChanged(new ThemeEventArgs(_theme));
        }

        /// <summary>Saves the theme to a file path.</summary>
        /// <param name="filePath">The file path.</param>
        public void SaveTheme(string filePath)
        {
            _theme.Save(filePath);
        }

        public override string ToString()
        {
            StringBuilder _stringBuilder = new StringBuilder();
            _stringBuilder.AppendLine("Theme name: " + _theme.Information.Name);
            _stringBuilder.AppendLine("Theme author: " + _theme.Information.Author);
            _stringBuilder.Append(Environment.NewLine);
            _stringBuilder.AppendLine("Total forms: " + Forms.Count);
            _stringBuilder.AppendLine("Total controls: " + Controls.Count);
            _stringBuilder.AppendLine("Supported controls: " + SupportedControls.Count);
            _stringBuilder.AppendLine("Unsupported controls: " + UnsupportedControls.Count);
            return _stringBuilder.ToString();
        }

        /// <summary>Updates all the <see cref="Control" />/s and <see cref="Form" />/s.</summary>
        public void Update()
        {
            if (_formCollection.IsEmpty())
            {
                return;
            }

            foreach (Form _form in _formCollection)
            {
                if (_form == null)
                {
                    throw new ArgumentNullException(nameof(_form));
                }

                UpdateComponent(_form);

                if (_form.Controls.IsEmpty())
                {
                    return;
                }

                foreach (Control _control in _form.Controls)
                {
                    UpdateComponent(_control);
                }
            }

            OnThemeChanged(new ThemeEventArgs(_theme));
        }

        /// <summary>Update the components theme.</summary>
        /// <param name="component">The component to update.</param>
        public void UpdateComponent(IDisposable component)
        {
            if (component is IThemeSupport)
            {
                foreach (Type registeredTypes in ControlManager.ThemeSupportedTypes())
                {
                    if (component.HasMethod(DefaultConstants.ComponentUpdateMethodName))
                    {
                        switch (component)
                        {
                            case Form form:
                                {
                                    if (form.GetType().BaseType == registeredTypes)
                                    {
                                        if (registeredTypes != null)
                                        {
                                            Theme.InvokeThemeUpdate(form, registeredTypes, _theme);
                                        }
                                    }
                                    else
                                    {
                                        // Form not registered.
                                    }

                                    break;
                                }

                            case Control control:
                                {
                                    if (control.GetType() == registeredTypes)
                                    {
                                        Theme.InvokeThemeUpdate(control, registeredTypes, _theme);
                                    }
                                    else
                                    {
                                        // Control not registered.
                                    }

                                    break;
                                }
                        }
                    }
                    else
                    {
                        // The component does not contain method.
                    }
                }
            }
            else
            {
                // The component not supported.
            }
        }

        #endregion

        #region Methods

        /// <summary>The theme changed event.</summary>
        /// <param name="e">The event args.</param>
        protected virtual void OnThemeChanged(ThemeEventArgs e)
        {
            ThemeChanged?.Invoke(e);
        }

        /// <summary>Creates the default theme file.</summary>
        private static void CreateDefaultThemeFile()
        {
            Theme _defaultTheme = new Theme(Themes.Visual);
            string _defaultThemePath = DefaultConstants.TemplatesFilePath;

            string _text = _defaultTheme.RawTheme;

            if (!Directory.Exists(DefaultConstants.TemplatesFolder))
            {
                Directory.CreateDirectory(DefaultConstants.TemplatesFolder);
            }

            File.WriteAllText(_defaultThemePath, _text);
        }

        #endregion
    }
}