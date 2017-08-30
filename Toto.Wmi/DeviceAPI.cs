using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Toto.MS
{
    public class DeviceAPI
    {
        /// <summary>
        ///  屏幕显示设置
        /// </summary>
        /// <param name="lpDevMode"></param>
        /// <param name="dwFlags"> 0则表面屏幕图形模式要动态改变</param>
        /// <returns></returns>
        [DllImport("user32.dll", EntryPoint = "ChangeDisplaySettings", CharSet = CharSet.Auto)]
        internal static extern int ChangeDisplaySettings([In] ref DEVMODE lpDevMode, int dwFlags);

        public struct DEVMODE
        {
            public short dmSize; // (short) Marshal.SizeOf(typeof(DEVMODE));
            public int dmPelsWidth;
            public int dmPelsHeight;

            public int dmDisplayFrequency; // 刷新率
            public int dmBitsPerPel; // 颜色质量
            public int dmFields; // 在改变显示设置时使用过的字段 |

        }

        [DllImport("winmm.dll", EntryPoint = "mciSendString")]
        internal static extern int mciSendString(string cmd, StringBuilder info, uint len, IntPtr hwnd);

        public const string MCI_CDROM_OPEN = "set cdaudio door open wait";
        public const string MCI_CDROM_CLOSE = "set cdaudio door close wait";
    }
}