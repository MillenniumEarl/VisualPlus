#region Namespace

using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows.Forms;

using VisualPlus.Constants;
using VisualPlus.Delegates;
using VisualPlus.Events;

#endregion

namespace VisualPlus.Toolkit.VisualBase
{
    [DesignerCategory("code")]
    [ClassInterface(ClassInterfaceType.AutoDispatch)]
    [ComVisible(true)]
    [ToolboxItem(false)]
    public class TextBoxExtended : TextBox
    {
        #region Events

        public event ClipboardEventHandler ClipboardCopy;

        public event ClipboardEventHandler ClipboardCut;

        public event ClipboardEventHandler ClipboardPaste;

        #endregion

        #region Overrides

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