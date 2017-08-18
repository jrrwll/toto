using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Toto.MS
{
    public static class KeyboardAPI
    {
        /// <summary>
        /// 判断虚拟键的状态
        /// 144-NumLock，20-CapsLock
        /// </summary>
        /// <param name="intKey"> 虚拟键键码 </param>
        /// <returns> 非0表示已按下该键 </returns>
        [DllImport("user32.dll", EntryPoint = "GetKeyState")]
        internal static extern int GetKeyState(int intKey);
        
        // ---------------------------------- SendInput ----------------------------------
        /// <summary>
        /// 合成击键，鼠标移动，按键等输入操作
        /// </summary>
        /// <param name="nInputs"> Input结构的大小 </param>
        /// <param name="pInputs"> 指向Input结构数组的指针 </param>
        /// <param name="cbSize">  定义Input结构的大小 </param>
        /// <returns></returns>
        [DllImport("user32.dll", EntryPoint = "SendInput", CharSet = CharSet.Auto)]
        internal static extern uint SendInput( uint nInputs, Input[] pInputs, int cbSize );

        public struct Input
        {
            
        }



        /// <summary>
        /// 系统热键注册
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="id"></param>
        /// <param name="modifiers"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        [DllImport("user32.dll", SetLastError = true)]
        internal static extern bool RegisterHotKey (IntPtr hWnd, int id, KeyModifiers modifiers, Keys key);

        [DllImport("user32.dll", SetLastError = true)]
        internal static extern bool UnregisterHotKey(IntPtr hWnd, int id);



    }

    public enum KeyModifiers
    {
        None=0, Alt=1, Control=2
    }
}