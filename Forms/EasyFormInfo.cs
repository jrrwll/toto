using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Toto.Forms
{
    public class EasyFormInfo
    {
        public static void ListSysInfo(TextWriter writer = null)
        {
            var w = writer ?? Console.Out;
            foreach (var i in typeof(SystemInformation).GetProperties(
                BindingFlags.Static | BindingFlags.Public))
            {
                w.WriteLine(i.Name + "=" + i.GetValue(null));
            }
        }

        public static void ListApplication(TextWriter writer = null)
        {
            var w = writer ?? Console.Out;
            foreach (var i in typeof(Application).GetProperties(
                BindingFlags.Static | BindingFlags.Public))
            {
                w.WriteLine(i.Name + "=" + i.GetValue(null));
            }
        }
    }
}
