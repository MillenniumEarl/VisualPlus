#region License

// -----------------------------------------------------------------------------------------------------------
//
// Name: SCROLLINFO.cs
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

using System;
using System.Runtime.InteropServices;

#endregion Namespace

namespace VisualPlus.Structure
{
    /// <summary>The structure contains information about the scrollbar.</summary>
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct SCROLLINFO
    {
        #region Fields

        /// <summary>Specifies the size, in bytes, of this structure. The caller must set this to sizeof(SCROLLINFO).</summary>
        public int cbSize;

        /// <summary>Specifies the scroll bar parameters to set or retrieve.</summary>
        public int fMask;

        /// <summary>Specifies the maximum scrolling position.</summary>
        public int nMax;

        /// <summary>Specifies the minimum scrolling position.</summary>
        public int nMin;

        /// <summary>
        ///     Specifies the page size, in device units. A scroll bar uses this value to determine the appropriate size of
        ///     the proportional scroll box.
        /// </summary>
        public int nPage;

        /// <summary>Specifies the position of the scroll box.</summary>
        public int nPos;

        /// <summary>Specifies the immediate position of a scroll box that the user is dragging.</summary>
        public int nTrackPos;

        #endregion Fields

        #region Constructors and Destructors

        /// <summary>Initializes a new instance of the <see cref="SCROLLINFO" /> struct.</summary>
        /// <param name="mask">The mask.</param>
        /// <param name="min">The minimum.</param>
        /// <param name="max">The maximum.</param>
        /// <param name="page">The page.</param>
        /// <param name="pos">The position.</param>
        /// <param name="trackPosition">The track position.</param>
        public SCROLLINFO(int mask, int min, int max, int page, int pos, int trackPosition)
        {
            fMask = mask;
            nMin = min;
            nMax = max;
            nPage = page;
            nPos = pos;
            nTrackPos = trackPosition;
            cbSize = Marshal.SizeOf(typeof(SCROLLINFO));
        }

        #endregion Constructors and Destructors
    }
}