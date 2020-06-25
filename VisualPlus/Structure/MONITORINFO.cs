#region License

// -----------------------------------------------------------------------------------------------------------
//
// Name: MONITORINFO.cs
//
// Copyright (c) 2018 - 2019 VisualPlus <https://darkbyte7.github.io/VisualPlus/>
// All Rights Reserved.
//
// -----------------------------------------------------------------------------------------------------------
//
// GNU General Public License v3.0 (GPL-3.0)
//
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, EITHER
// EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED WARRANTIES OF
// MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
//
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.
//
// This file is subject to the terms and conditions defined in the file
// 'LICENSE.md', which should be in the root directory of the source code package.
//
// -----------------------------------------------------------------------------------------------------------

#endregion License

#region Namespace

using System.Runtime.InteropServices;

#endregion Namespace

namespace VisualPlus.Structure
{
    /// <summary>The structure contains information about a display monitor.</summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto, Pack = 4)]
    public struct MONITORINFO
    {
        #region Constants

        /// <summary>Size of a device name string.</summary>
        private const int CCHDEVICENAME = 32;

        #endregion Constants

        #region Fields

        /// <summary>
        ///     A string that specifies the device name of the monitor being used. Most applications have no use for a display
        ///     monitor name,
        ///     and so can save some bytes by using a MONITORINFO structure.
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = CCHDEVICENAME)]
        public char[] DeviceName;

        /// <summary>
        ///     The attributes of the display monitor.
        ///     This member can be the following value:
        ///     1 : MONITORINFOF_PRIMARY
        /// </summary>
        public int Flags;

        /// <summary>
        ///     A RECT structure that specifies the display monitor rectangle, expressed in virtual-screen coordinates.
        ///     Note that if the monitor is not the primary display monitor, some of the rectangle's coordinates may be negative
        ///     values.
        /// </summary>
        public RECT Monitor;

        /// <summary>
        ///     The size, in bytes, of the structure. Set this member to sizeof(MONITORINFOEX) (72) before calling the
        ///     GetMonitorInfo function.
        ///     Doing so lets the function determine the type of structure you are passing to it.
        /// </summary>
        public int Size;

        /// <summary>
        ///     A RECT structure that specifies the work area rectangle of the display monitor that can be used by applications,
        ///     expressed in virtual-screen coordinates. Windows uses this rectangle to maximize an application on the monitor.
        ///     The rest of the area in rcMonitor contains system windows such as the task bar and side bars.
        ///     Note that if the monitor is not the primary display monitor, some of the rectangle's coordinates may be negative
        ///     values.
        /// </summary>
        public RECT WorkArea;

        #endregion Fields

        #region Constructors and Destructors

        /// <summary>Initializes a new instance of the <see cref="MONITORINFO" /> struct.</summary>
        /// <param name="flag">The flag.</param>
        /// <param name="monitor">The monitor.</param>
        /// <param name="workArea">The working area.</param>
        public MONITORINFO(int flag, RECT monitor, RECT workArea)
        {
            Flags = flag;
            Monitor = monitor;
            WorkArea = workArea;
            DeviceName = new char[CCHDEVICENAME];
            Size = Marshal.SizeOf(typeof(MONITORINFO));
        }

        #endregion Constructors and Destructors
    }
}