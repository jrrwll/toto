using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Toto.Win.Imit
{
    public class MouseImitator
    {
        /// <summary>
        /// 设置鼠标的绝对坐标
        /// </summary>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern bool SetCursorPos(int X, int Y);

        /// <summary>
        /// 设置鼠标相等坐标
        /// </summary>
        /// <param name="flags"></param>
        /// <param name="dx"> 0 </param>
        /// <param name="dy"> 0 </param>
        /// <param name="data"> 0 </param>
        /// <param name="extraInfo"> UIntPtr.Zero </param>
        [DllImport("user32.dll")]
        public static extern void mouse_event(MouseEventFlag flags, int dx, int dy, uint data, UIntPtr extraInfo);

        /// <summary>
        /// IntPtr ptrTaskbar = FindWindow("Shell_TrayWnd", null),返任务栏句柄
        /// </summary>
        /// <param name="strClass"></param>
        /// <param name="strWindow"></param>
        /// <returns> IntPtr.Zero表示未找到 </returns>
        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(string strClass, string strWindow);

        /// <summary>
        /// 指定条件以查找
        /// </summary>
        /// <param name="hwndParent"> new HandleRef(this, ptrTaskbar) </param>
        /// <param name="hwndChildAfter"> new HandleRef(this, IntPtr.Zero) </param>
        /// <param name="strClass"> "Button",则返按钮句柄 </param>
        /// <param name="strWindow"> null </param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern IntPtr FindWindowEx(HandleRef hwndParent, HandleRef hwndChildAfter, string strClass, string strWindow);

        /// <summary>
        ///  
        /// </summary>
        /// <param name="hwnd"> new HandleRef(this, ptrStartBtn) </param>
        /// <param name="rect"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern bool GetWindowRect(HandleRef hwnd, out NativeRect rect);
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct NativeRect
    {
        public int left;
        public int top;
        public int right;
        public int bottom;
    }

    /// <summary>
    /// 鼠标动作的键值
    /// </summary>
    [Flags]
    public enum MouseEventFlag : uint
    {
        Move = 0x0001,
        LeftDown = 0x0002,
        LeftUp = 0x0004,
        RightDown = 0x0008,
        RightUp = 0x0010,
        MiddleDown = 0x0020,
        MiddleUp = 0x0040,
        XDown = 0x0080,
        XUp = 0x0100,
        Wheel = 0x0800,
        VirtualDesk = 0x4000,
        Absolute = 0x8000
    }
}
