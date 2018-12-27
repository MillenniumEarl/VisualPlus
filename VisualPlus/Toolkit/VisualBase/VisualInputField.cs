#region Namespace

using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using VisualPlus.Constants;
using VisualPlus.Delegates;
using VisualPlus.Designer;
using VisualPlus.Events;
using VisualPlus.Localization;

#endregion

namespace VisualPlus.Toolkit.VisualBase
{
    /// <summary>The <see cref="VisualInputField" /> control.</summary>
    [ClassInterface(ClassInterfaceType.AutoDispatch)]
    [ComVisible(true)]
    [DefaultEvent("TextChanged")]
    [DefaultProperty("Text")]
    [Description("The Visual Input Field")]
    [Designer(typeof(VisualInputFieldDesigner))]
    [ToolboxBitmap(typeof(VisualInputField), "VisualInputField.bmp")]
    [ToolboxItem(true)]
    public class VisualInputField : TextBox
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

        #region Events

        public event ClipboardEventHandler ClipboardCopy;

        public event ClipboardEventHandler ClipboardCut;

        public event ClipboardEventHandler ClipboardPaste;

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

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == ClipboardConstants.WM_CUT)
            {
                OnClipboardCut(new ClipboardEventArgs(Clipboard.GetText()));
            }
            else if (m.Msg == ClipboardConstants.WM_COPY)
            {
                OnClipboardCopy(new ClipboardEventArgs(Clipboard.GetText()));
            }
            else if (m.Msg == ClipboardConstants.WM_PASTE)
            {
                OnClipboardPaste(new ClipboardEventArgs(Clipboard.GetText()));
            }

            base.WndProc(ref m);
        }

        /// <summary>Occurs when the clipboard copy event fires.</summary>
        /// <param name="e">The event args.</param>
        protected virtual void OnClipboardCopy(ClipboardEventArgs e)
        {
            ClipboardCopy?.Invoke(this, e);
        }

        /// <summary>Occurs when the clipboard cut event fires.</summary>
        /// <param name="e">The event args.</param>
        protected virtual void OnClipboardCut(ClipboardEventArgs e)
        {
            ClipboardCut?.Invoke(this, e);
        }

        /// <summary>Occurs when the clipboard paste event fires.</summary>
        /// <param name="e">The event args.</param>
        protected virtual void OnClipboardPaste(ClipboardEventArgs e)
        {
            ClipboardPaste?.Invoke(this, e);
        }

        #endregion
    }
}