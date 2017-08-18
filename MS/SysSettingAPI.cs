using System.Runtime.InteropServices;

namespace Toto.MS
{
    public static class SysSettingAPI
    {
       
        
        
        
        // ---------------------------------- SetSystemCursor ----------------------------------
        [DllImport("user32.dll", EntryPoint = "SetSystemCursor")]
        internal static extern void SetSystemCursor(int hcur, int id);

        // SetSystemCursor: id 
        public const int OCR_APPSTARTING = 32650; // 箭头 & 沙漏
        public const int OCR_NORAAC = 32512; // 箭头
        public const int OCR_CROSS = 32515;  // 十字
        public const int OCR_HAND = 32649; // 手
        public const int OCR_IBEAM = 32649; // I
        public const int OCR_NO = 32649; // 斜杠圆
        
        public const int OCR_SIZEALL = 32646; // 四方位箭头
        public const int OCR_SIZENS = 32645; // 上下箭头
        public const int OCR_SIZENWSE = 32642; // 西北，东南箭头
        public const int OCR_SIZEWE = 32644; //左右箭头
        public const int OCR_UP = 32516; // 左右上箭头
        public const int OCR_WAIT = 32514; // 沙漏
        
        
        // ---------------------------------- SetSystemTime ----------------------------------
        /// <summary>
        /// 东八区时间 - 8小时，UTC时间
        /// </summary>
        /// <param name="lpSystemTime"></param>
        /// <returns></returns>
        [DllImport("kernel32.dll", EntryPoint = "SetSystemTime", CharSet = CharSet.Ansi)]
        internal static extern bool SetSystemTime(ref SYSTEMTIME lpSystemTime);

        public struct SYSTEMTIME
        {
            public short Year;
            public short Month;
            public short DayOfWeek;
            public short Day;
            public short Hour;
            public short Minute;
            public short Second;
            public short Millisecond;
        }
        
        
        
       
    }
}