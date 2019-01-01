#region Namespace

using System;
using System.Runtime.InteropServices;
using System.Security;

#endregion

namespace VisualPlus.Native
{
    [SuppressUnmanagedCodeSecurity]
    public static class gdi32
    {
        #region Methods

        /// <summary>
        ///     The BitBlt function performs a bit-block transfer of the color data corresponding to a rectangle of pixels
        ///     from the specified source device context into a destination device context.
        /// </summary>
        /// <param name="hDC">A handle to the destination device context.</param>
        /// <param name="x">The x-coordinate, in logical units, of the upper-left corner of the destination rectangle.</param>
        /// <param name="y">The y-coordinate, in logical units, of the upper-left corner of the destination rectangle.</param>
        /// <param name="nWidth">The width, in logical units, of the source and destination rectangles.</param>
        /// <param name="nHeight">The height, in logical units, of the source and the destination rectangles.</param>
        /// <param name="hSrcDC">A handle to the source device context.</param>
        /// <param name="xSrc">The x-coordinate, in logical units, of the upper-left corner of the source rectangle.</param>
        /// <param name="ySrc">The y-coordinate, in logical units, of the upper-left corner of the source rectangle.</param>
        /// <param name="dwRop">
        ///     A raster-operation code. These codes define how the color data for the source rectangle is to be
        ///     combined with the color data for the destination rectangle to achieve the final color.
        /// </param>
        /// <returns>If the function succeeds, the return value is nonzero.</returns>
        [DllImport("gdi32.dll", EntryPoint = "BitBlt", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        public static extern int BitBlt(IntPtr hDC, int x, int y, int nWidth, int nHeight, IntPtr hSrcDC, int xSrc, int ySrc, int dwRop);

        /// <summary>The CreateRoundRectRgn function creates a rectangular region with rounded corners.</summary>
        /// <param name="x1">Specifies the x-coordinate of the upper-left corner of the region in device units.</param>
        /// <param name="y1">Specifies the y-coordinate of the upper-left corner of the region in device units.</param>
        /// <param name="x2">Specifies the x-coordinate of the lower-right corner of the region in device units.</param>
        /// <param name="y2">Specifies the y-coordinate of the lower-right corner of the region in device units.</param>
        /// <param name="w">Specifies the width of the ellipse used to create the rounded corners in device units.</param>
        /// <param name="h">Specifies the height of the ellipse used to create the rounded corners in device units.</param>
        /// <returns>If the function succeeds, the return value is the handle to the region.</returns>
        [DllImport("gdi32.dll")]
        public static extern IntPtr CreateRoundRectRgn(int x1, int y1, int x2, int y2, int w, int h);

        #endregion
    }
}