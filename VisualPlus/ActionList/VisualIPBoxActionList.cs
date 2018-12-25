#region Namespace

using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing.Design;

using VisualPlus.Toolkit.Controls.Editors;

#endregion

namespace VisualPlus.ActionList
{
    internal class VisualIPBoxActionList : DesignerActionList
    {
        #region Variables

        private VisualIPBox _control;
        private DesignerActionUIService _designerService;

        #endregion

        #region Constructors

        public VisualIPBoxActionList(IComponent component) : base(component)
        {
            _control = (VisualIPBox)component;
            _designerService = (DesignerActionUIService)GetService(typeof(DesignerActionUIService));
        }

        #endregion

        #region Properties

        //[Category("Behaviour")]
        //[Description("Gets or sets a value indicating whether this is a multiline TextBox control.")]
        //[DefaultValue(false)]
        //public virtual bool MultiLine
        //{
        //    get
        //    {
        //        return _control.MultiLine;
        //    }

        //    set
        //    {
        //        _control.MultiLine = value;
        //        _control.Invalidate();
        //    }
        //}

        //[Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        //[Localizable(false)]
        //public virtual string Text
        //{
        //    get
        //    {
        //        return _control.Text;
        //    }

        //    set
        //    {
        //        _control.Text = value;
        //    }
        //}

        #endregion

        #region Overrides

        //public override DesignerActionItemCollection GetSortedActionItems()
        //{
        //    DesignerActionItemCollection items = new DesignerActionItemCollection
        //        {
        //            new DesignerActionPropertyItem("MultiLine", "MultiLine"),
        //            new DesignerActionPropertyItem("Text", "Edit Text:")
        //        };

        //    return items;
        //}

        #endregion
    }
}