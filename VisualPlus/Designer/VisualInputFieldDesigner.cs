#region Namespace

using System.Collections;
using System.ComponentModel.Design;
using VisualPlus.ActionList;

#endregion

namespace VisualPlus.Designer
{
    internal class VisualInputFieldDesigner : VisualStyleBaseDesigner
    {
        #region Variables

        private DesignerActionListCollection _actionListCollection;

        #endregion

        #region Properties

        /// <summary>Gets the design-time action lists supported by the component associated with the designer.</summary>
        public override DesignerActionListCollection ActionLists
        {
            get
            {
                if (_actionListCollection == null)
                {
                    _actionListCollection = new DesignerActionListCollection
                    {
                        new VisualInputFieldActionList(Component)
                    };
                }

                return _actionListCollection;
            }
        }

        #endregion

        #region Overrides

        protected override void PreFilterProperties(IDictionary properties)
        {
            properties.Remove("ImeMode");
            properties.Remove("Padding");
            properties.Remove("FlatAppearance");
            properties.Remove("FlatStyle");
            properties.Remove("AutoEllipsis");
            properties.Remove("UseCompatibleTextRendering");
            properties.Remove("ImageAlign");
            properties.Remove("ImageIndex");
            properties.Remove("ImageKey");
            properties.Remove("ImageList");
            properties.Remove("TextImageRelation");
            properties.Remove("BackgroundImage");
            properties.Remove("BackgroundImageLayout");
            properties.Remove("UseVisualStyleBackColor");
            properties.Remove("RightToLeft");

            base.PreFilterProperties(properties);
        }

        #endregion
    }
}