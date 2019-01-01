#region Namespace

using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using VisualPlus.Enumerators;
using VisualPlus.Toolkit.Dialogs;

#endregion

namespace VisualPlus.Toolkit.VisualBase
{
    public class VisualDialog : VisualForm
    {
        #region Constructors

        /// <summary>Initializes a new instance of the <see cref="VisualDialog" /> class.</summary>
        /// <param name="text">The caption text.</param>
        public VisualDialog(string text) : this()
        {
            Text = text;
        }

        /// <summary>Initializes a new instance of the <see cref="VisualDialog" /> class.</summary>
        public VisualDialog()
        {
            BackColor = Color.White;
            ButtonSize = new Size(75, 23);
            HelpButton = false;
            MinimizeBox = false;
            MaximizeBox = false;
            Sizable = false;
            ShowIcon = false;
            ShowInTaskbar = false;
            TitleAlignment = Alignment.TextAlignment.Left;
            StartPosition = FormStartPosition.CenterParent;
        }

        #endregion

        #region Properties

        /// <summary>Gets or sets the button size.</summary>
        [Browsable(false)]
        public Size ButtonSize { get; set; }

        #endregion
    }
}