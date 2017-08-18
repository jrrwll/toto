using System;
using System.Runtime.InteropServices;

namespace Toto.MS
{
    public class OSAPI
    {
       
        /// <summary>
        /// 格式化磁盘
        /// SHFormatDrive(this.Handle, index, 0xFFFF, 0)
        /// </summary>
        /// <param name="hWnd"> 窗口句柄 </param>
        /// <param name="drive"> 目标磁盘，从0开始 </param>
        /// <param name="fmtID"> 格式化id </param>
        /// <param name="options"> 选项 </param>
        /// <returns></returns>
        [DllImport("user32.dll", EntryPoint = "SHFormatDrive")]
        internal static extern int SHFormatDrive(IntPtr hWnd, int drive, long fmtID, int options);

        [DllImport("kernel32.dll", EntryPoint = "SetComputerName")]
        internal static extern int SetComputerName(string name);
        
        /// <summary>
        /// 清空回收站 SHEmptyRecycleBin(this.Handle, "", 7);
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="root"> null或""  表示全部</param>
        /// <param name="flags"> 1 不确认，2 不显示进度条，4 删完后不出声</param>
        /// <returns></returns>
        [DllImport("shell32.dll")]
        internal static extern int SHEmptyRecycleBin(IntPtr hWnd, string root, int flags);
        
        /// <summary>
        /// 控制计算机的关闭
        /// </summary>
        /// <param name="DoFlag"></param>
        /// <param name="rea"></param>
        /// <returns></returns>
        [DllImport("user32.dll", ExactSpelling = true, SetLastError = true)]
        internal static extern bool ExitWindowsEx(int DoFlag, int rea);
        
        internal const int EWX_LOGOFF = 0x00000000; // 注销
        internal const int EWX_SHUTDOWN = 0x00000001; // 关机
        internal const int EWX_REBOOT = 0x00000002; // 重启
        internal const int EWX_FORCE = 0x00000004; // 强制中止进程
        internal const int EWX_POWEROFF = 0x00000008; //
        internal const int EWX_FORCEIFHUNG = 0x00000010; //

    }
}