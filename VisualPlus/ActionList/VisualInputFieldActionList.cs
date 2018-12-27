#region Namespace

using System.ComponentModel;
using System.ComponentModel.Design;
using VisualPlus.Toolkit.VisualBase;

#endregion

namespace VisualPlus.ActionList
{
    internal class VisualInputFieldActionList : DesignerActionList
    {
        #region Variables

        private VisualInputField _control;
        private DesignerActionUIService _designerService;

        #endregion

        #region Constructors

        public VisualInputFieldActionList(IComponent component) : base(component)
        {
            _control = (VisualInputField)component;
            _designerService = (DesignerActionUIService)GetService(typeof(DesignerActionUIService));
        }

        #endregion
    }
}