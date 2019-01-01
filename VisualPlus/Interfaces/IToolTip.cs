#region Namespace

using System.Drawing;
using System.Windows.Forms;

using VisualPlus.Structure;
using VisualPlus.Toolkit.Components;

#endregion

namespace VisualPlus.Interfaces
{
    public interface IToolTip
    {
        #region Properties

        /// <summary>Gets or sets the <see cref="VisualToolTip" /> image.</summary>
        Image Image { get; set; }

        /// <summary>Gets or sets the <see cref="VisualToolTip" /> text.</summary>
        string Text { get; set; }

        /// <summary>Gets or sets the <see cref="VisualToolTip" /> info.</summary>
        TipInfo TipInfo { get; set; }

        /// <summary>Gets or sets the <see cref="VisualToolTip" /> type.</summary>
        TipInfo.ToolTipType TipType { get; set; }

        /// <summary>Gets or sets the <see cref="VisualToolTip" /> control.</summary>
        Control ToolTipControl { get; set; }

        /// <summary>Gets or sets the <see cref="VisualToolTip" /> title.</summary>
        string ToolTipTitle { get; set; }

        #endregion

        #region Methods

        /// <summary>Retrieves the <see cref="VisualToolTip" /> text associated with the specified control.</summary>
        /// <param name="control">The control for which to retrieve the <see cref="VisualToolTip" /> title.</param>
        /// <returns>The <see cref="string" />.</returns>
        string GetToolTip(Control control);

        /// <summary>Associates <see cref="VisualToolTip" /> text with the control.</summary>
        /// <param name="control">The control to associate <see cref="VisualToolTip" /> text with.</param>
        /// <param name="caption">The <see cref="VisualToolTip" /> text to display when the pointer is on the control.</param>
        void SetToolTip(Control control, string caption);

        /// <summary>
        ///     Sets the <see cref="VisualToolTip" /> text associated with the specified control, and displays the
        ///     <see cref="VisualToolTip" /> modally.
        /// </summary>
        /// <param name="text">A <see cref="string" /> containing the new <see cref="VisualToolTip" /> text.</param>
        /// <param name="window">The <see cref="Control" /> to display the <see cref="VisualToolTip" /> for.</param>
        void Show(string text, IWin32Window window);

        /// <summary>
        ///     Sets the <see cref="VisualToolTip" /> text associated with the specified control, and displays the
        ///     <see cref="VisualToolTip" /> modally.
        /// </summary>
        /// <param name="text">A <see cref="string" /> containing the new <see cref="VisualToolTip" /> text.</param>
        /// <param name="window">The <see cref="Control" /> to display the <see cref="VisualToolTip" /> for.</param>
        /// <param name="duration">
        ///     An <see cref="int" /> containing the duration, in milliseconds, to display the
        ///     <see cref="VisualToolTip" />.
        /// </param>
        void Show(string text, IWin32Window window, int duration);

        /// <summary>
        ///     Sets the <see cref="VisualToolTip" /> text associated with the specified control, and displays the
        ///     <see cref="VisualToolTip" /> modally.
        /// </summary>
        /// <param name="text">A <see cref="string" /> containing the new <see cref="VisualToolTip" /> text.</param>
        /// <param name="window">The <see cref="Control" /> to display the <see cref="VisualToolTip" /> for.</param>
        /// <param name="point">
        ///     A <see cref="Point" /> containing the offset, in pixels, relative to the upper-left corner of the
        ///     associated control window, to display the <see cref="VisualToolTip" />.
        /// </param>
        void Show(string text, IWin32Window window, Point point);

        /// <summary>
        ///     Sets the <see cref="VisualToolTip" /> text associated with the specified control, and displays the
        ///     <see cref="VisualToolTip" /> modally.
        /// </summary>
        /// <param name="text">A <see cref="string" /> containing the new <see cref="VisualToolTip" /> text.</param>
        /// <param name="window">The <see cref="Control" /> to display the <see cref="VisualToolTip" /> for.</param>
        /// <param name="point">
        ///     A <see cref="Point" /> containing the offset, in pixels, relative to the upper-left corner of the
        ///     associated control window, to display the <see cref="VisualToolTip" />.
        /// </param>
        /// <param name="duration">
        ///     An <see cref="int" /> containing the duration, in milliseconds, to display the
        ///     <see cref="VisualToolTip" />.
        /// </param>
        void Show(string text, IWin32Window window, Point point, int duration);

        /// <summary>
        ///     Sets the <see cref="VisualToolTip" /> text associated with the specified control, and displays the
        ///     <see cref="VisualToolTip" /> modally.
        /// </summary>
        /// <param name="text">A <see cref="string" /> containing the new <see cref="VisualToolTip" /> text.</param>
        /// <param name="window">The <see cref="Control" /> to display the <see cref="VisualToolTip" /> for.</param>
        /// <param name="x">
        ///     The horizontal offset, in pixels, relative to the upper-left corner of the associated control window,
        ///     to display the <see cref="VisualToolTip" />.
        /// </param>
        /// <param name="y">
        ///     The vertical offset, in pixels, relative to the upper-left corner of the associated control window, to
        ///     display the <see cref="VisualToolTip" />.
        /// </param>
        void Show(string text, IWin32Window window, int x, int y);

        /// <summary>
        ///     Sets the <see cref="VisualToolTip" /> text associated with the specified control, and displays the
        ///     <see cref="VisualToolTip" /> modally.
        /// </summary>
        /// <param name="text">A <see cref="string" /> containing the new <see cref="VisualToolTip" /> text.</param>
        /// <param name="window">The <see cref="Control" /> to display the <see cref="VisualToolTip" /> for.</param>
        /// <param name="x">
        ///     The horizontal offset, in pixels, relative to the upper-left corner of the associated control window,
        ///     to display the <see cref="VisualToolTip" />.
        /// </param>
        /// <param name="y">
        ///     The vertical offset, in pixels, relative to the upper-left corner of the associated control window, to
        ///     display the <see cref="VisualToolTip" />.
        /// </param>
        /// <param name="duration">
        ///     An <see cref="int" /> containing the duration, in milliseconds, to display the
        ///     <see cref="VisualToolTip" />.
        /// </param>
        void Show(string text, IWin32Window window, int x, int y, int duration);

        #endregion
    }
}