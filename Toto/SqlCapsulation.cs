using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Linq;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Toto.Data
{
    /// <summary>
    /// 封装一个 SQL Server 连接管理器
    /// </summary>
    public class SqlCapsulation: IDisposable
    {
        private readonly SqlConnection conn;
        public SqlDataAdapter Adapter { get; }

        private bool disposed;
        
        /// <summary>
        /// 建立一个 SQL Server 连接
        /// </summary>
        /// <param name="source_name">default .</param>
        /// <param name="database_name"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        public SqlCapsulation( string source_name,
            string database_name, string username, string password ) :
            this($"Data Source={source_name};Initial Catalog={database_name};"
                 + $"User Id={username};PWD={password}") { }
        
        public SqlCapsulation(string database_name, string username, string password = "") :
            this($@"Server=(local)\EXPRESS;"
                 + $@"User Id={username};PWD={password};Database={database_name}") { }

        public SqlCapsulation(string connection_string)
        {
            conn = new SqlConnection(connection_string);
            Adapter = new SqlDataAdapter();
            disposed = false;
        }

        ~SqlCapsulation ()
        {
            Dispose();
        }
        
        public SqlCommand Cmd ( string cmd_text )
        {
            var command = new SqlCommand(cmd_text, conn);
            Adapter.SelectCommand = command;
            return command;
        }

        public DataContext DC ()
        {
            return new DataContext( conn );
        }
        
        /// <summary>
        /// 接受Transact-SQL语句，返回SqlDataReader
        /// </summary>
        /// <param name="cmd_text"></param>
        /// <returns></returns>
        public SqlDataReader GetReader( string cmd_text )
        {
            conn.Open();
            var reader = new SqlCommand(cmd_text, conn).ExecuteReader();
            conn.Close();
            return reader;
        }
        /// <summary>
        /// 接受Transact-SQL语句，返回XmlReader
        /// </summary>
        /// <param name="cmd_text"></param>
        /// <returns></returns>
        public XmlReader GetXmlReader( string cmd_text )
        {
            conn.Open();
            var reader = new SqlCommand(cmd_text, conn).ExecuteXmlReader();
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
            var reader = new SqlCommand(cmd_text, conn).ExecuteNonQuery();
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
            var reader = new SqlCommand(cmd_text, conn).ExecuteScalar();
            conn.Close();
            return reader;
        }

        public void Dispose ()
        {
            if (!disposed)
            {
                try
                {
                    Adapter.Dispose();
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


}

/*
SqlDataAdapter adapter = new SqlDataAdapter();
adapter.TableMappings.Add("Table", "Suppliers");
DataSet dataSet = new DataSet("Suppliers");
connection.Open();

SqlCommand command = new SqlCommand(
       "SELECT SupplierID, CompanyName FROM dbo.Suppliers;",connection);
command.CommandType = CommandType.Text;     
adapter.Fill(dataSet);   

SqlDataAdapter productsAdapter = new SqlDataAdapter();
productsAdapter.TableMappings.Add("Table", "Products");
SqlCommand productsCommand = new SqlCommand(
       "SELECT ProductID, SupplierID FROM dbo.Products;",connection);
productsAdapter.SelectCommand = productsCommand;
productsAdapter.Fill(dataSet);
                                  
connection.Close();
DataColumn parentColumn =
     dataSet.Tables["Suppliers"].Columns["SupplierID"];
DataColumn childColumn =
     dataSet.Tables["Products"].Columns["SupplierID"];
              
DataRelation relation = new System.Data.DataRelation("SuppliersProducts",
       parentColumn, childColumn);
dataSet.Relations.Add(relation);
                
*/
