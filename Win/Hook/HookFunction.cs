using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Toto.Win.Hook
{
    /// <summary>
    /// 回调委托
    /// </summary>
    /// <param name="nCode">0表示此消息(被之前的消息钩子)丢弃，非0表示此消息继续有效</param>
    /// <param name="wParam"></param>
    /// <param name="lParam"></param>
    public delegate int HookProcedure(int nCode, int wParam, IntPtr lParam);


    /// <summary>
    /// 声明键盘钩子的封送结构类型
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct KeyboardHookStruct
    {
        /// <summary>
        /// 虚拟按键码(1--254)
        /// </summary>
        public int vkCode;
        /// <summary>
        /// 硬件按键扫描码
        /// </summary>
        public int scanCode;
        /// <summary>
        /// 键按下：128 抬起：0
        /// </summary>
        public int flags;
        /// <summary>
        /// 消息时间戳间
        /// </summary>
        public int time;
        /// <summary>
        /// 额外信息
        /// </summary>
        public int dwExtraInfo;
    }


    /// <summary>
    /// 声明鼠标钩子的封送结构类型
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct MouseHookStruct
    {
        /// <summary>
        /// Point结构对象，保存鼠标在屏幕上的x,y坐标
        /// </summary>
        public Point pt;
        /// <summary>
        /// 接收到鼠标消息的窗口的句柄
        /// </summary>
        public IntPtr hWnd;
        /// <summary>
        /// hit-test值，详细描述参见WM_NCHITTEST消息
        /// </summary>
        public int wHitTestCode;
        /// <summary>
        /// 指定与本消息联系的额外消息
        /// </summary>
        public int dwExtraInfo;
    }


    public static class HookFunction
    {
        /// <summary>
        /// 安装钩子
        /// </summary>
        /// <param name="hookType"> 钩子类型 </param>
        /// <param name="lpfn"> 钩子发挥作用时的回调函数 </param>
        /// <param name="handle"></param>
        /// <param name="dwThreadId"> 与安装的钩子子程相关联的线程的标识符 </param>
        /// <returns></returns>
        [DllImport("user32.dll", CallingConvention = CallingConvention.StdCall,
            CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern int SetWindowsHookEx(int hookType,
            HookProcedure lpfn, IntPtr handle, int dwThreadId);

        [DllImport("user32.dll", CallingConvention = CallingConvention.StdCall,
            CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern bool UnhookWindowsHookEx(int handle);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern int CallNextHookEx(int idHook, int nCode, int wParam, IntPtr lParam);

        /// <summary>
        /// 模块指针
        /// </summary>
        /// <param name="lpModuleName"></param>
        /// <returns></returns>
        [DllImport("kernel32.dll")]
        public static extern IntPtr GetModuleHandle(string lpModuleName);
    }


}
