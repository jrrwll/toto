using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Windows.Forms;
using Microsoft.Win32;

namespace Toto
{
    public class RegEditUtil
    {
        public void Search (ListBox box, RegistryKey main, string son)
        {
            string[] keys;
            string[] vals;
            var rk = main.OpenSubKey(son, true);

            if (rk != null)
            {
                vals = rk.GetValueNames();
                foreach (var v in vals)
                {
                    box.Items.Add(v);
                }

                keys = rk.GetSubKeyNames();
                foreach (var k in keys)
                {
                    Search(box, rk, k);
                }
            }
        }

        public void Backup (string path)
        {
            try
            {
                var proc = new Process();
                proc.StartInfo.FileName = "cmd.exe";
                proc.StartInfo.UseShellExecute = false;
                proc.StartInfo.RedirectStandardInput = true;
                proc.StartInfo.RedirectStandardOutput = true;
                proc.StartInfo.RedirectStandardError = true;
                proc.StartInfo.CreateNoWindow = true;
                proc.Start();
            
                proc.StandardInput.WriteLine( "regedit /e \"" + path  + "\"" );
                MessageBox.Show("注册表备份成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
        public void Restore (string path)
        {
            try
            {
                var proc = new Process();
                proc.StartInfo.FileName = "cmd.exe";
                proc.StartInfo.UseShellExecute = false;
                proc.StartInfo.RedirectStandardInput = true;
                proc.StartInfo.RedirectStandardOutput = true;
                proc.StartInfo.RedirectStandardError = true;
                proc.StartInfo.CreateNoWindow = true;
                proc.Start();
            
                proc.StandardInput.WriteLine( "regedit /s \"" + path  + "\"" );
                MessageBox.Show("注册表还原成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void Disable(bool disable = true)
        {
            var i = disable ? 1 : 0;
            var str = new[] { "启", "禁"};
            
            try
            {
                var rk = Registry.CurrentUser.CreateSubKey(@"Software\Microsoft\"
                        + @"Windows\Current\Version\Policies\System");
                rk.SetValue("DisableRegistryTools", i, RegistryValueKind.DWord);
                rk.Close();
                MessageBox.Show(str[i] + "用注册表成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void Open ()
        {
            Process.Start( Environment.GetEnvironmentVariable("WinDir") + @"\regedit.exe" );
        }

        public void AsociateFile (string app_path, string ext_name, string descr)
        {
            var mime = MimeMapping.GetMimeMapping( ext_name );;
            
            var rk = Registry.ClassesRoot.CreateSubKey(ext_name);
            rk.SetValue("", descr);
            rk.SetValue("Content Type", mime );
            rk = rk.CreateSubKey(@"shell\open\command");
            rk.SetValue("", app_path + " %1");
            rk.Close();
        }
        public void UnasociateFile ( string ext_name )
        {
            var rk = Registry.ClassesRoot.CreateSubKey(ext_name + @"\shell\open");
            rk.DeleteSubKey("command");
            rk.Close();
        }
        
        public void SetRunOnBoot ( string app_path, bool delete = false )
        {
            if (!File.Exists(app_path)) return;

            var name = app_path.Substring(app_path.LastIndexOf("\\") + 1);
            var rk = Registry.LocalMachine.OpenSubKey(@"Software\Microsoft\"
                     + @"Windows\Current\Version\Run", true);
            if (rk == null)
                rk = Registry.LocalMachine.CreateSubKey(@"Software\Microsoft\"
                         + @"Windows\Current\Version\Run");

            if (delete)
            {
                rk.DeleteValue(name, false); rk.Close();
                MessageBox.Show("已从开机启动中移除：" + app_path);
            }
            else
            {
                rk.SetValue(name, app_path); rk.Close();
                MessageBox.Show("已加入开机启动：" + app_path);
            }
            
        }

        public string[] GetSoftwareList ()
        {
            var rk = Registry.LocalMachine.OpenSubKey(@"Software\Microsoft\"
                        + @"Windows\Current\Version\Uninstall");
            var names = rk.GetSubKeyNames();

            return names.Where((e) => e[0] != '{' ).ToArray();
        }

        public void DisableTaskMgr (bool disable = true)
        {
            var i = disable ? 1 : 0;
            var str = new[] { "启", "禁"};
            
            var rk = Registry.LocalMachine.OpenSubKey(
                @"Software\Microsoft\Windows\CurrentVersion\Policies\System");
            if (rk == null)
                rk = Registry.LocalMachine.CreateSubKey(
                    @"Software\Microsoft\Windows\CurrentVersion\Policies\System");       
            
            rk.SetValue("DisableTaskMgr", i);
            rk.Close();
            MessageBox.Show( str[i] + "用任务管理器成功");
            
        }
        
        public void DisableCMD (bool disable = true)
        {
            var i = disable ? 1 : 0;
            var str = new[] { "启", "禁"};
            
            var rk = Registry.LocalMachine.OpenSubKey(
                @"Software\Microsoft\Windows\CurrentVersion\Policies\System");
            if (rk == null)
                rk = Registry.LocalMachine.CreateSubKey(
                    @"Software\Microsoft\Windows\CurrentVersion\Policies\System");       
            
            rk.SetValue("DisableCMD", i);
            rk.Close();
            MessageBox.Show( str[i] + "用命令提示符成功");
        }

        public void AutoEndTasks (bool auto = true)
        {
            var i = auto ? 1 : 0;
            var str = new[] { "启", "禁"};
            
            var rk = Registry.CurrentUser.OpenSubKey(@"Control Panel\Desktop", true);
            rk.SetValue("AutoEndTasks", i);
            rk.Close();
            MessageBox.Show( str[i] + "用自动关闭未响应程序");
        }

        
    }
}