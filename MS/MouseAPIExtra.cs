using System;
using System.Reflection;
using System.Runtime.InteropServices;

using static Toto.MS.MouseAPI;
using static Toto.MS.SysSettingAPI;
using static Toto.MS.SysAPI;
using System.Windows.Forms;

namespace Toto.MS
{
    public class MouseAPIExtra
    {
        /// <summary>
        ///  更改系统预定义的鼠标图标
        /// </summary>
        /// <param name="filename"> .ani，.cur 动画指针文件 </param>
        /// <param name="id"> 取值 GlobalAPI.OCR_... </param>
        public void ModifySystemCursor(string filename, int id)
        {
            if( filename.ToLower().StartsWith(@"c:\windows\cursors\") )
                throw new ArgumentException(@"filename must be in c:\windows\cursors\");
            
            var cur = IntLoadCursorFromFile( filename );
            SetSystemCursor( cur, id);
        }

        /// <summary>
        /// 从文件中提取光标
        /// 用法 form.Cursor = GetFormCursor(@"c:\windows\cursors\mycur.cur");
        /// </summary>
        /// <param name="filename">ANI 或 CUR文件</param>
        /// <returns></returns>
        public Cursor GetCursorFromFile( string filename )
        {
            if( filename.ToLower().StartsWith(@"c:\windows\cursors\") )
                throw new ArgumentException(@"filename must be in c:\windows\cursors\");
            
            var cursor = new Cursor( Cursor.Current.Handle );
            var hcur = LoadCursorFromFile( filename );
            cursor.GetType().InvokeMember("handle",
                BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance
                | BindingFlags.SetField, null, cursor, new object[] {hcur} );
            return cursor;
        }
  
        /// <summary>
        /// 返回鼠标按键个数
        /// </summary>
        /// <returns></returns>
        public static int GetMouseCount()
        {
            return GetSystemMetrics(SM_CMOUSEBUTTONS);
        }

        /// <summary>
        /// 返回鼠标速度
        /// </summary>
        /// <returns> 1-20， 默认10 </returns>
        public int GetMouseSpeed()
        {
            var pvParam = 0;
            SystemParametersInfo(SPI_GETMOUSESPEED, 0, ref pvParam, 0);
            return pvParam;
        }
        
        /// <summary>
        /// 设置鼠标速度
        /// </summary>
        /// <param name="speed"> 1-20， 默认10 </param>
        /// <exception cref="ArgumentException"></exception>
        public void SetMouseSpeed(int speed)
        {
            if (speed <1 || speed >20) 
                throw new ArgumentException("require speed in [1, 20]");
            
            SystemParametersInfo(SPI_SETMOUSE, 0, ref speed, SPIF_UPDATEINIFILE);
        }

        
       


    }
}