namespace VisualPlus.Constants
{
    public class ToolTipConstants
    {
        #region Methods

        public const int CW_USEDEFAULT = unchecked((int)0x80000000);
        public const int SWP_NOACTIVATE = 0x0010;
        public const int SWP_NOMOVE = 0x0002;
        public const int SWP_NOSIZE = 0x0001;
        public const int TOPMOST = -1;
        public const int TTDT_AUTOPOP = 2;
        public const int TTDT_INITIAL = 3;
        public const int TTF_SUBCLASS = 0x0010;
        public const int TTF_TRANSPARENT = 0x0100;
        public const int TTM_ACTIVATE = WM_USER + 1;
        public const int TTM_ADDTOOL = WM_USER + 50;
        public const int TTM_DELTOOL = WM_USER + 51;
        public const int TTM_GETTOOLINFO = WM_USER + 53;
        public const int TTM_SETDELAYTIME = WM_USER + 3;
        public const int TTM_SETMAXTIPWIDTH = WM_USER + 24;
        public const int TTM_SETTIPBKCOLOR = WM_USER + 19;
        public const int TTM_SETTIPTEXTCOLOR = WM_USER + 20;
        public const int TTM_SETTITLE = WM_USER + 33;
        public const int TTM_SETTOOLINFO = WM_USER + 54;
        public const int TTM_UPDATETIPTEXT = WM_USER + 57;
        public const int TTS_ALWAYSTIP = 0x01;
        public const int TTS_BALLOON = 0x40;
        public const int TTS_NOPREFIX = 0x02;
        public const int WM_USER = 0x0400;
        public const int WS_POPUP = unchecked((int)0x80000000);

        #endregion
    }
}