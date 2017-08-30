using System;
using System.Runtime.InteropServices;

namespace Toto.MS
{
    public static partial class WinAPI
    {
        
        
        
        /// <summary>
        /// 获取桌面窗口
        /// </summary>
        /// <returns></returns>
        [DllImport("user32.dll", EntryPoint = "GetDesktopWindow")]
        internal static extern int GetDesktopWindow();

        
        /// <summary>
        /// 刷新窗口
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        [DllImport("user32.dll", EntryPoint = "FlashWindow")]
        internal static extern bool FlashWindow(IntPtr hWnd, bool flags);
        
        /// <summary>
        /// 动画播放窗口
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="timespan"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        [DllImport("user32.dll", EntryPoint = "AnimateWindow")]
        internal static extern bool AnimateWindow(IntPtr hWnd, int timespan, int flags);

        // AnimateWindow: flags
        public const int ANIWIN_LEFTTORIGHT = 1;
        public const int ANIWIN_RIGHTTOLEFT = 2;
        public const int ANIWIN_TOPTOBOTTOM = 4;
        public const int ANIWIN_BOTTOMTOTOP = 8;
        public const int ANIWIN_HIDDEN = 10000;
        public const int ANIWIN_SLIPPING = 40000;
        public const int ANIWIN_FADEIN = 80000;

    }
}