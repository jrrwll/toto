using System;
using System.Text;
using System.Runtime.InteropServices;

namespace Toto.MS
{
    public class SysAPI
    {
        // ---------------------------------- SystemParametersInfo ----------------------------------
        [DllImport("user32.dll", EntryPoint = "SystemParametersInfo")]
        internal static extern bool SystemParametersInfo(uint uiAction, 
            uint uiParam, ref int pvParam, uint fWinIni);
        
        // SystemParametersInfo: uiAction
        public const uint SPI_GETMOUSESPEED = 112U;// 获取鼠标速度，1-20，默认10
        public const uint SPI_SETMOUSE = 4U; // 设置鼠标
        // SystemParametersInfo: uiParam
        // 已安装Windows Extenson，则返非0
        
        // SystemParametersInfo: fWinIni
        public const uint SPIF_SENDWININICHANGE = 0x0002U;
        public const uint SPIF_UPDATEINIFILE = 0x0001U;
        
        /// <summary>
        /// 获取及设置系统参数
        /// </summary>
        /// <param name="uAction"></param>
        /// <param name="uParam"> 0 </param>
        /// <param name="lpvparam"> null </param>
        /// <param name="fuwinIni"> 1 </param>
        /// <returns></returns>
        [DllImport("user32.dll", EntryPoint = "SystemParametersInfo")]
        internal static extern bool SystemParametersInfo(int uAction, 
            int uParam, string lpvparam, int fuwinIni);

        // lpvparam 壁纸文件路径
        public const int SPI_SETDESKWALLPAPER = 20; // 设置壁纸

        
        // ---------------------------------- GetSystemMetrics ----------------------------------
        [DllImport("user32.dll", EntryPoint = "GetSystemMetrics")]
        internal static extern int GetSystemMetrics(int intcount);
        
        public const int SM_CMOUSEBUTTONS = 43; // 返鼠标按键数目
        public const int SM_SWAPBUTTON = 23; // 左右按键已交换则返非0
        public const int SM_CXCUPSOR = 36; // 标准指针大小
        public const int SM_MOUSEPRESENT = 19; // 安装了鼠标则返非0
        public const int SM_MOUSEWHEELPRESENT = 75; // 安装了带轮鼠标则返非0

        public const int SM_SCREEN_WIDTH = 0; // 屏幕宽度
        public const int SM_SCREEN_HEIGHT = 1; // 屏幕高度


    }
}