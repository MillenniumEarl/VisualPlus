#region Namespace

using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

using VisualPlus.Designer;
using VisualPlus.Events;
using VisualPlus.Interfaces;
using VisualPlus.Structure;
using VisualPlus.Toolkit.VisualBase;

#endregion

namespace VisualPlus.Toolkit.Controls.Interactivity
{
    [ClassInterface(ClassInterfaceType.AutoDispatch)]
    [ComVisible(true)]
    [DefaultEvent("ToggleChanged")]
    [DefaultProperty("Checked")]
    [Description("The Visual RadioButton")]
    [Designer(typeof(VisualRadioButtonDesigner))]
    [ToolboxBitmap(typeof(VisualRadioButton), "VisualRadioButton.bmp")]
    [ToolboxItem(true)]
    public class VisualRadioButton : RadioButtonBase, IThemeSupport
    {
        #region Constructors

        /// <summary>Initializes a new instance of the <see cref="VisualRadioButton" /> class.</summary>
        public VisualRadioButton()
        {
            Cursor = Cursors.Hand;
            Size = new Size(125, 23);

            Border = new Border { Rounding = Settings.DefaultValue.Rounding.RoundedRectangle };

            CheckStyle = new CheckStyle(ClientRectangle)
                {
                    Style = CheckStyle.CheckType.Shape,
                    ShapeRounding = Settings.DefaultValue.Rounding.Default,
                    Bounds = new Rectangle(new Point(), new Size(8, 8))
                };

            UpdateTheme(ThemeManager.Theme);
        }

        #endregion

        #region Methods

        public void UpdateTheme(Theme theme)
        {
            try
            {
                Border.Color = theme.ColorPalette.BorderNormal;
                Border.HoverColor = theme.ColorPalette.BorderHover;

                CheckStyle.CheckColor = theme.ColorPalette.Progress;

                ForeColor = theme.ColorPalette.TextEnabled;
                TextStyle.Enabled = theme.ColorPalette.TextEnabled;
                TextStyle.Disabled = theme.ColorPalette.TextDisabled;

                // Font = theme.ColorPalette.Font;
                BoxColorState.Enabled = theme.ColorPalette.Enabled;
                BoxColorState.Disabled = theme.ColorPalette.Disabled;
                BoxColorState.Hover = theme.ColorPalette.Hover;
                BoxColorState.Pressed = theme.ColorPalette.Pressed;
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