using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading;

namespace Toto.MS.WMI
{
    public class EasySysInfo
    {
        
        
        public static void ListEnvir(TextWriter writer = null)
        {
            var w = writer ?? Console.Out;
            foreach(var i in typeof(Environment).GetProperties(
                BindingFlags.Static | BindingFlags.Public ))
            {
                w.WriteLine(i.Name + "=" + i.GetValue(null) );
            }
        }
        
        public static void ListDriveInfo(TextWriter writer = null)
        {
            var w = writer ?? Console.Out;
            foreach(var i in typeof(DriveInfo).GetProperties(
                BindingFlags.Static | BindingFlags.Public ))
            {
                w.WriteLine(i.Name + "=" + i.GetValue(null) );
            }
        }
        
        public static void ListOS(TextWriter writer = null)
        {
            var w = writer ?? Console.Out;
            var os = Environment.OSVersion;
            foreach(var i in os.GetType().GetProperties(BindingFlags.Static | BindingFlags.Public) )
            {
                w.WriteLine(i.Name + "=" + i.GetValue(os) );
            }
        }
        
        public static void ListFolder(TextWriter writer = null)
        {
            var w = writer ?? Console.Out;
            var os = Environment.OSVersion;
            foreach(Environment.SpecialFolder i in typeof(Environment.SpecialFolder).GetEnumValues() )
            {
                w.WriteLine(i  + "=" + Environment.GetFolderPath( i ) );
            }
        }
        
        public static void ListProcesses(TextWriter writer = null)
        {
            var w = writer ?? Console.Out;
            var ps = Process.GetProcesses();
            var t = DateTime.Now;
            foreach( var p in ps )
            {
                var span  = t - p.StartTime;
                w.WriteLine(p.ProcessName  + "(id:" + p.Id +
                        ", size:" +  p.WorkingSet64 + ", time:" + span + ")");
            }
        }

        /// <summary>
        /// cpl取值；main鼠标，desk桌面，ncpa网络,mmsys声音
        /// </summary>
        /// <param name="cpl"></param>
        public static void OpenCpl (string cpl)
        {
            Process.Start(cpl + ".cpl");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="action"> 提供this.Close()等方法来中断本程序 </param>
        public static void SetSingleRun (string mutex, Action action)
        {
            bool createNew;
            var m = new Mutex(true, mutex, out createNew);
            if (createNew) m.ReleaseMutex();
            else action();
        }
        
        public static void SetSingleRun ( Action action )
        {
            var mod = Process.GetCurrentProcess().MainModule.ModuleName;
            var name = Path.GetFileNameWithoutExtension(mod);
            var procs = Process.GetProcessesByName(name);
            if (procs.Length > 1) action();
        }


        
    }
    
    
}