#region Namespace

using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

#endregion

namespace VisualPlus.Native
{
    [SuppressUnmanagedCodeSecurity]
    public static class shlwapi
    {
        #region Methods

        /// <summary>Truncates a path to fit within a certain number of characters by replacing path components with ellipses.</summary>
        /// <param name="hDc">A handle to the device context used for font metrics. This value can be NULL.</param>
        /// <param name="pszPath">
        ///     A pointer to a null-terminated string of length MAX_PATH that contains the path to be modified.
        ///     On return, this buffer will contain the modified string.
        /// </param>
        /// <param name="dx">The width, in pixels, in which the string must fit.</param>
        /// <returns>
        ///     Returns TRUE if the path was successfully compacted to the specified width. Returns FALSE on failure, or if
        ///     the base portion of the path would not fit the specified width.
        /// </returns>
        [DllImport("shlwapi.dll", CharSet = CharSet.Auto)]
        public static extern bool PathCompactPath(IntPtr hDc, [In] [Out] StringBuilder pszPath, int dx);

        #endregion
    }
}