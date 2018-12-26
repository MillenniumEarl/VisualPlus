#region Namespace

using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using VisualPlus.Localization;
using VisualPlus.Toolkit.Controls.Interactivity;

#endregion

namespace VisualPlus.Toolkit.VisualBase
{
    /// <summary>The <see cref="VisualInputField" /> control.</summary>
    [ClassInterface(ClassInterfaceType.AutoDispatch)]
    [ComVisible(true)]
    [DefaultEvent("Click")]
    [DefaultProperty("Text")]
    [Description("The Visual Input Field")]
    [ToolboxBitmap(typeof(VisualButton), "VisualButton.bmp")]
    [ToolboxItem(true)]
    public class VisualInputField : TextBoxExtended
    {
        #region Variables

        private bool alphaNumericToggle;

        #endregion

        #region Constructors

        /// <summary>Initializes a new instance of the <see cref="VisualInputField" /> class.</summary>
        public VisualInputField()
        {
            alphaNumericToggle = false;
        }

        #endregion

        #region Properties

        /// <summary>Gets or sets the alpha numeric toggle, specifying whether to only accept numbers input.</summary>
        [Description(PropertyDescription.Toggle)]
        [Category(PropertyCategory.Behavior)]
        public bool AlphaNumeric
        {
            get { return alphaNumericToggle; }
            set { alphaNumericToggle = value; }
        }

        #endregion

        #region Overrides

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            // Determine if numeric only input
            if (alphaNumericToggle)
            {
                const char DECIMAL_POINT = '.';
                // const char NEGATIVE_VALUE = '-'; // TODO: Check for '-' to allow negative values

                // Check key char input
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != DECIMAL_POINT))
                {
                    e.Handled = true;
                }

                // Allow one decimal point only - allow more in future commits
                if ((e.KeyChar == DECIMAL_POINT) && (Text.IndexOf(DECIMAL_POINT) > -1))
                {
                    e.Handled = true;
                }
            }

            base.OnKeyPress(e);
        }

        #endregion
    }
}