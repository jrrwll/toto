using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Toto.Forms.API
{
    public partial class FromAPI
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


        /// <summary>
        /// 从文件中提取光标
        /// 用法 form.Cursor = GetFormCursor(@"c:\windows\cursors\mycur.cur");
        /// </summary>
        /// <param name="filename">ANI 或 CUR文件</param>
        /// <returns></returns>
        public Cursor GetCursorFromFile(string filename)
        {
            if (filename.ToLower().StartsWith(@"c:\windows\cursors\"))
                throw new ArgumentException(@"filename must be in c:\windows\cursors\");

            var cursor = new Cursor(Cursor.Current.Handle);
            var hcur = LoadCursorFromFile(filename);
            cursor.GetType().InvokeMember("handle",
                BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance
                | BindingFlags.SetField, null, cursor, new object[] { hcur });
            return cursor;
        }

        [DllImport("user32.dll", EntryPoint = "LoadCursorFromFile")]
        public static extern IntPtr LoadCursorFromFile(string filename);

        [DllImport("user32.dll", EntryPoint = "IntLoadCursorFromFile")]
        public static extern int IntLoadCursorFromFile(string filename);

        /// <summary>
        ///  更改系统预定义的鼠标图标
        /// </summary>
        /// <param name="filename"> .ani，.cur 动画指针文件 </param>
        /// <param name="id"> 取值 GlobalAPI.OCR_... </param>
        public void ModifySystemCursor(string filename, int id)
        {
            if (filename.ToLower().StartsWith(@"c:\windows\cursors\"))
                throw new ArgumentException(@"filename must be in c:\windows\cursors\");

            var cur = IntLoadCursorFromFile(filename);
            SetSystemCursor(cur, id);
        }

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
    }
}
