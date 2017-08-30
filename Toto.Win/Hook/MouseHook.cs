using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

using static Toto.Win.Hook.HookFunction;

namespace Toto.Win.Hook
{
    public class MouseHook
    {
        /// <summary>
        /// 当鼠标轮子转动时发送此消息个当前有焦点的控件
        /// </summary>
        public const int WM_MOUSEWHEEL = 0x20A;
        /// <summary>
        /// 双击鼠标中键
        /// </summary>
        public const int WM_MBUTTONDBLCLK = 0x209;
        /// <summary>
        /// 释放鼠标中键
        /// </summary>
        public const int WM_MBUTTONUP = 0x208;
        /// <summary>
        /// 移动鼠标时发生，同WM_MOUSEFIRST
        /// </summary>
        public const int WM_MOUSEMOVE = 0x200;
        /// <summary>
        /// 按下鼠标左键
        /// </summary>
        public const int WM_LBUTTONDOWN = 0x201;
        /// <summary>
        /// 释放鼠标左键
        /// </summary>
        public const int WM_LBUTTONUP = 0x202;
        /// <summary>
        /// 双击鼠标左键
        /// </summary>
        public const int WM_LBUTTONDBLCLK = 0x203;
        /// <summary>
        /// 按下鼠标右键
        /// </summary>
        public const int WM_RBUTTONDOWN = 0x204;
        /// <summary>
        /// 释放鼠标右键
        /// </summary>
        public const int WM_RBUTTONUP = 0x205;
        /// <summary>
        /// 双击鼠标右键
        /// </summary>
        public const int WM_RBUTTONDBLCLK = 0x206;
        /// <summary>
        /// 按下鼠标中键
        /// </summary>
        public const int WM_MBUTTONDOWN = 0x207;

        public event MouseEventHandler OnMouseLeftButtonDown;
        public event MouseEventHandler OnMouseLeftButtonUp;

        public event MouseEventHandler OnMouseMove;
        public event MouseEventHandler OnMouseWheel;

        public event MouseEventHandler OnMouseRightButtonUp;
        public event MouseEventHandler OnMouseRightButtonDown;

        /// <summary>
        /// 监视输入到线程消息队列中的鼠标消息
        /// </summary>
        public const int WH_MOUSE_LL = 14;

        private HookProcedure procedure;
        private static int hook = 0;

        private int mouseHookProcedure(int nCode, int wParam, IntPtr lParam)
        {
            if (nCode >= 0 && ( OnMouseLeftButtonUp != null || OnMouseLeftButtonDown != null ||
                OnMouseMove != null || OnMouseWheel != null || OnMouseRightButtonUp != null ||
                OnMouseRightButtonDown != null ))
            {
                var mhs = (MouseHookStruct)Marshal.PtrToStructure(lParam, typeof(MouseHookStruct));

                if (OnMouseLeftButtonUp != null && wParam == WM_LBUTTONUP)
                {
                    var e = new MouseEventArgs(Mouse.PrimaryDevice, 0);
                    OnMouseLeftButtonUp(this, e);
                } else if ( OnMouseLeftButtonDown != null &&  wParam == WM_LBUTTONDOWN )
                {
                    var e = new MouseEventArgs(Mouse.PrimaryDevice, 0);
                    OnMouseLeftButtonDown(this, e);
                }
                
                else if (OnMouseMove != null && wParam == WM_MOUSEMOVE)
                {
                    var e = new MouseEventArgs(Mouse.PrimaryDevice, 0);
                    OnMouseMove(this, e);
                } else if (OnMouseWheel != null && wParam == WM_MOUSEWHEEL)
                {
                    var e = new MouseEventArgs(Mouse.PrimaryDevice, 0);
                    OnMouseWheel(this, e);
                }
                
                else if (OnMouseRightButtonUp != null && wParam == WM_RBUTTONUP)
                {
                    var e = new MouseEventArgs(Mouse.PrimaryDevice, 0);
                    OnMouseRightButtonUp(this, e);
                } else if (OnMouseRightButtonDown != null && wParam == WM_RBUTTONDOWN)
                {

                    var e = new MouseEventArgs(Mouse.PrimaryDevice, 0);
                    OnMouseRightButtonDown(this, e);
                }

            }
            return CallNextHookEx(hook, nCode, wParam, lParam);
        }

        public bool Start()
        {
            if (hook == 0)
            {
                procedure = new HookProcedure( mouseHookProcedure );
                using (var mod = Process.GetCurrentProcess().MainModule)
                {
                    hook = SetWindowsHookEx(WH_MOUSE_LL,
                        procedure, GetModuleHandle(mod.ModuleName), 0);
                }

                if (hook == 0)
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

        public MouseHook()
        {
            Start();
        }

        ~MouseHook()
        {
            Stop();
        }
    }
}
