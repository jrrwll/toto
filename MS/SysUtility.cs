using System;
using System.Runtime.InteropServices;

namespace Toto.MS
{
    public class SysUtility
    {
        /// <summary>
        /// 将一副位图从一个设备场景复制至另一个
        /// hdc1 = graphics1.GetHdc()
        /// graphics1.ReleaseHdc( hdc1 )
        /// </summary>
        /// <param name="hdcDest"> 目标场景graphics.GetHdc() </param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="w"></param>
        /// <param name="h"></param>
        /// <param name="hdcSrc"></param>
        /// <param name="sx"></param>
        /// <param name="sy"></param>
        /// <param name="dwRop"> 13369376 </param>
        /// <returns></returns>
        [DllImport("gdi32.dll")]
        public static extern long Bitblt(IntPtr hdcDest, int x, int y,
            int w, int h, IntPtr hdcSrc, int sx, int sy, int dwRop);




    }
}