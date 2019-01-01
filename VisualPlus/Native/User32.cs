#region Namespace

using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Security;

using VisualPlus.Structure;

#endregion

namespace VisualPlus.Native
{
    [SuppressUnmanagedCodeSecurity]
    internal static class User32
    {
        #region Methods

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
        [DllImport("user32.dll", SetLastError = true)]
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
        [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern int DestroyWindow(IntPtr hwnd);

        [Description("Retrieves the cursor position in screen coordinates.")]
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool GetCursorPos(ref Point lpPoint);

        [Description("Retrieves the monitor information.")]
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool GetMonitorInfo(HandleRef hmonitor, [In] [Out] MonitorManager info);

        [Description("The GetScrollInfo function retrieves the parameters of a scroll bar.")]
        [DllImport("user32.dll", SetLastError = true)]
        public static extern int GetScrollInfo(IntPtr hWnd, int n, ref ScrollInfo lpScrollInfo);

        [Description("Retrieves the system menu.")]
        [DllImport("user32.dll")]
        public static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

        [Description("Retrieves the monitor from a window handle.")]
        [DllImport("user32.dll")]
        public static extern IntPtr MonitorFromWindow(IntPtr hwnd, uint dwFlags);

        [Description("You can use the ReleaseCapture() function to provide drag functionality.")]
        [DllImport("user32.dll")]
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
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
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
        [DllImport("User32.dll", SetLastError = true)]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int cx, int cy, int uFlags);

        [Description("Displays a popup menu at a specified point. The function also tracks the menu, updating the selection highlight until the user either selects an item or otherwise closes the menu.")]
        [DllImport("user32.dll")]
        public static extern int TrackPopupMenuEx(IntPtr hmenu, uint fuFlags, int x, int y, IntPtr hwnd, IntPtr lptpm);

        [Description("The WindowFromPoint function retrieves a handle to the window that contains the specified point.")]
        [DllImport("user32.dll")]
        public static extern IntPtr WindowFromPoint(Point point);

        #endregion
    }
}