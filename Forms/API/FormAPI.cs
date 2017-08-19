using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Toto.Forms.API
{
    public partial class FormAPI
    {
        /// <summary>
        /// 系统热键注册
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="id"></param>
        /// <param name="modifiers"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool RegisterHotKey(IntPtr hWnd, int id, KeyModifiers modifiers, Keys key);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool UnregisterHotKey(IntPtr hWnd, int id);


        /// <summary>
        /// 获取窗口矩阵范围
        /// </summary>
        /// <param name="hwnd"></param>
        /// <param name="lpRect"></param>
        /// <returns></returns>
        [DllImport("user32.dll", EntryPoint = "GetWindowRect")]
        internal static extern int GetWindowRect(int hwnd, ref Rectangle lpRect);

        /// <summary>
        /// 屏蔽窗口的某区域
        /// </summary>
        /// <param name="hwnd"></param>
        /// <param name="lpRect"></param>
        /// <param name="bErase"> 非0导致在重画前先删除 </param>
        /// <returns></returns>
        [DllImport("user32.dll", EntryPoint = "InvalidateRect")]
        internal static extern int InvalidateRect(int hwnd, ref Rectangle lpRect, bool bErase);

    }

    public enum KeyModifiers
    {
        None = 0, Alt = 1, Control = 2
    }
}
