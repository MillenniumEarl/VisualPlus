#region License

// -----------------------------------------------------------------------------------------------------------
// 
// Name: FormConstants.cs
// VisualPlus - The VisualPlus Framework (VPF) for WinForms .NET development.
// 
// Created: 10/12/2018 - 11:45 PM
// Last Modified: 02/01/2019 - 1:23 AM
// 
// Copyright (c) 2016-2019 VisualPlus <https://darkbyte7.github.io/VisualPlus/>
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

#endregion

namespace VisualPlus.Constants
{
    public class FormConstants
    {
        public const int DCX_CACHE = 0x00000002;
        public const int DCX_CLIPCHILDREN = 0x00000008;
        public const int DCX_CLIPSIBLINGS = 0x00000010;
        public const int DCX_EXCLUDERGN = 0x00000040;
        public const int DCX_INTERSECTRGN = 0x00000080;
        public const int DCX_INTERSECTUPDATE = 0x00000200;
        public const int DCX_LOCKWINDOWUPDATE = 0x00000400;
        public const int DCX_NORESETATTRS = 0x00000004;
        public const int DCX_PARENTCLIP = 0x00000020;
        public const int DCX_VALIDATE = 0x00200000;

        // GetDCEx Flags
        public const int DCX_WINDOW = 0x00000001;

        public const uint WM_NCCALCSIZE = 0x83;
        public const uint WM_NCHITTEST = 0x84;

        // Window Messages
        public const uint WM_NCPAINT = 0x85;

        #region Constants

        public const int HT_CAPTION = 0x2;
        public const int HTBOTTOM = 0xF;
        public const int HTBOTTOMLEFT = 0x10;
        public const int HTBOTTOMRIGHT = 0x11;
        public const int HTCLIENT = 0x1;
        public const int HTLEFT = 0xA;
        public const int HTRIGHT = 0xB;
        public const int HTTOP = 0xC;
        public const int HTTOPLEFT = 0xD;
        public const int HTTOPRIGHT = 0xE;
        public const uint MF_BYCOMMAND = 0x00000000;
        public const int MF_BYPOSITION = 0x400;
        public const uint MF_DISABLED = 0x00000002;
        public const int MF_GRAYED = 0x1;
        public const int MF_REMOVE = 0x1000;
        public const int MF_SEPARATOR = 0x800;
        public const uint SC_ARRANGE = 0xF110;
        public const int SC_CLOSE = 0xF060;
        public const uint SC_CONTEXTHELP = 0xF180;
        public const uint SC_DEFAULT = 0xF160;
        public const uint SC_HOTKEY = 0xF150;
        public const uint SC_HSCROLL = 0xF080;
        public const uint SC_KEYMENU = 0xF100;
        public const uint SC_MAXIMIZE = 0xF030;
        public const uint SC_MINIMIZE = 0xF020;
        public const uint SC_MONITORPOWER = 0xF170;
        public const uint SC_MOUSEMENU = 0xF090;
        public const uint SC_MOVE = 0xF010;
        public const uint SC_NEXTWINDOW = 0xF040;
        public const uint SC_PREVWINDOW = 0xF050;
        public const uint SC_RESTORE = 0xF120;
        public const uint SC_SCREENSAVE = 0xF140;
        public const uint SC_SEPARATOR = 0xF00F;
        public const uint SC_SIZE = 0xF000;
        public const uint SC_TASKLIST = 0xF130;
        public const uint SC_VSCROLL = 0xF070;
        public const uint TPM_LEFTALIGN = 0x0000;
        public const uint TPM_RETURNCMD = 0x0100;
        public const int WM_LBUTTONDOWN = 0x0201;
        public const int WM_LBUTTONUP = 0x0202;
        public const int WM_MOUSEMOVE = 0x0200;
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int WM_RBUTTONDOWN = 0x0204;
        public const int WM_SYSCOMMAND = 0x0112;
        public const int WMSZ_BOTTOM = 0x6;
        public const int WMSZ_BOTTOMLEFT = 0x7;
        public const int WMSZ_BOTTOMRIGHT = 0x8;
        public const int WMSZ_LEFT = 0x1;
        public const int WMSZ_RIGHT = 0x2;
        public const int WMSZ_TOP = 0x3;
        public const int WMSZ_TOPLEFT = 0x4;
        public const int WMSZ_TOPRIGHT = 0x5;
        public const int WS_MINIMIZEBOX = 0x20000;
        public const int WS_SYSMENU = 0x00080000;

        #endregion
    }
}