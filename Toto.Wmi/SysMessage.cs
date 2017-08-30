using System;
using System.Runtime.InteropServices;

namespace Toto.MS
{
    public class SysMessage
    {
        // ---------------------------------- SendMessage ----------------------------------
        /// <summary>
        /// 将一个消息发送给窗口
        /// </summary>
        /// <param name="hWnd"> 需要接受消息的窗口句柄 </param>
        /// <param name="msg"> 消息的标识符 </param>
        /// <param name="wParam"></param>
        /// <param name="lParam"> 0 </param>
        /// <returns></returns>
        [DllImport("user32.dll", EntryPoint = "SendMessage", CharSet = CharSet.Auto)]
        internal static extern uint SendMessage( IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

        // SendMessage(h, WM_SYSCOMMAND, SC_SCREENSAVE, 0);;
        // 启动屏幕保护
        public const int WM_SYSCOMMAND = 0x0112; 
        public const int SC_SCREENSAVE = 0xf140;
        
        
    }
}