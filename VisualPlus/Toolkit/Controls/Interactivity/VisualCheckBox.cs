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
    [Description("The Visual CheckBox")]
    [Designer(typeof(VisualCheckBoxDesigner))]
    [ToolboxBitmap(typeof(VisualCheckBox), "VisualCheckBox.bmp")]
    [ToolboxItem(true)]
    public class VisualCheckBox : CheckBoxBase, IThemeSupport
    {
        #region Constructors

        /// <summary>Initializes a new instance of the <see cref="VisualCheckBox" /> class.</summary>
        public VisualCheckBox()
        {
            Size = new Size(125, 23);

            Border = new Border { Rounding = Settings.DefaultValue.Rounding.BoxRounding };

            CheckStyle = new CheckStyle(ClientRectangle)
                    {
                       Style = CheckStyle.CheckType.Checkmark 
                    };

            UpdateTheme(ThemeManager.Theme);
        }

        #endregion

        #region Overrides

        protected override void CreateHandle()
        {
            base.CreateHandle();
            Cursor = Cursors.Hand;
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