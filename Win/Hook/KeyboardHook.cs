using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Input;

using static Toto.Win.Hook.HookFunction;

namespace Toto.Win.Hook
{
    public class KeyboardHook
    {
        /// <summary>
        /// 按下一个键
        /// </summary>
        public const int WM_KEYDOWN = 0x0100;

        /// <summary>
        /// 释放一个键
        /// </summary>
        public const int WM_KEYUP = 0x0101;

        /// <summary>
        /// 当用户按住ALT键同时按下其它键时提交此消息给拥有焦点的窗口
        /// </summary>
        public const int WM_SYSKEYDOWN = 0x104;

        /// <summary>
        /// 当用户释放一个键同时ALT 键还按着时提交此消息给拥有焦点的窗口
        /// </summary>
        public const int WM_SYSKEYUP = 0x105;

        public event KeyEventHandler OnKeyDown;
        public event KeyEventHandler OnKeyUp;

        /// <summary>
        /// 监视输入到线程消息队列中的键盘消息
        /// </summary>
        public const int WH_KEYBOARD_LL = 13;

        private HookProcedure procedure;
        private static int hook = 0;

        private int keyboardHookProcedure(int nCode, int wParam, IntPtr lParam)
        {
            if(nCode >= 0 && ( OnKeyDown !=null || OnKeyUp != null))
            {
                var khs = (KeyboardHookStruct)Marshal.PtrToStructure(lParam, typeof(KeyboardHookStruct));

                if(OnKeyDown != null && (wParam == WM_KEYDOWN || wParam == WM_SYSKEYDOWN ))
                {
                    var code = khs.vkCode;
                    var e = new KeyEventArgs(Keyboard.PrimaryDevice,
                        Keyboard.PrimaryDevice.ActiveSource, 0, ( Key)code );
                    // 引发事件 OnKeyDown
                    OnKeyDown(this, e);
                }else if (OnKeyUp != null && ( wParam == WM_KEYUP || wParam == WM_SYSKEYUP ))
                {
                    var code = khs.vkCode;
                    var e = new KeyEventArgs(Keyboard.PrimaryDevice,
                        Keyboard.PrimaryDevice.ActiveSource, 0, (Key)code);
                    // 引发事件 OnKeyDown
                    OnKeyUp(this, e);
                }

            }
            return CallNextHookEx(hook, nCode, wParam, lParam);
        }

        public bool Start()
        {
            if (hook == 0)
            {
                procedure = new HookProcedure(keyboardHookProcedure);
                using (var mod = Process.GetCurrentProcess().MainModule)
                {
                    hook = SetWindowsHookEx(WH_KEYBOARD_LL, 
                        procedure, GetModuleHandle(mod.ModuleName), 0);
                }

                if(hook == 0)
                {
                    Stop(); return false;
                }
            }
            return true;
        }

        public bool Stop()
        {
            bool result = true;

            if (hook != 0)
            {
                result = UnhookWindowsHookEx(hook);
                hook = 0;
            }
            return result;
        }

        public KeyboardHook()
        {
            Start();
        }

        ~KeyboardHook()
        {
            Stop();
        }
    }
}