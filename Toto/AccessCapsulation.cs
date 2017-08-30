using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Toto.Data
{
    public class AccessCapsulation: IDisposable
    {
        private OleDbConnection conn;
        private bool disposed ;
        
        private AccessCapsulation()
        {
            disposed = false;
        }

        /// <summary>
        /// 建立到mdb文件的Access数据库的加密连接
        /// </summary>
        /// <param name="source_name"> 形如d:\temp.mdb </param>
        /// <param name="password"></param>
        /// <param name="username"></param>
        public AccessCapsulation(string source_name, string password = ""
            , string username = "admin"): this()
        {
            conn = new OleDbConnection( $"Provider=Microsoft.Jet.OLEDB.4.0;" +
                     $"Jet OLEDB:DataBase Password='{password}';" +
                     $"User Id={username};Data Source={source_name}" );
        }
        
        /// <summary>
        /// ODBC 非DSN 连接 SQL Server数据库
        /// </summary>
        /// <param name="username"></param>
        /// <param name="database_name"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static AccessCapsulation Open(string database_name, 
            string username, string password="", string server=@".\EXPRESS")
        {
            var obj = new AccessCapsulation();
            obj.conn = new OleDbConnection($@"driver=SQL Server;server={server};"
                + $@"UID={username};PWD={password};database={database_name}");
            return obj;
        }
        
        /// <summary>
        /// ODBC DSN 连接 SQL Server数据库
        /// </summary>
        /// <param name="dsn_name"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static AccessCapsulation OpenDSN(string dsn_name, 
            string username, string password="")
        {
            var obj = new AccessCapsulation();
            obj.conn = new OleDbConnection($@"DSN={dsn_name};"
                + $@"UID={username};PWD={password};");
            return obj;
        }

        /// <summary>
        /// OLE DB 连接 SQL Server数据库
        /// </summary>
        /// <param name="catalog_name"></param>
        /// <param name="source_name"></param>
        /// <returns></returns>
        public static AccessCapsulation OpenOLEDB( 
            string catalog_name, string source_name = @".\EXPRESS")
        {
            var obj = new AccessCapsulation();
            obj.conn = new OleDbConnection($@"Provider=SQLOLEDB; Data Source={source_name};"
                + $@"Integrated Security=SSPI;Persist Security Info=False;"
                + $@"Initial Catalog={catalog_name}");
            return obj;
        }
        
        /// <summary>
        /// 连接Excel
        /// </summary>
        /// <param name="source_name"> XLS文件路径 </param>
        /// <returns></returns>
        public static AccessCapsulation OpenExcel( string source_name )
        {
            var obj = new AccessCapsulation();
            obj.conn = new OleDbConnection($@"Provider=Microsoft.Jet.OLEDB.4.0;"
                    + $@"Persist Security Info=False;Extended Properties=Excel 8.0;"
                    + $@"Data Source={source_name}");
            return obj;
        }
        /// <summary>
        /// 连接Text
        /// </summary>
        /// <param name="source_name"></param>
        /// <returns></returns>
        public static AccessCapsulation OpenText( string source_name )
        {
            var obj = new AccessCapsulation();
            obj.conn = new OleDbConnection($@"Provider=Microsoft.Jet.OLEDB.4.0;"
                   + $@"Extended Properties=Text;Data Source={source_name}");
            return obj;
        }

        public OleDbCommand Cmd ( string cmd_text )
        {
            return new OleDbCommand(cmd_text, conn);
        }
        
        public DataContext DC ()
        {
            return new DataContext( conn );
        }
        
        /// <summary>
        /// 返回OleDbDataReader
        /// </summary>
        /// <param name="cmd_text"></param>
        /// <returns></returns>
        public OleDbDataReader GetReader( string cmd_text )
        {
            conn.Open();
            var reader = new OleDbCommand(cmd_text, conn).ExecuteReader();
            conn.Close();
            return reader;
        }
        
        /// <summary>
        /// 返回受影响的行数
        /// </summary>
        /// <param name="cmd_text"></param>
        /// <returns></returns>
        public int GetNonQuery( string cmd_text )
        {
            conn.Open();
            var reader = new OleDbCommand(cmd_text, conn).ExecuteNonQuery();
            conn.Close();
            return reader;
        }
        
        /// <summary>
        /// 返回查询的第一行第一列的结果
        /// </summary>
        /// <param name="cmd_text"></param>
        /// <returns></returns>
        public object GetScalar( string cmd_text )
        {
            conn.Open();
            var reader = new OleDbCommand(cmd_text, conn).ExecuteScalar();
            conn.Close();
            return reader;
        }

        ~AccessCapsulation ()
        {
            Dispose();
        }
        
        public void Dispose ()
        {
            if (disposed) return;
            
            try
            {
                conn.Dispose();
            }
            finally
            {
                disposed = true;
                GC.SuppressFinalize(this);
            }
        }
        
    }
}