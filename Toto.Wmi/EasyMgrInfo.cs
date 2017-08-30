using System;
using System.IO;
using System.Management;

namespace Toto.MS.WMI
{
    public static class EasyMgrInfo
    {
        /// <summary>
        /// CPU序列号
        /// </summary>
        /// <returns></returns>
        public static string GetCPUSerial ()
        {
            string result = null;
            var cpu = new ManagementClass("Win32_Processor");
            var coll = cpu.GetInstances();
            foreach (var obj in coll)
            {
                result = obj.Properties ["processorid"].Value.ToString();
                break;
            }
            return result;
        }
        /// <summary>
        /// 磁盘卷标序列号
        /// </summary>
        /// <param name="device_id"> 取值c, d, e, f, ...</param>
        /// <returns></returns>
        public static string GetDiskSerial (string device_id)
        {
            var disk = new ManagementObject($"Win32_LogicalDisk.DeviceId=\"{device_id}:\"");
            disk.Get();
            return disk.GetPropertyValue("VolumeSerialNumber").ToString();
        }
        /// <summary>
        /// 网卡序列号
        /// </summary>
        /// <returns></returns>
        public static string GetNetAdapterSerial ()
        {
            string result = null;
            var nac = new ManagementClass("Win32_NetworkAdapterConfiguration");
            var coll = nac.GetInstances();
            foreach (var obj in coll)
            {
                if ( (bool)obj["IPEnabled"] )
                    result = obj ["MacAdress"].ToString();
            }
            return result;
        }
        
        
        public static void ListMgrInfo (string param = "Win32_Processor", TextWriter writer = null)
        {
            var w = writer ?? Console.Out;
            string query = "select * from " + param;
            var seacher = new ManagementObjectSearcher(query);

            w.WriteLine( "Query:    " + query);
            w.WriteLine( "----------- the result -------------" );
            foreach (var obj in seacher.Get() )
            {
                w.WriteLine("");
                try
                {
                    w.WriteLine("Name: " + obj ["Name"]);
                }
                catch
                {
                    w.WriteLine( obj.ToString() );
                }

                if (obj.Properties.Count <= 0) continue;
                foreach (var p in obj.Properties )
                {
                    if (p.Value != null && p.Value.ToString() != "")
                    {
                        switch (p.Value.GetType().ToString())
                        {
                             case   "System.String[]":
                                 var strs =  p.Value as string[] ;
                                 foreach (var line1 in strs) w.WriteLine( line1 );
                                 break;
                              case "System.UInt16" :
                                  var shorts =  p.Value as ushort[] ;
                                  var line2  = "";
                                  foreach (var v in shorts) line2 += v + " ";
                                  w.WriteLine(line2);
                                  break;
                              default: w.WriteLine(p.Value);break;
                        }
                    }
                }
                
            }
            w.WriteLine( "------------------------------------" );
        }

        /// <summary>
        /// CPU
        /// </summary>
        public const string Win32_Processor = "Win32_Processor"; 
        /// <summary>
        /// 主板
        /// </summary>
        public const string Win32_BaseBoard = "Win32_BaseBoard";
        /// <summary>
        /// 物理磁盘
        /// </summary>
        public const string Win32_PhysicalMedia = "Win32_PhysicalMedia";
        /// <summary>
        /// 逻辑分区
        /// </summary>
        public const string Win32_LogicalDisk = "Win32_LogicalDisk";
        /// <summary>
        /// 显示设备
        /// </summary>
        public const string Win32_VideoController = "Win32_VideoController";
        /// <summary>
        /// 音频设备
        /// </summary>
        public const string Win32_SoundDevice = "Win32_SoundDevice";
        /// <summary>
        /// 操作系统
        /// </summary>
        public const string Win32_OperatingSystem = "Win32_OperatingSystem";

        public const string Win32_UserAccount = "Win32_UserAccount";
        
        public const string Win32_Group = "Win32_Group";
        /// <summary>
        /// 当前进程
        /// </summary>
        public const string Win32_Process = "Win32_Process";
        
        public const string Win32_Service = "Win32_Service";
        
        public const string Win32_SystemDriver = "Win32_SystemDriver";
        
        public const string Win32_BIOS = "Win32_BIOS";
        
        public const string Win32_PhysicalMemory = "Win32_PhysicalMemory";
        
        public const string Win32_NetworkAdapter = "Win32_NetworkAdapter";
        
        public const string Win32_NetworkProtocol = "Win32_NetworkProtocol";
        
        public const string Win32_Printer = "Win32_Printer";
        
        public const string Win32_Keyboard = "Win32_Keyboard";
        /// <summary>
        /// 鼠标
        /// </summary>
        public const string Win32_PointingDevice = "Win32_PointingDevice";
        /// <summary>
        /// 串口
        /// </summary>
        public const string Win32_SerialPort = "Win32_SerialPort";
        
        public const string Win32_IDEController = "Win32_IDEController";
        /// <summary>
        /// 软驱
        /// </summary>
        public const string Win32_FloppyController = "Win32_FloppyController";
        
        public const string Win32_USBController = "Win32_USBController";
        
        public const string Win32_SCSIController = "Win32_SCSIController";

        public const string Win32_1394Controller = "Win32_1394Controller";
        /// <summary>
        /// 即插即用
        /// </summary>
        public const string Win32_PnPEntity = "Win32_PnPEntity";


    }
}