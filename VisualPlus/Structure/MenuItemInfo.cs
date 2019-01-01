#region Namespace

using System;
using System.Runtime.InteropServices;

#endregion

namespace VisualPlus.Structure
{
    [StructLayout(LayoutKind.Sequential)]
    public struct MenuItemInfo
    {
        #region Methods

        public uint cbSize;
        public uint fMask;
        public uint fType;
        public uint fState;
        public uint wID;
        public IntPtr hSubMenu;
        public IntPtr hbmpChecked;
        public IntPtr hbmpUnchecked;
        public IntPtr dwItemData;
        public string dwTypeData;
        public uint cch;
        public IntPtr hbmpItem;

        // Return the size of the structure
        public static uint sizeOf
        {
            get
            {
                return (uint)Marshal.SizeOf(typeof(MenuItemInfo));
            }
        }

        #endregion
    }
}