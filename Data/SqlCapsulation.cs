using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class SqlCapsulation
    {
        private SqlConnection conn;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source_name">default .</param>
        /// <param name="database_name"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        public SqlCapsulation(string source_name, 
            string database_name, string username, string password)
        {
            string connection_string = string.Format(
                "Data Source={0};Initial Catalog={1};User Id={2};Pwd={3}",
                source_name, database_name, username, password
                );
            this.conn = new SqlConnection(connection_string);
        }
        public SqlCapsulation(string connection_string)
        {
            this.conn = new SqlConnection(connection_string);
        }

        public SqlDataReader GetSqlDataReader( string cmd_text )
        {
            SqlCommand cmd = new SqlCommand(cmd_text, conn);
            conn.Open();
            return cmd.ExecuteReader();
        }
    }


}
