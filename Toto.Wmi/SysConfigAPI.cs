using System.Runtime.InteropServices;
using System.Text;

namespace Toto.MS
{
    public class SysConfigAPI
    {
        // ---------------------------------- WritePrivateProfileString ----------------------------------
        [DllImport("kernel32.dll", EntryPoint = "WritePrivateProfileString")]
        internal static extern long WritePrivateProfileString(string section
            , string kay, string value, string filePath);
        
        /// <summary>
        /// ini文件操作
        /// </summary>
        /// <param name="section"> 节点 </param>
        /// <param name="kay"> 键 </param>
        /// <param name="defval"> 默认值</param>
        /// <param name="retval"> 待填充的值</param>
        /// <param name="size"> retval的大小 </param>
        /// <param name="filePath"> ini文件的路径 </param>
        /// <returns></returns>
        [DllImport("kernel32.dll", EntryPoint = "GetPrivateProfileString")]
        internal static extern int GetPrivateProfileString(string section
            , string kay, string defval, StringBuilder retval, int size, string filePath);
    }
}