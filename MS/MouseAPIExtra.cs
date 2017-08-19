using System;

using static Toto.MS.SysAPI;

namespace Toto.MS
{
    public class MouseAPIExtra
    {
        /// <summary>
        /// 返回鼠标按键个数
        /// </summary>
        /// <returns></returns>
        public static int GetMouseCount()
        {
            return GetSystemMetrics(SM_CMOUSEBUTTONS);
        }

        /// <summary>
        /// 返回鼠标速度
        /// </summary>
        /// <returns> 1-20， 默认10 </returns>
        public int GetMouseSpeed()
        {
            var pvParam = 0;
            SystemParametersInfo(SPI_GETMOUSESPEED, 0, ref pvParam, 0);
            return pvParam;
        }
        
        /// <summary>
        /// 设置鼠标速度
        /// </summary>
        /// <param name="speed"> 1-20， 默认10 </param>
        /// <exception cref="ArgumentException"></exception>
        public void SetMouseSpeed(int speed)
        {
            if (speed <1 || speed >20) 
                throw new ArgumentException("require speed in [1, 20]");
            
            SystemParametersInfo(SPI_SETMOUSE, 0, ref speed, SPIF_UPDATEINIFILE);
        }

        
       


    }
}