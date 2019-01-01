#region Namespace

using System.Drawing;
using System.Windows.Forms;

using VisualPlus.Toolkit.Components;

#endregion

namespace VisualPlus.Structure
{
    public class TipInfo
    {
        #region Constructors

        /// <summary>Initializes a new instance of the <see cref="TipInfo" /> class.</summary>
        public TipInfo()
        {
            Caption = string.Empty;
            Control = null;
            Icon = ToolTipIcon.None;
            Image = null;
            Position = new Point();
            Text = string.Empty;
            Type = ToolTipType.Default;
        }

        #endregion

        #region Enumerators

        public enum ToolTipType
        {
            /// <summary>The default.</summary>
            Default = 0,

            /// <summary>The image.</summary>
            Image = 1,

            /// <summary>The text.</summary>
            Text = 2
        }

        #endregion

        #region Properties

        /// <summary>Gets or sets the <see cref="VisualToolTip" /> title to display when the pointer is on the control.</summary>
        public string Caption { get; set; }

        /// <summary>Gets or sets the <see cref="VisualToolTip" /> control.</summary>
        public Control Control { get; set; }

        /// <summary>
        ///     Gets or sets a value that defines the type of icon to be displayed along side <see cref="VisualToolTip" />
        ///     Text.
        /// </summary>
        public ToolTipIcon Icon { get; set; }

        /// <summary>
        ///     Gets or sets a value that defines the type of image to be displayed along side <see cref="VisualToolTip" />
        ///     Text.
        /// </summary>
        public Image Image { get; set; }

        /// <summary>Gets or sets the <see cref="VisualToolTip" /> position.</summary>
        public Point Position { get; set; }

        /// <summary>Gets or sets the <see cref="VisualToolTip" /> size.</summary>
        public Size Size { get; set; }

        /// <summary>Gets or sets the <see cref="VisualToolTip" /> text content to display when the pointer is on the control.</summary>
        public string Text { get; set; }

        /// <summary>Gets or sets a value that defines the type to be displayed.</summary>
        public ToolTipType Type { get; set; }

        #endregion
    }
}