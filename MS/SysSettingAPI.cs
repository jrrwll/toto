using System.Runtime.InteropServices;

namespace Toto.MS
{
    public static class SysSettingAPI
    {
        
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