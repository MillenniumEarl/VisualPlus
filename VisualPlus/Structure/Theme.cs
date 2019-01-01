#region Namespace

using System;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Runtime.InteropServices;
using System.Xml.Linq;

using VisualPlus.Enumerators;
using VisualPlus.Interfaces;
using VisualPlus.Managers;
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
        #region Variables

        private ColorPalette _colorPalette;
        private ThemeInformation _information;
        private string _rawTheme;

        #endregion

        #region Constructors

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
                throw new NoNullAllowedException(ExceptionMessenger.IsNullOrEmpty(filePath));
            }

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException(ExceptionMessenger.FileNotFound(filePath));
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

        #region Properties

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

        #region Overrides

        public override string ToString()
        {
            return _rawTheme;
        }

        #endregion

        #region Methods

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
                throw new NoNullAllowedException(ExceptionMessenger.IsNullOrEmpty(filePath));
            }

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException(ExceptionMessenger.FileNotFound(filePath));
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
                    ConsoleEx.WriteDebug(new FileNotFoundException(ExceptionMessenger.FileNotFound(filePath)));
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
                throw new NoNullAllowedException(ExceptionMessenger.IsNullOrEmpty(filePath));
            }

            _rawTheme = ThemeSerialization.Serialize(this);

            if (string.IsNullOrEmpty(_rawTheme))
            {
                throw new ArgumentNullException(nameof(_rawTheme));
            }

            XDocument _theme = XDocument.Parse(_rawTheme);
            _theme.Save(filePath);
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