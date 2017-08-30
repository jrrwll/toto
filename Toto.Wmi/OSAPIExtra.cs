using System;
using System.Runtime.InteropServices;
using static Toto.MS.OSAPI;

namespace Toto.MS
{
    public class OSAPIExtra
    {
        public static void Reboot()
        {
            DoExitWin(EWX_FORCE | EWX_REBOOT);
        }

        public static void PowerOff()
        {
            DoExitWin(EWX_FORCE | EWX_POWEROFF);
        }

        public static void LogoOff()
        {
            DoExitWin(EWX_FORCE | EWX_LOGOFF);
        }
        
        private static void DoExitWin(int DoFlag)
        {
            ExitWindowsEx(DoFlag, 0);
        }
        
       
    }
}