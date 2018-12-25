#region Namespace

using System.ComponentModel;
using System.Drawing;
using System.Net;
using System.Runtime.InteropServices;
using VisualPlus.Designer;
using VisualPlus.Toolkit.VisualBase;

#endregion

namespace VisualPlus.Toolkit.Controls.Editors
{
    [ClassInterface(ClassInterfaceType.AutoDispatch)]
    [ComVisible(true)]
    [DefaultEvent("TextChanged")]
    [DefaultProperty("Text")]
    [Description("The Visual IPBox")]
    [Designer(typeof(VisualIPBoxDesigner))]
    [ToolboxBitmap(typeof(VisualIPBox), "VisualIPBox.bmp")]
    [ToolboxItem(false)]
    public class VisualIPBox : VisualStyleBase
    {
        #region Variables

        private int boxSpacing;
        private IPAddress ipAddress;

        #endregion

        #region Constructors

        public VisualIPBox()
        {
            // Variables
            boxSpacing = 2;
            ipAddress = IPAddress.Parse("127.0.0.1");
            Size = new Size(135, 25);

            // TODO: Place box location automatically and resize handle
        }

        #endregion

        #region Properties

        public int BoxSpacing
        {
            get { return boxSpacing; }
            set { boxSpacing = value; }
        }

        /// <summary> Gets or set the IP address.</summary>
        public IPAddress IPAddress
        {
            get { return ipAddress; }

            set
            {
                // TODO: Update numeric boxes
                ipAddress = value;
            }
        }

        #endregion

        #region Overrides

        public override string ToString()
        {
            return nameof(VisualIPBox) + ", Value = " + ipAddress;
        }

        #endregion
    }
}