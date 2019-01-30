#region License

// -----------------------------------------------------------------------------------------------------------
// 
// Name: User32.cs
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

#endregion

#region Namespace

using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Security;

using VisualPlus.Structure;

#endregion

namespace VisualPlus.Native
{
    /// <summary>Represents the <see cref="User32" /> class.</summary>
    /// <remarks>For the assistance of accessing unmanaged method calls.</remarks>
    [SuppressUnmanagedCodeSecurity]
    public static class User32
    {
        #region Constants

        /// <summary>
        ///     The name of the DLL that contains the unmanaged method. This can include an assembly display name, if the DLL is
        ///     included in an assembly.
        /// </summary>
        private const string DllName = "user32.dll";

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The CreateWindowEx function creates an overlapped, pop-up, or child window with an extended window style;
        ///     otherwise, this function is identical to the CreateWindow function.
        /// </summary>
        /// <param name="dwExStyle">Specifies the extended window style of the window being created.</param>
        /// <param name="lpClassName">
        ///     Pointer to a null-terminated string or a class atom created by a previous call to the
        ///     RegisterClass or RegisterClassEx function. The atom must be in the low-order word of lpClassName; the high-order
        ///     word must be zero. If lpClassName is a string, it specifies the window class name. The class name can be any name
        ///     registered with RegisterClass or RegisterClassEx, provided that the module that registers the class is also the
        ///     module that creates the window. The class name can also be any of the predefined system class names.
        /// </param>
        /// <param name="lpWindowName">
        ///     Pointer to a null-terminated string that specifies the window name. If the window style
        ///     specifies a title bar, the window title pointed to by lpWindowName is displayed in the title bar. When using
        ///     CreateWindow to create controls, such as buttons, check boxes, and static controls, use lpWindowName to specify the
        ///     text of the control. When creating a static control with the SS_ICON style, use lpWindowName to specify the icon
        ///     name or identifier. To specify an identifier, use the syntax "#num".
        /// </param>
        /// <param name="dwStyle">
        ///     Specifies the style of the window being created. This parameter can be a combination of window
        ///     styles, plus the control styles indicated in the Remarks section.
        /// </param>
        /// <param name="x">
        ///     Specifies the initial horizontal position of the window. For an overlapped or pop-up window, the x
        ///     parameter is the initial x-coordinate of the window's upper-left corner, in screen coordinates. For a child window,
        ///     x is the x-coordinate of the upper-left corner of the window relative to the upper-left corner of the parent
        ///     window's client area. If x is set to CW_USEDEFAULT, the system selects the default position for the window's
        ///     upper-left corner and ignores the y parameter. CW_USEDEFAULT is valid only for overlapped windows; if it is
        ///     specified for a pop-up or child window, the x and y parameters are set to zero.
        /// </param>
        /// <param name="y">
        ///     Specifies the initial vertical position of the window. For an overlapped or pop-up window, the y parameter is the
        ///     initial y-coordinate of the window's upper-left corner, in screen coordinates. For a child window, y is the initial
        ///     y-coordinate of the upper-left corner of the child window relative to the upper-left corner of the parent window's
        ///     client area. For a list box y is the initial y-coordinate of the upper-left corner of the list box's client area
        ///     relative to the upper-left corner of the parent window's client area.
        ///     <para>
        ///         If an overlapped window is created with the WS_VISIBLE style bit set and the x parameter is set to
        ///         CW_USEDEFAULT, then the y parameter determines how the window is shown. If the y parameter is CW_USEDEFAULT,
        ///         then the window manager calls ShowWindow with the SW_SHOW flag after the window has been created. If the y
        ///         parameter is some other value, then the window manager calls ShowWindow with that value as the nCmdShow
        ///         parameter.
        ///     </para>
        /// </param>
        /// <param name="nWidth">
        ///     Specifies the width, in device units, of the window. For overlapped windows, nWidth is the
        ///     window's width, in screen coordinates, or CW_USEDEFAULT. If nWidth is CW_USEDEFAULT, the system selects a default
        ///     width and height for the window; the default width extends from the initial x-coordinates to the right edge of the
        ///     screen; the default height extends from the initial y-coordinate to the top of the icon area. CW_USEDEFAULT is
        ///     valid only for overlapped windows; if CW_USEDEFAULT is specified for a pop-up or child window, the nWidth and
        ///     nHeight parameter are set to zero.
        /// </param>
        /// <param name="nHeight">
        ///     Specifies the height, in device units, of the window. For overlapped windows, nHeight is the
        ///     window's height, in screen coordinates. If the nWidth parameter is set to CW_USEDEFAULT, the system ignores
        ///     nHeight.
        /// </param>
        /// <param name="hWndParent">
        ///     Handle to the parent or owner window of the window being created. To create a child window or an owned window,
        ///     supply a valid window handle. This parameter is optional for pop-up windows.
        ///     <para>
        ///         Windows 2000/XP: To create a message-only window, supply HWND_MESSAGE or a handle to an existing message-only
        ///         window.
        ///     </para>
        /// </param>
        /// <param name="hMenu">
        ///     Handle to a menu, or specifies a child-window identifier, depending on the window style. For an
        ///     overlapped or pop-up window, hMenu identifies the menu to be used with the window; it can be NULL if the class menu
        ///     is to be used. For a child window, hMenu specifies the child-window identifier, an integer value used by a dialog
        ///     box control to notify its parent about events. The application determines the child-window identifier; it must be
        ///     unique for all child windows with the same parent window.
        /// </param>
        /// <param name="hInstance">Handle to the instance of the module to be associated with the window.</param>
        /// <param name="lpParam">
        ///     Pointer to a value to be passed to the window through the CREATESTRUCT structure (lpCreateParams
        ///     member) pointed to by the lParam param of the WM_CREATE message. This message is sent to the created window by this
        ///     function before it returns.
        /// </param>
        /// <returns>The <see cref="IntPtr" /> window handle.</returns>
        [DllImport(DllName, SetLastError = true)]
        public static extern IntPtr CreateWindowEx(int dwExStyle, string lpClassName, string lpWindowName, int dwStyle, int x, int y, int nWidth, int nHeight, int hWndParent, int hMenu, int hInstance, int lpParam);

        /// <summary>
        ///     The DestroyWindow function destroys the specified window. The function sends WM_DESTROY and WM_NCDESTROY
        ///     messages to the window to deactivate it and remove the keyboard focus from it. The function also destroys the
        ///     window's menu, flushes the thread message queue, destroys timers, removes clipboard ownership, and breaks the
        ///     clipboard viewer chain (if the window is at the top of the viewer chain).
        /// </summary>
        /// <param name="hwnd">Handle to the window to be destroyed.</param>
        /// <returns>
        ///     If the function succeeds, the return value is nonzero. If the function fails, the return value is zero. To get
        ///     extended error information, call GetLastError.
        /// </returns>
        [DllImport(DllName, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern int DestroyWindow(IntPtr hwnd);

        /// <summary>
        ///     Redraws the menu bar of the specified window. If the menu bar changes after the system has created the window,
        ///     this function must be called to draw the changed menu bar.
        /// </summary>
        /// <param name="hWnd">A handle to the window whose menu bar is to be redrawn.</param>
        /// <returns>If the function succeeds, the return value is nonzero.</returns>
        [DllImport(DllName)]
        public static extern bool DrawMenuBar(IntPtr hWnd);

        /// <summary>Retrieves the position of the mouse cursor, in screen coordinates.</summary>
        /// <param name="lpPoint">A pointer to a POINT structure that receives the screen coordinates of the cursor.</param>
        /// <returns>Returns nonzero if successful or zero otherwise. To get extended error information, call GetLastError.</returns>
        [DllImport(DllName, SetLastError = true)]
        public static extern bool GetCursorPos(ref Point lpPoint);

        /// <summary>
        ///     This function retrieves a handle to a display device context (DC) for the client area of a specified window or
        ///     for the entire screen. You can use the returned handle in subsequent graphics display interface (GDI) functions to
        ///     draw in the device context.
        /// </summary>
        /// <param name="hWnd">
        ///     Handle to the window whose device context is to be retrieved. If this value is NULL, GetDCEx
        ///     retrieves the device context for the entire screen.
        /// </param>
        /// <param name="hrgnClip">Specifies a clipping region that may be combined with the visible region of the device context.</param>
        /// <param name="flags">
        ///     Specifies how the device context is created. This parameter can be a combination of the following
        ///     values.
        /// </param>
        /// <returns>
        ///     The handle of the device context for the specified window indicates success. NULL indicates failure. An
        ///     invalid value for the hWnd parameter causes the function to fail.
        /// </returns>
        [DllImport(DllName, ExactSpelling = true)]
        public static extern IntPtr GetDCEx(IntPtr hWnd, IntPtr hrgnClip, int flags);

        /// <summary>The GetMonitorInfo function retrieves information about a display monitor.</summary>
        /// <param name="hMonitor">A handle to the display monitor of interest.</param>
        /// <param name="lpmi">
        ///     A pointer to a MONITORINFO or MONITORINFOEX structure that receives information about the specified
        ///     display monitor.
        /// </param>
        /// <returns>If the function succeeds, the return value is nonzero.</returns>
        [DllImport(DllName, CharSet = CharSet.Auto)]
        public static extern bool GetMonitorInfo(HandleRef hMonitor, [In] [Out] MONITORINFO lpmi);

        /// <summary>
        ///     The GetScrollInfo function retrieves the parameters of a scroll bar, including the minimum and maximum
        ///     scrolling positions, the page size, and the position of the scroll box (thumb).
        /// </summary>
        /// <param name="hwnd">
        ///     Handle to a scroll bar control or a window with a standard scroll bar, depending on the value of the
        ///     fnBar parameter.
        /// </param>
        /// <param name="nBar">Specifies the type of scroll bar for which to retrieve parameters.</param>
        /// <param name="lpsi">
        ///     Pointer to a SCROLLINFO structure. Before calling GetScrollInfo, set the cbSize member to
        ///     sizeof(SCROLLINFO), and set the fMask member to specify the scroll bar parameters to retrieve. Before returning,
        ///     the function copies the specified parameters to the appropriate members of the structure.
        /// </param>
        /// <returns>If the function retrieved any values, the return value is nonzero.</returns>
        [DllImport(DllName, SetLastError = true)]
        public static extern int GetScrollInfo(IntPtr hwnd, int nBar, ref SCROLLINFO lpsi);

        /// <summary>
        ///     Enables the application to access the window menu (also known as the system menu or the control menu) for
        ///     copying and modifying.
        /// </summary>
        /// <param name="hWnd">A handle to the window that will own a copy of the window menu.</param>
        /// <param name="bRevert">
        ///     The action to be taken. If this parameter is FALSE, GetSystemMenu returns a handle to the copy of
        ///     the window menu currently in use. The copy is initially identical to the window menu, but it can be modified. If
        ///     this parameter is TRUE, GetSystemMenu resets the window menu back to the default state. The previous window menu,
        ///     if any, is destroyed.
        /// </param>
        /// <returns>
        ///     If the bRevert parameter is FALSE, the return value is a handle to a copy of the window menu. If the bRevert
        ///     parameter is TRUE, the return value is NULL.
        /// </returns>
        [DllImport(DllName)]
        public static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

        /// <summary>
        ///     The GetWindowDC function retrieves the device context (DC) for the entire window, including title bar, menus,
        ///     and scroll bars. A window device context permits painting anywhere in a window, because the origin of the device
        ///     context is the upper-left corner of the window instead of the client area.
        /// </summary>
        /// <param name="hWnd">
        ///     A handle to the window with a device context that is to be retrieved. If this value is NULL,
        ///     GetWindowDC retrieves the device context for the entire screen.
        /// </param>
        /// <returns>
        ///     If the function succeeds, the return value is a handle to a device context for the specified window. If the
        ///     function fails, the return value is NULL, indicating an error or an invalid hWnd parameter.
        /// </returns>
        [DllImport(DllName, ExactSpelling = true)]
        public static extern IntPtr GetWindowDC(IntPtr hWnd);

        /// <summary>
        ///     Retrieves the dimensions of the bounding rectangle of the specified window. The dimensions are given in screen
        ///     coordinates that are relative to the upper-left corner of the screen.
        /// </summary>
        /// <param name="hWnd">A handle to the window.</param>
        /// <param name="lpRect">
        ///     A pointer to a <see cref="RECT" /> structure that receives the screen coordinates of the
        ///     upper-left and lower-right corners of the window.
        /// </param>
        /// <returns>If the function succeeds, the return value is nonzero.</returns>
        [DllImport(DllName, ExactSpelling = true, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

        /// <summary>Inserts a new menu item into a menu, moving other items down the menu.</summary>
        /// <param name="hMenu">A handle to the menu to be changed.</param>
        /// <param name="uPosition">
        ///     The menu item before which the new menu item is to be inserted, as determined by the uFlags
        ///     parameter.
        /// </param>
        /// <param name="uFlags">
        ///     Controls the interpretation of the uPosition parameter and the content, appearance, and behavior
        ///     of the new menu item.
        /// </param>
        /// <param name="uIDNewItem">
        ///     The identifier of the new menu item or, if the uFlags parameter has the MF_POPUP flag set, a
        ///     handle to the drop-down menu or submenu.
        /// </param>
        /// <param name="lpNewItem">
        ///     The content of the new menu item. The interpretation of lpNewItem depends on whether the uFlags
        ///     parameter includes the MF_BITMAP, MF_OWNERDRAW, or MF_STRING flag, as follows.
        /// </param>
        /// <returns>If the function succeeds, the return value is nonzero.</returns>
        [DllImport(DllName, CharSet = CharSet.Auto)]
        public static extern bool InsertMenu(IntPtr hMenu, uint uPosition, uint uFlags, uint uIDNewItem, string lpNewItem);

        /// <summary>Inserts a new menu item at the specified position in a menu.</summary>
        /// <param name="hmenu">A handle to the menu in which the new menu item is inserted.</param>
        /// <param name="item">
        ///     The identifier or position of the menu item before which to insert the new item. The meaning of this
        ///     parameter depends on the value of fByPosition.
        /// </param>
        /// <param name="fByPosition">
        ///     Controls the meaning of uItem. If this parameter is FALSE, uItem is a menu item identifier.
        ///     Otherwise, it is a menu item position.
        /// </param>
        /// <param name="lpmi">A pointer to a MENUITEMINFO structure that contains information about the new menu item.</param>
        /// <returns>If the function succeeds, the return value is nonzero.</returns>
        [DllImport(DllName, CharSet = CharSet.Auto)]
        public static extern bool InsertMenuItem(IntPtr hmenu, uint item, bool fByPosition, [In] ref MENUITEMINFO lpmi);

        /// <summary>
        ///     The MonitorFromWindow function retrieves a handle to the display monitor that has the largest area of
        ///     intersection with the bounding rectangle of a specified window.
        /// </summary>
        /// <param name="hwnd">A handle to the window of interest.</param>
        /// <param name="dwFlags">Determines the function's return value if the window does not intersect any display monitor.</param>
        /// <returns>
        ///     If the window intersects one or more display monitor rectangles, the return value is an HMONITOR handle to the
        ///     display monitor that has the largest area of intersection with the window.
        /// </returns>
        [DllImport(DllName)]
        public static extern IntPtr MonitorFromWindow(IntPtr hwnd, uint dwFlags);

        /// <summary>
        ///     Releases the mouse capture from a window in the current thread and restores normal mouse input processing. A
        ///     window that has captured the mouse receives all mouse input, regardless of the position of the cursor, except when
        ///     a mouse button is clicked while the cursor hot spot is in the window of another thread.
        /// </summary>
        /// <returns>If the function succeeds, the return value is nonzero.</returns>
        [DllImport(DllName)]
        public static extern bool ReleaseCapture();

        /// <summary>
        ///     Sends the specified message to a window or windows. The SendMessage function calls the window procedure for
        ///     the specified window and does not return until the window procedure has processed the message.
        /// </summary>
        /// <param name="hWnd">
        ///     A handle to the window whose window procedure will receive the message. If this parameter is
        ///     HWND_BROADCAST ((HWND)0xffff), the message is sent to all top-level windows in the system, including disabled or
        ///     invisible unowned windows, overlapped windows, and pop-up windows; but the message is not sent to child windows.
        /// </param>
        /// <param name="msg">The message to be sent.</param>
        /// <param name="wParam">Additional w message-specific information.</param>
        /// <param name="lParam">Additional l message-specific information.</param>
        /// <returns>The return value specifies the result of the message processing; it depends on the message sent.</returns>
        [DllImport(DllName, SetLastError = true, CharSet = CharSet.Auto)]
        public static extern int SendMessage(IntPtr hWnd, int msg, int wParam, IntPtr lParam);

        /// <summary>
        ///     Changes the size, position, and Z order of a child, pop-up, or top-level window. These windows are ordered
        ///     according to their appearance on the screen. The topmost window receives the highest rank and is the first window
        ///     in the Z order.
        /// </summary>
        /// <param name="hWnd">A handle to the window.</param>
        /// <param name="hWndInsertAfter">
        ///     A handle to the window to precede the positioned window in the Z order. This parameter
        ///     must be a window handle or one of the following values.
        /// </param>
        /// <param name="x">The new position of the left side of the window, in client coordinates.</param>
        /// <param name="y">The new position of the top of the window, in client coordinates.</param>
        /// <param name="cx">The new width of the window, in pixels.</param>
        /// <param name="cy">The new height of the window, in pixels.</param>
        /// <param name="uFlags">
        ///     The window sizing and positioning flags. This parameter can be a combination of the following
        ///     values.
        /// </param>
        /// <returns>If the function succeeds, the return value is nonzero.</returns>
        [DllImport(DllName, SetLastError = true)]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int cx, int cy, int uFlags);

        /// <summary>
        ///     Displays a shortcut menu at the specified location and tracks the selection of items on the shortcut menu. The
        ///     shortcut menu can appear anywhere on the screen.
        /// </summary>
        /// <param name="hmenu">
        ///     A handle to the shortcut menu to be displayed. This handle can be obtained by calling the
        ///     CreatePopupMenu function to create a new shortcut menu or by calling the GetSubMenu function to retrieve a handle
        ///     to a submenu associated with an existing menu item.
        /// </param>
        /// <param name="fuFlags">Specifies function options.</param>
        /// <param name="x">The horizontal location of the shortcut menu, in screen coordinates.</param>
        /// <param name="y">The vertical location of the shortcut menu, in screen coordinates.</param>
        /// <param name="hwnd">
        ///     A handle to the window that owns the shortcut menu. This window receives all messages from the menu.
        ///     The window does not receive a WM_COMMAND message from the menu until the function returns. If you specify
        ///     TPM_NONOTIFY in the fuFlags parameter, the function does not send messages to the window identified by hwnd.
        ///     However, you must still pass a window handle in hwnd. It can be any window handle from your application.
        /// </param>
        /// <param name="lptpm">
        ///     A pointer to a TPMPARAMS structure that specifies an area of the screen the menu should not
        ///     overlap. This parameter can be NULL.
        /// </param>
        /// <returns>
        ///     If you specify TPM_RETURNCMD in the fuFlags parameter, the return value is the menu-item identifier of the
        ///     item that the user selected. If the user cancels the menu without making a selection, or if an error occurs, the
        ///     return value is zero.
        /// </returns>
        [DllImport(DllName)]
        public static extern int TrackPopupMenuEx(IntPtr hmenu, uint fuFlags, int x, int y, IntPtr hwnd, IntPtr lptpm);

        /// <summary>Retrieves a handle to the window that contains the specified point.</summary>
        /// <param name="point">The point to be checked.</param>
        /// <returns>
        ///     The return value is a handle to the window that contains the point. If no window exists at the given point,
        ///     the return value is NULL. If the point is over a static text control, the return value is a handle to the window
        ///     under the static text control.
        /// </returns>
        [DllImport(DllName)]
        public static extern IntPtr WindowFromPoint(Point point);

        #endregion
    }
}