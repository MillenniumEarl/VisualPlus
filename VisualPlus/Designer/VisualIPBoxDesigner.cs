#region Namespace

using System.ComponentModel.Design;
using VisualPlus.ActionList;

#endregion

namespace VisualPlus.Designer
{
    internal class VisualIPBoxDesigner : VisualStyleBaseDesigner
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
                        new VisualIPBoxActionList(Component)
                    };
                }

                return _actionListCollection;
            }
        }

        #endregion
    }
}