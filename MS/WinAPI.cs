using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace Toto.MS
{
    public static partial class WinAPI
    {
        // ---------------------------------- SetWindowLong ----------------------------------
        /// <summary>
        /// 为指定的窗口设置信息
        /// </summary>
        /// <param name="hwnd"> 窗口句柄 </param>
        /// <param name="nIndex"></param>
        /// <param name="dwNewLong"></param>
        /// <returns> 返回设置前的值 </returns>
        [DllImport("user32.dll", EntryPoint = "SetWindowLong")]
        internal static extern uint SetWindowLong(
            IntPtr hwnd, int nIndex, uint dwNewLong);
        
        /// <summary>
        /// 获取指定的窗口的信息
        /// </summary>
        /// <param name="hwnd"></param>
        /// <param name="nIndex"></param>
        /// <returns> 返0表示出错 </returns>
        [DllImport("user32.dll", EntryPoint = "GetWindowLong")]
        internal static extern uint GetWindowLong( IntPtr hwnd, int nIndex );
        
        // SetWindowLong: nIndex
        public const int GWL_EXSTYLE = -20; // 扩展窗口样式
        public const int GWL_STYLE = -16; // 窗口样式
        public const int GWL_WNDPROC = -4; // 该窗口的窗口函数地址
        public const int GWL_HINSTANCE = -6; // 拥有窗口实例的句柄
        public const int GWL_HWNDPARENT = -8; // 窗口的父窗口的句柄
        public const int GWL_ID = -12; // 对话框中一个子窗口的标识符
        public const int GWL_USERDATA = -21; //
        public const int DWL_DLGPROC = 4; // 该窗口的对话框函数地址
        public const int DWL_MSGRESULT = 0; // 对话框函数中处理的一条消息返回的值
        public const int DWL_USER = 8; // 
        
        
        // ---------------------------------- FindWindow ----------------------------------
        /// <summary>
        /// 查找窗口句柄
        /// Shell_TrayWnd  任务栏
        /// </summary>
        /// <param name="lpClassName"> 窗口类名称字符串指针 </param>
        /// <param name="lpWindowName"> 窗口名称 </param>
        /// <returns></returns>
        [DllImport("user32.dll", EntryPoint = "FindWindow")]
        internal static extern int FindWindow(string lpClassName, string lpWindowName);

        /// <summary>
        /// 控制窗口显示状态
        /// </summary>
        /// <param name="hwnd"></param>
        /// <param name="nCmdShow"></param>
        /// <returns></returns>
        [DllImport("user32.dll", EntryPoint = "ShowWindow")]
        internal static extern int ShowWindow(int hwnd, int nCmdShow);
        
        // ShowWindow: nCmdShow
        public const int SW_HIDE = 0; // 隐藏窗口
        public const int SW_MINIMIZE = 6; // 最小化
        public const int SW_RESTORE = 9; // 用原来的大小并及位置显示一个窗口，并激活
        public const int SW_SHOW = 5; // 用当前的大小及位置显示一个窗口，并激活
        public const int SW_SHOWMAXIMIZED = 3; // 最大化并激活
        public const int SW_SHOWMINIMIZED = 2; // 最小化并激活
        public const int SW_SHOWMINNOACTIVE = 7; //最小化，同时不改变活动窗口
        public const int SW_SHOWNA = 8; //用当前的大小及位置显示一个窗口，同时不改变活动窗口
        public const int SW_SHOWNOACTIVATE = 4; //用最近的大小及位置显示一个窗口，同时不改变活动窗口
        public const int SW_SHOWNORMAL = 1; // 同SW_RESTORE


        /// <summary>
        ///  根据条件以查找窗口
        /// </summary>
        /// <param name="hwnd1"> 0表示桌面，否则表示父窗口</param>
        /// <param name="hwnd2"> 从此窗口开始查找 </param>
        /// <param name="lpsz1"> null 欲搜索的类名 </param>
        /// <param name="lpsz2"> null 欲搜索的类名 </param>
        /// <returns></returns>
        [DllImport("user32.dll", EntryPoint = "FindWindowEx")]
        internal static extern int FindWindowEx(int hwnd1, int hwnd2, string lpsz1, string lpsz2);

        /// <summary>
        /// 获取窗口
        /// </summary>
        /// <param name="hwnd"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        [DllImport("user32.dll", EntryPoint = "GetWindow")]
        internal static extern IntPtr GetWindow(IntPtr hwnd, int flags);
        
        /// <summary>
        /// 获取句柄
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        [DllImport("user32.dll", EntryPoint = "WindowFromPoint")]
        internal static extern int WindowFromPoint(int x, int y);
        
        /// <summary>
        /// 获取父窗口
        /// </summary>
        /// <param name="hwnd"></param>
        /// <returns></returns>
        [DllImport("user32.dll", EntryPoint = "GetParent")]
        internal static extern IntPtr GetParent(IntPtr hwnd);
    }
}