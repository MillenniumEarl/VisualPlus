#region Namespace

using System.Collections;
using System.Windows.Forms.Design;

#endregion

namespace VisualPlus.Designer
{
    internal class VisualStyleBaseDesigner : ControlDesigner
    {
        #region Overrides

        protected override void PreFilterProperties(IDictionary properties)
        {
            properties.Remove("AutoEllipsis");
            properties.Remove("BackgroundImage");
            properties.Remove("BackgroundImageLayout");
            properties.Remove("FlatAppearance");
            properties.Remove("FlatStyle");
            properties.Remove("ImageAlign");
            properties.Remove("ImageIndex");
            properties.Remove("ImageKey");
            properties.Remove("ImageList");
            properties.Remove("ImeMode");
            properties.Remove("RightToLeft");
            properties.Remove("UseCompatibleTextRendering");
            properties.Remove("UseVisualStyleBackColor");

            base.PreFilterProperties(properties);
        }

        #endregion
    }
}