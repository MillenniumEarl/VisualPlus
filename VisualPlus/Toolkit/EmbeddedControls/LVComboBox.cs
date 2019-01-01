#region Namespace

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms;

using VisualPlus.Interfaces;
using VisualPlus.Toolkit.Child;
using VisualPlus.Toolkit.Controls.DataManagement;

#endregion

namespace VisualPlus.Toolkit.EmbeddedControls
{
    [ToolboxItem(false)]
    public class LVComboBox : ComboBox, ILVEmbeddedControl
    {
        #region Variables

        private Container _components;
        private VisualListViewItem _item;
        private VisualListView _owner;
        private VisualListViewSubItem _subItem;

        #endregion

        #region Constructors

        /// <summary>Initializes a new instance of the <see cref="LVComboBox" /> class.</summary>
        public LVComboBox()
        {
            InitializeComponent();
        }

        #endregion

        #region Properties

        public VisualListViewItem Item
        {
            get
            {
                return _item;
            }

            set
            {
                _item = value;
            }
        }

        public VisualListView ListView
        {
            get
            {
                return _owner;
            }

            set
            {
                _owner = value;
            }
        }

        public VisualListViewSubItem SubItem
        {
            get
            {
                return _subItem;
            }

            set
            {
                _subItem = value;
            }
        }

        #endregion

        #region Overrides

        /// <summary>Cleanup any resources being used.</summary>
        /// <param name="disposing">Indicates whether the method call comes from a <see cref="Dispose" /> method or a finalizer.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _components?.Dispose();
            }

            base.Dispose(disposing);
        }

        protected override void OnGotFocus(EventArgs e)
        {
            Debug.WriteLine("LVTextBox::Got Focus");
            base.OnGotFocus(e);
        }

        protected override void OnLostFocus(EventArgs e)
        {
            Debug.WriteLine("LVTextBox::Lost Focus");
            base.OnLostFocus(e);
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            // TODO: Add custom paint code here

            // Calling the base class OnPaint
            base.OnPaint(pe);
        }

        #endregion

        #region Methods

        public bool LVEmbeddedControlLoad(VisualListViewItem item, VisualListViewSubItem subItem, VisualListView listView)
        {
            _item = item;
            _subItem = subItem;
            _owner = listView;

            Text = _subItem.Text;

            Items.Add("Item1");
            Items.Add("Item2");
            Items.Add("Item3");

            return true;
        }

        public string LVEmbeddedControlReturnText()
        {
            return Text;
        }

        public void LVEmbeddedControlUnload()
        {
            // take information from control and return it to the item
            _subItem.Text = Text;
        }

        /// <summary>Required method for Designer support - do not modify the contents of this method with the code editor.</summary>
        private void InitializeComponent()
        {
            _components = new Container();
        }

        #endregion
    }
}