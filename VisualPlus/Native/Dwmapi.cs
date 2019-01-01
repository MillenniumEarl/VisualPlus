#region Namespace

using System.Runtime.InteropServices;
using System.Security;

#endregion

namespace VisualPlus.Native
{
    [SuppressUnmanagedCodeSecurity]
    public static class dwmapi
    {
        #region Methods

        /// <summary>
        ///     Obtains a value that indicates whether Desktop Window Manager (DWM) composition is enabled. Applications on
        ///     machines running Windows 7 or earlier can listen for composition state changes by handling the
        ///     WM_DWMCOMPOSITIONCHANGED notification.
        /// </summary>
        /// <param name="enabled">
        ///     A pointer to a value that, when this function returns successfully, receives TRUE if DWM
        ///     composition is enabled; otherwise, FALSE.
        /// </param>
        [DllImport("dwmapi.dll", CharSet = CharSet.Auto)]
        public static extern void DwmIsCompositionEnabled(out bool enabled);

        #endregion
    }
}