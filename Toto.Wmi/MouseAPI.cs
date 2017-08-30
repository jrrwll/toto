using System;
using System.Runtime.InteropServices;

namespace Toto.MS
{
    public static class MouseAPI
    {
        [DllImport("user32.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr GetCursorPos(POINT pnt);
        
        [DllImport("user32.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr SetCursorPos(POINT pnt);
        
        
        /// <summary>
        /// 模拟鼠标操作
        /// </summary>
        /// <param name="dwFlags"></param>
        /// <param name="dx">从上次鼠标事件产生以来的移动量</param>
        /// <param name="dy"></param>
        /// <param name="dwData"> 正值则表示鼠标滚轮向前滚动 </param>
        /// <param name="dwExtraInfo"> GetMessageExtraInfo() 可以获取此值</param>
        /// <returns></returns>
        [DllImport("user32.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr mouse_event(int dwFlags, 
            int dx, int dy, int dwData, int dwExtraInfo);
        
        // mouse_event: dwFlags
        public const int MOOSEEVENTF_ABSOLOTE = 0x8000; // dx, dy为绝对坐标，默认相对
        public const int MOOSEEVENTF_MOVE = 0x0001; // 发生移动
        public const int MOOSEEVENTF_LEFTDOWN = 0x0002; // 按下左键
        public const int MOOSEEVENTF_LEFTUP = 0x0004; // 释放左键
        public const int MOOSEEVENTF_RIGHTDOWN = 0x0008; // 
        public const int MOOSEEVENTF_RIGHTUP = 0x0010; // 
        public const int MOOSEEVENTF_MIDDLEDOWN = 0x0020; // 
        public const int MOOSEEVENTF_MIDDLEUP = 0x0040; // 
        public const int MOOSEEVENTF_WHEEL = 0x0800; // 鼠标轮滚动，滚动量为dwData
        
        
        /// <summary>
        /// 返回单击间隔
        /// </summary>
        /// <returns></returns>
        [DllImport("user32.dll", EntryPoint = "GetDoubleClickTime")]
        public static extern int GetDoubleClickTime();
        
        /// <summary>
        /// 设置单击间隔
        /// </summary>
        /// <param name="wCount"> 毫秒间隔 </param>
        /// <returns></returns>
        [DllImport("user32.dll", EntryPoint = "SetDoubleClickTime")]
        public static extern int SetDoubleClickTime(int wCount);

        /// <summary>
        /// 返回光标闪烁频率
        /// </summary>
        /// <returns></returns>
        [DllImport("user32.dll", EntryPoint = "GetCaretBlinkTime")]
        public static extern int GetCaretBlinkTime(); 
        
              
        /// <summary>
        /// 交换鼠标左右按键功能
        /// </summary>
        /// <param name="bSwap"> 非0，则交换鼠标按键，否则恢复正常</param>
        /// <returns> 非0，则之前已经交换过， 否则返0</returns>
        [DllImport("user32.dll", EntryPoint = "LoadCursorFromFile")]
        public static extern int SwapMouseButton(int bSwap);
        
        /// <summary>
        /// 控制鼠标的可视性
        /// </summary>
        /// <param name="bSwap"> true则显示鼠标 </param>
        /// <returns></returns>
        [DllImport("user32.dll", EntryPoint = "ShowCursor")]
        public static extern bool ShowCursor(bool bSwap);
       
        
    }
}