#region Namespace

using System.Drawing;

using VisualPlus.Enumerators;
using VisualPlus.Structure;
using VisualPlus.Toolkit.Child;
using VisualPlus.Toolkit.Components;
using VisualPlus.Toolkit.Controls.DataManagement;

#endregion

namespace VisualPlus.Interfaces
{
    /// <summary>The IListViewEmbeddedControl interface.</summary>
    public interface ILVEmbeddedControl
    {
        #region Properties

        VisualListViewItem Item { get; set; }

        VisualListView ListView { get; set; }

        VisualListViewSubItem SubItem { get; set; }

        #endregion

        #region Methods

        /// <summary>Populate the control with settings.</summary>
        /// <param name="item">The item.</param>
        /// <param name="subItem">The sub item.</param>
        /// <param name="listView">The list view.</param>
        /// <returns>The <see cref="bool" />.</returns>
        bool LVEmbeddedControlLoad(VisualListViewItem item, VisualListViewSubItem subItem, VisualListView listView);

        /// <summary>The return text string.</summary>
        /// <returns>The <see cref="string" />.</returns>
        string LVEmbeddedControlReturnText();

        /// <summary>Unload the control.</summary>
        void LVEmbeddedControlUnload();

        #endregion
    }

    /// <summary>The IThemeManager.</summary>
    public interface IThemeManager
    {
        #region Properties

        /// <summary>The style manager.</summary>
        StyleManager ThemeManager { get; set; }

        #endregion
    }

    /// <summary>The ITheme supported control.</summary>
    public interface IThemeSupport
    {
        #region Methods

        /// <summary>Update the control theme.</summary>
        /// <param name="theme">The theme to update with.</param>
        void UpdateTheme(Theme theme);

        #endregion
    }

    public interface IInputMethods
    {
        #region Methods

        void AppendText(string text);

        void Clear();

        void ClearUndo();

        void Copy();

        void Cut();

        void DeselectAll();

        int GetCharFromPosition(Point pt);

        int GetCharIndexFromPosition(Point pt);

        int GetFirstCharIndexFromLine(int lineNumber);

        int GetLineFromCharIndex(int index);

        Point GetPositionFromCharIndex(int index);

        void Paste();

        void ScrollToCaret();

        void Select(int start, int length);

        void SelectAll();

        void Undo();

        #endregion
    }

    public interface IAnimationSupport
    {
        #region Properties

        /// <summary>Gets or sets the animation state.</summary>
        bool Animation { get; set; }

        #endregion

        #region Methods

        /// <summary>Configures the animation settings.</summary>
        /// <param name="effectIncrement">The effect Increment.</param>
        /// <param name="effectType">The effect Type.</param>
        void ConfigureAnimation(double[] effectIncrement, EffectType[] effectType);

        /// <summary>Draws the animation on the graphics.</summary>
        /// <param name="graphics">The specified graphics to draw on.</param>
        void DrawAnimation(Graphics graphics);

        #endregion
    }

    /// <summary>The ITextColor.</summary>
    public interface ITextColor
    {
        #region Properties

        /// <summary>Gets or sets the Disabled state <see cref="Color" />.</summary>
        Color Disabled { get; set; }

        /// <summary>Gets or sets the Enabled state <see cref="Color" />.</summary>
        Color Enabled { get; set; }

        /// <summary>Gets or sets the Hover state <see cref="Color" />.</summary>
        Color Hover { get; set; }

        /// <summary>Gets or sets the Pressed state <see cref="Color" />.</summary>
        Color Pressed { get; set; }

        #endregion
    }
}