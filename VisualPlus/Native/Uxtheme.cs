#region License

// -----------------------------------------------------------------------------------------------------------
// 
// Name: uxtheme.cs
// 
// Copyright (c) 2016 - 2019 VisualPlus <https://darkbyte7.github.io/VisualPlus/>
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
using System.Runtime.InteropServices;
using System.Security;

using VisualPlus.Structure;

#endregion

namespace VisualPlus.Native
{
    [SuppressUnmanagedCodeSecurity]
    public static class uxtheme
    {
        #region Public Methods and Operators

        /// <summary>Closes the theme data handle.</summary>
        /// <param name="hTheme">Handle to a window's specified theme data. Use OpenThemeData to create an HTHEME.</param>
        [DllImport("uxtheme.dll", ExactSpelling = true)]
        public static extern void CloseThemeData(IntPtr hTheme);

        /// <summary>Draws the border and fill defined by the visual style for the specified control part.</summary>
        /// <param name="hTheme">Handle to a window's specified theme data. Use OpenThemeData to create an HTHEME.</param>
        /// <param name="hdc">HDC used for drawing the theme-defined background image.</param>
        /// <param name="iPartId">Value of type int that specifies the part to draw. </param>
        /// <param name="iStateId">Value of type int that specifies the state of the part to draw.</param>
        /// <param name="pRect">
        ///     Pointer to a RECT structure that contains the rectangle, in logical coordinates, in which the
        ///     background image is drawn.
        /// </param>
        /// <param name="pClipRect">
        ///     Pointer to a RECT structure that contains a clipping rectangle. This parameter may be set to
        ///     NULL.
        /// </param>
        [DllImport("uxtheme.dll", ExactSpelling = true)]
        public static extern void DrawThemeBackground(IntPtr hTheme, IntPtr hdc, int iPartId, int iStateId, ref RECT pRect, ref RECT pClipRect);

        /// <summary>Draws one or more edges defined by the visual style of a rectangle.</summary>
        /// <param name="hTheme">Handle to a window's specified theme data. Use OpenThemeData to create an HTHEME.</param>
        /// <param name="hdc">HDC used for drawing the theme-defined edge.</param>
        /// <param name="iPartId">Value of type int that specifies the part that contains the rectangle.</param>
        /// <param name="iStateId">Value of type int that specifies the state of the part.</param>
        /// <param name="pDestRect">Pointer to a RECT structure that contains, in logical coordinates, the rectangle.</param>
        /// <param name="uEdge">
        ///     UINT that specifies the type of inner and outer edges to draw. This parameter must be a combination
        ///     of one inner-border flag and one outer-border flag, or one of the combination flags.
        /// </param>
        /// <param name="uFlags">UINT that specifies the type of border to draw.</param>
        /// <param name="pContentRect">
        ///     Pointer to a RECT structure that contains, in logical coordinates, the rectangle that
        ///     receives the interior rectangle, if uFlags is set to BF_ADJUST. This parameter may be set to NULL.
        /// </param>
        [DllImport("uxtheme.dll", ExactSpelling = true)]
        public static extern void DrawThemeEdge(IntPtr hTheme, IntPtr hdc, int iPartId, int iStateId, ref RECT pDestRect, uint uEdge, uint uFlags, ref RECT pContentRect);

        /// <summary>Draws an image from an image list with the icon effect defined by the visual style.</summary>
        /// <param name="hTheme">Handle to a window's specified theme data. Use OpenThemeData to create an HTHEME.</param>
        /// <param name="hdc">HDC used for drawing the theme-defined icon.</param>
        /// <param name="iPartId">Value of type int that specifies the part that contains the rectangle.</param>
        /// <param name="iStateId">Value of type int that specifies the state of the part.</param>
        /// <param name="pRect">
        ///     Pointer to a RECT structure that contains, in logical coordinates, the rectangle in which the image
        ///     is drawn.
        /// </param>
        /// <param name="himl">Handle to an image list that contains the image to draw.</param>
        /// <param name="iImageIndex">Value of type int that specifies the index of the image to draw.</param>
        [DllImport("uxtheme.dll", ExactSpelling = true)]
        public static extern void DrawThemeIcon(IntPtr hTheme, IntPtr hdc, int iPartId, int iStateId, ref RECT pRect, IntPtr himl, int iImageIndex);

        /// <summary>Draws the part of a parent control that is covered by a partially-transparent or alpha-blended child control.</summary>
        /// <param name="hwnd">The child control.</param>
        /// <param name="hdc">The child control's DC.</param>
        /// <param name="pRect">
        ///     The area to be drawn. The rectangle is in the child window's coordinates. If this parameter is
        ///     NULL, the area to be drawn includes the entire area occupied by the child control.
        /// </param>
        [DllImport("uxtheme.dll", ExactSpelling = true)]
        public static extern void DrawThemeParentBackground(IntPtr hwnd, IntPtr hdc, ref RECT pRect);

        /// <summary>Draws text using the color and font defined by the visual style.</summary>
        /// <param name="hTheme">Handle to a window's theme data. Use OpenThemeData to create an HTHEME.</param>
        /// <param name="hdc">HDC to use for drawing.</param>
        /// <param name="iPartId">
        ///     The control part that has the desired text appearance. If this value is 0, the text is drawn in
        ///     the default font, or a font selected into the device context.
        /// </param>
        /// <param name="iStateId">The control state that has the desired text appearance.</param>
        /// <param name="pszText">Pointer to a string that contains the text to draw.</param>
        /// <param name="cchText">
        ///     Value of type int that contains the number of characters to draw. If the parameter is set to -1,
        ///     all the characters in the string are drawn.
        /// </param>
        /// <param name="dwTextFlags">
        ///     DWORD that contains one or more values that specify the string's formatting. See Format
        ///     Values for possible parameter values.
        /// </param>
        /// <param name="dwTextFlags2">Not used. Set to zero.</param>
        /// <param name="pRect">
        ///     Pointer to a RECT structure that contains the rectangle, in logical coordinates, in which the text
        ///     is to be drawn. It is recommended to use pExtentRect from GetThemeTextExtent to retrieve the correct coordinates.
        /// </param>
        [DllImport("uxtheme.dll")]
        public static extern void DrawThemeText(IntPtr hTheme, IntPtr hdc, int iPartId, int iStateId, [MarshalAs(UnmanagedType.LPTStr)] string pszText, int cchText, uint dwTextFlags, uint dwTextFlags2, ref RECT pRect);

        /// <summary>Tests if a visual style for the current application is active.</summary>
        /// <returns>Returns one of the following values if true or false.</returns>
        [DllImport("uxtheme.dll", ExactSpelling = true)]
        public static extern int IsThemeActive();

        /// <summary>Opens the theme data for a window and its associated class.</summary>
        /// <param name="hwnd">Handle of the window for which theme data is required.</param>
        /// <param name="pszClassList">Pointer to a string that contains a semicolon-separated list of classes.</param>
        /// <returns>
        ///     OpenThemeData tries to match each class, one at a time, to a class data section in the active theme. If a
        ///     match is found, an associated HTHEME handle is returned. If no match is found NULL is returned.
        /// </returns>
        [DllImport("uxtheme.dll", ExactSpelling = true, CharSet = CharSet.Unicode)]
        public static extern IntPtr OpenThemeData(IntPtr hwnd, [MarshalAs(UnmanagedType.LPTStr)] string pszClassList);

        /// <summary>Causes a window to use a different set of visual style information than its class normally uses.</summary>
        /// <param name="hwnd">Handle to the window whose visual style information is to be changed.</param>
        /// <param name="pszSubAppName">
        ///     Pointer to a string that contains the application name to use in place of the calling
        ///     application's name. If this parameter is NULL, the calling application's name is used.
        /// </param>
        /// <param name="pszSubIdList">
        ///     Pointer to a string that contains a semicolon-separated list of CLSID names to use in place
        ///     of the actual list passed by the window's class. If this parameter is NULL, the ID list from the calling class is
        ///     used.
        /// </param>
        [DllImport("uxtheme.dll", PreserveSig = false, SetLastError = true, ExactSpelling = true, CharSet = CharSet.Unicode)]
        public static extern void SetWindowTheme(IntPtr hwnd, string pszSubAppName, string pszSubIdList);

        #endregion
    }
}